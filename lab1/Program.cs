using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program
    {

        static void Main1(string[] args)
        {
            double a = Math.PI / 5, b = 9 * a, x = Math.PI / 5;
            double y, sn;
            double step = (b - a) / 10;
            
            for (int dotCount = 1; dotCount <= 10; dotCount++)
            {
                // Y
                y = -Math.Log(Math.Abs(2 * Math.Sin(x / 2)));


                // SN
                sn = 0;
                for (double n = 1; n <= 40; n++)
                {
                    sn += Math.Cos(n * x) / n;
                }

                Console.WriteLine($"x={Math.Round(x,5)} sn = {Math.Round(sn, 5)} y={Math.Round(y, 5)}");
                x += step;
            }
          



            Console.ReadLine();


        }



    }
}