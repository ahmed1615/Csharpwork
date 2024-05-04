Feature: NegativeScenarios

Background: 
	Given the user accepts the cookies

@LoginWithoutEmail
Scenario: verify the login without any value 
	When the user wants to login without email
	Then the focus class element should be appear and user can not see the main page

@LoginwithOnlyName
	Scenario: Verify the login using invalidname
	When the user wants to login with invalid name
	Then the focus class element should be appear and user can not see the main page

@LoginwithOnlyValidNameWithout@symbol
	Scenario: Verify the login using validname without usin @ symbol
	When the user wants to login with valid name with out using @ symbol
	Then the focus class element should be appear and user can not see the main page

	@LoginwithInvalidEmail
	Scenario: Verify the login using invalid email
	When the user wants to login with invalid email
	Then Login error message should be appear

	@LoginwithInvalidEmailThenwithvalidemail
	Scenario: Verify the login using invalid email then login with valid email
	When the user wants to login with invalid email
	Then Login error message should be appear
	When the user login into the application
	Then the user is logged in

	@EYiconInHomePageBackToWelcomePage
	Scenario: Verify clicking on EY icon in the home page back to the Welcome page
	When the user login into the application
	Then the user is logged in
	When the user in the home page and click on EY icon
	Then the user back to welcome page again

	@EYiconInLHNPageeBackToWelcomePage
	Scenario: Verify clicking on EY icon in the LHN page back to the Welcome page
	When the user login into the application
	Then the user is logged in
	When the user select"Left hand navigation"from catalog items
	Then the title of left hand navigation is visible
	When the user in the home page and click on EY icon
	Then the user back to welcome page again

	@EYiconInProfilePageeBackToWelcomePage
	Scenario: Verify clicking on EY icon in the profile page back to the Welcome page
	When the user login into the application
	Then the user is logged in
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	When the user in the home page and click on EY icon
	Then the user back to welcome page again
	When the user click on browser back button
	When the user wants to go to my view items
	When the user in the home page and click on EY icon
	Then the user back to welcome page again

	@PaginationInInternalProfile 
	Scenario: Verify the pagination in Internal Profile 
	When the user login into the application
	Then the user is logged in
	Given User clicks on the user profile icon
	Then the user sees the username from the dropdown and click on it
	When the user wants to go to my view items
	When the user scroll down to the footer
	When the user wants to go to click on "next" button
	When the user wants to go to click on "back" button
	When the user wants to go to click on "next to the last page" button
	When the user wants to go to click on "back to the first page" button