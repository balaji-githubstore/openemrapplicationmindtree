using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEMRApplication.Pages
{
    class AcknCertificationPage
    {
        private IWebDriver driver;

        public AcknCertificationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public string GetPageSource()
        {
            return driver.PageSource;
        }
    }
}
