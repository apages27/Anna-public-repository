using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TipCalculator.Models;

namespace CalculatorAcceptanceTests
{
    [Binding]
    public class TipCalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        public CalculatorInput TestInput { get; set; }
        public CalculatorOutput TestOutput { get; set; }
        
        [Given(@"the cost of my meal is (.*)")]
        public void GivenTheCostOfMyMealIs(decimal mealCost)
        {
            TestInput = new CalculatorInput();
            TestInput.Amount = mealCost;
        }

        [Given(@"the tip percent I enter is (.*)")]
        public void GivenTheTipPercentIEnterIsM(decimal tipPercent)
        {
            TestInput.TipPercent = tipPercent;
        }

        [When(@"I call the calculate function")]
        public void WhenICallTheCalculateFunction()
        {
            TestOutput = new CalculatorOutput(TestInput);
        }

        [Then(@"the tip amount should be (.*)")]
        public void ThenTheTipAmountShouldBe(Decimal tipAmount)
        {
            Assert.AreEqual(tipAmount, TestOutput.TipAmount);
        }

        [Then(@"the total cost should be (.*)")]
        public void ThenTheTotalCostShouldBe(Decimal totalCost)
        {
            Assert.AreEqual(totalCost, TestOutput.TotalAmount);
        }
    }
}
