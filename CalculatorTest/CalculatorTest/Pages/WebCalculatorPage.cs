using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace CalculatorTest.Pages
{
    public class WebCalculatorPage
    {
        public IWebDriver _driver;
        private readonly By _leftNumberTexBox = By.Id("leftNumber");
        private readonly By _rightNumberTextBox = By.Id("rightNumber");
        private readonly By _resultTextBox = By.ClassName("result");
        private readonly By _operatorList = By.Id("operator");
        private readonly By _calculateButton = By.Id("calculate");
        private readonly By _iFrame = By.TagName("iframe");
        private string _result;
        private WebDriverWait _wait;

        public WebCalculatorPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        public void SetLeftNumber(int leftNumber)
        {
            _driver.SwitchTo().DefaultContent();
            _driver.FindElement(_leftNumberTexBox).Clear();
            _driver.FindElement(_leftNumberTexBox).SendKeys(leftNumber.ToString());
        }

        public void SetRightNumber(int rightNumber)
        {
            _driver.SwitchTo().DefaultContent();
            _driver.FindElement(_rightNumberTextBox).Clear();
            _driver.FindElement(_rightNumberTextBox).SendKeys(rightNumber.ToString());
        }

        public void SetOperator(string selectedOperator)
        {
            _driver.SwitchTo().DefaultContent();
            var operatorList = _driver.FindElement(_operatorList);
            operatorList.Click();
            var select = new SelectElement(operatorList);
            select.SelectByValue(selectedOperator);
        }

        public void ClickCalculateButton()
        {
            _driver.SwitchTo().Frame(_driver.FindElement(_iFrame));
            _driver.FindElement(_calculateButton).Click();
        }

        public int ReturnCalculationResult()
        {
            _driver.SwitchTo().DefaultContent();
            _wait.Until(d => _driver.FindElement(_resultTextBox).GetAttribute("value") != "");
            return Convert.ToInt32(_driver.FindElement(_resultTextBox).GetAttribute("value"));
        }
    }
}
