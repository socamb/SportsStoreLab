using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.Tests
{
    [TestClass]
    public class MathTests
    {
        [TestMethod]
        [TestCategory("CI_Build")]
        public void AddTests()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
