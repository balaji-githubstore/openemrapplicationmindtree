using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class LoginPage
    {
        private By userLocator = By.Id("authUser");
        private By passLocator = By.Name("clearPass");
        private By languageLocator = By.Name("languageChoice");
        private By loginLocator = By.XPath("//button[@type='submit']");
        private By errorLocator = By.XPath("//div[contains(text(),'Invalid')]");
        private By ackCertLocator = By.XPath("//a[contains(text(),'Acknowledgments')]");

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SendUsername(string username)
        {
            driver.FindElement(userLocator).SendKeys(username);
        }

        public void SendPassword(string password)
        {
            driver.FindElement(passLocator).SendKeys(password);
        }

        public void SelectLanguage(string language)
        {
            SelectElement selectLanguage = new SelectElement(driver.FindElement(languageLocator));
            selectLanguage.SelectByText(language);
        }

        public void ClickOnLogin()
        {
            driver.FindElement(loginLocator).Click();
        }

        public string GetErrorMessage()
        {
            /*string text = driver.FindElement(errorLocator).Text;
            return text;*/

            return driver.FindElement(errorLocator).Text; 
        }

        public void ClickOnAcknowledgmentLink()
        {
            driver.FindElement(ackCertLocator).Click();
        }

        public void SwitchToAcknowledgmentsWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
        }
    }
}
