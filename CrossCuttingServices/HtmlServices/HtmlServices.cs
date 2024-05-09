using System;
using CsQuery;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Sra.P2rmis.CrossCuttingServices
{
   /// <summary>
   /// The HtmlServices class provides HTML data format manipulation.
   /// </summary>
    public static class HtmlServices
    {
        /// <summary>
        /// Collection of css styles available for re-use with server side html processing
        /// </summary>
        public static class HtmlStyles
        {
            /// <summary>
            /// Font-family style property for contract documents
            /// </summary>
            public const string ContractFontFamily = "Arial";
            /// <summary>
            /// Font-size style property for main contract text
            /// </summary>
            public const string ContractMainFontSize = "14px";
            /// <summary>
            /// Font-size style property for sub contract text
            /// </summary>
            public const string ContractSubFontSize = "12px";
            /// <summary>
            /// Line-height property for contract documents
            /// </summary>
            public const string ContractLineHeight = "20px";
            /// <summary>
            /// Margin-left style property for sub contract text
            /// </summary>
            public const string ContractSubMarginLeft = "5px";
        }
        /// <summary>
        /// Gets a clean version of HTML content by auto-accepting tracked changes
        /// </summary>
        /// <param name="htmlContent">The HTML content with tracked changes</param>
        /// <returns>The clean HTML content with tracked changes accepted</returns>
        public static string GetHtmlContentByAcceptingTrackedChanges(string htmlContent)
        {
            CQ dom = htmlContent;
            var changedNodes = dom.Select("[command]");
            foreach (var node in changedNodes)
            {
                node.RemoveAttribute("title");
                node.RemoveAttribute("timestamp");                    
                node.RemoveAttribute("class");
                node.RemoveAttribute("author");
                node.RemoveAttribute("cssproperty");
                node.RemoveAttribute("command");
            }
            var iframeNodes = dom.Select("iframe");
            foreach (var node in iframeNodes)
            {
                node.Remove();
            }
            var deletedNodes = dom.Select("del");
            foreach (var node in deletedNodes)
            {
                node.Remove();
            }
            var insertedNodes = dom.Select("ins");
            foreach (var node in insertedNodes)
            {
                node.OuterHTML = node.InnerHTML;
            }
            var newHtml = ReplaceSpanUlWithTag(dom).Render();
            return WebUtility.HtmlDecode(newHtml);
        }
        /// <summary>
        /// Gets a clean version of HTML content for PDF creation
        /// </summary>
        /// <param name="htmlContent">The HTML content with JavaScript and non-necessary elements/styles</param>
        /// <returns>The clean HTML content with styles required for PDF creation</returns>
        public static string GetHtmlContentAsPdfCompatible(string htmlContent)
        {
            CQ dom = CQ.CreateDocument(htmlContent);
            var jsNodes = dom.Select("script");
            foreach (var node in jsNodes)
            {
                node.Remove();
            }
            var hiddenInputNodes = dom.Select("input[type='hidden']");
            foreach (var node in hiddenInputNodes)
            {
                node.Remove();
            }
            dom = ReplaceTextareaWithDiv(dom);
            var newHtml = MoveHeaderToEnd(dom).Render();
            return newHtml;
        }
        /// <summary>
        /// Gets the node HTML by identifier.
        /// </summary>
        /// <param name="htmlContent">Content of the HTML.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <returns></returns>
        public static string GetNodeHtmlById(string htmlContent, string nodeId)
        {
            CQ dom = CQ.CreateDocument(htmlContent);
            var node = dom.Select("#" + nodeId);
            var nodeHtml = node.Html();
            return nodeHtml;
        }
        /// <summary>
        /// Update document signature
        /// </summary>
        /// <param name="htmlContent">The document's HTML content</param>
        /// <param name="signature">The signature string</param>
        /// <returns>The document's HTML content with signature</returns>
        public static string UpdateDocumentSignature(string htmlContent, string signature)
        {
            CQ dom = CQ.CreateDocument(htmlContent);
            var signatureNode = dom.Select("#signatureSection")[0];
            signatureNode.InnerHTML = signature;
            //Hack to remove padding and margin from body tag to eliminate blank page issue
            RemoveBodyStyle(dom);
            var newHtml = dom.Render();
            return newHtml;
        }

        /// <summary>
        /// Removes the default body style spacing.
        /// </summary>
        /// <param name="dom">The DOM object.</param>
        /// <returns>DOM with body margin/padding removed</returns>
        /// <remarks>This function helps prevent blank page generating in pdf version</remarks>
       internal static CQ RemoveBodyStyle(CQ dom)
       {
           var bodyNode = dom.Select("body").First();
           bodyNode.CssSet(new {padding = "0", margin = "0"});
           return bodyNode;
       }

       /// <summary>
        /// Update document signature name/date on the contract
        /// </summary>
        /// <param name="htmlContent">The document's HTML content</param>
        /// <param name="signedDate">The date signed</param>
        /// <returns>The document's HTML content with signature on contract</returns>
        public static string UpdateDocumentSignatureOnContract(string htmlContent, string signedDate)
        {
            CQ dom = CQ.CreateDocument(htmlContent);
            var signatureDateNode = dom.Select("#signatureDateOnContract")[0];
            if (signatureDateNode != null)
            {
                signatureDateNode.InnerHTML = signedDate;
            }
            var signatureNameNode = dom.Select("#contractAdminSignature")[0];
            if (signatureNameNode != null)
            {
                signatureNameNode.Style.Remove("display");
            }
            var newHtml = dom.Render();
            return newHtml;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fiscalYear"></param>
        /// <param name="programAbbreviation"></param>
        /// <param name="panelAbbreviation"></param>
        /// <param name="documentId"></param>
        /// <param name="documentVersion"></param>
        /// <param name="signatureContent"></param>
        /// <returns></returns>
        public static string GetHtmlDocumentSignature(string fiscalYear, string programAbbreviation, string panelAbbreviation, int documentId, string documentVersion, string signatureContent, string helpText)
        {
            CQ dom = $"<div style='font-family: {HtmlStyles.ContractFontFamily}; font-size: {HtmlStyles.ContractMainFontSize}; line-height: {HtmlStyles.ContractLineHeight};'>" + 
                $"<div><label><strong>Panel: </strong></label><strong>{fiscalYear} {programAbbreviation} {panelAbbreviation}</strong></div>" +
                $"<div><label>Doc ID: </label><span style='margin-right:15px;'>{documentId}</span> <label>Version: </label><span>{documentVersion}</span></div>" +
                $"<div><label>Signed: </label><span>{signatureContent}</span></div>" +
                $"<div style='font-size: {HtmlStyles.ContractSubFontSize}; margin-left: {HtmlStyles.ContractSubMarginLeft};'>{helpText}</div></div>";
            return dom.Render();
        }

        private static CQ ReplaceTextareaWithDiv(CQ dom)
        {
            var textAreas = dom.Select("textarea");
            foreach (var node in textAreas)
            {
                node.OuterHTML = String.Format("{0}{1}{2}", "<div>", node.InnerHTML, "</div>");
            }
            return dom;
        }

        /// <summary>
        /// Move header to the end
        /// </summary>
        /// <param name="dom">The CQ object before the move</param>
        /// <returns>The CQ object after the move</returns>
        private static CQ MoveHeaderToEnd(CQ dom)
        {
            // Get header
            var headerNode = dom.Select("#documentFormHeader")[0];
            // Clone to be footer
            var endNode = CQ.CreateFragment(headerNode.Clone().OuterHTML);
            // Insert to body
            dom.Select("#documentFormContent").After(endNode);
            // Remove header
            headerNode.Remove();
            return dom;
        }

        //Hack to add u tags for SSRS to render properly
        //Styles will be duplicated, however browser ignores this (we can't just remove style attribute in case there are others?)
       internal static CQ ReplaceSpanUlWithTag(CQ dom)
       {
           var ulSpan = dom.Select("span[style*='underline']");
           foreach (var node in ulSpan)
           {
               node.InnerHTML = String.Format("{0}{1}{2}", "<u>", node.InnerHTML, "</u>");
           }
           return dom;
       }
       /// <summary>
       /// Remove any markup from the display string.
       /// </summary>
       /// <param name="s">Display string</param>
       /// <returns>String without markup</returns>
       public static string RemoveMarkup(string s)
       {
           string result = string.Empty;

           if (s != null)
           {
               HtmlDocument htmlDoc = new HtmlDocument();
               htmlDoc.LoadHtml(s);
               result = htmlDoc.DocumentNode.InnerText;
               result = HtmlEntity.DeEntitize(result);
           }
           return result;
        }

        /// <summary>
        /// Remove any markup from the display string while maintaining paragraph structure.
        /// </summary>
        /// <param name="text">Display string</param>
        /// <returns>Display string without markup but paragraph structure maintained</returns>
        public static string RemoveMarkupButMaintainParagraphs(string text)
        {
            //
            // if we have a string to play with
            //
            string result = text;

            if (!string.IsNullOrEmpty(text))
            {
                //
                // First we replace any close paragraph marker with a close paragraph marker & a carriage return.
                //
                result = text.Replace("</p>", "</p>\n");
                //
                // Then we remove the last carriage return.  If you do not do this you will end up with a new empty
                // paragraph at the end each time the content is processed.
                //
                int location = result.LastIndexOf("\n");
                result = (location > 0) ? result.Substring(0, location) : result;
            }
            return (string.IsNullOrEmpty(text)) ? text : RemoveMarkup(result);
        }
        /// <summary>
        /// Replace carriage returns (/n) with HTML paragraph markers (<p></p>).  Intended for use with the 
        /// criteria editor.
        /// </summary>
        /// <param name="text">Test string received from the editor</param>
        /// <returns>Updated string with the carriage returns replaced with HTML paragraph markers</returns>
        public static string ReplaceParagraphMarkers(string text)
        {
            return (string.IsNullOrEmpty(text)) ? text : String.Concat("<p>", text.Replace("\n", "</p><p>"), "</p>");
        }
        /// <summary>
        /// Surrounds a string with paragraph markup.
        /// </summary>
        /// <param name="text">string to make into paragraph</param>
        /// <returns>Marked up string</returns>
        public static string SurroundWithParagraphMarkers(string text)
        {
            return string.Concat("<p>", text, "</p>");
        }

        /// <summary>
        /// Sanitizes the HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns></returns>
        public static string SanitizeHtml(string html)
        {
            string acceptable = "b|i|u|p|strong|em|ol|ul|li|span";
            string stringPattern = @"</?(?(?=" + acceptable + @")notag|[a-zA-Z0-9]+)(?:\s[a-zA-Z0-9\-]+=?(?:(["",']?).*?\1?)?)*\s*/?>";
            html = Regex.Replace(html, "<span style=\"text-decoration: underline;\">(.*?)</span>", "<u>$1</u>", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
            return Regex.Replace(html, stringPattern, string.Empty);
        }
        /// <summary>
        /// Gets the contents with title.
        /// </summary>
        /// <param name="contentsWithTitle">The contents with title.</param>
        /// <returns></returns>
        public static string GetContentsWithTitle(List<KeyValuePair<string, string>> contentsWithTitle)
        {
            var htmlContent = new StringBuilder();
            foreach(var x in contentsWithTitle)
            {
                htmlContent.Append(String.Format("<p><b>{0}</b></p>", x.Key));
                htmlContent.Append(String.Format("<p>{0}</p>", x.Value));
            }
            return htmlContent.ToString();
        }
    }
}
