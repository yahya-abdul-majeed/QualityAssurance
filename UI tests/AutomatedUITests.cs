using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UI_tests.PageObjectModels;
using Xunit.Sdk;

namespace UI_tests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly HomePage _homePage;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver();
            _homePage = new HomePage(_driver);
            _homePage.Navigate();
        }

        [Fact]
        public void Index_WhenExecuted_ReturnsIndexViewWithCorrectTitle()
        {
           Assert.Equal("Home Page - Web_App", _driver.Title);
        }
        [Fact]
        public void Index_WhenExecuted_ReturnsIndexViewWithCorrectPrompt()
        {
            var prompt = "Given n pairs of parentheses, generate" +
                " all combinations of well-formed parentheses.";
            Assert.Equal(prompt, _homePage.PromptElement.Text);
        }
        [Fact]
        public void Index_WhenExecuted_ReturnsIndexViewWithCorrectConstraints()
        {
            var constraints = "Constraints: 1 <= n <= 8";
            Assert.Equal(constraints, _homePage.ConstraintsElement.Text);
        }
        [Fact]
        public void Index_WhenExecuted_ReturnsIndexViewWithEmptyListOfCombinations()
        {
            var listOfCombinations = _homePage.Combinations.Select(e => e.Text).ToList();
            Assert.Empty(listOfCombinations);
        }

        [Theory]
        [InlineData(3)]
        public void Index_WhenButtonClicked_ReturnsListOfCombinations_ForValidInput(int n)
        {
            _homePage.InputN(n);
            _homePage.ClickBtn();
            var listOfCombinations = _homePage.Combinations.Select(e => e.Text).ToList();
            Assert.Equal(5, listOfCombinations.Count);
        }
        [Theory]
        [InlineData(9)]
        public void Index_InputBackgroundChangesRed_ForInvalidInput(int n)
        {
            _homePage.InputN(n);
            var bgColor = _homePage.InputElement.GetCssValue("backgroundColor");
            string[] rgba = bgColor.Replace("rgba(", "").Replace(")", "").Split(',');
            int r = int.Parse(rgba[0].Trim());
            int g = int.Parse(rgba[1].Trim());
            int b = int.Parse(rgba[2].Trim());
            string hexColor = $"#{r:X2}{g:X2}{b:X2}";
            Assert.Equal("#FA5E71",hexColor);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
