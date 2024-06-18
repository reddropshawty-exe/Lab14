using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Lab10FINLIB
{
    public class Car : IInit, IComparable, ICloneable, GetKey
    {
        // ОПРЕДЕЛЕНИЕ ПОЛЕЙ БАЗОВОГО КЛАССА
        private string brand;
        private int year;
        private string color;
        private double cost;
        private double groundClearance;

        // Метод Clone из интерфейса IClonable
        public object Clone()
        {
            return new Car(this);
        }

        public override int GetHashCode()
        {
            int hashCode = 100;
            hashCode = (hashCode) ^ Brand.GetHashCode();
            hashCode = (hashCode) ^ Year.GetHashCode();
            hashCode = (hashCode) ^ Color.GetHashCode();
            hashCode = (hashCode) ^ Cost.GetHashCode();
            hashCode = (hashCode) ^ GroundClearance.GetHashCode();
            return hashCode;
        }

        // Аксессоры полей
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public int Year
        {
            get { return year; }
            set
            {
                try
                {
                    if (value > 1900 || value <= 2024)
                    {
                        year = value;
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                    year = 2015; // или любое другое значение по умолчанию
                }
            }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public double Cost
        {
            get { return cost; }
            set { cost = Math.Round(value / 100000) * 100000; }
        }

        public double GroundClearance
        {
            get { return groundClearance; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("GroundClearance", value, "Клиранс должен быть выше 0 см");
                }
                groundClearance = value;
            }
        }

        // КОНСТРУКТОРЫ

        // Конструктор без параметров, создает случайную машину
        [ExcludeFromCodeCoverage]
        public Car()
        {
            RandomInit();
        }

        // Конструктор с параметрами
        public Car(string brand, int year, string color, double cost, double groundClearance)
        {
            Brand = brand;
            Year = year;
            Color = color;
            Cost = cost;
            GroundClearance = groundClearance;
        }

        // Конструктор копирования
        public Car(Car otherCar)
        {
            Brand = otherCar.brand;
            Year = otherCar.year;
            Color = otherCar.color;
            Cost = otherCar.cost;
            GroundClearance = otherCar.groundClearance;
        }

        // НЕВИРТУАЛЬНЫЙ МЕТОД ВЫВОДА

        public void ShowAll()
        {
            Console.WriteLine($"автомобиль {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм.\n");
        }

        // ВИРТУАЛЬНЫЙ МЕТОД ВЫВОДА

        public virtual void Show()
        {
            Console.WriteLine($"автомобиль {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм.\n");
        }

        public override string ToString()
        {
            return $"автомобиль {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм.\n";
        }

        public string GetKey()
        {
            return $"автомобиль {Color} {Brand} {Year} года выпуска";
        }

        // ДЛЯ РАНДОМНОГО ЗАПОЛНЕНИЯ

        protected Random rnd = new Random();
        static string[] brands = { "Volkswagen", "Skoda", "Audi", "Porshe", "Bugatti", "Lamborgini" };
        static string[] colours = { "красный", "черный", "желтый", "серый", "серебристый", "синий", "белый" };

        // РАНДОМНЫЙ ВВОД

        public void RandomInit()
        {
            brand = brands[rnd.Next(brands.Length)];
            year = rnd.Next(2018, 2024);
            color = colours[rnd.Next(colours.Length)];
            cost = Math.Round(rnd.Next(300000, 10000000) / 100000.0) * 100000;
            groundClearance = rnd.Next(10, 50) / 10.0;
        }

        // РУЧНОЕ ЗАПОЛНЕНИЕ

        public void Init()
        {
            Console.Write("Введите бренд: ");
            brand = Console.ReadLine();

            year = EntryInt("год выпуска автомобиля", 2018);

            Console.Write("Введите цвет: ");
            color = Console.ReadLine();

            cost = Math.Round(EntryDouble("стоимость автомобиля", 1500000) / 100000) * 100000;

            groundClearance = EntryDouble("клиренс автомобиля в мм", 5);
        }

        // СРАВНЕНИЯ

        // Метод для сравнения машин на равенство
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Car p)
                return this.brand == p.brand
                        && this.year == p.year
                        && this.color == p.color
                        && this.cost == p.cost
                        && this.groundClearance == p.groundClearance;
            return false;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return -1;
            if (!(obj is Car)) return -1;
            Car c = obj as Car;
            return this.Year.CompareTo(c.Year);
        }

        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ

        static public double EntryDouble(string message, double baseValue)
        {
            double value;
            Console.WriteLine("Введите " + message);
            string c = Console.ReadLine();
            try
            {
                value = double.Parse(c);
            }
            catch
            {
                value = baseValue;
            }
            return value;
        }

        static public int EntryInt(string message, int baseValue)
        {
            int value;
            Console.WriteLine("Введите " + message);
            string c = Console.ReadLine();
            try
            {
                value = int.Parse(c);
            }
            catch
            {
                value = baseValue;
            }
            return value;
        }
    }
}
