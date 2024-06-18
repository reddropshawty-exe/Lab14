using System.Linq;
using Lab10FINLIB;
using System;
using System.Collections.Generic;

namespace Lab14_
{
    public class CarFabric
    {
        public Queue<List<Car>> fabric { get; set; }
        private Random rnd = new Random();

        private Car CreateRandomCar()
        {
            int carType = rnd.Next(4); // случайное число от 0 до 2
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

        public CarFabric(int length = 3)
        {
            fabric = new Queue<List<Car>>(length);
            for (int i = 0; i < length; i++)
            {
                int worklen = 5; // или rnd.Next(5, 10)
                List<Car> workshop = new List<Car>(worklen);
                for (int j = 0; j < worklen; j++)
                {
                    Car added = CreateRandomCar();
                    added.RandomInit();
                    workshop.Add(added);
                }
                this.fabric.Enqueue(workshop);
            }
        }


        public void PrintFab()
        {
            Console.WriteLine("Все машины завода:");
            foreach (var workshop in fabric)
            {
                foreach (Car car in workshop)
                {
                    Console.WriteLine(car.ToString());
                }
            }
        }

         //ВЫБОРКА WHERE
        //LINQ
        public void FindPremiumLinq()
        {
            string[] premium = new string[] {"Bugatti", "Lamborgini"};
            var resPremium = from workshop in fabric
                             from car in workshop
                             where premium.Contains(car.Brand)
                             select new { Brand = car.Brand, Year = car.Year, Cost = car.Cost };
            foreach (var item in resPremium)
            {
                Console.WriteLine($"Премиальное авто {item.Brand} {item.Year} за {item.Cost} рублей");
            }
        }
        //Extension
        public void FindPremiumUsingExtensions()
        {
            string[] premium = new string[] { "Bugatti", "Lamborgini" };
            var resPremium = fabric
                .SelectMany(workshop => workshop)
                .Where(car => premium.Contains(car.Brand))
                .Select(car => new { Brand = car.Brand, Year = car.Year, Cost = car.Cost });

            foreach (var item in resPremium)
            {
                Console.WriteLine($"Премиальное авто {item.Brand} {item.Year} за {item.Cost} рублей");
            }
        }

        //ОПЕРАЦИЯ ИСКЛЮЧЕНИЯ
        public void FindExclusiveUsingExtensions(CarFabric otherFact)
        {
            var firstCars = fabric.SelectMany(workshop => workshop);
            var otherCars = otherFact.fabric.SelectMany(workshop => workshop);

            var exclusiveCars = firstCars.Except(otherCars);
 
            foreach (var car in exclusiveCars)
            {
                Console.WriteLine($"Эксклюзивный авто {car.Brand} {car.Year} за {car.Cost} рублей");
            }
        }

        public void FindExclusiveLinq(CarFabric otherFact)
        {
            var exclusiveCars = from workshop in fabric
                                from car in workshop
                                where !(from otherWorkshop in otherFact.fabric
                                        from otherCar in otherWorkshop
                                        select otherCar).Contains(car)
                                select car;

            foreach (var car in exclusiveCars)
            {
                Console.WriteLine($"Эксклюзивный авто {car.Brand} {car.Year} за {car.Cost} рублей");
            }
        }


        //СРЕДНЯЯ ЦЕНА ГРУЗОВИКОВ, МЕТОД AVERAGE
        public void GetAverageTruckPriceUsingExtensions()
        {
            // Используем LINQ для вычисления средней цены для объектов типа Truck
            var averagePrice = fabric
                .SelectMany(workshop => workshop)
                .Where(car => car is Truck) // Фильтруем объекты, чтобы остались только Truck
                .Select(truck => ((Truck)truck).Cost) // Приводим к типу Truck и выбираем цену
                .Average(); // Вычисляем среднее значение цен

            Console.WriteLine($"Cредняя цена грузовика - {averagePrice}");
        }

        // Метод GetAverageTruckPrice, использующий LINQ-запрос
        public void GetAverageTruckPriceLinq()
        {
            var averagePrice = (from workshop in fabric
                                from car in workshop
                                where car is Truck
                                select ((Truck)car).Cost).Average();

            Console.WriteLine($"Cредняя цена грузовика - {averagePrice}");
        }


        //АРТИКУЛ МАШИНЫ, ИСПОЛЬЗУЯ LET
        public void GetCodeLinq()
        {
            string[] lux = new string[] { "Bugatti", "Lamborgini", "Porche", "Audi"};
            var carCode = from workshop in fabric
                       from car in workshop
                       let code = car.Brand.Substring(0, 3).ToUpper() + $"{car.Year}" + car.Color.Substring(0, 4).ToUpper()
                       select new { Brand = car.Brand, Year = car.Year, Cost = car.Cost, Code=code};
            foreach(var car in carCode)
            {
                Console.WriteLine($"{car.Brand} {car.Year} c кодом {car.Code}");
            }
        }

        // Метод GetCode, использующий методы расширения коллекции, нет let
        public void GetCodeUsingExtensions()
        {
            string[] lux = new string[] { "Bugatti", "Lamborgini", "Porche", "Audi" };
            var carCode = fabric
                .SelectMany(workshop => workshop)
                .Select(car => new
                {
                    Brand = car.Brand,
                    Year = car.Year,
                    Cost = car.Cost,
                    Code = car.Brand.Substring(0, 3).ToUpper() + $"{car.Year}" + car.Color.Substring(0, 4).ToUpper()
                });

            foreach (var car in carCode)
            {
                Console.WriteLine($"{car.Brand} {car.Year} c кодом {car.Code}");
            }
        }

        //НАЙТИ САЛОНЫ, JOIN
        public void GetShowroomsLinq(List<Showroom> company)
        {
            var res = from workshop in fabric
                      from car in workshop
                      join showroom in company
                      on car.Brand equals showroom.Brand
                      select new { Brand = car.Brand, Color = car.Color, Year = car.Year, Cost = car.Cost, City = showroom.City, CarClass = showroom.carClass};
            foreach(var item in res)
            {
                Console.WriteLine($"Вы можете приобрести автомобиль {item.Color + " " + item.Brand} {item.Year} в автосалоне {item.CarClass}-класса в городе {item.City}");
            }
        }

        // Метод GetShowrooms, использующий методы расширения коллекции
        public void GetShowroomsUsingExtensions(List<Showroom> company)
        {
            var res = fabric
                .SelectMany(workshop => workshop)
                .Join(company, car => car.Brand, showroom => showroom.Brand, (car, showroom) => new
                {
                    Brand = car.Brand,
                    Color = car.Color,
                    Year = car.Year,
                    Cost = car.Cost,
                    City = showroom.City,
                    CarClass = showroom.carClass
                });

            foreach (var item in res)
            {
                Console.WriteLine($"Вы можете приобрести автомобиль {item.Color + " " + item.Brand} {item.Year} в автосалоне {item.CarClass}-класса в городе {item.City}");
            }
        }

        //ГРУППИРОВКА ПО ГРУППАМ
        public void GroupByBrand()
        {
            var groupedCars = fabric
                .SelectMany(workshop => workshop)
                .GroupBy(car => car.Brand);


            foreach (var group in groupedCars)
            {
                Console.WriteLine($"Марка: {group.Key}");
                Console.WriteLine(group.Count());
                foreach (var car in group)
                {
                    Console.WriteLine($"\t{car.Brand} {car.Year} за {car.Cost} рублей");
                }
            }
        }

        // Метод GroupByBrand, использующий LINQ-запрос
        public void GroupByBrandLinq()
        {
            var groupedCars = from workshop in fabric
                              from car in workshop
                              group car by car.Brand;

            foreach (var group in groupedCars)
            {
                Console.WriteLine($"Марка: {group.Key}");
                foreach (var car in group)
                {
                    Console.WriteLine($"\t{car.Brand} {car.Year} за {car.Cost} рублей");
                }
            }
        }


    }


