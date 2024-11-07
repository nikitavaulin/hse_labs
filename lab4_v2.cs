using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab1
{
    internal class lab4
    {
        /*static int ParsingVar2(string varName)
        {
            string buffer = "";
            bool didParse = false;
            int outputValue;

            do
            {
                Console.WriteLine($"Введите, пожалуйста, значение {varName}\n");
                buffer = Console.ReadLine();

                didParse = int.TryParse(buffer, out outputValue);

                if (!didParse)
                {
                    Console.WriteLine($"Вы ввели {varName} неверно. Повторите, пожалуйста, ввод.\n");
                }
            } while (!didParse);

            Console.WriteLine($"Вы ввели {varName} = {outputValue}");
            return outputValue;
        }*/


        static int ParsingVar(string varName, string outputText)
        {
            string buffer = "";
            bool didParse = false;
            int outputValue=0;

            
            do
            {
                try
                {
                    Console.WriteLine(outputText);
                    outputValue = Convert.ToInt32(Console.ReadLine());
                    didParse = true;
                    //if ( outputValue <= 0 )
                    //{
                        
                    //}


                }
                catch (FormatException)
                {
                    Console.WriteLine($"Вы ввели не целое число. Повторите, пожалуйста, ввод.\n");
                    didParse = false;
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Вы ввели число не в пределах типа Int32");
                    didParse = false;
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите, пожалуйста, ввод");
                    didParse = false;
                }
            }while (!didParse);

            Console.WriteLine($"Вы ввели {varName} = {outputValue}");
            return outputValue;
        }

        static int[] GetRandomArray(int n)
        {   Random rand = new Random();
            int[] array = new int[n];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-2147483647, 2147483647);
            }
            return array;
        }

        static int[] GetArray(int n)
        {
            int[] array = new int[n];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ParsingVar($"Элемент массива №{i + 1}", $"Введите {i + 1} элемент массива");
            }
            return array;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Доброго времени суток! Пожалуйста, выберите режим формирования массива \n");
            Console.WriteLine("1) Чтобы сформировать элементы массива случайно, введите '1'");
            Console.WriteLine("2) Чтобы сформировать элементы массива вручную, введите '2'\n");

            int mode = ParsingVar("режим", "Введите, пожалуйста, номер режима");


            int n = ParsingVar("n", "\nВведите, пожалуйста, значение n");

            int[] array = GetArray(n);

            // вывод массива
            Console.Write($"Ваш массив: ");
            foreach (int elem in array) 
                Console.Write($"{elem} ");

            // Task 1
            //for (int i = 0; i < array.Length; i++)
            //{
            //    array[i] = rand.Next(-2147483647, 2147483647);
            //}


            Console.ReadLine();
        }
    }
}
