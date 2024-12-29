using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;
using SpurGearCalculation.API.Tables;

namespace SpurGearCalculation;

public partial class MainForm : Form
{
	private SpurGear SpurGear { get; set; }

	public MainForm()
	{
		InitializeComponent();

		nud_L.Value = 5;
		cb_Kd.SelectedIndex = 0;
		cb_Ks.SelectedIndex = 1;
		cb_WorkModeType.SelectedIndex = 2;
		cb_IsReversible.SelectedIndex = 1;
		nud_ManufactoringAccuracy.Value = 7;
		nud_Kp.Value = 3;
		nud_U.Value = 4;
		tb_n1.Text = "66,85";
		tb_n2.Text = "16,71";
		tb_T1.Text = "227,4";
		tb_T2.Text = "873,5";

		cb_SteelMarkGear.DataSource = Enum.GetValues(typeof(SteelType));
		cb_SteelMarkGear.SelectedIndex = 3;
		cb_SteelMarkWheel.DataSource = Enum.GetValues(typeof(SteelType));
		cb_SteelMarkWheel.SelectedIndex = 3;
	}

	private void cb_SteelMark_SelectedIndexChanged(object sender, EventArgs e)
	{
		var selectedSteelType = (SteelType)cb_SteelMarkGear.SelectedItem;

		var processingTypes = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.Where(prop => prop.SteelType == selectedSteelType)
			.Select(prop => prop.ProcessingType)
			.Distinct()
			.ToList();

		cb_ProcessingTypeGear.DataSource = processingTypes;
		if (processingTypes.Count > 0)
		{
			cb_ProcessingTypeGear.SelectedIndex = 1;
		}
	}

	private void cb_SteelMarkWheel_SelectedIndexChanged(object sender, EventArgs e)
	{
		var selectedSteelType = (SteelType)cb_SteelMarkWheel.SelectedItem;

		var processingTypes = SteelMechanicalPropertiesTable.SteelMechanicalProperties
			.Where(prop => prop.SteelType == selectedSteelType)
			.Select(prop => prop.ProcessingType)
			.Distinct()
			.ToList();

		cb_ProcessingTypeWheel.DataSource = processingTypes;
		if (processingTypes.Count > 0)
		{
			cb_ProcessingTypeWheel.SelectedIndex = 0;
		}
	}

