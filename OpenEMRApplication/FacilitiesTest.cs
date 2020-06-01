using AutomationWrapper.Base;
using NUnit.Framework;
using OpenEMRApplication.Pages;
using AutomationWrapper.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenEMRApplication
{
    class FacilitiesTest : WebDriverWrapper
    {
        public static object[] AddFacilitySource()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            path = path.Substring(0, path.LastIndexOf("bin"));
            path = new Uri(path).LocalPath;
            path = path + @"TestData\OpenEMRData.xlsx";

            string currentMethodName = System.Reflection.MethodBase.GetCurrentMethod().Name;

            //object[] main = ExcelUtils.GetSheetIntoObject(path, currentMethodName);
            object[] main = ClosedExcelUtils.GetSheetIntoObjectUsingClosedXML(path, currentMethodName);
            return main;
        }

        [Test, TestCaseSource("AddFacilitySource"),Category("high")]
        public void AddFacilityTest(string username, string password, string language, string facilityName, string color, string expectedValue)
        {
            LoginPage login = new LoginPage(driver);

            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickOnLogin();


            //Dashboard page
            Actions actions = new Actions(driver);

            actions.MoveToElement(driver.FindElement(By.XPath("//div[text()='Administration']"))).Build().Perform();

            driver.FindElement(By.XPath("//div[text()='Facilities']")).Click();


            //FacilitiesPage
            //1
            driver.SwitchTo().Frame("adm");

            //2
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(By.XPath("//span[text()='Add Facility']"))).Click();

            //driver.FindElement(By.XPath("//span[text()='Add Facility']")).Click();
            //3
            driver.SwitchTo().DefaultContent();

            //4            
            driver.SwitchTo().Frame("modalframe");


            driver.FindElement(By.XPath("//input[@name='facility']")).SendKeys(facilityName);
            driver.FindElement(By.Id("ncolor")).SendKeys(color);
            driver.FindElement(By.XPath("//span[text()='Save']")).Click();
            //comeout 
            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame("adm");
            Thread.Sleep(5000);

            string pageSource = driver.PageSource;
            Assert.IsTrue(pageSource.Contains(facilityName), "Assertion on Add Facility");

            //db
            string res = DBUtils.GetFirstCellValue("SELECT count(*) FROM [dbo].[tbl_magento_user] where username='"+facilityName+"'");

            int result = Convert.ToInt32(res);

            if(result <= 0)
            {
                Assert.Fail("No rows available");
            }




            /* var rowsEle = driver.FindElements(By.XPath("//table[@class='table table-striped']/tbody/tr"));
             int rowCount = rowsEle.Count;

             bool check = false;
             for (int i = 1; i <= rowCount; i++)
             {
                 string name = driver.FindElement(By.XPath("//table[@class='table table-striped']/tbody/tr[" + i + "]/td[1]")).Text;
                 if (name.Trim().Equals("bala1234"))
                 {
                     check = true;
                 }
             }
             Assert.IsTrue(check, "Assertion on Add Facility"); *///expect true

            //on deleting
            //Assert.IsFalse(check);//expect false
        }
    }
}
