Feature: Internal user profile page

Background: 
	Given the user accepts the cookies
	When the user login into the application
    Then the user is logged in

@ProfilePage_AboutMe @4796729
Scenario: Verify the user able to edit the records and save them in the About Me section
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on Profile page and validating About Me section
	When the user clicks on Edit profile button
	Then the user sees the editable fields of organization,Team name,role,logged in date
	Then the user clicks on the save button
	And the user could able to see the saved record upon profile updated successfully message with close mark

@ProfilePage_MyAreasOfInterest_Edit @4796748
Scenario: Verify whether the user is able to delete and add the areas of expertise in My Areas of Interest section
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on Profile page and validating Areas of Interest section
	When the user clicks on Edit Areas button
	Then the user sees the ask me about interest area team role dropdowns and click close mark to delete all areas of expertise
	Then the user clicks the save button
	And the user click the add Areas button to areas of expertise
	Then by clicking the dropdown the user adds the areas of expertise and clicks save button
	And  the user could see the saved record

@ProfilePage_MyAreasOfInterest_Delete @4796748
Scenario: Verify whether the user is able to delete each areas of expertise seperately and save the records
    Given User clicks on the user profile icon
    Then the user sees the username from the dropdown and click on it
    And the user lands on Profile page and validating Areas of Interest section
    When the user clicks on Edit Areas button
    Then the user sees the exisiting areas of expertise in ask me about interest areas and team dropdown and delete each records and save them

@ProfilePage_MyContributionStatus @4836161 
Scenario: Verify the color and state of contribution items
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on profile page
	And the user validates contribution tab
	And the user verifies the color and state of contribution items

@ProfilePage_MyContributionPagination @4836165 
Scenario: Verify the pagination of contribution items
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on profile page
	And the user validates contribution tab
	And the user verifies the single page result of contribution items 
	And the user verifies the pagination of contribution items
	And the user verifies the first and last page button

@ProfilePage_MyViewedItems @4950646
Scenario: Verify the My Viewed Items title and click try now link
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on profile page
	And the user validates the My Viewed item tab
	And the user validates the title of each containers in My Viewed Items
	And the user verifies the presence of try now link

@ProfilePage_MyFavorites @4836087
Scenario: Verify the My Favorites tab
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	And the user lands on profile page
	And the user clicks the My Favorites tab
	And the user validates the title of My Favorites
	And  the user verifies the catalog items which are marked as fovorites is tagged with yellow label mark
	

	