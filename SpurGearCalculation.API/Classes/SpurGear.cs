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
}
