using System;
using AutomationPractice.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static AutomationPractice.UiTests.HelperMethods;

namespace AutomationPractice.UiTests.UIMethods
{
    public class InteractiveElementMethods
    {
        public static void InputStringIntoTextBox(IWebDriver? driver, string pageName, string elementName, string textToInput)
        {
            var page = Array.Find(UiTestSession.Current.Settings.PagesWithElements.ToArray(), element => element.Name == pageName);
            var fieldElement = Array.Find(page?.Elements.ToArray() ?? Array.Empty<Element>(),element => element.Name == elementName);
            var field = FindElement(driver, By.CssSelector(fieldElement?.XPath), 20);
            field.SendKeys(textToInput);
        }

        public static void ClickButton( IWebDriver? driver, string pageName, string elementName, int waitTime = 20)
        {
            var page = Array.Find(UiTestSession.Current.Settings.PagesWithElements.ToArray(), element => element.Name == pageName);
            var fieldElement = Array.Find(page?.Elements.ToArray() ?? Array.Empty<Element>(),element => element.Name == elementName);
            var field = FindElement(driver, By.CssSelector(fieldElement?.XPath), waitTime);
            field.Click();
        }
        
        public static void ClickButtonWithName( IWebDriver? driver, string elementName, int waitTime = 20)
        {
            var field = FindElement(driver, By.Name(elementName), waitTime);
            field.Click();
        }
        
        public static void SelectFromDropDown(IWebDriver? driver, string pageName, string elementName, string valueToSelect)
        {
            var page = Array.Find(UiTestSession.Current.Settings.PagesWithElements.ToArray(), element => element.Name == pageName);
            var fieldElement = Array.Find(page?.Elements.ToArray() ?? Array.Empty<Element>(),element => element.Name == elementName);
            var field = FindElement(driver, By.CssSelector(fieldElement?.XPath), 20);    
            var selectElement = new SelectElement(field);
            selectElement.SelectByValue(valueToSelect);
        }
    }
}