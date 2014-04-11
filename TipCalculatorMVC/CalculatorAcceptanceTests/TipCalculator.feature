Feature: TipCalculator
	In order not to be a jerk
	As a restaurant patron with bad math skills
	I want to calculate an appropriate tip

Scenario: To calculate the amount of a 20% tip
	Given the cost of my meal is 10.00
	And the tip percent I enter is 20
	When I call the calculate function
	Then the tip amount should be 2.00

Scenario: To calculate the total cost including a 20% tip
	Given the cost of my meal is 10.00
	And the tip percent I enter is 20
	When I call the calculate function
	Then the total cost should be 12.00