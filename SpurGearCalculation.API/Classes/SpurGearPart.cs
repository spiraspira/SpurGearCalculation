namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Часть цилиндрической передачи (шестерня / колесо).
/// </summary>
public class SpurGearPart(SteelType steelType, ProcessingType processingType, double N, double T)
{
	public SteelType SteelType { get; set; } = steelType; // тип стали

	public ProcessingType ProcessingType { get; set; } = processingType; // тип термообработки

	public double n { get; set; } = N; // частота вращения

	public double t { get; set; } = T; // вращающий момент на вале

	public int L { get; set; } //срок службы привода

	public double i { get; set; } //передаточное отношение

	public WorkMode WorkMode { get; set; } //режим работы

	public bool IsReversible { get; set; } //реверсивность

	public int ManufactoringAccuracy { get; set; } //степень точности изготовления колес

	/// <summary>
	/// Вспомогательный коэффициент для косозубых колес.
	/// </summary>
	public double Kd
	{
		get
		{
			return 680.0;
		}
	}

	/// <summary>
	/// Среднее значение твердости.
	/// </summary>
	public double AverageSurfaceHardness
	{
		get
		{
			return (SteelMechanicalProperty.SurfaceHardness.Item1 + SteelMechanicalProperty.SurfaceHardness.Item2) / 2;
		}
	}

	/// <summary>
	/// Проверка являются ли единицы твердости единицами Бриннеля.
	/// </summary>
	public bool IsHardnessLessThan350
	{
		get
		{
			return (SteelMechanicalProperty.SurfaceHardness.Item1 + SteelMechanicalProperty.SurfaceHardness.Item2) / 2 > 100;
		}
	}

	/// <summary>
	/// Получение параметров режима работы.
	/// </summary>
	public WorkModeProperty WorkModeProperty
	{
		get
		{
			return WorkModePropertiesTable.WorkModeProperties.Single(w =>
				w.IsHardnessLessThan350 == IsHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);
		}
	}

