using Aqua.Selenium.Framework;
using atFrameWork2.BaseFramework;
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
            WaitElementDisplayed(driver);
            PrintActionInfo(nameof(Click));

            Execute(button =>
            {
                BorderHighlight.Highlight(button, Color.Red);
                button.Click();
            }, driver);
        }

        public void SendKeys(string textToInput, IWebDriver driver)
        {
            WaitElementDisplayed(driver);
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

        public bool WaitElementDisplayed(IWebDriver driver, int maxWait_s = 5)
        {
            return WaitDisplayedCommon(driver, maxWait_s, true, "Ожидание отображения элемента " + DescriptionFull);
        }

        public bool WaitWhileElementDisplayed(IWebDriver driver, int maxWait_s = 5)
        {
            return WaitDisplayedCommon(driver, maxWait_s, false, "Ожидание пропадания элемента " + DescriptionFull);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="maxWait_s"></param>
        /// <param name="waitDirection">Если true то будет ждать пока элемент не станет отображаться, иначе будет ждать пока элемент отображается</param>
        /// <param name="waitDescription"></param>
        /// <returns></returns>
        bool WaitDisplayedCommon(IWebDriver driver, int maxWait_s, bool waitDirection, string waitDescription)
        {
            var impWait = driver.Manage().Timeouts().ImplicitWait;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            bool result = Waiters.WaitForCondition(() =>
            {
                bool expectedState = false;

                Execute(el =>
                {
                    expectedState = el.Displayed == waitDirection;
                }, driver);

                return expectedState;
            }, 1, maxWait_s, waitDescription);

            driver.Manage().Timeouts().ImplicitWait = impWait;
            return result;
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
                        else if (ex is ElementClickInterceptedException)
                        {
                            if (ex.Message.Contains("helpdesk-notification-popup"))
                            {
                                new WebItem("//div[contains(@class, 'popup-close-btn')]", "Кнопка закрытия баннера").Click(driver);
                                continue;
                            }
                            else
                                throw;
                        }
                        else
                            throw;
                    }

                    break;
                }
            }
        }

        private void PrintActionInfo(string actionTitle)
        {
            Log.Info($"{actionTitle}: " + DescriptionFull);
        }

        public string DescriptionFull { get => $"'{Description}' локаторы: {string.Join(", ", Locators)}"; }
    }
}
