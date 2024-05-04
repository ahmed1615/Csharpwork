using FluentAssertions;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;
using Assert = NUnit.Framework.Assert;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    internal class InternalUserProfilePageSteps
    {
        private readonly EventFiringWebDriver _driver;
        private readonly static string yamlProfilePageTestDataFilePath = "Resources/TestData/ProfilePageTestData.yaml";
        private readonly ExplicitWait _explicitWait;
        private readonly JsExecutor _jsExecutor;
        private readonly InternalUserProfilePage _internalUserProfilePage;

        public InternalUserProfilePageSteps(EventFiringWebDriver webDriver, InternalUserProfilePage internalUserProfilePage, ExplicitWait explicitWait, JsExecutor jsExecutor)
        {
            _driver = webDriver;
            _internalUserProfilePage = internalUserProfilePage;
            _explicitWait = explicitWait;
            _jsExecutor = jsExecutor;
        }

        [Then(@"the user lands on profile page")]
        public void ThenTheUserLandsOnProfilePage()
        {
            bool textDisplayed = _internalUserProfilePage.MyProfileText.Displayed;
            Assert.That(textDisplayed, Is.True);
        }

        [Then(@"the user validates contribution tab")]
        public void ThenTheUserValidatesContributionTab()
        {
            _internalUserProfilePage.ContributionsTab.Click();
            bool btnDisplayed = _internalUserProfilePage.ContributeNowButton.Displayed;
            Assert.That(btnDisplayed, Is.True);
            bool titleDisplayed = _internalUserProfilePage.ContributionTitle.Displayed;
            Assert.That(btnDisplayed, Is.True);
            string expectedContributionTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionTitle;
            Assert.AreEqual(_internalUserProfilePage.ContributionTitle.Text, expectedContributionTitle);
            bool descriptionDisplayed = _internalUserProfilePage.ContributionDescription.Displayed;
            Assert.That(btnDisplayed, Is.True);
            string expectedContributionDescription = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionDescription;
            string expectedContributionNameHeader = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionNameHeader;
            string expectedContributionDescriptionHeader = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionDescriptionHeader;
            string expectedContributionSubmissionDateHeader = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionSubmissionDateHeader;
            string expectedContributionStateHeader = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ContributionStateHeader;
            Assert.AreEqual(_internalUserProfilePage.ContributionDescription.Text, expectedContributionDescription);
            Assert.AreEqual(_internalUserProfilePage.ContributionNameHeader.Text, expectedContributionNameHeader);
            Assert.AreEqual(_internalUserProfilePage.ContributionDescriptionHeader.Text, expectedContributionDescriptionHeader);
            Assert.AreEqual(_internalUserProfilePage.ContributionDateHeader.Text, expectedContributionSubmissionDateHeader);
            Assert.AreEqual(_internalUserProfilePage.ContributionStateHeader.Text, expectedContributionStateHeader);
        }

        [Then(@"the user verifies the color and state of contribution items")]
        public void ThenTheUserVerifiesTheColorAndStateOfContributionItems()
        {
            _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
            var js = (IJavaScriptExecutor)_driver;
            string InReviewColor = js.ExecuteScript("return window.getComputedStyle(arguments[0],':after').getPropertyValue('background-color');", _internalUserProfilePage.ContributionInReviewStatus).ToString();
            string draftColor = js.ExecuteScript("return window.getComputedStyle(arguments[0],':after').getPropertyValue('background-color');", _internalUserProfilePage.ContributionDraftStatus).ToString();
            string submittedColor = js.ExecuteScript("return window.getComputedStyle(arguments[0],':after').getPropertyValue('background-color');", _internalUserProfilePage.ContributionSubmittedStatus).ToString();
            string approvedColor = js.ExecuteScript("return window.getComputedStyle(arguments[0],':after').getPropertyValue('background-color');", _internalUserProfilePage.ContributionApprovedStatus).ToString();
            Assert.AreEqual(InReviewColor, YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).InReviewRGBCode);
            Assert.AreEqual(draftColor, YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).DraftRGBCode);
            Assert.AreEqual(submittedColor, YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).SubmittedRGBCode);
            Assert.AreEqual(approvedColor, YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).ApprovedRGBCode);
        }

        [Then(@"the user verifies the pagination of contribution items")]
        public void ThenTheUserVerifiesThePaginationOfContributionItems()
        {
            int totalPage = int.Parse(_internalUserProfilePage.PaginationSize.Text);
            if (totalPage > 1)
            {
                _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                _internalUserProfilePage.ContributionRows.Count.Should().Be(5);
                bool firstPage = _internalUserProfilePage.FirstPageButton.Enabled;
                Assert.False(firstPage);
                bool previousPage = _internalUserProfilePage.PreviousPageButton.Enabled;
                Assert.False(previousPage);
                int currentPage = 0;
                while (currentPage < totalPage)
                {
                    _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                    currentPage = int.Parse(_internalUserProfilePage.CurrentPage.GetAttribute("value"));
                    _jsExecutor.JSClick(_internalUserProfilePage.NextPageButton);
                }
                _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                bool lastPage = _internalUserProfilePage.LastPageButton.Enabled;
                Assert.False(lastPage);
                bool nextPage = _internalUserProfilePage.NextPageButton.Enabled;
                Assert.False(nextPage);
                int currentPageAfterClick = 0;
                while (currentPageAfterClick > 1)
                {
                    currentPageAfterClick = int.Parse(_internalUserProfilePage.CurrentPage.GetAttribute("value"));
                    _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                    _jsExecutor.JSClick(_internalUserProfilePage.PreviousPageButton);
                }
                Assert.False(firstPage);
                Assert.False(previousPage);
                _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                ThenTheUserVerifiesTheFirstAndLastPageButton();
            }
        }

        [Then(@"the user verifies the first and last page button")]
        public void ThenTheUserVerifiesTheFirstAndLastPageButton()
        {
            int totalPage = int.Parse(_internalUserProfilePage.PaginationSize.Text);
            _jsExecutor.JSClick(_internalUserProfilePage.LastPageButton);
            int currentPage = int.Parse(_internalUserProfilePage.CurrentPage.GetAttribute("value"));
            Assert.AreEqual(currentPage, totalPage);
            _jsExecutor.JSClick(_internalUserProfilePage.FirstPageButton);
            int currentPageAfterClick = int.Parse(_internalUserProfilePage.CurrentPage.GetAttribute("value"));
            Assert.AreEqual(currentPageAfterClick, 1);
        }

        [Then(@"the user verifies the single page result of contribution items")]
        public void ThenTheUserVerifiesTheSinglePageResultOfContributionItems()
        {
            int totalPage = int.Parse(_internalUserProfilePage.PaginationSize.Text);
            if (totalPage == 1)
            {
                _jsExecutor.ScrollTillLast(_internalUserProfilePage.FirstPageButton);
                bool firstPage = _internalUserProfilePage.FirstPageButton.Enabled;
                Assert.False(firstPage);
                bool previousPage = _internalUserProfilePage.PreviousPageButton.Enabled;
                Assert.False(previousPage);
                bool lastPage = _internalUserProfilePage.LastPageButton.Enabled;
                Assert.False(lastPage);
                bool nextPage = _internalUserProfilePage.NextPageButton.Enabled;
                Assert.False(nextPage);
            }
        }

        [Then(@"the user lands on Profile page and validating About Me section")]
        public void ThenTheUserLandsOnProfilePageAndValidatingAboutMeSection()
        {
            String aboutMeTitleText = _internalUserProfilePage.AboutMeText.Text;
            string aboutMeText = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).AboutMeText;
            Assert.AreEqual(aboutMeText, aboutMeTitleText);
        }

        [When(@"the user clicks on Edit profile button")]
        public void WhenTheUserClicksOnEditProfileButton()
        {
            _internalUserProfilePage.EditProfileButton.Click();
        }

        [Then(@"the user sees the exisiting areas of expertise in ask me about interest areas and team dropdown and delete each records and save them")]
        public void ThenTheUserSeesTheExisitingAreasOfExpertiseInAskMeAboutInterestAreasAndTeamDropdownAndDeleteEachRecordsAndSaveThem()
        {
            IList<IWebElement> allAreas = _internalUserProfilePage.AllAreasOfExpertise;
            foreach (IWebElement element in allAreas)
            {
                try
                {
                    element.Click();
                }
                catch (StaleElementReferenceException e)
                {
                }
            }
            _internalUserProfilePage.MyAreasOfInterestSaveButton.Click();
        }

        [Then(@"the user sees the editable fields of organization,Team name,role,logged in date")]
        public void ThenTheUserSeesTheEditableFieldsOfOrganizationTeamNameRoleLoggedInDate()
        {
            foreach (IWebElement element in _internalUserProfilePage.AboutMeTextBoxes)
            {
                String textBoxValue = element.GetAttribute("value");
                int length = textBoxValue.Length;
                for (int i = 0; i < length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
            string orgFieldTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).Organization;
            string teamNameFieldTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).TeamName;
            string roleFieldTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).Role;
            string loggedInSinceFieldTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).LoggedInSince;
            _internalUserProfilePage.OrganizationFieldbox.SendKeys(orgFieldTestData);
            _internalUserProfilePage.TeamNameFieldbox.SendKeys(teamNameFieldTestData);
            _internalUserProfilePage.RoleFieldbox.SendKeys(roleFieldTestData);
            _internalUserProfilePage.LoggedInDateFieldbox.SendKeys(loggedInSinceFieldTestData);
        }

        [Then(@"the user clicks on the save button")]
        public void ThenTheUserClicksOnTheSaveButton()
        {
            _internalUserProfilePage.SaveButton.Click();
        }

        [Then(@"the user could able to see the saved record upon profile updated successfully message with close mark")]
        public void ThenTheUserCouldAbleToSeeTheSavedRecordUponProfileUpdatedSuccessfullyMessageWithCloseMark()
        {
            String aboutMeTitleText = _internalUserProfilePage.AboutMeSuccessMessage.Text;
            string aboutMeMessageTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).AboutMeSuccessMessage;
            Assert.AreEqual(aboutMeMessageTestData, aboutMeTitleText);
        }
        //the correct method for my view items access 
        [When(@"the user wants to go to my view items")]
        public void theuserwantstogotomyviewitems()
        {
            _internalUserProfilePage.MyViewedItem.Click();
        }

        [Then(@"the user lands on Profile page and validating Areas of Interest section")]
        public void ThenTheUserLandsOnProfilePageAndValidatingAreasOfInterestSection()
        {
            _explicitWait.Until(_internalUserProfilePage.MyAreasOfInterestButton);
            _internalUserProfilePage.MyAreasOfInterestButton.Click();
            string aboutMeTitleTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).MyAreasOfInterestTitle;
            String myAreaOfInterestTitleText = _internalUserProfilePage.MyAreasOfInterestText.Text;
            Assert.AreEqual(aboutMeTitleTestData, myAreaOfInterestTitleText);
        }

        [When(@"the user clicks on Edit Areas button")]
        public void WhenTheUserClicksOnEditAreasButton()
        {
            _internalUserProfilePage.EditAreasButton.Click();
        }

        [Then(@"the user sees the ask me about interest area team role dropdowns and click close mark to delete all areas of expertise")]
        public void ThenTheUserSeesTheAskMeAboutInterestAreaTeamRoleDropdownsAndClickCloseMarkToDeleteAllAreasOfExpertise()
        {
            IList<IWebElement> closeMarks = _internalUserProfilePage.CloseMark;
            foreach (IWebElement element in closeMarks)
            {
                try
                {
                    element.Click();
                }
                catch (StaleElementReferenceException e)
                {

                }
            }
        }

        [Then(@"the user clicks the save button")]
        public void ThenTheUserClicksTheSaveButton()
        {
            _internalUserProfilePage.MyAreasOfInterestSaveButton.Click();
        }

        [Then(@"the user click the add Areas button to areas of expertise")]
        public void ThenTheUserClickTheAddAreasButtonToAreasOfExpertise()
        {
            try
            {
                Thread.Sleep(2000);
                _jsExecutor.JSClick(_internalUserProfilePage.AddAreasButton);
            }
            catch (StaleElementReferenceException e)
            {

            }
        }

        [Then(@"by clicking the dropdown the user adds the areas of expertise and clicks save button")]
        public void ThenByClickingTheDropdownTheUserAddsTheAreasOfExpertiseAndClicksSaveButton()
        {
            IList<IWebElement> allDropDowns = _internalUserProfilePage.DropDownButtons;
            foreach (IWebElement element in allDropDowns)
            {
                try
                {
                    _jsExecutor.JSClick(_internalUserProfilePage.MyAreasOfInterestText);
                    element.Click();
                    IList<IWebElement> allAreas = _internalUserProfilePage.AllAreas;
                    foreach (IWebElement element1 in allAreas)
                    {
                        string text = element1.Text;
                        if (text.Equals("Area 1") || text.Equals("Area 2"))
                        {
                            element1.Click();
                        }

                        else if (text.Equals("Area 9") || text.Equals("Area 10"))
                        {
                            element1.Click();
                        }

                        else if (text.Equals("Area 14") || text.Equals("Area 15"))
                        {
                            element1.Click();
                        }
                    }
                }
                catch (StaleElementReferenceException e)
                {

                }
            }
        }

        [Then(@"the user could see the saved record")]
        public void ThenTheUserCouldSeeTheSavedRecord()
        {
            _internalUserProfilePage.MyAreasOfInterestSaveButton.Click();
            string myAreaOfInterestSuccessText = _internalUserProfilePage.MyAreasOfInterestSuccessMessage.Text;
            string myAreaofInterestMessageTestData = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).MyAreasOfInterestSuccessMessage;
            Assert.AreEqual(myAreaofInterestMessageTestData, myAreaOfInterestSuccessText);
        }

        [Then(@"the user validates the My Viewed item tab")]
        public void ThenTheUserValidatesTheMyViewedItemTab()
        {
            _internalUserProfilePage.MyViewedItemsButton.Click();
            bool titleDisplayed = _internalUserProfilePage.MyViewedItemsTitle.Displayed;
            Assert.That(titleDisplayed, Is.True);
            string expectedMyViewedItemsTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).MyViewedItemsTitle;
            Assert.AreEqual(_internalUserProfilePage.MyViewedItemsTitle.Text, expectedMyViewedItemsTitle);
        }

        [Then(@"the user verifies the catalog items which are marked as fovorites is tagged with yellow label mark")]
        public void ThenTheUserVerifiesTheCatalogItemsWhichAreMarkedAsFovoritesIsTaggedWithYellowLabelMark()
        {
            string expectedColor = _internalUserProfilePage.YellowBookmark.GetCssValue("fill");
            string expectedBookmarkColor = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).BookmarkRGBCode;
            Assert.AreEqual(expectedBookmarkColor, expectedColor);
        }

        [Then(@"the user validates the title of each containers in My Viewed Items")]
        public void ThenTheUserValidatesTheTitleOfEachContainersInMyViewedItems()
        {
            string expectedCatalogItemTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).CatalogItemTitle;
            Assert.AreEqual(_internalUserProfilePage.CatalogItem.Text, expectedCatalogItemTitle);
            string expectedAreaOfInterestTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).AreaOfInterestTitle;
            Assert.AreEqual(_internalUserProfilePage.MyAreasOfInterest.Text, expectedAreaOfInterestTitle);
            string expectedDescriptionTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).DescriptionTitle;
            Assert.AreEqual(_internalUserProfilePage.Description.Text, expectedDescriptionTitle);
        }

        [Then(@"the user verifies the presence of try now link")]
        public void ThenTheUserVerifiesThePresenceOfTryNowLink()
        {
            IList<IWebElement> allTryNow = _internalUserProfilePage.TryNowLink;
            foreach (IWebElement element in allTryNow)
            {
                bool expectedTryNow = element.Displayed;
                Assert.That(expectedTryNow, Is.True);
            }
        }

        [Then(@"the user clicks the My Favorites tab")]
        public void ThenTheUserClicksTheMyFavoritesTab()
        {
            _internalUserProfilePage.MyFavoritesTab.Click();
        }

        [Then(@"the user validates the title of My Favorites")]
        public void ThenTheUserValidatesTheTitleOfMyFavorites()
        {
            string expectedMyFavoritesTitle = YamlReader.FetchYamlData<ProfilePageContentDTO>(yamlProfilePageTestDataFilePath).MyFavoritesTitle;
            Assert.AreEqual(_internalUserProfilePage.MyFavoritesTitle.Text, expectedMyFavoritesTitle);
        }

        //back, firstpage, next, last page 
        [When(@"the user wants to go to click on ""(.*)"" button")]
        public void theuserwantstogotoclickonbutton(String page)
        {
            var firstPageValueBeforClick = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
            var LastPageValueBeforeclick = _internalUserProfilePage.NumberOfPagination.GetAttribute("innerHTML");
            try
            {
                if (page == "back to the first page" || page == "BACK TO THE FIRST PAGE")
                {
                    if (Convert.ToInt32(firstPageValueBeforClick) == 1)
                    {
                        try
                        {
                            _jsExecutor.JSClick(_internalUserProfilePage.FirstPageButton);
                        }
                        catch (ElementClickInterceptedException ex)
                        {
                        }
                    }
                    else if (Convert.ToInt32(firstPageValueBeforClick) > 1)
                    {
                        _jsExecutor.JSClick(_internalUserProfilePage.FirstPageButton);
                        var FirstPageNumber = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
                        Assert.AreEqual(Convert.ToInt32(FirstPageNumber), 1);
                    }
                }
                else if (page == "back" || page == "BACK")
                {
                    if (Convert.ToInt32(firstPageValueBeforClick) == 1)
                    {
                        try
                        {
                            _jsExecutor.JSClick(_internalUserProfilePage.PreviousPageButton);
                        }
                        catch (ElementClickInterceptedException ex)
                        {
                        }
                    }
                    else if (Convert.ToInt32(firstPageValueBeforClick) > 1)
                    {
                        _jsExecutor.JSClick(_internalUserProfilePage.PreviousPageButton);
                        Actions actions = new Actions(_driver);
                        var FirstPageNumberafterback = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
                        Assert.AreEqual(Convert.ToInt32(FirstPageNumberafterback) + 1, Convert.ToInt32(firstPageValueBeforClick));
                    }
                }
                else if (page == "next" || page == "NEXT")
                {
                    if (Convert.ToInt32(firstPageValueBeforClick) == 1)
                    {
                        _jsExecutor.JSClick(_internalUserProfilePage.NextPageButton);
                        var FirstPageNumber = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
                        var LastPageNumber = _internalUserProfilePage.NumberOfPagination.GetAttribute("innerHTML");
                        Assert.AreEqual(Convert.ToInt32(LastPageNumber), Convert.ToInt32(FirstPageNumber));
                    }
                    else if (Convert.ToInt32(firstPageValueBeforClick) != 1)
                    {
                        _jsExecutor.JSClick(_internalUserProfilePage.NextPageButton);
                        var FirstPageNumber = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
                        var LastPageNumber = _internalUserProfilePage.NumberOfPagination.GetAttribute("innerHTML");
                        Assert.AreEqual(Convert.ToInt32(firstPageValueBeforClick) + 1, Convert.ToInt32(FirstPageNumber));
                        Assert.AreEqual(Convert.ToInt32(LastPageValueBeforeclick), Convert.ToInt32(LastPageNumber));
                    }
                }
                else if (page == "next to the last page" || page == "NEXT TO THE LAST PAGE")
                {
                    _jsExecutor.JSClick(_internalUserProfilePage.LastPageButton);
                    var FirstPageNumber = _internalUserProfilePage.NumberOfFirstPage.GetAttribute("innerHTML");
                    var LastPageNumber = _internalUserProfilePage.NumberOfPagination.GetAttribute("innerHTML");
                    Assert.AreEqual(Convert.ToInt32(LastPageNumber), Convert.ToInt32(FirstPageNumber));
                }
            }
            catch (ElementClickInterceptedException ex)
            {
            }
        }
    }
}