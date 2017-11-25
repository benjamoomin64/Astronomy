using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Astronomy;
using System.Diagnostics;

namespace MathChecking
{
    [TestClass]
    public class UnitTest1
    {
        private bool MarginOfError(double result, double expected)
        {
            Debug.WriteLine("Expected: " + expected + ", Result: " + result);
            return ((Math.Abs(result / expected) - 1) < 0.001);
        }

        [TestMethod]
        public void TestMethod1()
        {
            BetterStar star = new BetterStar();
            star.Spatial.ArcSeconds = 0.04439;
            star.AbsoluteMagnitude = 1.58;
            star.FindOtherValues();

            Assert.IsTrue(MarginOfError(star.Spatial.Parsecs, 22.53));
            Assert.IsTrue(MarginOfError(star.Spatial.Lightyears, 73.5));
            Assert.IsTrue(MarginOfError(star.ApparentMagnitude, 3.33));
            Assert.IsTrue(MarginOfError(star.Luminosity, (17.7 * Math.Pow(10, 26) * 3.828)));
        }

        [TestMethod]
        public void TestMethod2()
        {
            BetterStar star = new BetterStar();
            star.AbsoluteMagnitude = -5.9;
            star.ApparentMagnitude = 0.53;
            star.FindOtherValues();
            Assert.IsTrue(MarginOfError(star.Spatial.Parsecs, 193.20));
        }

        [TestMethod]
        public void TestMethod3()
        {
            BetterStar star = new BetterStar();
            star.ApparentMagnitude = -22.2;
            star.Spatial.Parsecs = 178.5;
            star.FindOtherValues();
            Assert.IsTrue(MarginOfError(star.AbsoluteMagnitude, -28.46));
        }
    }
}
