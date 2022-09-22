using GoogleMapAutomationProject.GoogleMapPageObjectModel;
using GoogleMapAutomationProject.SetUpBaseClass;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;

namespace GoogleMapAutomationProject
{
    public class Tests: SetUpGoogleMapTest
    {
        GoogleMapPageObject googleMapPageObject;
        [SetUp]
        public void GetObjectFiles()
        {
            googleMapPageObject = new GoogleMapPageObject(SetUpGoogleMapTest.driver);
        }

        /// <summary>
        ///This Test Case will verify the basic Ui web elements are present on the web page 
        ///Will Verify Searchbox, Search Icon, Zoom in Zoom out, Directions Buton etc. 
        /// </summary>
        [Test]
     
        public void BasicUIMapElementsVerification()
        {
            //Passing browsername in the onetime setup method so that it will verify te cross browser testing 
            
            string url = driver.Url;
            Assert.AreEqual("https://www.google.com/maps", url);
            googleMapPageObject.VerifyMapUIFunctionalities();
        }

        /// <summary>
        ///Scenario: It will search for a text in the text Box
        ///Will clear the text box afterwards to verify clear search button is working and will clear the text box
        ///again enter the text and will search
        ///Also Verify whether you are able to see the suggestions while entering a place
        /// </summary>


        [Test]
        public void EnterAndClearSearchFromSearchBox()
        {
            string placeName = "Berlin";
            googleMapPageObject.SearchBox.SendKeys(placeName);
            #region  Verify the Suggestions are visible 
            int[] placeSuggestion = new int[3] { 1, 3, 5 };
            for (int i = 0; i < placeSuggestion.Length; i++)
            {
                Assert.IsTrue(googleMapPageObject.PlaceSuggestions(placeSuggestion[i]).Displayed);
            }
            #endregion
            #region Clear the Text from the search box and again enter the text 
            Assert.IsTrue(googleMapPageObject.SearchButtonIcon.Displayed);
            googleMapPageObject.SearchButtonIcon.Click();
            Assert.IsTrue(googleMapPageObject.ClearSearch.Displayed);
            googleMapPageObject.ClearSearch.Click();
            Assert.IsTrue(googleMapPageObject.SearchBox.Text.Contains(""));
            #endregion
        }

        ///<summary>
        ///Scenario: Search for 4 different places in the search box
        ///And verify that those places are visible on the map 
        /// </summary>
        [Test]
        public void EnterDifferentPlacesAndVerify()
        {
            #region Prepare a list of the places and pass it to the function 
            List<string> placeNames = new List<string>()
            {"Berlin",
              "London",
              "Netherlands",
              "Paris"
            };
            googleMapPageObject.SearchAndVerifyPlacesAreVisible(placeNames);
            #endregion
        }

        ///<summary>
        ///Method verifies that we have entered valid destinations
        ///And verify the details as well. 
        ///</summary>
        [Test]
        public void VerifyAndSearchForValidDirections()
        {
            // clicking on Directions Button 
            string StartPlace = "Berlin, Germany";
            string EndPlace = "Brunswick, Germany";
            Assert.IsTrue(googleMapPageObject.DirectionsButton.Displayed);
            googleMapPageObject.DirectionsButton.Click();
            #region enter starting and ending position and search 
            googleMapPageObject.CheckAndVerifyValidDistanceDetails(StartPlace, EndPlace);
            #endregion
        }

        /* --------------------------------- Negative Scenarios for the Google Map ------------------------------- */

        ///<summary>
        ///Scenario : Verify that for invalid place input 
        ///Google Map will show Google Maps can't find 'PlaceName'
        /// </summary>

        [Test]
        public void VerifyInvalidInputPlaceResult()
        {
            string invalidPLace = "dffhrgttyrvryrgyrgfddfdfdfdfd";
            #region Search for an invalid place
            googleMapPageObject.SearchBox.SendKeys(invalidPLace);
            googleMapPageObject.SearchButtonIcon.Click();
            #endregion
            #region Verify for the Google map can't found search result 
            Assert.IsTrue(googleMapPageObject.PlaceNotFound(invalidPLace).Displayed);
            Assert.IsTrue(googleMapPageObject.VerifyInvalidPlaceResult.Displayed);
            #endregion
        }

        ///<summary>
        ///Click on the directions button 
        ///Enter Destination and Source Place like : India to Germany 
        ///Verify the message "Sorry, we could not calculate directions from "India" to "Germany""
        /// </summary>

        [Test]
        public void VerifyAndSearchForInvlaidDirections()
        {
            // clicking on Directions Button 
            string StartPlace = "India";
            string EndPlace = "Germany";
            Assert.IsTrue(googleMapPageObject.DirectionsButton.Displayed);
            googleMapPageObject.DirectionsButton.Click();
            #region enter starting and ending position and search 
            googleMapPageObject.CheckAndVerifyInvalidDistanceMessage(StartPlace, EndPlace);
            #endregion
        }
    }
}