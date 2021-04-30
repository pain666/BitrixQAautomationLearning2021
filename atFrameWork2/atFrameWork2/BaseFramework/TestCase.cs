using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace atFrameWork2.BaseFramework
{
    class TestCase
    {
        public TestCase(string title, Action<IWebDriver, PortalHomePage> body)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Node = new TreeNode(title);
        }

        public void Execute(Uri selenoidHubUri, PortalInfo testPortal)
        {
            IWebDriver driver = default;

            try
            {
                Log.Info($"---------------Запуск кейса '{Title}'---------------");
                driver = DriverActions.GetNewDriver(selenoidHubUri);
                var portalLoginPage = new PortalLoginPage(driver, testPortal);
                var homePage = portalLoginPage.Login(testPortal.PortalAdmin);
                Body.Invoke(driver, homePage);
            }
            catch (Exception e)
            {
                Log.Error($"Кейс не пройден, причина:{Environment.NewLine}{e}");
            }

            Log.Info($"---------------Кейс '{Title}' завершён---------------");

            try
            {
                if (driver != default)
                    driver.Quit();
            }
            catch (Exception) { }
        }

        public string Title { get; set; }
        Action<IWebDriver, PortalHomePage> Body { get; set; }
        public TreeNode Node { get; set; }
    }
}
