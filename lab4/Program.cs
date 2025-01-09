using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace lab4
{
    internal class lab4
    {
        // проверка принадлежит ли области допустимых значений
        static bool InBound(int value, int downBound = -2147483647, int upBound = 2147483647)
        {
            return downBound <= value && value <= upBound;
        }

        /// <summary>
        /// Метод считывания переменных, введённых пользователем
        /// </summary>
        /// <param name="varClass">Класс переменной, для определения её допустимых значений</param>
        /// <param name="outputText">Текст для вывода в консоль</param>
        /// <returns>Значение переменной с типом Int32</returns>
        static int ParsingIntVar(string varClass, string outputText)
        {
            bool isParse = false;
            int outputValue = 0;

            do
            {
                try
                {
                    Console.WriteLine(outputText);
                    outputValue = Convert.ToInt32(Console.ReadLine());
                    isParse = true;

                    switch (varClass)
                    {
                        case "arrayLength":
                            isParse = InBound(outputValue, 1);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите число больше 0. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case "answer":
                            isParse = InBound(outputValue, 0, 8);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите целое число от 0 до 8. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case "mode":
                            isParse = InBound(outputValue, 1, 2);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите целое число от 1 до 2. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case "stepShiftRight":
                            isParse = InBound(outputValue, 0);
                            if (!isParse)
                            {
                                Console.WriteLine($"Введите целое положительное число в пределах Int32. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case "elem":
                            isParse = InBound(outputValue);
                            if (!isParse)
                            {
                                Console.WriteLine($"Элемент массива должен быть числом в пределах Int32. Повторите, пожалуйста, ввод.\n");
                            }
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Вы ввели не целое число. Повторите, пожалуйста, ввод.\n");
                    isParse = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Вы ввели число не в пределах Int32. Повторите, пожалуйста, ввод.\n");
                    isParse = false;
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка, повторите, пожалуйста, ввод");
                    isParse = false;
                }
            } while (!isParse);

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
                array[i] = ParsingIntVar($"elem", $"Введите, пожалуйста, #{i + 1} элемент массива");
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

        // выбор с каким массивом продолжать работать
        static int[] ChooseArray(int[] array, int[] arrayNew)
        {
            Console.WriteLine("\nС каким массивом продолжить работу?");
            Console.WriteLine("1. Продолжить работу с предыдущим массивом");
            Console.WriteLine("2. Продолжить работу с текущим массивом\n");

            int mode = ParsingIntVar("mode", "Введите, пожалуйста, номер режима");
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

        // добавление случайных элементов в массив
        static int[] AddElemArrayRandom(int[] array, int addCount)
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

        // добавление заданных элементов в массив
        static int[] AddElemArray(int[] array, int addCount)
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
                    newArray[i] = ParsingIntVar("elem", $"Введите, пожалуйста, #{i + 1} элемент массива");
                }
            }
            return newArray;
        }

        // Копирование одного массива в другой
        static int[] CopyArray(int[] array, int[] arrayNew)
        {
            for (int i = 0; i < arrayNew.Length; i++)
            {
                arrayNew[i] = array[i];
            }
            return arrayNew;
        }

        // бинарный поиск элемента в массиве
        static int SearchBinary(int[] array, int element)
        {
            int low = 0;
            int high = array.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (array[mid] == element)
                {
                    return mid + 1;
                }
                if (array[mid] < element)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return -1;
        }

        // сортировка выбором массива
        static int[] SortSelection(int[] array)
        {
            // сортировка выбором
            for (int position = 0; position < array.Length - 1; position++)
            {
                for (int i = position + 1; i < array.Length; i++)
                {
                    if (array[i] < array[position])
                    {
                        (array[position], array[i]) = (array[i], array[position]);
                    }
                }
            }
            return array;
        }

        // сдвиг массива вправо
        static int[] ShiftArrayRight(int[] array, int stepShift)
        {
            for (int m = 0; m < stepShift; m++)
            {
                int temp = array[array.Length - 1]; // сохраняется последний элемент массива

                for (int i = array.Length - 2; i >= 0; i--) // перебор с конца
                {
                    array[i + 1] = array[i]; // текущий элемент переходит на следующую позицию
                }

                array[0] = temp; // на первую позицию встает последний элемент
            }
            return array;
        }

        // проверка пустой ли массив
        static bool isArrayExist(int[] array, bool isArrCreated)
        {
            if (!isArrCreated)
            {
                Console.WriteLine("Массив не сформирован");
                return false;
            }
            if (!(array.Length > 0))
            {
                Console.WriteLine("Массив пустой");
                return false;
            }
            return true;
        }

        // вычисление кол-ва сравнений (для бинарного поиска)
        static double CalcCountCompares(int lengthArray)
        {
            return Math.Ceiling(Math.Log(lengthArray, 2)); ;
        }

        static int[] DeleteElemArray(int[] array)
        {
            int mMin = array[0]; // 
            int countMin = 0;

            // поиск минимума
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < mMin) // если текущий элемент меньше минимального
                {
                    mMin = array[i]; // минимальным становится текущий элемент
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
            int[] arrayNew = new int[array.Length - countMin];
            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != mMin)
                {
                    arrayNew[j] = array[i];
                    j++;
                }
            }
            return arrayNew;
        }

        static void SearchFirstNegative(int[] array)
        {
            int countCompare = 0; // счётчик количества сравнений
            bool isNegativeFound = false;

            for (int i = 0; i < array.Length; i++)
            {
                countCompare++;
                if (array[i] < 0)
                {
                    Console.WriteLine($"Первый отрицательный элемент в массиве = {array[i]}, его порядковый номер {i + 1}");
                    isNegativeFound = true;
                    break;
                }
            }
            if (!isNegativeFound)
            {
                Console.WriteLine("В массиве нет отрицательных элементов");
            }

            Console.WriteLine($"Количество сравнений, необходимых для поиска = {countCompare}\n");
        }

        static void Main133(string[] args)
        {
            #region инициализация переменных

            int answer = -1; // выбор пользователя
            bool isArrayCreated = false; // создан ли массив
            bool isRunProgram = true; // работает ли программа
            bool isArraySorted = false; // отсортирован ли массив
            int[] array = null; // создание массива в памяти

            #endregion

            // основной алгоритм
            do
            {
                // МЕНЮ
                #region menu
                Console.WriteLine("\nПожалуйста, выберите действите:");
                Console.WriteLine("1. Сформировать массив");
                Console.WriteLine("2. Вывести массив на печать");
                Console.WriteLine("3. Удалить из массива минимальный элемент (задание 1)");
                Console.WriteLine("4. Добавить в массив новые элементы (задание 2)");
                Console.WriteLine("5. Сдвинуть циклически массив вправо (задание 3)");
                Console.WriteLine("6. Найти первый отрицательный элемент в массиве (задание 4)");
                Console.WriteLine("7. Отсортировать массив по возрастанию (задание 5)");
                Console.WriteLine("8. Найти элемент в массиве (задание 6)");
                Console.WriteLine("0. Завершить работу программы\n");

                // выбор пользователем действия
                answer = ParsingIntVar("answer", "Введите номер действия\n");
                #endregion
                switch (answer)
                {
                    case 1: // сформировать массив
                        Console.WriteLine("Пожалуйста, выберите режим формирования массива \n");
                        Console.WriteLine("1. Сформировать элементы массива случайно");
                        Console.WriteLine("2. Сформировать элементы массива вручную\n");

                        int mode = ParsingIntVar("mode", "Введите, пожалуйста, номер режима");
                        int arrayLength = ParsingIntVar("arrayLength", "\nВведите, пожалуйста, длину массива");
                        array = new int[arrayLength]; // расширение памяти для массива

                        switch (mode)
                        {
                            case 1: // массив случайных чисел
                                array = GetRandomArray(array);
                                break;
                            case 2: // массив заданных чисел
                                array = GetArray(array);
                                break;
                        }
                        OutputArray(array);     // вывод массива
                        isArrayCreated = true; // массив создан
                        continue;

                    case 2: // Вывод массива
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        OutputArray(array);
                        break;

                    case 3: //  Первое задание (удаление элементов из массива)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        // удаление минимального элемента массива
                        int[] arrayTask1 = DeleteElemArray(array);

                        if (arrayTask1.Length > 0)
                        {
                            Console.Write("Теперь ");
                            OutputArray(arrayTask1); // вывод массива
                        }
                        else
                        {
                            Console.Write("\nТеперь ваш массив пустой\n");
                        }
                        // пользователь выбирает с каким массивом продолжить работу
                        array = ChooseArray(array, arrayTask1);
                        break;

                    case 4: //  Второе задание (добавление элементов в массив)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        int addCount = ParsingIntVar("arrayLength", "Сколько элементов вы хотите добавить в массив? Введите, пожалуйста, целое число.");
                        int[] arrayTask2 = new int[array.Length + addCount];

                        Console.WriteLine("Как вы хотите добавить новые элементы массива? \n");
                        Console.WriteLine("1. Задать новые элементы массива случайно");
                        Console.WriteLine("2. Задать новые элементы массива вручную\n");

                        mode = ParsingIntVar("mode", "Введите, пожалуйста, номер режима");

                        switch (mode)
                        {
                            case 1: // случайные числа
                                arrayTask2 = AddElemArrayRandom(array, addCount);
                                break;
                            case 2:
                                arrayTask2 = AddElemArray(array, addCount);
                                break;
                        }
                        Console.Write("Теперь ");
                        OutputArray(arrayTask2);
                        isArrayCreated = true;

                        // пользователь выбирает с каким массивом продолжить работу
                        array = ChooseArray(array, arrayTask2);

                        break;

                    case 5: //  Третье задание (циклический сдвиг массива вправо на М элементов)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        // копирование массива в массив для 3 задания
                        int[] arrayTask3 = new int[array.Length];
                        arrayTask3 = CopyArray(array, arrayTask3);

                        // считывание числа М (насколько сдвинуть массив)
                        int stepShift = ParsingIntVar("stepShiftRight", "Насколько элементов нужно сдвинуть вправо? Введите целое положительное число.\n");

                        // циклический сдвиг вправо
                        arrayTask3 = ShiftArrayRight(arrayTask3, stepShift);

                        Console.WriteLine($"Ваш массив сдвинут на {stepShift} элементов");

                        Console.Write("Теперь ");
                        OutputArray(arrayTask3);

                        array = ChooseArray(array, arrayTask3);
                        break;

                    case 6: // Четвертое задание (поиск первого отрицательного элемента)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }
                        // поиск первого отрицательного элемента
                        SearchFirstNegative(array);
                        break;

                    case 7: // Пятое задание (сортировка выбором)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        // копирование массива
                        int[] arrayTask4 = new int[array.Length];
                        arrayTask4 = CopyArray(array, arrayTask4);

                        // сортировка выбором
                        arrayTask4 = SortSelection(arrayTask4);
                        isArraySorted = true;

                        Console.Write("Теперь ");
                        OutputArray(arrayTask4); // вывод массива

                        // выбор пользователем с каким массивом продолжить работу
                        array = ChooseArray(array, arrayTask4);
                        break;

                    case 8: // Шестое задание (бинарный поиск элемента)
                        // проверка существования и заполненности массива
                        if (!isArrayExist(array, isArrayCreated))
                        {
                            break;
                        }

                        // вычисление количества сравнений
                        double countCompares = CalcCountCompares(array.Length);

                        // сортировка массива
                        int[] arraySorted = new int[array.Length];
                        arraySorted = CopyArray(array, arraySorted);

                        if (!isArraySorted) // если массив не отсортирован
                        {
                            arraySorted = SortSelection(arraySorted);
                            Console.WriteLine("Ваш массив был отсортирован");
                            OutputArray(arraySorted);
                        }

                        // ввод элемента, который нужно найти
                        int element = ParsingIntVar("elem", "Введите целое число, которое хотите найти в массиве");

                        // бинарный поиск
                        int elemPosition = SearchBinary(arraySorted, element);

                        if (elemPosition != -1) // если элемент найден
                        {
                            Console.Write($"\nЭлемент {element} есть в массиве, его порядковый номер в отсортированом массиве = {elemPosition}");
                        }
                        else
                        {
                            Console.WriteLine($"В массиве нет элемента {element}");
                        }
                        Console.WriteLine($"\nКоличество сравнений = {countCompares}");

                        // выбор пользователем с каким массивом продолжить работу
                        array = ChooseArray(array, arraySorted);
                        break;

                    case 0: // конец работы программы
                        Console.WriteLine("\nКонец работы программы");
                        isRunProgram = false;
                        break;
                }

                #region финиш программы
                Console.WriteLine("\n1.Вернуться к меню\n2.Завершить программу\n");
                int answ = ParsingIntVar("mode", "Введите, пожалуйста, номер действия");
                switch (answ)
                {
                    case 1:
                        break;
                    case 2:
                        Console.WriteLine("\nКонец работы программы");
                        isRunProgram = false;
                        break;
                }
                #endregion

            } while (isRunProgram);

            Console.ReadLine();
        }
    }
}