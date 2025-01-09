using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace lab5
{
    internal class lab5
    {
        enum VarClass   // перечисление классов переменных
        {
            element,            // элементы массивов 
            length,             // длина измерений
            answerStartMenu,    // выбор в стартовом меню
            answerObjectMenu,   // выбор в меню объекта
            strNumber,          // номер выбранной строки
            columnPosition,     // позиция добавляемого столбца
            mode                // выбор режима ввода
        }
        enum MsgClass   // перечисление классов сообщений меню
        {
            StartMenu,     // стартовое меню
            MatrixMenu,    // меню двумерного массива
            RagArrMenu,    // меню рваного массива
            StringMenu     // меню строк
        }

        /// <summary>
        /// Метод провеки находится ли число в заданных границах
        /// </summary>
        /// <param name="number">проверяемое число</param>
        /// <param name="downBound">нижняя граница</param>
        /// <param name="upBound">верхняя граница</param>
        /// <returns>Истина / Ложь</returns>
        static bool IsInBound(int number, int downBound = -2147483648, int upBound = 2147483647) => downBound <= number && number <= upBound;

        /// <summary>
        /// Метод считывания введённой переменной типа Int32
        /// </summary>
        /// <param name="varClass">Класс переменной</param>
        /// <param name="message">Сообщение для вывода</param>
        /// <returns>Int32 значение</returns>
        static int ParsingIntVars(VarClass varClass, string message)
        {
            bool isParsed = true;       // флаг верно ли считалось значение
            int inputNum = 0;           // введенное значение

            do
            {
                try
                {
                    Console.WriteLine(message);
                    inputNum = Convert.ToInt32(Console.ReadLine());    // считывание значения

                    // проверки удовлетворяет ли значение переменной условиям своего класса
                    switch (varClass)
                    {
                        case VarClass.element:
                            isParsed = IsInBound(inputNum);
                            if (!isParsed)
                            {
                                Console.WriteLine("Вы ввели число не в пределах Int32. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.length:
                            isParsed = IsInBound(inputNum, 1, 50);
                            if (!isParsed)
                            {
                                Console.WriteLine("Длина измерения может быть в диапазонее от 1 до 50. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.answerStartMenu:
                            isParsed = IsInBound(inputNum, 0, 3);
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 0 до 3. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.answerObjectMenu:
                            isParsed = IsInBound(inputNum, 1, 3);
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 1 до 3. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.strNumber:
                            isParsed = IsInBound(inputNum, 1, GetTestArrayString().Length);
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 1 до 3. Повторите, пожалуйста, ввод.\n");  // зависит от размера массива тестовых строк
                            }
                            break;

                        case VarClass.mode:
                            isParsed = IsInBound(inputNum, 1, 2);
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 1 до 2. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.columnPosition:
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Вы ввели не число, пожалуйста. Повторите ввод.\n");
                    isParsed = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"Вы ввели число не в пределах Int32. Повторите, пожалуйста, ввод.\n");
                    isParsed = false;
                }
                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка. Повторите, пожалуйста, ввод");
                    isParsed = false;
                }

            } while (!isParsed);    // пока не будет введено верно

            return inputNum;
        }

        #region Методы Print (вывод в консоль)
        /// <summary>
        /// Метод вывода в консоль разных меню
        /// </summary>
        /// <param name="msgClass"> Классы сообщений меню</param>
        /// <param name="message">Сообщение для вывода</param>
        static void PrintMenu(MsgClass msgClass, string message)
        {
            switch (msgClass)
            {
                case (MsgClass.StartMenu):
                    Console.WriteLine(message);
                    Console.WriteLine("1. Обработка двумерного массива");
                    Console.WriteLine("2. Обработка рваного массив");
                    Console.WriteLine("3. Обработка строки");
                    Console.WriteLine("0. Завершить работу программы");
                    break;

                case (MsgClass.MatrixMenu):
                    Console.WriteLine(message);
                    Console.WriteLine("1. Создать двумерный массив");
                    Console.WriteLine("2. Напечатать двумерный массив");
                    Console.WriteLine("3. Добавить новый столбец в двумерный массив");
                    break;

                case (MsgClass.RagArrMenu):
                    Console.WriteLine(message);
                    Console.WriteLine("1. Создать рваный массив");
                    Console.WriteLine("2. Напечатать рваный массив");
                    Console.WriteLine("3. Удалить минимальную строку из рваного массива");
                    break;

                case (MsgClass.StringMenu):
                    Console.WriteLine(message);
                    Console.WriteLine("1. Ввести строку");
                    Console.WriteLine("2. Напечатать строку");
                    Console.WriteLine("3. Перевернуть каждое чётное слово в строке");
                    break;
            }
        }

        /// <summary>
        /// Метод вывода в консоль массива тестовых строк
        /// </summary>
        /// <param name="strArray">Массив тестовых строк</param>
        static void PrintArrString(string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i++)       // перебор по индексу каждого элемента массива
            {
                Console.WriteLine($"{i + 1}) {strArray[i]}");       // вывод в консоль строки с нумерацией
            }
        }
        static void PrintMsgChoiceMode(string message)       // вывод в консоль выбор режима ввода массивов
        {
            Console.WriteLine(message);
            Console.WriteLine("1. Случайный ввод");
            Console.WriteLine("2. Ручной ввод");
        }
        static void PrintMsgChoiceCreateStr(string message)  // вывод в консоль выбор режима ввода строк
        {
            Console.WriteLine(message);
            Console.WriteLine("1. Выбрать строку из массива готовых строк");
            Console.WriteLine("2. Ввести строку вручную");
        }
        #endregion

        #region Проверка на пустоту
        /// <summary>
        /// Метод проверки пустой ли объект (3 перегрузки)
        /// </summary>
        /// <param name="obj">Двумерный массив / Рваный массив / Строка / Массив символов</param>
        /// <returns>Истина / Ложь</returns>
        static bool isObjEmpty(int[,] obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(int[][] obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(string obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(char[] obj) => obj == null || obj.Length == 0;
        #endregion

        #region Двумерный массив
        /// <summary>
        /// Создание двумерного массива с помощью ДСЧ
        /// </summary>
        /// <param name="rows">строки</param>
        /// <param name="columns">столбцы</param>
        /// <returns>двумерный массив</returns>
        static int[,] CreateRandomMatrix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];     // выделение памяти под матрицу
            Random rand = new Random();
            for (int row = 0; row < matrix.GetLength(0); row++)      // перебор индексов строк
            {
                for (int column = 0; column < columns; column++)     // перебор индексов столбцов
                {
                    matrix[row, column] = rand.Next(-100, 100);     // присваивание рандомного числа
                }
            }
            return matrix;
        }

        /// <summary>
        /// Создание двумерного массива
        /// </summary>
        /// <param name="rows">строки</param>
        /// <param name="columns">столбцы</param>
        /// <returns>двумерный массив</returns>
        static int[,] CreateMatrix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int i = 0; i < columns; i++)
                {
                    matrix[row, i] = ParsingIntVars(VarClass.element, $"Введите элемент [{row},{i}] двумерного массива"); // присваивание введеного числа в элемент массива
                }
            }
            return matrix;
        }

        static void PrintMatrix(int[,] matrix)      // вывод матрицы в консоль
        {
            for (int row = 0; row < matrix.GetLength(0); row++)      // перебор строк
            {
                for (int column = 0; column < matrix.GetLength(1); column++)      // перебор столбцов
                {
                    Console.Write($"{matrix[row, column],-5}");       // вывод элемента матрицы
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод расшрирения матрицы, путем добавления нового столбца
        /// </summary>
        /// <param name="matrix">исходный двумерный массив</param>
        /// <param name="position">позиция нового столбца</param>
        /// <returns>расширенный двумерный массив</returns>
        static int[,] ExpandMatrix(int[,] matrix, int position)
        {
            // выделение памяти под новую матрицу с количеством столбцов + 1
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int row = 0; row < newMatrix.GetLength(0); row++)
            {
                int column = 0;
                // копирование столбцов матрицы до нового столбца
                for (int columnNewMatr = 0; columnNewMatr < position; columnNewMatr++)
                {
                    newMatrix[row, columnNewMatr] = matrix[row, column];
                    column++;
                }
                // копирование столбцов матрицы после нового столбца
                for (int columnNewMatr = position + 1; columnNewMatr < newMatrix.GetLength(1); columnNewMatr++)
                {
                    newMatrix[row, columnNewMatr] = matrix[row, column];
                    column++;
                }
            }
            return newMatrix;
        }

        /// <summary>
        /// Метод заполнения нового столбца матрицы
        /// </summary>
        /// <param name="matrix">исходная матрица</param>
        /// <param name="position">позиция нового столбца</param>
        /// <returns>новая матрица</returns>
        static int[,] FillColumnMatrix(int[,] matrix, int position)
        {
            Random rand = new Random();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                matrix[row, position] = rand.Next(-100, 100);
            }
            return matrix;
        }

        /// <summary>
        /// Метод добавления в матрицу нового столбца
        /// </summary>
        /// <param name="matrix">двумерный массив</param>
        /// <param name="position">позиция нового столбца</param>
        /// <returns>новая матрица</returns>
        static int[,] AddColumnMatrix(int[,] matrix, int position)
        {
            int[,] newMatrix = FillColumnMatrix(ExpandMatrix(matrix, position), position);
            return newMatrix;
        }

        /// <summary>
        /// Метод считывания позиции нового столбца
        /// </summary>
        /// <param name="matrixColumnCount">текущее количество столбцов матрицы</param>
        /// <returns>позиция нового столбца</returns>
        static int GetColumnPos(int matrixColumnCount)
        {
            bool isValid = false;
            int position = 0;
            do
            {
                position = ParsingIntVars(VarClass.columnPosition, $"\nВведите номер столбца в границах двумерного массива + 1 (от 1 до {matrixColumnCount + 1})");
                isValid = IsInBound(position, 1, matrixColumnCount + 1);

                if (!isValid)
                {
                    Console.WriteLine("Номер столбца не может быть за пределами массива.");
                }
            } while (!isValid);
            return position;
        }

        #endregion

        #region Рваный массив
        static int[][] CreateRandRagArray(int countString)
        {
            Random rand = new Random();
            int[][] ragArr = new int[countString][];
            for (int str = 0; str < countString; str++)
            {
                int countElement = rand.Next(1, 10);    // кол-во  элементов в строке
                ragArr[str] = new int[countElement];    // выделение памяти под строку массива

                for (int elem = 0; elem < countElement; elem++)
                {
                    ragArr[str][elem] = rand.Next(-100, 100);
                }
            }
            return ragArr;
        }

        static int[][] CreateRagArray(int countString)
        {
            int[][] ragArr = new int[countString][];
            for (int str = 0; str < countString; str++)
            {
                // кол-во  элементов в строке
                int countElement = ParsingIntVars(VarClass.length, $"Введите количество элементов в строке {str + 1}");
                // выделение памяти под строку массива
                ragArr[str] = new int[countElement];

                for (int elem = 0; elem < countElement; elem++)
                {
                    ragArr[str][elem] = ParsingIntVars(VarClass.element, $"Введите элемент {elem + 1} строки {str + 1}");
                }
            }
            return ragArr;
        }

        static void PrintRagArray(int[][] ragArr)
        {
            if (isObjEmpty(ragArr))
            {
                Console.WriteLine("\nВаш рваный массив пустой");
            }
            else
            {
                for (int str = 0; str < ragArr.GetLength(0); str++)
                {
                    for (int elem = 0; elem < ragArr[str].Length; elem++)
                    {
                        Console.Write($"{ragArr[str][elem],-5}");
                    }
                    Console.WriteLine();
                }
            }
        }

        #region Задание для рваного массива
        static int[][] DeleteMinStrRagArr(int[][] ragArr, int countMinStr, int minLen)
        {
            int[][] newRagArr = new int[ragArr.GetLength(0) - countMinStr][];
            int line = 0;
            for (int str = 0; str < ragArr.GetLength(0); str++)
            {
                if (ragArr[str].Length != minLen)
                {
                    newRagArr[line] = new int[ragArr[str].Length];
                    for (int elem = 0; elem < ragArr[str].Length; elem++)
                    {
                        newRagArr[line][elem] = ragArr[str][elem];
                    }
                    line++;
                }
            }

            return newRagArr;
        }

        static int FindMinLenStrRagArr(int[][] ragArr)
        {
            int minLen = ragArr[0].Length;
            for (int str = 1; str < ragArr.GetLength(0); str++)
            {
                if (ragArr[str].Length < minLen)
                {
                    minLen = ragArr[str].Length;
                }
            }
            return minLen;
        }

        static int CountMinStrRagArr(int[][] ragArr, int minLen)
        {
            int countMinStr = 0;
            for (int str = 0; str < ragArr.GetLength(0); str++)
            {
                if (ragArr[str].Length == minLen)
                {
                    countMinStr++;
                }
            }
            return countMinStr;
        }
        #endregion

        #endregion

        #region Строки
        static void PrintString(string str, string msg = "\nВаша строка:")
        {
            if (isObjEmpty(str))
            {
                Console.WriteLine("\nВаша строка пустая или была введена некорректно");
            }
            else
            {
                Console.WriteLine(msg);
                Console.WriteLine(str);
            }
        }

        static string ReverseEvenWordStr(string str)
        {
            string[] arrayWord = str.Split(' ');


            for (int wordInd = 1; wordInd < arrayWord.Length; wordInd += 2)
            {
                char[] wordChars = arrayWord[wordInd].ToCharArray();

                if (isObjEmpty(wordChars))
                {
                    return null;
                }

                wordChars = ReverseWord(wordChars);

                string reversedWord = new string(wordChars);
                arrayWord[wordInd] = reversedWord;
            }

            string reversedString = String.Join(" ", arrayWord);

            return reversedString;
        }

        static char[] ReverseWord(char[] arrChars)
        {
            Array.Reverse(arrChars);
            char lastChar = arrChars[0];

            if (isSign(lastChar))
            {
                arrChars = ShiftArrayLeft(arrChars);
                return arrChars;
            }
            return arrChars;
        }

        static bool isSign(char checkChar, string signs = ".,!;:?") => signs.Contains(checkChar);

        static bool isSignFirst(char checkChar, string signs = " .,!;:?") => signs.Contains(checkChar);

        static bool isSpaceClose(string str) => str.Contains("  ");

        static bool isSignClose(string str, string signs = ".,!;:?")
        {
            foreach (char char1 in signs)
            {
                foreach (char char2 in signs)
                {
                    if (str.Contains(String.Concat(char1, char2)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool isStrContainBanSigns(string str)
        {
            string alphabet = " QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnmЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁйцукенгшщзхъфывапролджэячсмитьбюё.,!;:?";
            for (int i = 0; i < str.Length; i++)
            {
                if (!alphabet.Contains(str[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static bool isStrContainsLetter(string str)
        {
            string alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnmЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁйцукенгшщзхъфывапролджэячсмитьбюё";
            for (int i = 0; i < str.Length; i++)
            {
                if (alphabet.Contains(str[i]))
                {
                    return true;
                }
            }
            return false;
        }

        static char[] ShiftArrayLeft(char[] array)
        {
            char firstChar = array[0];
            for (int i = 0; i < array.Length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[array.Length - 1] = firstChar;

            return array;
        }

        static bool isWordContainSign(string str, string signs = ".,!;:?")
        {
            string[] words = str.Split(' ');
            char[] wordArr = null;

            foreach (string word in words)
            {
                wordArr = word.ToCharArray();
                foreach (char sign in signs)
                {
                    if (wordArr.Contains(sign))
                    {
                        if (Array.IndexOf(wordArr, sign) != wordArr.Length - 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        static bool isStringValid(string str)
        {
            bool isStrValid = true;

            if (!isStrContainBanSigns(str))
            {
                isStrValid = false;
                Console.WriteLine("\nВ строке есть неподходящие символы. Повторите, пожалуйста, ввод.");
            }
            if (isSpaceClose(str))
            {
                isStrValid = false;
                Console.WriteLine("\nВ строке не могут стоять несколько пробелов подряд. Повторите, пожалуйста, ввод.");
            }
            if (isSignClose(str))
            {
                isStrValid = false;
                Console.WriteLine("\nВ строке не могут стоять несколько знаков препинания подряд. Повторите, пожалуйста, ввод.");
            }
            if (!isStrContainsLetter(str))
            {
                isStrValid = false;
                Console.WriteLine("\nВ вашей строке нет слов. Повторите, пожалуйста, ввод.");
            }
            if (isSignFirst(str[0]))
            {
                isStrValid = false;
                Console.WriteLine("\nСтрока не может начинаться со знака препинания или пробела. Повторите, пожалуйста, ввод.");
            }
            if (isWordContainSign(str))
            {
                isStrValid = false;
                Console.WriteLine("\nВ словах не могут содержаться знаки препинания. Повторите, пожалуйста, ввод.");
            }

            return isStrValid;
        }

        static string ParsingString(string msg)
        {
            string str = null;
            bool isStrValid = true;
            do
            {
                try
                {
                    Console.WriteLine(msg);
                    str = Console.ReadLine();

                    isStrValid = isStringValid(str);

                }
                catch
                {
                    Console.WriteLine("\nНепредвиденная ошибка. Повторите, пожалуйста, ввод.");
                    isStrValid = false;
                }


            } while (!isStrValid);
            return str;
        }

        static string FixString(string str, string signs = ".,!;:?")
        {
            str = str.Trim();

            foreach (char sign in signs)
            {
                str = str.Replace($" {sign}", sign.ToString());
            }

            return str;
        }
        #endregion

        static string[] GetTestArrayString()
        {
            string[] testArrayString = {
                                    "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                                    "Была лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                                    "В траве сидел кузнечик! Кузнечик: не трогал козявок и дружил с мухом.",
                                    "В этой строке запятая внут,ри слова.",
                                    "В этой строке два пробела  подряд.",
                                    "В этой строке три пробела   подряд.",
                                    "В этой строке знак препинания стоит ! отдельно.",
                                    "В этой строке есть неподходящий симв0л.",
                                    "В этой строке есть (-_-) неподходящие символы.",
                                    " Эта строка начинается с пробела.",
                                    ", Эта строка начинается с запятой."
                                };
            return testArrayString;
        }

        static void Main(string[] args)
        {
            #region Инициализация переменных
            bool isFinishProgram = false;
            int[,] matrix = null;
            int[][] ragArray = null;
            string str = null;
            int mode = 0;
            #endregion

            // Main body
            do
            {
                PrintMenu(MsgClass.StartMenu, "\nМеню выбора действий:");
                int answerStart = ParsingIntVars(VarClass.answerStartMenu, "\nВведите номер действия:");

                switch (answerStart)
                {
                    // Обработка двумерных массивов
                    case 1:
                        PrintMenu(MsgClass.MatrixMenu, "\nОбработка двумерных массивов");
                        int answMatrMenu = ParsingIntVars(VarClass.answerObjectMenu, "\nВведите номер действия:");
                        switch (answMatrMenu)
                        {
                            case 1:
                                PrintMsgChoiceMode("\nКак вы хотите заполнить массив?");
                                mode = ParsingIntVars(VarClass.mode, "\nВведите номер режима ввода:");

                                int rows = ParsingIntVars(VarClass.length, "\nВведите количество строк");
                                int columns = ParsingIntVars(VarClass.length, "\nВведите количество столбцов");

                                switch (mode)
                                {
                                    case 1:
                                        matrix = CreateRandomMatrix(rows, columns);
                                        break;
                                    case 2:
                                        matrix = CreateMatrix(rows, columns);
                                        break;
                                }

                                Console.WriteLine("\nВаш двумерный массив:");
                                PrintMatrix(matrix);
                                break;

                            case 2:
                                if (isObjEmpty(matrix))
                                {
                                    Console.WriteLine("\nМассив пустой");
                                    break;
                                }

                                Console.WriteLine("\nВаш двумерный массив:");
                                PrintMatrix(matrix);
                                break;

                            case 3:
                                if (isObjEmpty(matrix))
                                {
                                    Console.WriteLine("\nМассив пустой");
                                    break;
                                }

                                int position = GetColumnPos(matrix.GetLength(1)) - 1;
                                matrix = AddColumnMatrix(matrix, position);

                                Console.WriteLine("\nВаш новый двумерный массив:");
                                PrintMatrix(matrix);
                                break;
                        }

                        break;

                    // Обработка рваных массивов
                    case 2:
                        PrintMenu(MsgClass.RagArrMenu, "\nОбработка рваных массивов");
                        int answRagArrMenu = ParsingIntVars(VarClass.answerObjectMenu, "\nВведите номер действия:");

                        switch (answRagArrMenu)
                        {
                            case 1:
                                PrintMsgChoiceMode("\nКак вы хотите сформировать рваный массив?");
                                mode = ParsingIntVars(VarClass.mode, "\nВведите номер режима ввода:");

                                int countString = ParsingIntVars(VarClass.length, "\nВведите количество строк");

                                switch (mode)
                                {
                                    case 1:
                                        ragArray = CreateRandRagArray(countString);
                                        break;
                                    case 2:
                                        ragArray = CreateRagArray(countString);
                                        break;
                                }

                                Console.WriteLine("\nВаш рваный массив:");
                                PrintRagArray(ragArray);

                                break;

                            case 2:
                                if (isObjEmpty(ragArray))
                                {
                                    Console.WriteLine("\nРваный массив пустой");
                                    break;
                                }

                                Console.WriteLine("\nВаш рваный массив:");
                                PrintRagArray(ragArray);

                                break;

                            case 3:
                                if (isObjEmpty(ragArray))
                                {
                                    Console.WriteLine("\nРваный массив пустой");
                                    break;
                                }

                                int minLen = FindMinLenStrRagArr(ragArray);
                                int countMinStr = CountMinStrRagArr(ragArray, minLen);

                                ragArray = DeleteMinStrRagArr(ragArray, countMinStr, minLen);

                                Console.WriteLine("\nУдалены все строки минимальной длины.");
                                Console.WriteLine("Ваш новый рваный массив:");
                                PrintRagArray(ragArray);

                                break;
                        }

                        break;

                    // Обработка строк
                    case 3:
                        PrintMenu(MsgClass.StringMenu, "\nОбработка строк");
                        int answStrMenu = ParsingIntVars(VarClass.answerObjectMenu, "\nВведите номер действия:");

                        switch (answStrMenu)
                        {
                            case 1:
                                string[] testArrayString = GetTestArrayString();
                                int strNumber = 0;

                                PrintMsgChoiceCreateStr("\nКак вы хотите cформировать строку?");
                                mode = ParsingIntVars(VarClass.mode, "\nВведите номер режима ввода:");

                                switch (mode)
                                {
                                    case 1: // выбор строки из массива
                                        bool isStrChosen = false;
                                        do
                                        {
                                            Console.WriteLine("\nВыберите одну из строк");
                                            PrintArrString(testArrayString);
                                            strNumber = ParsingIntVars(VarClass.strNumber, "\nВведите номер строки:");
                                            str = testArrayString[strNumber - 1];

                                            isStrChosen = isStringValid(str);
                                        } while (!isStrChosen);

                                        break;

                                    case 2: // ввод строки пользователем
                                        str = ParsingString("\nВведите строку, содержащую только латиницу и кириллицу (в двух регистрах), пробелы и знаки .,!;:?");
                                        break;
                                }
                                str = FixString(str); // корректирование строки
                                PrintString(str);
                                break;

                            case 2:
                                if (isObjEmpty(str))
                                {
                                    Console.WriteLine("\nСтрока пустая");
                                    break;
                                }

                                PrintString(str);
                                break;

                            case 3:
                                if (isObjEmpty(str))
                                {
                                    Console.WriteLine("\nСтрока пустая");
                                    break;
                                }

                                str = ReverseEvenWordStr(str);

                                PrintString(str, "\nВаша новая строка:");
                                break;
                        }
                        break;

                    // Завершение работы программы
                    case 0:
                        Console.WriteLine("\nКонец работы программы. До свидания!");
                        isFinishProgram = true;
                        break;
                }

            } while (!isFinishProgram);

            Console.ReadLine();
        }
    }
}