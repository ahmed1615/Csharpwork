Feature: Search

Background: 
	Given the user accepts the cookies
	When the user login into the application
    Then the user is logged in

@Search_ValidKeyword @4733598
Scenario: Verify the quick search results display on entering search text
	When the user enters text in the search bar
	Then the user should see five relavant suggestions in dropdown
	And the user should see the view all link

@Search_InvalidKeyword @4733615
Scenario: Verify the no results notification when there are no results matching the search term
	When the user enters invalid text in the search bar
	Then the user should see no results notification