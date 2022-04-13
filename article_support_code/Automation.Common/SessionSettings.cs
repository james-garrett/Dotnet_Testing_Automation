using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Automation.Common;
using OpenQA.Selenium;

namespace Automation.Common
{

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
    public class SessionSettings
    {
        public Browsers Browser { get; set; }
        public string DriverPath { get; set; } = string.Empty;
        public string LogPath { get; set; } = string.Empty;
        public bool Headless { get; set; }
        public string DownloadDirectory { get; set; } = string.Empty;
        public uint DefaultTimeoutSeconds { get; set; }
        
        public ListOfCredentials TestCredentials { get; set; }
        public string ApplicationUrl { get; set; }
        
        public List<PaymentCard> PaymentCards { get; set; }
        public List<PageWithElements> PagesWithElements { get; set; }
        
        public SQLDB SQL { get; set; }
    }
}

public class SQLDB
{
    public string ConnectionString { get; set; }
    public string ResetPortalUserIdentitySubject { get; set; }
}

// public class ListofPaymentCards
// {
//     public List<PaymentCard> TestCards { get; set; }
// }

public enum CardType {
    CreditCard,
    DebitCard
}

public class PaymentCard
{
    public CardType CardType {get; set;}
    public string Number {get; set;}
    public string SecurityCode {get; set;}
    public string NameOnCard {get; set;}
    public string ExpiryDate {get; set;}
}
public class ListOfCredentials
{
    public List<User> TestUsers { get; set; }
    public User PrimaryUser { get; set; }
}

public class User
{
   public string UserName { get; set; }
   public string Password { get; set; }
}

public class PageWithElements
{
    public string Name { get; set; }
    public List<Element> Elements { get; set; }
}

public class Element
{
    public string Name { get; set; }
    public string XPath { get; set; }
}