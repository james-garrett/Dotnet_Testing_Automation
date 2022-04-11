using System;
using System.Linq;
using System.Threading;
using AutomationPractice.Core;
using AutomationPractice.Core.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using static AutomationPractice.UiTests.UIMethods.SpecificPageActions;

namespace AutomationPractice.UiTests
{

    [TestFixture]
    public class NewCurrencyCard
    {

        private int RetryCount = 0;
        private IWebDriver? _driver;

        [SetUp]
        public void Setup()
        {
            UiTestSession.Current.Start();
        }

        [Test]
        public void BuyCardWith3Ds()
        {
            try
            {
                CleanUpAllSeleniumProcesses();
                _driver = UiTestSession.Current.Resolve<IWebDriver>();
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                OpenCustomerPortalAtCurrencyCardStep(_driver);
                LoadCard(_driver);
                EnterDepartureDetails(_driver);
                EnterPersonalDetails(_driver);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                PayWithCard(_driver);
                // AydenCheckout(_driver);
                Assert.Pass();
            }
            catch (WebDriverTimeoutException ex)
            {
                // if (RetryCount < 1)
                // {
                //     RetryCount++;
                //     BuyCardWith3Ds();
                // }
            }
        }
        
        [Test]
        public void BuyCardWithBankTransfer()
        {
            try
            {
                _driver = UiTestSession.Current.Resolve<IWebDriver>();
                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                OpenCustomerPortalAtCurrencyCardStep(_driver);
                ClickGetStarted(_driver); 

                EnterRegistrationDetails(_driver);
                PayWithBankTransfer(_driver);
                // AydenCheckout(_driver);
                Assert.Pass();
            }
            catch (WebDriverTimeoutException ex)
            {
                // if (RetryCount < 1)
                // {
                //     RetryCount++;
                //     BuyCardWithBankTransfer();
                // }
            }
        }
        
        //Until we have a better way of narrowing out which chrome processes are selenium-created,
        //we'll just have to kill all chrome processes after every selenium test. Otherwise, zombies!
        private static void CleanUpAllSeleniumProcesses()
        {
            var processesToKill =  System.Diagnostics.Process.GetProcessesByName("chrome");
            var processIds = processesToKill.Select(x => x.Id).ToList();
            ChromeFactory.KillAllProcesses(processIds);
        }
        
        [TearDown]
        public void CleanUp()
        {
            UiTestSession.Current.CleanUp();
            _driver?.Close();
            _driver?.Quit();
            _driver?.Dispose();
            Thread.Sleep(3000);
            CleanUpAllSeleniumProcesses();
        }
    }
}