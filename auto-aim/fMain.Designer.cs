namespace auto_aim
{
    partial class fMain
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
            btStart = new Button();
            pictureBox = new PictureBox();
            cbDrawDetection = new CheckBox();
            lbFPS = new Label();
            lbWidthPlayer = new Label();
            lbHeightPlayer = new Label();
            lbEstimateDistancePlayer = new Label();
            lbWidthHead = new Label();
            lbHeightHead = new Label();
            lbEstimateDistanceHead = new Label();
            cbEnableDetection = new CheckBox();
            cbEnableGun1 = new CheckBox();
            cbEnableGun2 = new CheckBox();
            nBitmapW = new NumericUpDown();
            nBitmapH = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            cbEnableAimHead = new CheckBox();
            cbEnableAimPlayer = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapH).BeginInit();
            SuspendLayout();
            // 
            // btStart
            // 
            btStart.Location = new Point(664, 393);
            btStart.Name = "btStart";
            btStart.Size = new Size(124, 45);
            btStart.TabIndex = 0;
            btStart.Text = "START";
            btStart.UseVisualStyleBackColor = true;
            btStart.Click += btStart_Click;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(400, 400);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            // 
            // cbDrawDetection
            // 
            cbDrawDetection.AutoSize = true;
            cbDrawDetection.Location = new Point(445, 37);
            cbDrawDetection.Name = "cbDrawDetection";
            cbDrawDetection.Size = new Size(138, 19);
            cbDrawDetection.TabIndex = 2;
            cbDrawDetection.Text = "(F2) - Draw Detection";
            cbDrawDetection.UseVisualStyleBackColor = true;
            cbDrawDetection.CheckedChanged += cbDrawPredict_CheckedChanged;
            // 
            // lbFPS
            // 
            lbFPS.AutoSize = true;
            lbFPS.Location = new Point(12, 9);
            lbFPS.Name = "lbFPS";
            lbFPS.Size = new Size(50, 15);
            lbFPS.TabIndex = 3;
            lbFPS.Text = "FPS: --.-";
            // 
            // lbWidthPlayer
            // 
            lbWidthPlayer.AutoSize = true;
            lbWidthPlayer.Location = new Point(445, 338);
            lbWidthPlayer.Name = "lbWidthPlayer";
            lbWidthPlayer.Size = new Size(55, 15);
            lbWidthPlayer.TabIndex = 3;
            lbWidthPlayer.Text = "Width: --";
            // 
            // lbHeightPlayer
            // 
            lbHeightPlayer.AutoSize = true;
            lbHeightPlayer.Location = new Point(445, 353);
            lbHeightPlayer.Name = "lbHeightPlayer";
            lbHeightPlayer.Size = new Size(59, 15);
            lbHeightPlayer.TabIndex = 3;
            lbHeightPlayer.Text = "Height: --";
            // 
            // lbEstimateDistancePlayer
            // 
            lbEstimateDistancePlayer.AutoSize = true;
            lbEstimateDistancePlayer.Location = new Point(445, 368);
            lbEstimateDistancePlayer.Name = "lbEstimateDistancePlayer";
            lbEstimateDistancePlayer.Size = new Size(116, 15);
            lbEstimateDistancePlayer.TabIndex = 3;
            lbEstimateDistancePlayer.Text = "Estimate Distance: --";
            // 
            // lbWidthHead
            // 
            lbWidthHead.AutoSize = true;
            lbWidthHead.Location = new Point(445, 254);
            lbWidthHead.Name = "lbWidthHead";
            lbWidthHead.Size = new Size(55, 15);
            lbWidthHead.TabIndex = 3;
            lbWidthHead.Text = "Width: --";
            // 
            // lbHeightHead
            // 
            lbHeightHead.AutoSize = true;
            lbHeightHead.Location = new Point(445, 269);
            lbHeightHead.Name = "lbHeightHead";
            lbHeightHead.Size = new Size(59, 15);
            lbHeightHead.TabIndex = 3;
            lbHeightHead.Text = "Height: --";
            // 
            // lbEstimateDistanceHead
            // 
            lbEstimateDistanceHead.AutoSize = true;
            lbEstimateDistanceHead.Location = new Point(445, 284);
            lbEstimateDistanceHead.Name = "lbEstimateDistanceHead";
            lbEstimateDistanceHead.Size = new Size(116, 15);
            lbEstimateDistanceHead.TabIndex = 3;
            lbEstimateDistanceHead.Text = "Estimate Distance: --";
            // 
            // cbEnableDetection
            // 
            cbEnableDetection.AutoSize = true;
            cbEnableDetection.Location = new Point(445, 12);
            cbEnableDetection.Name = "cbEnableDetection";
            cbEnableDetection.Size = new Size(146, 19);
            cbEnableDetection.TabIndex = 2;
            cbEnableDetection.Text = "(F1) - Enable Detection";
            cbEnableDetection.UseVisualStyleBackColor = true;
            cbEnableDetection.CheckedChanged += cbEnableDetection_CheckedChanged;
            // 
            // cbEnableGun1
            // 
            cbEnableGun1.AutoSize = true;
            cbEnableGun1.Location = new Point(445, 146);
            cbEnableGun1.Name = "cbEnableGun1";
            cbEnableGun1.Size = new Size(120, 19);
            cbEnableGun1.TabIndex = 2;
            cbEnableGun1.Text = "(1) - Enable Gun 1";
            cbEnableGun1.UseVisualStyleBackColor = true;
            cbEnableGun1.CheckedChanged += cbEnableGun1_CheckedChanged;
            // 
            // cbEnableGun2
            // 
            cbEnableGun2.AutoSize = true;
            cbEnableGun2.Location = new Point(445, 171);
            cbEnableGun2.Name = "cbEnableGun2";
            cbEnableGun2.Size = new Size(120, 19);
            cbEnableGun2.TabIndex = 2;
            cbEnableGun2.Text = "(2) - Enable Gun 2";
            cbEnableGun2.UseVisualStyleBackColor = true;
            cbEnableGun2.CheckedChanged += cbEnableGun2_CheckedChanged;
            // 
            // nBitmapW
            // 
            nBitmapW.Location = new Point(63, 418);
            nBitmapW.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nBitmapW.Name = "nBitmapW";
            nBitmapW.Size = new Size(75, 23);
            nBitmapW.TabIndex = 4;
            nBitmapW.TextAlign = HorizontalAlignment.Center;
            nBitmapW.Value = new decimal(new int[] { 250, 0, 0, 0 });
            nBitmapW.ValueChanged += nBitmapW_ValueChanged;
            // 
            // nBitmapH
            // 
            nBitmapH.Location = new Point(212, 418);
            nBitmapH.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nBitmapH.Name = "nBitmapH";
            nBitmapH.Size = new Size(75, 23);
            nBitmapH.TabIndex = 4;
            nBitmapH.TextAlign = HorizontalAlignment.Center;
            nBitmapH.Value = new decimal(new int[] { 250, 0, 0, 0 });
            nBitmapH.ValueChanged += nBitmapH_ValueChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 423);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 5;
            label1.Text = "Width: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(160, 423);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 5;
            label2.Text = "Height:";
            // 
            // cbEnableAimHead
            // 
            cbEnableAimHead.AutoSize = true;
            cbEnableAimHead.Location = new Point(445, 78);
            cbEnableAimHead.Name = "cbEnableAimHead";
            cbEnableAimHead.Size = new Size(145, 19);
            cbEnableAimHead.TabIndex = 2;
            cbEnableAimHead.Text = "(H) - Enable Aim Head";
            cbEnableAimHead.UseVisualStyleBackColor = true;
            cbEnableAimHead.CheckedChanged += cbEnableAimHead_CheckedChanged;
            // 
            // cbEnableAimPlayer
            // 
            cbEnableAimPlayer.AutoSize = true;
            cbEnableAimPlayer.Location = new Point(445, 103);
            cbEnableAimPlayer.Name = "cbEnableAimPlayer";
            cbEnableAimPlayer.Size = new Size(147, 19);
            cbEnableAimPlayer.TabIndex = 2;
            cbEnableAimPlayer.Text = "(P) - Enable Aim Player";
            cbEnableAimPlayer.UseVisualStyleBackColor = true;
            cbEnableAimPlayer.CheckedChanged += cbEnableAimPlayer_CheckedChanged;
            // 
            // fMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 465);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nBitmapH);
            Controls.Add(nBitmapW);
            Controls.Add(lbEstimateDistanceHead);
            Controls.Add(lbHeightHead);
            Controls.Add(lbEstimateDistancePlayer);
            Controls.Add(lbWidthHead);
            Controls.Add(lbHeightPlayer);
            Controls.Add(lbWidthPlayer);
            Controls.Add(lbFPS);
            Controls.Add(cbEnableDetection);
            Controls.Add(cbEnableGun2);
            Controls.Add(cbEnableAimPlayer);
            Controls.Add(cbEnableAimHead);
            Controls.Add(cbEnableGun1);
            Controls.Add(cbDrawDetection);
            Controls.Add(pictureBox);
            Controls.Add(btStart);
            Name = "fMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YoloDotnet";
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapW).EndInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapH).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStart;
        private PictureBox pictureBox;
        private CheckBox cbDrawDetection;
        private Label lbFPS;
        private Label lbWidthPlayer;
        private Label lbHeightPlayer;
        private Label lbEstimateDistancePlayer;
        private Label lbWidthHead;
        private Label lbHeightHead;
        private Label lbEstimateDistanceHead;
        private CheckBox cbEnableDetection;
        private CheckBox cbEnableGun1;
        private CheckBox cbEnableGun2;
        private NumericUpDown nBitmapW;
        private NumericUpDown nBitmapH;
        private Label label1;
        private Label label2;
        private CheckBox cbEnableAimHead;
        private CheckBox cbEnableAimPlayer;
    }
}
