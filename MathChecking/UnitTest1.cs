using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Astronomy;
using System.Diagnostics;

namespace MathChecking
{
    [TestClass]
    public class UnitTest1
    {
        #region Units
        private static UnitConversion[] masses = new UnitConversion[] { new UnitConversion("kg", 1), new UnitConversion("solar mass", (1.98855 * Math.Pow(10, 30))), new UnitConversion("earth mass", (5.97237 * Math.Pow(10, 24))) };
        private static UnitConversion[] distances = { new UnitConversion("meter", 1), new UnitConversion("kilometers", 1000), new UnitConversion("Parsecs", (3.08567782 * Math.Pow(10, 16))),
            new UnitConversion("lightyear", (9.46073047258 * Math.Pow(10, 15))), new UnitConversion("AU", (1.495978707 * Math.Pow(10, 11))), new UnitConversion("miles", 1.60938),
            new UnitConversion("lightsecond", 299792458)};
        private static UnitConversion[] times = { new UnitConversion("seconds", 1), new UnitConversion("minutes", 60), new UnitConversion("hours", 3600), new UnitConversion("days", 86400),
            new UnitConversion("week", 604800), new UnitConversion("year", 31556952)};
        #endregion

        private Satellite jupiter = new Satellite(masses[2].ToStandard(317.8), masses[1].ToStandard(1), 778570000, 0.04839266, times[3].ToStandard(365.249), 816620000, 740520000);
        private Satellite mars = new Satellite(masses[2].ToStandard(0.107), masses[1].ToStandard(1), distances[4].ToStandard(1.523679), 0.0934, times[3].ToStandard(686.97), distances[4].ToStandard(1.666), distances[4].ToStandard(1.3814));
        private Satellite saturn = new Satellite(masses[2].ToStandard(95.16), masses[1].ToStandard(1), 1433530000, 0.0565, times[3].ToStandard(10752), 1514500000, 1352550000);
        private Satellite mercury = new Satellite(masses[2].ToStandard(0.055), masses[1].ToStandard(1), 57910000, 0.205630, times[3].ToStandard(87.9685), 69820000, 46000000);
        private Satellite theMoon = new Satellite(masses[2].ToStandard(0.0123), masses[2].ToStandard(1), 384400, 0.0549, times[2].ToStandard(29.530589), 405500, 363300);

        private void SatelliteToDebug(Satellite satellite)
        {
            Debug.WriteLine("Mass: " + satellite.Mass);
            Debug.WriteLine("Mass of Object: " + satellite.MassOfCenter);
            Debug.WriteLine("Semi-major Axis: " + satellite.SemiMajorAxis);
            Debug.WriteLine("Semi-minor Axis: " + satellite.SemiMinorAxis);
            Debug.WriteLine("Eccentricity: " + satellite.Eccentricity);
            Debug.WriteLine("Orbital period: " + satellite.OrbitalPeriod);
            Debug.WriteLine("Aphelion: " + satellite.Aphelion);
            Debug.WriteLine("Parihelion: " + satellite.Perihelion);
            Debug.WriteLine("Distance between focci: " + satellite.DistanceBetweenFocci);
        }

        private bool MarginOfError(double result, double expected, string title = "")
        {
            Debug.WriteLine(title + "Expected: " + expected + ", Result: " + result);
            return ((Math.Abs(result / expected) - 1) < 0.01);
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

        [TestMethod]
        public void SatelliteTest1()
        {
            Satellite satellite = new Satellite();
            satellite.Aphelion = mars.Aphelion;
            satellite.Perihelion = mars.Perihelion;

            satellite.FindOtherValues();

            SatelliteToDebug(satellite);
            Assert.IsTrue(MarginOfError(satellite.SemiMajorAxis, mars.SemiMajorAxis));
            Assert.IsTrue(MarginOfError(satellite.Eccentricity, mars.Eccentricity));
        }

        [TestMethod]
        public void SatelliteTest2()
        {
            Satellite satellite = new Satellite();
            satellite.Eccentricity = jupiter.Eccentricity;
            satellite.SemiMajorAxis = jupiter.SemiMajorAxis;

            satellite.FindOtherValues();

            SatelliteToDebug(satellite);
            Assert.IsTrue(MarginOfError(satellite.Aphelion, jupiter.Aphelion, "Aphelion"));
            Assert.IsTrue(MarginOfError(satellite.Perihelion, jupiter.Perihelion, "Perihelion"));
        }

        [TestMethod]
        public void SatelliteTest3()
        {
            Satellite satellite = new Satellite();
            satellite.Mass = theMoon.Mass;
            satellite.SemiMajorAxis = theMoon.SemiMajorAxis;
            satellite.OrbitalPeriod = theMoon.OrbitalPeriod;

            satellite.FindOtherValues();
            SatelliteToDebug(satellite);

            Assert.IsTrue(MarginOfError(satellite.MassOfCenter, theMoon.MassOfCenter));
        }

        [TestMethod]
        public void SatelliteTests4()
        {
            Satellite satellite = new Satellite();
            satellite.DistanceBetweenFocci = saturn.DistanceBetweenFocci;
            satellite.SemiMajorAxis = saturn.SemiMajorAxis;

            satellite.FindOtherValues();
            SatelliteToDebug(satellite);

            Assert.IsTrue(MarginOfError(satellite.Eccentricity, saturn.Eccentricity));
            Assert.IsTrue(MarginOfError(satellite.Aphelion, saturn.Aphelion));
            Assert.IsTrue(MarginOfError(satellite.Perihelion, saturn.Perihelion));
        }
    }
}
