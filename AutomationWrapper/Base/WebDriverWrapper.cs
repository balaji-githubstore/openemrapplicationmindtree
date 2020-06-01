﻿using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AutomationWrapper.Base
{
    public class WebDriverWrapper
    {
        protected IWebDriver driver;
        public static string projectPath;
        public static ExtentReports extent;
        public static ExtentTest test;
        public static string screenShotPath;


        [OneTimeSetUp]
        public void Init()
        {
            //to avoid unnecessary update on each class intialization
            if (extent == null)
            {
                projectPath = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                projectPath = projectPath.Substring(0, projectPath.LastIndexOf("bin"));
                projectPath = new Uri(projectPath).LocalPath;
                string reportPath = projectPath + @"Reports\";

                ExtentHtmlReporter reporter = new ExtentHtmlReporter(reportPath);
                extent = new ExtentReports();
                extent.AttachReporter(reporter);
            }
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }

        [SetUp]
        public void Initialization()
        {
            //var t= ConfigurationManager.OpenExeConfiguration(@"D:\Mine\Company\Mindtree\Stream Track - 2020\OpenEMRApplication\OpenEMRApplication\App.config");
            //string browserName = t.AppSettings["browser"];
            LaunchBrowser("ch");   
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            //driver.Url = ConfigurationManager.AppSettings["url"];
            driver.Url = "https://demo.openemr.io/b/openemr/interface/login/login.php?site=default";
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        public void LaunchBrowser(string browserName)
        {
           

            if (browserName.ToLower().Equals("ff"))
            {
                driver = new FirefoxDriver();
            }
            else if (browserName.ToLower().Equals("ie"))
            {
                driver = new InternetExplorerDriver();
            }
            else
            {
                driver = new ChromeDriver();
            }
        }

        public void TakeScreenShot(string testName)
        {
            if (driver != null)
            {
                string name = DateTime.Now.ToString().Replace('/', '-').Replace(':', '-');
                screenShotPath = projectPath + @"\Reports\screenshot_" + testName + "_" + name+".png";
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                ss.SaveAsFile(screenShotPath, ScreenshotImageFormat.Png);
            }
        }

        [TearDown]
        public void AddTestResultAndQuitBrowser()
        {
            string testName = TestContext.CurrentContext.Test.MethodName;

            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
                var errorMessage = TestContext.CurrentContext.Result.Message;
                TakeScreenShot(testName);
                test.Log(Status.Fail, stackTrace + errorMessage);
                test.Log(Status.Fail, "Failed - Snapshot below:");
                test.AddScreenCaptureFromPath(screenShotPath, title: testName);
            }
            else if (status == TestStatus.Passed)
            {
                TakeScreenShot(testName);
                test.Log(Status.Pass, "Passed - Snapshot below:");
                test.AddScreenCaptureFromPath(screenShotPath, title: testName);
            }
            else if (status == TestStatus.Skipped)
            {
                test.Log(Status.Skip, "Skipped - " + testName);
            }
            driver.Quit();
        }
    }
}
