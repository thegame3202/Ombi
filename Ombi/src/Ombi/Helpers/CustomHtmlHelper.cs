using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Ombi.Helpers;

namespace Ombi.Helpers
{
    public static class CustomHtmlHelper
    {
        public static IHtmlContent GetInformationalVersion(this IHtmlHelper helper)
        {
            var fileVersion = AssemblyHelper.GetAssemblyVersion();
            var htmlString = $"<!--\r\n##################################################################\r\nVersion: {fileVersion}\r\n##################################################################\r\n--> ";

            return helper.Raw(htmlString);
        }

        public static IHtmlContent Checkbox(this IHtmlHelper helper, bool check, string name, string display)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div class=\"form-group\">");
            sb.AppendLine("<div class=\"checkbox\">");
            sb.AppendFormat("<input type=\"checkbox\" id=\"{0}\" name=\"{0}\" {2}><label for=\"{0}\">{1}</label>", name, display, check ? "checked=\"checked\"" : string.Empty);
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return helper.Raw(sb.ToString());
        }
    }
}