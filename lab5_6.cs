using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace lab1
{
    internal class lab5
    {
        enum VarClass
        {
            element,
            length,
            answer,
            columnPosition,
            mode
        }

        static bool IsInBound(int number, int downBound = -2147483648, int upBound = 2147483647) => downBound <= number && number <= upBound;
        static int ParsingIntVars(VarClass varClass, string message)
        {
            bool isParsed = true;
            int inputNum = 0;

            do
            {
                try
                {
                    Console.WriteLine(message);
                    inputNum = Convert.ToInt32(Console.ReadLine());

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

                        case VarClass.answer:
                            isParsed = IsInBound(inputNum, 0, 10); // FIXME
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 0 до 10. Повторите, пожалуйста, ввод.\n");  // FIXME
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

            } while (!isParsed);

            return inputNum;
        }

        static void PrintMenu()
        {
            Console.WriteLine("\nМеню действий:");
            Console.WriteLine("1. Создать двумерный массив");
            Console.WriteLine("2. Напечатать двумерный массив");
            Console.WriteLine("3. Добавить новый столбец в двумерный массив");
            Console.WriteLine("4. Создать рваный массив");
            Console.WriteLine("5. Напечатать рваный массив");
            Console.WriteLine("6. Удалить минимальную строку из рваного массива");
            Console.WriteLine("7. Ввести строку");
            Console.WriteLine("8. Напечатать строку");
            Console.WriteLine("9. Перевернуть каждое чётное слово в строке");
            Console.WriteLine("0. Завершить работу программы");
        }
        static void PrintChoiceString(string[] strArray)
        {
            Console.WriteLine("\nВыберите строку для работы.");
            for (int i = 0; i < strArray.Length; i++)
            {
                Console.WriteLine($"{i+1}) {strArray[i]}");
            }
        }
        static void PrintMsgChoiceMode(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("1. Случайный ввод");
            Console.WriteLine("2. Ручной ввод");
        }

        static bool isObjEmpty(int[,] obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(int[][] obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(string obj) => obj == null || obj.Length == 0;
        static bool isObjEmpty(char[] obj) => obj == null || obj.Length == 0;

        #region Двумерный массив
        static int[,] CreateRandomMatrix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            Random rand = new Random();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int i = 0; i < columns; i++)
                {
                    matrix[row, i] = rand.Next(-100, 100);
                }
            }
            return matrix;
        }

        static int[,] CreateMatrix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int i = 0; i < columns; i++)
                {
                    matrix[row, i] = ParsingIntVars(VarClass.element, $"Введите элемент [{row},{i}] двумерного массива");
                }
            }
            return matrix;
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    Console.Write($"{matrix[row, column],-5}");
                }
                Console.WriteLine();
            }
        }

        static int[,] ExpandMatrix(int[,] matrix, int position)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) + 1];

            for (int row = 0; row < newMatrix.GetLength(0); row++)
            {
                int column = 0;
                for (int columnNewMatr = 0; columnNewMatr < position; columnNewMatr++)
                {
                    newMatrix[row, columnNewMatr] = matrix[row, column];
                    column++;
                }
                for (int columnNewMatr = position + 1; columnNewMatr < newMatrix.GetLength(1); columnNewMatr++)
                {
                    newMatrix[row, columnNewMatr] = matrix[row, column];
                    column++;
                }
            }
            return newMatrix;
        }

        static int[,] FillColumnMatrix(int[,] matrix, int position)
        {
            Random rand = new Random();
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                matrix[row, position] = rand.Next(-100, 100);
            }
            return matrix;
        }

        static int[,] AddColumnMatrix(int[,] matrix, int position)
        {
            int[,] newMatrix = FillColumnMatrix(ExpandMatrix(matrix, position), position);
            return newMatrix;
        }

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
                Console.WriteLine("\nСтрока пустая");
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
                wordChars = ReverseWord(wordChars);

                if (isObjEmpty(wordChars))
                {
                    return null;
                }

                string reversedWord = new string(wordChars);
                arrayWord[wordInd] = reversedWord;
            }

            string reversedString = String.Join(" ", arrayWord);

            return reversedString;
        }

        static char[] ReverseWord(char[] arrChars)
        {
            if (isObjEmpty(arrChars))
            {
                return null;
            }

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

        static bool isSpaceClose(string str) => str.Contains("  "); // FIXME    ИСПРАВИТЬ НА ПРОВЕРКУ ВСЕХ ЗНАКОВ

        static bool isStringValid(string str)
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
                    if (!isStrValid)
                    {
                        isStrValid = false;
                        Console.WriteLine("\nВ строке есть неподходящие символы. Повторите, пожалуйста, ввод.");
                    }
                    if (isSpaceClose(str))
                    {
                        isStrValid = false;
                        Console.WriteLine("\nВ строке не могут стоять несколько пробелов подряд. Повторите, пожалуйста, ввод.");
                    }
                }
                catch
                {
                    Console.WriteLine("\nНепредвиденная ошибка. Повторите, пожалуйста, ввод.");
                    isStrValid = false;
                }


            }while(!isStrValid);
            return str;
        }

        #endregion
        static void Main(string[] args)
        {
            #region Инициализация переменных
            bool isFinishProgram = false;
            int[,] matrix = null;
            int[][] ragArray = null;
            string str = null;
            #endregion

            // Main body
            do
            {
                PrintMenu();
                int answer = ParsingIntVars(VarClass.answer, "\nВведите номер действия:");

                switch (answer)
                {
                    #region Двумерный массив
                    case 1: // формирование двумерного массива
                        PrintMsgChoiceMode("\nКак вы хотите заполнить массив?");
                        int mode = ParsingIntVars(VarClass.mode, "\nВведите номер режима ввода:");

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

                    case 2: // печать двумерного массива
                        if (isObjEmpty(matrix))
                        {
                            Console.WriteLine("Массив пустой");
                            break;
                        }

                        Console.WriteLine("\nВаш двумерный массив:");
                        PrintMatrix(matrix);
                        break;

                    case 3:
                        if (isObjEmpty(matrix))
                        {
                            Console.WriteLine("Массив пустой");
                            break;
                        }

                        int position = GetColumnPos(matrix.GetLength(1)) - 1;
                        matrix = AddColumnMatrix(matrix, position);

                        Console.WriteLine("\nВаш новый двумерный массив:");
                        PrintMatrix(matrix);
                        break;
                    #endregion

                    #region Рваный массив
                    case 4: // создание рваного массива
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

                    case 5: // печать рваного массива
                        if (isObjEmpty(ragArray))
                        {
                            Console.WriteLine("\nРваный массив пустой");
                            break;
                        }

                        Console.WriteLine("\nВаш рваный массив:");
                        PrintRagArray(ragArray);

                        break;

                    case 6: // удаление минимальных строк рваного массива
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
                    #endregion

                    #region Строки
                    case 7: // ввод строки
                        string[] testArrayString = {
                        "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                        "Была лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                        "В траве сидел кузнечик! Кузнечик: не трогал козявок и дружил с мухом."};

                        str = ParsingString("Введите строку, содержащую только латиницу и кириллицу (в двух регистрах), пробелы и знаки .,!;:? ");

                        PrintString(str);
                        break;

                    case 8: // печать строки
                        PrintString(str);
                        break;

                    case 9: // перевернуть каждое чётное слово в строке
                        if (isObjEmpty(str))
                        {
                            Console.WriteLine("\nСтрока пустая");
                            break;
                        }

                        str = ReverseEvenWordStr(str);

                        PrintString(str, "\nВаша новая строка:");
                        break;
                    #endregion

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
