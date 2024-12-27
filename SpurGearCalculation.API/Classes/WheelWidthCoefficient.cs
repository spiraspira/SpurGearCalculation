namespace SpurGearCalculation.API.Classes;

public record WheelWidthCoefficient(
	WheelArrangementType WheelArrangementType, // расположение колес относительно опор
	bool IsHardnessLessThan350, // твердость рабочей поверхности зубьев
	(double WidthCoefficient, double AxleDistance) PsyBa, // коэффициент ширины колеса относительно межосевого расстояния
	(double MaxWidthCoefficient, double MaxAxleDistance) PsyBdMax // максимально допустимое значение коэффициента
);