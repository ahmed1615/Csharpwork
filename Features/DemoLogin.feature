Feature: DemoLogin

@Demo
Scenario: Login to demo
	Given the user fill username
	When the user fill the password
	When the user click on login button
	Then homepage should appear
