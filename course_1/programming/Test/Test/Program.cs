using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Xml.Schema;

class Program
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("== Главное меню == \n1. Отгадай ответ \n2. Математическая игра \n3. Сортировка массива \n4. Игра \n5. Об авторе \n0. Выход");
            Console.Write("Выберите пункт (0-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GuessAnswer();
                    break;
                case "2":
                    MathGame();
                    break;
                case "3":
                    SortingArray();
                    break;
                case "4":
                    Game();
                    break;
                case "5":
                    ShowAbout();
                    break;
                case "0":
                    running = ConfirmExit();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте ещё раз.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в меню...");
                Console.ReadKey();
            }
        }
    }

    static void GuessAnswer()
    {
        double a, b, answer, func;
        Console.Clear();
        Console.WriteLine("Запущено приложение - Отгадай ответ");

        do
        {
            Console.Write("Введите значение для переменной a: ");
            a = DoubleInput();
            Console.Write("Введите значение для переменной b (b > 0, b != 1): ");
            b = DoubleInput();

            func = Function(a, b);

            if (double.IsNaN(func))
            {
                Console.WriteLine("При введённых значениях функция не имеет ответа, попробуйте ещё раз");
            }
        } while (double.IsNaN(func));

        Console.Write("Ваш ответ, округлённый до 2 знаков после запятой: ");
        answer = DoubleInput();
        if (func == Math.Round(answer, 2))
        {
            Console.WriteLine($"Вы угадали, ответ был {func}");
        }
        else
        {
            Console.WriteLine($"Вы не угадали, ответ был {func}");
        }
    }

    static void ShowAbout()
    {
        Console.Clear();
        Console.WriteLine("Об авторе:");
        Console.WriteLine("Автор: Полевода Денис Алексеевич");
        Console.WriteLine("Группа: 6105-090301D");
    }

    static bool ConfirmExit()
    {
        while (true)
        {
            Console.Write("\nВы действительно хотите выйти? (д/н): ");
            string answer = Console.ReadLine();

            if (answer == "д" || answer == "")
            {
                Console.WriteLine("Выход из программы...");
                return false;
            }
            else if (answer == "н")
            {
                Console.WriteLine("Возврат в главное меню...");
                return true;
            }
            else
            {
                Console.WriteLine("Неверный ввод. Введите 'д' или 'н'.");
            }
        }
    }

    static int PuzzleGenerator()
    {
        Random rnd = new Random();
        int num1 = rnd.Next(101);
        int num2 = rnd.Next(101);
        int answer, correctAnswer = 0;
        char oper = '+';

        switch (rnd.Next(3))
        {
            case 0:
                oper = '+';
                correctAnswer = num1 + num2;
                break;
            case 1:
                oper = '-';
                correctAnswer = num1 - num2;
                break;
            case 2:
                oper = '*';
                correctAnswer = num1 * num2;
                break;
        }
        Console.Write($"Ваш пример: {num1} {oper} {num2} = ");
        answer = IntInput();
        if (answer == correctAnswer)
        {
            Console.WriteLine("Вы ответили верно");
            return 1;
        }
        else
        {
            Console.WriteLine("Вы ответили неверно");
            return 0;
        }
    }
    static void MathGame()
    {
        Console.Clear();
        Console.WriteLine("Запущено приложение - Математическая игра");
        int rightAnswers = 0;
        for (int i = 0; i < 3; i++)
        {
            rightAnswers += PuzzleGenerator();
        }
        Console.WriteLine($"\nКоличество правильных ответов: {rightAnswers}");
    }
    static int IntInput()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            Console.Write("Неверная запись числа, повторите попытку: ");
        }
        return num;
    }
    static double DoubleInput()
    {
        double num;
        while (!double.TryParse(Console.ReadLine(), out num))
        {
            Console.Write("Неверная запись числа, повторите попытку: ");
        }
        return num;
    }
    static double Function(double a, double b)
    {
        double func = Math.Round(Math.Log(Math.Sqrt(b + Math.Sin(a)) / Math.Pow(Math.Cos(a), 3), b), 2);
        return func;
    }
    static void SortingArray()
    {
        Console.WriteLine("Запущено приложение - Сортировка массива");
        int length = ArrayLen();
        int[] mass = ArrayFill(length);
        int[] massCopy = ArrayCopying(mass);
        ArrayOutput(mass);
        Stopwatch stopwatchShell = new Stopwatch();
        Stopwatch stopwatchSellect = new Stopwatch();
        stopwatchShell.Start();
        int[] sortedMass = ShellSort(length, mass);
        stopwatchShell.Stop();
        stopwatchSellect.Start();
        int[] sortedMassCopy = SelectionSort(length, massCopy);
        stopwatchSellect.Stop();
        Console.WriteLine($"Сортировка массива методом Шелла заняла {stopwatchShell.Elapsed.TotalMilliseconds} мс");
        Console.WriteLine($"Сортировка массива методом Выбора заняла {stopwatchSellect.Elapsed.TotalMilliseconds} мс");
        ArrayOutput(sortedMass);

    }
    static int ArrayLen()
    {
        int lenght;
        Console.Write("Введите желаемую длинну массива: ");
        lenght = IntInput();
        while (lenght == 0) { Console.WriteLine("\nОшибка, длинна массива длжна быть больше 0"); lenght = IntInput(); }
        return lenght;
    }
    static int[] ArrayFill(int length)
    {
        Random rnd = new Random();
        int[] mass = new int[length];
        for (int i = 0; i < length; i++)
        {
            mass[i] = rnd.Next(501);
        }
        return mass;
    }
    static int[] ArrayCopying(int[] mass)
    {
        int[] copy = new int[mass.Length];
        Array.Copy(mass, copy, mass.Length);
        return copy;
    }
    static void ArrayOutput(int[] mass)
    {
        if (mass.Length <= 10)
        {
            for (int i = 0; i < mass.Length - 1; i++)
            {
                Console.Write($"{mass[i]}, ");
            }
            Console.Write($"{mass[mass.Length - 1]}");
        }
        else { Console.WriteLine("\nОшибка вывода, длинна массива больше 10"); }
    }
    static int[] ShellSort(int lenght, int[] mass)
    {
        int i, j, step, temp;
        for (step = lenght / 2; step > 0; step /= 2)
            for (i = step; i < lenght; i++)
            {
                temp = mass[i];
                for (j = i; j >= step; j -= step)
                {
                    if (temp < mass[j - step])
                        mass[j] = mass[j - step];
                    else
                        break;
                }
                mass[j] = temp;
            }
        return mass;
    }
    static int[] SelectionSort(int lenght, int[] mass)
    {
        for (int i = 0; i < lenght - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < lenght; j++)
            {
                if (mass[j] < mass[minIndex])
                {
                    minIndex = j;
                }
            }
            int temp = mass[i];
            mass[i] = mass[minIndex];
            mass[minIndex] = temp;
        }
        return mass;
    }
    static void Game()
    {
        Console.Clear();
        Console.WriteLine("Запущенно приложение - Поиск сокровищ");
        int[] difficulty = DifficultySellect();
        string[,] field = FieldGenerator(difficulty);
        string[,] emptyField = EmptyFieldGenerator(difficulty);
        bool isGameRunning = true;
        int treasureCount = 0;
        int sellectedTreasureCount;
        sellectedTreasureCount = difficulty[2];

        do
        {
            FieldReview(emptyField, treasureCount);
            int[] coords = CoordinatesInput(difficulty);
            int x = coords[0], y = coords[1];
            while (emptyField[x, y] != null)
            {
                Console.WriteLine("\nВы уже были на этой клетке, повторите попытку.");
                coords = CoordinatesInput(difficulty);
                x = coords[0];
                y = coords[1];
            }
            emptyField[x, y] = field[x, y];
            treasureCount = CountTreasures(emptyField);
            int trapCount = CountTraps(emptyField);

            if (treasureCount == sellectedTreasureCount || trapCount != 0)
            {
                isGameRunning = false;
                FieldReview(emptyField, treasureCount);
                if (treasureCount == 10) { Console.WriteLine("---Вы победили---"); }
                if (trapCount != 0) { Console.WriteLine("---Вы проиграли---"); }
            }
        } while (isGameRunning);

    }
    static int[] DifficultySellect()
    {
        Console.Write("Введите желаемый увроень сложности (1-3): ");

        int difficulty = IntInput();
        while (difficulty < 1 || difficulty > 3)
        {
            Console.WriteLine("Неверный выбор. Попробуйте ещё раз.");
            difficulty = IntInput();
        }
        int length = 0, width = 0, treasureCount = 0, trapCount = 0;
        switch (difficulty)
        {
            case 1:
                length = 10;
                width = 10;
                treasureCount = 10;
                trapCount = 5;
                break;
            case 2:
                length = 12;
                width = 12;
                treasureCount = 10;
                trapCount = 8;
                break;
            case 3:
                length = 15;
                width = 15;
                treasureCount = 10;
                trapCount = 10;
                break;
        }
        return new int[4] { length, width, treasureCount, trapCount };
    }
    static string[,] FieldGenerator(int[] difficulty)
    {
        int length, width, treasureCount, trapCount;
        length = difficulty[0];
        width = difficulty[1];
        treasureCount = difficulty[2];
        trapCount = difficulty[3];

        string[,] field = new string[length, width];
        Random rnd = new Random();
        for (int i = 0; i < treasureCount; i++)
        {
            int x, y;
            do
            {
                x = rnd.Next(length);
                y = rnd.Next(width);
            } while (field[x, y] != null);
            field[x, y] = "C";
        }

        for (int i = 0; i < trapCount; i++)
        {
            int x, y;
            do
            {
                x = rnd.Next(length);
                y = rnd.Next(width);
            } while (field[x, y] != null);
            field[x, y] = "T";
        }

        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (field[x, y] == null) { field[x, y] = "0"; }
            }
        }
        return field;
    }
    static string[,] EmptyFieldGenerator(int[] difficulty)
    {
        int length, width;
        length = difficulty[0];
        width = difficulty[1];
        string[,] field = new string[length, width];
        return field;
    }
    static void FieldReview(string[,] field, int treasureCount)
    {
        Console.Clear();
        Console.Write("   ");
        for (int i = 0; i < field.GetLength(1); i++)
        {
            Console.Write($"{i,3}");
        }
        Console.WriteLine();

        for (int i = 0; i < field.GetLength(0); i++)
        {
            Console.Write($"{i,3}");
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (string.IsNullOrEmpty(field[i, j])) Console.Write("[ ]");
                else Console.Write($"[{field[i, j]}]");
            }
            Console.WriteLine();
        }
        Console.WriteLine($"\nКоличество сокровищ которое вы нашли: {treasureCount}");
    }
    static int CountTreasures(string[,] field)
    {
        int count = 0;
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == "C")
                {
                    count++;
                }
            }
        }
        return count;
    }
    static int CountTraps(string[,] field)
    {
        int count = 0;
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == "T")
                {
                    count++;
                }
            }
        }
        return count;
    }
    static int[] CoordinatesInput(int[] difficulty)
    {
        int length, width;
        length = difficulty[0] - 1;
        width = difficulty[1] - 1;
        int[] coords = new int[2];
        Console.Write("Введите координату по оси X (номер столбца): ");
        coords[0] = IntInput();
        while (coords[0] > length || coords[0] < 0)
        {
            Console.Write($"Неверная запись, координата должна быть меньше {length}: ");
            coords[0] = IntInput(); 
        }
        Console.Write("Введите координату по оси Y (номер строки): ");
        coords[1] = IntInput();
        while (coords[1] > width || coords[1] < 0)
        {
            Console.Write($"Неверная запись, координата должна быть меньше {width}: ");
            coords[1] = IntInput(); 
        }
        return coords;
    }
}