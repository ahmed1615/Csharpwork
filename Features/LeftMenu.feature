Feature: Left menu

Background: 
	Given the user accepts the cookies
	When the user login into the application
    Then the user is logged in

@HamburgerMenu_Extends&Collapses @4679024
Scenario: Check if hamburger menu is extendig left menu
	Given User clicks on hamburger menu
	Then the user sees the extended menu of left navigation bar
	And the user clicks back hamburger menu it collapses

@VerticalMenu_InitialLoad @4950847
Scenario: Check if relevant text is displayed on hovering left navigation bar icons
	Then the user sees collapsed left navigation bar on initial load
	And the user sees hamburger menu at the top of left navigation bar
	And the user sees the relevant text on hovering icons

@VerticalMenu_Arrows_Absence @4672774 
Scenario: Check if arrows are not displayed in vertical menu
	Then the user sees collapsed left navigation bar on initial load
	And the arrows are not displayed in vertical menu

@ExtendedMenu_ScrollBar @4672825
Scenario: Check if extended menu is scrollable if the number of items exceeds the height of the screen
	Given User clicks on hamburger menu
	Then the extended menu is displayed
	And the scrollbar is displayed on clicking arrow in the extended menu

@VerticalMenu_Hover @4672806
Scenario: Check if user is able to see submenu items on hovering menu items
	Then the user sees collapsed left navigation bar on initial load
	And user verifies the list of submenu items on hovering menu items

@ExtendedMenu_Expands @4672763
Scenario: Check if the menu items in the extended menu can be expanded on clicking the arrow
    Given User clicks on hamburger menu
    Then the extended menu is displayed
    And  the submenu items are displayed on clicking arrow in the extended menu

  
@ExtendedMenu_Responsiveness @4679052
Scenario Outline: Check the responsiveness of the application in different screen sizes
	Given user clicks hamburger menu and see the behaviour of application in different screen sizes
	Then user set the screen size to different resolution "<Tab>" in both landscape and potrait mode
Examples:
	| Tab    |
	| IPad   |
	| Galaxy |

@ExtendedMenu_Scroll @4679038s
Scenario: Check if extended menu is scrollable when the number of items exceeds
	Given user clicks hamburger menu
	When change the application window to smaller resolution
	Then the user sees the extended menu with scroll option
	And the user scroll till last element

@ExtendedMenu_Collapse @4672816
Scenario: Check if the menu items in the extended menu can be collapsed on clicking the arrow again
    Given User clicks on hamburger menu
    Then the extended menu is displayed
    And  the submenu items are displayed on clicking arrow in the extended menu
    And  the submenu items are collapsed on clicking arrow again in the extended menu

	@E2Eflow
Scenario: Check if left hand navigation bar is working properly
#Collapsed Navigation Bar displays sub menu items on hovering menu icons
	Then the user sees collapsed left navigation bar on initial load
	And the user sees hamburger menu at the top of left navigation bar
	And the user sees the relevant text on hovering icons
	And submenu items are displayed on hovering menu items
#Extended Navigation Bar displays sub menu items on clicking arrows in menu icons
	Given User clicks on hamburger menu
    Then the extended menu is displayed
    And  the submenu items are displayed on clicking arrow in the extended menu
	#And  the submenu items are collapsed on clicking arrow again in the extended menu
	And the user clicks back hamburger menu it collapses
#About Me section Verification
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on Profile page and validating About Me section
	When the user clicks on Edit profile button
	Then the user sees the editable fields of organization,Team name,role,logged in date
	Then the user clicks on the save button
	And the user could able to see the saved record upon profile updated successfully message with close mark
#My Viewed Items Verification
	And the user validates the My Viewed item tab
	And the user validates the title of each containers in My Viewed Items
	And the user verifies the presence of try now link
#Area Of Interest Verification
	And the user lands on Profile page and validating Areas of Interest section
	When the user clicks on Edit Areas button
	Then the user sees the ask me about interest area team role dropdowns and click close mark to delete all areas of expertise
	Then the user clicks the save button
	And the user click the add Areas button to areas of expertise
	Then by clicking the dropdown the user adds the areas of expertise and clicks save button
	And  the user could see the saved record
#My Favorites Verification
	And the user clicks the My Favorites tab
	And the user validates the title of My Favorites
	And  the user verifies the catalog items which are marked as fovorites is tagged with yellow label mark
#Contribution color verification
	And the user validates contribution tab
	And the user verifies the color and state of contribution items
#Contribution Pagination verification
	And the user verifies the single page result of contribution items 
	And the user verifies the pagination of contribution items
	And the user verifies the first and last page button
#Verify search with valid keyword
	When the user enters text in the search bar
	Then the user should see five relavant suggestions in dropdown
	And the user should see the view all link
#Verify search with invalid keyword
	When the user enters invalid text in the search bar
	Then the user should see no results notification


    