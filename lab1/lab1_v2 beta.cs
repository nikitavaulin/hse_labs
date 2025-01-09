using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class lab1_v2
    {
        static void Main3(string[] args)
        {

            // ввод чисел пользователем
            double VarParsing(string varName)
            {
                bool didParse = false;
                string buffer = "";
                double input;
                do
                {
                    if (!didParse)
                    {
                        Console.WriteLine($"Введите число {varName}");
                        buffer = Console.ReadLine();
                    }
                    didParse = double.TryParse(buffer, out input);
                    if (didParse == false)
                    {
                        Console.WriteLine("Ошибка преобразования строки");
                    }
                } while (!didParse);
                Console.WriteLine($"Считано число {varName} = {input}");
                return input;
            }

            double n, m;
            n = VarParsing("n");
            m = VarParsing("m");
            /*
            double n, m;
            string buffer = "";
            bool didParse = false;

            do
            {
                if (!didParse)
                {
                    Console.WriteLine("Введите число n");
                    buffer = Console.ReadLine();
                }
                didParse = double.TryParse(buffer, out n);
                if (didParse == false)
                {
                    Console.WriteLine("Ошибка преобразования строки");
                }
            } while (!didParse);
            Console.WriteLine($"Считано число n = {n}");

            didParse = false;

            do
            {
                if (!didParse)
                {
                    Console.WriteLine("Введите число m");
                    buffer = Console.ReadLine();
                }
                didParse = double.TryParse(buffer, out m);
                if (didParse == false)
                {
                    Console.WriteLine("Ошибка преобразования строки");
                }
            } while (!didParse);
            Console.WriteLine($"Считано число m = {m}");
            */

            Console.WriteLine();

            // Task 1
            Console.WriteLine("Task 1");

            // 1) ++n * ++m
            double result1 = ++n * ++m;
            Console.WriteLine($"1) ++n * ++m = {result1}");
            Console.WriteLine($"Сейчас n={n}, m={m}");

            Console.WriteLine();

            // 2) m++ < n
            bool result2 = m++ < n;
            Console.WriteLine($"2) m++ < n = {result2}");
            Console.WriteLine($"Сейчас n={n}, m={m}");

            Console.WriteLine();

            // 3) n++ > m
            bool result3 = n++ > m;
            Console.WriteLine($"3) n++ > m = {result3}");
            Console.WriteLine($"Сейчас n={n}, m={m}");

            Console.WriteLine();

            // 4) x + 1/(x^3-x) - 2
            double result4, x;
            // bool didParse = false;
            bool isValid = false;
            /*
            do
            {
                if (!didParse || !isValid)
                {
                    Console.WriteLine("Введите число x");
                    buffer = Console.ReadLine();
                }
                didParse = double.TryParse(buffer, out x);
                isValid = didParse && (Math.Pow(x, 3) - x) != 0;
;               if (didParse == false)
                {
                    Console.WriteLine("Ошибка преобразования строки");
                }
                
                if (isValid == false && didParse)
                {
                    Console.WriteLine("Х не удовлетворяет условию ОДЗ");
                }

            } while (!didParse || isValid == false);
            Console.WriteLine($"Считано число x = {x}");

            Console.WriteLine();

            result4 = x + 1 / (Math.Pow(x,3) - x) - 2;
            Console.WriteLine($"4) x + 1/(x^3-x) - 2 = {result4}");

            Console.WriteLine();


            // Task 2 -------------------------------------------------------------------------
            Console.WriteLine("Task 2");

            didParse = false;
            double X, Y;
            do
            {
                if (!didParse)
                {
                    Console.WriteLine("Введите число X");
                    buffer = Console.ReadLine();
                }
                didParse = double.TryParse(buffer, out X);
                if (didParse == false)
                {
                    Console.WriteLine("Ошибка преобразования строки");
                }
            } while (!didParse);
            Console.WriteLine($"Считано число X = {X}");

            didParse = false;

            do
            {
                if (!didParse)
                {
                    Console.WriteLine("Введите число Y");
                    buffer = Console.ReadLine();
                }
                didParse = double.TryParse(buffer, out Y);
                if (didParse == false)
                {
                    Console.WriteLine("Ошибка преобразования строки");
                }
            } while (!didParse);
            Console.WriteLine($"Считано число Y = {Y}");

            Console.WriteLine();

            bool inRange = (-7 <= X) && (X <= 0) && (-2 <= Y) && (Y <= 0);
            if (inRange) 
                Console.WriteLine($"Ваша точка ({X};{Y}) входит в заштрихованную область");
            else 
                Console.WriteLine($"Ваша точка ({X};{Y}) не входит в заштрихованную область");

            Console.WriteLine();

            // Task 3
            Console.WriteLine("Task 3\n");
            // ((a - b)^2 - (a^2 + 2ab)) / (b^2), а=1000, b=0.0001 //
            Console.WriteLine("При а=1000, b=0.0001: выражение ((a - b)^2 - (a^2 + 2ab)) / (b^2) имеет");

            double a = 1000;
            double b = 0.0001, result5;
            result5 = (Math.Pow(a - b, 2) - (Math.Pow(a, 2) + 2 * a * b)) / (Math.Pow(b, 2));
            Console.WriteLine($"1) результат через double = {result5}");

            float af = 1000;
            float bf = 0.0001f, result5f;
            result5f = ((float)Math.Pow(af - bf, 2) - (float)(Math.Pow(af, 2) + 2 * af * bf)) / (float)(Math.Pow(bf, 2));
            Console.WriteLine($"2) результат через flout = {result5f}");
            */



            Console.WriteLine("\nНажмите любую кнопку, чтобы продолжить");
            Console.ReadKey();
        }
    }
}
