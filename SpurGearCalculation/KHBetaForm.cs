namespace SpurGearCalculation;

public partial class KHBetaForm : Form
{
	public KHBetaForm()
	{
		InitializeComponent();
	}

	private void btn_Enter_Click(object sender, EventArgs e)
	{
		try
		{
			(Owner as MainForm).SpurGear.KHBeta = double.Parse(tb_KHBeta.Text);

			Close();
		}
		catch { }
	}
}
