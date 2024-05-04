Feature: API Testing

@GET
Scenario: Retrieve the results - sample request
	When the user sends GET request to "/{id}"
	Then the response status code should be 200
	Then capture the error if it fails

@POST
Scenario: Post the request - sample request
	When the user post request "/{id}"
	Then the response status code should be 200
	Then verify whether response contains expected text

@CREATE
Scenario: Creation - sample request
	When the user post request for creation "/{id}"
	Then the response status code should be 201
	Then verify whether response contains expected text for user creation

@UPDATE
Scenario: Update - sample request
	When the user send request for update "/{id}"
	Then the response status code should be 200
	Then verify whether response contains expected text for user update

@DELETE
Scenario: Delete - sample request
	When the user send request for delete "/{id}"
	Then the response status code should be 204
	Then verify the response for user delete

@SERIALIZATION
Scenario: Serialization and Deserialization
	Given I have the API endpoint
	Then serilaizing the JSON response
	And Deserializing the JSON response and asserting it

@AUTHORIZE
Scenario: Authorize - sample request
	When the user send request for authorize "/{id}"
	Then the response status code should be 200
	Then verify the response for user authorization
	

