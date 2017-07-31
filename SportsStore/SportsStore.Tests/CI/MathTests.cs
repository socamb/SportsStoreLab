using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.Tests.CI
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        [TestCategory("CI_Build")]
        public void AddTests()
        {
            Assert.AreEqual(1, 2);
        }
    }
}
