namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Режим работы.
/// </summary>
public class WorkMode
{
	public int Kd { get; set; } //количество рабочих дней в году

	public int Ks { get; set; } //количество смен

	public WorkModeType WorkModeType { get; set; } //режим работы

	public WorkMode(int workDaysInWeek, int ks, WorkModeType workModeType)
	{
		// Kd = (365 / 7) * workDaysInWeek;
		Kd = workDaysInWeek == 5 ? 255 : 305;
		Ks = ks;
		WorkModeType = workModeType;
	}
}
