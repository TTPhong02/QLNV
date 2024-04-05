using MISA.AMISDemo.Core.Interfaces.Excels;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Services.Excels
{
    public class ExportExcelService<T> : IExportExcelService<T> where T : class
    {
        public  byte[] ExportExcel(IEnumerable<T> data)
        {
            //danh sách properties
            var properties = typeof(T).GetProperties();

            //danh sách các cột export vào excel
            var columns = new List<string>();

            foreach (var property in properties)
            {
                var displayNameAttribute = property.Name;
                columns.Add(displayNameAttribute);
            }

            var stream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(stream))
            {
                // đặt tên người tạo file
                excelPackage.Workbook.Properties.Author = "TTPhong";

                //Tạo một sheet để làm việc trên đó
                var ws = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // set style mặc định cho toàn bộ file
                ws.Cells.Style.Font.Size = 14;
                ws.Cells.Style.Font.Name = "Times New Roman";

                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = columns.Count();

                // gán giá trị cho cell vừa merge
                ws.Cells[1, 1].Value = "Danh sách nhân viên";
                // merge các column lại từ column 1 đến số column header và set style
                ws.Cells[1, 1, 2, countColHeader + 1].Merge = true;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.Font.Bold = true;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.Font.Size = 24;
                ws.Cells[1, 1, 2, countColHeader + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerCol = 2;
                var headerRow = 3;
                ws.Cells[headerRow, 1].Value = "STT";

                //tạo các header từ column header đã tạo từ bên trên
                foreach (var item in columns)
                {
                    //gán giá trị
                    ws.Cells[headerRow, headerCol].Value = item;
                    headerCol++;
                }

                //set style cho header
                var cells = ws.Cells[headerRow, 1, headerRow, countColHeader + 1];
                //set màu thành gray
                var fill = cells.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                //căn chỉnh các border
                var border = cells.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = ExcelBorderStyle.Thin;


                var currentRow = 4;
                var colSTT = 1;
                //gán giá trị cho từng dòng và cột
                foreach (var item in data)
                {
                    //var itemDto = mapDto(item);
                    ws.Cells[currentRow, 1].Value = colSTT;
                    var currentCol = 2;

                    foreach (var property in properties)
                    {
                        var propValue = property.GetValue(item);
                        if (propValue != null && property.PropertyType == typeof(DateTime?))
                        {
                            propValue = String.Format("{0:dd/MM/yyyy}", propValue); ;
                        }
                        //gán giá trị cho từng cell                      
                        ws.Cells[currentRow, currentCol].Value = propValue;
                        currentCol++;
                    }

                    //set style cho từng ô giá trị
                    ws.Cells[currentRow, 1, currentRow, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    ws.Cells.AutoFitColumns();
                    ws.Rows[currentRow].Height = 20;

                    colSTT++;
                    currentRow++;
                }

                excelPackage.Save();
                //chuyển đổi nội dung của đối tượng stream thành một mảng byte.
                var bytes = stream.ToArray();
                return bytes;
            }
        }
    }
}
