using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class DashboardPage
    {
        private By billyLocator = By.XPath("//span[text()='Billy']");
        private By logoutLocator = By.XPath("//li[text()='Logout']");

        private IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitForPresenceOfBillyText()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Message = "Error on waiting for presence of element Billy!!";
            wait.Until(x => x.FindElement(billyLocator));
        }

        public string GetCurrentTitle()
        {
            return driver.Title;
        }
        public void MouseHoverOnBilly()
        {
            IWebElement billyEle = driver.FindElement(billyLocator);
            Actions actions = new Actions(driver);
            actions.MoveToElement(billyEle).Build().Perform();
        }
        public void ClickOnLogout()
        {
            driver.FindElement(logoutLocator).Click();
        }
    }
}
