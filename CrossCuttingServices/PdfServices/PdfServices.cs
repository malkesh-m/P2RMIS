using SelectPdf;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using System.IO;
using System.Web;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf.Streaming;
using Telerik.Windows.Documents.Flow.FormatProviders.Html;
using Telerik.Windows.Documents.Flow.FormatProviders.Rtf;
using Telerik.Windows.Documents.Flow.FormatProviders.Txt;
using Telerik.Windows.Documents.Flow.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// The PdfServices class provides PDF creation.
    /// </summary>
    public static class PdfServices
    {
        private static string EmbeddedViewerFilesPhysicalDrivePath = $"{HttpRuntime.AppDomainAppPath}Content\\PdfViewerFiles";
        /// <summary>
        /// Create PDF in binary format
        /// </summary>
        /// <param name="htmlContent">The HTML content to be  converted</param>
        /// <param name="footerText">The text in the footer</param>
        /// <param name="baseUrl">The base URL to prepend file references</param>
        /// <param name="depPath">The path of Select.Html.dep</param>
        /// <returns>The PDF in binary format</returns>
        public static byte[] CreatePdf(string htmlContent, string footerText, string baseUrl, string depPath)
        {
            // Define the .dep path
            SelectPdf.GlobalProperties.HtmlEngineFullPath = depPath;
            // Instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();
            // Configure margins
            converter.Options.PdfPageSize = PdfPageSize.Letter;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 36;
            converter.Options.MarginRight = 36;
            converter.Options.MarginTop = 36;
            converter.Options.MarginBottom = 36;
            converter.Options.WebPageWidth = 800;
            // Add footer
            converter.Options.DisplayFooter = true;
            converter.Footer.Height = 20;
            PdfTextSection footer = new PdfTextSection(0, 10, footerText,
                    new System.Drawing.Font("Verdana", 8));
            footer.HorizontalAlign = PdfTextHorizontalAlign.Right;
            converter.Footer.Add(footer);
            // Create a new pdf document converting the html code
            PdfDocument doc = converter.ConvertHtmlString(htmlContent, baseUrl);
            byte[] returnFile = doc.Save();
            return returnFile;
        }

        /// <summary>
        /// Merges 2 pdf files to each other and returns merged file as byte array
        /// </summary>
        /// <param name="file1">Stream representing the 1st pdf file of the merge</param>
        /// <param name="file2">Stream representing the 2nd pdf file of the merge</param>
        /// <returns></returns>
        public static byte[] MergePdf(byte[] file1, byte[] file2)
        {
            MemoryStream ms = new MemoryStream();
            Stream fileStream1 = new MemoryStream(file1);
            Stream fileStream2 = new MemoryStream(file2);
            using (PdfStreamWriter fileWriter = new PdfStreamWriter(ms, true))
            {
                using (PdfFileSource fileSource = new PdfFileSource(fileStream1))
                {
                    foreach (PdfPageSource pageSource in fileSource.Pages)
                    {
                        fileWriter.WritePage(pageSource);
                    }
                }
                using (PdfFileSource fileSource = new PdfFileSource(fileStream2))
                {
                    foreach (PdfPageSource pageSource in fileSource.Pages)
                    {
                        fileWriter.WritePage(pageSource);
                    }
                }
            }

            return ms.ToArray();
        }
        /// <summary>
        /// Convert file to PDF format
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Stream ConvertToPdf(byte[] file, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName).Replace(".", "").ToLower();
            Stream inputStream = null;
           
            if (fileExtension == FileConstants.FileExtensions.Pdf)
            {
                inputStream = new MemoryStream(file);
            }
            else if (fileExtension == FileConstants.FileExtensions.Xlsx)
            {
                var xlsxFormatProvider = new XlsxFormatProvider();
                var workbook = xlsxFormatProvider.Import(file);
                foreach (var worksheet in workbook.Worksheets)
                {
                    worksheet.WorksheetPageSetup.FitToPages = true;
                    worksheet.WorksheetPageSetup.FitToPagesTall = 100;
                    worksheet.WorksheetPageSetup.PageOrder = Telerik.Windows.Documents.Spreadsheet.Model.Printing.PageOrder.OverThenDown;
                    worksheet.WorksheetPageSetup.PageOrientation = Telerik.Windows.Documents.Model.PageOrientation.Landscape;
                }
                inputStream = ExportToPdf(workbook);
            }
            else if (fileExtension == FileConstants.FileExtensions.Docx)
            {
                inputStream = WordServices.ConvertWordToPDF(file);                
            }
            else
            {
                var radFlowDocument = new RadFlowDocument();
                switch (fileExtension)
                {                    
                    case FileConstants.FileExtensions.Html:
                        var htmlFormatProvider = new HtmlFormatProvider();
                        radFlowDocument = htmlFormatProvider.Import(inputStream);
                        break;
                    case FileConstants.FileExtensions.Rtf:
                        var rtfFormatProvider = new RtfFormatProvider();
                        radFlowDocument = rtfFormatProvider.Import(inputStream);
                        break;
                    case FileConstants.FileExtensions.Txt:
                        var txtFormatProvider = new TxtFormatProvider();
                        radFlowDocument = txtFormatProvider.Import(inputStream);
                        break;
                }
                inputStream = ExportToPdf(radFlowDocument);

            }
            return inputStream;
        }
        /// <summary>
        /// convert workbook to pdf format
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        private static Stream ExportToPdf(Workbook workbook)
        {
            MemoryStream outStream = new MemoryStream();
            try
            {
                var pdfFormatProvider = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.PdfFormatProvider();                
                var pdfExportSettings = new Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.Export.PdfExportSettings(
                    Telerik.Windows.Documents.Spreadsheet.FormatProviders.ExportWhat.EntireWorkbook, false
                    );
                pdfFormatProvider.ExportSettings = pdfExportSettings;
                pdfFormatProvider.Export(workbook, outStream);
                return new MemoryStream(outStream.ToArray());
            }
            finally
            {
                outStream.Dispose();
            }
        }
        /// <summary>
        /// Export a Telerik RadFlowDocument to PDF
        /// </summary>
        /// <param name="radFlowDocument"></param>
        private static Stream ExportToPdf(RadFlowDocument radFlowDocument)
        {
            MemoryStream outStream = new MemoryStream();
            try
            {
                var pdfFormatProvider = new Telerik.Windows.Documents.Flow.FormatProviders.Pdf.PdfFormatProvider();
                pdfFormatProvider.Export(radFlowDocument, outStream);
                return new MemoryStream(outStream.ToArray());
            }
            finally
            {
                outStream.Dispose();
            }
        }
    }
}
