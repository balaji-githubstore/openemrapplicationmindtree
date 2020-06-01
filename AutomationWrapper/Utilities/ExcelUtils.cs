using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutomationWrapper.Utilities
{
    public class ExcelUtils
    {
        public static object[] GetSheetIntoObject(string fileDetails, string sheetName)
        {
            Excel.Application xlApp = null;
            Excel.Workbook xlBook = null;
            object[] main = null;
            try
            {
                xlApp = new Excel.Application();
                xlBook = xlApp.Workbooks.Open(fileDetails);
                Excel.Worksheet xlSheet = xlBook.Worksheets[sheetName];
                Excel.Range xlRange = xlSheet.UsedRange;
                int rowCount = xlRange.Rows.Count;
                int ColCount = xlRange.Columns.Count;
                main = new object[rowCount - 1];
                for (int i = 2; i <= rowCount; i++)
                {
                    object[] temp = new object[ColCount];
                    for (int j = 1; j <= ColCount; j++)
                    {
                        string value =Convert.ToString(xlRange.Cells[i, j].value);
                        Console.WriteLine(value);
                        temp[j - 1] = value;
                    }
                    main[i - 2] = temp;
                }
            }
            finally
            {
                //run always

                xlBook.Close();
                xlApp.Quit();
            }

            return main;
        }
    }
}
