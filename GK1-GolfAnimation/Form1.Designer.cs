
namespace GK1_GolfAnimation
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FOVnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.fovTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.phongRadioButton = new System.Windows.Forms.RadioButton();
            this.gouraudRadioButton = new System.Windows.Forms.RadioButton();
            this.constantRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.followingBallRadioButton = new System.Windows.Forms.RadioButton();
            this.trackingBallRadioButton = new System.Windows.Forms.RadioButton();
            this.staticRadioButton = new System.Windows.Forms.RadioButton();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOVnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.mainPictureBox);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(210, 450);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FOVnumericUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.fovTrackBar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Variables";
            // 
            // FOVnumericUpDown
            // 
            this.FOVnumericUpDown.Location = new System.Drawing.Point(161, 41);
            this.FOVnumericUpDown.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.FOVnumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FOVnumericUpDown.Name = "FOVnumericUpDown";
            this.FOVnumericUpDown.Size = new System.Drawing.Size(43, 23);
            this.FOVnumericUpDown.TabIndex = 2;
            this.FOVnumericUpDown.Value = new decimal(new int[] {
            55,
            0,
            0,
            0});
            this.FOVnumericUpDown.ValueChanged += new System.EventHandler(this.FOVnumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "FOV:";
            // 
            // fovTrackBar
            // 
            this.fovTrackBar.Location = new System.Drawing.Point(12, 41);
            this.fovTrackBar.Maximum = 180;
            this.fovTrackBar.Minimum = 1;
            this.fovTrackBar.Name = "fovTrackBar";
            this.fovTrackBar.Size = new System.Drawing.Size(143, 45);
            this.fovTrackBar.TabIndex = 0;
            this.fovTrackBar.TickFrequency = 20;
            this.fovTrackBar.Value = 55;
            this.fovTrackBar.Scroll += new System.EventHandler(this.fovTrackBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.phongRadioButton);
            this.groupBox2.Controls.Add(this.gouraudRadioButton);
            this.groupBox2.Controls.Add(this.constantRadioButton);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 152);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shading";
            // 
            // phongRadioButton
            // 
            this.phongRadioButton.AutoSize = true;
            this.phongRadioButton.Location = new System.Drawing.Point(12, 76);
            this.phongRadioButton.Name = "phongRadioButton";
            this.phongRadioButton.Size = new System.Drawing.Size(60, 19);
            this.phongRadioButton.TabIndex = 2;
            this.phongRadioButton.TabStop = true;
            this.phongRadioButton.Text = "Phong";
            this.phongRadioButton.UseVisualStyleBackColor = true;
            this.phongRadioButton.CheckedChanged += new System.EventHandler(this.phongRadioButton_CheckedChanged);
            // 
            // gouraudRadioButton
            // 
            this.gouraudRadioButton.AutoSize = true;
            this.gouraudRadioButton.Location = new System.Drawing.Point(12, 51);
            this.gouraudRadioButton.Name = "gouraudRadioButton";
            this.gouraudRadioButton.Size = new System.Drawing.Size(71, 19);
            this.gouraudRadioButton.TabIndex = 1;
            this.gouraudRadioButton.TabStop = true;
            this.gouraudRadioButton.Text = "Gouraud";
            this.gouraudRadioButton.UseVisualStyleBackColor = true;
            this.gouraudRadioButton.CheckedChanged += new System.EventHandler(this.gouraudRadioButton_CheckedChanged);
            // 
            // constantRadioButton
            // 
            this.constantRadioButton.AutoSize = true;
            this.constantRadioButton.Location = new System.Drawing.Point(12, 26);
            this.constantRadioButton.Name = "constantRadioButton";
            this.constantRadioButton.Size = new System.Drawing.Size(73, 19);
            this.constantRadioButton.TabIndex = 0;
            this.constantRadioButton.TabStop = true;
            this.constantRadioButton.Text = "Constant";
            this.constantRadioButton.UseVisualStyleBackColor = true;
            this.constantRadioButton.CheckedChanged += new System.EventHandler(this.constantRadioButton_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.followingBallRadioButton);
            this.groupBox3.Controls.Add(this.trackingBallRadioButton);
            this.groupBox3.Controls.Add(this.staticRadioButton);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 255);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(204, 192);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Camera Mode";
            // 
            // followingBallRadioButton
            // 
            this.followingBallRadioButton.AutoSize = true;
            this.followingBallRadioButton.Location = new System.Drawing.Point(12, 72);
            this.followingBallRadioButton.Name = "followingBallRadioButton";
            this.followingBallRadioButton.Size = new System.Drawing.Size(99, 19);
            this.followingBallRadioButton.TabIndex = 5;
            this.followingBallRadioButton.TabStop = true;
            this.followingBallRadioButton.Text = "Following ball";
            this.followingBallRadioButton.UseVisualStyleBackColor = true;
            this.followingBallRadioButton.CheckedChanged += new System.EventHandler(this.followingBallRadioButton_CheckedChanged);
            // 
            // trackingBallRadioButton
            // 
            this.trackingBallRadioButton.AutoSize = true;
            this.trackingBallRadioButton.Location = new System.Drawing.Point(12, 47);
            this.trackingBallRadioButton.Name = "trackingBallRadioButton";
            this.trackingBallRadioButton.Size = new System.Drawing.Size(91, 19);
            this.trackingBallRadioButton.TabIndex = 4;
            this.trackingBallRadioButton.TabStop = true;
            this.trackingBallRadioButton.Text = "Tracking ball";
            this.trackingBallRadioButton.UseVisualStyleBackColor = true;
            this.trackingBallRadioButton.CheckedChanged += new System.EventHandler(this.trackingBallRadioButton_CheckedChanged);
            // 
            // staticRadioButton
            // 
            this.staticRadioButton.AutoSize = true;
            this.staticRadioButton.Location = new System.Drawing.Point(12, 22);
            this.staticRadioButton.Name = "staticRadioButton";
            this.staticRadioButton.Size = new System.Drawing.Size(54, 19);
            this.staticRadioButton.TabIndex = 3;
            this.staticRadioButton.TabStop = true;
            this.staticRadioButton.Text = "Static";
            this.staticRadioButton.UseVisualStyleBackColor = true;
            this.staticRadioButton.CheckedChanged += new System.EventHandler(this.staticRadioButton_CheckedChanged);
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(586, 450);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.SizeChanged += new System.EventHandler(this.mainPictureBox_SizeChanged);
            this.mainPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPictureBox_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "GK1 Golf Animation";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FOVnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fovTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown FOVnumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar fovTrackBar;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton gouraudRadioButton;
        private System.Windows.Forms.RadioButton constantRadioButton;
        private System.Windows.Forms.RadioButton phongRadioButton;
        private System.Windows.Forms.RadioButton followingBallRadioButton;
        private System.Windows.Forms.RadioButton trackingBallRadioButton;
        private System.Windows.Forms.RadioButton staticRadioButton;
    }
}

