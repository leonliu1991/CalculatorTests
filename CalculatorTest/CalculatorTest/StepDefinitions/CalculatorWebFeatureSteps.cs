using TechTalk.SpecFlow;
using CalculatorTest.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTest.StepDefinitions
{
    [Binding]
    public sealed class CalculatorWebFeatureSteps : BaseStepDefinition
    {
        private WebCalculatorPage webCalculator;
        public CalculatorWebFeatureSteps()
        {
            webCalculator = new WebCalculatorPage(driver);
        }

        [Given(@"the left number is (.*)")]
        public void GivenTheLeftNumberIs(int leftNumber)
        {
            webCalculator.SetLeftNumber(leftNumber);
        }

        [Given(@"the right number is (.*)")]
        public void GivenTheRightNumberIs(int rightNumber)
        {
            webCalculator.SetRightNumber(rightNumber);
        }

        [Given(@"the operator is (.*)")]
        public void WhenTheOperatorIs(string selectedOperator)
        {
            webCalculator.SetOperator(selectedOperator);
        }

        [When(@"Calculate button is clicked")]
        public void WhenCalculateButtonIsClicked()
        {
            webCalculator.ClickCalculateButton();
        }

        [Then(@"the calculation result should be (.*)")]
        public void ThenTheCalculationResultShouldBe(int expectedResult)
        {
            int actualResult = webCalculator.ReturnCalculationResult();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Given(@"the test scenario is to '(.*)'")]
        public void GivenTheTestScenarioIsTo(string p0)
        {
            // This is merely to display in the test report
        }
    }
}
