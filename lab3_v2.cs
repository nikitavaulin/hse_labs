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
        static double ParsingVar(string varName)
        {
            string buffer = "";
            double inputNum = 12;

            return inputNum;
        }
        static void Main(string[] args)
        {
            //int n = 40;
            double a = Math.Round(Math.PI/5, 4);
            double b = 9*a;
            double step = (b-a) / 10;
            double e = 0.0001;
            int count = 1;

            Console.WriteLine($"a={a}");
            Console.WriteLine($"b={b}");
            Console.WriteLine($"step={step}");
            //for (double x = a; x <= b; x += step)
            //{
            //    Console.WriteLine(x);
            //}

            for (double x = a; x <= b; x += step)
            {
                // Y
                double y = -Math.Log(Math.Abs(2 * Math.Sin(x / 2)));

                //SN
                double sn = 0;
                for (int n = 1; n <= 40; n++)
                    sn += Math.Cos(n * x) / n;
                // SE
                double se = 0;
                double An = 1;
                int n2 = 1;
                while (Math.Abs(An) > e)
                {
                    An = Math.Cos(n2 * x) / n2;
                    se += An;
                    n2++;
                }


                Console.WriteLine($"{count}) x={Math.Round(x, 4)} sn={Math.Round(sn, 4)} se={Math.Round(se, 4)} y={Math.Round(y, 4)}\n");
                count++;
            }

            Console.ReadKey();
        }
    }
}