	/// <summary>
	/// Получение параметров марки стали.
	/// </summary>
	public SteelMechanicalProperty SteelMechanicalProperty
	{
		get
		{
			return SteelMechanicalPropertiesTable.SteelMechanicalProperties
				.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType);
		}
	}

	/// <summary>
	/// Вычисление предела контактной выносливости.
	/// </summary>
	/// <value>Предел контактной выносливости.</value>
	public double SigmaHlim
	{
		get
		{
			double sigmaHlim;

			//Отсутствует вычисления для поверхностной и объемной закалки
			switch (ProcessingType)
			{
				case ProcessingType.Normalization:
				case ProcessingType.Enhancement:
					{
						sigmaHlim = 1.8 * AverageSurfaceHardness + 65;

						break;
					}
				case ProcessingType.Cementation:
					{
						sigmaHlim = 23 * AverageSurfaceHardness;

						break;
					}
				case ProcessingType.Nitriding:
					{
						sigmaHlim = 880;

						break;
					}
				default:
					{
						sigmaHlim = 0;

						break;
					}
			}

			return sigmaHlim;
		}
	}

	/// <summary>
	/// Вычисление коэффициента безопасности.
	/// </summary>
	/// <value>Коэффициент безопасности.</value>
	public double Sh
	{
		get
		{
			double sh = ProcessingType switch
			{
				ProcessingType.Cementation or ProcessingType.Nitriding => 1.2,
				_ => 1.1
			};

			return sh;
		}
	}

	/// <summary>
	/// Вычисление базового числа циклов.
	/// </summary>
	/// <value>Базовое число циклов.</value>
	public double NHG
	{
		get
		{
			double nhg = 0;

			switch (ProcessingType)
			{
				case ProcessingType.Enhancement:
				case ProcessingType.Normalization:
					{
						nhg = 30 * Math.Pow(AverageSurfaceHardness, 2.4);

						break;
					}
				case ProcessingType.Nitriding:
				case ProcessingType.Cementation:
					{
						double HB = HardnessUnitsGraph.Values.Single(v => Math.Abs(v.HRC - AverageSurfaceHardness) < 0.01).HB;

						nhg = 30 * Math.Pow(HB, 2.4);

						break;
					}
			}

			return Math.Round(nhg / 1000000) * 1000000;
		}
	}

	/// <summary>
	/// Вычисление ресурса работы передачи в часах. 
	/// </summary>
	/// <value></value>
	public double TSigma => L * WorkMode.Kd * WorkMode.Ks * 8;

	/// <summary>
	/// Вычисление эквивалентного числа циклов.
	/// </summary>
	/// <value>n1 либо n2</value>
	/// <value></value>
	public double NHE
	{
		get
		{
			return Math.Round((WorkModeProperty.MuH * 60 * 1 * n * TSigma) / 100000) * 100000;
		}
	}

	/// <summary>
	/// Коэффициент долговечности.
	/// </summary>
	/// <value></value>
	public double ZN => Math.Round(Math.Pow(NHG / NHE, 1.0 / 6.0), 3);

	/// <summary>
	/// Расчет допускаемого контактного напряжения.
	/// </summary>
	/// <value></value>
	public double SigmaH => (SigmaHlim / Sh) * ZN;

	/// <summary>
	/// Коэффициент влияния двухстороннего приложения нагрузки
	/// </summary>
	/// <value></value>
	public double YA => IsReversible ? (0.7 + 0.8) / 2 : 1.0;

	/// <summary>
	/// Вычисление предела выносливости зубьев по напряжениям изгиба.
	/// </summary>
	/// <value></value>
	public double SigmaFlim
	{
		get
		{
			double sigmaFLim = 0;

			switch (ProcessingType)
			{
				case ProcessingType.Enhancement:
				case ProcessingType.Normalization:
					{
						sigmaFLim = AverageSurfaceHardness * 1.8;

						break;
					}
				case ProcessingType.Nitriding:
					{
						sigmaFLim = 12 * ((SteelMechanicalProperty.SurfaceHardness.Item1 * 0.52 + SteelMechanicalProperty.SurfaceHardness.Item2 * 0.51) / 2) + 300; // 26..32?

						break;
					}
				case ProcessingType.Cementation:
					{
						sigmaFLim = 12 * ((SteelMechanicalProperty.SurfaceHardness.Item1 * 0.57 + SteelMechanicalProperty.SurfaceHardness.Item2 * 0.73) / 2) + 300;

						break;
					}
			}

			return sigmaFLim;
		}
	}

	/// <summary>
	/// Вычисление эквивалентного числа циклов.
	/// </summary>
	/// <value></value>
	/// <value></value>
	/// <value></value>
	public double NFE
	{
		get
		{
			return Math.Round((WorkModeProperty.MuF * 60 * 1 * n * TSigma) / 10000) * 10000;
		}
	}

	/// <summary>
	/// Вычисление коэффициента долговечности.
	/// </summary>
	/// <value></value>
	public double YN
	{
		get
		{
			double nfg = 4 * Math.Pow(10, 6);

			double yn = Math.Round(Math.Pow(nfg / NFE, 1.0 / WorkModeProperty.Mf), 3);

			if (IsHardnessLessThan350)
			{
				if (yn < 1)
				{
					yn = 1.0;
				}

				if (yn > 4)
				{
					yn = 4.0;
				}
			}
			else
			{
				if (yn < 1)
				{
					yn = 1.0;
				}

				if (yn > 1.25)
				{
					yn = 1.25;
				}
			}

			return yn;
		}
	}

	/// <summary>
	/// Вычисление Коэффициента безопасности
	/// </summary>
	/// <value></value>
	public double SF
	{
		get
		{
			switch (ProcessingType)
			{
				case ProcessingType.Enhancement:
				case ProcessingType.Normalization:
				case ProcessingType.Nitriding:
					{
						return 1.75;
					}
				case ProcessingType.Cementation:
					{
						return 1.55;
					}
			}

			return 0;
		}
	}

	/// <summary>
	/// Вычисление допускаемого напряжения изгиба
	/// </summary>
	/// <value></value>
	public double SigmaF => Math.Round((SigmaFlim / SF) * YA * YN);

	/// <summary>
	/// Вычисление максимального допускаемого контактного напряжения.
	/// </summary>
	/// <value></value>
	public double SigmaHMax
	{
		get
		{
			switch (ProcessingType)
			{
				case ProcessingType.Normalization:
				case ProcessingType.Enhancement:
					{
						return Math.Round(2.8 * SteelMechanicalProperty.SigmaT);
					}
				case ProcessingType.Cementation:
					{
						return Math.Round(44 * AverageSurfaceHardness);
					}
				case ProcessingType.Nitriding:
					{
						return Math.Truncate(35 * AverageSurfaceHardness);
					}
			}

			return 0;
		}
	}

	/// <summary>
	/// Определение максимальной величины коэффициента долговечности.
	/// </summary>
	/// <value></value>
	public double YNMax
	{
		get
		{
			if (AverageSurfaceHardness < 100)
			{
				return 2.5;
			}

			return 4;
		}
	}

	/// <summary>
	/// Определение коэффициента учета частоты приложения пиковой нагрузки.
	/// </summary>
	/// <value></value>
	public double Kst
	{
		get
		{
			if (AverageSurfaceHardness < 100)
			{
				return 1.2;
			}

			return 1.3;
		}
	}

	/// <summary>
	/// Максимальное допускаемое напряжение изгиба.
	/// </summary>
	/// <value></value>
	public double SigmaFMax => Math.Round(0.5 * SigmaFlim * YNMax * Kst);

	/// <summary>
	/// Поиск ориентировочных значений коэффициента ширины колеса в таблице.
	/// </summary>
	public WheelWidthCoefficient WheelWidthCoefficient
	{
		get
		{
			return WheelWidthCoefficientTable.WheelWidthCoefficients.First(w =>
				w.IsHardnessLessThan350 == IsHardnessLessThan350 &&
				w.WheelArrangementType == WheelArrangementType.Asymmetrical);
		}
	}

	/// <summary>
	/// Вычисление коэффициента ширины колеса относительно делительного диаметра.
	/// </summary>
	public double PsyBd
	{
		get
		{
			return 0.5 * WheelWidthCoefficient.AveragePsyBa * (i + 1);
		}
	}

	/// <summary>
	/// Коэффициент концентрации нагрузки. Сделать ввод с клавиатуры.
	/// </summary>
	public double KHBeta
	{
		get
		{
			return 1.04;
		}
	}
}
