﻿using System;
using System.Text;
using System.Windows.Forms;

namespace chuonglap
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}