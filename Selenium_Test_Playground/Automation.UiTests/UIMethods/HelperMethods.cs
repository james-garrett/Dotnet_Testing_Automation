using System;
using System.Data.SqlClient;
using System.Threading;
using AutomationPractice.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace AutomationPractice.UiTests
{
    public static class HelperMethods
    {
        public static IWebElement FindElement( IWebDriver? driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
        
        //TODO: Method which chooses from a list of accepted card numbers
        public static string PickRandomBankAccountCard()
        {
            var bankNumberArray = UiTestSession.Current.Settings.PaymentCards.ToArray();
            var random = new Random();
            var randomIndex = random.Next(0, (bankNumberArray.Length - 1));
            var randomBankNumber = bankNumberArray[randomIndex].Number;
            return randomBankNumber;
        }

        public static void ResetDb()
        {
            using (SqlConnection connection = new SqlConnection(UiTestSession.Current.Settings.SQL.ConnectionString))
            {
                SqlCommand command = new SqlCommand(UiTestSession.Current.Settings.SQL.ResetPortalUserIdentitySubject, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}",
                            reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }

        public static IWebElement FindElementOnPage(IWebDriver? driver, string elementName)
        {
            IWebElement body = driver.FindElement(By.TagName(elementName));
            return body;
        }
        
        public static bool CanFindElementOnPage( IWebDriver? driver, string elementName)
        {
            return FindElementOnPage(driver, elementName) != null;
        }
        
        public static void WaitUntilVisible( IWebDriver? driver, string elementName)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(elementName)));
        }
        public static void GetIframeWithId(string id) { }
        public static void PayWithGoogleOrApplePay( IWebDriver? driver) { }
    }
}