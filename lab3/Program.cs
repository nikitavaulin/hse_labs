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

        static void Main(string[] args)
        {
            double a = Math.PI / 5;     // левая граница
            double b = 9 * a;           // правая граница
            double step = (b - a) / 10; // шаг
            double e = 0.0001;          // заданная точность 
            int pointNumber = 1;        // номер точки

            Console.WriteLine($"a = {a:F4}\n");
            Console.WriteLine($"b = {b:F4}\n");
            Console.WriteLine($"step = {step:F4}\n");

            for (double x = a; x <= b; x += step)   // перебор параметра Х
            {
                if (pointNumber > 10)
                    break;

                // Y - значение функции
                double y = -1 * Math.Log(Math.Abs(2 * Math.Sin(x / 2)));

                // SN - сумма для заданного N 
                double sn = 0;
                for (int n = 1; n <= 400; n++) // в задании n = 40, но количество итераций должно быть как можно больше, чтобы число было ближе к Y
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

                Console.WriteLine($"{pointNumber}) x = {x:F4} | sn = {sn:F4} | se = {se:F4} | y = {y:F4}\n");
                pointNumber++;
            }

            Console.ReadLine();
        }
    }
}