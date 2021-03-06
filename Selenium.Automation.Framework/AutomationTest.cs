﻿using System;
using System.IO;

namespace Selenium.Automation.Framework
{
    public abstract class AutomationTest : InfrastructureObject
    {
        private static readonly string ScreenshotFileNameTemplate = "{0:yyyy-MM-dd HH-mm-ss}.jpg";

        private readonly DirectoryInfo _currentDirectory;

        protected AutomationTest()
        {
            _currentDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        }

        public DirectoryInfo CurrentDirectory
        {
            get { return _currentDirectory; }
        }

        protected string GetPath(string folder, string file)
        {
            return Path.Combine(_currentDirectory.CreateSubdirectory(folder).FullName, file);
        }

        protected TDependency Scenario<TDependency>(params object[] parameters)
        {
            return Resolve<TDependency>(parameters);
        }

        protected void StartTest()
        {
        }

        protected void EndTest(Func<bool> isTestFailed)
        {
            try
            {
                if (isTestFailed())
                {
                    Browser.SaveScreenshot(GetPath(Configuration.TestResultPath, string.Format(ScreenshotFileNameTemplate, DateTime.Now)));
                }
            }
            finally
            {
                Browser.Quit();
            }
        }
    }
}
