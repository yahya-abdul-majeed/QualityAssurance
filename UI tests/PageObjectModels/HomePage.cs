using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_tests.PageObjectModels
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private const string URI = "https://localhost:7173/";

        By PageTitle = By.TagName("title");
        By Input = By.Id("inputField");
        By Button = By.Id("btnGenerate");
        By Prompt = By.Id("Prompt");
        By Constraints = By.Id("constraints");
        By Combination = By.ClassName("combination");

        public IWebElement InputElement => _driver.FindElement(Input);
        public IWebElement ButtonElement => _driver.FindElement(Button);
        public IWebElement PromptElement => _driver.FindElement(Prompt);
        public IWebElement ConstraintsElement => _driver.FindElement(Constraints);
        public ReadOnlyCollection<IWebElement> Combinations => _driver.FindElements(Combination);

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(URI);
        public void InputN(int n) => InputElement.SendKeys(n.ToString());
        public void ClickBtn()=> ButtonElement.Click();
    }
}
