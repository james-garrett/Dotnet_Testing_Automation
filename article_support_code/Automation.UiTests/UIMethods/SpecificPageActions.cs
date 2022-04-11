using System;
using AutomationPractice.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static AutomationPractice.UiTests.UIMethods.InteractiveElementMethods;
using static AutomationPractice.UiTests.HelperMethods;

namespace AutomationPractice.UiTests.UIMethods
{
    public class SpecificPageActions
    {
        
        public static void LoadCard( IWebDriver? driver)
        {
            ClickButton(driver, "Step_YourCurrencyCard", "LoadCard");
            ClickButton(driver, "Step_YourCurrencyCard","Next");
        }
        public static void EnterPersonalDetails(IWebDriver? driver)
        {
            SelectFromDropDown(driver,"Step_PersonalDetails", "Title", "Mr");
            InputStringIntoTextBox(driver,"Step_PersonalDetails", "FirstName", "John");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "MiddleName", "B");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "LastName", "Burton");
            
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "Email", "test@testing.com.au");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "ConfirmEmail", "test@testing.com.au");
            
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "MobileNumber", "450000000");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "HomeNumber", "32032123");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "AreaCode", "7");
            
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "UnitNo", "1");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "Suburb", "Brisbane City");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "StreetNo", "123");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "StreetName", "Eagle");
            InputStringIntoTextBox(driver, "Step_PersonalDetails", "Postcode", "4000");
            
            SelectFromDropDown(driver,"Step_PersonalDetails", "State", "QLD");

            ClickButton(driver,"Step_PersonalDetails", "No");
            ClickButton(driver,"Step_PersonalDetails", "DisclaimerCheckbox");
            
            //TODO - if this happens again, make a "FindElementByName" function
            var field = FindElement(driver, By.Name("next"), 20);
            field.Click();
        }
        
        public static void PayWithCard( IWebDriver? driver)
        {
            ClickButton(driver,"Step_Payment", "PickCardPaymentMethod");
            
            driver.SwitchTo()
                .Frame(driver.FindElement(By.CssSelector("#credit-card-container > div > div > div.adyen-checkout__loading-input__form.jZ0YjSr9W9MlpurLCM8H > div.adyen-checkout__card__form > div.adyen-checkout__field.adyen-checkout__field--cardNumber > label > div > span > iframe")));
            
            //We randomly generate a bank number so that the payment API isn't being overwhelemed with the same bank requests
            InputStringIntoTextBox(driver,"Step_Payment", "CardNumberInputTextField",PickRandomBankAccountCard());
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo()
                .Frame(driver.FindElement(By.CssSelector("#credit-card-container > div > div > div.adyen-checkout__loading-input__form.jZ0YjSr9W9MlpurLCM8H > div.adyen-checkout__card__form > div.adyen-checkout__card__exp-cvc.adyen-checkout__field-wrapper > div.adyen-checkout__field.adyen-checkout__field--50.adyen-checkout__field--expiryDate > label > div > span > iframe")));    
            InputStringIntoTextBox(driver,"Step_Payment", "CardExpiryTextField", UiTestSession.Current.Settings.PaymentCards[0].ExpiryDate);
            
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo()
                .Frame(driver.FindElement(By.CssSelector("#credit-card-container > div > div > div.adyen-checkout__loading-input__form.jZ0YjSr9W9MlpurLCM8H > div.adyen-checkout__card__form > div.adyen-checkout__card__exp-cvc.adyen-checkout__field-wrapper > div.adyen-checkout__field.adyen-checkout__field--50.adyen-checkout__field__cvc.adyen-checkout__field--securityCode > label > div > span > iframe")));
            InputStringIntoTextBox(driver,"Step_Payment", "CCVTextField", UiTestSession.Current.Settings.PaymentCards[0].SecurityCode);
            
            driver.SwitchTo().DefaultContent();
            InputStringIntoTextBox(driver,"Step_Payment", "NameTextField", UiTestSession.Current.Settings.PaymentCards[0].NameOnCard);
            
            InputStringIntoTextBox(driver,"Step_Payment", "MotherTextField", "Mother");

            ClickButton(driver,"Step_Payment", "DisclaimerCheckbox");
            ClickButton(driver,"Step_Payment", "Submit", 60);
        }

        public static void PayWithBankTransfer( IWebDriver? driver)
        {
            ClickButton(driver,"Step_Payment", "PickBankPaymentMethod");
            ClickButton(driver,"Step_Payment", "DisclaimerCheckbox");
            ClickButton(driver,"Step_Payment", "Submit", 60);
        }
        
        public static void OpenCustomerPortal(IWebDriver? driver)
        {
            var url = UiTestSession.Current.Settings.ApplicationUrl;
            driver?.Navigate().GoToUrl(url);
        }
        
        public static void OpenCustomerPortalAtCurrencyCardStep(IWebDriver? driver)
        {
            var url = "https://customerportal.travelmoneyoz.local/purchasecurrencycard?step=YourCurrencyCard";
            driver?.Navigate().GoToUrl(url);
        }

        public static void ClickGetStarted(IWebDriver? driver)
        {
            ClickButton(driver,"Login", "GetStartedBtn");
        }

        public static void LoginAsTestUser(IWebDriver? driver)
        {
            InputStringIntoTextBox( driver,"Login", "UsernameTextField", UiTestSession.Current.Settings.TestCredentials.PrimaryUser.UserName);
            InputStringIntoTextBox(driver,"Login", "PasswordTextField", UiTestSession.Current.Settings.TestCredentials.PrimaryUser.Password);

            ClickButton(driver,"Login", "LogInBtn");
        }

        public static void EnterDepartureDetails( IWebDriver? driver)
        {
            ClickButton(driver,"Step_DepartureDetails", "DepartureDatePeriodPicker");

            SelectFromDropDown(driver,"Step_DepartureDetails", "DepartureDateDayInputDay", "1");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DepartureDateDayInputMonth", "1");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DepartureDateDayInputYear", "2026");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputDay", "1");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputMonth", "1");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputYear", "1990");
            ClickButton(driver,"Step_DepartureDetails", "Next");
        }
        
        //For the GetStarted page
        public static void EnterRegistrationDetails( IWebDriver? driver)
        {
            InputStringIntoTextBox(driver,"Step_DepartureDetails", "FirstName", "John");
            InputStringIntoTextBox(driver,"Step_DepartureDetails", "LastName", "Burton");
            
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputDay2", "5");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputMonth2", "2");
            SelectFromDropDown(driver,"Step_DepartureDetails", "DOBInputYear2", "1983");
            
            InputStringIntoTextBox(driver,"Step_DepartureDetails", "Email", "test@testing.com.au");
            
            //TODO - change to specific user deets pulled from DB
            InputStringIntoTextBox(driver,"Step_DepartureDetails", "Last4Digits", "3639");
            SelectFromDropDown(driver,"Step_DepartureDetails", "CardExpiryMonth", "1");
            SelectFromDropDown(driver,"Step_DepartureDetails", "CardExpiryYear", "2026");
            ClickButton(driver,"Step_DepartureDetails", "Next2", 160);
        }
        
        public static void AydenCheckout( IWebDriver? driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            driver.SwitchTo()
                .Frame(driver.FindElement(By.CssSelector("#threedsContainer > div > iframe")));
            InputStringIntoTextBox( driver,"Step_Ayden_Checkout", "PasswordTextInput", "password");
            ClickButton(driver,"Step_Ayden_Checkout", "Continue");
            driver.SwitchTo().DefaultContent();
        }
    }
}