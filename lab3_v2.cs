using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    internal class Program2
    {

        static void Main(string[] args)
        {
            double a = Math.PI / 5;     // левая граница
            double b = 9 * a;           // левая граница
            double step = (b - a) / 10; // шаг
            double e = 0.0001;          // заданная точность 
            int pointNumber = 1;        // номер точки

            Console.WriteLine($"a = {Math.Round(a, 4)}\n");
            Console.WriteLine($"b = {Math.Round(b, 4)}\n");
            Console.WriteLine($"step = {Math.Round(step, 4)}\n");

            for (double x = a; x <= b; x += step)   // перебор параметра Х
            {
                if (pointNumber > 10)
                    break;

                // Y - значение функции
                double y = -1 * Math.Log(Math.Abs(2 * Math.Sin(x / 2)));

                // SN - сумма для заданного N 
                double sn = 0;
                for (int n = 1; n <= 40; n++)
                    sn += Math.Cos(n * x) / n;

                // SE - сумма для заданной точности
                double se = 0, n2 = 1;
                double An = Math.Cos(n2 * x) / n2;  // первое слагаемое
                while (Math.Abs(An) > e)
                {
                    se += An;
                    n2++;
                    An = Math.Cos(n2 * x) / n2;
                }

                Console.WriteLine($"{pointNumber}) x = {Math.Round(x, 4)} | sn = {Math.Round(sn, 4)} | se = {Math.Round(se, 4)} | y = {Math.Round(y, 4)}\n");
                pointNumber++;
            }
            
            Console.ReadLine();
        }
    }
}
