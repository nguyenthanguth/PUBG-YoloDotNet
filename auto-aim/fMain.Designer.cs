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
            cbEnableDetection = new CheckBox();
            cbEnableGun1 = new CheckBox();
            cbEnableGun2 = new CheckBox();
            nBitmapW = new NumericUpDown();
            nBitmapH = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            cbEnableAimPlayer = new CheckBox();
            cbExecutionProvider = new ComboBox();
            cbModelName = new ComboBox();
            btApplyModelType = new Button();
            cbPredictMove = new CheckBox();
            groupBox1 = new GroupBox();
            lbProcessBar = new Label();
            btStopDetectionVideo = new Button();
            btScreenshotVideo = new Button();
            btYoloTrain = new Button();
            btCropeImage = new Button();
            btYoloLabels = new Button();
            cbShowPictureBoxDebug = new CheckBox();
            cbAutoFocus = new CheckBox();
            cbIsCircle = new CheckBox();
            cbPrecision = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapW).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapH).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btStart
            // 
            btStart.Location = new Point(656, 266);
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
            cbEnableGun1.Size = new Size(193, 19);
            cbEnableGun1.TabIndex = 2;
            cbEnableGun1.Text = "(1 + Left Mouse) - Enable Gun 1";
            cbEnableGun1.UseVisualStyleBackColor = true;
            cbEnableGun1.CheckedChanged += cbEnableGun1_CheckedChanged;
            // 
            // cbEnableGun2
            // 
            cbEnableGun2.AutoSize = true;
            cbEnableGun2.Location = new Point(445, 171);
            cbEnableGun2.Name = "cbEnableGun2";
            cbEnableGun2.Size = new Size(132, 19);
            cbEnableGun2.TabIndex = 2;
            cbEnableGun2.Text = "(2)   -   Enable Gun 2";
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
            // cbExecutionProvider
            // 
            cbExecutionProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            cbExecutionProvider.FormattingEnabled = true;
            cbExecutionProvider.Items.AddRange(new object[] { "CudaExecutionProvider", "TensorRtExecutionProvider" });
            cbExecutionProvider.Location = new Point(445, 218);
            cbExecutionProvider.Name = "cbExecutionProvider";
            cbExecutionProvider.Size = new Size(113, 23);
            cbExecutionProvider.TabIndex = 6;
            cbExecutionProvider.SelectedIndexChanged += cbExecutionProvider_SelectedIndexChanged;
            // 
            // cbModelName
            // 
            cbModelName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModelName.FormattingEnabled = true;
            cbModelName.Location = new Point(564, 218);
            cbModelName.Name = "cbModelName";
            cbModelName.Size = new Size(121, 23);
            cbModelName.TabIndex = 6;
            cbModelName.SelectedIndexChanged += cbModel_SelectedIndexChanged;
            // 
            // btApplyModelType
            // 
            btApplyModelType.Location = new Point(691, 217);
            btApplyModelType.Name = "btApplyModelType";
            btApplyModelType.Size = new Size(75, 23);
            btApplyModelType.TabIndex = 7;
            btApplyModelType.Text = "Apply";
            btApplyModelType.UseVisualStyleBackColor = true;
            btApplyModelType.Click += btApplyModelType_Click;
            // 
            // cbPredictMove
            // 
            cbPredictMove.AutoSize = true;
            cbPredictMove.Location = new Point(554, 280);
            cbPredictMove.Name = "cbPredictMove";
            cbPredictMove.Size = new Size(96, 19);
            cbPredictMove.TabIndex = 2;
            cbPredictMove.Text = "Predict Move";
            cbPredictMove.UseVisualStyleBackColor = true;
            cbPredictMove.CheckedChanged += cbPredictMove_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lbProcessBar);
            groupBox1.Controls.Add(btStopDetectionVideo);
            groupBox1.Controls.Add(btScreenshotVideo);
            groupBox1.Controls.Add(btYoloTrain);
            groupBox1.Controls.Add(btCropeImage);
            groupBox1.Controls.Add(btYoloLabels);
            groupBox1.Location = new Point(418, 317);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(370, 136);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            // 
            // lbProcessBar
            // 
            lbProcessBar.AutoSize = true;
            lbProcessBar.Location = new Point(130, 0);
            lbProcessBar.Name = "lbProcessBar";
            lbProcessBar.Size = new Size(64, 15);
            lbProcessBar.TabIndex = 3;
            lbProcessBar.Text = "processBar";
            // 
            // btStopDetectionVideo
            // 
            btStopDetectionVideo.Location = new Point(287, 58);
            btStopDetectionVideo.Name = "btStopDetectionVideo";
            btStopDetectionVideo.Size = new Size(75, 23);
            btStopDetectionVideo.TabIndex = 2;
            btStopDetectionVideo.Text = "Stop";
            btStopDetectionVideo.UseVisualStyleBackColor = true;
            btStopDetectionVideo.Click += btStopDetectionVideo_Click;
            // 
            // btScreenshotVideo
            // 
            btScreenshotVideo.Location = new Point(216, 22);
            btScreenshotVideo.Name = "btScreenshotVideo";
            btScreenshotVideo.Size = new Size(146, 30);
            btScreenshotVideo.TabIndex = 0;
            btScreenshotVideo.Text = "screenshot-video";
            btScreenshotVideo.UseVisualStyleBackColor = true;
            btScreenshotVideo.Click += btScreenshotVideo_Click;
            // 
            // btYoloTrain
            // 
            btYoloTrain.Location = new Point(111, 22);
            btYoloTrain.Name = "btYoloTrain";
            btYoloTrain.Size = new Size(99, 30);
            btYoloTrain.TabIndex = 0;
            btYoloTrain.Text = "yolo-train-val";
            btYoloTrain.UseVisualStyleBackColor = true;
            btYoloTrain.Click += btYoloTrain_Click;
            // 
            // btCropeImage
            // 
            btCropeImage.Location = new Point(6, 100);
            btCropeImage.Name = "btCropeImage";
            btCropeImage.Size = new Size(99, 30);
            btCropeImage.TabIndex = 0;
            btCropeImage.Text = "crope image";
            btCropeImage.UseVisualStyleBackColor = true;
            btCropeImage.Click += btCropeImage_Click;
            // 
            // btYoloLabels
            // 
            btYoloLabels.Location = new Point(6, 22);
            btYoloLabels.Name = "btYoloLabels";
            btYoloLabels.Size = new Size(99, 30);
            btYoloLabels.TabIndex = 0;
            btYoloLabels.Text = "yolo-labels";
            btYoloLabels.UseVisualStyleBackColor = true;
            btYoloLabels.Click += btLabels_Click;
            // 
            // cbShowPictureBoxDebug
            // 
            cbShowPictureBoxDebug.AutoSize = true;
            cbShowPictureBoxDebug.Location = new Point(311, 418);
            cbShowPictureBoxDebug.Name = "cbShowPictureBoxDebug";
            cbShowPictureBoxDebug.Size = new Size(83, 19);
            cbShowPictureBoxDebug.TabIndex = 2;
            cbShowPictureBoxDebug.Text = "Show View";
            cbShowPictureBoxDebug.UseVisualStyleBackColor = true;
            cbShowPictureBoxDebug.CheckedChanged += cbShowView_CheckedChanged;
            // 
            // cbAutoFocus
            // 
            cbAutoFocus.AutoSize = true;
            cbAutoFocus.Location = new Point(610, 171);
            cbAutoFocus.Name = "cbAutoFocus";
            cbAutoFocus.Size = new Size(162, 19);
            cbAutoFocus.TabIndex = 2;
            cbAutoFocus.Text = "(CAPSLOCK) - Auto focus";
            cbAutoFocus.UseVisualStyleBackColor = true;
            cbAutoFocus.CheckedChanged += cbAutoFocus_CheckedChanged;
            // 
            // cbIsCircle
            // 
            cbIsCircle.AutoSize = true;
            cbIsCircle.Location = new Point(311, 443);
            cbIsCircle.Name = "cbIsCircle";
            cbIsCircle.Size = new Size(67, 19);
            cbIsCircle.TabIndex = 2;
            cbIsCircle.Text = "is Circle";
            cbIsCircle.UseVisualStyleBackColor = true;
            cbIsCircle.CheckedChanged += cbIsCircle_CheckedChanged;
            // 
            // cbPrecision
            // 
            cbPrecision.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPrecision.FormattingEnabled = true;
            cbPrecision.Items.AddRange(new object[] { "INT8", "FP16", "FP32" });
            cbPrecision.Location = new Point(564, 247);
            cbPrecision.Name = "cbPrecision";
            cbPrecision.Size = new Size(86, 23);
            cbPrecision.TabIndex = 6;
            // 
            // fMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 465);
            Controls.Add(groupBox1);
            Controls.Add(btApplyModelType);
            Controls.Add(cbPrecision);
            Controls.Add(cbModelName);
            Controls.Add(cbExecutionProvider);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(nBitmapH);
            Controls.Add(nBitmapW);
            Controls.Add(lbFPS);
            Controls.Add(cbEnableDetection);
            Controls.Add(cbAutoFocus);
            Controls.Add(cbEnableGun2);
            Controls.Add(cbEnableAimPlayer);
            Controls.Add(cbEnableGun1);
            Controls.Add(cbPredictMove);
            Controls.Add(cbIsCircle);
            Controls.Add(cbShowPictureBoxDebug);
            Controls.Add(cbDrawDetection);
            Controls.Add(pictureBox);
            Controls.Add(btStart);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "fMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YoloDotnet";
            FormClosing += fMain_FormClosing;
            Load += fMain_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapW).EndInit();
            ((System.ComponentModel.ISupportInitialize)nBitmapH).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btStart;
        private PictureBox pictureBox;
        private CheckBox cbDrawDetection;
        private Label lbFPS;
        private CheckBox cbEnableDetection;
        private CheckBox cbEnableGun1;
        private CheckBox cbEnableGun2;
        private NumericUpDown nBitmapW;
        private NumericUpDown nBitmapH;
        private Label label1;
        private Label label2;
        private CheckBox cbEnableAimPlayer;
        private ComboBox cbExecutionProvider;
        private ComboBox cbModelName;
        private Button btApplyModelType;
        private CheckBox cbPredictMove;
        private GroupBox groupBox1;
        private Button btYoloLabels;
        private Button btYoloTrain;
        private Button btScreenshotVideo;
        private Button btStopDetectionVideo;
        private Label lbProcessBar;
        private Button btCropeImage;
        private CheckBox cbShowPictureBoxDebug;
        private CheckBox cbAutoFocus;
        private CheckBox cbIsCircle;
        private ComboBox cbPrecision;
    }
}
