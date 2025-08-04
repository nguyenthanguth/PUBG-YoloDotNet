namespace auto_aim
{
    partial class fLabels
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
            pictureBox = new PictureBox();
            btBack = new Button();
            btNext = new Button();
            tbPathLabels = new TextBox();
            btReload = new Button();
            lbClasses = new Label();
            label1 = new Label();
            cbDrawLabels = new ComboBox();
            btClear = new Button();
            btSave = new Button();
            lbFileName = new Label();
            cbFillNoLabels = new CheckBox();
            btDetection = new Button();
            btApplyModelType = new Button();
            cbModelName = new ComboBox();
            cbExecutionProvider = new ComboBox();
            lbModel = new Label();
            btAutoLabels = new Button();
            lbCurrentIndex = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox.Location = new Point(12, 12);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(520, 520);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // btBack
            // 
            btBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btBack.Location = new Point(466, 539);
            btBack.Name = "btBack";
            btBack.Size = new Size(30, 25);
            btBack.TabIndex = 1;
            btBack.Text = "<";
            btBack.UseVisualStyleBackColor = true;
            btBack.Click += btBack_Click;
            // 
            // btNext
            // 
            btNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btNext.Location = new Point(502, 538);
            btNext.Name = "btNext";
            btNext.Size = new Size(30, 25);
            btNext.TabIndex = 1;
            btNext.Text = ">";
            btNext.UseVisualStyleBackColor = true;
            btNext.Click += btNext_Click;
            // 
            // tbPathLabels
            // 
            tbPathLabels.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbPathLabels.Location = new Point(12, 540);
            tbPathLabels.Name = "tbPathLabels";
            tbPathLabels.Size = new Size(354, 23);
            tbPathLabels.TabIndex = 2;
            // 
            // btReload
            // 
            btReload.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btReload.Location = new Point(372, 540);
            btReload.Name = "btReload";
            btReload.Size = new Size(88, 23);
            btReload.TabIndex = 3;
            btReload.Text = "Reload";
            btReload.UseVisualStyleBackColor = true;
            btReload.Click += btReload_Click;
            // 
            // lbClasses
            // 
            lbClasses.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbClasses.AutoSize = true;
            lbClasses.Location = new Point(538, 35);
            lbClasses.Name = "lbClasses";
            lbClasses.Size = new Size(43, 15);
            lbClasses.TabIndex = 4;
            lbClasses.Text = "classes";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(538, 12);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 5;
            label1.Text = "draw";
            // 
            // cbDrawLabels
            // 
            cbDrawLabels.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbDrawLabels.FormattingEnabled = true;
            cbDrawLabels.Location = new Point(577, 9);
            cbDrawLabels.Name = "cbDrawLabels";
            cbDrawLabels.Size = new Size(121, 23);
            cbDrawLabels.TabIndex = 6;
            // 
            // btClear
            // 
            btClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btClear.Location = new Point(538, 206);
            btClear.Name = "btClear";
            btClear.Size = new Size(86, 23);
            btClear.TabIndex = 7;
            btClear.Text = "Delete labels";
            btClear.UseVisualStyleBackColor = true;
            btClear.Click += btClear_Click;
            // 
            // btSave
            // 
            btSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btSave.Location = new Point(538, 409);
            btSave.Name = "btSave";
            btSave.Size = new Size(75, 23);
            btSave.TabIndex = 7;
            btSave.Text = "Save";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // lbFileName
            // 
            lbFileName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbFileName.AutoSize = true;
            lbFileName.Location = new Point(538, 188);
            lbFileName.Name = "lbFileName";
            lbFileName.Size = new Size(56, 15);
            lbFileName.TabIndex = 4;
            lbFileName.Text = "name file";
            // 
            // cbFillNoLabels
            // 
            cbFillNoLabels.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cbFillNoLabels.AutoSize = true;
            cbFillNoLabels.Location = new Point(538, 513);
            cbFillNoLabels.Name = "cbFillNoLabels";
            cbFillNoLabels.Size = new Size(125, 19);
            cbFillNoLabels.TabIndex = 8;
            cbFillNoLabels.Text = "fill image no labels";
            cbFillNoLabels.UseVisualStyleBackColor = true;
            cbFillNoLabels.CheckedChanged += cbFillNoLabels_CheckedChanged;
            // 
            // btDetection
            // 
            btDetection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btDetection.Location = new Point(538, 380);
            btDetection.Name = "btDetection";
            btDetection.Size = new Size(75, 23);
            btDetection.TabIndex = 7;
            btDetection.Text = "Detection";
            btDetection.UseVisualStyleBackColor = true;
            btDetection.Click += btDetection_Click;
            // 
            // btApplyModelType
            // 
            btApplyModelType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btApplyModelType.Location = new Point(538, 322);
            btApplyModelType.Name = "btApplyModelType";
            btApplyModelType.Size = new Size(75, 23);
            btApplyModelType.TabIndex = 11;
            btApplyModelType.Text = "Apply";
            btApplyModelType.UseVisualStyleBackColor = true;
            btApplyModelType.Click += btApplyModelType_Click;
            // 
            // cbModelName
            // 
            cbModelName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbModelName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModelName.FormattingEnabled = true;
            cbModelName.Location = new Point(538, 293);
            cbModelName.Name = "cbModelName";
            cbModelName.Size = new Size(121, 23);
            cbModelName.TabIndex = 9;
            cbModelName.SelectedIndexChanged += cbModelName_SelectedIndexChanged;
            // 
            // cbExecutionProvider
            // 
            cbExecutionProvider.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbExecutionProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            cbExecutionProvider.FormattingEnabled = true;
            cbExecutionProvider.Items.AddRange(new object[] { "CudaExecutionProvider", "TensorRtExecutionProvider" });
            cbExecutionProvider.Location = new Point(538, 264);
            cbExecutionProvider.Name = "cbExecutionProvider";
            cbExecutionProvider.Size = new Size(113, 23);
            cbExecutionProvider.TabIndex = 10;
            cbExecutionProvider.SelectedIndexChanged += cbExecutionProvider_SelectedIndexChanged;
            // 
            // lbModel
            // 
            lbModel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lbModel.AutoSize = true;
            lbModel.Location = new Point(619, 326);
            lbModel.Name = "lbModel";
            lbModel.Size = new Size(41, 15);
            lbModel.TabIndex = 4;
            lbModel.Text = "model";
            // 
            // btAutoLabels
            // 
            btAutoLabels.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAutoLabels.Location = new Point(538, 484);
            btAutoLabels.Name = "btAutoLabels";
            btAutoLabels.Size = new Size(121, 23);
            btAutoLabels.TabIndex = 7;
            btAutoLabels.Text = "Auto labels";
            btAutoLabels.UseVisualStyleBackColor = true;
            btAutoLabels.Click += btAutoLabels_Click;
            // 
            // lbCurrentIndex
            // 
            lbCurrentIndex.AutoSize = true;
            lbCurrentIndex.Location = new Point(538, 545);
            lbCurrentIndex.Name = "lbCurrentIndex";
            lbCurrentIndex.Size = new Size(13, 15);
            lbCurrentIndex.TabIndex = 4;
            lbCurrentIndex.Text = "0";
            // 
            // fLabels
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(740, 575);
            Controls.Add(btApplyModelType);
            Controls.Add(cbModelName);
            Controls.Add(cbExecutionProvider);
            Controls.Add(cbFillNoLabels);
            Controls.Add(btDetection);
            Controls.Add(btAutoLabels);
            Controls.Add(btSave);
            Controls.Add(btClear);
            Controls.Add(cbDrawLabels);
            Controls.Add(label1);
            Controls.Add(lbCurrentIndex);
            Controls.Add(lbModel);
            Controls.Add(lbFileName);
            Controls.Add(lbClasses);
            Controls.Add(btReload);
            Controls.Add(tbPathLabels);
            Controls.Add(btNext);
            Controls.Add(btBack);
            Controls.Add(pictureBox);
            Name = "fLabels";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "View labels";
            FormClosing += fLabels_FormClosing;
            Load += fLabels_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private Button btBack;
        private Button btNext;
        private TextBox tbPathLabels;
        private Button btReload;
        private Label lbClasses;
        private Label label1;
        private ComboBox cbDrawLabels;
        private Button btClear;
        private Button btSave;
        private Label lbFileName;
        private CheckBox cbFillNoLabels;
        private Button btDetection;
        private Button btApplyModelType;
        private ComboBox cbModelName;
        private ComboBox cbExecutionProvider;
        private Label lbModel;
        private Button btAutoLabels;
        private Label lbCurrentIndex;
    }
}