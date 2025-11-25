using System;

class Program
{
    const int MaxHistory = 10;

    static double[] weights = new double[MaxHistory];
    static double[] heights = new double[MaxHistory];
    static string[] genders = new string[MaxHistory];
    static int[] ages = new int[MaxHistory];
    static double[] bmiResults = new double[MaxHistory];
    static DateTime[] dates = new DateTime[MaxHistory];

    static int historyCount = 0;

    static void Main()
    {
        int choice;

        do
        {
            Console.Clear();
            Console.WriteLine("=== Анализатор ИМТ ===");
            Console.WriteLine("1. Новый расчёт");
            Console.WriteLine("2. История замеров");
            Console.WriteLine("3. Анализ динамики");
            Console.WriteLine("4. Рекомендации");
            Console.WriteLine("5. Выйти");
            Console.Write("\nВыберите действие: ");

            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1:
                    NewMeasurement();
                    break;
                case 2:
                    ShowMeasurementHistory();
                    break;
                case 3:
                    AnalyzeDynamics();
                    break;
                case 4:
                    ShowRecommendations();
                    break;
            }

        } while (choice != 5);
    }

    static void NewMeasurement()
    {
        Console.Clear();
        Console.WriteLine("=== Новый расчёт ИМТ ===");

        double weight = ReadDouble("Введите вес (кг): ");
        double height = ReadDouble("Введите рост (м): ");

        string gender;
        do
        {
            Console.Write("Введите пол (M/F): ");
            gender = Console.ReadLine().Trim().ToUpper();
        } while (gender != "M" && gender != "F");

        int age = ReadInt("Введите возраст: ");

        double bmi = CalculateBMI(weight, height);
        string category = DetermineBMICategory(bmi);

        Console.WriteLine($"\nВаш ИМТ: {bmi:F1}");
        Console.WriteLine($"Категория: {category}");

        SaveMeasurementToHistory(weight, height, gender, age, bmi);

        Console.WriteLine("\nДанные сохранены!");
        Console.ReadKey();
    }

    static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    static string DetermineBMICategory(double bmi)
    {
        if (bmi < 18.5) return "Недостаточная масса";
        if (bmi < 25) return "Норма";
        if (bmi < 30) return "Избыточный вес";
        return "Ожирение";
    }

    static void SaveMeasurementToHistory(double weight, double height, string gender, int age, double bmi)
    {
        if (historyCount < MaxHistory)
        {
            int i = historyCount;
            weights[i] = weight;
            heights[i] = height;
            genders[i] = gender;
            ages[i] = age;
            bmiResults[i] = bmi;
            dates[i] = DateTime.Now;
            historyCount++;
        }
        else
        {
            for (int i = 1; i < MaxHistory; i++)
            {
                weights[i - 1] = weights[i];
                heights[i - 1] = heights[i];
                genders[i - 1] = genders[i];
                ages[i - 1] = ages[i];
                bmiResults[i - 1] = bmiResults[i];
                dates[i - 1] = dates[i];
            }

            int last = MaxHistory - 1;
            weights[last] = weight;
            heights[last] = height;
            genders[last] = gender;
            ages[last] = age;
            bmiResults[last] = bmi;
            dates[last] = DateTime.Now;
        }
    }

    static void ShowMeasurementHistory()
    {
        Console.Clear();
        Console.WriteLine("=== История замеров ===");

        if (historyCount == 0)
        {
            Console.WriteLine("История пуста.");
        }
        else
        {
            for (int i = 0; i < historyCount; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. {dates[i]:dd.MM.yyyy} — Вес: {weights[i]} кг, " +
                    $"Рост: {heights[i]} м, Пол: {genders[i]}, Возраст: {ages[i]}, " +
                    $"ИМТ: {bmiResults[i]:F1}");
            }
        }

        Console.ReadKey();
    }

    static void AnalyzeDynamics()
    {
        Console.Clear();
        Console.WriteLine("=== Анализ динамики ===");

        if (historyCount == 0)
        {
            Console.WriteLine("Нет данных для анализа.");
            Console.ReadKey();
            return;
        }

        double sum = 0;
        double max = bmiResults[0];
        double min = bmiResults[0];
        DateTime maxDate = dates[0];
        DateTime minDate = dates[0];

        for (int i = 0; i < historyCount; i++)
        {
            sum += bmiResults[i];

            if (bmiResults[i] > max)
            {
                max = bmiResults[i];
                maxDate = dates[i];
            }

            if (bmiResults[i] < min)
            {
                min = bmiResults[i];
                minDate = dates[i];
            }
        }

        double avg = sum / historyCount;

        double delta = 0;
        if (historyCount > 1)
        {
            delta = bmiResults[historyCount - 1] - bmiResults[historyCount - 2];
        }

        Console.WriteLine($"Всего замеров: {historyCount}");
        Console.WriteLine($"Средний ИМТ: {avg:F1}");
        Console.WriteLine($"Максимальный ИМТ: {max:F1} ({maxDate:dd.MM.yyyy})");
        Console.WriteLine($"Минимальный ИМТ: {min:F1} ({minDate:dd.MM.yyyy})");
        Console.WriteLine($"Изменение последнего замера: {delta:+0.0;-0.0;0.0}");

        Console.ReadKey();
    }


    static void ShowRecommendations()
    {
        Console.Clear();
        Console.WriteLine("=== Рекомендации ===");

        if (historyCount == 0)
        {
            Console.WriteLine("Нет данных.");
            Console.ReadKey();
            return;
        }

        double bmi = bmiResults[historyCount - 1];

        if (bmi < 18.5)
            Console.WriteLine("Вам стоит увеличить калорийность питания.");
        else if (bmi < 25)
            Console.WriteLine("Ваш вес в норме!");
        else if (bmi < 30)
            Console.WriteLine("Рекомендуется обратить внимание на физическую активность.");
        else
            Console.WriteLine("Рекомендуется консультация с врачом.");

        Console.ReadKey();
    }

    static double ReadDouble(string msg)
    {
        double value;
        do
        {
            Console.Write(msg);
        } while (!double.TryParse(Console.ReadLine(), out value) || value <= 0);
        return value;
    }

    static int ReadInt(string msg)
    {
        int value;
        do
        {
            Console.Write(msg);
        } while (!int.TryParse(Console.ReadLine(), out value) || value <= 0);
        return value;
    }
}
