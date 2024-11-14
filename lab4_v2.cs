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
                                Console.WriteLine($"Введите целое число от 1 до 2. Повторите, пожалуйста, ввод.\n");
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
            } while (!isParse);

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
                array[i] = ParsingVar($"elem", $"Введите, пожалуйста, #{i + 1} элемент массива");
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

        // выбор с каким массивом дальше работать
        static int[] ChooseArray(int[] array, int[] arrayNew)
        {
            Console.WriteLine("\nС каким массивом продолжить работу?");
            Console.WriteLine("1. Продолжить работу с предыдущим массивом");
            Console.WriteLine("2. Продолжить работу с текущим массивом\n");

            int mode = ParsingVar("mode", "Введите, пожалуйста, номер режима");
            switch (mode)
            {
                case 1:
                    Console.WriteLine("Вы выбрали предыдущий массив");
                    OutputArray(array);
                    break;
                case 2:
                    array = arrayNew;
                    Console.WriteLine("Вы выбрали новый массив");
                    OutputArray(array);
                    break;
            }
            return array;
        }

        static int[] AddArrayRandom(int[] array, int addCount)
        {
            int[] newArray = new int[array.Length + addCount];
            Random rand = new Random();

            for (int i = 0; i < newArray.Length; i++)
            {
                if (i < array.Length)
                {
                    newArray[i] = array[i];
                }
                else
                {
                    newArray[i] = rand.Next();
                }
            }
            return newArray;
        }

        static int[] AddArray(int[] array, int addCount)
        {
            int[] newArray = new int[array.Length + addCount];

            for (int i = 0; i < newArray.Length; i++)
            {
                if (i < array.Length)
                {
                    newArray[i] = array[i];
                }
                else
                {
                    newArray[i] = ParsingVar("elem", $"Введите, пожалуйста, #{i + 1} элемент массива");
                }
            }
            return newArray;
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
                Console.WriteLine("3. Удалить из массива минимальный элемент (задание 1)");
                Console.WriteLine("4. Добавить в массив новые элементы (задание 2)\n");
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

                    case 3: //  Первое задание (удаление элементов из массива)
                        if (!isArrayCreated)
                        {
                            Console.WriteLine("Массив не сформирован");
                            break;
                        }

                        int mMin = array[0];
                        int countMin = 0;

                        // поиск минимума
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] < mMin)
                            {
                                mMin = array[i];
                            }
                        }
                        // подсчёт количества минимальных элементов
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] == mMin)
                            {
                                countMin++;
                            }
                        }

                        // формирование нового массива без удаленных элементов
                        int[] arrayTask1 = new int[array.Length - countMin];
                        int j = 0;

                        for (int i = 0; i < array.Length; i++)
                        {
                            if (array[i] != mMin)
                            {
                                arrayTask1[j] = array[i];
                                j++;
                            }
                        }
                        Console.Write("Теперь ");
                        OutputArray(arrayTask1); // вывод массива

                        array = ChooseArray(array, arrayTask1); // пользователь выбирает с каким массивом продолжить работу

                        break;

                    case 4: //  Второе задание (добавление элементов в массив)
                        if (!isArrayCreated)
                        {
                            Console.WriteLine("Массив не сформирован");
                            break;
                        }

                        int addCount = ParsingVar("arrayLength", "Сколько элементов вы хотите добавить в массив? Введите, пожалуйста, целое число.");
                        int[] arrayTask2 = new int[array.Length + addCount];

                        Console.WriteLine("Как вы хотите добавить новые элементы массива? \n");
                        Console.WriteLine("1. Задать новые элементы массива случайно");
                        Console.WriteLine("2. Задать новые элементы массива вручную\n");

                        mode = ParsingVar("mode", "Введите, пожалуйста, номер режима");

                        switch (mode)
                        {
                            case 1: // случайные числа
                                arrayTask2 = AddArrayRandom(array, addCount);
                                break;
                            case 2:
                                arrayTask2 = AddArray(array, addCount);
                                break;
                        }
                        Console.Write("Теперь ");
                        OutputArray(array);
                        isArrayCreated = true;

                        // пользователь выбирает с каким массивом продолжить работу
                        array = ChooseArray(array, arrayTask2); 

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
