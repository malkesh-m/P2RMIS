using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Markup;
using Aspose.Words.Replacing;
using Aspose.Words.Reporting;
using Sra.P2rmis.CrossCuttingServices.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.CrossCuttingServices.DocumentServices
{
    /// <summary>
    /// Class that performs functions related to creating word documents
    /// </summary>
    public static class WordServices
    {
        /// <summary>
        /// Constant string for the MS template plaveholder
        /// </summary>
        public const string TemplatePlaceholderText = "click here to enter text.";

        #region Constructor

        /// <summary>
        /// Static constructor that sets license for aspose
        /// </summary>
        static WordServices()
        {
            License license = new License();
            license.SetLicense("Aspose.Words.lic");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates the report.
        /// </summary>
        /// <param name="templateFileUnc">The template location.</param>
        /// <param name="stream">The memory stream</param>
        /// <param name="dataSet">The data set.</param>
        public static void CreateReport(string templateFileUnc, ISummaryDocumentModel dataSet, MemoryStream stream)
        {
            Document doc = CreateDocumentFromTemplate(dataSet, templateFileUnc);
            SetIdTag(doc);
            RemoveEmptyNonTaggedParagraphSections(doc);
            doc.Save(stream, SaveFormat.Docx);
        }
        #endregion
        #region Helpers
        private static void SetIdTag(Document doc)
        {
            FindReplaceOptions options = new FindReplaceOptions();
            options.ReplacingCallback = new MoveIdToTag();
            doc.Range.Replace(new Regex("Tag\\|\\|[0-9]+\\|\\|", RegexOptions.IgnoreCase), "", options);
        }
        private static Document CreateDocumentFromTemplate(ISummaryDocumentModel dataSet, string templateLocation)
        {
            Document doc = new Document(templateLocation);
            ReportingEngine engine = new ReportingEngine();
            engine.BuildReport(doc, dataSet, "d");
            return doc;
        }
        private class MoveIdToTag : IReplacingCallback
        {
            ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
            {
                StructuredDocumentTag parentDocumentTag = (StructuredDocumentTag)e.MatchNode.ParentNode.ParentNode;
                parentDocumentTag.Tag = e.Match.Value.Replace("Tag||", string.Empty).Replace("||", string.Empty);
                return ReplaceAction.Replace;
            }
        }
        /// <summary>
        /// Reads the specified document file.
        /// </summary>
        /// <param name="documentData">The document byte data.</param>
        /// <returns>Dictionary collection of form fields from the document</returns>
        public static IList<Tuple<int, string, string>> Read(byte[] documentData)
        {
            var result = new List<Tuple<int, string, string>>();
            if (documentData != null)
            {
                using (Stream stream = new MemoryStream(documentData))
                {
                    using (stream)
                    {
                        Document doc = new Document(stream);
                        foreach (StructuredDocumentTag sdt in doc.GetChildNodes(NodeType.StructuredDocumentTag, true).ToArray())
                        {
                            if (!sdt.LockContents && sdt.Tag.Length > 1)
                            {
                                result.Add(new Tuple<int, string, string>(int.Parse(sdt.Tag), sdt.ToString(SaveFormat.Text), sdt.ToString(SaveFormat.Html)));
                            }
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Reads the specified document data.
        /// </summary>
        /// <param name="documentData">The document data.</param>
        /// <returns></returns>
        /// <remarks>I believe the read part of this should be removed and that method called seperately from BL as needed</remarks>
        public static byte[] Process(byte[] documentData)
        {
            MemoryStream newMemoryStream = new MemoryStream();
            if (documentData != null)
            {
                using (Stream stream = new MemoryStream(documentData))
                {
                    Document doc = new Document(stream);
                    // Get paragraph titles for all sections prior to acceptance of revisions.
                    var paragraphTitles = GetParagraphTitles(doc);
                    // Accept all revisions
                    doc.AcceptAllRevisions();
                    // Collect all comments in the document
                    NodeCollection comments = doc.GetChildNodes(NodeType.Comment, true);
                    // Remove all comments.
                    comments.Clear();
                    // Fix double strike data generated by Track Moves
                    FixDoubleStrikesSections(doc, paragraphTitles);
                    // Remove empty text sections
                    RemoveEmptyTextSections(doc);
                    // Remove rich text (we do this in a seperate step as removing early can mess up previous method, can possibly be done at once with redesigned template)
                    RemoveRichTextControls(doc);
                    doc.Save(newMemoryStream, SaveFormat.Docx);
                }
            }
            return newMemoryStream.ToArray();
        }
        /// <summary>
        /// Accepts the track changes and remove comments.
        /// </summary>
        /// <param name="documentData">The document data.</param>
        /// <returns></returns>
        public static byte[] AcceptTrackChangesAndRemoveComments(byte[] documentData)
        {
            MemoryStream newMemoryStream = new MemoryStream();
            if (documentData != null)
            {
                using (Stream stream = new MemoryStream(documentData))
                {
                    Document doc = new Document(stream);
                    // Accept all revisions
                    doc.AcceptAllRevisions();
                    // Collect all comments in the document
                    NodeCollection comments = doc.GetChildNodes(NodeType.Comment, true);
                    // Remove all comments.
                    comments.Clear();
                    doc.Save(newMemoryStream, SaveFormat.Docx);
                }
            }
            return newMemoryStream.ToArray();
        }
        /// <summary>
        /// Convert Word Byte Array to PDF stream
        /// </summary>
        /// <param name="documentFile"></param>
        /// <returns></returns>
        public static Stream ConvertWordToPDF(byte[] documentFile)
        {
            MemoryStream outStream = new MemoryStream();
            if (documentFile != null)
            {
                using (Stream stream = new MemoryStream(documentFile))
                {
                    var wrdf = new Document(stream);
                    wrdf.Save(outStream, SaveFormat.Pdf);
                }
            }
            return new MemoryStream(outStream.ToArray());
        }
        /// <summary>
        /// Removes the rich text controls.
        /// </summary>
        /// <param name="doc">The document.</param>
        private static void RemoveRichTextControls(Document doc)
        {
            foreach (StructuredDocumentTag sdt in doc.GetChildNodes(NodeType.StructuredDocumentTag, true).ToArray())
            {
                sdt.RemoveSelfOnly();
            }
        }
        /// <summary>
        /// Remove empty non-tagged paragraph sections (top level)
        /// </summary>
        /// <param name="doc">Aspose.Words document object</param>
        private static void RemoveEmptyNonTaggedParagraphSections(Document doc)
        {
            Body bd = (Body)doc.GetChild(NodeType.Body, 0, true);
            StructuredDocumentTag sdt = (StructuredDocumentTag)bd.GetChild(NodeType.StructuredDocumentTag, 0, true);

            foreach (CompositeNode childNode in sdt.ChildNodes)
            {
                if (childNode.GetType() == typeof(Paragraph))
                {
                    if (Regex.Replace(childNode.ToString(SaveFormat.Text), @"\s", string.Empty).Equals(string.Empty))
                    {
                        childNode.Remove();
                    }
                }
                //else if (childNode.GetType() == typeof(StructuredDocumentTag))
                //{
                //    ((StructuredDocumentTag)childNode).IsShowingPlaceholderText = false;
                //}
            }

        }
        /// <summary>
        /// Get paragraph titles for all sections
        /// </summary>
        /// <param name="doc">The Aspose.Words document.</param>
        /// <returns></returns>
        private static List<string> GetParagraphTitles(Document doc)
        {
            var paragraphTitles = new List<string>();

            Body bd = (Body)doc.GetChild(NodeType.Body, 0, true);
            StructuredDocumentTag sdt = (StructuredDocumentTag)bd.GetChild(NodeType.StructuredDocumentTag, 0, true);

            foreach (CompositeNode childNode in sdt.ChildNodes)
            {
                if (childNode.GetType() == typeof(Paragraph))
                {
                    if (!Regex.Replace(childNode.ToString(SaveFormat.Text), @"\s", string.Empty).Equals(string.Empty))
                    {
                        paragraphTitles.Add(childNode.ToString(SaveFormat.Text));
                    }
                }
            }
            return paragraphTitles;
        }

        /// <summary>
        /// Fix double strikes section caused by incorrect handling of Track Moves in Aspose
        /// </summary>
        /// <param name="doc">The document</param>
        /// <param name="paragraphTitles">Paragraph titles</param>
        private static void FixDoubleStrikesSections(Document doc, List<string> paragraphTitles)
        {
            Body bd = (Body)doc.GetChild(NodeType.Body, 0, true);
            StructuredDocumentTag sdt = (StructuredDocumentTag)bd.GetChild(NodeType.StructuredDocumentTag, 0, true);

            var i = 0;
            foreach (CompositeNode childNode in sdt.ChildNodes)
            {
                if (childNode.GetType() == typeof(Paragraph) &&
                        !Regex.Replace(childNode.ToString(SaveFormat.Text), @"\s", string.Empty).Equals(string.Empty))
                {
                    // Only applies to sections that ends with but different with the original titles
                    if (childNode.GetText().Trim() != paragraphTitles[i].Trim() &&
                            childNode.GetText().Trim().EndsWith(paragraphTitles[i].Trim()))
                    {
                        var previousSibling = childNode.PreviousSibling;
                        var runs = childNode.GetChildNodes(NodeType.Run, true);
                        // Only when the previous section is a structured document tag (content)
                        if (previousSibling.GetType() == typeof(StructuredDocumentTag))
                        {
                            var title = String.Empty;
                            var toBeMoved = new List<Run>();
                            for (var iRun = runs.Count - 1; iRun > -1; iRun--)
                            {
                                var run = (Run)runs[iRun];
                                // 
                                if (title != paragraphTitles[i].Trim())
                                {
                                    title = run.Text + title;
                                }
                                else
                                {
                                    // Add extra contents to list
                                    if (toBeMoved.Count == 0)
                                        run.Text += "\r\n";
                                    toBeMoved.Add(run);
                                }
                            }
                            if (toBeMoved.Count > 0)
                            {
                                // Fix by moving parts from header to the previous section's SDT
                                var previousTag = (StructuredDocumentTag)previousSibling;
                                var contentPara = new Paragraph(sdt.Document);
                                contentPara.ParagraphFormat.SpaceAfter = ((Paragraph)childNode).ParagraphFormat.SpaceAfter;
                                for (var iToBeMoved = 0; iToBeMoved < toBeMoved.Count; iToBeMoved++)
                                {
                                    contentPara.Runs.Insert(0, toBeMoved[iToBeMoved]);
                                }
                                previousTag.AppendChild(contentPara);
                                // Removes tags such as MoveFromRangeStart, MoveFromRangeEnd, BookmarkStart, BookmarkEnd
                                var headerParts = childNode.ChildNodes;
                                foreach (Node headerPart in headerParts)
                                {
                                    if (headerPart.NodeType != NodeType.Run)
                                    {
                                        headerPart.Remove();
                                    }
                                }
                            }
                        }
                    }
                    i++;
                }
            }
        }

        /// <summary>
        /// Removes the empty text sections.
        /// </summary>
        /// <param name="doc">The document.</param>
        private static void RemoveEmptyTextSections(Document doc)
        {
            // Get root structured document tag node
            Body bd = (Body)doc.GetChild(NodeType.Body, 0, true);
            StructuredDocumentTag rootSdt = (StructuredDocumentTag)bd.GetChild(NodeType.StructuredDocumentTag, 0, true);

            foreach (StructuredDocumentTag sdt in rootSdt.GetChildNodes(NodeType.StructuredDocumentTag, false).ToArray())
            {  
                if (!sdt.LockContents && Regex.Replace(sdt.ToString(SaveFormat.Text), @"\s", string.Empty).Equals(string.Empty) )
                {
                    if (sdt.PreviousSibling != null)
                        sdt.PreviousSibling.Remove();
                    sdt.Remove();
                } 
            }

            // Remove the "click here" hint
            FindReplaceOptions options = new FindReplaceOptions()
            {
                MatchCase = false,
                FindWholeWordsOnly = false
            };
            bd.Range.Replace(TemplatePlaceholderText, string.Empty, options);

        }
        /// <summary>
        /// Get previous structured document tag
        /// </summary>
        /// <param name="documentTag">Current structured document tag</param>
        /// <returns></returns>
        private static StructuredDocumentTag GetPreviousStructuredDocumentTag(Paragraph documentTag)
        {
            var cn = (Node)documentTag;
            while (cn.PreviousSibling != null)
            {
                if (cn.PreviousSibling.GetType() == typeof(StructuredDocumentTag))
                {
                    return (StructuredDocumentTag)cn.PreviousSibling;
                }
                cn = cn.PreviousSibling;
            }
            return null;
        }
        #endregion
    }
}
