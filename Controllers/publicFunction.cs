using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Controllers
{
    public static class publicFunction
    {
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