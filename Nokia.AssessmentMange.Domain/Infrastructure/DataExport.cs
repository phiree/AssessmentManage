using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Globalization;
using System.Text.RegularExpressions;

using NPOI.SS.UserModel;
using NPOI;
using NPOI.XSSF.UserModel;
namespace Nokia.AssessmentMange.Domain.Infrastructure
{
    /// <summary>
    /// 数据导成excel文件
    /// </summary>
    public class DataExport
    {

        /// </summary>
        public int HeaderRows { get; set; }
        /// <summary>
        /// 目标Excel文件
        /// </summary>
        public string XSLFilePath { private get; set; }
        /// <summary>
        /// 需要導出的數據
        /// </summary>

        public XSSFWorkbook XSSFBook { get; set; }
        //excel内存对象
        public HSSFWorkbook Book { get; set; }
        private bool NeedInsertImage { get; set; }
        public DataSet DataToExport { get; set; }
        public DataExport()
        {
            HeaderRows = 1;
        }
        public DataExport(DataSet ds)
            : this(ds, 1)
        {

        }
        public DataExport(DataSet ds, int headerRows) : this(ds, headerRows, true)
        {

        }
        public DataExport(DataSet ds, int headerRows, bool needInsertImage)
        {
            if (headerRows < 0)
                throw new Exception("dataStartRowNumber必须大于等于0");
            HeaderRows = headerRows;
            DataToExport = ds;
            NeedInsertImage = needInsertImage;
        }
        IDrawing patriarch;
        public void CreateWorkBook()
        {
            if (string.IsNullOrEmpty(XSLFilePath))
            {
                Book = new HSSFWorkbook();
            }
            else
            {
                Book = new HSSFWorkbook(new FileStream(XSLFilePath, FileMode.OpenOrCreate));
            }
            for (int i = 0; i < DataToExport.Tables.Count; i++)
            {
                FillSheet(i, DataToExport.Tables[i]);
            }
            //   FillSheet(0);
        }

        public void CreateXSSFWorkBook()
        {
            if (string.IsNullOrEmpty(XSLFilePath))
            {
                XSSFBook = new XSSFWorkbook();
            }
            else
            {
                FileStream fsOut = new FileStream(XSLFilePath, FileMode.OpenOrCreate, FileAccess.Read);
                XSSFBook = new XSSFWorkbook(fsOut);

                fsOut.Close();

            }
            for (int i = 0; i < DataToExport.Tables.Count; i++)
            {
                FillXSSFSheet(i, DataToExport.Tables[i]);
            }
            //   FillSheet(0);
        }

        private void FillSheet(int tableIndex, DataTable dataToFill)
        {


            if (string.IsNullOrEmpty(dataToFill.TableName))
            {
                dataToFill.TableName = "table" + tableIndex;
            }
            ISheet sheet;
            if (tableIndex < Book.NumberOfSheets)
            {
                sheet = Book.GetSheetAt(tableIndex);
            }
            else
            {
                sheet = Book.CreateSheet(dataToFill.TableName);
            }
            patriarch = sheet.CreateDrawingPatriarch();
            DataColumnCollection cols = dataToFill.Columns;
            //创建表头
            for (int h = 0; h <= HeaderRows; h++)
            {
                IRow headrow = sheet.CreateRow(h);
                if (h == HeaderRows)
                {
                    CreateCellForRow(headrow, cols, null, true);
                }
                else
                {
                    CreateCellForRow(headrow, cols, null, false);
                }
            }
            //填充内容
            for (int i = 0; i < dataToFill.Rows.Count; i++)
            {
                var dataRow = dataToFill.Rows[i];
                var excelRow = sheet.CreateRow(HeaderRows + 1 + i);
                CreateCellForRow(excelRow, cols, dataRow, false);
            }
        }

        private void FillXSSFSheet(int tableIndex, DataTable dataToFill)
        {


            if (string.IsNullOrEmpty(dataToFill.TableName))
            {
                dataToFill.TableName = "table" + tableIndex;
            }
            ISheet sheet;

            sheet = XSSFBook.GetSheet("Sheet1");

            patriarch = sheet.CreateDrawingPatriarch();
            DataColumnCollection cols = dataToFill.Columns;
            //创建表头
            //for (int h = 0; h <= HeaderRows; h++)
            //{
            //    IRow headrow = sheet.CreateRow(h);
            //    if (h == HeaderRows)
            //    {
            //        CreateCellForRow(headrow, cols, null, true);
            //    }
            //    else
            //    {
            //        CreateCellForRow(headrow, cols, null, false);
            //    }
            //}
            //填充内容
            for (int i = 0; i < dataToFill.Rows.Count; i++)
            {
                var dataRow = dataToFill.Rows[i];
                //var excelRow = sheet.CreateRow(HeaderRows + 6 + i);
                //CreateCellForRow(excelRow, cols, dataRow, false);
                GetCellForRow(sheet, HeaderRows + 6 + i, cols, dataRow, false);
            }
        }

