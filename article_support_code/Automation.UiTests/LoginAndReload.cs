using System;
using AutomationPractice.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using static AutomationPractice.UiTests.HelperMethods;
using static AutomationPractice.UiTests.UIMethods.SpecificPageActions;
using static AutomationPractice.UiTests.UIMethods.InteractiveElementMethods;

namespace AutomationPractice.UiTests
{

    [TestFixture]
    public class LoginAndReload
    {

        private int RetryCount = 0;
        private IWebDriver? _driver;

        [SetUp]
        public void Setup()
        {
            UiTestSession.Current.Start();
        }

        [Test]
        public void CardPaymentWith3DsTest()
        {
            _driver = UiTestSession.Current.Resolve<IWebDriver>();
            // ResetDB();
            OpenCustomerPortal(_driver);
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

        [TearDown]
        public void CleanUp()
        {
            UiTestSession.Current.CleanUp();
            _driver?.Close();
            _driver?.Quit();
        }
    }
}