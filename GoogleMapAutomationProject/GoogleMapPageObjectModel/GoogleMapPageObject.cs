using GoogleMapAutomationProject.SetUpBaseClass;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace GoogleMapAutomationProject.GoogleMapPageObjectModel
{
    public class GoogleMapPageObject : SetUpGoogleMapTest
    {
        private WebDriverWait wait;
        public GoogleMapPageObject(IWebDriver driver)
        {
            GoogleMapPageObject.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // This part will contain all the web locators or webelements present on the web page 

        public IWebElement SearchBox => wait.Until(ExpectedConditions.ElementExists(By.Id("searchboxinput")));
        public IWebElement SearchButtonIcon => wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("searchbox-searchbutton")));
        public IWebElement ClearSearch => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@tooltip='Clear search']")));
        public IWebElement SatelliteView => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='mylocation']//button[@id='sVuEFc']")));
        public IWebElement ZoomIn => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='zoom']//button[@aria-label='Zoom in']")));
        public IWebElement ZoomOut => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='zoom']//button[@aria-label='Zoom out']")));
        public IWebElement DirectionsButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@aria-label='Directions']")));
        public IWebElement PlaceSuggestions(int Index) => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='sbsg50']//div[" + Index + "]")));
        public IWebElement VerifyPlaceVisibleOnMap(string PlaceName) => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='QA0Szd']//h1//span[contains(text(),'" + PlaceName + "')]")));
        public IWebElement PlaceNotFound(string PlaceName) => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='QA0Szd']//i[contains(text(),'" + PlaceName + "')]")));
        public IWebElement VerifyInvalidPlaceResult => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(text(),'Make sure your search is spelled correctly.')]")));
        public IWebElement StartingPosition => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='sb_ifc51']//input")));
        public IWebElement EndingPosition => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='sb_ifc52']//input")));
        public IWebElement ErrorMessage => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//jsl[contains(text(),'Sorry, we could not calculate directions from')]")));
        public IWebElement DirectionsSearchButton => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='directions-searchbox-1']//button[@aria-label='Search']")));
        public IWebElement DistanceVerify => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//h1[@id='section-directions-trip-title-0']")));
        public IWebElement DistanceDetails => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@aria-label='Directions from Berlin, Germany to Brunswick, Germany']")));
        public IWebElement VerifyPlacesNameInTitle(string StartPlace, string EndPlace) => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[contains(text(),'from')]//span[contains(text(),'" + StartPlace + "')]/following::div//span[contains(text(),'" + EndPlace + "')]")));






        /* This Part will contain all the methods 
         * That are associated with the particular action that needs to be perfromed on the web element
         */

        /// <summary>
        /// Method to Verify search box and Search icon are visible over the Web Page 
        /// And We are able to enter text into the search box
        /// </summary>
        /// <param name="LocationName"></param>
        public void SearchForALocation(string LocationName)
        {
            Assert.IsTrue(SearchBox.Displayed);
            SearchBox.SendKeys(LocationName);
            Assert.IsTrue(SearchButtonIcon.Displayed);
            SearchButtonIcon.Click();
        }

        /// <summary>
        /// Method will verify the basic UI functionalities of the webpage
        /// Like presence of buttons, search icon, Zoom in etc. 
        /// </summary>

        public void VerifyMapUIFunctionalities()
        {
            Thread.Sleep(1000);
            Assert.IsTrue(SearchBox.Displayed);
            Assert.IsTrue(SearchButtonIcon.Displayed);
            Assert.IsTrue(DirectionsButton.Displayed);
            Assert.IsTrue(ZoomIn.Displayed);
            Assert.IsTrue(ZoomOut.Displayed);
            Assert.IsTrue(SatelliteView.Displayed);
        }

        ///<summary>
        ///Method will iterate over the 4 different places
        ///And verify that the place name is visible over the Map 
        /// </summary>

        public void SearchAndVerifyPlacesAreVisible(List<string> placeName)
        {
            foreach (var place in placeName)
            {
                SearchBox.SendKeys(place);
                SearchButtonIcon.Click();
                Thread.Sleep(1000);
                Assert.IsTrue(VerifyPlaceVisibleOnMap(place).Displayed);
                ClearSearch.Click();
            }
        }

        ///<summary>
        ///Method to check for the invalid distance measurement between Two places 
        ///And Verifying the error message 
        /// </summary>

        public void CheckAndVerifyInvalidDistanceMessage(string StartingPlace, string EndPlace)
        {
            //Enter Text in Input fields
            Assert.IsTrue(StartingPosition.Displayed);
            StartingPosition.SendKeys(StartingPlace);
            Assert.IsTrue(EndingPosition.Displayed);
            EndingPosition.SendKeys(EndPlace);

            //CLick on the search button 
            Assert.IsTrue(DirectionsSearchButton.Displayed);
            DirectionsSearchButton.Click();
            // Introduced Sleep as pagen is taking time to load properly
            Thread.Sleep(1000);
            Assert.IsTrue(ErrorMessage.Displayed);

        }

        ///<summary>
        ///Method to check for the valid distance measurement between Two places 
        ///And Verifying the details 
        /// </summary>

        public void CheckAndVerifyValidDistanceDetails(string StartingPlace, string EndPlace)
        {
            //Enter Text in Input fields
            Assert.IsTrue(StartingPosition.Displayed);
            StartingPosition.SendKeys(StartingPlace);
            Assert.IsTrue(EndingPosition.Displayed);
            EndingPosition.SendKeys(EndPlace);

            //CLick on the search button 
            Assert.IsTrue(DirectionsSearchButton.Displayed);
            DirectionsSearchButton.Click();

            // Introduced Sleep as page is taking time to load properly
            Thread.Sleep(1000);
            Assert.IsTrue(DistanceVerify.Displayed);
            DistanceVerify.Click();
            Assert.IsTrue(VerifyPlacesNameInTitle(StartingPlace, EndPlace).Displayed);
            Assert.IsTrue(DistanceDetails.Displayed);

        }


    }
}
