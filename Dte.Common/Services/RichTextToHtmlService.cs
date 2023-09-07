using System.Linq;
using System.Text;
using Dte.Common.Models;

namespace Dte.Common.Services
{
    public class RichTextToHtmlService
    {
        public string Convert(RichTextNode node, string baseUrl)
        {
            StringBuilder html = new StringBuilder();

            switch (node.NodeType)
            {
                case "document":
                    foreach (var contentNode in node.Content)
                    {
                        html.Append(Convert(contentNode, baseUrl));
                    }

                    break;

                case "heading-1":
                    html.Append(
                        $"<span style='font-size: 24px; color: #193e72'><strong>{node.Content.FirstOrDefault()?.Value}</strong></span>");
                    break;
                case "heading-2":
                    html.Append(
                        $"<span style='font-size: 20px; color: #193e72'><strong>{node.Content.FirstOrDefault()?.Value}</strong></span>");
                    break;
                case "heading-3":
                    html.Append(
                        $"<span style='font-size: 18px; color: #193e72'><strong>{node.Content.FirstOrDefault()?.Value}</strong></span>");
                    break;
                case "heading-4":
                    html.Append(
                        $"<span style='font-size: 16px; color: #193e72'><strong>{node.Content.FirstOrDefault()?.Value}</strong></span>");
                    break;

                case "paragraph":
                    foreach (var contentNode in node.Content)
                    {
                        html.Append(
                            $"<p style='display: block; margin: 13px 0'>{Convert(contentNode, baseUrl)}</p>");
                    }

                    break;

                case "text":
                    html.Append($"<span style='font-size: 16px'>{node.Value}</span>");
                    break;

                case "hyperlink":
                    var uri = node.Data["uri"].ToString();
                    if (uri == null) break;
                    html.Append("<span style='font-size: 16px'>");
                    if (uri.StartsWith("mailto") || uri.StartsWith("http"))
                    {
                        html.Append(
                            $"<a href='{uri}' style='color: #193e72; text-decoration: none'>{node.Content.FirstOrDefault()?.Value}</a>");
                    }
                    else 
                    {
                        html.Append(
                            $"<a href='{baseUrl}{uri}' style='color: #193e72; text-decoration: none'>{baseUrl}{node.Content.FirstOrDefault()?.Value}</a>");
                    }
                    html.Append("</span>");
                    break;
            }

            return html.ToString();
        }
    }
}