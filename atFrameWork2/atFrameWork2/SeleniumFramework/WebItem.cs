using Aqua.Selenium.Framework;
using atFrameWork2.BaseFramework.LogTools;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace atFrameWork2.SeleniumFramework
{
    class WebItem
    {
        public WebItem(string locator, string description) : this(new List<string> { locator }, description) 
        { 
        }

        public WebItem(List<string> locators, string description)
        {
            Locators = locators;
            Description = description;
        }

        List<string> Locators { get; set; } = new List<string>();
        public string Description { get; set; }

        public void Click(IWebDriver driver)
        {
            PrintActionInfo(nameof(Click));

            Execute(button =>
            {
                BorderHighlight.Highlight(button, Color.Red);
                button.Click();
            }, driver);
        }

        public void SendKeys(string textToInput, IWebDriver driver)
        {
            PrintActionInfo(nameof(SendKeys));
            Execute(input => 
            { 
                BorderHighlight.Highlight(input, Color.Blue);
                input.SendKeys(textToInput); 
            }, driver);
        }

        public void SwitchToFrame(IWebDriver driver)
        {
            PrintActionInfo(nameof(SwitchToFrame));
            Execute(frame =>
            {
                BorderHighlight.Highlight(frame, Color.Green);
                driver.SwitchTo().Frame(frame);
            }, driver);
        }

        public void AssertTextContains(IWebDriver driver, string expectedText, string failMessage)
        {
            PrintActionInfo(nameof(AssertTextContains));

            Execute(targetElement =>
            {
                string factText = targetElement.Text;

                if(string.IsNullOrEmpty(factText) || !factText.Contains(expectedText))
                {
                    Log.Error(failMessage + Environment.NewLine +
                        $"Ожидалось наличие подстроки: {expectedText}, но было:{Environment.NewLine}{factText}");
                }
            }, driver);
        }

        void Execute(Action<IWebElement> seleniumCode, IWebDriver driver)
        {
            foreach (var locator in Locators)
            {
                int staleRetryCount = 3;

                for (int i = 0; i < staleRetryCount; i++)
                {
                    try
                    {
                        IWebElement targetElement = driver.FindElement(By.XPath(locator));
                        seleniumCode.Invoke(targetElement);
                        break;
                    }
                    catch (WebDriverException ex)
                    {
                        if (ex is NoSuchElementException)
                        {
                            if (locator == Locators.Last())
                            {
                                throw;
                            }
                        }
                        else if (ex is StaleElementReferenceException)
                        {
                            continue;
                        }
                        //else if (ex is ElementClickInterceptedException)
                        //{

                        //}
                        else
                            throw;
                    }

                    break;
                }
            }
        }

        private void PrintActionInfo(string actionTitle)
        {
            Log.Info($"{actionTitle}: '{Description}' локаторы: {string.Join(", ", Locators)}");
        }
    }
}
