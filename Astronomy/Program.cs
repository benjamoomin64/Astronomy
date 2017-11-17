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
                        star.ArcSeconds = value;
                        break;
                    case 2:
                        star.ArcMinutes = value;
                        break;
                    case 3:
                        star.Radius = value;
                        break;
                    case 4:
                        star.Temperature = value;
                        break;
                    case 5:
                        star.Lightyears = value;
                        break;
                    case 6:
                        star.Parsecs = value;
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
                        star.Redshift = value;
                        break;
                    case 12:
                        star.RegressionalVelocity = value;
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

    struct Star
    {
        public double ParallaxMinutes
        {
            get { return this.ParallaxMinutes; }
            set
            {
                this.ParallaxMinutes = value;
                ParallaxSeconds = value * 60;
            }
        }
        public double ParallaxSeconds
        {
            get { return this.ParallaxSeconds; }
            set
            {
                this.ParallaxSeconds = value;
                ParallaxMinutes = value / 60;
            }
        }
        public double Luminosity
        { get; set; }
        public double Brightness
        { get; set; }
        public double AbsoluteMagnitude
        { get; set; }

    }

    class BetterStar
    {
        private Dictionary<string, bool> WhatIKnow = new Dictionary<string, bool>();
        private Dictionary<string, double> WhatTheyAre = new Dictionary<string, double>();

        private Dictionary<string, double> SunValues = new Dictionary<string, double>();

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
            foreach (string item in variables)
            {
                WhatIKnow.Add(item, false);
                WhatTheyAre.Add(item, double.NaN);
            }
            SunStuffs();
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
            foreach (KeyValuePair<string, double> pair in WhatTheyAre)
            {
                Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }
            

        public void FindOtherValues()
        {
            double sigma = 0.00000000567;
            bool didSomething = true;
            while (didSomething)
            {
                //Console.Clear();
                //ToConsole();

                didSomething = false;
                if (!WhatIKnow["arcseconds"])
                {
                    if (WhatIKnow["parsecs"])
                    {
                        ArcSeconds = 1 / Parsecs;
                        didSomething = true;
                        continue;
                    }
                    
                }
                if (!WhatIKnow["radius"])
                {
                    if (WhatIKnow["luminosity"] && WhatIKnow["temperature"])
                    {
                        Radius = Math.Sqrt(Luminosity / (Math.Pow(Temperature, 4) * 4 * Math.PI * sigma));
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["temperature"])
                {
                    if (WhatIKnow["luminosity"] && WhatIKnow["radius"])
                    {
                        Temperature = Math.Pow((Luminosity / (4 * Math.PI * Radius * Radius * sigma)), .25);
                        didSomething = true;
                        continue;
                    }

                }
                if (!WhatIKnow["lightyears"])
                {
                    if (WhatIKnow["parsecs"])
                    {
                        Lightyears = Parsecs * 3.26156;
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["parsecs"])
                {
                    if (WhatIKnow["lightyears"])
                    {
                        Parsecs = Lightyears * 0.306601;
                        didSomething = true;
                        continue;
                    }
                    if (WhatIKnow["arcseconds"])
                    {
                        Parsecs = 1 / ArcSeconds;
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["luminosity"])
                {
                    if (WhatIKnow["radius"] && WhatIKnow["temperature"])
                    {
                        Luminosity = Math.Pow(Radius, 2) * Math.Pow(Temperature, 4) * 4 * Math.PI * sigma;
                        didSomething = true;
                        continue;
                    }
                    if (WhatIKnow["absolute magnitude"])
                    {
                        Luminosity = Math.Pow(2.512, (SunValues["absolute magnitude"] - WhatTheyAre["absolute magnitude"])) * SunValues["luminosity"];
                        didSomething = true;
                        continue;
                    }

                }
                if (!WhatIKnow["absolute magnitude"])
                {
                    if (WhatIKnow["apparent magnitude"] && WhatIKnow["parsecs"])
                    {
                        AbsoluteMagnitude = -1 * (5 * Math.Log10(Parsecs / 10) - ApparentMagnitude);
                        didSomething = true;
                        continue;
                    }
                    if (WhatIKnow["luminosity"])
                    {
                        AbsoluteMagnitude = (2.5 * Math.Log10(Luminosity / SunValues["luminosity"])) + SunValues["absolute magnitude"];
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["apparent magnitude"])
                {
                    if (WhatIKnow["luminosity"])
                    {
                        ApparentMagnitude = (2.5 * Math.Log10(Luminosity / SunValues["luminosity"])) + SunValues["apparent magnitude"];
                        didSomething = true;
                        continue;
                    }
                    if (WhatIKnow["absolute magnitude"] && WhatIKnow["parsecs"])
                    {
                        ApparentMagnitude = (5 * Math.Log10(Parsecs / 10)) + AbsoluteMagnitude;
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["redshift"])
                {
                    if (WhatIKnow["regressional velocity"])
                    {
                        Redshift = RegressionalVelocity / 299800000;
                        didSomething = true;
                        continue;
                    }
                }
                if (!WhatIKnow["regressional velocity"])
                {
                    if (WhatIKnow["redshift"])
                    {
                        RegressionalVelocity = Redshift * 299800000;
                        didSomething = true;
                        continue;
                    }
                }
            }
            ToConsole();
        }



        // Brightness equals Luminosity divided by four pi Distance^2
        private double LumToBrightness()
        {
            return (Luminosity / (4 * Math.PI * Math.Pow((Lightyears * 9460730800000), 2)));
        }


        public double Brightness
        {
            get { return WhatTheyAre["brightness"]; }
            set { WhatTheyAre["brightness"] = value; WhatIKnow["brightness"] = true;  }
        }

        public double RegressionalVelocity
        {
            get { return WhatTheyAre["regressional velocity"]; }
            set { WhatTheyAre["regressional velocity"] = value; WhatIKnow["regressional velocity"] = true;  }
        }

        public double Redshift
        {
            get { return WhatTheyAre["redshift"]; }
            set { WhatTheyAre["redshift"] = value; WhatIKnow["redshift"] = true;  }
        }

        public double MagnitudeRatio
        {
            get { return WhatTheyAre["abs/app Magnitude ratio"]; }
            set { WhatTheyAre["abs/app Magnitude ratio"] = value; WhatIKnow["abs/app Magnitude ratio"] = true;  }
        }

        public double ApparentMagnitude
        {
            get { return WhatTheyAre["apparent magnitude"]; }
            set { WhatTheyAre["apparent magnitude"] = value; WhatIKnow["apparent magnitude"] = true;  }
        }

        public double AbsoluteMagnitude
        {
            get { return WhatTheyAre["absolute magnitude"]; }
            set { WhatTheyAre["absolute magnitude"] = value; WhatIKnow["absolute magnitude"] = true;  }
        }

        public double Luminosity
        {
            get { return WhatTheyAre["luminosity"]; }
            set { WhatTheyAre["luminosity"] = value; WhatIKnow["luminosity"] = true;  }
        }

        public double Parsecs
        {
            get { return WhatTheyAre["parsecs"]; }
            set { WhatTheyAre["parsecs"] = value; WhatIKnow["parsecs"] = true;  }
        }

        public double Lightyears
        {
            get { return WhatTheyAre["lightyears"]; }
            set { WhatTheyAre["lightyears"] = value; WhatIKnow["lightyears"] = true;  }
        }

        public double Temperature
        {
            get { return WhatTheyAre["temperature"]; }
            set { WhatTheyAre["temperature"] = value; WhatIKnow["temperature"] = true;  }
        }

        public double Radius
        {
            get { return WhatTheyAre["radius"]; }
            set { WhatTheyAre["radius"] = value; WhatIKnow["radius"] = true;  }
        }

        public double ArcSeconds
        {
            get
            {
                return this.WhatTheyAre["arcseconds"];
            }
            set
            {
                this.WhatTheyAre["arcseconds"] = value;
                this.WhatIKnow["arcseconds"] = true;
                this.WhatTheyAre["arcminutes"] = value / 60;
                this.WhatIKnow["arcminutes"] = true;
                
            }
        }

        public double ArcMinutes
        {
            get
            {
                return this.WhatTheyAre["arcminutes"];
            }
            set
            {
                ArcSeconds = value * 60;
            }
        } // Done, calls the Arcseconds thing
    }
}
