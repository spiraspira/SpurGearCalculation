namespace SpurGearCalculation.API.Tables;

public static class WorkModePropertiesTable
{
	public static List<WorkModeProperty> WorkModeProperties { get; set; } = [
		new WorkModeProperty(WorkModeType.Zero, true, 1.000, 6.0, 1.0),
		new WorkModeProperty(WorkModeType.I,    true, 0.500, 6.0, 0.3),
		new WorkModeProperty(WorkModeType.II,   true, 0.250, 6.0, 0.143),
		new WorkModeProperty(WorkModeType.III,  true, 0.180, 6.0, 0.065),
		new WorkModeProperty(WorkModeType.IV,   true, 0.125, 6.0, 0.038),
		new WorkModeProperty(WorkModeType.V,    true, 0.063, 6.0, 0.013),
		new WorkModeProperty(WorkModeType.Zero, false, 1.000, 9.0, 1.0),
		new WorkModeProperty(WorkModeType.I,    false, 0.500, 9.0, 0.2),
		new WorkModeProperty(WorkModeType.II,   false, 0.250, 9.0, 0.1),
		new WorkModeProperty(WorkModeType.III,  false, 0.180, 9.0, 0.036),
		new WorkModeProperty(WorkModeType.IV,   false, 0.125, 9.0, 0.016),
		new WorkModeProperty(WorkModeType.V,    false, 0.063, 9.0, 0.004),
	];
}
