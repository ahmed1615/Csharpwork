
Feature: DemoLogin
Background: 
	Given the user fill username
	When the user fill the password
	When the user click on login button
	Then homepage should appear

@Demo
Scenario: Login to demo
	When the user get all the product from A to Z and Z to A
	When the user select product "Sauce Labs Backpack"
	When the user check the card
	When the user wants to back to home
	Then homepage should appear

@burger
Scenario: go to burger menu
	When the user open the hamburger menu
	When the user over all list



	
