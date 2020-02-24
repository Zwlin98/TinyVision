using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public static class Files
    {
        public static string GetLastPartNameOfPath(string path)
        {
            // 正则说明：
            // [^/\\]   ：表示匹配除了斜杠(/)和反斜杠(\)以外的任意字符，双反斜杠用于转义
            // +        ：表示匹配前面的表达式一次或多次
            // [/\\]    ：表示匹配斜杠(/)或反斜杠(\)
            // *        ：表示匹配零次或多次
            // $        ：表示从后向前匹配

            // 截取最后一部分名称，名称的末尾可能带有多个斜杠(/)或反斜杠(\)
            var pattern = @"[^/\\]+[/\\]*$";
            var match = System.Text.RegularExpressions.Regex.Match(path, pattern);
            var name = match.Value;

            // 截取名称中不带斜杠(/)或反斜杠(\)的部分
            pattern = @"[^/\\]+";
            match = System.Text.RegularExpressions.Regex.Match(name, pattern);
            name = match.Value;

            return name;
        }
        
        private static string[] imagefiles = new string[] {
            "*.bmp",
            "*.dib",
            "*.jpeg",
            "*.jpg",
            "*.jpe",
            "*.png",
            "*.pbm",
            "*.pgm",
            "*.ppm",
            "*.sr",
            "*.ras",
            "*.tiff",
            "*.tif",
            "*.exr",
            "*.jp2"
        };

        private static string GenImageFileSuffix(string type)
        {
            return $"*.{type}";
        }

        public static string GetOpenFile()
        {
            string filter = "";
            foreach (var type in imagefiles)
            {
                filter += $"{type};";
            }
            return $"Image files ({filter})|{filter}|All files(*.*)|*.*";
        }
    }
}