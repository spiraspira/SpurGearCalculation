namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Часть цилиндрической передачи (шестерня / колесо).
/// </summary>
public class SpurGearPart
{
	public string Name { get; set; }

	public SteelType SteelType { get; set; }

	public ProcessingType ProcessingType { get; set; }

	public double n { get; set; }

	public double t { get; set; }
}
