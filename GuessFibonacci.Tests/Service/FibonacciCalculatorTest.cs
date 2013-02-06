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
    public class FibonacciCalculatorTest
    {
        private FibonacciCalculator calculator;

        [TestInitialize]
        public void SetUp()
        {
            calculator = new FibonacciCalculator(100);
        }

        [TestMethod]
        public void TestFirst()
        {
            //when
            var result = calculator.Calculate(1);

            //then
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestSecond()
        {
            //when
            var result = calculator.Calculate(2);

            //then
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestOther()
        {
            //when
            var result = calculator.Calculate(7);

            //then
            Assert.AreEqual(13, result);
        }

        [TestMethod]
        public void TestAboveMax()
        {
            //when
            try
            {
                calculator.Calculate(101);
            }
            catch (Exception e)
            {
                //then
                Assert.AreEqual("Term must be between 1 and 100.", e.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void TestLessThanOne()
        {
            //when
            try
            {
                calculator.Calculate(0);
            }
            catch (Exception e)
            {
                //then
                Assert.AreEqual("Term must be between 1 and 100.", e.Message);
                return;
            }
            Assert.Fail();
        }
    }
}
