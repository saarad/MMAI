using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace graphQLService.Helpers
{
    public static class MmaiCalculator
    {
        /// <summary>
        /// Converts the height from Imperial Units to the Metric System.
        /// </summary>
        /// <param name="height">This is the height of the fighter in imperial units.</param>
        /// <returns>Returns the fighter's height in meters.</returns>
        public static double GetHeightMetric(String height)
        {
            char[] charSeparators = new char[] { '\'' };
            String[] arr = height.Split(charSeparators, 2, StringSplitOptions.None);
            double feet = Convert.ToDouble(arr[0]);
            double heightCmFeet = feet * 30.48;
            String normalizedInches = Regex.Replace(arr[1], "\"", "");
            double inches = Convert.ToDouble(normalizedInches);
            double heightCmInches = inches * 2.54;
            double resDouble = heightCmInches + heightCmFeet;
            double roundedRes = Math.Round(resDouble, 0);
            return roundedRes;
        }

        /// <summary>
        /// Converts the weight from Imperial Units to the Metric System.
        /// </summary>
        /// <param name="weight">This is the weight of the fighter in lbs</param>
        /// <returns>Returns the fighter's weight in kilos.</returns>
        public static double GetWeightKg(int? weight)
        {
            //String weightString = Regex.Replace(weight, "lbs", "");
            double weightLbs = Convert.ToDouble(weight);
            double weightKg = 0.45359237 * weightLbs;
            double roundedNumber = Math.Round(weightKg, 0);
            return roundedNumber;
        }

        /// <summary>
        /// Converts the reach from Imperial Units to the Metric System.
        /// </summary>
        /// <param name="reach">This is the reach of the fighter in inches</param>
        /// <returns>Returns the fighters reach in meters.</returns>
        public static double GetReachMetric(int? reach)
        {
            double inches = Convert.ToDouble(reach);
            double res = inches * 2.54;
            double roundedRes = Math.Round(res, 0);
            //int result = (int)Math.Ceiling(resDouble);
            return roundedRes;
        }

        /// <summary>
        /// Converts the figher's weight to its respective weightclass.
        /// </summary>
        /// <param name="weight">This is the fighters weight in lbs</param>
        /// <returns>Returns the name of the weightclass the fighter belongs in.</returns>
        public static string CalculateWeightclass(int? weight)
        {
            Dictionary<string, int> weightClasses = new Dictionary<string, int>()
            {
                {"Strawweight", 115},
                {"Flyweight", 125},
                {"Bantamweight",135},
                {"Featherweight",145},
                {"Lightweight",155},
                {"Welterweight",170},
                {"Middleweight",185},
                {"Light Heavyweight",205},
            };

            foreach (KeyValuePair<string, int> wh in weightClasses)
            {
                if (weight <= wh.Value)
                {
                    return wh.Key;
                }
            }

            return "Heavyweight";

        }

        /// <summary>
        /// Calculates the age given a time of birth.
        /// </summary>
        /// <param name="Dob">This is the fighters date of birth.</param>
        /// <returns>Returs an int representing the fighters age.</returns>
        public static string CalculateAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            return "" + Years;

        }
    }
}
