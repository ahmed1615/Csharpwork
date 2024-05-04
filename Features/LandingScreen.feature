Feature: Platform Landing Screen

Background:
	Given the user accepts the cookies
	When the user login into the application
	Then the user is logged in

@LandingScreen_Header @4836152 
Scenario: Validate the Header of the Platform Landing Screen
	Given User hovers to EY Branding image and validate the link with homepage link
	When user clicks the user profile icon
	Then the user sees dropdown with full user profile name
	And the user clicks the logout button
	Then the user navigates to the signin page
	
@LandingScreen_Footer @4836157 
Scenario: Validate the footer
	When the user scroll down to the footer
	Then the footer should have the image in the right side
	Then the footer should have the link of Privacy Statement in the left side
	Then the footer should have copyright visible
	Then the footer should have the EYGM Limited
	Then the footer should have the EYClients only
	When the user click on Privacy Statement link
	Then privacy statement link should open a new tab

@LeftHandNavigation_GeneralDescription @4799240
Scenario: Validate the left hand navigation general description
	When the user scroll down to see view more
	When the user select"Left hand navigation"from catalog items
	Then the title of left hand navigation is visible
	Then the general description tab should be selected by default
	When the user click on try out tab
	Then try out page should be present and the highlight  should be under try out tab
	When the user click on browser back button
	Then the general description tab should be selected by default
	When the user scroll down to assets section
	Then request access should include four applications