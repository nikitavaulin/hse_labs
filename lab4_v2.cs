using System;
using System.Collections.Generic;
using System.Configuration;
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
            bool isParse = false;
            int outputValue;

            do
            {
                Console.WriteLine($"Введите, пожалуйста, значение {varName}\n");
                buffer = Console.ReadLine();

                isParse = int.TryParse(buffer, out outputValue);

                if (!isParse)
                {
                    Console.WriteLine($"Вы ввели {varName} неверно. Повторите, пожалуйста, ввод.\n");
                }
            } while (!isParse);

            Console.WriteLine($"Вы ввели {varName} = {outputValue}");
            return outputValue;
        }*/

        static bool InBound(int value, int downBound = -2147483647, int upBound = 2147483647)
        {
            return downBound <= value && value <= upBound;
        }


        static int ParsingVar(string varName, string outputText)
        {
            string buffer = "";
            bool isParse = false;
            int outputValue = 0;
            
            do
            {
                try
                {
                    Console.WriteLine(outputText);
                    outputValue = Convert.ToInt32(Console.ReadLine());
                    isParse = true;
                    
                    switch (varName)
                    {
                        case "arrayLength":
                            isParse = InBound(outputValue, 1, 2147483647);
                            if (!isParse)
                            {
                                Console.WriteLine($"Длина массива должна быть больше 0. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case "answer":
                            isParse = InBound(outputValue, 0, 10);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите целое число от 0 до 10. Повторите, пожалуйста, ввод.\n"); // FIXME
                            }
                            break;

                        case "mode":
                            isParse = InBound(outputValue, 1, 2);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите целое число от 0 до 10. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        default:
                            isParse = InBound(outputValue);
                            if (!isParse)
                            {
                                Console.WriteLine($"Длина массива должна быть больше 0. Повторите, пожалуйста, ввод.\n");
                            }
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Вы ввели не целое число. Повторите, пожалуйста, ввод.\n");
                    isParse = false;
                }

                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите, пожалуйста, ввод");
                    isParse = false;
                }
            }while (!isParse);

            //Console.WriteLine($"Вы ввели {varName} = {outputValue}");
            return outputValue;
        }

        // формирование массива случайных чисел
        static int[] GetRandomArray(int[] array)
        {   
            Random rand = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-2147483647, 2147483647);
            }
            return array;
        }

        // формирование массива введёнными числами
        static int[] GetArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ParsingVar($"Элемент массива №{i + 1}", $"Введите {i + 1} элемент массива");
            }
            return array;
        }

        // вывод массива
        static void OutputArray(int[] array) 
        {
            Console.Write($"Ваш массив: ");
            foreach (int elem in array)
            {
                Console.Write($"{elem} ");
            }   
            Console.Write("\n");
        }


        static void Main(string[] args)
        {
            // инициализация переменных
            int answer = -1; // выбор пользователя
            bool isArrayCreated = false; // создан ли массив
            bool isRunProgram = true; // работает ли программа
            int[] array = new int[1]; // создание массива

            // основной алгоритм
            do
            {
                Console.WriteLine("\nПожалуйста, выберите действите:");
                Console.WriteLine("1. Сформировать массив");
                Console.WriteLine("2. Вывести массив на печать");
                Console.WriteLine("3. Удалить из массива минимальный элемент (задание 1)\n");
                Console.WriteLine("0. Завершить работу программы\n");

                // выбор пользователем действия
                answer = ParsingVar("answer", "Введите номер действия\n");

                switch (answer)
                {
                    case 1: // сформировать массив
                        Console.WriteLine("Пожалуйста, выберите режим формирования массива \n");
                        Console.WriteLine("1. Сформировать элементы массива случайно");
                        Console.WriteLine("2. Сформировать элементы массива вручную\n");

                        int mode = ParsingVar("mode", "Введите, пожалуйста, номер режима");
                        int arrayLength = ParsingVar("arrayLength", "\nВведите, пожалуйста, длину массива");

                        Array.Resize(ref array, arrayLength);   // FIXME (спросить у преподавателя)

                        switch (mode)
                        {
                            case 1: // массив случайных чисел
                                array = GetRandomArray(array);
                                break;
                            case 2:
                                array = GetArray(array);
                                break;
                        }
                        OutputArray(array);
                        isArrayCreated = true;
                        break;

                    case 2: // Вывод массива
                        if (!isArrayCreated)
                        {
                            Console.WriteLine("Массив не сформирован");
                            break;
                        }
                        OutputArray(array);
                        break;

                    case 3: //  Первое задание
                        if (!isArrayCreated)
                        {
                            Console.WriteLine("Массив не сформирован");
                            break;
                        }

                        int mMin = array[0];
                        int countMin = 0;
                        
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] < mMin)
                            {
                                mMin = array[i];
                            }
                        }
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] == mMin)
                            {
                                countMin++;
                            }
                        }

                        int[] arrayTask1 = new int[array.Length - countMin];
                        int j = 0;

                        for (int i = 0; i < array.Length - 1; i++)
                        {
                            if (array[i] != mMin)
                            {
                                arrayTask1[j] = array[i];
                                j++;
                            }
                        }
                        Console.Write("Теперь ");
                        OutputArray(arrayTask1);

                        Console.WriteLine("\nС каким массивом продолжить работу?");
                        Console.WriteLine("1. Продолжить работу с первым массивом");
                        Console.WriteLine("2. Продолжить работу с новым массивом\n");

                        mode = ParsingVar("mode", "Введите, пожалуйста, номер режима");
                        if (mode == 2)
                        {
                            array = arrayTask1;
                            Console.WriteLine("Вы выбрали новый массив");
                            break;
                        }
                        Console.WriteLine("Вы выбрали старый массив");

                        break;

                    case 4: //  Второе задание
                        break;

                    case 5: //  Третье задание
                        break;

                    case 0:
                        Console.WriteLine("\nКонец работы программы");
                        isRunProgram = false;
                        break;

                }
            } while (isRunProgram);

            
            Console.ReadLine();
        }
    }
}
