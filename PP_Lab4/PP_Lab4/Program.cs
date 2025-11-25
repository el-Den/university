using System;

namespace BmiApp
{
    class BmiMeasurement
    {
        private double weight;
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 30 || value > 300) throw new ArgumentException("Вес должен быть в диапазоне 30-300 кг");
                weight = value;
            }
        }

        private double height;
        public double Height
        {
            get => height;
            set
            {
                if (value < 1.0 || value > 2.5) throw new ArgumentException("Рост должен быть в диапазоне 1.0-2.5 м");
                height = value;
            }
        }

        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                if (value != "м" && value != "ж") throw new ArgumentException("Пол должен быть 'м' или 'ж'");
                gender = value;
            }
        }

        private int age;
        public int Age
        {
            get => age;
            set
            {
                if (value < 1 || value > 120) throw new ArgumentException("Возраст должен быть 1-120");
                age = value;
            }
        }

        public double BmiValue { get; private set; }
        public string Category { get; private set; }
        public DateTime MeasurementDate { get; set; }

        public void CalculateBmi()
        {
            BmiValue = Weight / (Height * Height);
        }

        public void DetermineCategory()
        {
            if (BmiValue < 18.5) Category = "Недостаток веса";
            else if (BmiValue < 25) Category = "Норма";
            else if (BmiValue < 30) Category = "Избыточный вес";
            else Category = "Ожирение";
        }

        public void PrintReport()
        {
            Console.WriteLine($"Дата: {MeasurementDate:dd.MM.yyyy}");
            Console.WriteLine($"Вес: {Weight} кг, Рост: {Height} м, Пол: {Gender}, Возраст: {Age}");
            Console.WriteLine($"ИМТ: {BmiValue:F1}, Категория: {Category}");
            Console.WriteLine(new string('-', 30));
        }
    }

    class BmiAnalyzer
    {
        private BmiMeasurement[] measurements = new BmiMeasurement[10];
        private int currentIndex = 0;

        public void AddMeasurement(BmiMeasurement measurement)
        {
            measurements[currentIndex] = measurement;
            currentIndex = (currentIndex + 1) % measurements.Length;
        }

        public void ShowHistory()
        {
            Console.WriteLine("=== История замеров ===");
            foreach (var m in measurements)
            {
                if (m != null) m.PrintReport();
            }
        }

        public void AnalyzeTrends()
        {
            var validMeasurements = Array.FindAll(measurements, m => m != null);
            if (validMeasurements.Length < 2)
            {
                Console.WriteLine("Недостаточно данных для анализа.");
                return;
            }

            double first = validMeasurements[0].BmiValue;
            double last = validMeasurements[validMeasurements.Length - 1].BmiValue;
            double change = last - first;

            Console.WriteLine("=== Динамика показателей ===");
            Console.WriteLine($"Период: {validMeasurements[0].MeasurementDate:dd.MM.yyyy} - {validMeasurements[^1].MeasurementDate:dd.MM.yyyy}");
            Console.WriteLine($"Изменение ИМТ: {change:+0.0;-0.0} ({first:F1} → {last:F1})");
        }

        public void CompareMeasurements(int index1, int index2)
        {
            if (measurements[index1] == null || measurements[index2] == null)
            {
                Console.WriteLine("Невозможно сравнить: один из замеров отсутствует.");
                return;
            }

            double diff = measurements[index2].BmiValue - measurements[index1].BmiValue;
            Console.WriteLine($"Разница ИМТ между замерами {index1} и {index2}: {diff:+0.0;-0.0}");
        }
    }

    class Program
    {
        static void Main()
        {
            BmiAnalyzer analyzer = new BmiAnalyzer();

            while (true)
            {
                Console.WriteLine("=== Анализатор ИМТ (ООП) ===");
                Console.WriteLine("1. Новый замер");
                Console.WriteLine("2. История измерений");
                Console.WriteLine("3. Анализ динамики");
                Console.WriteLine("4. Выйти");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BmiMeasurement m = new BmiMeasurement();
                        try
                        {
                            Console.Write("Вес (кг): ");
                            m.Weight = double.Parse(Console.ReadLine());
                            Console.Write("Рост (м): ");
                            m.Height = double.Parse(Console.ReadLine());
                            Console.Write("Пол (м/ж): ");
                            m.Gender = Console.ReadLine();
                            Console.Write("Возраст: ");
                            m.Age = int.Parse(Console.ReadLine());
                            m.MeasurementDate = DateTime.Now;

                            m.CalculateBmi();
                            m.DetermineCategory();

                            analyzer.AddMeasurement(m);
                            Console.WriteLine("Замер добавлен!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка ввода: " + ex.Message);
                        }
                        break;

                    case "2":
                        analyzer.ShowHistory();
                        break;

                    case "3":
                        analyzer.AnalyzeTrends();
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
