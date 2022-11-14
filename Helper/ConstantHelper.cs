using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Helper
{
    public static class ConstantHelper
    {
        public static string hostEmail = "sneakershoptmdt2022@gmail.com";
        public static int portEmail = 587;
        public static string emailSender = "sneakershoptmdt2022@gmail.com";
        public static string passwordSender = "@Aa1234567890";

        private static string GetFirstParagraph(string htmltext)
        {
            Match m = Regex.Match(htmltext, @"<p>\s*(.+?)\s*</p>");
            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                return htmltext;
            }
        }
    }
}