        /// <summary>
        /// 根据datatable
        /// </summary>
        /// <param name="dataStartRowNumber">Excel文件顶部可能需要写入其他数据</param>
        /// <param name="dt"></param>
        /// <param name="xslTemplate"></param>
        /// <param name="saveNtsNumber"></param>
        /// <param name="savePath">保存位置</param>
        public void SaveWorkBook(string savePath)
        {
            CreateWorkBook();

            FileStream fsOut = new FileStream(savePath, FileMode.Create);
            Book.Write(fsOut);
            fsOut.Close();
        }
        /// <summary>
        /// 根据datatable
        /// </summary>
        /// <param name="dataStartRowNumber">Excel文件顶部可能需要写入其他数据</param>
        /// <param name="dt"></param>
        /// <param name="xslTemplate"></param>
        /// <param name="saveNtsNumber"></param>
        /// <param name="savePath">保存位置</param>
        public void SaveWorkBookbyExport(string savePath)
        {
            XSLFilePath = savePath;
            CreateXSSFWorkBook();


            //FileStream fsOut = new FileStream(savePath, FileMode.Create);
            //Book.Write(fsOut);
            //fsOut.Close();
        }
        /// <summary>
        /// 根据datarow 创建 cells. 
        /// </summary>
        /// <param name="excelRow"></param>
        /// <param name="columns"></param>
        /// <param name="row">如果为null 则该行所有cell的值为空</param>
        /// <param name="isHead">如果是true 则创建表头.</param>
        private void CreateCellForRow(IRow excelRow, DataColumnCollection columns, DataRow row, bool isHead)
        {


            for (int i = 0; i < columns.Count; i++)
            {
                var cell = excelRow.CreateCell(i);
                if (isHead)
                {
                    cell.SetCellValue(columns[i].ColumnName);
                }
                else
                {
                    string cellValue = string.Empty;
                    if (row != null) { cellValue = row[i].ToString(); }

                    {
                        cell.SetCellValue(cellValue);
                    }
                }
            }
        }
        /// <summary>
        /// 根据datarow 修改 cells. 
        /// </summary>
        /// <param name="excelRow"></param>
        /// <param name="columns"></param>
        /// <param name="row">如果为null 则该行所有cell的值为空</param>
        /// <param name="isHead">如果是true 则创建表头.</param>
        private void GetCellForRow(ISheet sheet, int rownum, DataColumnCollection columns, DataRow row, bool isHead)
        {


            for (int j = 0; j < columns.Count; j++)
            {


                string cellValue = string.Empty;
                if (row != null) { cellValue = row[j].ToString(); }
                //int rowCount = sheet.GetRow(rownum).LastCellNum;

                if (sheet.GetRow(rownum).GetCell(j) == null)
                {
                    var cell = sheet.GetRow(rownum).CreateCell(j);
                    cell.SetCellValue(cellValue);
                }
                else
                {
                    sheet.GetRow(rownum).GetCell(j).SetCellValue(cellValue);
                }




            }
        }
        //private void InsertImageToCell(MemoryStream imageStream, int left, int top)
        //{

        //    //store the coordinates of which cell and where in the cell the image goes
        //    HSSFClientAnchor anchor = new HSSFClientAnchor(1, 1, 0, 0, left, top, left + 1, top + 1);
        //    //types are 0, 2, and 3. 0 resizes within the cell, 2 doesn't
        //    anchor.AnchorType = 2;
        //    //add the byte array and encode it for the excel file
        //    int index = Book.AddPicture(imageStream.ToArray(), PictureType.PNG);
        //    IPicture signaturePicture = patriarch.CreatePicture(anchor, index);
        //}
        /// <summary>
        /// 根据地址读取excel的内容
        /// </summary>
        /// <param name="dataStartRowNumber">Excel文件顶部可能需要写入其他数据</param>
        /// <param name="dt"></param>
        /// <param name="xslTemplate"></param>
        /// <param name="saveNtsNumber"></param>
        /// <param name="savePath">保存位置</param>
        public DataSet InsterWorkBookbyExport(Stream s)
        {

            //FileStream fsOut = new FileStream(XSLFilePath, FileMode.OpenOrCreate, FileAccess.Read);
            Book = new HSSFWorkbook(s);
            HSSFSheet sheet = null;
            DataSet ds = null;
            for (int i = 0; i < Book.NumberOfSheets; i++)
            {
                ds = new DataSet();
                sheet = (HSSFSheet)Book.GetSheetAt(i);
                ds.DataSetName = sheet.SheetName;
                DataTable DT = ExcelToDataTableForFlightVip(sheet, 1, 7);
                ds.Tables.Add(DT);
            }
            return ds;
        }
        public DataTable ExcelToDataTableForFlightVip(HSSFSheet Sheet, int RowIndex, int ColIndex)
        {
            DataTable dt = new DataTable("tablename");
            if (Sheet == null) return dt;

            //最后一列的标号  即总的行数
            int rowCount = Sheet.LastRowNum;
            DataRow dr = null;
            HSSFRow row = null;
            string cellvalue = string.Empty;
            for (int j = 0; j < ColIndex; j++)
            {
                dt.Columns.Add(new DataColumn("columns" + j, Type.GetType("System.String")));//
            }
            for (int i = RowIndex; i < rowCount + 1; i++)
            {
                row = (HSSFRow)Sheet.GetRow(i);
                if (row != null)
                {

                    dr = dt.NewRow();
                    for (int k = 0; k < ColIndex; k++)
                    {
                        dr[k] = row.GetCell(k);
                    }
                    dt.Rows.Add(dr);

                }
            }
            return dt;

        }
    }
}
