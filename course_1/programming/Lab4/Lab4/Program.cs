using System;
using System.Diagnostics;

namespace Lab5
{
    /// <summary>
    /// Точка входа в программу — меню, которое запускает все части лабораторной работы.
    /// </summary>
    class Program
    {
        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("== Главное меню == \n1. Отгадай ответ \n2. Математическая игра \n3. Сортировка массива \n4. Игра (Поиск сокровищ) \n5. Об авторе \n0. Выход");
                Console.Write("Выберите пункт (0-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        FunctionGame.Play(); // статический класс функции + игра
                        break;
                    case "2":
                        MathGame.Run(); // математическая игра (3 задачки)
                        break;
                    case "3":
                        SortingMenu();
                        break;
                    case "4":
                        var treasureGame = new TreasureGame();
                        treasureGame.Play();
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

        /// <summary>
        /// Меню запуска сортировки массива — демонстрация класса ArrayWorker.
        /// </summary>
        private static void SortingMenu()
        {
            Console.Clear();
            Console.WriteLine("Запущено приложение - Сортировка массива");
            Console.Write("Введите желаемую длину массива: ");
            int length = InputValidator.IntInput();
            while (length <= 0)
            {
                Console.WriteLine("Ошибка, длина массива должна быть больше 0");
                length = InputValidator.IntInput();
            }

            // Создаём объект ArrayWorker c параметром
            var worker = new ArrayWorker(length);

            // Копия массива для второго метода
            int[] copy = worker.ArrayCopy();

            Console.WriteLine("\nИсходный массив:");
            worker.PrintArrayInline();

            var swShell = Stopwatch.StartNew();
            int[] shellSorted = worker.ShellSort(); // сортирует внутренний массив и возвращает ссылку
            swShell.Stop();

            // На копии применим сортировку выбора
            var swSelect = Stopwatch.StartNew();
            int[] selectSorted = ArrayWorker.SelectionSortStatic(copy);
            swSelect.Stop();

            Console.WriteLine($"\nСортировка массива методом Шелла заняла {swShell.Elapsed.TotalMilliseconds} мс");
            Console.WriteLine($"Сортировка массива методом Выбора заняла {swSelect.Elapsed.TotalMilliseconds} мс");

            Console.WriteLine("\nРезультат сортировки (Шелл):");
            // worker.PrintArrayInline(); // уже отсортировано
            Console.WriteLine(string.Join(", ", shellSorted));
        }

        /// <summary>
        /// Показать информацию об авторе.
        /// </summary>
        private static void ShowAbout()
        {
            Console.Clear();
            Console.WriteLine("Об авторе:");
            Console.WriteLine("Автор: Полевода Денис Алексеевич");
            Console.WriteLine("Группа: 6105-090301D");
        }

        /// <summary>
        /// Подтверждение выхода из программы.
        /// </summary>
        private static bool ConfirmExit()
        {
            while (true)
            {
                Console.Write("\nВы действительно хотите выйти? (д/н): ");
                string answer = Console.ReadLine()?.Trim().ToLower();

                if (answer == "д")
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
    }

    #region FunctionGame

    /// <summary>
    /// Статический класс, содержащий методы для вычисления функции
    /// и мини-игры "Отгадай ответ".
    /// </summary>
    public static class FunctionGame
    {
        /// <summary>
        /// Вычисляет значение функции f(a,b) = round(log( sqrt(b + sin(a)) / cos^3(a) , b ), 2).
        /// Возвращает double.NaN если функция не определена при данных a,b.
        /// </summary>
        /// <param name="a">угол/параметр a (в радианах)</param>
        /// <param name="b">основание логарифма b</param>
        /// <returns>значение функции или NaN если не определено</returns>
        public static double Calculate(double a, double b)
        {
            // проверяем базовые условия (подкоренное > 0, b > 0 и b != 1, косинус != 0)
            double underSqrt = b + Math.Sin(a);
            if (b <= 0 || b == 1 || Math.Cos(a) == 0 || underSqrt <= 0)
                return double.NaN;

            double value = Math.Sqrt(underSqrt) / Math.Pow(Math.Cos(a), 3);
            if (value <= 0) return double.NaN;

            double result = Math.Log(value, b);
            return Math.Round(result, 2);
        }

        /// <summary>
        /// Игра: пользователь вводит a и b, программа вычисляет функцию,
        /// затем пользователь пытается угадать (вводит значение). 
        /// Результат сравнивается с округлением до 2 знаков.
        /// </summary>
        public static void Play()
        {
            Console.Clear();
            Console.WriteLine("Запущено приложение - Отгадай ответ");

            double a, b, func;
            do
            {
                Console.Write("Введите значение для переменной a: ");
                a = InputValidator.DoubleInput();
                Console.Write("Введите значение для переменной b (b > 0, b != 1): ");
                b = InputValidator.DoubleInput();

                func = Calculate(a, b);

                if (double.IsNaN(func))
                    Console.WriteLine("При введённых значениях функция не имеет ответа, попробуйте ещё раз");
            } while (double.IsNaN(func));

            Console.Write("Ваш ответ, округлённый до 2 знаков после запятой: ");
            double answer = InputValidator.DoubleInput();

            if (func == Math.Round(answer, 2))
                Console.WriteLine($"Вы угадали, ответ был {func}");
            else
                Console.WriteLine($"Вы не угадали, ответ был {func}");
        }
    }

    #endregion

    #region MathGame

    /// <summary>
    /// Небольшая математическая игра — три простых примера.
    /// </summary>
    public static class MathGame
    {
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Запустить игру.
        /// </summary>
        public static void Run()
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

        /// <summary>
        /// Сгенерировать один пример (сложение, вычитание, умножение) и вернуть 1 если правильно, 0 если нет.
        /// </summary>
        private static int PuzzleGenerator()
        {
            int num1 = _rnd.Next(101);
            int num2 = _rnd.Next(101);
            int correctAnswer;
            char oper;

            switch (_rnd.Next(3))
            {
                case 0:
                    oper = '+';
                    correctAnswer = num1 + num2;
                    break;
                case 1:
                    oper = '-';
                    correctAnswer = num1 - num2;
                    break;
                default:
                    oper = '*';
                    correctAnswer = num1 * num2;
                    break;
            }

            Console.Write($"Ваш пример: {num1} {oper} {num2} = ");
            int answer = InputValidator.IntInput();
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
    }

    #endregion

    #region InputValidator

    /// <summary>
    /// Статический класс для проверки ввода и безопасного считывания чисел.
    /// </summary>
    public static class InputValidator
    {
        /// <summary>
        /// Безопасный ввод целого числа.
        /// </summary>
        /// <returns>Введённое целое число</returns>
        public static int IntInput()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.Write("Неверная запись числа, повторите попытку: ");
            }
            return num;
        }

        /// <summary>
        /// Безопасный ввод числа с плавающей точкой.
        /// </summary>
        /// <returns>Введённое число double</returns>
        public static double DoubleInput()
        {
            double num;
            while (!double.TryParse(Console.ReadLine(), out num))
            {
                Console.Write("Неверная запись числа, повторите попытку: ");
            }
            return num;
        }
    }

    #endregion

    #region ArrayWorker

    /// <summary>
    /// Класс для работы с массивом: хранит массив, размер и предоставляет методы заполнения и сортировки.
    /// </summary>
    public class ArrayWorker
    {
        private int _size;
        private int[] _array;
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Доступ к внутреннему массиву (только для чтения ссылки).
        /// </summary>
        public int[] Array => _array;

        /// <summary>
        /// Конструктор по умолчанию (создаёт массив из 10 элементов).
        /// </summary>
        public ArrayWorker() : this(10) { }

        /// <summary>
        /// Конструктор с указанием размера.
        /// </summary>
        /// <param name="size">размер массива</param>
        public ArrayWorker(int size)
        {
            if (size <= 0) size = 10;
            _size = size;
            _array = new int[_size];
            Fill();
        }

        /// <summary>
        /// Заполняет массив случайными числами от 0 до 500.
        /// </summary>
        private void Fill()
        {
            for (int i = 0; i < _size; i++)
                _array[i] = _rnd.Next(501);
        }


        /// <summary>
        /// Возвращает копию массива, выполненную через цикл for (по требованию лабораторной).
        /// </summary>
        public int[] ArrayCopy()
        {
            int[] copy = new int[_array.Length];
            for (int i = 0; i < _array.Length; i++)
            {
                copy[i] = _array[i];
            }
            return copy;
        }


        /// <summary>
        /// Выводит массив одной строкой через запятую (если маленький).
        /// </summary>
        public void PrintArrayInline()
        {
            if (_array.Length <= 100) // печатаем любые разумные длины
            {
                Console.WriteLine(string.Join(", ", _array));
            }
            else
            {
                Console.WriteLine("Массив слишком длинный для вывода.");
            }
        }

        /// <summary>
        /// Сортировка методом Шелла (изменяет внутренний массив и возвращает его).
        /// </summary>
        /// <returns>Отсортированный массив</returns>
        public int[] ShellSort()
        {
            int n = _array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = _array[i];
                    int j;
                    for (j = i; j >= gap && _array[j - gap] > temp; j -= gap)
                    {
                        _array[j] = _array[j - gap];
                    }
                    _array[j] = temp;
                }
            }
            return _array;
        }

