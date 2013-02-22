using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessFibonacci.Models;

namespace GuessFibonacci.Tests.Models
{
    [TestClass]
    public class GuessResultTest
    {
        [TestMethod]
        public void PositiveDifference()
        {
            // given
            var result = new GuessResult { correctResult = 5, guess = 4 };

            //when
            var difference = result.CalculateDifference();

            //then
            Assert.AreEqual(1, difference);
        }

        [TestMethod]
        public void NegativeDifference()
        {
            // given
            var result = new GuessResult { correctResult = 5, guess = 6 };

            //when
            var difference = result.CalculateDifference();

            //then
            Assert.AreEqual(1, difference);
        }
    }
}
