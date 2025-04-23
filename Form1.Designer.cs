// Đảm bảo namespace khớp với dự án của bạn
namespace chuanstt
{
    // Đảm bảo đây là partial class Form1
    partial class Form1
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
            // Khởi tạo các controls và components
            this.buttonChonInput = new System.Windows.Forms.Button();
            this.textBoxInputPath = new System.Windows.Forms.TextBox();
            this.buttonChonOutput = new System.Windows.Forms.Button();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.labelInput = new System.Windows.Forms.Label();
            this.labelOutput = new System.Windows.Forms.Label();
            this.labelInputFormat = new System.Windows.Forms.Label();
            this.textBoxInputFormat = new System.Windows.Forms.TextBox();
            this.labelOutputFormat = new System.Windows.Forms.Label();
            this.textBoxOutputFormat = new System.Windows.Forms.TextBox();
            this.buttonXuLy = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();

            //
            // labelInput
            //
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(12, 15);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(73, 15); // Kích thước tự động dựa trên Text
            this.labelInput.TabIndex = 0;
            this.labelInput.Text = "File nguồn:";
            //
            // textBoxInputPath
            //
            this.textBoxInputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInputPath.Location = new System.Drawing.Point(15, 33);
            this.textBoxInputPath.Name = "textBoxInputPath";
            this.textBoxInputPath.ReadOnly = true;
            this.textBoxInputPath.Size = new System.Drawing.Size(376, 23);
            this.textBoxInputPath.TabIndex = 1;
            //
            // buttonChonInput
            //
            this.buttonChonInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChonInput.Location = new System.Drawing.Point(397, 32);
            this.buttonChonInput.Name = "buttonChonInput";
            this.buttonChonInput.Size = new System.Drawing.Size(75, 25);
            this.buttonChonInput.TabIndex = 2;
            this.buttonChonInput.Text = "Chọn...";
            this.buttonChonInput.UseVisualStyleBackColor = true;
            this.buttonChonInput.Click += new System.EventHandler(this.buttonChonInput_Click); // Liên kết sự kiện
            //
            // labelOutput
            //
            this.labelOutput.AutoSize = true;
            this.labelOutput.Location = new System.Drawing.Point(12, 65); // Cách dòng trên 1 chút
            this.labelOutput.Name = "labelOutput";
            this.labelOutput.Size = new System.Drawing.Size(70, 15);
            this.labelOutput.TabIndex = 3;
            this.labelOutput.Text = "File kết quả:";
            //
            // textBoxOutputPath
            //
            this.textBoxOutputPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputPath.Location = new System.Drawing.Point(15, 83);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.ReadOnly = true;
            this.textBoxOutputPath.Size = new System.Drawing.Size(376, 23);
            this.textBoxOutputPath.TabIndex = 4;
            //
            // buttonChonOutput
            //
            this.buttonChonOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonChonOutput.Location = new System.Drawing.Point(397, 82);
            this.buttonChonOutput.Name = "buttonChonOutput";
            this.buttonChonOutput.Size = new System.Drawing.Size(75, 25);
            this.buttonChonOutput.TabIndex = 5;
            this.buttonChonOutput.Text = "Lưu vào...";
            this.buttonChonOutput.UseVisualStyleBackColor = true;
            this.buttonChonOutput.Click += new System.EventHandler(this.buttonChonOutput_Click); // Liên kết sự kiện
            //
            // labelInputFormat
            //
            this.labelInputFormat.AutoSize = true;
            this.labelInputFormat.Location = new System.Drawing.Point(12, 119); // Cách dòng trên
            this.labelInputFormat.Name = "labelInputFormat";
            this.labelInputFormat.Size = new System.Drawing.Size(190, 15); // Kích thước tự động
            this.labelInputFormat.TabIndex = 6;
            this.labelInputFormat.Text = "Mẫu chương đầu dòng (* thay số):";
            //
            // textBoxInputFormat
            //
            this.textBoxInputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right))); // Có thể cho rộng ra
            this.textBoxInputFormat.Location = new System.Drawing.Point(15, 137);
            this.textBoxInputFormat.Name = "textBoxInputFormat";
            this.textBoxInputFormat.Size = new System.Drawing.Size(457, 23); // Rộng hơn
            this.textBoxInputFormat.TabIndex = 7;
            this.textBoxInputFormat.PlaceholderText = "Ví dụ: Chương *:";
            //
            // labelOutputFormat
            //
            this.labelOutputFormat.AutoSize = true;
            this.labelOutputFormat.Location = new System.Drawing.Point(12, 168); // Cách dòng trên
            this.labelOutputFormat.Name = "labelOutputFormat";
            this.labelOutputFormat.Size = new System.Drawing.Size(151, 15); // Kích thước tự động
            this.labelOutputFormat.TabIndex = 8;
            this.labelOutputFormat.Text = "Mẫu kết quả (* thay số):";
            //
            // textBoxOutputFormat
            //
            this.textBoxOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFormat.Location = new System.Drawing.Point(15, 186);
            this.textBoxOutputFormat.Name = "textBoxOutputFormat";
            this.textBoxOutputFormat.Size = new System.Drawing.Size(457, 23); // Rộng hơn
            this.textBoxOutputFormat.TabIndex = 9;
            this.textBoxOutputFormat.Text = "第*章"; // Giá trị mặc định
            //
            // buttonXuLy
            //
            this.buttonXuLy.Anchor = System.Windows.Forms.AnchorStyles.Top; // Căn giữa
            this.buttonXuLy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point); // Font to hơn chút
            this.buttonXuLy.Location = new System.Drawing.Point(188, 225); // Dịch xuống chút
            this.buttonXuLy.Name = "buttonXuLy";
            this.buttonXuLy.Size = new System.Drawing.Size(109, 34);
            this.buttonXuLy.TabIndex = 10;
            this.buttonXuLy.Text = "Xử lý File";
            this.buttonXuLy.UseVisualStyleBackColor = true;
            this.buttonXuLy.Click += new System.EventHandler(this.buttonXuLy_Click); // Liên kết sự kiện
            //
            // openFileDialog1
            //
            this.openFileDialog1.FileName = "openFileDialog1";
            // (Filter, Title... được đặt trong Form1.cs)
            //
            // saveFileDialog1
            //
            // (Filter, Title... được đặt trong Form1.cs)
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 275); // Tăng chiều cao cửa sổ
            // Thêm controls vào Form theo thứ tự TabIndex hợp lý (ví dụ)
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.textBoxInputPath);
            this.Controls.Add(this.buttonChonInput);
            this.Controls.Add(this.labelOutput);
            this.Controls.Add(this.textBoxOutputPath);
            this.Controls.Add(this.buttonChonOutput);
            this.Controls.Add(this.labelInputFormat);
            this.Controls.Add(this.textBoxInputFormat);
            this.Controls.Add(this.labelOutputFormat);
            this.Controls.Add(this.textBoxOutputFormat);
            this.Controls.Add(this.buttonXuLy);
            this.MinimumSize = new System.Drawing.Size(400, 314); // Đặt kích thước tối thiểu
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công cụ Chuẩn hóa Tên Chương";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Khai báo biến cho Controls và Components (phải khớp tên ở trên)
        private Button buttonChonInput;
        private TextBox textBoxInputPath;
        private Button buttonChonOutput;
        private TextBox textBoxOutputPath;
        private Label labelInput;
        private Label labelOutput;
        private Label labelInputFormat; // Đổi tên
        private TextBox textBoxInputFormat; // Đổi tên
        private Label labelOutputFormat; // Mới
        private TextBox textBoxOutputFormat; // Mới
        private Button buttonXuLy;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
    }
}