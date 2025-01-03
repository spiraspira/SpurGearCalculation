﻿using SpurGearCalculation.API.Classes;
using SpurGearCalculation.API.Enums;
using SpurGearCalculation.API.Tables;

namespace SpurGearCalculation;

public partial class MainForm : Form
{
	public SpurGear SpurGear { get; set; }

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
		Log($"[σF]1 = {SpurGear.Gear.SigmaF} МПа");
		Log($"[σF]2 = {SpurGear.Wheel.SigmaF} МПа");

		Log("\n===Допускаемые напряжения при перегрузках===");
		Log("Максимальные допускаемые контактные напряжения:");
		Log($"[σH]max1 = {SpurGear.Gear.SigmaHMax} МПа");
		Log($"[σH]max2 = {SpurGear.Wheel.SigmaHMax} МПа");
		Log($"σT1 = {SpurGear.Gear.SteelMechanicalProperty.SigmaT} МПа");
		Log($"σT2 = {SpurGear.Wheel.SteelMechanicalProperty.SigmaT} МПа");
		Log("Максимально допускаемые напряжения изгиба:");
		Log($"[σF]max1 = {SpurGear.Gear.SigmaFMax} МПа");
		Log($"[σF]max2 = {SpurGear.Wheel.SigmaFMax} МПа");

		Log("\n===Проектный расчет цилиндрической прямозубой передачи===");
		Log("Вспомогательный коэффициент");
		Log($"Kd = {SpurGear.Kd}");
		Log("Передаточной число передачи:");
		Log($"u = {SpurGear.u}");
		Log("Коэффициент ширины колеса относительно межосевого расстояния:");
		Log($"ψba = {SpurGear.WheelWidthCoefficient.AveragePsyBa}");
		Log("Коэффициент ширины колеса относительно делительного диаметра:");
		Log($"ψbd = {SpurGear.PsyBd}");
		var KHBetaForm = new KHBetaForm();
		KHBetaForm.Owner = this;
		KHBetaForm.ShowDialog();
		Log($"KHβ = {SpurGear.KHBeta}");
		Log($"KHα = {SpurGear.KHalpha}");
		Log("Межосевое расстояние:");
		Log($"aw = {SpurGear.Aw} мм");
		Log("Ширина зубчатых колес:");
		Log($"bw = {SpurGear.bw} мм");
		Log($"Ширина шестерни и колеса:");
		Log($"bw1 = {SpurGear.BW1} мм");
		Log($"bw2 = {SpurGear.BW2} мм");
		Log($"ψm = {SpurGear.PsyM}");
		Log("Модуль передачи:");
		Log($"m = {SpurGear.M} мм");
		Log("Суммарное число зубьев:");
		Log($"zΔ = {SpurGear.zDelta}");
		Log($"Число зубьев шестерни и колеса:");
		Log($"z1 = {SpurGear.z1}");
		Log($"z2 = {SpurGear.z2}");
		Log("Делительные диаметры:");
		Log($"d1 = {SpurGear.d1} мм");
		Log($"d2 = {SpurGear.d2} мм");

