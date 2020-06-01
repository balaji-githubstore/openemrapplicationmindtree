using AutomationWrapper.Base;
using AutomationWrapper.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalCarApp
{
    class SignUpTest : WebDriverWrapper
    {
        public static object[] ValidCredentialSource()
        {
            object[] main = ExcelUtils.GetSheetIntoObject(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\TestData\OpenEMRData.xlsx", "ValidCredentialSource");
            return main;
        }

        [Test, Order(1), TestCaseSource("ValidCredentialSource")]
        public void ValidRoyalTest(string username, string password, string language, string expectedValue)
        {
            Console.WriteLine(username);
        }

    }
}
