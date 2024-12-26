namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Часть цилиндрической передачи (шестерня / колесо).
/// </summary>
public class SpurGearPart
{
	public SpurGearPart(SteelType steelType, ProcessingType processingType, double N, double T)
	{
		SteelType = steelType;
		ProcessingType = processingType;
		n = N;
		t = T;
	}

	public SteelType SteelType { get; set; } // тип стали

	public ProcessingType ProcessingType { get; set; } // тип термообработки

	public double n { get; set; } // частота вращения

	public double t { get; set; } // вращающий момент на вале

	public int L { get; set; } //срок службы привода

	public double i { get; set; } //передаточное отношение

	public WorkMode WorkMode { get; set; } //режим работы

	public bool IsReversible { get; set; } //реверсивность

	public int ManufactoringAccuracy { get; set; } //степень точности изготовления колес

	/// <summary>
	/// Вычисление предела контактной выносливости.
	/// </summary>
	/// <returns>Предел контактной выносливости.</returns>
	public double CalculateSigmaHlim()
	{
		double sigmaHlim;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		//Отсутствует вычисления для поверхностной и объемной закалки
		switch (ProcessingType)
		{
			case ProcessingType.Normalization:
			case ProcessingType.Enhancement:
				{
					sigmaHlim = 1.8 * steelHardness + 65;

					break;
				}
			case ProcessingType.Cementation:
				{
					sigmaHlim = 23 * steelHardness;

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

	/// <summary>
	/// Вычисление коэффициента безопасности.
	/// </summary>
	/// <returns>Коэффициент безопасности.</returns>
	public double CalculateSh()
	{
		double sh = ProcessingType switch
		{
			ProcessingType.Cementation or ProcessingType.Nitriding => 1.2,
			_ => 1.1
		};

		return sh;
	}

	/// <summary>
	/// Вычисление базового числа циклов.
	/// </summary>
	/// <returns>Базовое число циклов.</returns>
	public double CalculateNHG()
	{
		double nhg = 0;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		switch (ProcessingType)
		{
			case ProcessingType.Enhancement:
			case ProcessingType.Normalization:
				{
					nhg = 30 * Math.Pow(steelHardness, 2.4);

					break;
				}
			case ProcessingType.Nitriding:
			case ProcessingType.Cementation:
				{
					double HB = HardnessUnitsGraph.Values.Single(v => Math.Abs(v.HRC - steelHardness) < 0.01).HB;

					nhg = 30 * Math.Pow(HB, 2.4);

					break;
				}
		}

		return Math.Round(nhg / 1000000) * 1000000;
	}

	/// <summary>
	/// Вычисление ресурса работы передачи в часах. 
	/// </summary>
	/// <returns></returns>
	public double CalculateTSigma()
	{
		return L * WorkMode.Kd * WorkMode.Ks * 8;
	}

	/// <summary>
	/// Вычисление эквивалентного числа циклов.
	/// </summary>
	/// <param name="n">n1 либо n2</param>
	/// <returns></returns>
	public double CalculateNHE()
	{
		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		return Math.Round((workModeProperty.MuH * 60 * 1 * n * CalculateTSigma()) / 100000) * 100000;
	}

	/// <summary>
	/// Коэффициент долговечности.
	/// </summary>
	/// <returns></returns>
	public double CalculateZN()
	{
		return Math.Round(Math.Pow(CalculateNHG() / CalculateNHE(), 1.0 / 6.0), 3);
	}

	/// <summary>
	/// Расчет допускаемого контактного напряжения.
	/// </summary>
	/// <returns></returns>
	public double CalculateSigmaH()
	{
		return (CalculateSigmaHlim() / CalculateSh()) * CalculateZN();
	}

	/// <summary>
	/// Коэффициент влияния двухстороннего приложения нагрузки
	/// </summary>
	/// <returns></returns>
	public double CalculateYA()
	{
		return IsReversible ? (0.7 + 0.8) / 2 : 1.0;
	}

	/// <summary>
	/// Вычисление предела выносливости зубьев по напряжениям изгиба.
	/// </summary>
	/// <returns></returns>
	public double CalculateSigmaFlim()
	{
		double sigmaFLim = 0;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		switch (ProcessingType)
		{
			case ProcessingType.Enhancement:
			case ProcessingType.Normalization:
				{
					sigmaFLim = steelHardness * 1.8;

					break;
				}
			case ProcessingType.Nitriding:
				{
					sigmaFLim = 12 * ((surfaceHardness.Item1 * 0.52 + surfaceHardness.Item2 * 0.51) / 2) + 300; // 26..32?

					break;
				}
			case ProcessingType.Cementation:
				{
					sigmaFLim = 12 * ((surfaceHardness.Item1 * 0.57 + surfaceHardness.Item2 * 0.73) / 2) + 300;

					break;
				}
		}

		return sigmaFLim;
	}

	/// <summary>
	/// Вычисление эквивалентного числа циклов.
	/// </summary>
	/// <param name="processingType"></param>
	/// <param name="n"></param>
	/// <returns></returns>
	public double CalculateNFE()
	{
		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType).SurfaceHardness;

		bool isHardnessLessThan350 = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2 > 100;

		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 == isHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		return Math.Round((workModeProperty.MuF * 60 * 1 * n * CalculateTSigma()) / 10000) * 10000;
	}

	/// <summary>
	/// Вычисление коэффициента долговечности.
	/// </summary>
	/// <returns></returns>
	public double CalculateYN()
	{
		double nfg = 4 * Math.Pow(10, 6);

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == ProcessingType).SurfaceHardness;

		bool isHardnessLessThan350 = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2 > 100;

		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 == isHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		double yn = Math.Round(Math.Pow(nfg / CalculateNFE(), 1.0 / workModeProperty.Mf), 3);

		if (isHardnessLessThan350)
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

	/// <summary>
	/// Вычисление Коэффициента безопасности
	/// </summary>
	/// <returns></returns>
	public double CalculateSF()
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

	/// <summary>
	/// Вычисление допускаемого напряжения изгиба
	/// </summary>
	/// <returns></returns>
	public double CalculateSigmaF()
	{
		return Math.Round((CalculateSigmaFlim() / CalculateSF()) * CalculateYA() * CalculateYN());
	}

	/// <summary>
	/// Вычисление максимального допускаемого контактного напряжения.
	/// </summary>
	/// <returns></returns>
	public double CalculateSigmaHMax()
	{
		SteelMechanicalProperty steelProperty =
			SteelMechanicalPropertiesTable.SteelMechanicalProperties.First(s =>
				s.SteelType == SteelType && s.ProcessingType == ProcessingType);

		double steelHardness = (steelProperty.SurfaceHardness.Item1 + steelProperty.SurfaceHardness.Item2) / 2;

		switch (ProcessingType)
		{
			case ProcessingType.Normalization:
			case ProcessingType.Enhancement:
				{
					return Math.Round(2.8 * steelProperty.SigmaT);
				}
			case ProcessingType.Cementation:
				{
					return Math.Round(44 * steelHardness);
				}
			case ProcessingType.Nitriding:
				{
					return Math.Truncate(35 * steelHardness);
				}
		}

		return 0;
	}
}
