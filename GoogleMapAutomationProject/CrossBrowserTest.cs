using System;
using System.Collections.Generic;
using System.Text;
using GoogleMapAutomationProject.GoogleMapPageObjectModel;
using GoogleMapAutomationProject.SetUpBaseClass;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace GoogleMapAutomationProject
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    class CrossBrowserTest<CrossBrowser> where CrossBrowser: IWebDriver, new()
    {
        public IWebDriver driver;
        GoogleMapPageObject googleMapPageObject;
        /// <summary>
        ///This Test Case will verify the basic Ui web elements are present on the web page 
        ///Will Verify Searchbox, Search Icon, Zoom in Zoom out, Directions Buton etc. 
        /// </summary>
        [Test]

        public void BasicUIMapElementsVerification()
        {
            #region Initializing the Browser instance and page object instance 
            driver = new CrossBrowser();
            googleMapPageObject = new GoogleMapPageObject(driver);
            #endregion

            #region Verify the Ui Functionalities 
            string baseUrl = "https://www.google.com/maps";
            driver.Navigate().GoToUrl(baseUrl);
            driver.Manage().Window.Maximize();
            string url = driver.Url;
            Assert.AreEqual("https://www.google.com/maps", url);
            googleMapPageObject.VerifyMapUIFunctionalities();
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }


    }
}