	private void b_Calculate_Click(object sender, EventArgs e)
	{
		rtb_Log.Clear();

		CreateSpurGear();

		Log("===НАЧАЛО ВЫЧИСЛЕНИЯ===");
		Log("\n===Входные параметры===");
		Log($"L = {SpurGear.L} лет");
		Log($"{(SpurGear.IsReversible ? "Реверсивный" : "Нереверсивный")}");
		Log($"i = {SpurGear.i}");
		Log($"KП = {SpurGear.Kp}");
		Log($"Режим работы - {SpurGear.WorkMode.WorkModeType}");
		Log($"Kd = {SpurGear.WorkMode.Kd} дней");
		Log($"Ks = {SpurGear.WorkMode.Ks} смен");
		Log($"n1 = {SpurGear.Gear.n} мин‒1");
		Log($"T1 = {SpurGear.Gear.t} Н·м");
		Log($"n2 = {SpurGear.Wheel.n} мин‒1");
		Log($"T2 = {SpurGear.Wheel.t} Н·м");

		Log("\n===Выбор марки стали===");
		Log($"Шестерня: {SpurGear.Gear.SteelType}");
		Log($"Колесо: {SpurGear.Wheel.SteelType}");

		Log("\n===Назначаем термообработку===");
		Log($"Для шестерни - {SpurGear.Gear.SteelMechanicalProperty.ProcessingType} {SpurGear.Gear.SteelMechanicalProperty.SurfaceHardness}");
		Log($"Для колеса - {SpurGear.Wheel.SteelMechanicalProperty.ProcessingType} {SpurGear.Wheel.SteelMechanicalProperty.SurfaceHardness}");

		Log("\n===Допускаемые контактные напряжения===");
		Log("Коэффициенты безопасности:");
		Log($"SH1 = {SpurGear.Gear.Sh}");
		Log($"SH2 = {SpurGear.Wheel.Sh}");
		Log("Пределы контактной выносливости:");
		Log($"σHlim1 = {SpurGear.Gear.SigmaHlim} МПа");
		Log($"σHlim2 = {SpurGear.Wheel.SigmaHlim} МПа");
		Log("Базовое число циклов:");
		Log($"NHG1 = {SpurGear.Gear.NHG}");
		Log($"NHG2 = {SpurGear.Wheel.NHG}");
		Log($"Ресурс работы передачи в часах:");
		Log($"tΣ = {SpurGear.Gear.TSigma} ч");
		Log($"Эквивалентное число циклов:");
		Log($"NHE1 = {SpurGear.Gear.NHE}");
		Log($"NHE2 = {SpurGear.Wheel.NHE}");
		Log("Коэффициенты долговечности:");
		Log($"ZN1 = {SpurGear.Gear.ZN} {SpurGear.Gear.IsZNAcceptable}");
		Log($"ZN2 = {SpurGear.Wheel.ZN} {SpurGear.Wheel.IsZNAcceptable}");
		Log("Допускаемые контактные напряжения:");
		Log($"[σH]1 = {SpurGear.Gear.SigmaH} МПа");
		Log($"[σH]2 = {SpurGear.Wheel.SigmaH} МПа");
		Log("Среднее допускаемое контактное напряжение:");
		Log($"[σH]m = {SpurGear.SigmaHm} МПа");
		Log("Предельное допускаемое контактное напряжение:");
		Log($"[σH]n = {SpurGear.SigmaHn} МПа");
		Log("Допускаемое контактное напряжение:");
		Log($"[σH] = {SpurGear.SigmaH} МПа");

		Log("\n===Допускаемые напряжения изгиба===");
		Log("Коэффициент влияния двухстороннего приложения нагрузки:");
		Log($"YA = {SpurGear.Gear.YA}");
		Log("Пределы выносливости по напряжениям изгиба:");
		Log($"σFlim1 = {SpurGear.Gear.SigmaFlim} МПа");
		Log($"σFlim2 = {SpurGear.Wheel.SigmaFlim} МПа");
		Log($"Базовое число циклов:");
		Log($"NFG = {SpurGear.Gear.NFG}");
		Log("Показатель степени, зависящий от вида термообработки:");
		Log($"μF1 = {SpurGear.Gear.WorkModeProperty.Mf}");
		Log($"μF2 = {SpurGear.Wheel.WorkModeProperty.Mf}");
		Log("Эквивалентное число циклов перемены напряжений изгиба:");
		Log($"NFE1 = {SpurGear.Gear.NFE}");
		Log($"NFE2 = {SpurGear.Wheel.NFE}");
		Log("Коэффициенты долговечности:");
		Log($"YN1 = {SpurGear.Gear.YN}");
		Log($"YN2 = {SpurGear.Wheel.YN}");
		Log($"Коэффициент безопасности:");
		Log($"SF1 = {SpurGear.Gear.SF}");
		Log($"SF2 = {SpurGear.Wheel.SF}");
		Log("Допускаемые напряжения изгиба:");
		Log($"[σF]1 = {SpurGear.Gear.SigmaF}");
		Log($"[σF]2 = {SpurGear.Wheel.SigmaF}");
	}

	private void CreateSpurGear()
	{
		WorkMode workMode = new WorkMode
		(
			(cb_Kd.SelectedIndex == 0 ? 5 : 6),
			(cb_Ks.SelectedIndex == 0 ? 1 : 2),
			(WorkModeType)cb_WorkModeType.SelectedIndex
		);

		SpurGearPart gear = new SpurGearPart(
			(SteelType)cb_SteelMarkGear.SelectedItem,
			(ProcessingType)cb_ProcessingTypeGear.SelectedItem,
			double.Parse(tb_n1.Text),
			double.Parse(tb_T1.Text)
		);

		SpurGearPart wheel = new SpurGearPart(
			(SteelType)cb_SteelMarkWheel.SelectedItem,
			(ProcessingType)cb_ProcessingTypeWheel.SelectedItem,
			double.Parse(tb_n2.Text),
			double.Parse(tb_T2.Text)
		);

		SpurGear = new SpurGear(
			(int)nud_L.Value,
			workMode,
			(cb_IsReversible.SelectedIndex == 0),
			(int)nud_ManufactoringAccuracy.Value,
			(double)nud_U.Value,
			(double)nud_Kp.Value,
			gear,
			wheel
		);
	}

	private void Log(string message)
	{
		rtb_Log.Text += "\n" + message;
	}
}