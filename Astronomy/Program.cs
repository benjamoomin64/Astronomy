using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astronomy
{
    class Program
    {
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
        }


    }

    class BetterStar
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

        private void ToConsole()
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
                //if (double.IsNaN(Lightyears))
                //{
                //    if (!double.IsNaN(Parsecs))
                //    {
                //        Lightyears = Parsecs * 3.26156;
                //        didSomething = true;
                //        continue;
                //    }
                //}
                //if (double.IsNaN(Parsecs))
                //{
                //    if (!double.IsNaN(Lightyears))
                //    {
                //        Parsecs = Lightyears * 0.306601;
                //        didSomething = true;
                //        continue;
                //    }
                //    if (!double.IsNaN(ArcSeconds))
                //    {
                //        Parsecs = 1 / ArcSeconds;
                //        didSomething = true;
                //        continue;
                //    }
                //}
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
                    if (!double.IsNaN(Luminosity))
                    {
                        ApparentMagnitude = (2.5 * Math.Log10(Luminosity / SunValues["luminosity"])) + SunValues["apparent magnitude"];
                        didSomething = true;
                        continue;
                    }
                    if (!double.IsNaN(AbsoluteMagnitude)&& !double.IsNaN(Spatial.Parsecs))
                    {
                        ApparentMagnitude = (5 * Math.Log10(Spatial.Parsecs / 10)) + AbsoluteMagnitude;
                        didSomething = true;
                        continue;
                    }
                }
                //if (double.IsNaN(Redshift))
                //{
                //    if (!double.IsNaN(Spatial.RegressionalVelocity))
                //    {
                //        Redshift = Spatial.RegressionalVelocity / 299800000;
                //        didSomething = true;
                //        continue;
                //    }
                //}
                

                
            }
            ToConsole();
        }
        
        // Brightness equals Luminosity divided by four pi Distance^2
        private double LumToBrightness()
        {
            
            return (Luminosity / (4 * Math.PI * Math.Pow((Spatial.Lightyears * 9460730800000), 2)));
        }



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

    }

    class SpatialMeasurements
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