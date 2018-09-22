using Ormer.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class StringExtensions
    {
        //A marker to replace newline
        private const string MarkerCommonNewline = "<t:common.newline>";

        //Temp data folder
        private const string Folder_TempData = @"Cache\Temp\";

        /// <summary>
        /// Trim all form newline of start and end position
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        public static string TrimNewLine(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = str.Trim('\r');
            str = str.Trim('\n');
            str = str.Trim("\r\n".ToCharArray());

            return str;
        }

        /// <summary>
        /// Trim all form newline of start and end position, then append an empty line to end(two newline in total).
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns></returns>
        public static string TrimThenAppendNewLine(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            str = str.TrimNewLine();
            return str + Environment.NewLine + Environment.NewLine;
        }
        
        public static string Replace(this string str, string marker, string newValue, bool backspaceIfNullOrEmpty)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (string.IsNullOrEmpty(newValue) && backspaceIfNullOrEmpty)
            {
                var oldValueIndex = str.IndexOf(marker);
                if (oldValueIndex >= 0)
                {
                    str = str.Replace(marker, string.Empty);
                    str = str.Substring(0, oldValueIndex - 1) + str.Substring(oldValueIndex + 1);
                }
            }
            else
            {
                str = str.Replace(marker, newValue);
            }

            return str;
        }

        public static string ReplaceDocumentary(this string str, bool append3Slash = false)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (append3Slash && !str.StartsWith("///"))
            {
                str += "///";
            }

            str = str.ReplaceNewlineWith("/// ");

            return str;
        }

        public static string ReplaceForSummary(this string str, string marker, string description, string extra = null, bool alignFirstLine = false)
        {
            var commonSummary = File.ReadAllText(@"Common\Templates\Common.Summary.txt");

            if (string.IsNullOrEmpty(description + extra))
            {
                str = str.Replace(marker, string.Empty, true);
            }

            if (!string.IsNullOrEmpty(extra))
            {
                description = description + Environment.NewLine + extra;
            }

            description = description.ReplaceDocumentary();
            commonSummary = commonSummary.Replace("<t:common.description>", description);

            if (alignFirstLine)
            {
                str = str.ReplaceAndAlignToFirstLine(marker, commonSummary);
            }
            else
            {
                str = str.Replace(marker, commonSummary);
            }
            
            return str;
        }

        public static string ReplaceNewlineWith(this string str, string newValue)
        {
            if (str == null)
            {
                return null;
            }
            str = str.Replace("\r\n", MarkerCommonNewline);
            str = str.Replace("\r", MarkerCommonNewline);
            str = str.Replace("\n", MarkerCommonNewline);
            str = str.Replace(MarkerCommonNewline, Environment.NewLine + newValue);

            return str;
        }

        public static string ReplaceAndAlignToFirstLine(this string str, string marker, string newValue)
        {
            var whitespace = GetWhitespaceToFill(str, marker);
            newValue = newValue.ReplaceNewlineWith(whitespace);

            str = str.Replace(marker, newValue);

            return str;
        }

        private static string GetWhitespaceToFill(this string str, string marker)
        {
            var strHash = HashUtil.Sha1(str);

            var findLineFileName = strHash + ".txt";
            var findLineFileFolder = Path.Combine(Folder_TempData, DateTime.Now.ToString("yyyyMMdd"));
            Directory.CreateDirectory(findLineFileFolder);
            var findLineFilePath = Path.Combine(findLineFileFolder, findLineFileName);

            string[] allLines;
            if (!File.Exists(findLineFilePath))
            {
                File.AppendAllText(findLineFilePath, str);
            }
            allLines = File.ReadAllLines(findLineFilePath);

            foreach (var line in allLines)
            {
                var index = line.IndexOf(marker);
                if (index > -1)
                {
                    var subStr = line.Substring(0, index);
                    var tabCount = subStr.ToCharArray().Count(m => m.Equals('\t'));

                    var lengthToFill = subStr.Length + tabCount * 3;
                    var whiteSpace = new StringBuilder();
                    for (var i = 0; i < lengthToFill; i++)
                    {
                        whiteSpace.Append(" ");
                    }

                    return whiteSpace.ToString();
                }
            }

            return string.Empty;
        }

        public static string ToFirstLetterLower(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string ToFirstLetterUpper(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 单词变成单数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToSingular(this string word)
        {
            var plural1 = new Regex("(?<keep>[^aeiou])ies$");
            var plural2 = new Regex("(?<keep>[aeiou]y)s$");
            var plural3 = new Regex("(?<keep>[sxzh])es$");
            var plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}y");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}");

            return word;
        }
        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToPlural(this string word)
        {
            var plural1 = new Regex("(?<keep>[^aeiou])y$");
            var plural2 = new Regex("(?<keep>[aeiou]y)$");
            var plural3 = new Regex("(?<keep>[sxzh])$");
            var plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}ies");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}s");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}es");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}s");

            return word;
        }

    }
}
