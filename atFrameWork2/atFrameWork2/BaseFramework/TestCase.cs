using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.BaseFramework
{
    class TestCase
    {
        public TestCase(string title, Action<IWebDriver, PortalHomePage> body)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public void Execute()
        {
            try
            {
                IWebDriver driver = DriverActions.GetNewDriver();
                var admin = new User() { Login = "qa-at-learning@mail.bx24.net", Password = "^YHN7ujm*IK<" };
                var portalLoginPage = new PortalLoginPage(driver, new PortalInfo(new Uri("https://bxprod77.bitrix24.ru/"), admin));
                var homePage = portalLoginPage.Login(admin);
                Body.Invoke(driver, homePage);
            }
            catch (Exception e)
            {
                Log.Error($"Кейс не пройден, причина:{Environment.NewLine}{e}");
            }
        }

        public string Title { get; set; }
        Action<IWebDriver, PortalHomePage> Body { get; set; }
    }
}
