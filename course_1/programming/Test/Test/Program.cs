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
            Console.WriteLine("== Главное меню == \n1. Отгадай ответ \n2. Математическая игра \n3. Сортировка массива \n4. Об авторе \n5. Выход");
            Console.Write("Выберите пункт: ");

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
                    ShowAbout();
                    break;
                case "5":
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
        else { Console.WriteLine("Ошибка вывода, длинна массива больше 10"); }
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
}