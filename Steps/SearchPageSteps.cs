using FluentAssertions;
using MicroappPlatformQaAutomation.Core.Commons;
using MicroappPlatformQaAutomation.Model;
using MicroappPlatformQaAutomation.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Assert = NUnit.Framework.Assert;

namespace MicroappPlatformQaAutomation
{
    [Binding]
    internal class SearchPageSteps
    {
        private readonly static string yamlSearchTestDataFilePath = "Resources/TestData/SearchTestData.yaml";
        private readonly ExplicitWait _explicitWait;
        private readonly SearchPage _searchPage;

        public SearchPageSteps(SearchPage searchPage, ExplicitWait explicitWait)
        {
            _searchPage = searchPage;
            _explicitWait = explicitWait;
        }

        [When(@"the user enters text in the search bar")]
        public void WhenTheUserEntersTextInTheSearchBar()
        {
            _searchPage.SearchBar.Click();
            string validInputText = YamlReader.FetchYamlData<SearchContentDTO>(yamlSearchTestDataFilePath).ValidSearchInput;
            _searchPage.SearchTextBox.Click();
            _searchPage.SearchTextBox.SendKeys(validInputText);
        }

        [Then(@"the user should see five relavant suggestions in dropdown")]
        public void ThenTheUserShouldSeeFiveRelavantSuggestionsInDropdown()
        {
            bool typeaheadDisplayed = _searchPage.SearchSuggestionsBox.Displayed;
            Assert.That(typeaheadDisplayed, Is.True);
            _explicitWait.Until(_searchPage.ViewAllLink);
            _searchPage.SearchSuggestionsList.Count.Should().Be(6);
        }

        [Then(@"the user should see the view all link")]
        public void ThenTheUserShouldSeeTheViewAllLink()
        {
            bool linkDisplayed = _searchPage.ViewAllLink.Displayed;
            Assert.That(linkDisplayed, Is.True);
        }

        [When(@"the user enters invalid text in the search bar")]
        public void WhenTheUserEntersInvalidTextInTheSearchBar()
        {
            string invalidInputText = YamlReader.FetchYamlData<SearchContentDTO>(yamlSearchTestDataFilePath).InvalidSearchInput;
            _searchPage.SearchTextBox.Click();
            _searchPage.SearchTextBox.SendKeys(invalidInputText);
        }

        [Then(@"the user should see no results notification")]
        public void ThenTheUserShouldSeeNoResultsNotification()
        {
            _explicitWait.Until(_searchPage.NoResultsMessage);
            bool messageDisplayed = _searchPage.NoResultsMessage.Displayed;
            Assert.That(messageDisplayed, Is.True);
        }
    }
}



