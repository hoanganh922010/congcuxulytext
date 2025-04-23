using System;
using System.Text.RegularExpressions;

namespace chuanstt // Đảm bảo namespace khớp
{
    public static class ChapterUtils
    {
        /// <summary>
        /// Tìm và thay thế phần đánh dấu chương ở đầu dòng dựa trên mẫu do người dùng cung cấp.
        /// </summary>
        /// <param name="line">Dòng văn bản gốc.</param>
        /// <param name="inputFormat">Mẫu đầu vào do người dùng nhập, dùng '*' thay cho vị trí số (ví dụ: "Chương *:", "第*章.").</param>
        /// <param name="outputFormat">Mẫu đầu ra mong muốn, dùng '*' thay cho vị trí số (ví dụ: "第*章").</param>
        /// <returns>Dòng đã được thay thế phần đầu nếu khớp mẫu, hoặc null nếu không khớp mẫu ở đầu dòng.</returns>
        public static string? DoiTenChuong(string line, string inputFormat, string outputFormat = "第*章")
        {
            // Kiểm tra đầu vào cơ bản
            if (string.IsNullOrWhiteSpace(line) ||
                string.IsNullOrWhiteSpace(inputFormat) ||
                !inputFormat.Contains('*'))
            {
                return null; // Không thể xử lý
            }

            // Nếu mẫu đầu ra trống hoặc không hợp lệ, dùng mặc định
            if (string.IsNullOrWhiteSpace(outputFormat) || !outputFormat.Contains('*'))
            {
                outputFormat = "第*章";
            }

            try
            {
                // 1. Chuyển đổi inputFormat thành Regex Pattern
                string pattern = Regex.Escape(inputFormat).Replace(@"\*", @"(\d+)");
                pattern = "^" + pattern; // Chỉ khớp ở đầu dòng

                // 2. Thực hiện khớp Regex
                Match match = Regex.Match(line, pattern);

                // 3. Xử lý nếu khớp
                if (match.Success && match.Groups.Count > 1)
                {
                    string matchedMarker = match.Value;
                    string soChuong = match.Groups[1].Value;
                    string restOfLine = line.Substring(matchedMarker.Length);

                    // 4. Tạo phần đầu mới theo outputFormat
                    string newMarker = outputFormat.Replace("*", soChuong);

                    // 5. Nối phần đầu mới với phần còn lại
                    return newMarker + restOfLine;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Lỗi Regex không hợp lệ từ inputFormat '{inputFormat}': {ex.Message}");
                return null; // Trả về null nếu có lỗi Regex
            }
            catch (Exception ex) // Bắt các lỗi khác có thể xảy ra
            {
                Console.WriteLine($"Lỗi không mong muốn trong DoiTenChuong: {ex.Message}");
                return null;
            }


            // 6. Nếu không khớp ở đầu dòng
            return null;
        }
    }
}