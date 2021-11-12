using System;
using System.IO;

namespace Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            // Здесь все начинается юху.

            ChooseWhatYouWillDo();
        }

        /// <summary>
        /// Здесь пользователь выбирает что он будет делать. Есть 9 вариантов.
        /// </summary>
        private static void ChooseWhatYouWillDo()
        {
            Console.Write("Welcome to Matrix Calculator" +
                            "\r\nChoose one of the number below :" +
                            "\r\n1. Find trace of Matrix" +
                            "\r\n2. Transpose Matrix" +
                            "\r\n3. Sum of two Matrices" +
                            "\r\n4. Difference of two Matrices" +
                            "\r\n5. Multiplication of two Matrices" +
                            "\r\n6. Multiplicate Matrix by a number" +
                            "\r\n7. Find determinant of Matrix" +
                            "\r\n8. Solve system equation with Cramer method" +
                            "\r\n9. Solve system equation with Gauss method" +
                            "\r\n>>> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                FindTrace();
            }
            else if (input == "2")
            {
                TransposeMatrix();
            }
            else if (input == "3")
            {
                FindSum();
            }
            else if (input == "4")
            {
                FindDifference();
            }
            else if (input == "5")
            {
                FindMultiplication();
            }
            else if (input == "6")
            {
                MultiplicateByNumber();
            }
            else if (input == "7")
            {
                FindDeterminant();
            }
            else if (input == "8")
            {
                SolveSytemWithCramer();
            }
            else if (input == "9")
            {
                SolveSystemWithGauss();
            }
            else
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Здесь пользователь выбирает каким образом хочет создать матрицу.
        /// </summary>
        /// <param name="matrix">Это матрица которую пользователь создаст.</param>
        private static void ChooseTypeOfMatrix(out int[,] matrix)
        {
            Console.Write("Choose one of the number below :" +
                            "\r\n1. Create Matrix in Console" +
                            "\r\n2. Create Matrix by Random numbers" +
                            "\r\n3. Read Matrix from Text File" +
                            "\r\n>>> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                CreateMatrixInConsole(out matrix);
            }
            else if (input == "2")
            {
                CreateRandomMatrix(out matrix);
            }
            else if (input == "3")
            {
                CreateMatrixByFile(out matrix);
            }
            else
            {
                matrix = new int[1, 1];
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Тут пользователь выбирает размер матрицы.
        /// </summary>
        /// <param name="rows">Строки матрицы.</param>
        /// <param name="columns">Столбцы матрицы.</param>
        private static void ChooseSizeOfMatrix(out int rows, out int columns)
        {
            Console.Write("Enter number of rows (Must be uint and < 100): ");
            string inputRows = Console.ReadLine();
            if (!ValidateSizeOfMatrix(inputRows, out rows))
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }

            Console.Write("Enter number of columns (Must be uint and < 100): ");
            string inputColumns = Console.ReadLine();
            if (!ValidateSizeOfMatrix(inputColumns, out columns))
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы создать рандомную матрицу из чисел с -10000, до 10000.
        /// </summary>
        /// <param name="matrix">Матрица которую мы создадим рандомом.</param>
        private static void CreateRandomMatrix(out int[,] matrix)
        {
            Random rnd = new Random();
            int numFrom, numTo;
            int rows, columns;

            ChooseSizeOfMatrix(out rows, out columns);

            Console.Write("Enter number from which you will start randomizing (Must be int from -10000 to +10000)\r\n>>> ");
            string randomFrom = Console.ReadLine();
            if (!ValidateNumber(randomFrom, out numFrom))
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }

            Console.Write($"Enter number to which you will randomize (Must be int from {numFrom} to +10000)\r\n>>> ");
            string randomTo = Console.ReadLine();
            if (!ValidateNumber(randomTo, out numTo) || numFrom > numTo)
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }

            matrix = new int[rows,columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rnd.Next(numFrom, numTo + 1);
                }
            }

            PrintMatrix(matrix);
        }

        /// <summary>
        /// Метод чтобы проверить правильное значение ввел пользователь или нет (т.е. с -10000 до 10000).
        /// </summary>
        /// <param name="input">Ввод.</param>
        /// <param name="num">Число которое будет возвращена.</param>
        /// <returns>Булевское значение которое будет зависить от правильности числа для рандома.</returns>
        private static bool ValidateNumber(string input, out int num)
        {
            bool output = true;

            if (!int.TryParse(input, out num) || num < -10000 || num > 10000)
            {
                output = false;
            }

            return output;
        }

        /// <summary>
        /// Метод чтобы проверить правильный размер ввел пользователь или нет (т.е. с 0 до 100).
        /// </summary>
        /// <param name="input">Ввод.</param>
        /// <param name="num">Число которое будет возвращена.</param>
        /// <returns>Булевское значение которое будет зависить от правильности числа.</returns>
        private static bool ValidateSizeOfMatrix(string input, out int num)
        {
            bool output = true;

            if (!int.TryParse(input, out num) || num <= 0 || num > 100)
            {
                output = false;
            }

            return output;
        }

        /// <summary>
        /// Метод чтобы создать матрицу в нашей консоле.
        /// </summary>
        /// <param name="matrix">Наша матрица.</param>
        private static void CreateMatrixInConsole(out int[,] matrix)
        {
            int rows, columns;
            ChooseSizeOfMatrix(out rows, out columns);
            matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"So then, enter {i+1}-row int elements by dividing them with Space \r\n(Number of elements must be = columns)");
                string[] parts = Console.ReadLine().Split(' ');
                if (parts.Length != columns)
                {
                    Console.WriteLine("Wrong input");
                    ExitOrRestart();
                }

                for (int j = 0; j < columns; j++)
                {
                    if (!int.TryParse(parts[j], out matrix[i, j]))
                    {
                        Console.WriteLine("Wrong input");
                        ExitOrRestart();
                    }
                }
            }
        }

        /// <summary>
        /// Метод чтобы создать матрицу прочитав ее из текстового файла.
        /// </summary>
        /// <param name="matrix">Наша матрица которую мы создадим.</param>
        private static void CreateMatrixByFile(out int[,] matrix)
        {
            string path = GetPathForTextFile();
            string[] lines;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ExitOrRestart();
            }
            lines = File.ReadAllLines(path);
            string[] columns = lines[0].Split(' ');
            matrix = new int[lines.Length, columns.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                if (parts.Length != columns.Length)
                {
                    Console.WriteLine("Wrong Matrix");
                    ExitOrRestart();
                }
                for (int j = 0; j < parts.Length; j++)
                {
                    if (!int.TryParse(parts[j], out matrix[i, j]))
                    {
                        Console.WriteLine("Wrong Matrix");
                        ExitOrRestart();
                    }
                }
            }

            PrintMatrix(matrix);
        }

        /// <summary>
        /// Метод чтобы узнать путь к текстовому файлу.
        /// </summary>
        /// <returns>Возвращает путь к текстовому файлу.</returns>
        private static string GetPathForTextFile()
        {
            string inputPath = "";
            Console.Write("Choose one option : " +
                "\r\n1. Take examples which is in the main directory (Matrix)" +
                "\r\n2. Enter path by myself" +
                "\r\n>>> ");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.Write($"We have two examples " +
                    $"\r\n(in {Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)} " +
                    $"\r\nChoose one (by entering 1 or 2)\r\n>>> ");
                if (Console.ReadLine() == "2")
                {
                    inputPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Example2.txt";
                }
                else
                {
                    inputPath = $"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\Example1.txt";
                }
            }
            else if (input == "2")
            {
                Console.WriteLine("\r\nEnter path for Text File(e.g D:\\Games\\TextFileName.txt)");
                inputPath = Console.ReadLine();
                string[] parts = inputPath.Split('.');
                if (parts.Length != 2 || parts[1] != "txt")
                {
                    Console.WriteLine("Wrong input");
                    ExitOrRestart();
                }
            }
            else
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
            
            return inputPath;
        }

        /// <summary>
        /// Метод чтобы вывести на экран матрицу.
        /// </summary>
        /// <param name="matrix">Матрица которая будет выведена на экран</param>
        private static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]}\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод чтобы вывести на экран матрицу.
        /// </summary>
        /// <param name="matrix">Матрица которая будет выведена на экран</param>
        private static void PrintMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]:F2}\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод чтобы найти след матрицы.
        /// </summary>
        private static void FindTrace()
        {
            int[,] matrix;
            ChooseTypeOfMatrix(out matrix);

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                Console.WriteLine("Matrix must be square type");
                ExitOrRestart();
            }
            else
            {
                int trace = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    trace += matrix[i, i];
                }

                Console.WriteLine($"\r\nTrace of this matrix is {trace}");
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы транспонировать матрицу.
        /// </summary>
        private static void TransposeMatrix()
        {
            int[,] matrix;
            ChooseTypeOfMatrix(out matrix);
            int[,] transposedMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }
            Console.WriteLine("\r\nYour transposed matrix is :");
            PrintMatrix(transposedMatrix);
            ExitOrRestart();
        }

        /// <summary>
        /// Метод чтобы найти сумму двух матриц.
        /// </summary>
        private static void FindSum()
        {
            int[,] firstMatrix, secondMatrix, result;
            Console.WriteLine("Create first Matrix");
            ChooseTypeOfMatrix(out firstMatrix);
            Console.WriteLine("\r\nCreate second Matrix");
            ChooseTypeOfMatrix(out secondMatrix);

            if (firstMatrix.GetLength(0) != secondMatrix.GetLength(0) || secondMatrix.GetLength(1) != firstMatrix.GetLength(1))
            {
                Console.WriteLine("To add matrices their size must be the same");
                ExitOrRestart();
            }
            else
            {
                result = new int[firstMatrix.GetLength(0), firstMatrix.GetLength(1)];

                for (int i = 0; i < firstMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < firstMatrix.GetLength(1); j++)
                    {
                        result[i, j] = firstMatrix[i, j] + secondMatrix[i, j];
                    }
                }

                Console.WriteLine("\r\nAnswer of sum is :");
                PrintMatrix(result);
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы найти разницу двух матриц.
        /// </summary>
        private static void FindDifference()
        {
            int[,] firstMatrix, secondMatrix, result;
            Console.WriteLine("Create first Matrix");
            ChooseTypeOfMatrix(out firstMatrix);
            Console.WriteLine("\r\nCreate second Matrix");
            ChooseTypeOfMatrix(out secondMatrix);

            if (firstMatrix.GetLength(0) != secondMatrix.GetLength(0) || secondMatrix.GetLength(1) != firstMatrix.GetLength(1))
            {
                Console.WriteLine("To find difference of matrices their size must be the same");
                ExitOrRestart();
            }
            else
            {
                result = new int[firstMatrix.GetLength(0), firstMatrix.GetLength(1)];

                for (int i = 0; i < firstMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < firstMatrix.GetLength(1); j++)
                    {
                        result[i, j] = firstMatrix[i, j] - secondMatrix[i, j];
                    }
                }

                Console.WriteLine("\r\nAnswer of difference is :");
                PrintMatrix(result);
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы найти произведение двух матриц.
        /// </summary>
        private static void FindMultiplication()
        {
            int[,] firstMatrix, secondMatrix, result;
            Console.WriteLine("Create first Matrix");
            ChooseTypeOfMatrix(out firstMatrix);
            Console.WriteLine("\r\nCreate second Matrix");
            ChooseTypeOfMatrix(out secondMatrix);

            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
            {
                Console.WriteLine("Number of columns of first matrix and rows of second matrix must be the same");
                ExitOrRestart();
            }
            else
            {
                result = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];

                for (int i = 0; i < firstMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < secondMatrix.GetLength(1); j++)
                    {
                        for (int a = 0; a < firstMatrix.GetLength(1); a++)
                        {
                            result[i, j] += firstMatrix[i, a] * secondMatrix[a, j];
                        }
                    }
                }

                Console.WriteLine("\r\nAnswer of multiplication is :");
                PrintMatrix(result);
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы умножить матрицу на число.
        /// </summary>
        private static void MultiplicateByNumber()
        {
            int[,] matrix;
            ChooseTypeOfMatrix(out matrix);
            int num;

            Console.Write("Enter number to multiplicate by matrix : ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out num))
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
            else
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] *= num;
                    }
                }
            }

            Console.WriteLine("\r\nAnswer of multiplication by a number is :");
            PrintMatrix(matrix);
            ExitOrRestart();
        }

        /// <summary>
        /// Метод чтобы найти детерминант матрицы.
        /// </summary>
        private static void FindDeterminant()
        {
            int[,] matrix;
            double result;
            ChooseTypeOfMatrix(out matrix);

            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                Console.WriteLine("Matrix must be square type");
                ExitOrRestart();
            }
            else
            {
                result = Deter(matrix.GetLength(0), matrix);
                Console.WriteLine($"\r\nDeterminant of matrix is {result}");
                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы решить Систему линейных алгебраических уравнений (СЛАУ) методом Крамера.
        /// </summary>
        private static void SolveSytemWithCramer()
        {
            int[,] matrix, freeMemberMatrix;
            double[,] answers;
            ChooseTypeOfMatrix(out matrix);
            int n = matrix.GetLength(0);

            if (n != matrix.GetLength(1))
            {
                Console.WriteLine("Matrix must be square type");
                ExitOrRestart();
            }
            else
            {
                if (Deter(n, matrix) == 0)
                {
                    Console.WriteLine("Determinant mustn't be 0");
                    ExitOrRestart();
                }
                freeMemberMatrix = new int[1,n];
                Console.WriteLine($"Enter numbers with Space of free-member column matrix" +
                    $"\r\n(Number of them must be {n})");

                string[] parts = Console.ReadLine().Split(' ');
                if (parts.Length != n)
                {
                    Console.WriteLine("Wrong input");
                    ExitOrRestart();
                }

                int[][,] clones = new int[n][,];
                for (int i = 0; i < n; i++)
                {
                    if (!int.TryParse(parts[i], out freeMemberMatrix[0,i]))
                    {
                        Console.WriteLine("Wrong input");
                        ExitOrRestart();
                    }
                    clones[i] = new int[n,n];
                    for (int a = 0; a < n; a++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            clones[i][a, j] = matrix[a, j];
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        clones[i][j, i] = freeMemberMatrix[0, j];
                    }
                }

                answers = new double[1,n];
                Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    answers[0, i] = Deter(n, clones[i]) / Deter(n, matrix);
                    Console.WriteLine($"x{i+1} = {answers[0,i]:F3}");
                }

                ExitOrRestart();
            }
        }

        /// <summary>
        /// Метод чтобы решить Систему линейных алгебраических уравнений (СЛАУ) методом Гаусса.
        /// </summary>
        private static void SolveSystemWithGauss()
        {
            int[] freeMemberMatrix;
            int[,] matrix;
            ChooseTypeOfMatrix(out matrix);
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] matrixForGauss = new double[rows, columns + 1];
            freeMemberMatrix = new int[rows];
            Console.WriteLine($"Enter numbers with Space of free-member column matrix" +
                $"\r\n(Number of them must be {rows})");

            string[] parts = Console.ReadLine().Split(' ');
            if (parts.Length != rows)
            {
                Console.WriteLine("Wrong input");
                ExitOrRestart();
            }
            for (int i = 0; i < rows; i++)
            {
                if (!double.TryParse(parts[i], out matrixForGauss[i, columns]))
                {
                    Console.WriteLine("Wrong input");
                    ExitOrRestart();
                }
                for (int j = 0; j < columns; j++)
                {
                    matrixForGauss[i, j] = matrix[i, j];
                }
            }

            RowReduce(matrixForGauss);
            PrintMatrix(matrixForGauss);

            ExitOrRestart();
        }

        /// <summary>
        /// Метод чтобы привести матрицу к ступеньчатому виду.
        /// </summary>
        /// <param name="matrix">Наша матрица на которую хотим применить метод Гаусса.</param>
        /// <returns>Возвращает ступеньчатаю матрицу.</returns>
        private static double[,] RowReduce(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int forCols = 0; forCols < cols - 1; forCols++)
            {
                double doubleDivisor = matrix[forCols, forCols];
                for (int i = 0; i < cols; i++)
                {
                    matrix[forCols, i] = matrix[forCols, i] / doubleDivisor;
                }

                for (int forRows = 0; forRows < rows; forRows++)
                {
                    if (forRows != forCols)
                    {
                        double DoubleFactor = matrix[forRows, forCols] * -1;
                        for (int i = 0; i < cols; i++)
                        {
                            matrix[forRows, i] = matrix[forRows, i] + matrix[forCols, i] * DoubleFactor;
                        }
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// Метод для вычесления детерминанта.
        /// </summary>
        /// <param name="n">Размерность матрицы.</param>
        /// <param name="matrix">Сама матрица.</param>
        /// <returns>Возвращает вещественное число которая равна детерминанту матрицы.</returns>
        private static double Deter(int n, int[,] matrix)
        {
            double d = 0;
            int k, i, j, subi, subj;
            int[,] SUBMat = new int[n, n];

            if (n == 2)
            {
                return ((matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0, 1]));
            }

            else
            {
                for (k = 0; k < n; k++)
                {
                    subi = 0;
                    for (i = 1; i < n; i++)
                    {
                        subj = 0;
                        for (j = 0; j < n; j++)
                        {
                            if (j == k)
                            {
                                continue;
                            }
                            SUBMat[subi, subj] = matrix[i, j];
                            subj++;
                        }
                        subi++;
                    }
                    d = d + (Math.Pow(-1, k) * matrix[0, k] * Deter(n - 1, SUBMat));
                }
            }
            return d;
        }

        /// <summary>
        /// Метод чтобы выйти с приложения или начать все заново.
        /// </summary>
        private static void ExitOrRestart()
        {
            Console.Write("Write 1 if you want to Exit, or anything to Restart\r\n>>> ");
            if (Console.ReadLine() == "1")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                ChooseWhatYouWillDo();
            }
        }
    }
}