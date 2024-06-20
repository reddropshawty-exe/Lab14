using System;
using System.Collections.Generic;
using System.Linq;
using Lab10FINLIB;
using Lab12Tusk;
using Lab14_;

namespace Lab12_p1
{
    class Program
    {
        static public Car CreateRandomCar()
        {
            Random rnd = new Random();
            int carType = rnd.Next(4); // случайное число от 0 до 3
            switch (carType)
            {
                case 0:
                    return new PassengerCar();
                case 1:
                    return new SUV();
                case 2:
                    return new Truck();
                default:
                    return new Car();
            }
        }

        static int EntryInt(string intElement = "число", int min = -100, int max = 100, string errorMessage = "Повторите ввод!")
        {
            while (true)
            {
                Console.WriteLine($"\nВведите {intElement}!");
                string s = Console.ReadLine();
                int n;
                bool res = int.TryParse(s, out n);
                if (res && n >= min && n <= max)
                {
                    return n;
                }
                Console.WriteLine(errorMessage);
            }
        }

        // GETKEY 
        static public string GetKeyTruckLinq(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = from item in MyCollect where item.Value is Truck select new { Brand = item.Value.Brand, Year = item.Value.Year, Price = item.Value.Cost };
            string result = "Все грузовики в коллекции:\n";
            foreach (var item in res)
            {
                result += $"Грузовик {item.Brand} {item.Year} года за {item.Price} рублей\n";
            }
            return result;
        }

        static public string GetKeyTruckUsingExtensions(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = MyCollect
                .Where(item => item.Value is Truck)
                .Select(item => $"Грузовик {item.Value.Brand} {item.Value.Year} года за {item.Value.Cost} рублей");

            return string.Join("\n", res);
        }

        // AVG
        static public string GetAvgPriceUsingExtensions(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = MyCollect.Average(car => car.Value.Cost);
            return $"Средняя цена авто в коллекции: {res}";
        }

        static public string GetAvgPriceLinq(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = (from car in MyCollect select car.Value.Cost).Average();
            return $"Средняя цена авто в коллекции: {res}";
        }

        // COUNT
        static public string GetCountFWDUsingExtensions(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = MyCollect.Count(x => x.Value is SUV suv && suv.Fwd);
            return $"Количество полноприводных внедорожников: {res}";
        }

        static public string GetCountFWDLinq(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = (from item in MyCollect where item.Value is SUV suv && suv.Fwd select item).Count();
            return $"Количество полноприводных внедорожников: {res}";
        }

        // ГРУППИРОВКА ПО ГРУППАМ
        public static string GroupByBrandLinq(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = from item in MyCollect group item by item.Value.Brand;

            string result = "";
            foreach (var group in res)
            {
                result += $"Марка: {group.Key}\n";
                foreach (var car in group)
                {
                    result += $"\t{car.Value.Brand} {car.Value.Year} за {car.Value.Cost} рублей\n";
                }
            }
            return result;
        }

        public static string GroupByBrandUsingMethods(MyHashTableCollection<string, Car> MyCollect)
        {
            var res = MyCollect
                .GroupBy(item => item.Value.Brand);

            string result = "";
            foreach (var group in res)
            {
                result += $"Марка: {group.Key}\n";
                foreach (var car in group)
                {
                    result += $"\t{car.Value.Brand} {car.Value.Year} за {car.Value.Cost} рублей\n";
                }
            }
            return result;
        }

        static void PrintCollection(MyHashTableCollection<string, Car> MyCollect)
        {
            foreach (var item in MyCollect)
            {
                Console.WriteLine($"{item.Key}: {item.Value.Brand} {item.Value.Year} за {item.Value.Cost} рублей");
            }
        }

        static void Main(string[] args)
        {
            var fabric = new CarFabric();
            var otherFabric = new CarFabric();
            var showroomList = new List<Showroom>
            {
                new Showroom("Volkswagen"),
                new Showroom("Skoda"),
                new Showroom("Audi"),
                new Showroom("Porshe"),
                new Showroom("Bugatti"),
                new Showroom("Lamborgini")
            };

            var myHashTable = new MyHashTableCollection<string, Car>();
            for (int i = 0; i < 10; i++)
            {
                Car added = CreateRandomCar();
                myHashTable.Add(added.ToString(), added);
            }

            while (true)
            {
                Console.WriteLine("Выберите подменю:");
                Console.WriteLine("1. Первая часть");
                Console.WriteLine("2. Вторая часть");
                Console.WriteLine("0. Выход");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    break;
                }
                else if (choice == 1)
                {
                    ShowCarFabricMenu(fabric, otherFabric, showroomList);
                }
                else if (choice == 2)
                {
                    ShowHashTableMenu(myHashTable);
                }
            }
        }