        /// <summary>
        /// Статическая реализация сортировки выбором для переданного массива.
        /// </summary>
        /// <param name="mass">массив, который будет отсортирован (копия желательна)</param>
        /// <returns>Отсортированный массив</returns>
        public static int[] SelectionSortStatic(int[] mass)
        {
            int lenght = mass.Length;
            for (int i = 0; i < lenght - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < lenght; j++)
                {
                    if (mass[j] < mass[minIndex])
                        minIndex = j;
                }
                int temp = mass[i];
                mass[i] = mass[minIndex];
                mass[minIndex] = temp;
            }
            return mass;
        }
    }

    #endregion

    #region TreasureGame

    /// <summary>
    /// Класс, реализующий игру "Поиск сокровищ".
    /// Поля приватные, есть логика выбора сложности, генерации поля и игрового цикла.
    /// </summary>
    public class TreasureGame
    {
        private const string TREASURE = "C";
        private const string TRAP = "T";
        private const string EMPTY = "0";

        private readonly int _rows;
        private readonly int _cols;
        private readonly int _treasureCount;
        private readonly int _trapCount;

        private string[,] _field;      // реальное поле (C, T, 0)
        private string[,] _playerView; // то, что видит игрок (null или "C"/"T"/"0")

        private static readonly Random _rnd = new Random();

        /// <summary>
        /// Конструктор: выбирает уровень сложности и генерирует поле.
        /// </summary>
        public TreasureGame()
        {
            int[] diff = DifficultySelect();
            _rows = diff[0];
            _cols = diff[1];
            _treasureCount = diff[2];
            _trapCount = diff[3];

            _field = GenerateField();
            _playerView = new string[_rows, _cols]; // все элементы null изначально
        }

        /// <summary>
        /// Основной игровой цикл: игрок выбирает координаты, пока не найдёт все сокровища или не попадёт в ловушку.
        /// </summary>
        public void Play()
        {
            bool isRunning = true;
            int foundTreasures = 0;

            do
            {
                Console.Clear();
                FieldReview(foundTreasures);

                int[] coords = CoordinatesInput();
                int x = coords[0];
                int y = coords[1];

                if (!string.IsNullOrEmpty(_playerView[x, y]))
                {
                    Console.WriteLine("\nВы уже были на этой клетке, нажмите любую клавишу и попробуйте другую.");
                    Console.ReadKey();
                    continue;
                }

                // открыть клетку
                _playerView[x, y] = _field[x, y];

                foundTreasures = Count(_playerView, TREASURE);
                int trapsFound = Count(_playerView, TRAP);

                if (foundTreasures >= _treasureCount)
                {
                    Console.Clear();
                    FieldReview(foundTreasures);
                    Console.WriteLine("\n--- Вы победили! Все сокровища найдены! ---");
                    isRunning = false;
                }
                else if (trapsFound > 0)
                {
                    Console.Clear();
                    FieldReview(foundTreasures);
                    Console.WriteLine("\n--- Вы проиграли! Попались в ловушку! ---");
                    isRunning = false;
                }

            } while (isRunning);
        }

        /// <summary>
        /// Выбор уровня сложности пользователем (1..3).
        /// Возвращает массив: [rows, cols, treasureCount, trapCount].
        /// </summary>
        /// <returns>массив параметров сложности</returns>
        private int[] DifficultySelect()
        {
            Console.Write("Введите желаемый уровень сложности (1-3): ");
            int difficulty = InputValidator.IntInput();
            while (difficulty < 1 || difficulty > 3)
            {
                Console.Write("Неверный ввод. Повторите: ");
                difficulty = InputValidator.IntInput();
            }

            switch (difficulty)
            {
                case 1: return new int[] { 10, 10, 10, 5 };
                case 2: return new int[] { 12, 12, 10, 8 };
                case 3: return new int[] { 15, 15, 10, 10 };
                default: return new int[] { 10, 10, 10, 5 };
            }
        }

        /// <summary>
        /// Генерация поля с размещением сокровищ и ловушек (без пересечений).
        /// </summary>
        /// <returns>заполненное поле</returns>
        private string[,] GenerateField()
        {
            string[,] field = new string[_rows, _cols];

            // расставляем сокровища
            for (int i = 0; i < _treasureCount; i++)
            {
                int x, y;
                do
                {
                    x = _rnd.Next(_rows);
                    y = _rnd.Next(_cols);
                } while (field[x, y] != null);
                field[x, y] = TREASURE;
            }

            // расставляем ловушки
            for (int i = 0; i < _trapCount; i++)
            {
                int x, y;
                do
                {
                    x = _rnd.Next(_rows);
                    y = _rnd.Next(_cols);
                } while (field[x, y] != null);
                field[x, y] = TRAP;
            }

            // оставшиеся клетки — пустые
            for (int i = 0; i < _rows; i++)
                for (int j = 0; j < _cols; j++)
                    if (field[i, j] == null)
                        field[i, j] = EMPTY;

            return field;
        }

        /// <summary>
        /// Отображение текущего состояния поля, с выравниванием колонок.
        /// </summary>
        /// <param name="foundTreasures">сколько сокровищ уже найдено (для показа)</param>
        private void FieldReview(int foundTreasures)
        {
            Console.Write("    ");
            for (int i = 0; i < _cols; i++)
                Console.Write($"{i,3}");
            Console.WriteLine();

            for (int i = 0; i < _rows; i++)
            {
                Console.Write($"{i,3}");
                for (int j = 0; j < _cols; j++)
                {
                    if (string.IsNullOrEmpty(_playerView[i, j]))
                        Console.Write(" [ ]");
                    else
                        Console.Write($"[{_playerView[i, j],1}] ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"\nКоличество сокровищ, которое вы нашли: {foundTreasures}/{_treasureCount}");
        }

        /// <summary>
        /// Ввод координат X (строка) и Y (столбец) с проверкой диапазона.
        /// </summary>
        /// <returns>массив длины 2: [x,y]</returns>
        private int[] CoordinatesInput()
        {
            int[] coords = new int[2];

            Console.Write("Введите координату по оси X (номер строки): ");
            coords[0] = InputValidator.IntInput();
            while (coords[0] < 0 || coords[0] >= _rows)
            {
                Console.Write($"Неверная запись, координата должна быть в пределах 0-{_rows - 1}: ");
                coords[0] = InputValidator.IntInput();
            }

            Console.Write("Введите координату по оси Y (номер столбца): ");
            coords[1] = InputValidator.IntInput();
            while (coords[1] < 0 || coords[1] >= _cols)
            {
                Console.Write($"Неверная запись, координата должна быть в пределах 0-{_cols - 1}: ");
                coords[1] = InputValidator.IntInput();
            }

            return coords;
        }

        /// <summary>
        /// Подсчитывает количество символов symbol на поле.
        /// </summary>
        /// <param name="field">поле, по которому производится подсчёт</param>
        /// <param name="symbol">символ для подсчёта</param>
        /// <returns>количество вхождений</returns>
        private int Count(string[,] field, string symbol)
        {
            int count = 0;
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    if (field[i, j] == symbol)
                        count++;
            return count;
        }
    }

    #endregion
}
