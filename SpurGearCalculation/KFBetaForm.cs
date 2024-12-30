namespace SpurGearCalculation;
public partial class KFBetaForm : Form
{
	public KFBetaForm()
	{
		InitializeComponent();
	}

	private void btn_Enter_Click(object sender, EventArgs e)
	{
		try
		{
			(Owner as MainForm).SpurGear.KFbeta = double.Parse(tb_KFBeta.Text);

			Close();
		}
		catch { }
	}
}
