namespace chuonglap
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnBrowseInput = new System.Windows.Forms.Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageRemoveDuplicates = new System.Windows.Forms.TabPage();
            this.tabPageStandardize = new System.Windows.Forms.TabPage();
            this.labelInputFormat = new System.Windows.Forms.Label();
            this.txtInputFormat = new System.Windows.Forms.TextBox();
            this.tabPageFilterJunk = new System.Windows.Forms.TabPage();
            this.labelFilterJunkInfo = new System.Windows.Forms.Label();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageStandardize.SuspendLayout();
            this.tabPageFilterJunk.SuspendLayout();
            this.SuspendLayout();

            this.openFileDialog1.FileName = "openFileDialog1";

            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Input:";

            this.txtInputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputFile.Location = new System.Drawing.Point(15, 33);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.ReadOnly = true;
            this.txtInputFile.Size = new System.Drawing.Size(370, 23);
            this.txtInputFile.TabIndex = 1;

            this.btnBrowseInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseInput.Location = new System.Drawing.Point(391, 32);
            this.btnBrowseInput.Name = "btnBrowseInput";
            this.btnBrowseInput.Size = new System.Drawing.Size(81, 25);
            this.btnBrowseInput.TabIndex = 2;
            this.btnBrowseInput.Text = "Chọn File...";
            this.btnBrowseInput.UseVisualStyleBackColor = true;
            this.btnBrowseInput.Click += new System.EventHandler(this.btnBrowseInput_Click);

            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "File Output:";

            this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFile.Location = new System.Drawing.Point(15, 83);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.ReadOnly = true;
            this.txtOutputFile.Size = new System.Drawing.Size(370, 23);
            this.txtOutputFile.TabIndex = 4;

            this.btnBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseOutput.Location = new System.Drawing.Point(391, 82);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(81, 25);
            this.btnBrowseOutput.TabIndex = 5;
            this.btnBrowseOutput.Text = "Lưu vào...";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);

            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mã hóa File Input:";

            this.cmbEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Location = new System.Drawing.Point(121, 115);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(351, 23);
            this.cmbEncoding.TabIndex = 7;

            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageRemoveDuplicates);
            this.tabControlMain.Controls.Add(this.tabPageStandardize);
            this.tabControlMain.Controls.Add(this.tabPageFilterJunk);
            this.tabControlMain.Location = new System.Drawing.Point(15, 150);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(457, 120);
            this.tabControlMain.TabIndex = 8;
            this.tabControlMain.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed; // *** THÊM DÒNG NÀY ***
            this.tabControlMain.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControlMain_DrawItem); // *** THÊM DÒNG NÀY ***

            this.tabPageRemoveDuplicates.Location = new System.Drawing.Point(4, 24);
            this.tabPageRemoveDuplicates.Name = "tabPageRemoveDuplicates";
            this.tabPageRemoveDuplicates.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRemoveDuplicates.Size = new System.Drawing.Size(449, 92);
            this.tabPageRemoveDuplicates.TabIndex = 0;
            this.tabPageRemoveDuplicates.Text = "Loại bỏ lặp";
            this.tabPageRemoveDuplicates.UseVisualStyleBackColor = true;

            this.tabPageStandardize.Controls.Add(this.labelInputFormat);
            this.tabPageStandardize.Controls.Add(this.txtInputFormat);
            this.tabPageStandardize.Location = new System.Drawing.Point(4, 24);
            this.tabPageStandardize.Name = "tabPageStandardize";
            this.tabPageStandardize.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStandardize.Size = new System.Drawing.Size(449, 92);
            this.tabPageStandardize.TabIndex = 1;
            this.tabPageStandardize.Text = "Chuẩn hóa chương";
            this.tabPageStandardize.UseVisualStyleBackColor = true;

            this.labelInputFormat.AutoSize = true;
            this.labelInputFormat.Location = new System.Drawing.Point(12, 17);
            this.labelInputFormat.Name = "labelInputFormat";
            this.labelInputFormat.Size = new System.Drawing.Size(169, 15);
            this.labelInputFormat.TabIndex = 0;
            this.labelInputFormat.Text = "Mẫu chương đầu dòng (* số):";

            this.txtInputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputFormat.Location = new System.Drawing.Point(15, 35);
            this.txtInputFormat.Name = "txtInputFormat";
            this.txtInputFormat.PlaceholderText = "Ví dụ: Chương *:";
            this.txtInputFormat.Size = new System.Drawing.Size(418, 23);
            this.txtInputFormat.TabIndex = 1;

            this.tabPageFilterJunk.Controls.Add(this.labelFilterJunkInfo);
            this.tabPageFilterJunk.Location = new System.Drawing.Point(4, 24);
            this.tabPageFilterJunk.Name = "tabPageFilterJunk";
            this.tabPageFilterJunk.Padding = new System.Windows.Forms.Padding(10);
            this.tabPageFilterJunk.Size = new System.Drawing.Size(449, 92);
            this.tabPageFilterJunk.TabIndex = 2;
            this.tabPageFilterJunk.Text = "Lọc rác";
            this.tabPageFilterJunk.UseVisualStyleBackColor = true;

            this.labelFilterJunkInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFilterJunkInfo.Location = new System.Drawing.Point(10, 10);
            this.labelFilterJunkInfo.Name = "labelFilterJunkInfo";
            this.labelFilterJunkInfo.Size = new System.Drawing.Size(429, 72);
            this.labelFilterJunkInfo.TabIndex = 0;
            this.labelFilterJunkInfo.Text = "Chức năng này sẽ lọc bỏ các cụm từ được liệt kê trong file \"res/rác cần lọc.txt\"" +
    ".";
            this.labelFilterJunkInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.btnProcess.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnProcess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnProcess.Location = new System.Drawing.Point(190, 285);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(105, 35);
            this.btnProcess.TabIndex = 9;
            this.btnProcess.Text = "Xử lý File";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);

            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStatus.Location = new System.Drawing.Point(12, 335);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(460, 23);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Sẵn sàng";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 371);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.cmbEncoding);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.btnBrowseOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.btnBrowseInput);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(500, 410);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công cụ Xử lý Text";
            this.tabControlMain.ResumeLayout(false);
            this.tabPageStandardize.ResumeLayout(false);
            this.tabPageStandardize.PerformLayout();
            this.tabPageFilterJunk.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private Button btnBrowseInput;
        private TextBox txtInputFile;
        private Button btnBrowseOutput;
        private TextBox txtOutputFile;
        private Button btnProcess;
        private Label lblStatus;
        private Label label1;
        private Label label2;
        private TabControl tabControlMain;
        private TabPage tabPageRemoveDuplicates;
        private TabPage tabPageStandardize;
        private Label labelInputFormat;
        private TextBox txtInputFormat;
        private TabPage tabPageFilterJunk;
        private Label labelFilterJunkInfo;
        private ComboBox cmbEncoding;
        private Label label3;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}