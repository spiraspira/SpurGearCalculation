using SpurGearCalculation.API.Enums;
using SpurGearCalculation.API.Tables;

namespace SpurGearCalculation;

public partial class MainForm : Form
{
	public MainForm()
	{
		InitializeComponent();

		nud_L.Value = 4;
		cb_Kd.SelectedIndex = 0;
		cb_Kd.SelectedIndex = 1;
		cb_WorkModeType.SelectedIndex = 1;
		cb_IsReversible.SelectedIndex = 1;
		nud_ManufactoringAccuracy.Value = 7;
		nud_Kp.Value = 3;
		nud_U.Value = 4;
		tb_n1.Text = "66.85";
		tb_n2.Text = "15.71";
		tb_T1.Text = "227.4";
		tb_T2.Text = "873.5";

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
			cb_ProcessingTypeGear.SelectedIndex = 0;
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
			cb_ProcessingTypeWheel.SelectedIndex = 1;
		}
	}
}