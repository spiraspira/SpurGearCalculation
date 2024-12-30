namespace SpurGearCalculation;

partial class KFBetaForm
{
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	/// Required method for Designer support - do not modify
	/// the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KFBetaForm));
		pictureBox1 = new PictureBox();
		tb_KFBeta = new TextBox();
		btn_Enter = new Button();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		SuspendLayout();
		// 
		// pictureBox1
		// 
		pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new Point(12, 12);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new Size(835, 457);
		pictureBox1.TabIndex = 0;
		pictureBox1.TabStop = false;
		// 
		// tb_KFBeta
		// 
		tb_KFBeta.Location = new Point(853, 12);
		tb_KFBeta.Name = "tb_KFBeta";
		tb_KFBeta.Size = new Size(125, 27);
		tb_KFBeta.TabIndex = 1;
		// 
		// btn_Enter
		// 
		btn_Enter.Location = new Point(853, 45);
		btn_Enter.Name = "btn_Enter";
		btn_Enter.Size = new Size(125, 29);
		btn_Enter.TabIndex = 2;
		btn_Enter.Text = "Ввести";
		btn_Enter.UseVisualStyleBackColor = true;
		btn_Enter.Click += btn_Enter_Click;
		// 
		// KFBetaForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(993, 483);
		Controls.Add(btn_Enter);
		Controls.Add(tb_KFBeta);
		Controls.Add(pictureBox1);
		Name = "KFBetaForm";
		Text = "Выбор KFβ";
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private PictureBox pictureBox1;
	private TextBox tb_KFBeta;
	private Button btn_Enter;
}