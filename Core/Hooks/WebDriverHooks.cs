using AventStack.ExtentReports.Reporter;
using BoDi;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Core.Config;
using MicroappPlatformQaAutomation.Core.Driver;
using OpenQA.Selenium.Support.Events;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace MicroappPlatformQaAutomation.Core.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private readonly IObjectContainer _objectContainer;

        public WebDriverHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        static AventStack.ExtentReports.ExtentReports extent;
        static AventStack.ExtentReports.ExtentTest feature;
        AventStack.ExtentReports.ExtentTest scenario, step;
        static string reportpath = System.IO.Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Result"
           + Path.DirectorySeparatorChar + "Result_" + DateTime.Now.ToString("ddMMyyyy HHmmss");

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(reportpath);
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReport);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void InitializeWebDriver(ScenarioContext context, FeatureContext featureContext)
        {
            if (featureContext.FeatureInfo.Title != "API Testing")
            {
                scenario = feature.CreateNode(context.ScenarioInfo.Title);
                var driverManager = _objectContainer.Resolve<DriverManager>();
                var webDriver = driverManager.GetDriver();
                var explicitWait = new ExplicitWait(webDriver);
                var jsExecutor = new JsExecutor(webDriver);
                _objectContainer.RegisterInstanceAs<EventFiringWebDriver>(webDriver);
                _objectContainer.RegisterInstanceAs<ExplicitWait>(explicitWait);
                _objectContainer.RegisterInstanceAs<JsExecutor>(jsExecutor);
                Console.WriteLine("Printing url: ");
                Console.WriteLine(ConfigurationReader.GetTestConfiguration().Environment.URL);
                webDriver.Navigate().GoToUrl(ConfigurationReader.GetTestConfiguration().Environment.URL);
                webDriver.Manage().Window.Maximize();
                //webDriver.Manage().Window.Size = new Size(1366, 768);
                // webDriver.Manage().Window.Size = new Size(1920, 1080);
            }
            else
            {
                scenario = feature.CreateNode(context.ScenarioInfo.Title);
            }
        }

        [BeforeStep]
        public void BeforeStep()
        {
            step = scenario;
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context, FeatureContext featureContext)
        {
            if (featureContext.FeatureInfo.Title != "API Testing")
            {
                var driverManager = _objectContainer.Resolve<DriverManager>();
                var webDriver = driverManager.GetDriver();
                var screenshot = new Screenshot(webDriver);
                _objectContainer.RegisterInstanceAs<Screenshot>(screenshot);
                if (context.TestError == null)
                {
                    var mediaEntity = screenshot.CaptureScreenshotAndReturnModel(context.ScenarioInfo.Title.Trim());
                    step.Log(AventStack.ExtentReports.Status.Pass, context.StepContext.StepInfo.Text, mediaEntity);

                }
                else if (context.TestError != null)
                {
                    var mediaEntity = screenshot.CaptureScreenshotAndReturnModel(context.ScenarioInfo.Title.Trim());
                    step.Log(AventStack.ExtentReports.Status.Fail, context.StepContext.StepInfo.Text, mediaEntity);
                    step.Log(AventStack.ExtentReports.Status.Fail, "The error message is " + context.TestError.Message);
                    string[] strings = context.TestError.StackTrace.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string message in strings)
                    {
                        if (message.Contains("Page.cs:line"))
                        {
                            step.Log(AventStack.ExtentReports.Status.Fail, "The locator element  needs to be modified" + message);
                        }
                    }
                }
            }
            else
            {
                if (context.TestError == null)
                {
                    step.Log(AventStack.ExtentReports.Status.Pass, context.StepContext.StepInfo.Text);
                }
                else if (context.TestError != null)
                {
                    step.Log(AventStack.ExtentReports.Status.Fail, context.StepContext.StepInfo.Text);
                }
            }
        }

        [AfterScenario]
        public void CleanUpWebDriver(FeatureContext featureContext)
        {
            if (featureContext.FeatureInfo.Title != "API Testing")
            {
                var webDriver = _objectContainer.Resolve<EventFiringWebDriver>();
                webDriver.Quit();
            }

        }

        [AfterFeature]
        public static void AfterFeature()
        {
            extent.Flush();
        }
    }
}