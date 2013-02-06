using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessFibonacci;
using GuessFibonacci.Controllers;
using GuessFibonacci.Service;

namespace GuessFibonacci.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Moq.Mock<OrdinalSuffixProvider> mockOrdinalSuffixProvider;
        private Moq.Mock<RandomNumberGenerator> mockGenerator;
        private Moq.Mock<FibonacciCalculator> mockFibonacciCalculator;

        [TestInitialize]
        public void SetUp()
        {
            mockOrdinalSuffixProvider = new Moq.Mock<OrdinalSuffixProvider>();
            mockGenerator = new Moq.Mock<RandomNumberGenerator>(null, null, null);
            mockFibonacciCalculator = new Moq.Mock<FibonacciCalculator>(null);
            controller = new HomeController(mockGenerator.Object, mockOrdinalSuffixProvider.Object, mockFibonacciCalculator.Object);
        }


        [TestMethod]
        public void IndexProvidesTermWithOrdinalSuffix()
        {
            // given
            mockGenerator.Setup(framework => framework.Generate()).Returns(5);
            mockOrdinalSuffixProvider.Setup(framework => framework.GetSuffix(5)).Returns("th");

            // when
            ViewResult result = controller.Index() as ViewResult;

            // then
            Assert.AreEqual(5, result.ViewBag.Term);
            Assert.AreEqual("th", result.ViewBag.Suffix);
        }

        [TestMethod]
        public void SubmitCorrect()
        {
            // given
            mockFibonacciCalculator.Setup(framework => framework.Calculate(2)).Returns(1);
            mockOrdinalSuffixProvider.Setup(framework => framework.GetSuffix(2)).Returns("nd");

            // when
            ViewResult result = controller.Submit(2, "1") as ViewResult;

            // then
            Assert.AreEqual("Correct", result.ViewName);
            Assert.AreEqual(1, result.ViewBag.Value);
            Assert.AreEqual(2, result.ViewBag.Term);
            Assert.AreEqual("nd", result.ViewBag.Suffix);
        }

        [TestMethod]
        public void SubmitIncorrectGuessTooLow()
        {
            // given
            mockFibonacciCalculator.Setup(framework => framework.Calculate(5)).Returns(6);
            mockOrdinalSuffixProvider.Setup(framework => framework.GetSuffix(5)).Returns("th");

            // when
            ViewResult result = controller.Submit(5, "4") as ViewResult;

            // then
            Assert.AreEqual("Incorrect", result.ViewName);
            Assert.AreEqual(2, result.ViewBag.Difference);
        }

        [TestMethod]
        public void SubmitIncorrectGuessTooHigh()
        {
            // given
            mockFibonacciCalculator.Setup(framework => framework.Calculate(5)).Returns(2);
            
            // when
            ViewResult result = controller.Submit(5, "4") as ViewResult;

            // then
            Assert.AreEqual(2, result.ViewBag.Difference);
        }

        [TestMethod]
        public void SubmitNotNumeric()
        {
            // given
            mockOrdinalSuffixProvider.Setup(framework => framework.GetSuffix(2)).Returns("nd");

            // when
            ViewResult result = controller.Submit(2, "abc") as ViewResult;

            // then
            Assert.AreEqual("Index", result.ViewName);
            Assert.AreEqual("Your guess must be an integer", result.ViewBag.ValidationError);
            Assert.AreEqual(2, result.ViewBag.Term);
            Assert.AreEqual("nd", result.ViewBag.Suffix);
        }
    }
}
