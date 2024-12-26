namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Параметры цилиндрической передачи.
/// </summary>
public class SpurGear()
{
	public int L { get; set; } //срок службы привода

	public WorkMode WorkMode { get; set; } //режим работы

	public bool IsReversible { get; set; } //реверсивность

	public int ManufactoringAccuracy { get; set; } //степень точности изготовления колес

	public double n1 { get; set; } //частота вращения шестерни

	public double n2 { get; set; } //частота вращения колеса

	public double i { get; set; } //передаточное отношение

	public double t1 { get; set; } //вращающий момент на вале шестерни

	public double t2 { get; set; } //вращающий момент на вале колеса

	public SteelType SteelType { get; set; } //тип стали колеса и шестерни

	public ProcessingType GearProcessingType { get; set; } //термообработка шестерни

	public ProcessingType WheelProcessingType { get; set; } //термообработка колеса

	/// <summary>
	/// Установка входных параметров.
	/// </summary>
	public void SetGearProperties(int driveLife, WorkMode workMode, bool isReversible, int manufactoringAccuracy, double n1, double n2, double i, double t1, double t2)
	{
		L = driveLife;
		WorkMode = workMode;
		IsReversible = isReversible;
		ManufactoringAccuracy = manufactoringAccuracy;
		this.n1 = n1;
		this.n2 = n2;
		this.i = i;
		this.t1 = t1;
		this.t2 = t2;
	}

	/// <summary>
	/// Выбор стали и термообработки.
	/// </summary>
	public void SetSteelType(SteelType steelType, ProcessingType gearProcessingType, ProcessingType wheelProcessingType)
	{
		SteelType = steelType;

		GearProcessingType = gearProcessingType;

		WheelProcessingType = wheelProcessingType;
	}

	#region Допускаемые контактные напряжения

	/// <summary>
	/// Вычисление предела контактной выносливости.
	/// </summary>
	/// <returns>Предел контактной выносливости.</returns>
	public double CalculateSigmaHlim(ProcessingType processingType)
	{
		double sigmaHlim;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == processingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		//Отсутствует вычисления для поверхностной и объемной закалки
		switch (processingType)
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
	public double CalculateSh(ProcessingType processingType)
	{
		double sh = processingType switch
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
	public double CalculateNHG(ProcessingType processingType)
	{
		double nhg = 0;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == processingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		switch (processingType)
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
	public double CalculateNHE(double n)
	{
		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		return Math.Round((workModeProperty.MuH * 60 * 1 * n * CalculateTSigma()) / 100000) * 100000;
	}

	public double CalculateZN(double nhg, double nhe)
	{
		return Math.Round(Math.Pow(nhg / nhe, 1.0 / 6.0), 3);
	}

	/// <summary>
	/// Расчет среднего допускаемого контактного напряжения.
	/// </summary>
	/// <returns>Среднее допускаемое контактное напряжение.</returns>
	public double CalculateSigmaH()
	{
		double
			sigmaHlim1 = CalculateSigmaHlim(GearProcessingType), //предел контактной выносливости зубьев шестерни
			sigmaHlim2 = CalculateSigmaHlim(WheelProcessingType), //предел контактной выносливости зубьев колеса
			Sh1 = CalculateSh(GearProcessingType), //коэффициент безопасности шестерни
			Sh2 = CalculateSh(WheelProcessingType), //коэффициент безопасности колеса
			Nhg1 = CalculateNHG(GearProcessingType), //базовое число циклов шестерни
			Nhg2 = CalculateNHG(WheelProcessingType), //базовое число циклов колеса
			Nhe1 = CalculateNHE(n1), //эквивалентное число циклов шестерни
			Nhe2 = CalculateNHE(n2), //эквивалентное число циклов колеса
			Zn1 = CalculateZN(Nhg1, Nhe1), //коэффициент долговечности шестерни
			Zn2 = CalculateZN(Nhg2, Nhe2); //коэффициент долговечности колеса

		double
			sigmaH1 = (sigmaHlim1 / Sh1) * Zn1, //допускаемые контактные напряжения шестерни
			sigmaH2 = (sigmaHlim2 / Sh2) * Zn2; //допускаемые контактные напряжения колеса

		double sigmaHm = (sigmaH1 + sigmaH2) / 2; //среднее допускаемое напряжение

		double sigmaHmin = Math.Min(sigmaH1, sigmaH2); //минимальное допускаемое напряжение

		return Math.Round(1.25 * sigmaHmin, 2); //допускаемое контактное напряжение
	}

	#endregion

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
	public double CalculateSigmaFlim(ProcessingType processingType)
	{
		double sigmaFLim = 0;

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == processingType).SurfaceHardness;

		double steelHardness = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2;

		switch (processingType)
		{
			case ProcessingType.Enhancement:
			case ProcessingType.Normalization:
				{
					sigmaFLim = steelHardness * 1.8;

					break;
				}
			case ProcessingType.Nitriding:
				{
					sigmaFLim = 12 * ((surfaceHardness.Item1 * 0.42 + surfaceHardness.Item2 * 0.56) / 2) + 300; // 26..32?

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
	public double CalculateNFE(ProcessingType processingType, double n)
	{
		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == processingType).SurfaceHardness;

		bool isHardnessLessThan350 = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2 > 100;

		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 == isHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		return Math.Round((workModeProperty.MuF * 60 * 1 * n * CalculateTSigma()) / 10000) * 10000;
	}

	/// <summary>
	/// Вычисление коэффициента долговечности.
	/// </summary>
	/// <returns></returns>
	public double CalculateYN(ProcessingType processingType, double n)
	{
		double nfg = 4 * Math.Pow(10, 6);

		(double, double) surfaceHardness = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.First(s => s.SteelType == SteelType && s.ProcessingType == processingType).SurfaceHardness;

		bool isHardnessLessThan350 = (surfaceHardness.Item1 + surfaceHardness.Item2) / 2 > 100;

		var workModeProperty = WorkModePropertiesTable.WorkModeProperties.Single(w =>
			w.IsHardnessLessThan350 == isHardnessLessThan350 && w.WorkModeType == WorkMode.WorkModeType);

		double yn = Math.Round(Math.Pow(nfg / CalculateNFE(processingType, n), 1.0 / workModeProperty.Mf), 3);

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
}
