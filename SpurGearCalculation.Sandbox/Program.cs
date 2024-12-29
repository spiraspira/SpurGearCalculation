﻿using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;

WorkMode workMode = new(5, 2, WorkModeType.II);
SpurGearPart gear = new(SteelType.Steel40X, ProcessingType.Nitriding, 66.85, 227.4);
SpurGearPart wheel = new(SteelType.Steel40X, ProcessingType.Enhancement, 16.71, 873.5);
SpurGear spurGear = new(5, workMode, false, 7, 4, 3, gear, wheel);

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
Console.WriteLine($"KHalpha = {spurGear.KHalpha} {spurGear.IsKHalphaAcceptable}");
Console.WriteLine($"Межосевое расстояние: {spurGear.aw}");
Console.WriteLine($"Ширина зубчатых колес: {spurGear.bw}");
Console.WriteLine($"Ширина шестерни: {spurGear.BW1}");
Console.WriteLine($"Ширина колеса: {spurGear.BW2}");
Console.WriteLine($"Коэффициент модуля: {spurGear.PsyM}");
Console.WriteLine($"Модуль передачи в нормальном сечении: {spurGear.M}");
Console.WriteLine($"zDelta: {spurGear.zDelta}");
Console.WriteLine($"Предварительное число зубьев шестерни: {spurGear.z1}");
Console.WriteLine($"Предварительное число зубьев колеса: {spurGear.z2}");
Console.WriteLine($"Делительный диаметр шестерни: {spurGear.d1}");
Console.WriteLine($"Делительный диаметр колеса: {spurGear.d2}");

Console.WriteLine("\n===Проверочные расчеты===");
Console.WriteLine($"Коэффициент торцового перекрытия: {spurGear.SigmaAlpha}");
Console.WriteLine($"Коэффициент осевого перекрытия: {spurGear.SigmaBeta} >= 1.1 {spurGear.IsSigmaBetaAcceptable}");
Console.WriteLine($"Коэффициент повышения прочности по контактным напряжениям: {spurGear.ZHbeta}");
Console.WriteLine($"Коэффициент распределения нагрузки между зубьями: {spurGear.KHalpha} <= 1.6 {spurGear.IsKHalphaAcceptable}");
Console.WriteLine($"Окружная скорость: {spurGear.ipsilon}");
Console.WriteLine($"Коэффициент динамической нагрузки: {spurGear.KHipsilon}");
Console.WriteLine($"Коэффициент расчетной нагрузки: {spurGear.KH}");
Console.WriteLine($"Условие прочности, sigmaH = {spurGear.SigmaHFinal} <= {spurGear.SigmaH} {spurGear.IsSigmaHFinalAcceptable}");
Console.WriteLine($"Перегрузка/недогрузка: {spurGear.DeltaSigmaH} {spurGear.IsDeltaSigmaHAcceptable}");
spurGear.OptimizeBw();
Console.WriteLine($"Условие прочности, sigmaH = {spurGear.SigmaHFinal} <= {spurGear.SigmaH} {spurGear.IsSigmaHFinalAcceptable}");
Console.WriteLine($"Перегрузка/недогрузка: {spurGear.DeltaSigmaH} {spurGear.IsDeltaSigmaHAcceptable}");
Console.WriteLine($"Окружная сила: {spurGear.Ft}");
Console.WriteLine($"Fr: {spurGear.Fr}");
Console.WriteLine($"Fa: {spurGear.Fa}");
Console.WriteLine($"Эквивалентное число зубьев шестерни: {spurGear.Znu1}");
Console.WriteLine($"Эквивалентное число зубьев колеса: {spurGear.Znu2}");
spurGear.SetYFSs(3.81, 3.76);
Console.WriteLine($"Отношение [SigmaH]/Yfs шестерни: {spurGear.Gear.SigmaFYfsRelation}");
Console.WriteLine($"Отношение [SigmaH]/Yfs колеса: {spurGear.Wheel.SigmaFYfsRelation}");
Console.WriteLine($"SigmaF: {spurGear.SigmaF}");
Console.WriteLine($"Yfs: {spurGear.Yfs}");
Console.WriteLine($"Коэффициент повышения прочности косозубых передач по напряжениям изгиба: {spurGear.YFbeta}");
spurGear.SetKFbeta(1.12);
spurGear.SetKFipsilon(1.0);
Console.WriteLine($"Коэффициент расчетной нагрузки по напряжениям изгиба: {spurGear.KF}");
Console.WriteLine($"Напряжение изгиба: {spurGear.SigmaFFinal} > {spurGear.SigmaF} {spurGear.IsSigmaFFinalAcceptable}");
Console.WriteLine($"Перегрузка: {spurGear.DeltaSigmaF} > -30% {spurGear.IsDeltaSigmaFAcceptable}");
Console.WriteLine($"{spurGear.SigmaHFinalMax} <= {spurGear.SigmaHMax} {spurGear.IsSigmaHFinalMaxAcceptable}");
Console.WriteLine($"{spurGear.SigmaFFinalMax} <= {spurGear.SigmaFMax} {spurGear.IsSigmaFFinalMaxAcceptable}");

Console.WriteLine("\n===ПАРАМЕТРЫ ЗУБЧАТОЙ ПЕРЕДАЧИ===");
Console.WriteLine($"{"Параметр",-40} {"Шестерня",-20} {"Шестерня",-20}");
Console.WriteLine($"{"Число зубьев z",-40} {spurGear.z1,-20} {spurGear.z2,-20}");
Console.WriteLine($"{"Модуль mn, мм",-40} {spurGear.M,-40}");
Console.WriteLine($"{"Фактическое передаточное число u = z2/z1",-40} {spurGear.z2 / spurGear.z1,-40}");
Console.WriteLine($"{"Угол наклона зубьев βº",-40} {spurGear.beta,-40}");
Console.WriteLine($"{"Делительный диаметр d, мм",-40} {spurGear.d1,-20} {spurGear.d2,-20}");
Console.WriteLine($"{"Диаметр вершин da, мм",-40} {spurGear.Gear.da,-20} {spurGear.Wheel.da,-20}");
Console.WriteLine($"{"Диаметр впадин df, мм",-40} {spurGear.Gear.df,-20} {spurGear.Wheel.df,-20}");
Console.WriteLine($"{"Ширина зубчатого венца bw, мм",-40} {spurGear.Gear.Bw,-20} {spurGear.Wheel.Bw,-20}");
Console.WriteLine($"{"Коэффициент смещения х",-40} {0,-20} {0,-20}");
Console.WriteLine($"{"Межосевое расстояние aw, мм",-40} {spurGear.aw,-40}");