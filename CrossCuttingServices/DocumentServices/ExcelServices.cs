using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;

namespace Sra.P2rmis.CrossCuttingServices.OpenXmlServices
{
    /// <summary>
    /// OpenXml services for excel
    /// </summary>
    public class ExcelServices
    {
                /// <summary>
        /// The default sheet name
        /// </summary>
        public const string DefaultSheetName = "Sheet1";
        /// <summary>
        /// Creates an excel document from a data table.
        /// </summary>
        /// <param name="table">The data table.</param>
        /// <returns>Excel document</returns>
        /// <remarks>Not used currently, potential future use</remarks>
        public static byte[] CreateExcel(DataTable table)
        {
            byte[] excelBytes;
            using (MemoryStream mem = new MemoryStream())
            {
                using (SpreadsheetDocument x1 = SpreadsheetDocument.Create(mem, SpreadsheetDocumentType.Workbook))
                {
                    // Create SpreadsheetDocument 
                    WorkbookPart wbp = x1.AddWorkbookPart();
                    var wsp = wbp.AddNewPart<WorksheetPart>();
                    var wbsp = wbp.AddNewPart<WorkbookStylesPart>();
                    wbsp.Stylesheet = new Stylesheet
                    {
                        Fonts = new Fonts(new Font()),
                        Fills = new Fills(new Fill()),
                        Borders = new Borders(new Border()),
                        CellStyleFormats = new CellStyleFormats(new CellFormat()),
                        CellFormats =
                            new CellFormats(
                                new CellFormat(),
                                new CellFormat
                                {
                                    NumberFormatId = 22,
                                    ApplyNumberFormat = true
                                })
                    };
                    wbsp.Stylesheet.Save();

                    var ws = new Worksheet();
                    var sd = new SheetData();
                    Columns columns = new Columns();
                    columns.Append(new Column() { Min = 1, Max = 2, Width = 18, CustomWidth = true });
                    ws.Append(columns);

                    // Add header to sheetData 
                    Row headerRow = new Row();
                    List<KeyValuePair<String, Type>> dataColumns = new List<KeyValuePair<string, Type>>();
                    foreach (DataColumn column in table.Columns)
                    {
                        dataColumns.Add(new KeyValuePair<string, Type>(column.ColumnName, column.DataType));
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ColumnName);
                        headerRow.AppendChild(cell);
                    }
                    sd.AppendChild(headerRow);

                    // Add cells to sheetData 
                    foreach (DataRow row in table.Rows)
                    {
                        Row newRow = new Row();
                        dataColumns.ForEach(col =>
                        {
                            Cell cell = new Cell();
                            //If value is DBNull, do not set value to cell 
                            if (row[col.Key] != System.DBNull.Value)
                            {
                                if (col.Value == typeof(DateTime))
                                {
                                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
                                    cell.StyleIndex = 1;
                                    DateTime dtValue = (DateTime)(row[col.Key]);
                                    string strValue = dtValue.ToOADate().ToString(CultureInfo.InvariantCulture);
                                    cell.CellValue = new CellValue(strValue);
                                }
                                else
                                {
                                    cell.DataType = CellValues.String;
                                    cell.CellValue = new CellValue(row[col.Key].ToString());
                                }
                            }
                            newRow.AppendChild(cell);
                        });
                        sd.AppendChild(newRow);
                    }

                    ws.Append(sd);
                    wsp.Worksheet = ws;
                    wsp.Worksheet.Save();

                    var wb = new Workbook();
                    Sheets sheets = wb.AppendChild<Sheets>(new Sheets());
                    string relationshipId = wbp.GetIdOfPart(wsp);
                    var sheetName = String.IsNullOrEmpty(table.TableName) ? DefaultSheetName : table.TableName;
                    Sheet sheet = new Sheet() { Id = relationshipId, SheetId = 1, Name = sheetName };
                    sheets.Append(sheet);

                    wbp.Workbook = wb;
                    wbp.Workbook.Save();

                    x1.Close();
                }
                excelBytes = mem.ToArray();
            }
            return excelBytes;
        }
        /// <summary>
        /// Gets the excel data.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="sheetIndex">Index of the sheet.</param>
        /// <param name="hasHeader">if set to <c>true</c> [has header].</param>
        /// <param name="maxColumns">The maximum columns.</param>
        /// <returns></returns>
        public static DataTable GetExcelData(Stream stream, int sheetIndex, bool hasHeader, int maxColumns)
        {
            var table = new DataTable(); ;
            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(stream, false))
            {
                if (maxColumns > 24)
                {
                    if (hasHeader)
                    {
                        int count = 65;

                        for (var i = 65; i < 65 + maxColumns; i++)
                        {
                            var doubleLetter = ((char)i);
                            if (doubleLetter > 90)
                            {
                                var newCell = ((char)count).ToString();
                                var columnName = GetCellValue(document, sheetIndex, ("A" + newCell.ToString() + 1.ToString()));
                                while (table.Columns.Contains(columnName))
                                {
                                    columnName += " ";
                                }
                                table.Columns.Add(columnName, typeof(string));
                                count++;
                            }
                            else
                            {
                                var columnName = GetCellValue(document, sheetIndex, ((char)i).ToString() + 1.ToString());
                                while (table.Columns.Contains(columnName))
                                {
                                    columnName += " ";
                                }
                                table.Columns.Add(columnName, typeof(string));
                            }
                        }
                    }
                    var hasMoreData = true;
                    var iDataRow = hasHeader ? 2 : 1;
                    while (hasMoreData)
                    {
                        ArrayList list = new ArrayList();
                        var isEmpty = true;
                        int count = 65;
                        for (var i = 65; i < 65 + maxColumns; i++)
                        {
                            var doubleLetter = ((char)i);
                            if (doubleLetter > 90)
                            {
                                var newCell = ((char)count).ToString();
                                var rowData = GetCellValue(document, sheetIndex, ("A" + newCell.ToString() + iDataRow.ToString()));
                                if (!String.IsNullOrEmpty(rowData))
                                    isEmpty = false;

                                list.Add(rowData);
                                count++;
                            }
                            else
                            {
                                var rowData = GetCellValue(document, sheetIndex, ((char)i).ToString() + iDataRow.ToString());
                                if (!String.IsNullOrEmpty(rowData))
                                    isEmpty = false;

                                list.Add(rowData);
                            }

                        }
                        iDataRow++;
                        if (!isEmpty)
                            table.Rows.Add(list.ToArray());
                        else
                            hasMoreData = false;
                    }
                }
                else
                {
                    if (hasHeader)
                    {
                        for (var i = 65; i < 65 + maxColumns; i++)
                        {
                            var columnName = GetCellValue(document, sheetIndex, ((char)i).ToString() + 1.ToString());
                            if (columnName != null)
                            {
                                while (table.Columns.Contains(columnName))
                                {
                                    columnName += " ";
                                }
                                table.Columns.Add(columnName, typeof(string));
                            }
                        }
                    }
                    var hasMoreData = true;
                    var iDataRow = hasHeader ? 2 : 1;
                    while (hasMoreData)
                    {
                        ArrayList list = new ArrayList();
                        var isEmpty = true;
                        for (var i = 65; i < 65 + table.Columns.Count; i++)
                        {
                            var rowData = GetCellValue(document, sheetIndex, ((char)i).ToString() + iDataRow.ToString());
                            if (!String.IsNullOrEmpty(rowData))
                                isEmpty = false;
                            list.Add(rowData);
                        }
                        iDataRow++;
                        if (!isEmpty)
                            table.Rows.Add(list.ToArray());
                        else
                            hasMoreData = false;
                    }
                }
            }
            return table;
        }
        /// <summary>
        /// Gets the cell value.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="sheetIndex">Index of the sheet.</param>
        /// <param name="addressName">Name of the address.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">sheetName</exception>
        public static string GetCellValue(SpreadsheetDocument document, int sheetIndex, string addressName)
        {
            string value = null;

            // Retrieve a reference to the workbook part.
            WorkbookPart wbPart = document.WorkbookPart;

            // Find the sheet with the supplied name, and then use that 
            // Sheet object to retrieve a reference to the first worksheet.
            Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().ToList()[sheetIndex];

            // Throw an exception if there is no sheet.
            if (theSheet == null)
            {
                throw new ArgumentException();
            }

            // Retrieve a reference to the worksheet part.
            WorksheetPart wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            // Use its Worksheet property to get a reference to the cell 
            // whose address matches the address you supplied.
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().
                Where(c => c.CellReference == addressName).FirstOrDefault();

            // If the cell does not exist, return an empty string.
            if (theCell != null)
            {
                value = theCell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            // For shared strings, look up the value in the
                            // shared strings table.
                            var stringTable =
                                wbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            // If the shared string table is missing, something 
                            // is wrong. Return the index that is in
                            // the cell. Otherwise, look up the correct text in 
                            // the table.
                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;
                    }
                }
            }
            return value;
        }
        /// <summary>
        /// Sets the rows.
        /// </summary>
        /// <param name="table">The data table from excel file.</param>
        /// <param name="maximumCount">Maximum amount of columns needed</param>
        /// <returns></returns>
        public static List<List<string>> SetRows(DataTable table, int maximumCount)
        {
            var finalFile = new List<List<string>>();
            foreach (DataRow row in table.Rows)
            {
                int i = 0;
                List<string> excelTotal = new List<string>();
                for (i = 0; i < maximumCount; i++)
                {
                    excelTotal.Add(row[i].ToString());
                }
                finalFile.Add(excelTotal);
            }
            return finalFile;
        }
        /// <summary>
        /// Sets the rows.
        /// </summary>
        /// <param name="table">The data table from excel file.</param>
        /// <param name="maximumCount">Maximum amount of columns needed</param>
        /// <returns></returns>
        public static List<List<string>> SetMMRows(DataTable table, int maximumCount)
        {
            var finalFile = new List<List<string>>();
            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                List<string> excelTotal = new List<string>();
                for (i = 0; i < maximumCount; i++)
                {
                    if (i == 21 || i == 26)
                    {
                        var getDate = Convert.ToInt64(row[i]);
                        DateTime conv = DateTime.FromOADate(getDate);
                        var convDate = (conv.ToString("MM/dd/yyyy"));
                        excelTotal.Add(convDate.ToString());
                    }
                    if (i == 35)
                    {
                        decimal getFare = Convert.ToDecimal(row[i]);
                        excelTotal.Add(getFare.ToString());
                    }
                    if (i == 0 || i == 2 || i == 3 || i == 6 || i == 14 || i == 15 || i == 18 || i == 22 || i == 23 || i == 27 )
                    {
                        excelTotal.Add(row[i].ToString());
                    }
                }
                finalFile.Add(excelTotal);
            }
            return finalFile;
        }

		/// <summary>
        /// Sets the rm rows.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="maximumCount">The maximum count.</param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> SetRMRows(DataTable table, int maximumCount)
        {
            var finalFile = new List<KeyValuePair<string, string>>();
            foreach (DataRow row in table.Rows)
            {
                    KeyValuePair<string, string> excelTotal = new KeyValuePair<string, string>(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
                    finalFile.Add(excelTotal);
                
                
            }
            return finalFile;
        }
    }
}
