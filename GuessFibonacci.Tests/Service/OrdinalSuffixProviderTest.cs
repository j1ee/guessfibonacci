using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuessFibonacci.Service;

namespace GuessFibonacci.Tests.Service
{
    [TestClass]
    public class OrdinalSuffixProviderTest
    {
        private OrdinalSuffixProvider provider;

        [TestInitialize]
        public void SetUp()
        {
            provider = new OrdinalSuffixProvider();
        }

        [TestMethod]
        public void ProvideStIfEndsIn1AndIsNot11()
        {
            // when
            var result = provider.GetSuffix(41);

            // then
            Assert.AreEqual("st", result);
        }

        [TestMethod]
        public void ProvideThIfIs11()
        {
            // when
            var result = provider.GetSuffix(11);

            // then
            Assert.AreEqual("th", result);
        }

        [TestMethod]
        public void ProvideNdIfEndsIn2AndIsNot12()
        {
            // when
            var result = provider.GetSuffix(42);

            // then
            Assert.AreEqual("nd", result);
        }

        [TestMethod]
        public void ProvideThIfIs12()
        {
            // when
            var result = provider.GetSuffix(12);

            // then
            Assert.AreEqual("th", result);
        }

        [TestMethod]
        public void ProvideRdIfEndsIn3AndIsNot13()
        {
            // when
            var result = provider.GetSuffix(43);

            // then
            Assert.AreEqual("rd", result);
        }

        [TestMethod]
        public void ProvideThIfIs13()
        {
            // when
            var result = provider.GetSuffix(13);

            // then
            Assert.AreEqual("th", result);
        }

        [TestMethod]
        public void ProvideTh()
        {
            // when
            var result = provider.GetSuffix(44);

            // then
            Assert.AreEqual("th", result);
        }
    }
}
