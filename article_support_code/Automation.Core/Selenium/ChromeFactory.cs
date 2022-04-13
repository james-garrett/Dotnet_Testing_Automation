using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Automation.Common;
using AutomationPractice.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationPractice.Core.Selenium
{

    public class ChromeFactory : INamedBrowserFactory
    {
        private readonly SessionSettings _options;

        private static List<int> processes;

        public ChromeFactory(SessionSettings options)
        {
            processes = new List<int>();
            _options = options;
        }
        
        private void StoreProcess(int prcId)
        {
            processes.Add (prcId); // Add this to our list of processes to be kept track of
        }

        public static void KillAllProcesses(List<int> processesToKill)
        {
            if (!processesToKill.Any()) return;
            
            for (var i = 0; i < processesToKill.Count; i++)
            {
                KillProcess(processesToKill[i]);
                processesToKill.Remove(i);
            }
        }
        public static void KillProcess(int PID)
        {
            List<Process> allProcesses = Process.GetProcesses().ToList();
            
            // Search through the countless processes we have and try and find our process
            for (int i = 0; i <= allProcesses.Count; i++) {
                if (allProcesses [i] == null)
                {
                    continue; // This segment of code prevents NullPointerExceptions by checking if the process is null before doing anything with it
                }

                if (allProcesses[i].Id != PID) continue; // Is this our process?
                allProcesses [i].Kill (); // It is! Lets kill it
                while (!allProcesses [i].HasExited) { } // Wait until the process exits
                allProcesses [i] = null; // Mark this process to be skipped the next time around
                return;
            }
            // Couldn't find our process!!!
            throw new Exception ("Process not found!");
        }

        public IWebDriver Create()
        {
            var driverService = ChromeDriverService.CreateDefaultService(_options.DriverPath);
            var options = new ChromeOptions();
            StoreProcess(driverService.ProcessId);
            driverService.LogPath = _options.LogPath;
            driverService.EnableVerboseLogging = true;
            if (_options.Headless)
            {
                options.AddArgument("headless");
            }

            options.AddArgument("no-sandbox");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--start-maximized");
            
            options.AddArguments("--remote-debugging-port=9225"); 
            
            options.AddUserProfilePreference("download.default_directory", _options.DownloadDirectory);
            options.AddUserProfilePreference("profile.cookie_controls_mode", 0);
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            return new ChromeDriver(driverService, options, TimeSpan.FromSeconds(_options.DefaultTimeoutSeconds));
        }

        public Browsers Name => Browsers.Chrome;
    }
}