		Log("\n===Проверочные расчеты цилиндрической прямозубой передачи===");
		Log("Модуль упругости:");
		Log($"Enp = {SpurGear.Enp} МПа");
		Log("Коэффициент торцового перекрытия:");
		Log($"εα = {SpurGear.EpsilonAlpha}");
		Log("Коэффициент осевого перекрытия:");
		Log($"εβ = {SpurGear.EpsilonBeta}");
		Log($"ZHβ = {SpurGear.ZHbeta}");
		Log("Коэффициент твердости и типа зубьев:");
		Log($"C = {SpurGear.C}");
		Log("Коэффициент распределения нагрузки между зубьями:");
		Log($"KHα = {SpurGear.KHalpha} <= 1.25 {SpurGear.IsKHalphaAcceptable}");
		Log("Окружная скорость:");
		Log($"υ = {SpurGear.ipsilon} м/с");
		Log("Коэффициент расчетной нагрузки:");
		Log($"KH = {SpurGear.KH}");
		Log("Проверка на прочность");
		Log($"σH = {SpurGear.SigmaHFinal} МПа");
		do
		{
			Log($"{SpurGear.SigmaHFinal} > {SpurGear.SigmaH}");
			Log($"Погрешность ΔσH = {SpurGear.DeltaSigmaH}% {SpurGear.IsDeltaSigmaHAcceptable}");
			if (!SpurGear.IsDeltaSigmaHAcceptable)
			{
				SpurGear.Optimizeaw();
				SpurGear.Optimizem();
				Log($"aw = {SpurGear.Aw} мм");
				Log($"m = {SpurGear.M} мм");
				Log($"zΔ = {SpurGear.zDelta}");
				Log($"z1 = {SpurGear.z1}");
				Log($"z2 = {SpurGear.z2}");
				Log($"{SpurGear.SigmaHFinal} > {SpurGear.SigmaH}");
				Log($"Погрешность ΔσH = {SpurGear.DeltaSigmaH}% {SpurGear.IsDeltaSigmaHAcceptable}");
			}
		} while (!SpurGear.IsDeltaSigmaHAcceptable);
		Log("Проверочный расчет прочности передачи по напряжениям изгиба:");
		Log($"Ft = {SpurGear.Ft} Н");
		Log($"Fr = {SpurGear.Fr} Н");
		Log($"Fa = {SpurGear.Fa} Н");
		Log("Эквивалентные числа зубьев:");
		Log($"Zυ1 = {SpurGear.z1}");
		Log($"Zυ2 = {SpurGear.z2}");
		var YFSForm = new YFSForm();
		YFSForm.Owner = this;
		YFSForm.ShowDialog();
		Log("Коэффициенты формы зуба:");
		Log($"YFS1 = {SpurGear.Gear.Yfs}");
		Log($"YFS2 = {SpurGear.Gear.Yfs}");
		Log($"[σF]1/YFS1 = {SpurGear.Gear.SigmaFYfsRelation}");
		Log($"[σF]2/YFS2 = {SpurGear.Wheel.SigmaFYfsRelation}");
		Log($"[σF] = {SpurGear.SigmaF}");
		Log($"Коэффициент YFβ = {SpurGear.YFbeta}");
		Log($"ψbd = {SpurGear.PsyBd}");
		var KFBetaForm = new KFBetaForm();
		KFBetaForm.Owner = this;
		KFBetaForm.ShowDialog();
		Log($"KFα = {SpurGear.KFalpha}");
		Log($"KFβ = {SpurGear.KFbeta}");
		Log($"KFυ = {SpurGear.KFipsilon}");
		Log("Коэффициент расчетной нагрузки по напряжениям изгиба:");
		Log($"KF = {SpurGear.KF}");
		do
		{
			Log($"{SpurGear.SigmaFFinal} > {SpurGear.SigmaF}");
			Log($"Погрешность ΔσF = {SpurGear.DeltaSigmaF}% {SpurGear.IsDeltaSigmaFAcceptable}");
			if (!SpurGear.IsDeltaSigmaFAcceptable)
			{
				SpurGear.Optimizem();
				Log($"aw = {SpurGear.Aw} мм");
				Log($"m = {SpurGear.M} мм");
				Log($"zΔ = {SpurGear.zDelta}");
				Log($"z1 = {SpurGear.z1}");
				Log($"z2 = {SpurGear.z2}");
				Log($"{SpurGear.SigmaFFinal} > {SpurGear.SigmaF}");
				Log($"Погрешность ΔσF = {SpurGear.DeltaSigmaF}% {SpurGear.IsDeltaSigmaFAcceptable}");
			}
		} while (!SpurGear.IsDeltaSigmaFAcceptable);
		Log($"Максимальные контактные напряжения:");
		Log($"σHmax = {SpurGear.SigmaHFinalMax} <= {SpurGear.SigmaHMax} {SpurGear.IsSigmaHFinalMaxAcceptable}");
		Log($"Максимальные напряжения изгиба:");
		Log($"σFmax = {SpurGear.SigmaFFinalMax} <= {SpurGear.SigmaFMax} {SpurGear.IsSigmaFFinalMaxAcceptable}");

		Log("\n===Расчет геометрии передачи===");
		Log("Диаметры вершин зубьев:");
		Log($"da1 = {SpurGear.Gear.da} мм");
		Log($"da2 = {SpurGear.Wheel.da} мм");
		Log("Диаметры впадин зубьев:");
		Log($"df1 = {SpurGear.Gear.df} мм");
		Log($"df2 = {SpurGear.Wheel.df} мм");

		Log("\n===РАСЧЕТ ОКОНЧЕН===");

		PopulateDataGridView();
	}

	private void PopulateDataGridView()
	{
		dataGridView.Columns.Clear();
		dataGridView.Rows.Clear();
		dataGridView.Columns.Add("Parameter", "Параметр");
		dataGridView.Columns.Add("SpurGear1", "Шестерня 1");
		dataGridView.Columns.Add("SpurGear2", "Шестерня 2");

		// Add rows to DataGridView
		dataGridView.Rows.Add("Число зубьев z", SpurGear.z1, SpurGear.z2);
		dataGridView.Rows.Add("Модуль m, мм", SpurGear.M, "");
		dataGridView.Rows.Add("Фактическое передаточное число u = z2/z1", (double)SpurGear.z2 / SpurGear.z1, "");
		dataGridView.Rows.Add("Угол наклона зубьев βº", SpurGear.beta, "");
		dataGridView.Rows.Add("Делительный диаметр d, мм", SpurGear.d1, SpurGear.d2);
		dataGridView.Rows.Add("Диаметр вершин da, мм", SpurGear.Gear.da, SpurGear.Wheel.da);
		dataGridView.Rows.Add("Диаметр впадин df, мм", SpurGear.Gear.df, SpurGear.Wheel.df);
		dataGridView.Rows.Add("Ширина зубчатого венца bw, мм", SpurGear.Gear.Bw, SpurGear.Wheel.Bw);
		dataGridView.Rows.Add("Коэффициент смещения х", 0, 0);
		dataGridView.Rows.Add("Межосевое расстояние Aw, мм", SpurGear.Aw, "");
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
		rtb_Log.SelectionStart = rtb_Log.Text.Length;
		rtb_Log.ScrollToCaret();
	}
}