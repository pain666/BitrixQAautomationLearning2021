using atFrameWork2.BaseFramework.LogTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
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
            Log.Info($"{nameof(OpenUri)}: {uri}");
            driver.Navigate().GoToUrl(uri);
        }

        public static IWebDriver GetNewDriver(Uri selenoidHubUri)
        {
            IWebDriver driver;

            if (selenoidHubUri == default)
            {
                driver = new ChromeDriver();
            }
            else
            {
                var desiredCapabilities = new DesiredCapabilities();
                desiredCapabilities.SetCapability(CapabilityType.BrowserName, "chrome");
                desiredCapabilities.SetCapability(CapabilityType.BrowserVersion, "90.0");
                desiredCapabilities.Platform = new Platform(PlatformType.Any);
                desiredCapabilities.SetCapability("enableVNC", true);
                desiredCapabilities.SetCapability("enableVideo", true);
                desiredCapabilities.SetCapability("sessionTimeout", "1m");
                desiredCapabilities.SetCapability("timeZone", "Europe/Kaliningrad");
                desiredCapabilities.SetCapability("screenResolution", "1440x940x24");

                var rDriver = new RemoteWebDriver(selenoidHubUri, desiredCapabilities);
                Log.Info(selenoidHubUri.Scheme + "://" + selenoidHubUri.Host + ":4444/video/" + rDriver.SessionId.ToString() + ".mp4");
                driver = rDriver;
            }

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
