using System;
using AutomationPractice.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using Shared.SeleniumExtensions;
using static AutomationPractice.UiTests.HelperMethods;
using static AutomationPractice.UiTests.UIMethods.SpecificPageActions;
using static AutomationPractice.UiTests.UIMethods.InteractiveElementMethods;
using Assert = NUnit.Framework.Assert;

namespace AutomationPractice.UiTests
{

    [TestFixture]
    public class BasicTests
    {

        private int RetryCount = 0;
        private IWebDriver? _driver;

        [SetUp]
        public void Setup()
        {
            UiTestSession.Current.Start();
        }

        [Test]
        public void LoginAndViewDashboard()
        {
            _driver = UiTestSession.Current.Resolve<IWebDriver>();
            // ResetDB();
            OpenSupportPortal(_driver);
            Assert.IsTrue(CanFindElementOnPage(_driver, "Sales Portal"));
            Assert.Pass();
        }

        [Test]
        public void LoginAndBuyCardWith3Ds()
        {
            try
            {
                _driver = UiTestSession.Current.Resolve<IWebDriver>();

                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                ResetDb();
                OpenCustomerPortal(_driver);
                LoginAsTestUser(_driver);

                ClickButton(_driver, "Dashboard", "GoToCurrencyCardPurchasePath");

                LoadCard(_driver);
                EnterDepartureDetails(_driver);
                PayWithCard(_driver);
                // AydenCheckout(_driver);
                Assert.Pass();
            }
            catch (WebDriverTimeoutException ex)
            {
                if (RetryCount < 1)
                {
                    RetryCount++;
                    LoginAndBuyCardWith3Ds();
                }
                else
                {
                    throw;
                }
            }
        }
        
        [Test]
        public void LoginAndBuyCardWithBankTransfer()
        {
            try
            {
                _driver = UiTestSession.Current.Resolve<IWebDriver>();

                _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                ResetDb();
                OpenCustomerPortal(_driver);
                LoginAsTestUser(_driver);

                ClickButton(_driver,"Dashboard", "GoToCurrencyCardPurchasePath");

                LoadCard(_driver);
                EnterDepartureDetails(_driver);
                PayWithBankTransfer(_driver);
                // AydenCheckout(_driver);
                Assert.Pass();
            }
            catch (WebDriverTimeoutException ex)
            {
                if (RetryCount < 1)
                {
                    RetryCount++;
                    LoginAndBuyCardWithBankTransfer();
                }
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            _driver.TestCleanup();
            UiTestSession.Current.CleanUp();
            _driver?.Close();
            _driver?.Quit();
        }
    }
}