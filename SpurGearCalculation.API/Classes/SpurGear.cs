namespace SpurGearCalculation.API.Classes;

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

	public double i { get; set; } //передаточное отношение

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
	public double PsyBd => 0.5 * WheelWidthCoefficient.AveragePsyBa * (i + 1);

	/// <summary>
	/// Коэффициент концентрации нагрузки. Сделать ввод с клавиатуры.
	/// </summary>
	public double KHBeta => 1.04;

	/// <summary>
	/// Вспомогательный коэффициент для прямозубых колес.
	/// </summary>
	public double Kd => 780.0;

	/// <summary>
	/// Делительный диаметр шестерни.
	/// </summary>
	public double d1Stroke
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
	public double bw => Math.Round(aw * WheelWidthCoefficient.AveragePsyBa);

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

	/// <summary>
	/// Угол наклона зубьев в радианах.
	/// </summary>
	public double betaStrokeRad => Math.Round(Math.Asin(1.1 * Math.PI * M / bw), 3);

	/// <summary>
	/// Угол наклона зубьев в градусах.
	/// </summary>
	public double betaStroke => Math.Round(betaStrokeRad * 180 / Math.PI, 2);

	public double zDelta => 2 * aw / M;

	/// <summary>
	/// Число зубьев шестерни.
	/// </summary>
	public double z1
	{
		get
		{
			if (Gear.z == 0)
			{
				Gear.z = zDelta / (i + 1);
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

	/// <summary>
	/// Межосевое расстояние.
	/// </summary>
	public double aw
	{
		get
		{
			return CenterDistancesStandardValuesList.CenterDistancesStandardValues.MinBy(v => Math.Abs(v - (0.85 * (i + 1) * Math.Pow((210000 * Wheel.t * 1000 * KHalpha * KHBeta) / (Math.Pow(SigmaH, 2) * Math.Pow(i, 2) * WheelWidthCoefficient.AveragePsyBa), 1.0 / 3.0))));
		}
	}

	/// <summary>
	/// Финальный угол наклона зубьев в градусах.
	/// </summary>
	public double beta => Math.Round(Math.Round(betaRad, 3) * 180 / Math.PI, 2);

	/// <summary>
	/// Финальный угол наклона зубьев в радианах.
	/// </summary>
	public double betaRad
	{
		get
		{
			double beta;

			double betaRad;

			do
			{
				betaRad = Math.Acos(M * (z2 + z1) / (2 * aw));

				beta = betaRad * 180 / Math.PI;

				if (beta < 8)
				{
					z1 -= 1;
				}
				else if (betaRad > 20)
				{
					z1 += 1;
				}
			} while (beta < 8 || beta > 20);

			return Math.Round(betaRad, 3);
		}
	}

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
	public double SigmaAlpha => Math.Round((0.95 - 1.6 * (1.0 / z1 + 1.0 / z2)) * (1 + Math.Cos(betaRad)) * Math.Cos(betaRad), 3);

	public bool IsSigmaAlphaAcceptable => SigmaAlpha >= 1.1;

	/// <summary>
	/// Коэффициент осевого перекрытия.
	/// </summary>
	public double SigmaBeta => Math.Round(bw * Math.Sin(betaRad) / (Math.PI * M), 3);

	public bool IsSigmaBetaAcceptable => SigmaBeta >= 1.1;

	/// <summary>
	/// Коэффициент повышения прочности по контактным напряжениям.
	/// </summary>
	public double ZHbeta => Math.Round(Math.Sqrt(Math.Pow(Math.Cos(betaRad), 2) / SigmaAlpha), 3);

	public double C => 0.06;

	/// <summary>
	/// Коэффициент распределения нагрузки между зубьями.
	/// </summary>
	public double KHalpha => 1 + C * (7 - 5);

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

	/// <summary>
	/// Значение контактного напряжения для проверки условия прочности.
	/// </summary>
	public double SigmaHFinal => Math.Round(1.18 * ZHbeta * Math.Sqrt(((210000 * Gear.t * 1000 * KH) / (Math.Pow(d1, 2) * BW2 * Math.Sin(0.698132)) * ((i + 1) / i))), 2);

	public bool IsSigmaHFinalAcceptable => SigmaHFinal <= SigmaH;

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
	public double Fr => Math.Round(Ft * Math.Tan(0.349) / Math.Cos(betaRad));

	/// <summary>
	/// ?
	/// </summary>
	public double Fa => Math.Round(Ft * Math.Tan(betaRad));

	/// <summary>
	/// Эквивалентное число зубьев шестерни.
	/// </summary>
	public double Znu1 => Math.Round(z1 / Math.Pow(Math.Cos(betaRad), 3), 1);

	/// <summary>
	/// Эквивалентное число зубьев колеса.
	/// </summary>
	public double Znu2 => Math.Round(z2 / Math.Pow(Math.Cos(betaRad), 3), 1);

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
			var yfbeta = Math.Round((1 - beta / 100) / SigmaAlpha, 3);

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

	public double KFipsilon { get; set; }

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

	public void OptimizeModule()
	{
		while (!IsDeltaSigmaFAcceptable)
		{
			var index = StandardModuleValuesList.StandardModuleValues.IndexOf(M);

			M = StandardModuleValuesList.StandardModuleValues[index - 1];
		}
	}

	public double SigmaHFinalMax => Math.Round(SigmaHFinal * Math.Sqrt(Kp));

	public double SigmaHMax => Math.Max(Gear.SigmaHMax, Wheel.SigmaHMax);

	public bool IsSigmaHFinalMaxAcceptable => SigmaHFinalMax <= SigmaHMax;

	public double SigmaFFinalMax => Math.Round(SigmaFFinal * Kp);

	public double SigmaFMax => Math.Max(Gear.SigmaFMax, Wheel.SigmaFMax);

	public bool IsSigmaFFinalMaxAcceptable => SigmaFFinalMax <= SigmaFMax;
}
