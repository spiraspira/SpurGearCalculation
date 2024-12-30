namespace SpurGearCalculation;

partial class YFSForm
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YFSForm));
		pictureBox1 = new PictureBox();
		tb_YFS1 = new TextBox();
		label1 = new Label();
		label2 = new Label();
		tb_YFS2 = new TextBox();
		btn_Enter = new Button();
		((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
		SuspendLayout();
		// 
		// pictureBox1
		// 
		pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
		pictureBox1.Location = new Point(12, 12);
		pictureBox1.Name = "pictureBox1";
		pictureBox1.Size = new Size(867, 631);
		pictureBox1.TabIndex = 0;
		pictureBox1.TabStop = false;
		// 
		// tb_YFS1
		// 
		tb_YFS1.Location = new Point(954, 12);
		tb_YFS1.Name = "tb_YFS1";
		tb_YFS1.Size = new Size(160, 27);
		tb_YFS1.TabIndex = 1;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(885, 15);
		label1.Name = "label1";
		label1.Size = new Size(54, 20);
		label1.TabIndex = 2;
		label1.Text = "YFS1 =";
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(885, 48);
		label2.Name = "label2";
		label2.Size = new Size(54, 20);
		label2.TabIndex = 4;
		label2.Text = "YFS2 =";
		// 
		// tb_YFS2
		// 
		tb_YFS2.Location = new Point(954, 45);
		tb_YFS2.Name = "tb_YFS2";
		tb_YFS2.Size = new Size(160, 27);
		tb_YFS2.TabIndex = 3;
		// 
		// btn_Enter
		// 
		btn_Enter.Location = new Point(885, 78);
		btn_Enter.Name = "btn_Enter";
		btn_Enter.Size = new Size(229, 29);
		btn_Enter.TabIndex = 5;
		btn_Enter.Text = "Ввести";
		btn_Enter.UseVisualStyleBackColor = true;
		btn_Enter.Click += btn_Enter_Click;
		// 
		// YFSForm
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1126, 644);
		Controls.Add(btn_Enter);
		Controls.Add(label2);
		Controls.Add(tb_YFS2);
		Controls.Add(label1);
		Controls.Add(tb_YFS1);
		Controls.Add(pictureBox1);
		Name = "YFSForm";
		Text = "Выбор YFS";
		((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private PictureBox pictureBox1;
	private TextBox tb_YFS1;
	private Label label1;
	private Label label2;
	private TextBox tb_YFS2;
	private Button btn_Enter;
}