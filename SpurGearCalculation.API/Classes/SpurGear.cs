﻿namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Параметры цилиндрической передачи.
/// </summary>
public class SpurGear()
{
	private double m = 0;

	public double Kp { get; set; } //коэффициент перегрузки

	public int L { get; set; } //срок службы привода

	public WorkMode WorkMode { get; set; } //режим работы

	public bool IsReversible { get; set; } //реверсивность

	public int ManufactoringAccuracy { get; set; } //степень точности изготовления колес

	public double i { get; set; }//передаточное отношение

	public SpurGearPart Gear { get; set; } //параметры шестерни

	public SpurGearPart Wheel { get; set; } //параметры колеса

	/// <summary>
	/// Установка входных параметров.
	/// </summary>
	public SpurGear(int driveLife, WorkMode workMode, bool isReversible, int manufactoringAccuracy, double i, double kp, SpurGearPart gear, SpurGearPart wheel) : this()
	{
		L = driveLife;
		WorkMode = workMode;
		IsReversible = isReversible;
		ManufactoringAccuracy = manufactoringAccuracy;
		Kp = kp;
		this.i = i;
		gear.L = driveLife;
		gear.WorkMode = workMode;
		gear.IsReversible = isReversible;
		gear.ManufactoringAccuracy = manufactoringAccuracy;
		gear.i = u;
		wheel.L = driveLife;
		wheel.WorkMode = workMode;
		wheel.IsReversible = isReversible;
		wheel.ManufactoringAccuracy = manufactoringAccuracy;
		wheel.i = u;
		Gear = gear;
		Wheel = wheel;
	}

	public double u => Math.Abs(i);

	/// <summary>
	/// Расчет среднего допускаемого контактного напряжения.
	/// </summary>
	/// <value>Среднее допускаемое контактное напряжение.</value>
	public double SigmaH
	{
		get
		{
			return Math.Min(SigmaHm, SigmaHn);
		}
	}

	public double SigmaHmin => Math.Min(Gear.SigmaH, Wheel.SigmaH);

	public double SigmaHn => 1.25 * SigmaHmin;

	public double SigmaHm => (Gear.SigmaH + Wheel.SigmaH) / 2.0;

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
	public double PsyBd => 0.5 * WheelWidthCoefficient.AveragePsyBa * (u + 1);

	/// <summary>
	/// Коэффициент концентрации нагрузки. Сделать ввод с клавиатуры.
	/// </summary>
	public double KHBeta { get; set; }

	/// <summary>
	/// Вспомогательный коэффициент для прямозубых колес.
	/// </summary>
	public double Kd => 780.0;

	/// <summary>
	/// Ширина зубчатых колес.
	/// </summary>
	public double bw => Math.Round(Aw * WheelWidthCoefficient.AveragePsyBa);

	/// <summary>
	/// Ширина колеса.
	/// </summary>
	public double BW2
	{
		get
		{
			if (Wheel.Bw == 0)
			{
				Wheel.Bw = Math.Round(bw);
			}

			return Wheel.Bw;
		}

		set
		{
			Wheel.Bw = value;
			var d = BW1;
		}
	}

