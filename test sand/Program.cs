using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_sand
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Date date = new Date();
            date.day = 17;
            date.month = 5;
            date.year = 2006;

            Date date1 = new Date(2025, 1, 9);

            Date date2 = new Date();

            Date date3 = new Date(date1);

            date.Show();
            date1.Show();
            date2.Show();
            date3.Show();


            Console.ReadLine();
        }
    }
}
