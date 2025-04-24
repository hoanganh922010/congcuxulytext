using System;
using System.Text.RegularExpressions;

namespace chuonglap
{
    public static class ChapterUtils
    {
        public static string? DoiTenChuong(string line, string inputFormat, string outputFormat = "第*章")
        {
            if (string.IsNullOrWhiteSpace(line) ||
                string.IsNullOrWhiteSpace(inputFormat) ||
                !inputFormat.Contains('*'))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(outputFormat) || !outputFormat.Contains('*'))
            {
                outputFormat = "第*章";
            }

            try
            {
                string pattern = Regex.Escape(inputFormat).Replace(@"\*", @"(\d+)");
                pattern = "^" + pattern + @"\s*";

                Match match = Regex.Match(line, pattern);

                if (match.Success && match.Groups.Count > 1)
                {
                    string matchedMarker = match.Value;
                    string soChuong = match.Groups[1].Value;
                    string restOfLine = line.Substring(matchedMarker.Length);
                    string newMarker = outputFormat.Replace("*", soChuong);

                    if (!newMarker.EndsWith(' ') && !string.IsNullOrEmpty(restOfLine) && !char.IsWhiteSpace(restOfLine[0]))
                    {
                        return newMarker + " " + restOfLine;
                    }
                    else
                    {
                        return newMarker + restOfLine;
                    }
                }
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
    }
}