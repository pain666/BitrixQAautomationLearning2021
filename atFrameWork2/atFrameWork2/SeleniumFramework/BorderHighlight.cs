using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Aqua.Selenium.Framework
{
    public class BorderHighlight
    {
        /// <summary>
        /// Рисует контур элемента заданным цветом через js
        /// </summary>
        /// <param name="webElement"></param>
        public static void Highlight(IWebElement webElement, Color color)
        {
            if (webElement != default)
            {
                try
                {
                    string existingStyle = webElement.GetAttribute("style");
                    var script = "arguments[0].style.cssText = \"" + AddBorderToStyle(color, existingStyle) + "\";";
                    var rc = (RemoteWebElement)webElement;
                    var driver = (IJavaScriptExecutor)rc.WrappedDriver;
                    driver.ExecuteScript(script, rc);
                }
                catch (Exception e)
                {
                    
                }
            }
        }

        /// <summary>
        /// Добавляет к стилям объекта подсветку контура заданным цветом
        /// </summary>
        /// <param name="jsColor"></param>
        /// <param name="existingStyle"></param>
        /// <returns></returns>
        private static string AddBorderToStyle(Color color, string existingStyle)
        {
            string jsColor = color.Name.ToLower();

            var attrsToAdd = new Dictionary<string, string>
            {
                {"box-shadow", "0px 0px 0px 3px " + jsColor + " inset" }
            };

            if (!string.IsNullOrEmpty(existingStyle))
            {
                var existingAttrs = existingStyle.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                existingAttrs.RemoveAll(x => string.IsNullOrEmpty(x));
                //в этом цикле существующие атрибуты стиля элемента парсятся и докладываются в словарь из которого потом сформируется новый стиль
                foreach (var existingAttr in existingAttrs)
                {
                    if (attrsToAdd.Keys.FirstOrDefault(x => existingAttr.Contains(x)) == default)
                    {
                        var kv = existingAttr.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                        kv.RemoveAll(x => string.IsNullOrEmpty(x));
                        if (kv.Count > 0)
                            attrsToAdd[kv[0]] = kv.Count > 1 ? kv[1] : "";
                    }
                }
            }

            string moddedStyle = default;
            foreach (var attr in attrsToAdd)
                moddedStyle += attr.Key + ": " + attr.Value + "; ";
            return moddedStyle;
        }
    }
}
