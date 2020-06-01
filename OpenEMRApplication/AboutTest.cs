using NUnit.Framework;
using AutomationWrapper.Base;
using OpenEMRApplication.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OpenEMRApplication
{
    class AboutTest : WebDriverWrapper
    {
        [Test]
        public void VersionNumberTest()
        {

            LoginPage login = new LoginPage(driver);

            login.SendUsername("admin");
            login.SendPassword("pass");
            login.SelectLanguage("English (Indian)");
            login.ClickOnLogin();
            test.Info("finished with login");
           
            driver.FindElement(By.XPath("//div[text()='About']")).Click();

            driver.SwitchTo().Frame("msc");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchWindowException));
            wait.Until(x => x.FindElement(By.XPath("//h4[contains(text(),'Version')]")));


            string actualValue = driver.FindElement(By.XPath("//h4[contains(text(),'Version')]")).Text;

            test.Info("Version number on page "+ actualValue);

            Assert.AreEqual("Version Number: v5.0.2 (1)", actualValue);

            driver.SwitchTo().DefaultContent();
        }


    }
}
