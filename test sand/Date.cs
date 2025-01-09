using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace test_sand
{
    internal struct Date
    {
        public int year;
        public int month;
        public int day;

        public Date(int y, int m, int d)
        {
            year = y;
            month = m;
            day = d;
        }
        public Date(Date date)
        {
            this.year = date.year;
            this.month = date.month;
            this.day = date.day;
        }


        public void Show()
        {
            string date = "";
            if (day < 10)
            {
                date += $"0{day}:";
            }
            else
            {
                date += $"{day}:";
            }
            if (month < 10)
            {
                date += $"0{month}:";
            }
            else
            {
                date += $"{month}:";
            }
            date += $"{year}";

            Console.WriteLine(date);
        }

    }
}
