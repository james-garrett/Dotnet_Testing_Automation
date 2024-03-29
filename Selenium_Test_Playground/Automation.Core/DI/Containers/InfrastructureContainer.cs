﻿using System.Diagnostics.CodeAnalysis;
using Automation.Common;
using AutomationPractice.Core;
using AutomationPractice.Core.Selenium;
using Microsoft.Extensions.DependencyInjection;

namespace AutomationPractice.Core.DI.Containers
{

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)]
    public class InfrastructureContainer : IServiceContainer
    {
        public void Register(IServiceCollection collection)
        {
            collection
                .AddOptions()
                .UseTestConfiguration<SessionSettings>()
                .AddSingleton<INamedBrowserFactory, ChromeFactory>()
                .AddSingleton<INamedBrowserFactory, FirefoxFactory>()
                .AddTransient(s => new WebDriverFactory(s).Create());
        }
    }
}