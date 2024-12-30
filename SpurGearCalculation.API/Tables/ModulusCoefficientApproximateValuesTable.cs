namespace SpurGearCalculation.API.Tables;

public static class ModulusCoefficientApproximateValuesTable
{
	public static List<ModulusCoefficientApproximateValue> ModulusCoefficientApproximateValues { get; set; } = [
		new ModulusCoefficientApproximateValue(TransmissionType.HeavyDutyPrecision, true, (45.0, 30.0)),
		new ModulusCoefficientApproximateValue(TransmissionType.HeavyDutyPrecision, false, (30.0, 20.0)),
		new ModulusCoefficientApproximateValue(TransmissionType.RegularGear,        true, (30.0, 20.0)),
		new ModulusCoefficientApproximateValue(TransmissionType.RegularGear,        false, (20.0, 15.0)),
		new ModulusCoefficientApproximateValue(TransmissionType.Rough,              true, (15.0, 10.0)),
		new ModulusCoefficientApproximateValue(TransmissionType.Rough,              false, (15.0, 10.0)),
	];
}