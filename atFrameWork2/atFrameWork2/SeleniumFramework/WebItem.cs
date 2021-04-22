using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace atFrameWork2.SeleniumFramework
{
    class WebItem
    {
        public WebItem(List<string> locators, string description, IWebDriver driver)
        {
            Locators = locators;
            Description = description;
            drv = driver;
        }

        IWebDriver drv;
        List<string> Locators { get; set; } = new List<string>();
        public string Description { get; set; }

        public void Click()
        {
            Execute(button => button.Click());
        }

        public void SendKeys(string textToInput)
        {
            Execute(input => input.SendKeys(textToInput));
        }

        void Execute(Action<IWebElement> seleniumCode)
        {
            foreach (var locator in Locators)
            {
                try
                {
                    IWebElement searchSubmitBtn = drv.FindElement(By.XPath(locator));
                    seleniumCode.Invoke(searchSubmitBtn);
                    break;
                }
                catch (WebDriverException ex)
                {
                    if (ex is NoSuchElementException)
                    {
                        if (locator == Locators.Last())
                        {
                            //прерываем кейс!!1111
                        }
                    }
                    else if (ex is StaleElementReferenceException)
                    {

                    }
                    else if (ex is ElementClickInterceptedException)
                    {

                    }
                    else
                        throw;
                }
            }
        }
    }
}
