using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;

SpurGear spurGear = new SpurGear();
WorkMode workMode = new WorkMode(5, 2, WorkModeType.II);
spurGear.SetGearProperties(5, workMode, false, 7, 66.85, 16.71, 4, 227.4, 873.5);
spurGear.SetSteelType(SteelType.Steel40X, ProcessingType.Nitriding, ProcessingType.Enhancement);

Console.WriteLine("===Допускаемые контактные напряжения===");
Console.WriteLine($"Предел контактной выносливости шестерни: {spurGear.CalculateSigmaHlim(spurGear.GearProcessingType)}");
Console.WriteLine($"Предел контактной выносливости колеса: {spurGear.CalculateSigmaHlim(spurGear.WheelProcessingType)}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.CalculateSh(spurGear.GearProcessingType)}");
Console.WriteLine($"Коэффициент безопасности колеса: {spurGear.CalculateSh(spurGear.WheelProcessingType)}");
Console.WriteLine($"Базовое число циклов шестерни: {spurGear.CalculateNHG(spurGear.GearProcessingType)}");
Console.WriteLine($"Базовое число циклов колеса: {spurGear.CalculateNHG(spurGear.WheelProcessingType)}");
Console.WriteLine($"Эквивалентное число циклов шестерни: {spurGear.CalculateNHE(spurGear.n1)}");
Console.WriteLine($"Эквивалентное число циклов колеса: {spurGear.CalculateNHE(spurGear.n2)}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.CalculateZN(spurGear.CalculateNHG(spurGear.GearProcessingType), spurGear.CalculateNHE(spurGear.n1))}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.CalculateZN(spurGear.CalculateNHG(spurGear.WheelProcessingType), spurGear.CalculateNHE(spurGear.n2))}");
Console.WriteLine($"Допускаемое контактное напряжение: {spurGear.CalculateSigmaH()}");

Console.WriteLine("\n===Допускаемые напряжения изгиба===");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба шестерни: {spurGear.CalculateSigmaFlim(spurGear.GearProcessingType)}");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба колеса: {spurGear.CalculateSigmaFlim(spurGear.WheelProcessingType)}");
Console.WriteLine($"Эквивалентное число циклов для шестерни: {spurGear.CalculateNFE(spurGear.GearProcessingType, spurGear.n1)}");
Console.WriteLine($"Эквивалентное число циклов для колеса: {spurGear.CalculateNFE(spurGear.WheelProcessingType, spurGear.n2)}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.CalculateYN(spurGear.GearProcessingType, spurGear.n1)}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.CalculateYN(spurGear.WheelProcessingType, spurGear.n2)}");