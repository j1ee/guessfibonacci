using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessFibonacci;
using GuessFibonacci.Controllers;
using GuessFibonacci.Service;
using GuessFibonacci.Models;

namespace GuessFibonacci.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private Moq.Mock<OrdinalSuffixProvider> mockOrdinalSuffixProvider;
        private Moq.Mock<RandomNumberGenerator> mockGenerator;
        private Moq.Mock<GuessService> mockGuessService;

        [TestInitialize]
        public void SetUp()
        {
            mockOrdinalSuffixProvider = new Moq.Mock<OrdinalSuffixProvider>();
            mockGenerator = new Moq.Mock<RandomNumberGenerator>(null, null, null);
            mockGuessService = new Moq.Mock<GuessService>(null);
            controller = new HomeController(mockGenerator.Object, mockOrdinalSuffixProvider.Object, mockGuessService.Object);
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
            var mockResult = new Moq.Mock<GuessResult>();
            mockResult.Setup(framework => framework.isCorrect()).Returns(true);
            mockResult.Object.correctResult = 1;
            mockResult.Object.term = 2;

            mockGuessService.Setup(framework => framework.SubmitGuess(2, 1)).Returns(mockResult.Object);
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
        public void SubmitIncorrect()
        {
            // given
            var mockResult = new Moq.Mock<GuessResult>();
            mockResult.Setup(framework => framework.isCorrect()).Returns(false);
            mockResult.Setup(framework => framework.CalculateDifference()).Returns(9);
            mockGuessService.Setup(framework => framework.SubmitGuess(2, 1)).Returns(mockResult.Object);

            // when
            ViewResult result = controller.Submit(2, "1") as ViewResult;

            // then
            Assert.AreEqual("Incorrect", result.ViewName);
            Assert.AreEqual(9, result.ViewBag.Difference);
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
