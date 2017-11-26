using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy
{
    public enum Measurement { deFault, masses, distances, times, parallax, temperature, luminosity };
    public struct UnitConversion
    {
        private string name;
        private double conversion;
        public UnitConversion(string name, double conversionToSIStandard)
        {
            this.name = name;
            this.conversion = conversionToSIStandard;
        }

        public string GetName() { return name; }

        public double ToStandard(double given) { return given * conversion; }

        public double FromStandard(double given) { return given / conversion; }

    }
    class Program
    {
        public static UnitConversion[] masses = new UnitConversion[]{ new UnitConversion("kg", 1), new UnitConversion("solar mass", (1.98855 * Math.Pow(10, 30))), new UnitConversion("earth mass", (5.97237 * Math.Pow(10, 24))) };
        public static UnitConversion[] distances = { new UnitConversion("meter", 1), new UnitConversion("kilometers", 1000), new UnitConversion("Parsecs", (3.08567782 * Math.Pow(10, 16))),
            new UnitConversion("lightyear", (9.46073047258 * Math.Pow(10, 15))), new UnitConversion("AU", (1.495978707 * Math.Pow(10, 11))), new UnitConversion("miles", 1.60938),
            new UnitConversion("lightsecond", 299792458)};
        public static UnitConversion[] times = { new UnitConversion("seconds", 1), new UnitConversion("minutes", 60), new UnitConversion("hours", 3600), new UnitConversion("days", 86400),
            new UnitConversion("week", 604800), new UnitConversion("year", 31556952)};
        public static UnitConversion[] parallax = { new UnitConversion("arcseconds", 1), new UnitConversion("arcminutes", 60), new UnitConversion("milliarcseconds", 0.001) };
        public static UnitConversion[] luminosity = { new UnitConversion("watts", 1), new UnitConversion("solar luminosity", (3.828 * Math.Pow(10, 26))) };
        

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Astronomy app! How may I help you?");
            string input = "";
            while(true)
            {
                input = Console.ReadLine();
                if (KeepGoing(input))
                    break;
                if (input.ToLower().Contains("star"))
                    Star();
                else if (input.ToLower().Contains("satellite"))
                    SatelliteOrbitingSomething();


            }
        }

        static UnitConversion FindUnitValue(Measurement local)
        {
            switch (local)
            {
                case Measurement.masses:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < masses.Length; a++)
                    {
                        Console.WriteLine(a + ": " + masses[a].GetName());
                    }
                    return masses[FindIndex(masses.Length)];
                case Measurement.distances:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < distances.Length; a++)
                    {
                        Console.WriteLine(a + ": " + distances[a].GetName());
                    }
                    return distances[FindIndex(distances.Length)];
                case Measurement.times:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < times.Length; a++)
                    {
                        Console.WriteLine(a + ": " + times[a].GetName());
                    }
                    return times[FindIndex(times.Length)];
                case Measurement.parallax:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < parallax.Length; a++)
                    {
                        Console.WriteLine(a + ": " + parallax[a].GetName());
                    }
                    return parallax[FindIndex(parallax.Length)];
                case Measurement.luminosity:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < luminosity.Length; a++)
                    {
                        Console.WriteLine(a + ": " + luminosity[a].GetName());
                    }
                    return luminosity[FindIndex(luminosity.Length)];
                case Measurement.temperature:
                    return new UnitConversion("Kelvin", 1);
                default:
                    return new UnitConversion("real number", 1);
            }

        }
        static int FindIndex(int maximum)
        {
            int index;
            try
            {
                index = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("That was terrible, you entered a word when we specifically asked for an integer. Try again.");
                index = FindIndex(maximum);
            }

            if (index < maximum)
                return index;
            else
            {
                Console.WriteLine("That integer was out of range. Try again");
                return FindIndex(maximum);
            }
        }
        static bool KeepGoing(string input)
        {
            return (input.ToLower() == "quit");
        }

        static void Star()
        {
            BetterStar star = new BetterStar();
            string input = "";
            int index = 110;
            double value;
            Measurement local;
            string[] variables = new string[]{ "", "parallax", "radius", "temperature", "distance", "luminosity",
                "absolute magnitude", "apparent magnitude", "abs/app Magnitude ratio", "redshift", "regressional velocity"};
            
            while (input.ToLower() != "done")
            {
                Console.WriteLine("What values do you know?");
                for (int i = 1; i < variables.Length; i++)
                {
                    Console.WriteLine(i + ": " + variables[i]);
                }


                index = FindIndex(variables.Length);

                if (index == 1)
                    local = Measurement.parallax;
                else if (index == 5)
                    local = Measurement.luminosity;
                else if (index == 4 || index == 2)
                    local = Measurement.distances;
                else if (index == 3)
                    local = Measurement.temperature;
                else
                    local = Measurement.deFault;
                UnitConversion whatWeUse = FindUnitValue(local);


                Console.WriteLine("Excelent! So you're trying to input " + variables[index] + " in " + whatWeUse.GetName() + ". What's the value?");

                while (!double.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("I'm sorry that was not a numeric value, can you please try again?");
                }

                switch (index)
                {
                    case 1:
                        star.Spatial.Parallax = whatWeUse.ToStandard(value);
                        break;
                    case 2:
                        star.Radius = whatWeUse.ToStandard(value);
                        break;
                    case 3:
                        star.Temperature = whatWeUse.ToStandard(value);
                        break;
                    case 4:
                        star.Spatial.Distance = whatWeUse.ToStandard(value);
                        break;
                    case 5:
                        star.Luminosity = whatWeUse.ToStandard(value);
                        break;
                    case 6:
                        star.AbsoluteMagnitude = whatWeUse.ToStandard(value);
                        break;
                    case 7:
                        star.ApparentMagnitude = whatWeUse.ToStandard(value);
                        break;
                    case 8:
                        star.MagnitudeRatio = whatWeUse.ToStandard(value);
                        break;
                    case 9:
                        star.Spatial.Redshift = whatWeUse.ToStandard(value);
                        break;
                    case 10:
                        star.Spatial.RegressionalVelocity = whatWeUse.ToStandard(value);
                        break;
                }
                Console.WriteLine("Sweet. Data logged. Are you done inputting values?");
                input = Console.ReadLine();
                
                index = 100;
            }
            Console.WriteLine("Y'all ready for this?");

            star.FindOtherValues();
            star.ToConsole();
        }
        
        static void SatelliteOrbitingSomething()
        {
            Satellite satellite = new Satellite();
            string input = "";
            int valueIndex;
            double value;
            string[] objectInformation = {"", "mass of satellite", "mass of object satellite is orbitting", "semi-major axis", "semi-minor axis",
                "eccentricity", "orbital period", "appehelion", "parahelion", "distance between focci" };
            Measurement local;

            while (input.ToLower() != "done")
            {
                Console.WriteLine("What do you know about this object?");
                for (int i = 1; i < objectInformation.Length; i++)
                    Console.WriteLine(i + ": " + objectInformation[i]);

                valueIndex = FindIndex(objectInformation.Length);

                if (valueIndex == 5)
                    local = Measurement.deFault;
                else if (valueIndex == 6)
                    local = Measurement.times;
                else if (valueIndex == 1 || valueIndex == 2)
                    local = Measurement.masses;
                else
                    local = Measurement.distances;

                UnitConversion whatWeUse = FindUnitValue(local);

                Console.WriteLine("Okay! What's the value?");
                while (!double.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("I'm sorry but that was terrible. You included a letter somewhere. Try again.");
                }

                switch (valueIndex)
                {
                    case 1:
                        satellite.Mass = whatWeUse.ToStandard(value);
                        break;
                    case 2:
                        satellite.MassOfCenter = whatWeUse.ToStandard(value);
                        break;
                    case 3:
                        satellite.SemiMajorAxis = whatWeUse.ToStandard(value);
                        break;
                    case 4:
                        satellite.SemiMinorAxis = whatWeUse.ToStandard(value);
                        break;
                    case 5:
                        satellite.Eccentricity = whatWeUse.ToStandard(value);
                        break;
                    case 6:
                        satellite.OrbitalPeriod = whatWeUse.ToStandard(value);
                        break;
                    case 7:
                        satellite.Aphelion = whatWeUse.ToStandard(value);
                        break;
                    case 8:
                        satellite.Perihelion = whatWeUse.ToStandard(value);
                        break;
                    case 9:
                        satellite.DistanceBetweenFocci = whatWeUse.ToStandard(value);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Sweet! Are you done inputting data? (type \"done\" for yes)");
                input = Console.ReadLine();

            }

            satellite.FindOtherValues();
            Console.Clear();
            Console.WriteLine(satellite.ToString());
        }

        public static string AllUnits(UnitConversion[] units, double baseValue)
        {
            string added = "";
            foreach (UnitConversion unit in units)
            {
                added = added + "\t" + unit.GetName() + ": " + (unit.FromStandard(baseValue)) + "\n";
            }
            return added;
        }
    }
    

    public class Satellite
    {
        private double gConstant = 6.67408 * Math.Pow(10, -11);
        /* Things about a satellite I can know:
        * Mass of satellite (kg)
        * Mass of object satellite is orbitting (kg)
        * semi-major axis (meter)
        * semi-minor axis (meter)
        * eccentricity (just a value)
        * orbital period (seconds)
        * Appehelion (meter)
        * Parahelion (meter)
        * Distance to the second focci at
        *      Appehelion
        *      Parahelion
        * 
        * "mass of satellite", "mass of object satellite is orbitting", "semi-major axis", "semi-minor axis",
            "eccentricity", "orbital period", "appehelion", "parahelion", "distance between focci"
        */

        public Satellite()
        {
            Mass = double.NaN;
            MassOfCenter = double.NaN;
            SemiMajorAxis = double.NaN;
            SemiMinorAxis = double.NaN;
            Eccentricity = double.NaN;
            OrbitalPeriod = double.NaN;
            Aphelion = double.NaN;
            Perihelion = double.NaN;
            DistanceBetweenFocci = double.NaN;
        }

        public Satellite(double mass, double massOfCenter, double semiMajorAxis, double eccentricity,
            double orbitalPeriod, double aphelion, double perihelion)
        {
            Mass = mass;
            MassOfCenter = massOfCenter;
            SemiMajorAxis = semiMajorAxis;
            Eccentricity = eccentricity;
            OrbitalPeriod = orbitalPeriod;
            Aphelion = aphelion;
            Perihelion = perihelion;
            DistanceBetweenFocci = double.NaN;
            SemiMinorAxis = double.NaN;
            CalculateSemiMinorAxis();
            CalculateDistanceBetweenFocci();
        }

        public void FindOtherValues()
        {
            for (int a = 0; a < 6; a++)
            {
                CalculateMass();
                CalculateMassOfCenter();
                CalculateSemiMajorAxis();
                CalculateSemiMinorAxis();
                CalculateEccentricity();
                CalculateDistanceBetweenFocci();
                CalculateOrbitalPeriod();
                CalculateAphelion();
                CalculatePerihelion();
            }
        }

        #region CalculateMethods
        private void CalculateMass()
        {
            if (double.IsNaN(Mass))
            {
                if (!double.IsNaN(MassOfCenter) && !double.IsNaN(OrbitalPeriod) && !double.IsNaN(SemiMajorAxis))
                {
                    Mass = (4 * Math.PI * Math.PI * Math.Pow(SemiMajorAxis, 3)) / (gConstant * OrbitalPeriod * OrbitalPeriod) - MassOfCenter;
                }
            }
        }

        private void CalculateMassOfCenter()
        {
            if (double.IsNaN(MassOfCenter))
            {
                if (!double.IsNaN(Mass) && !double.IsNaN(OrbitalPeriod) && !double.IsNaN(SemiMajorAxis))
                {
                    MassOfCenter = (4 * Math.PI * Math.PI * Math.Pow(SemiMajorAxis, 3)) / (gConstant * OrbitalPeriod * OrbitalPeriod) - Mass;
                }
            }
        }

        private void CalculateSemiMajorAxis()
        {
            if (double.IsNaN(SemiMajorAxis))
            {
                if (!double.IsNaN(Mass) && !double.IsNaN(MassOfCenter) && !double.IsNaN(OrbitalPeriod))
                {
                    SemiMajorAxis = Math.Pow((OrbitalPeriod * OrbitalPeriod * gConstant * (Mass + MassOfCenter)) / (4 * Math.PI * Math.PI), (1 / 3));
                }
                else if (!double.IsNaN(Eccentricity) && !double.IsNaN(DistanceBetweenFocci))
                {
                    SemiMajorAxis = (DistanceBetweenFocci / 2) / Eccentricity;
                }
                else if (!double.IsNaN(Aphelion) && !double.IsNaN(Perihelion))
                {
                    SemiMajorAxis = (Aphelion + Perihelion) / 2;
                }
                else if (!double.IsNaN(Aphelion) && !double.IsNaN(DistanceBetweenFocci))
                {
                    SemiMajorAxis = Aphelion - (DistanceBetweenFocci / 2);
                }
                else if (!double.IsNaN(Perihelion) && !double.IsNaN(DistanceBetweenFocci))
                {
                    SemiMajorAxis = Perihelion + (DistanceBetweenFocci / 2);
                }
                else if (!double.IsNaN(SemiMinorAxis) && !double.IsNaN(Eccentricity))
                {
                    SemiMajorAxis = SemiMinorAxis / Math.Sqrt(1 - (Eccentricity * Eccentricity));
                }
            }

        }

        private void CalculateSemiMinorAxis()
        {
            if (double.IsNaN(SemiMinorAxis))
            {
                if (!double.IsNaN(Eccentricity) && !double.IsNaN(SemiMajorAxis))
                {
                    SemiMinorAxis = SemiMajorAxis * Math.Sqrt(1 - (Eccentricity * Eccentricity));
                }
            }

        }

        private void CalculateEccentricity()
        {
            if (double.IsNaN(Eccentricity))
            {
                if (!double.IsNaN(SemiMinorAxis) && !double.IsNaN(SemiMajorAxis))
                {
                    Eccentricity = Math.Sqrt(1 - ((SemiMinorAxis * SemiMinorAxis) / (SemiMajorAxis * SemiMajorAxis)));
                }
                if (!double.IsNaN(DistanceBetweenFocci) && !double.IsNaN(SemiMajorAxis))
                {
                    Eccentricity = DistanceBetweenFocci / (2 * SemiMajorAxis);
                }
            }
        }

        private void CalculateDistanceBetweenFocci()
        {
            if (double.IsNaN(DistanceBetweenFocci))
            {
                if (!double.IsNaN(Aphelion) && !double.IsNaN(Perihelion))
                {
                    DistanceBetweenFocci = Aphelion - Perihelion;
                }
                else if (!double.IsNaN(Eccentricity) && !double.IsNaN(SemiMajorAxis))
                {
                    DistanceBetweenFocci = Eccentricity * (2 * SemiMajorAxis);
                }
            }
        }

        private void CalculateOrbitalPeriod()
        {
            if (double.IsNaN(OrbitalPeriod))
            {
                if (!double.IsNaN(Mass) && !double.IsNaN(MassOfCenter) && !double.IsNaN(SemiMajorAxis))
                {
                    OrbitalPeriod = Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(SemiMajorAxis, 3)) / (gConstant * (Mass + MassOfCenter)));
                }
            }
        }

        private void CalculateAphelion()
        {
            if (double.IsNaN(Aphelion))
            {
                if (!double.IsNaN(Perihelion) && !double.IsNaN(SemiMajorAxis))
                {
                    Aphelion = (2 * SemiMajorAxis) - Perihelion;
                }
                else if (!double.IsNaN(SemiMajorAxis) && !double.IsNaN(DistanceBetweenFocci))
                {
                    Aphelion = SemiMajorAxis + (DistanceBetweenFocci / 2);
                }
            }
        }

        private void CalculatePerihelion()
        {
            if (double.IsNaN(Perihelion))
            {
                if (!double.IsNaN(Aphelion) && !double.IsNaN(SemiMajorAxis))
                {
                    Perihelion = (2 * SemiMajorAxis) - Aphelion;
                }
                else if (!double.IsNaN(SemiMajorAxis) && !double.IsNaN(DistanceBetweenFocci))
                {
                    Perihelion = SemiMajorAxis - (DistanceBetweenFocci / 2);
                }
            }
        }

        #endregion

        public override string ToString()
        {
            string output = "";
            output = output + "Mass: \n";
            output = output + Program.AllUnits(Program.masses, Mass);
            output = output + "Mass of object being orbitted: \n";
            output = output + Program.AllUnits(Program.masses, MassOfCenter);
            output = output + "Semi-major Axis: \n";
            output = output + Program.AllUnits(Program.distances, SemiMajorAxis);
            output = output + "Semi-minor Axis: \n";
            output = output + Program.AllUnits(Program.distances, SemiMinorAxis);
            output = output + "Eccentricity: " + Eccentricity + "\n";
            output = output + "Orbital Period: \n";
            output = output + Program.AllUnits(Program.times, OrbitalPeriod);
            output = output + "Aphelion: \n";
            output = output + Program.AllUnits(Program.distances, Aphelion);
            output = output + "Perihelion: \n";
            output = output + Program.AllUnits(Program.distances, Perihelion);
            output = output + "Distance between focci: \n";
            output = output + Program.AllUnits(Program.distances, DistanceBetweenFocci);

            return output;
        }


        #region Values

        public double Mass { get; set; } // in kg

        public double MassOfCenter { get; set; } // also in kg

        public double SemiMajorAxis { get; set; }

        public double SemiMinorAxis { get; set; } // both in meters

        public double Eccentricity { get; set; }

        public double OrbitalPeriod { get; set; } // in seconds

        public double Aphelion { get; set; }

        public double Perihelion { get; set; } // in km

        public double DistanceBetweenFocci { get; set; } // in km

        #endregion

    }

    public class BetterStar
    {
        private Dictionary<string, double> SunValues = new Dictionary<string, double>();
        public SpatialMeasurements Spatial = new SpatialMeasurements();
        /* Things about a star I can know:
         * Radius (meters)
         * Temperature (kelvin)
         * Parallax, in arcseconds and arcminutes 
         * Distance, in lightyears and parsecs
         * Luminosity, in watts
         * Absolute magnitude
         * Apparent Magnitude
         * Absolute Magnitude divided by Apparent Magnitude
         * Redshift (z)
         * Regressional velocity
         */

        public string[] variables = new string[]{ "arcseconds", "arcminutes", "radius", "temperature", "lightyears", "parsecs", "luminosity",
            "absolute magnitude", "apparent magnitude", "abs/app Magnitude ratio", "redshift", "regressional velocity"};

        public BetterStar()
        {
            SunStuffs();
            Radius = double.NaN;
            Temperature = double.NaN;
            Luminosity = double.NaN;
            AbsoluteMagnitude = double.NaN;
            ApparentMagnitude = double.NaN;
            MagnitudeRatio = double.NaN;

        }

        private void SunStuffs()
        {
            double[] sunData = new double[]{double.NaN, double.NaN, 695700000, 15700000, double.NaN, 0.000004848, (3.828 * Math.Pow(10, 26)),
                4.83, -26.74, (4.83 / -26.74), double.NaN, 0, -26.74};
            for (int i = 0; i < variables.Length; i++)
            {
                SunValues.Add(variables[i], sunData[i]);
            }


        }

        public void ToConsole()
        {
            Console.WriteLine(Spatial.ToString());
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            string output = "Stellar measurements: \n";

            output = output + "Radius: \n";
            output = output + Program.AllUnits(Program.distances, Radius);
            output = output + "Temperature: " + Temperature + "\n";
            output = output + "Luminosity: \n";
            output = output + Program.AllUnits(Program.luminosity, Luminosity);
            output = output + "Absolute Magnitude: " + AbsoluteMagnitude + " \n";
            output = output + "Apparent Magnitude: " + ApparentMagnitude + "\n";
            output = output + "Absolute/Apparent Magnitude Ratio: " + MagnitudeRatio + "\n";

            
            return output;
        }

        public void FindOtherValues()
        {
            Spatial.FindOthers();
            double sigma = 0.00000000567;
            bool didSomething = true;
            while (didSomething)
            {
                didSomething = false;

                if (double.IsNaN(Radius))
                {
                    if (!double.IsNaN(Luminosity) && !double.IsNaN(Temperature))
                    {
                        Radius = Math.Sqrt(Luminosity / (Math.Pow(Temperature, 4) * 4 * Math.PI * sigma));
                        didSomething = true;
                        continue;
                    }
                }
                if (double.IsNaN(Temperature))
                {
                    if (!double.IsNaN(Luminosity) && !double.IsNaN(Radius))
                    {
                        Temperature = Math.Pow((Luminosity / (4 * Math.PI * Radius * Radius * sigma)), .25);
                        didSomething = true;
                        continue;
                    }

                }
                if (double.IsNaN(Luminosity))
                {
                    if (!double.IsNaN(Radius) && double.IsNaN(Temperature))
                    {
                        Luminosity = Math.Pow(Radius, 2) * Math.Pow(Temperature, 4) * 4 * Math.PI * sigma;
                        didSomething = true;
                        continue;
                    }
                    if (!double.IsNaN(AbsoluteMagnitude))
                    {
                        Luminosity = Math.Pow(2.512, (SunValues["absolute magnitude"] - AbsoluteMagnitude)) * SunValues["luminosity"];
                        didSomething = true;
                        continue;
                    }

                }
                if (double.IsNaN(AbsoluteMagnitude))
                {
                    if (!double.IsNaN(ApparentMagnitude) && !double.IsNaN(Spatial.Distance))
                    {
                        AbsoluteMagnitude = -1 * (5 * Math.Log10(Program.distances[2].FromStandard(Spatial.Distance) / 10) - ApparentMagnitude);
                        didSomething = true;
                        continue;
                    }
                    if (!double.IsNaN(Luminosity))
                    {
                        AbsoluteMagnitude = (2.5 * Math.Log10(Luminosity / SunValues["luminosity"])) + SunValues["absolute magnitude"];
                        didSomething = true;
                        continue;
                    }
                }
                if (double.IsNaN(ApparentMagnitude))
                {
                    //if (!double.IsNaN(Luminosity))
                    //{
                    //    ApparentMagnitude = (2.5 * Math.Log10(Luminosity / SunValues["luminosity"])) + SunValues["apparent magnitude"];
                    //    didSomething = true;
                    //    continue;
                    //}
                    if (!double.IsNaN(AbsoluteMagnitude) && !double.IsNaN(Spatial.Distance))
                    {
                        ApparentMagnitude = (5 * Math.Log10(Program.distances[2].FromStandard(Spatial.Distance))) - 5 + AbsoluteMagnitude;
                        didSomething = true;
                        continue;
                    }
                }
                if (double.IsNaN(Spatial.Distance))
                {
                    Spatial.Distance = Math.Pow(10, (((ApparentMagnitude - AbsoluteMagnitude) / 5) + 1));
                }

            }
        }
        

        #region values

        public double MagnitudeRatio
        { get; set; }

        public double ApparentMagnitude
        { get; set; }

        public double AbsoluteMagnitude
        { get; set; }

        public double Luminosity
        { get; set; }

        public double Temperature
        { get; set; }

        public double Radius
        {
            get; set;
        }
        #endregion

    }

    public class SpatialMeasurements
    {
        public SpatialMeasurements()
        {
            RegressionalVelocity = double.NaN;
            Distance = double.NaN;
            Parallax = double.NaN;
            Redshift = double.NaN;
        }

        public override string ToString()
        {
            string output = "";
            output = output + "Parallax: \n";
            output = output + Program.AllUnits(Program.parallax, Parallax);
            output = output + "Redshift: " + Redshift + "\n";
            output = output + "Regressional velocity: " + RegressionalVelocity + "\n";
            output = output + "Distance: \n";
            output = output + Program.AllUnits(Program.distances, Distance);
            return output;
        }

        public void FindOthers()
        {
            if (double.IsNaN(RegressionalVelocity))
            {
                RegressionalVelocity = Redshift * 299800000;
            }
            if (double.IsNaN(Redshift))
            {
                Redshift = RegressionalVelocity / 299800000;
            }
            if (double.IsNaN(Parallax))
            {
                Parallax = 1 / (Distance / (3.08567782 * Math.Pow(10, 16)));
            }
            if (double.IsNaN(Distance))
            {
                Distance = (1 / Parallax) * (3.08567782 * Math.Pow(10, 16));
            }

        }

        public double RegressionalVelocity
        {
            get;
            set;
        }
        public double Parallax
        {
            get;
            set;
        }
        
        public double Distance
        { get; set; }
        
        public double Redshift
        { get; set; }

    }

}

namespace MyExtensions
{
    static class Extensions
    {
        public static bool IsNaN(this double x)
        {
            return (x == double.NaN);
        }
    }
}