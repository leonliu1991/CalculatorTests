using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace CalculatorTest.StepDefinitions
{
    [Binding]
    public class BaseStepDefinition
    {
        private static ExtentReports extent;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        public static string reportPath;
        public static IWebDriver driver;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd HH_mm");
            string targetPath = Path.GetFullPath(@"..\..\TestReport\") + today;

            Directory.CreateDirectory(targetPath);

            var htmlReporter = new ExtentHtmlReporter(targetPath + "\\TestReport.html");
            htmlReporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            reportPath = Path.GetFullPath(targetPath + "\\TestReport.html");
        }


        [BeforeScenario]
        public static void BeforeScenario(TestContext testContext)
        {
            string url = testContext.Properties["SiteUrl"].ToString();
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            driver.Quit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
        }

        [BeforeFeature]
        private static void BeforeFeature()
        {
            featureName = extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        private static void AfterStep(TestContext testContext)
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }
        }
    }
}
