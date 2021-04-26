using atFrameWork2.BaseFramework.LogTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.SeleniumFramework
{
    class DriverActions
    {
        public static void Refresh(IWebDriver driver)
        {
            Log.Info($"{nameof(Refresh)}");
            driver.Navigate().Refresh();
        }

        public static void OpenUri(IWebDriver driver, Uri uri)
        {
            Log.Info($"{nameof(OpenUri)}");
            driver.Navigate().GoToUrl(uri);
        }

        public static IWebDriver GetNewDriver()
        {
            var driver = new ChromeDriver();
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
