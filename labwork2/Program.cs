using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class lab2
    {
        /// <summary>
        /// Метод считывания введенных пользователем целых чисел 
        /// </summary>
        /// <param name = "varName" > имя переменной</param>
        /// <returns>значение введенной переменной с типом int</returns>
        static int ParsingIntVar(string varName)
        {
            {
                bool didParse = false;
                string buffer = "";
                int inputNum;
                string[] banVars = { "n", "k1", "k2" }; // переменные с ограничениями
                bool isValidNum = CheckNums(0, "test");
                ;
                if (!banVars.Contains(varName))
                {
                    do
                    {
                        if (!didParse)
                        {
                            Console.WriteLine($"Введите число {varName}");
                            buffer = Console.ReadLine();
                        }

                        didParse = int.TryParse(buffer, out inputNum);

                        if (!didParse)
                            Console.WriteLine("Ошибка преобразования строки в число. Повторите ввод");

                    } while (!didParse);
                    Console.WriteLine($"Считано число {varName} = {inputNum} \n");
                    return inputNum;
                }
                else
                {
                    do
                    {
                        if (!didParse || !isValidNum)
                        {
                            Console.WriteLine($"Введите число {varName}");
                            buffer = Console.ReadLine();
                        }

                        didParse = int.TryParse(buffer, out inputNum);
                        isValidNum = CheckNums(inputNum, varName);

                        if (!didParse)
                            Console.WriteLine("Ошибка преобразования строки в число. Повторите ввод");

                        if (!isValidNum && didParse)
                        {
                            if (varName == "n")
                                Console.WriteLine("Количество чисел последовательности не может быть меньше или равно 0");
                            else
                                Console.WriteLine("k1 и k2 не могут быть 0. Повторите ввод");
                        }


                    } while (!didParse || !isValidNum);
                    Console.WriteLine($"Считано число {varName} = {inputNum} \n");
                    return inputNum;
                }
            }
        }

        /// <summary>
        /// Метод проверки чисел для разных переменных разных задач
        /// </summary>
        /// <param name="inputNumber - вводимое ччисло"></param>
        /// <param name="varName - имя переменной"></param>
        /// <returns>bool значение</returns>
        static bool CheckNums(int inputNumber, string varName)
        {
            switch (varName)
            {
                case "n":
                    return inputNumber > 0;
                case "k1":
                    return inputNumber != 0;
                case "k2":
                    return inputNumber != 0;
                default:
                    return false;
            }
        }

        static void Main1(string[] args)
        {
            // Task 1 -----------------------------------------------------------------
            Console.WriteLine("Task 1 \n----------------------------");
            Console.WriteLine("Введите, пожалуйста, длину вашей последовательности\n");
            int n = ParsingIntVar("n");
            int sumEvenNums = 0;

            for (int i = 0; i < n; i++)
            {
                int number = ParsingIntVar($"#{i + 1}");
                if (number % 2 == 0)
                    sumEvenNums += number;
            }
            Console.WriteLine($"Сумма чётных чисел послед-ти = {sumEvenNums}\n");


            // Task 2 -----------------------------------------------------------------
            Console.WriteLine("\nTask 2 \n----------------------------\n");
            int countAllNums = 1, element = 1, countNums = 0;
            int k1, k2;
            do
            {
                k1 = ParsingIntVar("k1");
                k2 = ParsingIntVar("k2");
                if (k1 == k2)
                    Console.WriteLine("k1 и k2 не могут быть равны, иначе результат всегда будет равен 0. Пожалуйста, повторите ввод \n");
            } while (k1 == k2);

            while (element != 0)
            {
                element = ParsingIntVar($"#{countAllNums}");
                if (element == 0)
                    break;
                if ((element % k1 == 0) && (element % k2 != 0))
                    countNums++;
                countAllNums++;
            }

            Console.WriteLine($"Количество чисел кратных {k1} и не кратных {k2} равно = {countNums}\n");


            // Task 3 -----------------------------------------------------------------
            Console.WriteLine("\nTask 3 \n----------------------------");

            Console.WriteLine("Введите, пожалуйста, длину послед-ти нечётных чисел\n");
            int nTask3 = ParsingIntVar("n");
            int oddNumber = 1;
            int sumOddNums = 0;

            Console.WriteLine($"Последовательность нечётных чисел длиной {nTask3}:");
            for (int i = 0; i < nTask3; i++)
            {
                Console.Write($"{oddNumber} ");
                sumOddNums += oddNumber;
                oddNumber += 2;
            }

            Console.WriteLine($"\nСумма первых {nTask3} элементов последовательности нечетных чисел = {sumOddNums}");

            Console.WriteLine("\nНажмити любую кнопку, чтобы продолжить...");
            Console.ReadKey();
        }
    }
}