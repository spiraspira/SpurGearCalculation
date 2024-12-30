namespace SpurGearCalculation;

partial class KHBetaForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KHBetaForm));
		pictureBox1 = new PictureBox();
		tb_KHBeta = new TextBox();
		btn_Enter = new Button();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		SuspendLayout();
		// 
		// pictureBox1
		// 
		pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new Point(12, 12);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new Size(823, 760);
		pictureBox1.TabIndex = 0;
		pictureBox1.TabStop = false;
		// 
		// tb_KHBeta
		// 
		tb_KHBeta.Location = new Point(841, 12);
		tb_KHBeta.Name = "tb_KHBeta";
		tb_KHBeta.Size = new Size(164, 27);
		tb_KHBeta.TabIndex = 1;
		// 
		// btn_Enter
		// 
		btn_Enter.Location = new Point(841, 45);
		btn_Enter.Name = "btn_Enter";
		btn_Enter.Size = new Size(164, 29);
		btn_Enter.TabIndex = 2;
		btn_Enter.Text = "Ввод";
		btn_Enter.UseVisualStyleBackColor = true;
		btn_Enter.Click += btn_Enter_Click;
		// 
		// KHBetaForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1017, 784);
		Controls.Add(btn_Enter);
		Controls.Add(tb_KHBeta);
		Controls.Add(pictureBox1);
		Name = "KHBetaForm";
		Text = "Выбор KHβ";
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private PictureBox pictureBox1;
	private TextBox tb_KHBeta;
	private Button btn_Enter;
}