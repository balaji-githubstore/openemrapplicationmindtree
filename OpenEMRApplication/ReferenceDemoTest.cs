using ClosedXML.Excel;
using NUnit.Framework;
using AutomationWrapper.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace OpenEMRApplication
{
    class ReferenceDemoTest
    {

        [Test]
        public void TestDB()
        {
            DataTable dt = DBUtils.SelectQueryToDatatable("SELECT * FROM [dbo].[tbl_magento_user]");

            foreach(DataRow row in dt.Rows)
            {
                string username = row["username"].ToString();
                Console.WriteLine(username);
                string pass = row["password"].ToString();
                Console.WriteLine(pass);
            }
        }


        /*[Test]
        public void TestDB()
        {
            DataTable dt = DBUtils.SelectQuery("SELECT * FROM [dbo].[tbl_magento_user]");

            for(int i=0;i<dt.Rows.Count;i++)
            {
                DataRow row= dt.Rows[i];

                string username = row["username"].ToString();
                Console.WriteLine(username);
            }



        }*/


        /*[Test]
        public void TestDB()
        {
            int rowAff = DBUtils.UpdateDeleteInsertQuery("update [tbl_magento_user] set password='78op' where username='jack'");
            Console.WriteLine(rowAff);
        }
*/
        /* [Test]
         public void TestDB()
         {
            string res= DBUtils.GetFirstCellValue("SELECT count(*) FROM [dbo].[tbl_magento_user] where username='jack'");
             Console.WriteLine(res);
         }*/

        /*
                [Test]
                public void TestDB()
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["bala"].ConnectionString;
                    SqlConnection con = new SqlConnection(connectionString); //store db details //manage open and close connection
                    SqlCommand command = new SqlCommand("SELECT * FROM [dbo].[tbl_magento_user]", con); //store the query and con
                    con.Open();
                    string result =Convert.ToString(command.ExecuteScalar());
                    con.Close();
                }
        */

        /* [Test]
         public void TestDB()
         {
             DataTable dt= DataBaseUtils.SelectQueryToDatatable("select * from tbl_magento_user");

             foreach(DataRow row in dt.Rows)
             {
                 Console.WriteLine(row["id"]);
                 Console.WriteLine(row[1]);
                 Console.WriteLine(row["password"]);
             }
         }*/

        //peter
        //john
        //%#*@&&
        //323343


        /*public static object[] ProvideName()
        {
            object[] main = new object[4];
            main[0] = "peter";
            main[1] = "john";
            main[2] = "%#*@&&";
            main[3] = "323343";

            return main;
        }

        [Test,TestCaseSource("ProvideName")]
        public void RefMethod(string name)
        {
            Console.WriteLine(name);
        }*/

        //king,king123  -- temp1
        //queen,queen123  -- temp2
        //jack,jack123 --temp3

        //3 testcase --> 3 object[] req
        //2 parameter--> 3 object[] with size 2

        /*   public static object[] InvalidCredentialSource()
           {
               object[] temp1 = new object[2]; //number of parameter
               temp1[0] = "king";
               temp1[1] = "king123";

               object[] temp2 = new object[2]; //number of parameter
               temp2[0] = "queen";
               temp2[1] = "queen123";

               object[] temp3 = new object[2]; //number of parameter
               temp3[0] = "jack";
               temp3[1] = "jack123";

               object[] main = new object[3]; //number of test case
               main[0] = temp1;
               main[1] = temp2;
               main[2] = temp3;

               return main;

           }

           [Test, TestCaseSource("InvalidCredentialSource")]
           public void InvalidTest(string username, string password)
           {
               Console.WriteLine(username + password);
           }*/

        //Rough code

        /* [Test]
         public void ExcelRead()
         {
             Excel.Application xlApp = new Excel.Application();

             Excel.Workbook xlBook = xlApp.Workbooks.Open(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx");

             Excel.Worksheet xlSheet = xlBook.Worksheets["ValidCredentialSource"];

             Excel.Range xlRange = xlSheet.UsedRange;

             int rowCount = xlRange.Rows.Count;

             int ColCount = xlRange.Columns.Count;

             Console.WriteLine(rowCount);
             Console.WriteLine(ColCount);

             string value = xlRange.Cells[1, 2].value;

             Console.WriteLine(value);

             xlBook.Close();
             xlApp.Quit();

         }*/

        /*[Test]
        public void ExcelRead()
        {
            Excel.Application xlApp = new Excel.Application();

            Excel.Workbook xlBook = xlApp.Workbooks.Open(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx");

            Excel.Worksheet xlSheet = xlBook.Worksheets["ValidCredentialSource"];

            Excel.Range xlRange = xlSheet.UsedRange;

            int rowCount = xlRange.Rows.Count;

            int ColCount = xlRange.Columns.Count;

            Console.WriteLine(rowCount);
            Console.WriteLine(ColCount);


            for(int i= 2;i<=rowCount;i++)
            {
                for(int j=1;j<=ColCount;j++)
                {
                    string value = xlRange.Cells[i, j].value;
                    Console.WriteLine(value);
                }
            }

            xlBook.Close();
            xlApp.Quit();
        }*/

        /*[Test]
        public void ExcelRead()
        {
            Excel.Application xlApp = new Excel.Application();

            Excel.Workbook xlBook = xlApp.Workbooks.Open(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx");

            Excel.Worksheet xlSheet = xlBook.Worksheets["ValidCredentialSource"];

            Excel.Range xlRange = xlSheet.UsedRange;

            int rowCount = xlRange.Rows.Count;

            int ColCount = xlRange.Columns.Count;

            Console.WriteLine(rowCount);
            Console.WriteLine(ColCount);

            object[] main = new object[rowCount - 1]; //number of rows (Test case)

            for (int i = 2; i <= rowCount; i++)
            {
                object[] temp = new object[ColCount]; //number of parameter
                for (int j = 1; j <= ColCount; j++)
                {
                    string value = xlRange.Cells[i, j].value;
                    Console.WriteLine(value);
                    temp[j-1] = value;
                }
                main[i - 2] = temp;
            }

            xlBook.Close();
            xlApp.Quit();
        }*/

        /*[Test]
        public void ExcelRead()
        {
            using (XLWorkbook book = new XLWorkbook(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx"))
            {
                IXLWorksheet xlSheet = book.Worksheet("ValidCredentialSource");

                IXLRange xlRange = xlSheet.RangeUsed();

                int rowCount = xlRange.RowCount();

                int ColCount = xlRange.ColumnCount();

                Console.WriteLine(rowCount);
                Console.WriteLine(ColCount);

                object[] main = new object[rowCount - 1]; //number of rows (Test case)

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

        }*/

        /*  [Test]
          public void ExcelRead()
          {
              XLWorkbook book = new XLWorkbook(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx");

              IXLWorksheet xlSheet = book.Worksheet("ValidCredentialSource");

              IXLRange xlRange = xlSheet.RangeUsed();

              int rowCount = xlRange.RowCount();

              int ColCount = xlRange.ColumnCount();

              Console.WriteLine(rowCount);
              Console.WriteLine(ColCount);

              object[] main = new object[rowCount - 1]; //number of rows (Test case)

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



          }*/

        /* [Test]
         [TestCase("ff",3)]
         [TestCase("ch",5)]
         [TestCase("ie",89)]
         public void Demo(string browser,int a)
         {
             Console.WriteLine(browser);
         }

         [Test,Retry(2),Repeat(5),Ignore("no need")]
         public void Demo1([Values("ch","ff")] string browser,[Range(1,10)] int phone)
         {
             Console.WriteLine(browser);
         }*/
    }
}
