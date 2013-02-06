using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuessFibonacci.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GuessFibonacci.Tests.Service
{
    [TestClass]
    public class RandomNumberGeneratorTest
    {
        private RandomNumberGenerator generator;
        private Moq.Mock<Random> mockRandom;

        [TestInitialize]
        public void SetUp()
        {
            mockRandom = new Moq.Mock<Random>();
            generator = new RandomNumberGenerator(mockRandom.Object, 1, 2);
        }

        [TestMethod]
        public void GenerateWithMaxAndMin()
        {
            // given
            mockRandom.Setup(framework => framework.Next(1, 2)).Returns(5);

            // when
            var result = generator.Generate();

            // then
            mockRandom.Verify(foo => foo.Next(1, 2));
            Assert.AreEqual(5, result);
        }
    }
}
