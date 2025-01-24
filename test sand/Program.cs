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
            Runner r1 = new Runner();
            Runner r2 = new Runner("Стасик");
            Runner r3 = new Runner("Коля", 51.5);

            Console.ReadLine();
        }
    }
}
