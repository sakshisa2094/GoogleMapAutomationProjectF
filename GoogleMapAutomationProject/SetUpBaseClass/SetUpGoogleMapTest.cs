using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GoogleMapAutomationProject.SetUpBaseClass
{

    public class SetUpGoogleMapTest
    {
        public TestContext TestContext { get; set; }
        public static IWebDriver driver;


        ///<summary>
        ///One time Setup method that will be exceuted before execution of test cases begin
        ///For example : If there are 50 Test Cases in the Test Class 
        ///Then there will not be any need to initialize the browser again and again for each test case. 
        /// </summary>
        [OneTimeSetUp]
        public void OpenGoogleMap()
        {
            switch (TestContext.Parameters.Get("browserType").ToString())
           // switch (BrowserName) 
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;

                case "FireFox":
                    driver = new FirefoxDriver();
                    break;

                case "Edge":
                    driver = new EdgeDriver();
                    break;

                default:
                    throw new ArgumentException("Browser Not implemented");

            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(TestContext.Parameters.Get("baseQAUrl").ToString());

        }

        // One time Tear Down Method will be executed after the execution of all the test cases in the test class and it will 
        // ensure to close the Driver or browser in the end pf execution of all test cases. 
        [OneTimeTearDown]
        public void CloseGoogleMaps()
        {
            
            driver.Close();
        }
        public static IEnumerable<String> RunWithBrowser()
        {
            String[] browsers = { "Chrome", "Firefox" };
            foreach(string b in browsers)
            {
                yield return b;
            }
        }
    }
}
