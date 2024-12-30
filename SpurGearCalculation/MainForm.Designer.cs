namespace SpurGearCalculation
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			label1 = new Label();
			nud_L = new NumericUpDown();
			label2 = new Label();
			cb_Kd = new ComboBox();
			label3 = new Label();
			cb_Ks = new ComboBox();
			cb_WorkModeType = new ComboBox();
			label4 = new Label();
			nud_ManufactoringAccuracy = new NumericUpDown();
			label5 = new Label();
			nud_Kp = new NumericUpDown();
			label6 = new Label();
			nud_U = new NumericUpDown();
			label7 = new Label();
			label8 = new Label();
			label9 = new Label();
			tb_n1 = new TextBox();
			tb_T1 = new TextBox();
			label10 = new Label();
			label11 = new Label();
			label12 = new Label();
			label13 = new Label();
			tb_T2 = new TextBox();
			tb_n2 = new TextBox();
			groupBox1 = new GroupBox();
			cb_IsReversible = new ComboBox();
			groupBox2 = new GroupBox();
			label17 = new Label();
			label16 = new Label();
			cb_ProcessingTypeWheel = new ComboBox();
			cb_SteelMarkWheel = new ComboBox();
			label15 = new Label();
			cb_ProcessingTypeGear = new ComboBox();
			label14 = new Label();
			cb_SteelMarkGear = new ComboBox();
			rtb_Log = new RichTextBox();
			label18 = new Label();
			b_Calculate = new Button();
			dataGridView = new DataGridView();
			((System.ComponentModel.ISupportInitialize)nud_L).BeginInit();
			((System.ComponentModel.ISupportInitialize)nud_ManufactoringAccuracy).BeginInit();
			((System.ComponentModel.ISupportInitialize)nud_Kp).BeginInit();
			((System.ComponentModel.ISupportInitialize)nud_U).BeginInit();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(12, 28);
			label1.Name = "label1";
			label1.Size = new Size(167, 20);
			label1.TabIndex = 1;
			label1.Text = "Срок службы привода:";
			// 
			// nud_L
			// 
			nud_L.Location = new Point(212, 26);
			nud_L.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nud_L.Name = "nud_L";
			nud_L.Size = new Size(151, 27);
			nud_L.TabIndex = 2;
			nud_L.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(369, 28);
			label2.Name = "label2";
			label2.Size = new Size(31, 20);
			label2.TabIndex = 3;
			label2.Text = "лет";
			// 
			// cb_Kd
			// 
			cb_Kd.DisplayMember = "1";
			cb_Kd.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_Kd.FormattingEnabled = true;
			cb_Kd.Items.AddRange(new object[] { "Пятидневный", "Шестидневный" });
			cb_Kd.Location = new Point(212, 59);
			cb_Kd.Name = "cb_Kd";
			cb_Kd.Size = new Size(188, 28);
			cb_Kd.TabIndex = 4;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(12, 62);
			label3.Name = "label3";
			label3.Size = new Size(115, 20);
			label3.TabIndex = 5;
			label3.Text = "Режим работы:";
			// 
			// cb_Ks
			// 
			cb_Ks.DisplayMember = "1";
			cb_Ks.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_Ks.FormattingEnabled = true;
			cb_Ks.Items.AddRange(new object[] { "Односменный", "Двухсменный" });
			cb_Ks.Location = new Point(212, 93);
			cb_Ks.Name = "cb_Ks";
			cb_Ks.Size = new Size(188, 28);
			cb_Ks.TabIndex = 6;
			// 
			// cb_WorkModeType
			// 
			cb_WorkModeType.DisplayMember = "1";
			cb_WorkModeType.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_WorkModeType.FormattingEnabled = true;
			cb_WorkModeType.Items.AddRange(new object[] { "0 - Постоянный", "I - Тяжелый", "II - Средний равновероятный", "III - Средний нормальный", "IV - Легкий", "V - Особо легкий" });
			cb_WorkModeType.Location = new Point(212, 127);
			cb_WorkModeType.Name = "cb_WorkModeType";
			cb_WorkModeType.Size = new Size(188, 28);
			cb_WorkModeType.TabIndex = 7;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(12, 164);
			label4.Name = "label4";
			label4.Size = new Size(116, 20);
			label4.TabIndex = 8;
			label4.Text = "Реверсивность:";
			// 
			// nud_ManufactoringAccuracy
			// 
			nud_ManufactoringAccuracy.Location = new Point(212, 195);
			nud_ManufactoringAccuracy.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
			nud_ManufactoringAccuracy.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
			nud_ManufactoringAccuracy.Name = "nud_ManufactoringAccuracy";
			nud_ManufactoringAccuracy.Size = new Size(151, 27);
			nud_ManufactoringAccuracy.TabIndex = 11;
			nud_ManufactoringAccuracy.Value = new decimal(new int[] { 6, 0, 0, 0 });
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(12, 197);
			label5.Name = "label5";
			label5.Size = new Size(167, 20);
			label5.TabIndex = 10;
			label5.Text = "Срок службы привода:";
			// 
			// nud_Kp
			// 
			nud_Kp.Location = new Point(212, 228);
			nud_Kp.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nud_Kp.Name = "nud_Kp";
			nud_Kp.Size = new Size(151, 27);
			nud_Kp.TabIndex = 13;
			nud_Kp.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(12, 230);
			label6.Name = "label6";
			label6.Size = new Size(178, 20);
			label6.TabIndex = 12;
			label6.Text = "Допустимая перегрузка:";
			// 
			// nud_U
			// 
			nud_U.Location = new Point(212, 261);
			nud_U.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			nud_U.Name = "nud_U";
			nud_U.Size = new Size(151, 27);
			nud_U.TabIndex = 15;
			nud_U.Value = new decimal(new int[] { 1, 0, 0, 0 });
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(12, 263);
			label7.Name = "label7";
			label7.Size = new Size(196, 20);
			label7.TabIndex = 14;
			label7.Text = "Передаточное отношение:";
			// 
			// label8
			// 
			label8.AutoSize = true;
			label8.Location = new Point(12, 297);
			label8.Name = "label8";
			label8.Size = new Size(81, 20);
			label8.TabIndex = 16;
			label8.Text = "Шестерня:";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Location = new Point(12, 330);
			label9.Name = "label9";
			label9.Size = new Size(62, 20);
			label9.TabIndex = 17;
			label9.Text = "Колесо:";
			// 
			// tb_n1
			// 
			tb_n1.Location = new Point(148, 294);
			tb_n1.Name = "tb_n1";
			tb_n1.Size = new Size(76, 27);
			tb_n1.TabIndex = 18;
			// 
			// tb_T1
			// 
			tb_T1.Location = new Point(279, 294);
			tb_T1.Name = "tb_T1";
			tb_T1.Size = new Size(84, 27);
			tb_T1.TabIndex = 19;
			// 
			// label10
			// 
			label10.AutoSize = true;
			label10.Location = new Point(99, 297);
			label10.Name = "label10";
			label10.Size = new Size(43, 20);
			label10.TabIndex = 20;
			label10.Text = "n1 = ";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(230, 297);
			label11.Name = "label11";
			label11.Size = new Size(43, 20);
			label11.TabIndex = 21;
			label11.Text = "T1 = ";
			// 
			// label12
			// 
			label12.AutoSize = true;
			label12.Location = new Point(230, 330);
			label12.Name = "label12";
			label12.Size = new Size(43, 20);
			label12.TabIndex = 25;
			label12.Text = "T2 = ";
			// 
			// label13
			// 
			label13.AutoSize = true;
			label13.Location = new Point(99, 330);
			label13.Name = "label13";
			label13.Size = new Size(43, 20);
			label13.TabIndex = 24;
			label13.Text = "n2 = ";
			// 
			// tb_T2
			// 
			tb_T2.Location = new Point(279, 327);
			tb_T2.Name = "tb_T2";
			tb_T2.Size = new Size(84, 27);
			tb_T2.TabIndex = 23;
			// 
			// tb_n2
			// 
			tb_n2.Location = new Point(148, 327);
			tb_n2.Name = "tb_n2";
			tb_n2.Size = new Size(76, 27);
			tb_n2.TabIndex = 22;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(cb_IsReversible);
			groupBox1.Controls.Add(label12);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(label13);
			groupBox1.Controls.Add(nud_L);
			groupBox1.Controls.Add(tb_T2);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(tb_n2);
			groupBox1.Controls.Add(cb_Kd);
			groupBox1.Controls.Add(label11);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(label10);
			groupBox1.Controls.Add(cb_Ks);
			groupBox1.Controls.Add(tb_T1);
			groupBox1.Controls.Add(cb_WorkModeType);
			groupBox1.Controls.Add(tb_n1);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(label9);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(label8);
			groupBox1.Controls.Add(nud_ManufactoringAccuracy);
			groupBox1.Controls.Add(nud_U);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(label7);
			groupBox1.Controls.Add(nud_Kp);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(574, 371);
			groupBox1.TabIndex = 26;
			groupBox1.TabStop = false;
			groupBox1.Text = "Исходные данные";
			// 
			// cb_IsReversible
			// 
			cb_IsReversible.DisplayMember = "1";
			cb_IsReversible.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_IsReversible.FormattingEnabled = true;
			cb_IsReversible.Items.AddRange(new object[] { "Реверсивный", "Нереверсивный" });
			cb_IsReversible.Location = new Point(212, 164);
			cb_IsReversible.Name = "cb_IsReversible";
			cb_IsReversible.Size = new Size(188, 28);
			cb_IsReversible.TabIndex = 27;
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(label17);
			groupBox2.Controls.Add(label16);
			groupBox2.Controls.Add(cb_ProcessingTypeWheel);
			groupBox2.Controls.Add(cb_SteelMarkWheel);
			groupBox2.Controls.Add(label15);
			groupBox2.Controls.Add(cb_ProcessingTypeGear);
			groupBox2.Controls.Add(label14);
			groupBox2.Controls.Add(cb_SteelMarkGear);
			groupBox2.Location = new Point(12, 389);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(574, 127);
			groupBox2.TabIndex = 27;
			groupBox2.TabStop = false;
			groupBox2.Text = "Выбор стали";
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new Point(369, 23);
			label17.Name = "label17";
			label17.Size = new Size(62, 20);
			label17.TabIndex = 32;
			label17.Text = "Колесо:";
			// 
			// label16
			// 
			label16.AutoSize = true;
			label16.Location = new Point(212, 23);
			label16.Name = "label16";
			label16.Size = new Size(81, 20);
			label16.TabIndex = 31;
			label16.Text = "Шестерня:";
			// 
			// cb_ProcessingTypeWheel
			// 
			cb_ProcessingTypeWheel.DisplayMember = "1";
			cb_ProcessingTypeWheel.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_ProcessingTypeWheel.FormattingEnabled = true;
			cb_ProcessingTypeWheel.Location = new Point(369, 81);
			cb_ProcessingTypeWheel.Name = "cb_ProcessingTypeWheel";
			cb_ProcessingTypeWheel.Size = new Size(151, 28);
			cb_ProcessingTypeWheel.TabIndex = 30;
			// 
			// cb_SteelMarkWheel
			// 
			cb_SteelMarkWheel.DisplayMember = "1";
			cb_SteelMarkWheel.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_SteelMarkWheel.FormattingEnabled = true;
			cb_SteelMarkWheel.Location = new Point(369, 47);
			cb_SteelMarkWheel.Name = "cb_SteelMarkWheel";
			cb_SteelMarkWheel.Size = new Size(151, 28);
			cb_SteelMarkWheel.TabIndex = 29;
			cb_SteelMarkWheel.SelectedIndexChanged += cb_SteelMarkWheel_SelectedIndexChanged;
			// 
			// label15
			// 
			label15.AutoSize = true;
			label15.Location = new Point(12, 84);
			label15.Name = "label15";
			label15.Size = new Size(160, 20);
			label15.TabIndex = 27;
			label15.Text = "Тип термообработки:";
			// 
			// cb_ProcessingTypeGear
			// 
			cb_ProcessingTypeGear.DisplayMember = "1";
			cb_ProcessingTypeGear.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_ProcessingTypeGear.FormattingEnabled = true;
			cb_ProcessingTypeGear.Location = new Point(212, 81);
			cb_ProcessingTypeGear.Name = "cb_ProcessingTypeGear";
			cb_ProcessingTypeGear.Size = new Size(151, 28);
			cb_ProcessingTypeGear.TabIndex = 28;
			// 
			// label14
			// 
			label14.AutoSize = true;
			label14.Location = new Point(12, 50);
			label14.Name = "label14";
			label14.Size = new Size(99, 20);
			label14.TabIndex = 26;
			label14.Text = "Марка стали:";
			// 
			// cb_SteelMarkGear
			// 
			cb_SteelMarkGear.DisplayMember = "1";
			cb_SteelMarkGear.DropDownStyle = ComboBoxStyle.DropDownList;
			cb_SteelMarkGear.FormattingEnabled = true;
			cb_SteelMarkGear.Location = new Point(212, 47);
			cb_SteelMarkGear.Name = "cb_SteelMarkGear";
			cb_SteelMarkGear.Size = new Size(151, 28);
			cb_SteelMarkGear.TabIndex = 26;
			cb_SteelMarkGear.SelectedIndexChanged += cb_SteelMark_SelectedIndexChanged;
			// 
			// rtb_Log
			// 
			rtb_Log.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
			rtb_Log.Location = new Point(592, 71);
			rtb_Log.Name = "rtb_Log";
			rtb_Log.ReadOnly = true;
			rtb_Log.Size = new Size(316, 440);
			rtb_Log.TabIndex = 28;
			rtb_Log.Text = "";
			// 
			// label18
			// 
			label18.AutoSize = true;
			label18.Location = new Point(592, 45);
			label18.Name = "label18";
			label18.Size = new Size(127, 20);
			label18.TabIndex = 29;
			label18.Text = "Лог вычислений:";
			// 
			// b_Calculate
			// 
			b_Calculate.Location = new Point(592, 12);
			b_Calculate.Name = "b_Calculate";
			b_Calculate.Size = new Size(316, 29);
			b_Calculate.TabIndex = 30;
			b_Calculate.Text = "Вычислить";
			b_Calculate.UseVisualStyleBackColor = true;
			b_Calculate.Click += b_Calculate_Click;
			// 
			// dataGridView
			// 
			dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView.Location = new Point(930, 16);
			dataGridView.Name = "dataGridView";
			dataGridView.RowHeadersWidth = 51;
			dataGridView.Size = new Size(695, 500);
			dataGridView.TabIndex = 31;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1637, 526);
			Controls.Add(dataGridView);
			Controls.Add(b_Calculate);
			Controls.Add(label18);
			Controls.Add(rtb_Log);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "MainForm";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Рассчет прямозубой цилиндрической зубчатой передачи";
			((System.ComponentModel.ISupportInitialize)nud_L).EndInit();
			((System.ComponentModel.ISupportInitialize)nud_ManufactoringAccuracy).EndInit();
			((System.ComponentModel.ISupportInitialize)nud_Kp).EndInit();
			((System.ComponentModel.ISupportInitialize)nud_U).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label label1;
		private NumericUpDown nud_L;
		private Label label2;
		private ComboBox cb_Kd;
		private Label label3;
		private ComboBox cb_Ks;
		private ComboBox cb_WorkModeType;
		private Label label4;
		private NumericUpDown nud_ManufactoringAccuracy;
		private Label label5;
		private NumericUpDown nud_Kp;
		private Label label6;
		private NumericUpDown nud_U;
		private Label label7;
		private Label label8;
		private Label label9;
		private TextBox tb_n1;
		private TextBox tb_T1;
		private Label label10;
		private Label label11;
		private Label label12;
		private Label label13;
		private TextBox tb_T2;
		private TextBox tb_n2;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label14;
		private ComboBox cb_SteelMarkGear;
		private Label label15;
		private ComboBox cb_ProcessingTypeGear;
		private ComboBox cb_IsReversible;
		private Label label17;
		private Label label16;
		private ComboBox cb_ProcessingTypeWheel;
		private ComboBox cb_SteelMarkWheel;
		private RichTextBox rtb_Log;
		private Label label18;
		private Button b_Calculate;
		private DataGridView dataGridView;
	}
}
