Feature: test why not

@notfet
Scenario: Retrieve the results - sample request
	When the user sends GET request to "/{id}"
	Then the response status code should be 200
	Then capture the error if it fails