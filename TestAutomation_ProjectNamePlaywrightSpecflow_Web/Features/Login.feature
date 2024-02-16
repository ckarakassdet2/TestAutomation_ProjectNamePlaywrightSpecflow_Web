Feature: Login

Verifying that I can login to QSight application using valid credentials and logout safely. 


@smoke @login
Scenario: user is able to login QSight using valid credentials
	Given I navigate to QSight application
	And I enter valid username password  and click on login
	| username | password |
	| username | password |
	Then I select department 
	And I click on Continue button
	And I close training video
	And I verify landing page Add Product to Hospital Inventory
	Then I click on logout link 







