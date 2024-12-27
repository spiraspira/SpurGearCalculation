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

	public double i { get; set; } //передаточное отношение

	public SpurGearPart Gear { get; set; } //параметры шестерни

	public SpurGearPart Wheel { get; set; } //параметры колеса

	/// <summary>
	/// Установка входных параметров.
	/// </summary>
	public SpurGear(int driveLife, WorkMode workMode, bool isReversible, int manufactoringAccuracy, double i, SpurGearPart gear, SpurGearPart wheel) : this()
	{
		L = driveLife;
		WorkMode = workMode;
		IsReversible = isReversible;
		ManufactoringAccuracy = manufactoringAccuracy;
		this.i = i;
		gear.L = driveLife;
		gear.WorkMode = workMode;
		gear.IsReversible = isReversible;
		gear.ManufactoringAccuracy = manufactoringAccuracy;
		gear.i = i;
		wheel.L = driveLife;
		wheel.WorkMode = workMode;
		wheel.IsReversible = isReversible;
		wheel.ManufactoringAccuracy = manufactoringAccuracy;
		wheel.i = i;
		Gear = gear;
		Wheel = wheel;
	}

	/// <summary>
	/// Расчет среднего допускаемого контактного напряжения.
	/// </summary>
	/// <value>Среднее допускаемое контактное напряжение.</value>
	public double SigmaH
	{
		get
		{
			//double sigmaHm = (Gear.CalculateSigmaH() + Wheel.CalculateSigmaH()) / 2; //среднее допускаемое напряжение

			double sigmaHmin = Math.Min(Gear.SigmaH, Wheel.SigmaH); //минимальное допускаемое напряжение

			return Math.Round(1.25 * sigmaHmin, 2); //допускаемое контактное напряжение
		}
	}

	/// <summary>
	/// Поиск ориентировочных значений коэффициента ширины колеса в таблице.
	/// </summary>
	public WheelWidthCoefficient WheelWidthCoefficient
	{
		get
		{
			return WheelWidthCoefficientTable.WheelWidthCoefficients.First(w =>
				w.IsHardnessLessThan350 == Wheel.IsHardnessLessThan350 &&
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
	/// Делительный диаметр шестерни.
	/// </summary>
	public double d1
	{
		get
		{
			Gear.d = Math.Round(Kd * Math.Pow(Gear.t * KHBeta / (Math.Pow(SigmaH, 2) * PsyBd) * ((i + 1) / i), 1.0 / 3.0), 1);

			return Gear.d;
		}
	}

	/// <summary>
	/// Ширина зубчатых колес.
	/// </summary>
	public double bw
	{
		get
		{
			return Math.Ceiling(Gear.d * PsyBd);
		}
	}

	/// <summary>
	/// Ширина колеса.
	/// </summary>
	public double BW2
	{
		get
		{
			Wheel.Bw = Math.Round(bw);

			return Wheel.Bw;
		}
	}

	/// <summary>
	/// Ширина шестерни.
	/// </summary>
	public double BW1
	{
		get
		{
			var BW1 = BW2 + 6;

			Gear.Bw = BW1;

			return BW1;
		}
	}

	/// <summary>
	/// Коэффициент модуля колеса.
	/// </summary>
	public double PsyM
	{
		get
		{
			return ModulusCoefficientApproximateValuesTable.ModulusCoefficientApproximateValues.First(m =>
				m.TransmissionType == TransmissionType.RegularGear &&
				m.IsHardnessLessThan350 == Wheel.IsHardnessLessThan350).PsyM;
		}
	}

	/// <summary>
	/// Модуль передачи в нормальном сечении.
	/// </summary>
	public double Mn
	{
		get
		{
			return Math.Round(bw / PsyM);
		}
	}

	/// <summary>
	/// Угол наклона зубьев в радианах.
	/// </summary>
	public double betaRad
	{
		get
		{
			return Math.Round(Math.Asin(1.1 * Math.PI * Mn / bw), 3);
		}
	}

	/// <summary>
	/// Угол наклона зубьев в градусах.
	/// </summary>
	public double beta
	{
		get
		{
			return Math.Round(betaRad * 180 / Math.PI, 2);
		}
	}

	/// <summary>
	/// Число зубьев шестерни.
	/// </summary>
	public double z1
	{
		get
		{
			var z1 = Math.Ceiling(d1 * Math.Cos(betaRad) / Mn);

			Gear.z = z1;

			return z1;
		}
	}

	/// <summary>
	/// Число зубьев колеса.
	/// </summary>
	public double z2
	{
		get
		{
			Wheel.z = z1 * i;

			return Wheel.z;
		}
	}
}