    //КЛАСС-САЛОН
    public class Showroom
    {
        public string City;
        public string Brand;
        public string carClass;

        Random rnd = new Random();
        public Showroom()
        {
            string[] brands = { "Volkswagen", "Skoda", "Audi", "Porshe", "Bugatti", "Lamborgini" };
            string[] cities = new string[] { "Москва", "Санкт-Петербург", "Пермь", "Екатеринбург" };
            City = cities[rnd.Next(cities.Length)];
            Brand = brands[rnd.Next(brands.Length)];
            carClass = carClasses[Brand];
        }

        public Showroom(string brand)
        {
            string[] brands = { "Volkswagen", "Skoda", "Audi", "Porshe", "Bugatti", "Lamborgini" };
            string[] cities = new string[] { "Москва", "Санкт-Петербург", "Пермь", "Екатеринбург" };
            City = cities[rnd.Next(cities.Length)];
            Brand = brand;
            carClass = carClasses[Brand];
        }

        private Dictionary<string, string> carClasses = new Dictionary<string, string>()
        {
            ["Volkswagen"] = "Эконом",
            ["Skoda"] = "Эконом",
            ["Audi"] = "Бизнес",
            ["Porshe"] = "Бизнес",
            ["Bugatti"] = "Премиум",
            ["Lamborgini"] = "Премиум",

        };

    }
}
