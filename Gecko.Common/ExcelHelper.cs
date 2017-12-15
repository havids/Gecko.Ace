using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;

namespace Gecko.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// 类版本
        /// </summary>
        public string version
        {
            get { return "0.1"; }
        }
        readonly int EXCEL03_MaxRow = 65535;

        /// <summary>
        /// 将DataTable转换为excel2003格式。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public byte[] DataTable2Excel(DataTable dt, string sheetName)
        {

            IWorkbook book = new HSSFWorkbook();
            if (dt.Rows.Count < EXCEL03_MaxRow)
                DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
            else
            {
                int page = dt.Rows.Count / EXCEL03_MaxRow;
                for (int i = 0; i < page; i++)
                {
                    int start = i * EXCEL03_MaxRow;
                    int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                    DataWrite2Sheet(dt, start, end, book, sheetName + i.ToString());
                }
                int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName + page.ToString());
            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// 将DataSet转换为excel2003格式。
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public byte[] DataSet2Excel(DataSet ds, string sheetName)
        {
            IWorkbook book = new HSSFWorkbook();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                DataWrite2Sheet(ds.Tables[0], 0, ds.Tables[0].Rows.Count - 1, book, sheetName + ds.Tables[i].TableName);
            }

            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            return ms.ToArray();
        }

        public void AddDataTable(HSSFWorkbook book, DataTable dt, string sheetName)
        {
            DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
        }


        private void DataWrite2Sheet(DataTable dt, int startRow, int endRow, IWorkbook book, string sheetName)
        {
            ISheet sheet = book.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                string val = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                cell.SetCellValue(val);
            }
            int rowIndex = 1;
            for (int i = startRow; i <= endRow; i++)
            {
                DataRow dtRow = dt.Rows[i];
                IRow excelRow = sheet.CreateRow(rowIndex++);
                for (int j = 0; j < dtRow.ItemArray.Length; j++)
                {
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                }
            }

        }


        /// <summary>
        /// 从 excel 读取数据 为datatable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExcelData(Stream s, int startRow)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook workbook = new HSSFWorkbook(s);
            HSSFSheet sheet = workbook.GetSheetAt(0) as HSSFSheet;

            int firstRowIndex = startRow - 1;

            //获取sheet的首行
            HSSFRow headerRow = sheet.GetRow(firstRowIndex) as HSSFRow;

            while (headerRow == null)
            {
                firstRowIndex += 1;
                headerRow = sheet.GetRow(firstRowIndex) as HSSFRow;
            }

            //获取列数
            int cellCount = headerRow.PhysicalNumberOfCells;
            //获取行数
            int rowCount = sheet.PhysicalNumberOfRows;

            for (int i = firstRowIndex; i < startRow; i++)
            {
                var row = sheet.GetRow(i) as HSSFRow;
                if (row != null) rowCount ++;
            }



            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                string colName = headerRow.GetCell(i) != null ?  headerRow.GetCell(i).StringCellValue : "col" + i.ToString();
                DataColumn column = new DataColumn(colName);
                dt.Columns.Add(column);
            }



            ///从标题行+1开始循环
            for (int i = startRow; i < rowCount; i++)
            {
                HSSFRow row = sheet.GetRow(i) as HSSFRow;
                DataRow dr = dt.NewRow();

                if (row == null)
                    continue;

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    ICell cell = row.GetCell(j);
                    if (cell == null)
                    {
                        dr[j] = null;
                    }
                    else
                    {
                        //读取Excel格式，根据格式读取数据类型
                        switch (cell.CellType)
                        {
                            case CellType.BLANK: //空数据类型处理
                                dr[j] = "";
                                break;
                            case CellType.STRING: //字符串类型
                                dr[j] = cell.StringCellValue.Trim();
                                break;
                            case CellType.NUMERIC: //数字类型      
                                //判断格式是否为日期 
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dr[j] = cell.DateCellValue;
                                }
                                else
                                {
                                    dr[j] = cell.NumericCellValue;
                                }
                                break;
                            case CellType.FORMULA:
                                HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                dr[j] = e.Evaluate(cell).StringValue;
                                break;
                            default:
                                dr[j] = "";
                                break;
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            workbook = null;
            sheet = null;

            return dt;
        }



        /// <summary>
        /// 从 excel 读取数据 为datatable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetExcelData(Stream s)
        {
            return GetExcelData(s, 1);
        }



        public static void ReportToExcel(System.Web.UI.Control ctl, string fileName)
        {
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            ctl.RenderControl(htw);
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
    }
}


