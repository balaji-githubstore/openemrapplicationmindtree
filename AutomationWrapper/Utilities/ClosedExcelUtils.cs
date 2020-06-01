using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationWrapper.Utilities
{
    public class ClosedExcelUtils
    {
        public static object[] GetSheetIntoObjectUsingClosedXML(string fileDetails, string sheetName)
        {
            object[] main = null;
            using (XLWorkbook book = new XLWorkbook(fileDetails))
            {
                IXLWorksheet xlSheet = book.Worksheet(sheetName);

                IXLRange xlRange = xlSheet.RangeUsed();

                int rowCount = xlRange.RowCount();

                int ColCount = xlRange.ColumnCount();

                Console.WriteLine(rowCount);
                Console.WriteLine(ColCount);

                main = new object[rowCount - 1]; //number of rows (Test case)

                for (int i = 2; i <= rowCount; i++)
                {
                    object[] temp = new object[ColCount]; //number of parameter
                    for (int j = 1; j <= ColCount; j++)
                    {
                        string value = xlRange.Cell(i, j).GetString();
                        Console.WriteLine(value);
                        temp[j - 1] = value;
                    }
                    main[i - 2] = temp;
                }

                book.Dispose();
            }
            return main;
        }
    }
}
