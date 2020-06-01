using AutomationWrapper.Base;
using AutomationWrapper.Utilities;
using NUnit.Framework;
using OpenEMRApplication.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace OpenEMRApplication
{
    class LoginTest : WebDriverWrapper
    {
        public static object[] ValidCredentialSource()
        {
            object[] main = ExcelUtils.GetSheetIntoObject(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx", "ValidCredentialSource");
            return main;
        }

        [Test, Order(1), TestCaseSource("ValidCredentialSource")]
        public void ValidCredentialTest(string username, string password, string language, string expectedValue)
        {
            LoginPage login = new LoginPage(driver);

            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickOnLogin();

            DashboardPage dashboard = new DashboardPage(driver);
            dashboard.WaitForPresenceOfBillyText();

            string actualTitle = dashboard.GetCurrentTitle();
            Assert.AreEqual(expectedValue, actualTitle);

            dashboard.MouseHoverOnBilly();
            dashboard.ClickOnLogout();
        }


        //admin123,pass123,English (Indian),Invalid username or password - temp1
        //bala,bala123,French (Standard),Invalid username or password - temp2 

        public static object[] InvalidCredentialSource()
        {
            object[] temp1 = new object[4]; //number of parameter
            temp1[0] = "admin123";
            temp1[1] = "pass123";
            temp1[2] = "English (Indian)";
            temp1[3] = "Invalid username or password";

            object[] temp2 = new object[4];
            temp2[0] = "bala";
            temp2[1] = "bala123";
            temp2[2] = "French (Standard)";
            temp2[3] = "Invalid username or password";

            object[] main = new object[2]; //number of rows (excluding header)
            main[0] = temp1;
            main[1] = temp2;

            return main;

        }

        [Test, Order(2), TestCaseSource("InvalidCredentialSource")]
        public void InvalidCredentialTest(string username, string password, string language, string expectedValue)
        {
            LoginPage login = new LoginPage(driver);

            login.SendUsername(username);
            login.SendPassword(password);
            login.SelectLanguage(language);
            login.ClickOnLogin();

            string actualValue = login.GetErrorMessage();

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void AcknowledgeLicensingAndCertificationLinkTest()
        {
            LoginPage login = new LoginPage(driver);
            login.ClickOnAcknowledgmentLink();
            login.SwitchToAcknowledgmentsWindow();

            AcknCertificationPage ackPage = new AcknCertificationPage(driver);
            Thread.Sleep(5000);
            string pageSource = ackPage.GetPageSource();

            Assert.IsTrue(pageSource.Contains("Acknowledgments, Licensing and Certification"), "Assertion on AcknowledgeLicensingLinkTest.");
        }
    }
}