	/// <summary>
	/// Ширина шестерни.
	/// </summary>
	public double BW1
	{
		get
		{
			var BW1 = BW2 + 8;

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
	public double M
	{
		get
		{
			if (m == 0)
			{
				m = StandardModuleValuesList.StandardModuleValues.MinBy(value => Math.Abs(value - (bw / PsyM)));
			}

			return m;
		}

		set => m = value;
	}

	public double zDelta => Math.Round(2 * Aw / M);

	/// <summary>
	/// Число зубьев шестерни.
	/// </summary>
	public double z1
	{
		get
		{
			if (Gear.z == 0)
			{
				Gear.z = Math.Round(zDelta / (u + 1));
			}

			return Gear.z;
		}
		set => Gear.z = value;
	}

	/// <summary>
	/// Число зубьев колеса.
	/// </summary>
	public double z2
	{
		get
		{
			Wheel.z = zDelta - z1;

			return Wheel.z;
		}
	}

	private double aw;

	/// <summary>
	/// Межосевое расстояние.
	/// </summary>
	public double Aw
	{
		get
		{
			if (aw == 0)
			{
				aw = CenterDistancesStandardValuesList.CenterDistancesStandardValues.MinBy(v =>
					Math.Abs(v - (0.85 * (u + 1) *
								  Math.Pow(
									  (210000 * Wheel.t * 1000 * KHalpha * KHBeta) / (Math.Pow(SigmaH, 2) *
										  Math.Pow(u, 2) * WheelWidthCoefficient.AveragePsyBa), 1.0 / 3.0))));
			}

			return aw;
		}

		set
		{
			aw = value;
		}
	}

	/// <summary>
	/// Угол наклона зубьев.
	/// </summary>
	public double beta => 0;

	/// <summary>
	/// Делительный диаметр шестерни.
	/// </summary>
	public double d1
	{
		get
		{
			Gear.d = Math.Round(M * z1);

			return Gear.d;
		}
	}

	/// <summary>
	/// Делительный диаметр колеса.
	/// </summary>
	public double d2
	{
		get
		{
			Wheel.d = Math.Round(M * z2);

			return Wheel.d;
		}
	}

	/// <summary>
	/// Коэффициент торцового перекрытия.
	/// </summary>
	public double EpsilonAlpha => Math.Round((0.95 - 1.6 * (1.0 / z1 + 1.0 / z2)) * (1 + Math.Cos(beta)) * Math.Cos(beta), 3);


	/// <summary>
	/// Коэффициент осевого перекрытия.
	/// </summary>
	public double EpsilonBeta => Math.Round(bw * Math.Sin(beta) / (Math.PI * M), 3);

	public bool IsEpsilonBetaAcceptable => EpsilonBeta >= 1.1;

	/// <summary>
	/// Коэффициент повышения прочности по контактным напряжениям.
	/// </summary>
	public double ZHbeta => Math.Round(Math.Sqrt(Math.Pow(Math.Cos(beta), 2) / EpsilonAlpha), 3);

	public double C => 0.06;

	/// <summary>
	/// Коэффициент распределения нагрузки между зубьями.
	/// </summary>
	public double KHalpha => 1 + C * (ManufactoringAccuracy - 5);

	public bool IsKHalphaAcceptable => KHalpha <= 1.25;

	/// <summary>
	/// Окружная скорость.
	/// </summary>
	public double ipsilon => Math.Round(Math.PI * d1 * Gear.n / 60000, 3);

	/// <summary>
	/// Коэффициент динамической нагрузки.
	/// </summary>
	public double KHipsilon
	{
		get
		{
			double khipsilon = 0;

			//Добавить условие для иных скоростей по таблице В.1
			if (ipsilon <= 1.0)
			{
				khipsilon = 1.0;
			}

			return khipsilon;
		}
	}

	/// <summary>
	/// Коэффициент расчетной нагрузки.
	/// </summary>
	public double KH => Math.Round(KHalpha * KHBeta * KHipsilon, 2);

	public double Enp => 2.1 * Math.Pow(10, 5);

	/// <summary>
	/// Значение контактного напряжения для проверки условия прочности.
	/// </summary>
	public double SigmaHFinal => Math.Round(1.18 * ZHbeta * Math.Sqrt(((Enp * Gear.t * 1000 * KH) / (Math.Pow(d1, 2) * BW2 * Math.Sin(0.698132)) * ((u + 1) / u))), 2);

	/// <summary>
	/// Перегрузка/недогрузка.
	/// </summary>
	public double DeltaSigmaH => Math.Round(((SigmaHFinal - SigmaH) / SigmaH) * 100, 2);

	public bool IsDeltaSigmaHAcceptable => DeltaSigmaH >= -20.0 && DeltaSigmaH <= 5;

	/// <summary>
	/// Оптимизация ширины зубчатого венца.
	/// </summary>
	public void OptimizeBw()
	{
		while (!IsDeltaSigmaHAcceptable)
		{
			if (DeltaSigmaH > 5.0)
			{
				BW2 += 10;
			}

			if (DeltaSigmaH < -20.0)
			{
				BW2 -= 10;
			}
		}
	}

	/// <summary>
	/// Окружная сила Ft.
	/// </summary>
	public double Ft => Math.Round(2 * Gear.t * 1000 / d1);

	/// <summary>
	/// ?
	/// </summary>
	public double Fr => Math.Round(Ft * Math.Tan(0.349) / Math.Cos(beta));

	/// <summary>
	/// ?
	/// </summary>
	public double Fa => Math.Round(Ft * Math.Tan(beta));

	/// <summary>
	/// Эквивалентное число зубьев шестерни.
	/// </summary>
	public double Znu1 => Math.Round(z1 / Math.Pow(Math.Cos(beta), 3), 1);

	/// <summary>
	/// Эквивалентное число зубьев колеса.
	/// </summary>
	public double Znu2 => Math.Round(z2 / Math.Pow(Math.Cos(beta), 3), 1);

	/// <summary>
	/// Установка коэффициентов формы зуба (выбираем вручную по графику).
	/// </summary>
	public void SetYFSs(double yfs1, double yfs2)
	{
		Gear.Yfs = yfs1;

		Wheel.Yfs = yfs2;
	}

	/// <summary>
	/// 
	/// </summary>
	public double SigmaF => Gear.SigmaFYfsRelation > Wheel.SigmaFYfsRelation ? Wheel.SigmaF : Gear.SigmaF;

	public double Yfs => Gear.SigmaFYfsRelation > Wheel.SigmaFYfsRelation ? Wheel.Yfs : Gear.Yfs;

	/// <summary>
	/// Коэффициент повышения прочности косозубых передач по напряжениям изгиба.
	/// </summary>
	public double YFbeta
	{
		get
		{
			var yfbeta = Math.Round(1 / EpsilonAlpha, 3);

			return yfbeta >= 0.7 ? yfbeta : 0.7;
		}
	}

	public double KFalpha => KHalpha;

	public double KFbeta { get; set; }

	/// <summary>
	/// Ввод KFbeta, выбираем вручную по таблице 2.7.
	/// </summary>
	public void SetKFbeta(double kfbeta)
	{
		KFbeta = kfbeta;
	}

	public double KFipsilon { get; set; } = 1;

	/// <summary>
	/// Ввод KFipsilon, выбираем вручную по таблице В.1.
	/// </summary>
	/// <param name="kfipsilon"></param>
	public void SetKFipsilon(double kfipsilon)
	{
		KFipsilon = kfipsilon;
	}

	public double KF => Math.Round(KFbeta * KFalpha * KFipsilon, 2);

	public double SigmaFFinal => Math.Round((Ft * KF * Yfs * YFbeta) / (bw * M));

	public bool IsSigmaFFinalAcceptable => SigmaFFinal > SigmaF;

	/// <summary>
	/// Перегрузка/недогрузка.
	/// </summary>
	public double DeltaSigmaF => Math.Round(((SigmaFFinal - SigmaF) / SigmaF) * 100, 1);

	public bool IsDeltaSigmaFAcceptable => DeltaSigmaF >= -30.0;

	public void Optimizeaw()
	{
		if (DeltaSigmaH > 0)
		{
			var index = CenterDistancesStandardValuesList.CenterDistancesStandardValues.IndexOf(Aw);

			Aw = CenterDistancesStandardValuesList.CenterDistancesStandardValues[index + 1];
		}

		if (DeltaSigmaH < 0)
		{
			var index = CenterDistancesStandardValuesList.CenterDistancesStandardValues.IndexOf(Aw);

			Aw = CenterDistancesStandardValuesList.CenterDistancesStandardValues[index - 1];
		}

		Gear.z = 0;
		Wheel.z = 0;
	}

	public void Optimizem()
	{
		if (DeltaSigmaH > 0)
		{
			var index = StandardModuleValuesList.StandardModuleValues.IndexOf(M);

			M = StandardModuleValuesList.StandardModuleValues[index + 1];
		}

		if (DeltaSigmaH < 0)
		{
			var index = StandardModuleValuesList.StandardModuleValues.IndexOf(M);

			M = StandardModuleValuesList.StandardModuleValues[index - 1];
		}

		Gear.z = 0;
		Wheel.z = 0;
	}

	public double SigmaHFinalMax => Math.Round(SigmaHFinal * Math.Sqrt(Kp));

	public double SigmaHMax => Math.Max(Gear.SigmaHMax, Wheel.SigmaHMax);

	public bool IsSigmaHFinalMaxAcceptable => SigmaHFinalMax <= SigmaHMax;

	public double SigmaFFinalMax => Math.Round(SigmaFFinal * Kp);

	public double SigmaFMax => Math.Max(Gear.SigmaFMax, Wheel.SigmaFMax);

	public bool IsSigmaFFinalMaxAcceptable => SigmaFFinalMax <= SigmaFMax;
}
