using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.Helpers
{
    public static class MmaiDateConverter
    {
        /// <summary>
        /// Converts a given date with shortened months to datetime.
        /// </summary>
        /// <param name="date">This is the given date which uses spaces as separators and shortened months.</param>
        /// <returns>Returns a datetime object representing the given date.</returns>
        public static DateTime ConvertDateStringToDateTime(String date)
        {
            String[] dateParts = date.Split(' ');

            if (dateParts.Length < 1)
            {
                return Convert.ToDateTime("0000/01/01");
            }

            Dictionary<string, string> months = new Dictionary<string, string>();
            months.Add("Jan", "01");
            months.Add("Feb", "02");
            months.Add("Mar", "03");
            months.Add("Apr", "04");
            months.Add("May", "05");
            months.Add("Jun", "06");
            months.Add("Jul", "07");
            months.Add("Aug", "08");
            months.Add("Sep", "09");
            months.Add("Oct", "10");
            months.Add("Nov", "11");
            months.Add("Dec", "12");

            return Convert.ToDateTime(dateParts[2] + "/" + months[dateParts[0]] + "/" + dateParts[1]);

        }


        /// <summary>
        /// Converts a given date to datetime.
        /// </summary>
        /// <param name="date">This is the given date which uses spaces as separators.</param>
        /// <returns>Returns a datetime object representing the given date.</returns>
        public static DateTime ConvertDateFullNameStringToDateTime(string date)
        {
            String[] dateParts = date.Split(' ');

            if (dateParts.Length < 1)
            {
                return Convert.ToDateTime("0000/01/01");
            }

            Dictionary<string, string> months = new Dictionary<string, string>();
            months.Add("January", "01");
            months.Add("February", "02");
            months.Add("March", "03");
            months.Add("April", "04");
            months.Add("May", "05");
            months.Add("June", "06");
            months.Add("July", "07");
            months.Add("August", "08");
            months.Add("September", "09");
            months.Add("October", "10");
            months.Add("November", "11");
            months.Add("December", "12");

            return Convert.ToDateTime(dateParts[2] + "/" + months[dateParts[0]] + "/" + dateParts[1]);

        }

        /// <summary>
        /// Converts the UFC match date to correct format 
        /// </summary>
        /// <param name="date">This is the UFC match date.</param>
        /// <returns>Returns a string representing the date in the following format: (YYYY/MM/DD)</returns>
        public static string ConvertMatchDateFormat(String date)
        {
            // Juli 24, 2014
            String[] parts = date.Split(' ', ',');

            Dictionary<string, string> months = new Dictionary<string, string>();
            months.Add("January", "01");
            months.Add("February", "02");
            months.Add("March", "03");
            months.Add("April", "04");
            months.Add("May", "05");
            months.Add("June", "06");
            months.Add("July", "07");
            months.Add("August", "08");
            months.Add("September", "09");
            months.Add("October", "10");
            months.Add("November", "11");
            months.Add("December", "12");


            return parts[3] + "/" + months[parts[0]] + "/" + parts[1];

        }
    }
}
