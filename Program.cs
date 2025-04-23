using System;
using System.Windows.Forms;
using System.Text; // Cần thêm dòng này

namespace chuanstt // Đảm bảo namespace khớp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Đăng ký provider để hỗ trợ các encoding cũ trên .NET Core/5+
            // Đặt ở đây để đảm bảo nó được gọi trước khi Form1 khởi tạo
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            ApplicationConfiguration.Initialize(); // Dùng cho .NET 6+
            Application.Run(new Form1());
        }
    }
}