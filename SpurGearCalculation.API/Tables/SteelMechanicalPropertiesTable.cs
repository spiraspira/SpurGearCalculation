namespace SpurGearCalculation.API.Tables;

/// <summary>
/// Таблица "Механические свойства некоторых марок сталей после термообработки".
/// </summary>
public static class SteelMechanicalPropertiesTable
{
	public static List<SteelMechanicalProperty> SteelMechanicalProperties { get; set; } = [
		new SteelMechanicalProperty(SteelType.Steel45,      ProcessingType.Normalization,        (170, 217), 600,   340),
		new SteelMechanicalProperty(SteelType.Steel45,      ProcessingType.Enhancement,          (192, 240), 750,   450),
		new SteelMechanicalProperty(SteelType.Steel45,      ProcessingType.Enhancment2,          (240, 260), 850,   580),
		new SteelMechanicalProperty(SteelType.Steel20X,     ProcessingType.Cementation,          (56,  63 ), 650,   400),
		new SteelMechanicalProperty(SteelType.Steel12XH3A,  ProcessingType.Cementation,          (56,  63 ), 900,   700),
		new SteelMechanicalProperty(SteelType.Steel40X,     ProcessingType.Enhancement,          (230, 260), 850,   550),
		new SteelMechanicalProperty(SteelType.Steel40X,     ProcessingType.Enhancment2,          (260, 280), 950,   700),
		new SteelMechanicalProperty(SteelType.Steel40X,     ProcessingType.Nitriding,            (50,  59 ), 1000,  800),
		new SteelMechanicalProperty(SteelType.Steel40XH,    ProcessingType.Enhancement,          (235, 262), 800,   630),
		new SteelMechanicalProperty(SteelType.Steel40XH,    ProcessingType.Enhancment2,          (269, 302), 920,   750),
		new SteelMechanicalProperty(SteelType.Steel40XH,    ProcessingType.Hardening,            (48,  54 ), 1600, 1400),
	];
}
