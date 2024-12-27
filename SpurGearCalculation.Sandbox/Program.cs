﻿using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;

WorkMode workMode = new(5, 2, WorkModeType.II);
SpurGearPart gear = new(SteelType.Steel40X, ProcessingType.Nitriding, 66.85, 227.4);
SpurGearPart wheel = new(SteelType.Steel40X, ProcessingType.Enhancement, 16.71, 873.5);
SpurGear spurGear = new(5, workMode, false, 7, 4, gear, wheel);

Console.WriteLine("===Допускаемые контактные напряжения===");
Console.WriteLine($"Предел контактной выносливости шестерни: {spurGear.Gear.SigmaHlim}");
Console.WriteLine($"Предел контактной выносливости колеса: {spurGear.Wheel.SigmaHlim}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.Sh}");
Console.WriteLine($"Коэффициент безопасности колеса: {spurGear.Wheel.Sh}");
Console.WriteLine($"Базовое число циклов шестерни: {spurGear.Gear.NHG}");
Console.WriteLine($"Базовое число циклов колеса: {spurGear.Wheel.NHG}");
Console.WriteLine($"Эквивалентное число циклов шестерни: {spurGear.Gear.NHE}");
Console.WriteLine($"Эквивалентное число циклов колеса: {spurGear.Wheel.NHE}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.Gear.ZN}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.Wheel.ZN}");
Console.WriteLine($"Допускаемое контактное напряжение: {spurGear.SigmaH}");

Console.WriteLine("\n===Допускаемые напряжения изгиба===");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба шестерни: {spurGear.Gear.SigmaFlim}");
Console.WriteLine($"Предел выносливости зубьев по напряжениям изгиба колеса: {spurGear.Wheel.SigmaFlim}");
Console.WriteLine($"Эквивалентное число циклов для шестерни: {spurGear.Gear.NFE}");
Console.WriteLine($"Эквивалентное число циклов для колеса: {spurGear.Wheel.NFE}");
Console.WriteLine($"Коэффициент долговечности шестерни: {spurGear.Gear.YN}");
Console.WriteLine($"Коэффициент долговечности колеса: {spurGear.Wheel.YN}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.YN}");
Console.WriteLine($"Коэффициент безопасности шестерни: {spurGear.Gear.SF}");
Console.WriteLine($"Коэффициент безопасности колеса: {spurGear.Wheel.SF}");
Console.WriteLine($"Допускаемое напряжение изгиба шестерни: {spurGear.Gear.SigmaF}");
Console.WriteLine($"Допускаемое напряжение изгиба колеса: {spurGear.Wheel.SigmaF}");

Console.WriteLine("\n===Допускаемые напряжения при перегрузках===");
Console.WriteLine($"Максимальное допускаемое контактное напряжение шестерни: {spurGear.Gear.SigmaHMax}");
Console.WriteLine($"Максимальное допускаемое контактное напряжение колеса: {spurGear.Wheel.SigmaHMax}");
Console.WriteLine($"Максимальная величина коэффициента долговечности шестерни: {spurGear.Gear.YNMax}");
Console.WriteLine($"Максимальная величина коэффициента долговечности колеса: {spurGear.Wheel.YNMax}");
Console.WriteLine($"Коэффициент учета частоты приложения пиковой нагрузки шестерни: {spurGear.Gear.Kst}");
Console.WriteLine($"Коэффициент учета частоты приложения пиковой нагрузки колеса: {spurGear.Wheel.Kst}");
Console.WriteLine($"Максимальное допускаемое напряжение изгиба шестерни: {spurGear.Gear.SigmaFMax}");
Console.WriteLine($"Максимальное допускаемое напряжение изгиба колеса: {spurGear.Wheel.SigmaFMax}");

Console.WriteLine("\n===Делительные диаметры===");
Console.WriteLine($"Вспомогательный коэффициент: {spurGear.Kd}");
Console.WriteLine($"Ориентировочное значение коэффициента ширины колеса: {spurGear.WheelWidthCoefficient.AveragePsyBa}");
Console.WriteLine($"Коэффициент ширины колеса относительно делительного диаметра: {spurGear.PsyBd}");
Console.WriteLine($"Коэффициент концентрации нагрузки: {spurGear.KHBeta}");
Console.WriteLine($"Делительный диаметр шестерни: {spurGear.d1}");
Console.WriteLine($"Ширина зубчатых колес: {spurGear.bw}");
Console.WriteLine($"Ширина шестерни: {spurGear.BW1}");
Console.WriteLine($"Ширина колеса: {spurGear.BW2}");