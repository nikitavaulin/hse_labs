using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class lab1_v2
    {
        static void Main255(string[] args)
        {
            /* Метод считывания введенных пользователем целых чисел 
             -Параметры: varName - имя переменной
             -Возвращает значение введенной переменной с типом int */
            int IntVarParsing(string varName)
            {
                bool didParse = false;
                string buffer = "";
                int inputNum;
                do
                {
                    if (!didParse)
                    {
                        Console.WriteLine($"Введите число {varName}");
                        buffer = Console.ReadLine();
                    }
                    didParse = int.TryParse(buffer, out inputNum);
                    if (didParse == false)
                    {
                        Console.WriteLine("Ошибка преобразования строки в число. Повторите ввод");
                    }
                } while (!didParse);
                Console.WriteLine($"Считано число {varName} = {inputNum}");
                return inputNum;
            }

            /* Метод считывания введенных пользователем чисел 
             -Параметры: varName - имя переменной, isValidCheckNeed - требуется ли проверка ОДЗ
             -Возвращает значение введенной переменной с типом double */
            double VarParsing(string varName, bool isValidCheckNeed)
            {
                bool didParse = false, isValid = false;
                string buffer = "";
                double inputNum;

                //  нужна ли проверка ОДЗ
                if (isValidCheckNeed == false)      
                {
                    do
                    {
                        if (!didParse)
                        {
                            Console.WriteLine($"Введите число {varName}");
                            buffer = Console.ReadLine();
                        }

                        didParse = double.TryParse(buffer, out inputNum);

                        if (didParse == false)
                            Console.WriteLine("Ошибка преобразования строки в число. Повторите ввод");

                    } while (!didParse);

                    Console.WriteLine($"Считано число {varName} = {inputNum}");
                    return inputNum;
                }
                else
                {
                    do
                    {
                        if (!didParse || !isValid)
                        {
                            Console.WriteLine($"Введите число {varName}");
                            buffer = Console.ReadLine();
                        }
                        didParse = double.TryParse(buffer, out inputNum);
                        isValid = didParse && (Math.Pow(inputNum, 3) - inputNum) != 0;

                        if (didParse == false)
                            Console.WriteLine("Ошибка преобразования строки в число. Повторите ввод");
                       
                        if (isValid == false && didParse)
                            Console.WriteLine($"{varName} не удовлетворяет условию ОДЗ");

                    } while (!didParse || isValid == false);
                    Console.WriteLine($"Считано число {varName} = {inputNum}");
                    return inputNum;
                }
            }

            // Task 1 -------------------------------------------------------------------------
            Console.WriteLine("Task 1 ---------------------------------");

            int n, m;
            n = IntVarParsing("n");
            Console.WriteLine();
            m = IntVarParsing("m");

            Console.WriteLine();

            // 1) ++n * ++m
            int result1 = ++n * ++m;
            Console.WriteLine($"1) ++n * ++m = {result1} \nСейчас n={n}, m={m}");

            Console.WriteLine();

            // 2) m++ < n
            bool result2 = m++ < n;
            Console.WriteLine($"2) m++ < n = {result2} \nСейчас n={n}, m={m}");

            Console.WriteLine();

            // 3) n++ > m
            bool result3 = n++ > m;
            Console.WriteLine($"3) n++ > m = {result3} \nСейчас n={n}, m={m}");

            Console.WriteLine();

            // 4) x + 1/(x^3-x) - 2
            double result4, x;
            x = VarParsing("x", true);

            Console.WriteLine();

            result4 = x + 1 / (Math.Pow(x,3) - x) - 2;
            Console.WriteLine($"4) x + 1/(x^3-x) - 2 = {result4}");

            Console.WriteLine();

            
            // Task 2 -------------------------------------------------------------------------
            Console.WriteLine("Task 2 ---------------------------------");

            double X, Y;
            X = VarParsing("X", false);
            Console.WriteLine();
            Y = VarParsing("Y", false);

            Console.WriteLine();

            bool inRange = (-7 <= X) && (X <= 0) && (-2 <= Y) && (Y <= 0);
            if (inRange) 
                Console.WriteLine($"Ваша точка ({X};{Y}) принадлежит заштрихованной области");
            else 
                Console.WriteLine($"Ваша точка ({X};{Y}) не принадлежит заштрихованной области");

            Console.WriteLine();


            // Task 3 -------------------------------------------------------------------------
            Console.WriteLine("Task 3 ---------------------------------\n");
            // ((a - b)^2 - (a^2 + 2ab)) / (b^2), а=1000, b=0.0001 //
            Console.WriteLine("При а=1000, b=0.0001: выражение ((a - b)^2 - (a^2 + 2ab)) / (b^2) имеет");

            double a = 1000, b = 0.0001, result5;
            double diminutive = Math.Pow(a - b, 2);
            double eductible = Math.Pow(a, 2) + 2 * a * b;
            double denominator = Math.Pow(b, 2);

            result5 = (diminutive - eductible) / denominator;
            Console.WriteLine($"1) результат через тип double = {result5}");
            Console.WriteLine($"Уменьшаемое = {diminutive}, вычитаемое = {eductible}, знаменатель = {denominator}\n");

            float af = 1000, bf = 0.0001f, result5f;
            float diminutiveF = (float)(Math.Pow(af - bf, 2));
            float eductibleF = (float)(Math.Pow(af, 2) + 2 * af * bf);
            float denominatorF = (float)(Math.Pow(bf, 2));
                
            result5f = ((diminutiveF - eductibleF) / denominatorF);
            Console.WriteLine($"2) результат через тип flout = {result5f}");
            Console.WriteLine($"Уменьшаемое = {diminutiveF}, вычитаемое = {eductibleF}, знаменатель = {denominatorF}");



            // Преркащение работы программы
            Console.WriteLine("\nНажмите любую кнопку, чтобы продолжить");
            Console.ReadKey();
        }
    }
}