using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
<<<<<<< HEAD
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace chuonglap
{
    public partial class Form1 : Form
    {
=======
using System.Text.RegularExpressions; // Quan trọng
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Text; // Quan trọng

namespace chuonglap // Thay thế bằng namespace của bạn
{
    public partial class Form1 : Form
    {
        // Biến thành viên
>>>>>>> 2198815 (commit message)
        private string selectedInputFilePath = "";
        private string selectedOutputFilePath = "";
        private Dictionary<string, string> encodingOptions = new Dictionary<string, string>();

<<<<<<< HEAD
        private readonly Regex parenthesesRegex = new Regex(@"\s*[\(（].*?[\)）]");
        private readonly Regex datePrefixRegex = new Regex(@"^\d{4}-\d{2}-\d{2}\s+");
=======
        // Regex để xử lý dòng (loại bỏ phần không cần thiết để lấy "core")
        private readonly Regex parenthesesRegex = new Regex(@"\s*[\(（].*?[\)）]");
        private readonly Regex datePrefixRegex = new Regex(@"^\d{4}-\d{2}-\d{2}\s+");

        // Tên file chứa mẫu rác
>>>>>>> 2198815 (commit message)
        private const string JunkFileName = "rác cần lọc.txt";

        public Form1()
        {
<<<<<<< HEAD
            InitializeComponent();

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
=======
            InitializeComponent(); // Gọi hàm từ Designer

            // Thiết lập ban đầu
            SetupEncodingOptions();
            SetupIcon();
            SetupControlBehaviors();

            UpdateStatus("Sẵn sàng.", Color.Black);
        }

        // Cài đặt các lựa chọn mã hóa cho ComboBox
        private void SetupEncodingOptions()
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                encodingOptions.Clear();
>>>>>>> 2198815 (commit message)
                encodingOptions.Add("UTF-8 (Mặc định)", "utf-8");
                encodingOptions.Add("GB2312 (Tiếng Trung Giản thể)", "gb2312");
                encodingOptions.Add("TCVN3 (ABC)", "tcvn-3");
                encodingOptions.Add("VNI Windows", "vni-windows");
                encodingOptions.Add("Windows-1258 (Vietnam)", "windows-1258");
<<<<<<< HEAD
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
=======
                try { if (!encodingOptions.ContainsValue(Encoding.Default.WebName)) encodingOptions.Add($"ANSI ({Encoding.Default.EncodingName})", Encoding.Default.WebName); } catch { }

                if (this.cmbEncoding != null)
                {
                    // Sử dụng BindingSource để dễ dàng quản lý Key-Value
                    this.cmbEncoding.DataSource = new BindingSource(encodingOptions, null);
                    this.cmbEncoding.DisplayMember = "Key"; // Hiển thị tên thân thiện
                    this.cmbEncoding.ValueMember = "Value";   // Giá trị thực sự là tên mã hóa
                    this.cmbEncoding.SelectedValue = "utf-8"; // Chọn UTF-8 làm mặc định
                    if (this.cmbEncoding.SelectedIndex < 0 && this.cmbEncoding.Items.Count > 0)
                    {
                        this.cmbEncoding.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi khởi tạo mã hóa: {ex.Message}", "Lỗi Mã Hóa", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // Cài đặt Icon cho Form
        private void SetupIcon()
        {
            try { this.Icon = new System.Drawing.Icon("TTVicon.ico"); }
            catch { UpdateStatus("Không tìm thấy file icon TTVicon.ico", Color.Orange); }
        }

        // Gán các sự kiện cần thiết cho control (giờ chỉ còn các nút)
        private void SetupControlBehaviors()
        {
            if (this.btnBrowseInput != null) this.btnBrowseInput.Click += btnBrowseInput_Click;
            if (this.btnBrowseOutput != null) this.btnBrowseOutput.Click += btnBrowseOutput_Click;
            if (this.btnProcess != null) this.btnProcess.Click += btnProcess_Click;

            // Đảm bảo ô nhập mẫu chương luôn bật (nếu tồn tại)
            if (this.txtInputFormat != null) this.txtInputFormat.Enabled = true;
        }

        // --- Sự kiện Click nút chọn Input ---
        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*", Title = "Chọn file input" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedInputFilePath = ofd.FileName;
                    if (this.txtInputFile != null) this.txtInputFile.Text = selectedInputFilePath;
                    selectedOutputFilePath = "";
                    if (txtOutputFile != null) txtOutputFile.Text = "";
                    UpdateStatus("Đã chọn input.", Color.Blue);
>>>>>>> 2198815 (commit message)
                }
            }
        }

<<<<<<< HEAD
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
=======
        // --- Sự kiện Click nút chọn Output ---
        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*", Title = "Chọn nơi lưu file output" })
            {
                string suggestedName = "output_processed.txt"; string initialDir = "";
                try { initialDir = Path.GetDirectoryName(selectedInputFilePath); suggestedName = Path.GetFileNameWithoutExtension(selectedInputFilePath) + "_processed.txt"; } catch { }
                sfd.InitialDirectory = initialDir; sfd.FileName = suggestedName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    selectedOutputFilePath = sfd.FileName;
                    if (this.txtOutputFile != null) this.txtOutputFile.Text = selectedOutputFilePath;
                    UpdateStatus("Đã chọn output.", Color.Blue);
>>>>>>> 2198815 (commit message)
                }
            }
        }

<<<<<<< HEAD
        private async void btnProcess_Click(object sender, EventArgs e)
        {
=======
        // === HÀM XỬ LÝ CHÍNH KHI NHẤN NÚT "XỬ LÝ" ===
        private async void btnProcess_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra Input/Output cơ bản
>>>>>>> 2198815 (commit message)
            if (string.IsNullOrWhiteSpace(selectedInputFilePath) || !File.Exists(selectedInputFilePath)) { MessageBox.Show("Chọn file input hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(selectedOutputFilePath)) { MessageBox.Show("Chọn nơi lưu file output.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try { if (Path.GetFullPath(selectedInputFilePath).Equals(Path.GetFullPath(selectedOutputFilePath), StringComparison.OrdinalIgnoreCase)) { MessageBox.Show("Input và output trùng nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } }
            catch (Exception ex) { MessageBox.Show($"Lỗi kiểm tra đường dẫn:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
<<<<<<< HEAD
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

=======

            // 2. Lấy cấu hình từ giao diện
            string standardizeInputFormat = this.txtInputFormat?.Text ?? "";
            Encoding inputEncoding = Encoding.UTF8;
            if (this.cmbEncoding != null && this.cmbEncoding.SelectedValue is string encName)
            {
                try { inputEncoding = Encoding.GetEncoding(encName); } catch { /* Dùng mặc định nếu lỗi */ }
            }
            else if (this.cmbEncoding != null && encodingOptions.TryGetValue(this.cmbEncoding.Text, out string encNameFallback))
            {
                try { inputEncoding = Encoding.GetEncoding(encNameFallback); } catch { /* Dùng mặc định */ }
            }

            // 3. Chuẩn bị biến và trạng thái
            SetProcessingState(true); // Khóa giao diện
            string junkFilePath = Path.Combine(Application.StartupPath, "res", JunkFileName);
            List<string> originalLines = new List<string>(); // List chứa các dòng đọc từ file
            List<string> processedLines; // List chứa kết quả sau mỗi bước
            StringBuilder processLog = new StringBuilder(); // Log chi tiết cho MessageBox cuối cùng
            int linesRead = 0, junkLinesRemovedCount = 0, stdCount = 0, duplicateLinesRemovedCount = 0;
            bool didStandardize = false;

            try
            {
                // 4. Đọc File Input
                UpdateStatus($"Đang đọc file (Encoding: {inputEncoding.EncodingName})...", Color.Orange);
                try
                {
                    using (var reader = new StreamReader(selectedInputFilePath, inputEncoding)) { string? ln; while ((ln = await reader.ReadLineAsync()) != null) originalLines.Add(ln); linesRead = originalLines.Count; }
                    UpdateStatus($"Đã đọc {linesRead} dòng. Bắt đầu xử lý...", Color.Orange);
                }
                catch (DecoderFallbackException decEx) { throw new DecoderFallbackException($"Lỗi mã hóa khi đọc file input '{inputEncoding.EncodingName}'.", decEx.BytesUnknown, decEx.Index); }
                catch (IOException exRead) { throw new IOException($"Lỗi đọc file input: {exRead.Message}", exRead); }

                processedLines = new List<string>(originalLines); // Bắt đầu xử lý từ bản gốc

                // 5. Lọc Rác (Dùng Regex Pattern)
                UpdateStatus("Đang lọc rác (dùng pattern)...", Color.Orange);
                processLog.AppendLine(" - Lọc rác (dùng pattern):");
                List<Regex> junkRegexList = new List<Regex>();
                if (File.Exists(junkFilePath))
                {
                    try
                    {
                        string[] rawJunkPatterns = await File.ReadAllLinesAsync(junkFilePath, Encoding.UTF8);
                        foreach (string pattern in rawJunkPatterns) { if (!string.IsNullOrWhiteSpace(pattern)) { Regex compiledRegex = ConvertJunkPatternToRegex(pattern.Trim()); if (compiledRegex != null) junkRegexList.Add(compiledRegex); } }
                        processLog.AppendLine($"   + Đã nạp {junkRegexList.Count} mẫu rác từ {JunkFileName}");
                    }
                    catch (Exception exJunk) { processLog.AppendLine($"   + Lỗi đọc/biên dịch file rác: {exJunk.Message}"); }
                }
                else { processLog.AppendLine($"   + Không tìm thấy file rác: {junkFilePath}"); }

                if (junkRegexList.Count > 0)
                {
                    List<string> linesAfterJunkFilter = new List<string>();
                    foreach (string line in processedLines) // Xử lý trên danh sách hiện tại
                    {
                        string trimmedLine = line.Trim();
                        bool isJunk = false;
                        if (!string.IsNullOrEmpty(trimmedLine)) { foreach (Regex junkRegex in junkRegexList) { if (junkRegex.IsMatch(trimmedLine)) { isJunk = true; break; } } }
                        if (!isJunk) linesAfterJunkFilter.Add(line); else junkLinesRemovedCount++;
                    }
                    processedLines = linesAfterJunkFilter; // Cập nhật danh sách dòng
                    processLog.AppendLine($"   + Đã loại bỏ {junkLinesRemovedCount} dòng khớp mẫu rác.");
                }
                else { processLog.AppendLine("   + Không có mẫu rác nào được áp dụng."); }


                // 6. Chuẩn Hóa Chương (Chỉ chạy khi mẫu hợp lệ)
                processLog.AppendLine(" - Chuẩn hóa chương:");
                if (!string.IsNullOrWhiteSpace(standardizeInputFormat) && standardizeInputFormat.Contains('*'))
                {
                    UpdateStatus("Đang chuẩn hóa chương...", Color.Orange);
                    string outputFormat = "第*章"; // Hoặc lấy từ cấu hình khác
                    List<string> standardizedLines = new List<string>();
                    foreach (string line in processedLines) // Xử lý trên danh sách hiện tại
                    { string? pLine = ChapterUtils.DoiTenChuong(line, standardizeInputFormat, outputFormat); standardizedLines.Add(pLine ?? line); if (pLine != null) stdCount++; }
                    processedLines = standardizedLines; // Cập nhật danh sách dòng
                    didStandardize = true;
                    processLog.AppendLine($"   + Đã chuẩn hóa {stdCount} dòng.");
                }
                else { processLog.AppendLine("   + Bỏ qua (không có mẫu hợp lệ)."); }


                // 7. Loại Bỏ Lặp & Dòng Trống
                UpdateStatus("Đang loại bỏ dòng lặp và dòng trống...", Color.Orange);
                processLog.AppendLine(" - Loại bỏ dòng lặp và dòng trống:");
                List<string> uniqueLinesResult = new List<string>();
                HashSet<string> uniqueCores = new HashSet<string>();
                int initialLineCount = processedLines.Count; // Đếm số dòng trước khi loại bỏ lặp/trống

                foreach (string line in processedLines) // Xử lý trên danh sách hiện tại
                {
                    string trimmedLine = line.Trim();
                    if (string.IsNullOrEmpty(trimmedLine)) continue; // Bỏ qua dòng trống

                    string core = datePrefixRegex.Replace(parenthesesRegex.Replace(trimmedLine, ""), "").Trim();
                    if (!string.IsNullOrEmpty(core)) { if (uniqueCores.Add(core)) { uniqueLinesResult.Add(line); } else { duplicateLinesRemovedCount++; } }
                    // Bỏ qua dòng có core rỗng (sau khi xử lý regex)
                }
                processedLines = uniqueLinesResult; // Cập nhật danh sách dòng cuối cùng
                int blankAndEmptyCoreLinesRemoved = initialLineCount - processedLines.Count - duplicateLinesRemovedCount; // Tính số dòng trống/core rỗng
                processLog.AppendLine($"   + Đã loại bỏ {duplicateLinesRemovedCount} dòng lặp (có tên chương trùng).");
                processLog.AppendLine($"   + Đã loại bỏ {blankAndEmptyCoreLinesRemoved} dòng trống hoặc dòng chỉ chứa tiền tố/ngoặc.");


                // 8. Ghi File Output
                UpdateStatus($"Đang ghi kết quả ({processedLines.Count} dòng)...", Color.Orange);
                await File.WriteAllLinesAsync(selectedOutputFilePath, processedLines, inputEncoding);

                // 9. Thông báo Hoàn thành
                string finalStatus = $"Hoàn thành! File output có {processedLines.Count} dòng.";
                UpdateStatus(finalStatus, Color.Green);
                // Tạo thông báo chi tiết cuối cùng
                StringBuilder finalSummary = new StringBuilder();
                finalSummary.AppendLine(finalStatus);
                finalSummary.AppendLine("--- Tóm tắt các bước ---");
                finalSummary.AppendLine($"Đọc file input: {linesRead} dòng.");
                finalSummary.AppendLine($"Lọc rác (dùng pattern): {junkLinesRemovedCount} dòng bị loại bỏ.");
                if (didStandardize) finalSummary.AppendLine($"Chuẩn hóa chương: {stdCount} dòng được chuẩn hóa.");
                else finalSummary.AppendLine("Chuẩn hóa chương: Bỏ qua.");
                finalSummary.AppendLine($"Loại bỏ lặp/trống: {duplicateLinesRemovedCount} dòng lặp và {blankAndEmptyCoreLinesRemoved} dòng trống/rỗng đã bị loại bỏ.");
                MessageBox.Show(finalSummary.ToString(), "Hoàn thành", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            // --- Khối Catch Exception (Giữ nguyên) ---
            catch (IOException ioEx) { UpdateStatus($"Lỗi I/O: {ioEx.Message}", Color.Red); MessageBox.Show($"Lỗi đọc/ghi file:\n{ioEx.Message}", "Lỗi I/O", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (UnauthorizedAccessException authEx) { UpdateStatus($"Lỗi quyền: {authEx.Message}", Color.Red); MessageBox.Show($"Không có quyền truy cập:\n{authEx.Message}", "Lỗi Quyền", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (DecoderFallbackException decEx) { UpdateStatus($"Lỗi Mã Hóa Input: {decEx.Message}", Color.Red); MessageBox.Show($"Lỗi Mã Hóa khi đọc file input với encoding '{inputEncoding.EncodingName}'.\nChi tiết: {decEx.Message}", "Lỗi Mã Hóa Input", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (Exception ex) { UpdateStatus($"Lỗi không xác định: {ex.Message}", Color.Red); MessageBox.Show($"Lỗi không xác định:\n{ex.Message}", "Lỗi Chung", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                SetProcessingState(false); // Luôn mở lại giao diện
            }
        }

        // --- Hàm chuyển đổi Mẫu Rác thành Regex ---
        private static Regex ConvertJunkPatternToRegex(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) return null;
            // Dùng Guid để tạo placeholder tạm thời, tránh xung đột nếu mẫu có chứa "__STAR__"
            string tempPlaceholderStar = Guid.NewGuid().ToString("N");
            string tempPlaceholderDash = Guid.NewGuid().ToString("N");

            string escapedPattern = Regex.Escape(pattern.Replace("*", tempPlaceholderStar).Replace("-", tempPlaceholderDash));
            string regexPattern = escapedPattern.Replace(tempPlaceholderStar, @"\d+").Replace(tempPlaceholderDash, "-");
            regexPattern = $"^{regexPattern}$"; // Khớp toàn bộ dòng

            try { return new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase); }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Lỗi biên dịch mẫu Regex rác '{pattern}': {ex.Message}");
                // Có thể hiển thị lỗi cho người dùng nếu muốn:
                // MessageBox.Show($"Mẫu rác không hợp lệ trong file {JunkFileName}: '{pattern}'\nLỗi: {ex.Message}", "Lỗi Mẫu Rác", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        // --- Hàm cập nhật Label trạng thái ---
>>>>>>> 2198815 (commit message)
        private void UpdateStatus(string message, Color color)
        {
            if (this.lblStatus != null) { if (lblStatus.InvokeRequired) { lblStatus.BeginInvoke(new Action(() => { lblStatus.Text = message; lblStatus.ForeColor = color; })); } else { lblStatus.Text = message; lblStatus.ForeColor = color; } }
        }

<<<<<<< HEAD
=======
        // --- Hàm khóa/mở giao diện khi xử lý ---
>>>>>>> 2198815 (commit message)
        private void SetProcessingState(bool isProcessing)
        {
            bool enableState = !isProcessing;
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => SetProcessingState(isProcessing))); return; }
<<<<<<< HEAD
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
=======
            foreach (Control c in this.Controls) { if (c != lblStatus) c.Enabled = enableState; } // Khóa/mở tất cả trừ status
            this.Cursor = isProcessing ? Cursors.WaitCursor : Cursors.Default;
        }

        // --- Lớp tiện ích xử lý chương (Ví dụ - Cần thay thế logic) ---
        public static class ChapterUtils
        {
            // Hàm ví dụ, cần được thay thế bằng logic thực tế của bạn
            public static string? DoiTenChuong(string inputLine, string inputPattern, string outputPattern)
            {
                try
                {
                    // Chuyển đổi mẫu input thành Regex, coi * là group cần lấy
                    string regexPattern = Regex.Escape(inputPattern).Replace(@"\*", @"(.*?)"); // Lấy group bất kỳ non-greedy
                    Match match = Regex.Match(inputLine.Trim(), $"^{regexPattern}");
                    if (match.Success && match.Groups.Count > 1)
                    {
                        return outputPattern.Replace("*", match.Groups[1].Value);
                    }
                }
                catch (Exception ex) { Console.WriteLine($"Error in DoiTenChuong: {ex.Message}"); }
                return null;
            }
>>>>>>> 2198815 (commit message)
        }
    }
}