        static void ShowCarFabricMenu(CarFabric fabric, CarFabric otherFabric, List<Showroom> showroomList)
        {
            while (true)
            {
                Console.WriteLine("\nCarFabric меню:");
                Console.WriteLine("0. Вывести текущую коллекцию");
                Console.WriteLine("1. Найти премиум авто");
                Console.WriteLine("2. Найти эксклюзивные авто");
                Console.WriteLine("3. Средняя цена грузовиков");
                Console.WriteLine("4. Получить код авто");
                Console.WriteLine("5. Найти автосалоны");
                Console.WriteLine("6. Группировка по марке");
                Console.WriteLine("7. Назад");
                int choice = EntryInt("Пункт", 0, 7);

                if (choice == 7)
                {
                    break;
                }

                Console.WriteLine("Выберите метод:");
                Console.WriteLine("1. LINQ");
                Console.WriteLine("2. Методы расширения");
                int methodChoice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Текущая коллекция 1:");
                        Console.WriteLine(fabric.PrintFab());
                        Console.WriteLine("Текущая коллекция 2:");
                        Console.WriteLine(otherFabric.PrintFab());
                        break;
                    case 1:
                        if (methodChoice == 1)
                            Console.WriteLine(fabric.FindPremiumLinq());
                        else
                            Console.WriteLine(fabric.FindPremiumUsingExtensions());
                        break;
                    case 2:
                        if (methodChoice == 1)
                            Console.WriteLine(fabric.FindExclusiveLinq(otherFabric));
                        else
                            Console.WriteLine(fabric.FindExclusiveUsingExtensions(otherFabric));
                        break;
                    case 3:
                        if (methodChoice == 1)
                            Console.WriteLine(fabric.GetAverageTruckPriceLinq());
                        else
                            Console.WriteLine(fabric.GetAverageTruckPriceUsingExtensions());
                        break;
                    case 4:
                        if (methodChoice == 1)
                            Console.WriteLine(fabric.GetCodeLinq());
                        else
                            Console.WriteLine(fabric.GetCodeUsingExtensions());
                        break;
                    case 5:
                        if (methodChoice == 1)
                        {
                            Console.WriteLine(fabric.GetShowroomsLinq(showroomList));
                            Console.WriteLine("Автосалоны:");
                            showroomList.ForEach(s => Console.WriteLine($"Автосалон {s.CarClass}-класса, по продаже {s.Brand} в городе {s.City}."));
                        }
                        else
                        {
                            Console.WriteLine(fabric.GetShowroomsUsingExtensions(showroomList));
                            Console.WriteLine("Автосалоны:");
                            showroomList.ForEach(s => Console.WriteLine($"Автосалон {s.CarClass}-класса, по продаже {s.Brand} в городе {s.City}."));
                        }
                        break;
                    case 6:
                        if (methodChoice == 1)
                            Console.WriteLine(fabric.GroupByBrandLinq());
                        else
                            Console.WriteLine(fabric.GroupByBrand());
                        break;
                }
            }
        }

        static void ShowHashTableMenu(MyHashTableCollection<string, Car> myHashTable)
        {
            while (true)
            {
                Console.WriteLine("\nMyHashTableCollection меню:");
                Console.WriteLine("0. Вывести текущую коллекцию");
                Console.WriteLine("1. Найти грузовики");
                Console.WriteLine("2. Средняя цена авто");
                Console.WriteLine("3. Количество полноприводных внедорожников");
                Console.WriteLine("4. Группировка по марке");
                Console.WriteLine("5. Назад");
                int choice = EntryInt("Пункт", 0, 5);

                if (choice == 5)
                {
                    break;
                }

                Console.WriteLine("Выберите метод:");
                Console.WriteLine("1. LINQ");
                Console.WriteLine("2. Методы расширения");
                int methodChoice = EntryInt("Пункт", 1, 2);

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Текущая коллекция:");
                        PrintCollection(myHashTable);
                        break;
                    case 1:
                        if (methodChoice == 1)
                            Console.WriteLine(GetKeyTruckLinq(myHashTable));
                        else
                            Console.WriteLine(GetKeyTruckUsingExtensions(myHashTable));
                        break;
                    case 2:
                        if (methodChoice == 1)
                            Console.WriteLine(GetAvgPriceLinq(myHashTable));
                        else
                            Console.WriteLine(GetAvgPriceUsingExtensions(myHashTable));
                        break;
                    case 3:
                        if (methodChoice == 1)
                            Console.WriteLine(GetCountFWDLinq(myHashTable));
                        else
                            Console.WriteLine(GetCountFWDUsingExtensions(myHashTable));
                        break;
                    case 4:
                        if (methodChoice == 1)
                            Console.WriteLine(GroupByBrandLinq(myHashTable));
                        else
                            Console.WriteLine(GroupByBrandUsingMethods(myHashTable));
                        break;
                }
            }
        }
    }
}