using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System.Collections.Generic;

namespace MicroappPlatformQaAutomation.Pages
{
    public class InternalUserProfilePage
    {
        private readonly EventFiringWebDriver _driver;

        public InternalUserProfilePage(EventFiringWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement AboutMeText => _driver.FindElement(By.XPath("//h1[text()='About Me']"));
        public IWebElement EditProfileButton => _driver.FindElement(By.XPath("//button[text()='Edit Profile']"));
        public IWebElement OrganizationFieldbox => _driver.FindElement(By.Id("motif-input-0"));
        public IWebElement TeamNameFieldbox => _driver.FindElement(By.Id("motif-input-1"));
        public IWebElement RoleFieldbox => _driver.FindElement(By.Id("motif-input-2"));
        public IWebElement LoggedInDateFieldbox => _driver.FindElement(By.Id("motif-input-3"));
        public IWebElement SaveButton => _driver.FindElement(By.XPath("//button[text()='Save']"));
        public IWebElement AlertStayButton => _driver.FindElement(By.XPath("//button[text()='Stay']"));
        public IWebElement AlertLeaveButton => _driver.FindElement(By.XPath("//button[text()='Leave']"));
        public IWebElement MyProfileText => _driver.FindElement(By.XPath("//h2[text()='My Profile']"));
        public IWebElement ContributionsTab => _driver.FindElement(By.XPath("//button[text()='My Contributions']"));
        public IWebElement ContributeNowButton => _driver.FindElement(By.CssSelector("div[class='mycontributions'] button[type = 'submit']"));
        public IWebElement ContributionDescription => _driver.FindElement(By.CssSelector("div[class='description'] span"));
        public IWebElement ContributionTitle => _driver.FindElement(By.CssSelector("div[class='section-title']"));
        public IWebElement ContributionDraftStatus => _driver.FindElement(By.CssSelector("div[class$='draft motif-dot-left ']"));
        public IWebElement ContributionInReviewStatus => _driver.FindElement(By.CssSelector("div[class$='error motif-dot-left ']"));
        public IWebElement ContributionApprovedStatus => _driver.FindElement(By.CssSelector("div[class$='success motif-dot-left ']"));
        public IWebElement ContributionSubmittedStatus => _driver.FindElement(By.CssSelector("div[class$='submitted motif-dot-left ']"));
        public IList<IWebElement> AboutMeTextBoxes => _driver.FindElements(By.CssSelector("input[type='text']"));
        public IWebElement FirstPageButton => _driver.FindElement(By.CssSelector("button[aria-label = 'Go to first page']"));
        public IWebElement PreviousPageButton => _driver.FindElement(By.CssSelector("button[aria-label = 'Go to previous page']"));
        public IWebElement NextPageButton => _driver.FindElement(By.CssSelector("button[aria-label = 'Go to next page']"));
        public IWebElement LastPageButton => _driver.FindElement(By.CssSelector("button[aria-label = 'Go to last page']"));
        public IWebElement PaginationSize => _driver.FindElement(By.CssSelector("span[id ^= 'pagination-max-pages']"));
        public IList<IWebElement> ContributionRows => _driver.FindElements(By.CssSelector("div[class='ag-center-cols-container'] div[role='row']"));
        public IWebElement CurrentPage => _driver.FindElement(By.CssSelector("input[class='motif-pagination-current-page']"));
        public IWebElement LastPage => _driver.FindElement(By.CssSelector("span[id ^= 'pagination-max-pages']"));
        public IWebElement ContributionNameHeader => _driver.FindElement(By.CssSelector("div[aria-label ='Contribution Name']"));
        public IWebElement ContributionDescriptionHeader => _driver.FindElement(By.CssSelector("div[aria-label ='Description']"));
        public IWebElement ContributionDateHeader => _driver.FindElement(By.CssSelector("div[aria-label ='Date of Submission']"));
        public IWebElement ContributionStateHeader => _driver.FindElement(By.CssSelector("div[aria-label ='Current State']"));
        public IWebElement MyAreasOfInterestButton => _driver.FindElement(By.XPath("//button[text()='My Areas of Interest']"));
        public IWebElement MyAreasOfInterestText => _driver.FindElement(By.CssSelector("p[class='section-title']"));
        public IWebElement EditAreasButton => _driver.FindElement(By.XPath("//button[text()='Edit Areas']"));
        public IList<IWebElement> CloseMark => _driver.FindElements(By.CssSelector("span[class='remove-all']"));
        public IWebElement MyAreasOfInterestSaveButton => _driver.FindElement(By.XPath("//button[text()='Save']"));
        public IWebElement AddAreasButton => _driver.FindElement(By.XPath("(//div[@class='add-areas'])[1]"));
        public IList<IWebElement> DropDownButtons => _driver.FindElements(By.CssSelector("svg[class='motif-select-input-arrow']"));
        public IList<IWebElement> AllAreas => _driver.FindElements(By.CssSelector("div[class='motif-option ']"));
        public IWebElement Area1 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[1]"));
        public IWebElement Area3 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[2]"));
        public IWebElement Area5 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[3]"));
        public IWebElement Area10 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[6]"));
        public IWebElement Area12 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[7]"));
        public IWebElement Area14 => _driver.FindElement(By.XPath("(//div[@class='motif-select-checkbox'])[10]"));
        public IWebElement AboutMeSuccessMessage => _driver.FindElement(By.CssSelector("div[class='motif-inline-message-content']"));
        public IWebElement MyAreasOfInterestSuccessMessage => _driver.FindElement(By.XPath("//div[text()='Profile updated successfully.']"));
        public IWebElement MyViewedItemsButton => _driver.FindElement(By.XPath("//button[text()='My Viewed Items']"));
        // the correct element for my viewd item menu
        public IWebElement MyViewedItem => _driver.FindElement(By.XPath("//button[contains(text(),'My Viewed Itemst')]"));
        public IWebElement CatalogItem => _driver.FindElement(By.XPath("//div[text()='Catalog Item']"));
        public IWebElement MyAreasOfInterest => _driver.FindElement(By.XPath("//div[text()='Areas of Interest']"));
        public IWebElement Description => _driver.FindElement(By.XPath("//div[text()='Description']"));
        public IList<IWebElement> TryNowLink => _driver.FindElements(By.XPath("//span[text()='Try Now']"));
        public IWebElement MyViewedItemsTitle => _driver.FindElement(By.XPath("//h4[text()='My Viewed Items']"));
        public IWebElement ComingSoon => _driver.FindElement(By.XPath("//h1[text()='Coming Soon...']"));
        public IWebElement MyFavoritesTab => _driver.FindElement(By.XPath("//button[text()='My Favorites']"));
        public IWebElement MyFavoritesTitle => _driver.FindElement(By.XPath("//p[text()='My Favorites']"));
        public IWebElement YellowBookmark => _driver.FindElement(By.CssSelector("div.favorites-grid > div:nth-child(1) > header > button > span > svg > path"));
        public IList<IWebElement> AllAreasOfExpertise => _driver.FindElements(By.CssSelector("button[title='Remove value']"));

        //pagination
        public IWebElement NumberOfPagination => _driver.FindElement(By.XPath("//span[@class='motif-number-of-pages']/span[2]"));
        public IWebElement NumberOfFirstPage => _driver.FindElement(By.XPath("//span[@class='ref-input-elem']"));
    }
}