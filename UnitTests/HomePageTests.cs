using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App.Controllers;
using Web_App.Models;

namespace UnitTests
{
    public class HomePageTests
    {
        private readonly HomeController _controller;
        public HomePageTests()
        {
            _controller = new HomeController();
        }
        /// <summary>
        /// Giving nominal values, and inclusive boundary values,
        /// should return correct result
        /// </summary>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(8)]
        public void GenerateParenthesis_GivenValidInput_ReturnsList(int n)
        {
            //Arrange
            var combinations = Combinatorics.NumberOfParenthesesCombinations(n); 
            //Act
            var result = _controller.GenerateParenthesis(n);
            //Assert
            Assert.Equal(combinations, result.Count);
        }
        /// <summary>
        /// Given Exclusive Boundary values,
        /// should throw an ArgumentOutOfRangeException
        /// </summary>
        /// <param name="n"></param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(9)]
        public void GenerateParenthesis_GivenInvalidInput_ThrowsArgumentOutOfRangeException(int n)
        {
            var Ex = Assert.Throws<ArgumentOutOfRangeException>(()=>_controller.GenerateParenthesis(n));
            Assert.Equal("Valid values of n are 1<=n<=8 (Parameter 'n')", Ex.Message);
        }
        /// <summary>
        /// Given a valid input to the Post method of the HomeController,
        /// It should return a ViewResult with a populated list in the model
        /// </summary>
        /// <param name="n"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(8)]
        public void Index_GivenValidInput_ReturnViewResultWithIndexVM_IsInRangeTrue(int n)
        {
            var combinations = Combinatorics.NumberOfParenthesesCombinations(n);
            var _IActionResult = _controller.Index(n) ;
            var _ViewResult = Assert.IsAssignableFrom<ViewResult>(_IActionResult );
            var _model = Assert.IsAssignableFrom<IndexVM>(_ViewResult?.Model);
            Assert.Equal(combinations, _model.Combinations.Count);
            Assert.True(_model.IsInRange);
            Assert.Equal(_model.ErrorMessage, string.Empty);
        }
        /// <summary>
        /// Given Invalid input to the Post method of the HomeController,
        /// An Argument out of range Exception must be thrown
        /// </summary>
        /// <param name="n"></param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(9)]
        public void Index_GivenInvalidInput_ReturnViewResultWithIndexVM_IsInRangeFalse(int n)
        {
            var _IActionResult = _controller.Index(n) ;
            var _ViewResult = Assert.IsAssignableFrom<ViewResult>(_IActionResult );
            var _model = Assert.IsAssignableFrom<IndexVM>(_ViewResult?.Model);
            Assert.Empty(_model.Combinations);
            Assert.False(_model.IsInRange);
            Assert.NotEqual(_model.ErrorMessage, string.Empty);
        }
    }

    public class Combinatorics
    {

        public static int NumberOfParenthesesCombinations(int n)
        {
            double numerator = Factorial(2 * n);
            double denominator = Factorial(n + 1) * Factorial(n);
            double result = numerator / denominator;
            return (int)result;
        }

        private static double Factorial(int n)
        {
            double factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;
        }
    }
}
