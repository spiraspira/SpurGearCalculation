namespace SpurGearCalculation.API.Classes;

public record ModulusCoefficientApproximateValue(
	TransmissionType TransmissionType, // тип трансмиссии
	bool IsHardnessLessThan350, // твердость рабочей поверхности зубьев
	(double, double) PsyMTuple // твердость колеса
)
{
	public double PsyM
	{
		get
		{
			return Math.Round((PsyMTuple.Item1 + PsyMTuple.Item2) / 2, 1);
		}
	}
}