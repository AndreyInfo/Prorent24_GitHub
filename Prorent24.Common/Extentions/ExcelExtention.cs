using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Excels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Prorent24.Common.Extentions
{
    public static class ExcelExtention
    {
        public static ExcelCustomWorksheet ParceToExcelWorksheet(this MemoryStream memoryStream)
        {
            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                ExcelCustomWorksheet customWorksheet = new ExcelCustomWorksheet();

              

                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;




                for (int row = 1; row <= rowCount; row++)
                {
                    ExcelCustomRow excelRow = new ExcelCustomRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                       var data = worksheet.Cells[row, col];

                        if (data != null)
                        {
                            excelRow.Cells.Add(new Models.Columns.Column()
                            {
                                Order = (short)col,
                                Value = data.Value
                            });
                        }

                        //object[,] values = worksheet.Cells.Value as object[,];

                        //if (values != null && values.Length > 0)
                        //{
                        //    excelRow.Cells.Add(new Models.Columns.Column()
                        //    {
                        //        Order = (short)col,
                        //        Value = values[row, col]
                        //    });
                        //}
                    }

                    if (excelRow.Cells.Count > 0)
                    {
                        customWorksheet.Rows.Add(excelRow);
                    }
                }

                return customWorksheet;
            }
        }

        public static string CreateEmptyExcelWorksheetWithHeader(this List<Prorent24.Common.Models.Columns.Column> columns, string worksheetName, string path)
        {
            Localize localize = new Localize();

            using (ExcelPackage excel = new ExcelPackage())
            {

                excel.Workbook.Worksheets.Add(worksheetName);

                string[] columnArray = new string[columns.Count];

                for (int i = 0; i < columns.Count; i++)
                {
                    var column = columns[i];

                    columnArray[i] = localize[column.Key.ToLower()];

                    if (column.Required)
                    {
                        columnArray[i] = string.Concat(columnArray[i], " *");
                    }

                    if (column.Values != null)
                    {
                        // перевірити тип - enum!!!
                        var values = (column.Values) as IList<string>;
                        columnArray[i] = string.Concat(columnArray[i], $" { localize["values"]} ({string.Join(",", values)})");
                    }
                }

                var headerRow = new List<string[]>()
                {
                    columnArray
                };

                // Determine the header range (e.g. A1:D1)
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets[worksheetName];

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                worksheet.Cells.AutoFitColumns(25, 100);

                path = $"{path}{worksheetName}.xlsx";

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                FileInfo excelFile = new FileInfo(path);
                excel.SaveAs(excelFile);
                return excelFile.FullName;
            }
        }

        public static string CreatExcelWorksheetWithHeader(this List<Prorent24.Common.Models.Columns.Column> columns, List<Dictionary<string, object>> rows, string worksheetName, string path)
        {
            Localize localize = new Localize();

            using (ExcelPackage excel = new ExcelPackage())
            {

                excel.Workbook.Worksheets.Add(worksheetName);

                string[] columnArray = new string[columns.Count];

                for (int i = 0; i < columns.Count; i++)
                {
                    var column = columns[i];

                    columnArray[i] = localize[column.Key.ToLower()];

                    if (column.Required)
                    {
                        columnArray[i] = string.Concat(columnArray[i], " *");
                    }

                    if (column.Values != null)
                    {
                        // перевірити тип - enum!!!
                        var values = (column.Values) as IList<string>;
                        columnArray[i] = string.Concat(columnArray[i], $" { localize["values"]} ({string.Join(",", values)})");
                    }
                }


                List<string[]> bodyArray = new List<string[]>();

                for (int j = 0; j < rows.Count; j++)
                {
                    string[] rowArray = new string[columns.Count];
                    var _row = rows[j].Select(x => new { Key = x.Key.ToLower(), x.Value });
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var column = columns[i];
                        var key = column.Key.ToLower();
                        var val = _row.Where(x => x.Key == key).FirstOrDefault();

                        if (val != null && val.Value != null)
                        {
                            rowArray[i] = val.Value.ToString();
                        }
                        else
                        {
                            rowArray[i] = string.Empty;
                        }

                        //if (rows[j].TryGetValue(key, out object cellValue))
                        //{
                        //    rowArray[i] = cellValue.ToString();
                        //}
                        //else {
                        //    rowArray[i] = string.Empty;
                        //}
                    }
                    bodyArray.Add(rowArray);
                }


                var headerRow = new List<string[]>()
                {
                    columnArray
                };

                // Determine the header range (e.g. A1:D1)
                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

                // Target a worksheet
                var worksheet = excel.Workbook.Worksheets[worksheetName];

                // Popular header row data
                worksheet.Cells[headerRange].LoadFromArrays(headerRow);

                string bodyRange = "A2:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "2";

                worksheet.Cells[bodyRange].LoadFromArrays(bodyArray);


                worksheet.Cells.AutoFitColumns(25, 100);


                path = $"{path}{worksheetName}.xlsx";

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                FileInfo excelFile = new FileInfo(path);
                excel.SaveAs(excelFile);
                return excelFile.FullName;
            }
        }

    }
}
