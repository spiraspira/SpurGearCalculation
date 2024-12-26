using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;

SpurGear spurGear = new SpurGear();
WorkMode workMode = new WorkMode(5, 2, WorkModeType.II);
SpurGearPart gear = new SpurGearPart(SteelType.Steel40X, ProcessingType.Nitriding, 66.85, 227.4);
SpurGearPart wheel = new SpurGearPart(SteelType.Steel40X, ProcessingType.Enhancement, 16.71, 873.5);
spurGear.SetGearProperties(5, workMode, false, 7, 4, gear, wheel);

Console.WriteLine("===Допускаемые контактные напряжения===");
Console.WriteLine($"Предел контактной выносливости шестерни: {spurGear.Gear.CalculateSigmaHlim()}");
Console.WriteLine($"Предел контактной выносливости колеса: {spurGear.Wheel.CalculateSigmaHlim()}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.CalculateSh()}");
Console.WriteLine($"Коэффициент безопасности колеса: {spurGear.Wheel.CalculateSh()}");
Console.WriteLine($"Базовое число циклов шестерни: {spurGear.Gear.CalculateNHG()}");
Console.WriteLine($"Базовое число циклов колеса: {spurGear.Wheel.CalculateNHG()}");
Console.WriteLine($"Эквивалентное число циклов шестерни: {spurGear.Gear.CalculateNHE()}");
Console.WriteLine($"Эквивалентное число циклов колеса: {spurGear.Wheel.CalculateNHE()}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.Gear.CalculateZN()}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.Wheel.CalculateZN()}");
Console.WriteLine($"Допускаемое контактное напряжение: {spurGear.CalculateSigmaH()}");

Console.WriteLine("\n===Допускаемые напряжения изгиба===");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба шестерни: {spurGear.Gear.CalculateSigmaFlim()}");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба колеса: {spurGear.Wheel.CalculateSigmaFlim()}");
Console.WriteLine($"Эквивалентное число циклов для шестерни: {spurGear.Gear.CalculateNFE()}");
Console.WriteLine($"Эквивалентное число циклов для колеса: {spurGear.Wheel.CalculateNFE()}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.Gear.CalculateYN()}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.Wheel.CalculateYN()}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.CalculateYN()}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.CalculateSF()}");
Console.WriteLine($"Коэффициент безопасности колеса: {spurGear.Wheel.CalculateSF()}");
Console.WriteLine($"Допускаемое напряжение изгиба шестерни: {spurGear.Gear.CalculateSigmaF()}");
Console.WriteLine($"Допускаемое напряжение изгиба колеса: {spurGear.Wheel.CalculateSigmaF()}");

Console.WriteLine("\n===Допускаемые напряжения при перегрузках===");
Console.WriteLine($"Максимальное допускаемое контактное напряжение шестерни: {spurGear.Gear.CalculateSigmaHMax()}");
Console.WriteLine($"Максимальное допускаемое контактное напряжение колеса: {spurGear.Wheel.CalculateSigmaHMax()}");
Console.WriteLine($"Максимальная величина коэффициента долговечности шестерни: {spurGear.Gear.CalculateYNMax()}");
Console.WriteLine($"Максимальная величина коэффициента долговечности колеса: {spurGear.Wheel.CalculateYNMax()}");
Console.WriteLine($"Коэффициент учета частоты приложения пиковой нагрузки шестерни: {spurGear.Gear.CalculateKst()}");
Console.WriteLine($"Коэффициент учета частоты приложения пиковой нагрузки колеса: {spurGear.Wheel.CalculateKst()}");
Console.WriteLine($"Максимальное допускаемое напряжение изгиба шестерни: {spurGear.Gear.CalculateSigmaFMax()}");
Console.WriteLine($"Максимальное допускаемое напряжение изгиба колеса: {spurGear.Wheel.CalculateSigmaFMax()}");