using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy
{
    public enum Measurement { masses, distances, times };

    class Program
    {
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

        }

        public static UnitConversion[] masses = new UnitConversion[]{ new UnitConversion("kg", 1), new UnitConversion("solar mass", (1.98855 * Math.Pow(10, 30))) };
        public static UnitConversion[] distances = { new UnitConversion("meter", 1), new UnitConversion("kilometers", 1000), new UnitConversion("Parsecs", (3.08567782 * Math.Pow(10, 16))),
            new UnitConversion("lightyear", (9.46073047258 * Math.Pow(10, 15))), new UnitConversion("AU", (1.495978707 * Math.Pow(10, 11))), new UnitConversion("miles", 1.60938),
            new UnitConversion("lightsecond", 299792458)};
        public static UnitConversion[] times = { new UnitConversion("seconds", 1), new UnitConversion("minutes", 60), new UnitConversion("hours", 3600), new UnitConversion("days", 86400),
            new UnitConversion("week", 604800), new UnitConversion("year", 31556952)};
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
            
            while (input.ToLower() != "done")
            {
                Console.WriteLine("What values do you know?");
                for (int i = 1; i <= star.variables.Length; i++)
                {
                    Console.WriteLine(i + ": " + star.variables[i - 1]);
                }

                while (index > star.variables.Length)
                {
                    while (!int.TryParse(Console.ReadLine(), out index))
                    {
                        Console.WriteLine("I'm sorry that was not an integer value, can you please try again?");
                    }
                    if (index > star.variables.Length)
                        Console.WriteLine("I'm sorry but that value was outside the bounds of your options. Try again?");
                }
                Console.WriteLine("Excelent! So you're trying to input " + star.variables[index - 1] + ". What's the value?");

                while (!double.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("I'm sorry that was not a numeric value, can you please try again?");
                }

                switch (index)
                {
                    case 1:
                        star.Spatial.ArcSeconds = value;
                        break;
                    case 2:
                        star.Spatial.ArcMinutes = value;
                        break;
                    case 3:
                        star.Radius = value;
                        break;
                    case 4:
                        star.Temperature = value;
                        break;
                    case 5:
                        star.Spatial.Lightyears = value;
                        break;
                    case 6:
                        star.Spatial.Parsecs = value;
                        break;
                    case 7:
                        star.Luminosity = value;
                        break;
                    case 8:
                        star.AbsoluteMagnitude = value;
                        break;
                    case 9:
                        star.ApparentMagnitude = value;
                        break;
                    case 10:
                        throw new NotImplementedException("You lazy bum you didn't do this bit yet");
                    case 11:
                        star.Spatial.Redshift = value;
                        break;
                    case 12:
                        star.Spatial.RegressionalVelocity = value;
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
            int unitIndex;
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
                    continue;
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


            }



        }

        static UnitConversion FindUnitValue(Measurement local)
        {
            switch (local)
            {
                case Measurement.masses:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < 2; a++)
                    {
                        Console.WriteLine(a + ": " + masses[a].GetName());
                    }
                    return masses[FindIndex(2)];
                case Measurement.distances:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < 7; a++)
                    {
                        Console.WriteLine(a + ": " + distances[a].GetName());
                    }
                    return masses[FindIndex(7)];
                case Measurement.times:
                    Console.WriteLine("Okay, what unit are you going for?");
                    for (int a = 0; a < 6; a++)
                    {
                        Console.WriteLine(a + ": " + times[a].GetName());
                    }
                    return masses[FindIndex(6)];
                default
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

    public class Satellite
    {
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
         *
         */
        
        public Satellite()
        {

        }

        

        #region Values

        public double Mass { get; set; } // in kg
            
        public double MassOfCenter { get; set; }
        
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
            "absolute magnitude", "apparent magnitude", "abs/app Magnitude ratio", "redshift", "regressional velocity", "brightness"};

        public BetterStar()
        {
            SunStuffs();
            Radius = double.NaN;
            Temperature = double.NaN;
            Luminosity = double.NaN;
            AbsoluteMagnitude = double.NaN;
            ApparentMagnitude = double.NaN;
            MagnitudeRatio = double.NaN;
            Brightness = double.NaN;
            
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
            string final = "Stellar measurements: \n";
            final = final + "radius: " + Radius + "\n";
            final = final + "temperature: " + Temperature + "\n";
            final = final + "luminosity: " + Luminosity + "\n";
            final = final + "absolute magnitude: " + AbsoluteMagnitude + "\n";
            final = final + "apparent magnitude: " + ApparentMagnitude + "\n";
            final = final + "abs/app Magnitude ratio: " + MagnitudeRatio + "\n";
            final = final + "brightness: " + Brightness + "\n";
            return final;
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
                    if (!double.IsNaN(ApparentMagnitude) && !double.IsNaN(Spatial.Parsecs))
                    {
                        AbsoluteMagnitude = -1 * (5 * Math.Log10(Spatial.Parsecs / 10) - ApparentMagnitude);
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
                    if (!double.IsNaN(AbsoluteMagnitude)&& !double.IsNaN(Spatial.Parsecs))
                    {
                        ApparentMagnitude = (5 * Math.Log10(Spatial.Parsecs)) - 5 + AbsoluteMagnitude;
                        didSomething = true;
                        continue;
                    }
                }
                if (double.IsNaN(Spatial.Parsecs))
                {
                    Spatial.Parsecs = Math.Pow(10, (((ApparentMagnitude - AbsoluteMagnitude) / 5) + 1));
                }
                
            }
        }
        
        // Brightness equals Luminosity divided by four pi Distance^2
        private double LumToBrightness()
        {
            
            return (Luminosity / (4 * Math.PI * Math.Pow((Spatial.Lightyears * 9460730800000), 2)));
        }

        #region values
        public double Brightness
        { get; set; }

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
        { get; set;
        }
        #endregion

    }

    public class SpatialMeasurements
    {
        public SpatialMeasurements()
        {
            RegressionalVelocity = double.NaN;
            ArcSeconds = double.NaN;
            Parsecs = double.NaN;
            Redshift = double.NaN;
        }

        public override string ToString()
        {
            string final = "Spatial measurements: \n";
            final = final + "regressional velocity: " + RegressionalVelocity + "\n";
            final = final + "Arc seconds: " + ArcSeconds + "\n";
            final = final + "Arc minutes: " + ArcMinutes + "\n";
            final = final + "Parsecs: " + Parsecs + "\n";
            final = final + "Light-years: " + Lightyears + "\n";
            final = final + "Redshift: " + Redshift + "\n";

            return final;
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
            if (double.IsNaN(ArcSeconds))
            {
                ArcSeconds = 1 / Parsecs;
            }
            if (double.IsNaN(Parsecs))
            {
                Parsecs = 1 / ArcSeconds;
            }

        }

        public double RegressionalVelocity
        {
            get;
            set;
        }
        public double ArcSeconds
        {
            get;
            set;
        }

        public double ArcMinutes
        {
            get { return ArcSeconds * 60; }
            set { ArcSeconds = value / 60; }
        }
        public double Parsecs
        { get; set; }

        public double Lightyears
        { get { return Parsecs * 3.26156; } set { Parsecs = value / 3.26156; } }
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
}