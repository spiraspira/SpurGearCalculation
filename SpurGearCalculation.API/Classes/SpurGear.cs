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
	public double bw
	{
		get
		{
			return Math.Round(Gear.d * PsyBd);
		}
	}

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
	public double betaStrokeRad
	{
		get
		{
			return Math.Round(Math.Asin(1.1 * Math.PI * Mn / bw), 3);
		}
	}

	/// <summary>
	/// Угол наклона зубьев в градусах.
	/// </summary>
	public double betaStroke
	{
		get
		{
			return Math.Round(betaStrokeRad * 180 / Math.PI, 2);
		}
	}

	/// <summary>
	/// Число зубьев шестерни.
	/// </summary>
	public double z1
	{
		get
		{
			if (Gear.z == 0)
			{
				Gear.z = Math.Ceiling(d1Stroke * Math.Cos(betaStrokeRad) / Mn);
			}

			return Gear.z;
		}
		set
		{
			Gear.z = value;
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

	/// <summary>
	/// Межосевое расстояние.
	/// </summary>
	public double aw
	{
		get
		{
			return CenterDistancesStandardValuesList.CenterDistancesStandardValues.MinBy(v => Math.Abs(v - (Mn * (z1 + z2)) / (2 * Math.Cos(betaStrokeRad))));
		}
	}

	public double beta
	{
		get
		{
			return Math.Round(Math.Round(betaRad, 3) * 180 / Math.PI, 2);
		}
	}

	public double betaRad
	{
		get
		{
			double beta;

			double betaRad;

			do
			{
				betaRad = Math.Acos(Mn * (z2 + z1) / (2 * aw));

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

	public double d1
	{
		get
		{
			Gear.d = Math.Round(Mn * z1 / Math.Cos(betaRad), 3);

			return Gear.d;
		}
	}

	public double d2
	{
		get
		{
			Wheel.d = Math.Round(Mn * z2 / Math.Cos(betaRad), 3);

			return Wheel.d;
		}
	}

	/// <summary>
	/// Коэффициент торцового перекрытия.
	/// </summary>
	public double SigmaAlpha
	{
		get
		{
			return Math.Round((0.95 - 1.6 * (1.0 / z1 + 1.0 / z2)) * (1 + Math.Cos(betaRad)) * Math.Cos(betaRad), 3);
		}
	}

	public bool IsSigmaAlphaAcceptable
	{
		get
		{
			return SigmaAlpha >= 1.1;
		}
	}

	/// <summary>
	/// Коэффициент осевого перекрытия.
	/// </summary>
	public double SigmaBeta
	{
		get
		{
			return Math.Round(bw * Math.Sin(betaRad) / (Math.PI * Mn), 3);
		}
	}

	public bool IsSigmaBetaAcceptable
	{
		get
		{
			return SigmaBeta >= 1.1;
		}
	}

	/// <summary>
	/// Коэффициент повышения прочности по контактным напряжениям.
	/// </summary>
	public double ZHbeta
	{
		get
		{
			return Math.Round(Math.Sqrt(Math.Pow(Math.Cos(betaRad), 2) / SigmaAlpha), 3);
		}
	}

	/// <summary>
	/// Коэффициент распределения нагрузки между зубьями.
	/// </summary>
	public double KHalpha
	{
		get
		{
			return 1 + 0.25 * (7 - 5);
		}
	}

	public bool IsKHalphaAcceptable
	{
		get
		{
			return KHalpha <= 1.6;
		}
	}

	/// <summary>
	/// Окружная скорость.
	/// </summary>
	public double ipsilon
	{
		get
		{
			return Math.Round(Math.PI * d1 * Gear.n / 60000, 3);
		}
	}

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
	public double KH
	{
		get
		{
			return Math.Round(KHalpha * KHBeta * KHipsilon, 2);
		}
	}

	public double SigmaHFinal
	{
		get
		{
			return Math.Round(1.18 * ZHbeta * Math.Sqrt(((210000 * Gear.t * 1000 * KH) / (Math.Pow(d1, 2) * BW2 * Math.Sin(0.698132)) * ((i + 1) / i))), 2);
		}
	}

	public bool IsSigmaHFinalAcceptable
	{
		get
		{
			return SigmaHFinal <= SigmaH;
		}
	}

	/// <summary>
	/// Перегрузка/недогрузка.
	/// </summary>
	public double DeltaSigmaH
	{
		get
		{
			return Math.Round(((SigmaHFinal - SigmaH) / SigmaH) * 100, 2);
		}
	}

	public bool IsDeltaSigmaHAcceptable
	{
		get
		{
			return DeltaSigmaH >= -20.0 && DeltaSigmaH <= 5;
		}
	}

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
}
