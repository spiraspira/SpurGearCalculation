namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Механические свойства вида стали.
/// </summary>
public record SteelMechanicalProperty(
	SteelType SteelType, // тип стали
	ProcessingType ProcessingType, // вид термообработки
	(double, double) SurfaceHardness, // твердость поверхности
	double SigmaB, // предел прочности
	double SigmaT // предел текучести
);
