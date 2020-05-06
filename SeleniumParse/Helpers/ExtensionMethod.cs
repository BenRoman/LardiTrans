using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeleniumParser.Helpers
{
    public static class Extension
    {
        public static string GetValueByClassName(this IWebElement elem, string className)
        {

            string str = "";
            var spans = elem.FindElement(By.ClassName(className)).FindElements(By.TagName("span"))
                .Where(span => !span.GetInnerHtml().Contains("<span"));
            foreach (var item in spans)
            {
                str += item.GetInnerHtml() + "  ";
            }
            //.Select(span => str+=span.Text);$nbsp;
            return str.Replace("\r\n", "").Replace("&nbsp;", "");
        }
        public static string GetInnerHtml(this IWebElement element)
        {
            var remoteWebDriver = (RemoteWebElement)element;
            var javaScriptExecutor = (IJavaScriptExecutor)remoteWebDriver.WrappedDriver;
            var innerHtml = javaScriptExecutor.ExecuteScript("return arguments[0].innerHTML;", element).ToString();

            return innerHtml;
        }
    }
}