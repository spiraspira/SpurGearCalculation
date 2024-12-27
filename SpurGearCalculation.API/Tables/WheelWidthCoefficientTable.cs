namespace SpurGearCalculation.API.Tables;

public static class WheelWidthCoefficientTable
{
	public static List<WheelWidthCoefficient> WheelWidthCoefficients { get; set; } = [
		new WheelWidthCoefficient(WheelArrangementType.Symmetrical,  true,  (0.315, 0.500), (1.2,  1.6)),
		new WheelWidthCoefficient(WheelArrangementType.Symmetrical,  false, (0.250, 0.315), (0.9,  1.0)),
		new WheelWidthCoefficient(WheelArrangementType.Asymmetrical, true,  (0.250, 0.400), (1.0,  1.25)),
		new WheelWidthCoefficient(WheelArrangementType.Asymmetrical, false, (0.200, 0.250), (0.65, 0.8)),
		new WheelWidthCoefficient(WheelArrangementType.Console,      true,  (0.200, 0.250), (0.6,  0.7)),
		new WheelWidthCoefficient(WheelArrangementType.Console,      false, (0.150, 0.200), (0.45, 0.55)),
	];
}