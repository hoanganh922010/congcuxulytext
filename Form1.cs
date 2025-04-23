using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms; // Đảm bảo có using này

// Đảm bảo namespace khớp
namespace chuanstt
{
    public partial class Form1 : Form
    {
        private string? inputFilePath = null;
        private string? outputFilePath = null;

        public Form1()
        {
            InitializeComponent(); // Quan trọng: Gọi đầu tiên
            ConfigureDialogs();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Đăng ký encoding provider
        }

        private void ConfigureDialogs()
        {
            if (openFileDialog1 != null)
            {
                openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.Title = "Chọn file văn bản nguồn (.txt)";
                openFileDialog1.CheckFileExists = true;
                openFileDialog1.CheckPathExists = true;
            }
            if (saveFileDialog1 != null)
            {
                saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog1.Title = "Chọn nơi lưu file kết quả (.txt)";
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.FileName = "ket_qua.txt";
            }
        }

        private void buttonChonInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog1?.ShowDialog() == DialogResult.OK)
            {
                inputFilePath = openFileDialog1.FileName;
                if (textBoxInputPath != null) textBoxInputPath.Text = inputFilePath; // Cần textBoxInputPath trên Designer
                try
                {
                    string? directory = Path.GetDirectoryName(inputFilePath);
                    string inputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                    if (directory != null && saveFileDialog1 != null) { saveFileDialog1.InitialDirectory = directory; }
                    if (saveFileDialog1 != null) saveFileDialog1.FileName = $"{inputFileName}_processed.txt";
                    outputFilePath = null;
                    if (textBoxOutputPath != null) textBoxOutputPath.Text = ""; // Cần textBoxOutputPath trên Designer
                }
                catch { /* Ignore */ }
            }
        }

        private void buttonChonOutput_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1?.ShowDialog() == DialogResult.OK)
            {
                outputFilePath = saveFileDialog1.FileName;
                if (textBoxOutputPath != null) textBoxOutputPath.Text = outputFilePath; // Cần textBoxOutputPath trên Designer
            }
        }

        private void buttonXuLy_Click(object sender, EventArgs e)
        {
            // Kiểm tra file
            if (string.IsNullOrEmpty(inputFilePath) || !File.Exists(inputFilePath))
            {
                // Sửa lỗi CS0103: Dùng đúng MessageBoxIcon.Warning
                MessageBox.Show("File nguồn không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonChonInput?.Focus(); // Cần buttonChonInput trên Designer
                return;
            }
            if (string.IsNullOrEmpty(outputFilePath))
            {
                // Sửa lỗi CS0103: Dùng đúng MessageBoxIcon.Warning
                MessageBox.Show("Chưa chọn nơi lưu file.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonChonOutput?.Focus(); // Cần buttonChonOutput trên Designer
                return;
            }
            if (Path.GetFullPath(inputFilePath).Equals(Path.GetFullPath(outputFilePath), StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("File nguồn và đích trùng nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đọc format - Cần textBoxInputFormat và textBoxOutputFormat trên Designer
            string inputFormat = textBoxInputFormat?.Text ?? "";
            string outputFormat = textBoxOutputFormat?.Text ?? "第*章";

            if (string.IsNullOrWhiteSpace(inputFormat) || !inputFormat.Contains('*'))
            {
                // Sửa lỗi CS0103: Dùng đúng MessageBoxIcon.Warning (Ví dụ)
                MessageBox.Show("Vui lòng nhập 'Mẫu chương đầu dòng' và phải chứa dấu '*' đại diện cho số chương.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxInputFormat?.Focus();
                return;
            }

            Encoding detectedEncoding = Encoding.GetEncoding(936);
            List<string> outputLines = new List<string>();
            int processedCount = 0;
            int lineCount = 0;

            try
            {
                if (buttonXuLy != null) { buttonXuLy.Text = "Đang xử lý..."; buttonXuLy.Enabled = false; } // Cần buttonXuLy
                this.Cursor = Cursors.WaitCursor;

                using (var reader = new StreamReader(inputFilePath, detectedEncoding, true))
                {
                    string tempLine;
                    List<string> inputLines = new List<string>();
                    while ((tempLine = reader.ReadLine()) != null) { inputLines.Add(tempLine); }
                    detectedEncoding = reader.CurrentEncoding;

                    foreach (string line in inputLines)
                    {
                        lineCount++;
                        string? processedLine = ChapterUtils.DoiTenChuong(line, inputFormat, outputFormat);
                        outputLines.Add(processedLine ?? line);
                        if (processedLine != null) { processedCount++; }
                    }
                }

                using (var writer = new StreamWriter(outputFilePath, false, detectedEncoding))
                {
                    foreach (string outputLine in outputLines) { writer.WriteLine(outputLine); }
                }

                MessageBox.Show($"Xử lý hoàn tất!\n\nEncoding gốc (phát hiện/mặc định): {detectedEncoding.WebName}\nĐã đọc: {lineCount} dòng.\nĐã thay đổi: {processedCount} dòng.\n\nKết quả lưu tại:\n{outputFilePath}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Lỗi: Không tìm thấy file nguồn:\n{inputFilePath}\n\nVui lòng chọn lại.", "Lỗi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inputFilePath = null;
                if (textBoxInputPath != null) textBoxInputPath.Text = ""; // Cần textBoxInputPath
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Lỗi đọc/ghi file: {ex.Message}\n\nĐường dẫn: {(ex.GetType().ToString().Contains("Write") ? outputFilePath : inputFilePath)}\n\nVui lòng kiểm tra lại.", "Lỗi IO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không mong muốn: {ex.Message}\n\n{ex.StackTrace}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error); // Sửa ở đây
            }

            finally
            {
                if (buttonXuLy != null) { buttonXuLy.Text = "Xử lý File"; buttonXuLy.Enabled = true; } // Cần buttonXuLy
                this.Cursor = Cursors.Default;
            }
        }

        // --- Các event handler khác ---
        // Bạn có thể xóa chúng nếu không dùng và không có lỗi liên quan trong Designer.cs
        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) { }
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e) { }
        private void textBoxInputPath_TextChanged(object sender, EventArgs e) { }
        // Thêm các event handler bạn đã tạo (ví dụ TextChanged cho các ô mới)
    }
}