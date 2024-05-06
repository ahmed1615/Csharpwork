Feature: DemoLogin

@Demo
Scenario: Login to demo
	Given the user fill username
	When the user fill the password
	When the user click on login button
	Then homepage should appear
	When the user get all the product from A to Z and Z to A
	When the user select product "Sauce Labs Backpack"
	When the user check the card
