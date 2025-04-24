using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace chuonglap
{
    public partial class Form1 : Form
    {
        private string selectedInputFilePath = "";
        private string selectedOutputFilePath = "";
        private Dictionary<string, string> encodingOptions = new Dictionary<string, string>();

        private readonly Regex parenthesesRegex = new Regex(@"\s*[\(（].*?[\)）]");
        private readonly Regex datePrefixRegex = new Regex(@"^\d{4}-\d{2}-\d{2}\s+");
        private const string JunkFileName = "rác cần lọc.txt";

        public Form1()
        {
            InitializeComponent();

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                encodingOptions.Add("UTF-8 (Mặc định)", "utf-8");
                encodingOptions.Add("GB2312 (Tiếng Trung Giản thể)", "gb2312");
                encodingOptions.Add("TCVN3 (ABC)", "tcvn-3");
                encodingOptions.Add("VNI Windows", "vni-windows");
                encodingOptions.Add("Windows-1258 (Vietnam)", "windows-1258");
                try
                {
                    if (!encodingOptions.ContainsValue(Encoding.Default.WebName))
                        encodingOptions.Add($"ANSI ({Encoding.Default.EncodingName})", Encoding.Default.WebName);
                }
                catch { }

                if (this.cmbEncoding != null)
                {
                    this.cmbEncoding.Items.Clear();
                    foreach (string displayName in encodingOptions.Keys) { this.cmbEncoding.Items.Add(displayName); }
                    if (this.cmbEncoding.Items.Contains("UTF-8 (Mặc định)")) { this.cmbEncoding.SelectedItem = "UTF-8 (Mặc định)"; }
                    else if (this.cmbEncoding.Items.Count > 0) { this.cmbEncoding.SelectedIndex = 0; }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi mã hóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try { this.Icon = new System.Drawing.Icon("TTVicon.ico"); } catch { }

            UpdateStatus("Sẵn sàng.", Color.Black);
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                ofd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 1; ofd.RestoreDirectory = true; ofd.Title = "Chọn file input";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedInputFilePath = ofd.FileName;
                    if (this.txtInputFile != null) { this.txtInputFile.Text = selectedInputFilePath; }
                    try { selectedOutputFilePath = ""; if (txtOutputFile != null) txtOutputFile.Text = ""; } catch { }
                    UpdateStatus("Đã chọn input. Hãy chọn output.", Color.Blue);
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                string sName = "output_processed.txt"; string iDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!string.IsNullOrEmpty(selectedInputFilePath)) { try { iDir = Path.GetDirectoryName(selectedInputFilePath) ?? iDir; sName = Path.GetFileNameWithoutExtension(selectedInputFilePath) + "_processed.txt"; } catch { } }
                sfd.InitialDirectory = iDir; sfd.FileName = sName; sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FilterIndex = 1; sfd.RestoreDirectory = true; sfd.Title = "Chọn nơi lưu kết quả";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    selectedOutputFilePath = sfd.FileName;
                    if (this.txtOutputFile != null) { this.txtOutputFile.Text = selectedOutputFilePath; }
                    UpdateStatus("Đã chọn input và output. Sẵn sàng.", Color.Blue);
                }
            }
        }

        private async void btnProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedInputFilePath) || !File.Exists(selectedInputFilePath)) { MessageBox.Show("Chọn file input hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(selectedOutputFilePath)) { MessageBox.Show("Chọn nơi lưu file output.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try { if (Path.GetFullPath(selectedInputFilePath).Equals(Path.GetFullPath(selectedOutputFilePath), StringComparison.OrdinalIgnoreCase)) { MessageBox.Show("Input và output trùng nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } }
            catch (Exception ex) { MessageBox.Show($"Lỗi kiểm tra đường dẫn:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (tabControlMain == null || tabControlMain.SelectedTab == null) { MessageBox.Show("Lỗi giao diện: Không xác định được tab.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Encoding inputEncoding = Encoding.UTF8;
            if (this.cmbEncoding != null && this.cmbEncoding.SelectedItem != null)
            {
                string selectedDisplayName = this.cmbEncoding.SelectedItem.ToString() ?? "";
                if (encodingOptions.ContainsKey(selectedDisplayName))
                {
                    string encodingName = encodingOptions[selectedDisplayName];
                    try { inputEncoding = Encoding.GetEncoding(encodingName); } catch { }
                }
            }

            SetProcessingState(true);
            string junkFilePath = Path.Combine(Application.StartupPath, "res", JunkFileName);

            try
            {
                UpdateStatus($"Đang xử lý (Encoding Input: {inputEncoding.EncodingName})...", Color.Orange);

                if (tabControlMain.SelectedTab == tabPageRemoveDuplicates)
                {
                    int written = 0; List<string> lines = new List<string>(); string? prevCore = null;
                    using (var reader = new StreamReader(selectedInputFilePath, inputEncoding))
                    {
                        string? ln; while ((ln = await reader.ReadLineAsync()) != null) { string trLn = ln.Trim(); if (string.IsNullOrEmpty(trLn)) { lines.Add(ln); continue; } string core = trLn; core = datePrefixRegex.Replace(core, ""); core = parenthesesRegex.Replace(core, ""); core = core.Trim(); if (!string.IsNullOrEmpty(core)) { if (core != prevCore) { lines.Add(ln); prevCore = core; } } else { lines.Add(ln); } }
                    }
                    await File.WriteAllLinesAsync(selectedOutputFilePath, lines, Encoding.UTF8); written = lines.Count;
                    UpdateStatus($"Hoàn thành! Ghi {written} dòng (UTF-8).", Color.Green); MessageBox.Show($"Đã loại bỏ lặp:\n{selectedOutputFilePath}", "Xong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (tabControlMain.SelectedTab == tabPageStandardize)
                {
                    string iFormat = txtInputFormat?.Text ?? ""; string oFormat = "第*章";
                    if (string.IsNullOrWhiteSpace(iFormat) || !iFormat.Contains('*')) { UpdateStatus("Lỗi: Mẫu chương input không hợp lệ.", Color.Red); MessageBox.Show("Nhập mẫu chương input hợp lệ (có dấu *).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInputFormat?.Focus(); SetProcessingState(false); return; }
                    List<string> oLines = new List<string>(); int linesRead = 0; int stdCount = 0;
                    try { using (var reader = new StreamReader(selectedInputFilePath, inputEncoding)) { string? ln; while ((ln = await reader.ReadLineAsync()) != null) { oLines.Add(ln); } linesRead = oLines.Count; } }
                    catch (DecoderFallbackException decEx) { throw new DecoderFallbackException($"Không thể đọc file với mã hóa '{inputEncoding.EncodingName}'.", decEx.BytesUnknown, decEx.Index); }
                    catch (IOException exRead) { throw new IOException($"Lỗi đọc input: {exRead.Message}", exRead); }
                    UpdateStatus($"Đọc {linesRead} dòng. Chuẩn hóa...", Color.Orange); List<string> finalLines = new List<string>(linesRead);
                    foreach (string line in oLines) { string? pLine = ChapterUtils.DoiTenChuong(line, iFormat, oFormat); finalLines.Add(pLine ?? line); if (pLine != null) { stdCount++; } }
                    await File.WriteAllLinesAsync(selectedOutputFilePath, finalLines, inputEncoding);
                    UpdateStatus($"Hoàn thành! Chuẩn hóa {stdCount} dòng.", Color.Green); MessageBox.Show($"Đã chuẩn hóa:\n{selectedOutputFilePath}\n(Encoding: {inputEncoding.WebName})", "Xong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (tabControlMain.SelectedTab == tabPageFilterJunk)
                {
                    UpdateStatus("Đang lọc cụm từ rác...", Color.Orange);
                    List<string> junkPhrases = new List<string>();
                    if (File.Exists(junkFilePath)) { try { string[] rawJunk = await File.ReadAllLinesAsync(junkFilePath, Encoding.UTF8); foreach (string j in rawJunk) { if (!string.IsNullOrWhiteSpace(j)) junkPhrases.Add(j.Trim()); } UpdateStatus($"Đã nạp {junkPhrases.Count} cụm từ rác từ file. Đọc input...", Color.Orange); } catch (Exception exJunk) { UpdateStatus($"Lỗi đọc file rác: {exJunk.Message}", Color.Red); MessageBox.Show($"Lỗi đọc file {JunkFileName}: {exJunk.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); SetProcessingState(false); return; } }
                    else { UpdateStatus($"Cảnh báo: Không thấy file {junkFilePath}. Sẽ không lọc.", Color.Orange); }
                    if (junkPhrases.Count == 0) { UpdateStatus("Danh sách lọc rác trống hoặc file không tồn tại. Bỏ qua lọc.", Color.Orange); try { File.Copy(selectedInputFilePath, selectedOutputFilePath, true); MessageBox.Show($"Danh sách lọc rác trống/không tìm thấy. Đã sao chép file gốc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); } catch (Exception copyEx) { throw new IOException($"Lỗi copy file: {copyEx.Message}", copyEx); } SetProcessingState(false); return; }
                    List<string> inputLines = new List<string>(); int linesRead = 0;
                    try { using (var reader = new StreamReader(selectedInputFilePath, inputEncoding)) { string? ln; while ((ln = await reader.ReadLineAsync()) != null) { inputLines.Add(ln); } linesRead = inputLines.Count; } }
                    catch (DecoderFallbackException decEx) { throw new DecoderFallbackException($"Không thể đọc file với mã hóa '{inputEncoding.EncodingName}'.", decEx.BytesUnknown, decEx.Index); }
                    catch (IOException exRead) { throw new IOException($"Lỗi đọc input: {exRead.Message}", exRead); }
                    UpdateStatus($"Đọc {linesRead} dòng. Đang lọc...", Color.Orange); List<string> outputLines = new List<string>(); int totalReplacements = 0;
                    foreach (string line in inputLines) { string modifiedLine = line; foreach (string junkPhrase in junkPhrases) { if (!string.IsNullOrEmpty(junkPhrase) && modifiedLine.Contains(junkPhrase)) { int currentLength = modifiedLine.Length; string lineAfterReplace = modifiedLine.Replace(junkPhrase, string.Empty); int occurrences = (currentLength - lineAfterReplace.Length) / junkPhrase.Length; totalReplacements += occurrences; modifiedLine = lineAfterReplace; } } outputLines.Add(modifiedLine); }
                    await File.WriteAllLinesAsync(selectedOutputFilePath, outputLines, inputEncoding);
                    UpdateStatus($"Hoàn thành! Đã lọc và xóa {totalReplacements} cụm từ rác.", Color.Green); MessageBox.Show($"Đã lọc các cụm từ rác và lưu vào:\n{selectedOutputFilePath}\n(Đã xóa {totalReplacements} cụm từ)", "Xong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (IOException ioEx) { UpdateStatus($"Lỗi I/O: {ioEx.Message}", Color.Red); MessageBox.Show($"Lỗi đọc/ghi:\n{ioEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (UnauthorizedAccessException authEx) { UpdateStatus($"Lỗi quyền: {authEx.Message}", Color.Red); MessageBox.Show($"Không có quyền:\n{authEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (DecoderFallbackException decEx) { UpdateStatus($"Lỗi Mã Hóa Input: {decEx.Message}", Color.Red); MessageBox.Show($"Lỗi Mã Hóa khi đọc file input với encoding '{inputEncoding.EncodingName}'.\nVui lòng kiểm tra lại encoding.\nChi tiết: {decEx.Message}", "Lỗi Mã Hóa Input", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { UpdateStatus($"Lỗi: {ex.Message}", Color.Red); MessageBox.Show($"Lỗi:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally { SetProcessingState(false); }
        }

        private void UpdateStatus(string message, Color color)
        {
            if (this.lblStatus != null) { if (lblStatus.InvokeRequired) { lblStatus.BeginInvoke(new Action(() => { lblStatus.Text = message; lblStatus.ForeColor = color; })); } else { lblStatus.Text = message; lblStatus.ForeColor = color; } }
        }

        private void SetProcessingState(bool isProcessing)
        {
            bool enableState = !isProcessing;
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => SetProcessingState(isProcessing))); return; }
            if (btnBrowseInput != null) btnBrowseInput.Enabled = enableState;
            if (btnBrowseOutput != null) btnBrowseOutput.Enabled = enableState;
            if (btnProcess != null) btnProcess.Enabled = enableState;
            if (txtInputFile != null) txtInputFile.Enabled = enableState;
            if (txtOutputFile != null) txtOutputFile.Enabled = enableState;
            if (cmbEncoding != null) cmbEncoding.Enabled = enableState;
            if (label3 != null) label3.Enabled = enableState;
            if (tabControlMain != null) tabControlMain.Enabled = enableState;
            this.Cursor = isProcessing ? Cursors.WaitCursor : Cursors.Default;
        }


        private void tabControlMain_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Lấy TabPage và vùng vẽ header
            TabPage currentPage = tabControlMain.TabPages[e.Index];
            Rectangle itemRect = tabControlMain.GetTabRect(e.Index);
            Rectangle textRect = itemRect; // Vùng để vẽ chữ

            // Xác định trạng thái
            bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            // Xóa nền mặc định
            e.DrawBackground();

            // Màu sắc và Font chữ
            Color textColor;
            Font tabFont;

            if (isSelected)
            {
                // *** Vẽ nền Gradient cho tab được chọn ***
                // Màu xanh dương nhạt (bạn có thể thay đổi)
                Color lightBlue = Color.FromArgb(210, 230, 255); // Xanh dương rất nhạt ở trên
                Color midBlue = Color.FromArgb(170, 210, 255);   // Xanh dương nhạt hơn ở dưới
                using (LinearGradientBrush backBrush = new LinearGradientBrush(itemRect, lightBlue, midBlue, LinearGradientMode.Vertical))
                {
                    // Tô gradient lên toàn bộ vùng header
                    e.Graphics.FillRectangle(backBrush, itemRect);
                }

                // Màu chữ đen và Font chữ đậm khi được chọn
                textColor = Color.Black;
                tabFont = new Font(tabControlMain.Font, FontStyle.Bold);

                // (Tùy chọn) Vẽ đường kẻ dưới mỏng để tách biệt với nội dung tab
                using (Pen linePen = new Pen(midBlue, 1)) // Dùng màu đậm nhất của gradient
                {
                    e.Graphics.DrawLine(linePen, itemRect.Left, itemRect.Bottom - 1, itemRect.Right, itemRect.Bottom - 1);
                }
            }
            else
            {
                // Màu chữ và Font thường cho tab không được chọn
                textColor = SystemColors.ControlText;
                tabFont = tabControlMain.Font;
                // Nền của tab không được chọn sẽ được vẽ bởi e.DrawBackground() ở trên
            }

            // Vẽ chữ (Căn giữa)
            using (SolidBrush textBrush = new SolidBrush(textColor))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter
                };
                // Điều chỉnh vùng vẽ chữ để không sát viền
                textRect.Inflate(-2, -2);
                e.Graphics.DrawString(currentPage.Text, tabFont, textBrush, textRect, sf);
            }

            // Giải phóng tài nguyên Font nếu vừa tạo mới
            if (isSelected)
            {
                tabFont.Dispose();
            }

            // (Tùy chọn) Vẽ khung focus
            // if (e.State == DrawItemState.Focus)
            // {
            //     ControlPaint.DrawFocusRectangle(e.Graphics, itemRect);
            // }
        }
    }
}