using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    internal class lab5
    {
        enum VarClass
        {
            element,
            length,
            answer,
            columnPosition
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
                                Console.WriteLine("Длина измерения должна быть > 1. Повторите, пожалуйста, ввод.\n");
                            }
                            break;

                        case VarClass.answer:
                            isParsed = IsInBound(inputNum, 0, 10); // FIXME
                            if (!isParsed)
                            {
                                Console.WriteLine("Введите число от 0 до 10. Повторите, пожалуйста, ввод.\n");  // FIXME
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
            Console.WriteLine("3. Добавить новый столбец");
            Console.WriteLine("4. Создать рваный массив");
            Console.WriteLine("5. Напечатать рваный массив");
            //Console.WriteLine("");
            //Console.WriteLine("");
            //Console.WriteLine("");
            Console.WriteLine("0. Завершить работу программы");
        }

        static int[,] CreateMatrix(int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            for (int row = 0; row < matrix.GetLength(0); row++) 
            {
                for (int i = 0; i < columns; i++)
                {
                    matrix[row, i] = GetRandIntNum();
                }
            }
            return matrix;
        }

        static int GetRandIntNum (int low = -100, int high = 100)
        {
            Random rand = new Random();
            return rand.Next(low, high);
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    Console.Write($"{matrix[row, column], -5}");
                }
                Console.WriteLine();
            }
        }

        static int[,] ExpandMatrix(int[,] matrix, int position)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) + 1];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                Array.Copy(matrix, newMatrix, position);
                Array.Copy(matrix, position, newMatrix, position + 1, matrix.GetLength(1) - position);
            }
            return newMatrix;
        }

        static int[,] FillColumnMatrix(int[,] matrix, int position)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                matrix[row, position] = GetRandIntNum();
            }
            return matrix;
        }

        static int[,] AddColumnMatrix(int[,] matrix, int position)
        {
            int[,] newMatrix = FillColumnMatrix(ExpandMatrix(matrix, position), position);
            return newMatrix;
        }

        static bool isObjEmpty(int[,] matrix) => matrix == null || matrix.Length == 0;

        static int GetColumnPos(int matrixColumnCount)
        {
            bool isValid = false;
            int position = 0;
            do
            {
                position = ParsingIntVars(VarClass.columnPosition, $"Введите номер столбца в гранмцах двумерного массива (от 1 до {matrixColumnCount})");
                isValid = IsInBound(position, 1, matrixColumnCount);

                if (!isValid)
                {
                    Console.WriteLine("Номер столбца не может быть за пределами массива.");
                }
            } while (!isValid);
            return position;
        }

        static void Main(string[] args)
        {
            #region Инициализация переменных
            bool isFinishProgram = false;
            int[,] matrix = null;
            #endregion

            do
            {
                PrintMenu();
                int answer = ParsingIntVars(VarClass.answer, "\nВведите номер действия:");

                switch (answer)
                {
                    case 1: // формирование двумерного массива
                        int rows = ParsingIntVars(VarClass.length, "\nВведите количество строк");
                        int columns = ParsingIntVars(VarClass.length, "\nВведите количество столбцов");

                        matrix = CreateMatrix(rows, columns);

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

                        Console.WriteLine("\nВведите номер столбца, который вы хотите добавить");
                        int position = GetColumnPos(matrix.GetLength(1)) - 1;

                        int[,] expandMatrix = AddColumnMatrix(matrix, position);

                        Console.WriteLine("\nВаш новый двумерный массив:");
                        PrintMatrix(expandMatrix);
                        break;

                    case 4:
                        break;

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
