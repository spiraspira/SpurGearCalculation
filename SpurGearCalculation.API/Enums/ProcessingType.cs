namespace SpurGearCalculation.API.Enums;

/// <summary>
/// Виды термообработки.
/// </summary>
public enum ProcessingType
{
	Normalization, //Нормализация
	Enhancement, //Улучшение
	Cementation, //Цементация
	EnhancementHardening, //Улучшение + закалка ТВЧ
	Nitriding, // Азотирование
	Hardening // Закалка
}