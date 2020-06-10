Feature: SearchAimsTests

@smoke
Scenario: Search all aims
	Given I have navigated to LRS webui
	When I click on Search button
	Then I should see the search results page
