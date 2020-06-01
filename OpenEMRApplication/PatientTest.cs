using NUnit.Framework;
using AutomationWrapper.Base;
using OpenEMRApplication.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication
{
    class PatientTest : WebDriverWrapper
    {
        [Test,Category("high"),Category("patient")]
        public void AddPatientTest()
        {

            LoginPage login = new LoginPage(driver);

            login.SendUsername("admin");
            login.SendPassword("pass");
            login.SelectLanguage("English (Indian)");
            login.ClickOnLogin();

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(By.XPath("//*[text()='Patient/Client']")))
                .Build().Perform();

            driver.FindElement(By.XPath("//div[text()='New/Search']")).Click();


            //PatientPage
            IWebElement frameEle = driver.FindElement(By.XPath("//iframe[@name='pat']"));
            driver.SwitchTo().Frame(frameEle);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(By.XPath("//input[@id='form_fname']")));

            driver.FindElement(By.XPath("//input[@id='form_fname']")).SendKeys("abc");
            driver.FindElement(By.Id("form_lname")).SendKeys("def");
            driver.FindElement(By.Id("form_DOB")).SendKeys("2020-05-26");
            IWebElement genderEle = driver.FindElement(By.Id("form_sex"));
            SelectElement selectGender = new SelectElement(genderEle);
            selectGender.SelectByText("Female");
            driver.FindElement(By.Id("create")).Click();

            driver.SwitchTo().DefaultContent();

            driver.SwitchTo().Frame("modalframe");
            driver.FindElement(By.XPath("//input[@value='Confirm Create New Patient']")).Click();
            driver.SwitchTo().DefaultContent();

            wait.IgnoreExceptionTypes(typeof(NoAlertPresentException));
            wait.Until(x => x.SwitchTo().Alert()).Accept();

            driver.FindElement(By.XPath("//div[@class='closeDlgIframe']")).Click();

            driver.SwitchTo().Frame("pat");
            //get the text of patient name

            string actualText = driver.FindElement(By.XPath("//h2[contains(text(),'Medical Record Dashboard')]")).Text;
           
            Assert.IsTrue(actualText.Contains("Abc Def"),"Assertion on Add Patient");

            driver.SwitchTo().DefaultContent();
        }
    }
}
