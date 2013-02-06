using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using GuessFibonacci.Service;

namespace GuessFibonacci.Tests.Service
{
    [TestClass]
    public class GuessServiceTest
    {
        private GuessService guessService;
        private Moq.Mock<FibonacciCalculator> mockFibonacciCalculator;

        [TestInitialize]
        public void SetUp()
        {
            mockFibonacciCalculator = new Moq.Mock<FibonacciCalculator>(null);
            guessService = new GuessService(mockFibonacciCalculator.Object);
        }

        [TestMethod]
        public void TestGuess()
        {
            //given
            mockFibonacciCalculator.Setup(framework => framework.Calculate(1)).Returns(3);

            //when
            var result = guessService.SubmitGuess(1, 2);

            //then
            Assert.AreEqual(1, result.term);
            Assert.AreEqual(2, result.guess);
            Assert.AreEqual(3, result.correctResult);
        }
    }
}
