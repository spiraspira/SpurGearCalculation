namespace SpurGearCalculation;
public partial class YFSForm : Form
{
	public YFSForm()
	{
		InitializeComponent();
	}

	private void btn_Enter_Click(object sender, EventArgs e)
	{
		try
		{
			(Owner as MainForm).SpurGear.Gear.Yfs = double.Parse(tb_YFS1.Text);
			(Owner as MainForm).SpurGear.Wheel.Yfs = double.Parse(tb_YFS2.Text);

			Close();
		}
		catch { }
	}
}
