namespace SpurGearCalculation.API.Classes;

/// <summary>
/// Значение для таблицы "Коэффициенты для расчета эквивалентного числа циклов нагружения"
/// </summary>
public record WorkModeProperty(
	WorkModeType WorkModeType, // режим работы
	bool IsHardnessLessThan350, // твердость
	double MuH, // расчет по контактным напряжениям
	double Mf, // 
	double MuF // 
);
