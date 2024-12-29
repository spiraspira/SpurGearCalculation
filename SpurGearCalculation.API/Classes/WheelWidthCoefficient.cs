namespace SpurGearCalculation.API.Classes;

public record WheelWidthCoefficient(
	WheelArrangementType WheelArrangementType, // расположение колес относительно опор
	bool IsHardnessLessThan350, // твердость рабочей поверхности зубьев
	(double, double) PsyBa, // коэффициент ширины колеса относительно межосевого расстояния
	(double, double) PsyBdMax // максимально допустимое значение коэффициента
)
{
	public double AveragePsyBa => Math.Round((PsyBa.Item1 + PsyBa.Item2) / 2, 1);

	public double AveragePsyBdMax => Math.Round((PsyBdMax.Item1 + PsyBdMax.Item2) / 2, 1);
}