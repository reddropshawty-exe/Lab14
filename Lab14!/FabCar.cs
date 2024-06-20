using System;
using System.Collections.Generic;
using System.Linq;
using Lab10FINLIB;

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
                int worklen = rnd.Next(5, 10); // случайное число от 5 до 9
                List<Car> workshop = new List<Car>(worklen);
                for (int j = 0; j < worklen; j++)
                {
                    Car added = CreateRandomCar();
                    added.RandomInit();
                    workshop.Add(added);
                }
                fabric.Enqueue(workshop);
            }
        }

        public string PrintFab()
        {
            var result = "Все машины завода:\n";
            foreach (var workshop in fabric)
            {
                foreach (Car car in workshop)
                {
                    result += car.ToString() + "\n";
                }
            }
            return result;
        }

        public string FindPremiumLinq()
        {
            string[] premium = { "Bugatti", "Lamborgini" };
            var resPremium = fabric
                .SelectMany(workshop => workshop)
                .Where(car => premium.Contains(car.Brand))
                .Select(car => $"Премиальное авто {car.Brand} {car.Year} за {car.Cost} рублей");

            return string.Join("\n", resPremium);
        }

        public string FindPremiumUsingExtensions()
        {
            string[] premium = { "Bugatti", "Lamborgini" };
            var resPremium = fabric
                .SelectMany(workshop => workshop)
                .Where(car => premium.Contains(car.Brand))
                .Select(car => $"Премиальное авто {car.Brand} {car.Year} за {car.Cost} рублей");

            return string.Join("\n", resPremium);
        }

        public string FindExclusiveUsingExtensions(CarFabric otherFact)
        {
            var firstCars = fabric.SelectMany(workshop => workshop);
            var otherCars = otherFact.fabric.SelectMany(workshop => workshop);

            var exclusiveCars = firstCars.Except(otherCars);

            var result = exclusiveCars.Select(car => $"Эксклюзивный авто {car.Brand} {car.Year} за {car.Cost} рублей");

            return string.Join("\n", result);
        }

        public string FindExclusiveLinq(CarFabric otherFact)
        {
            var exclusiveCars = from workshop in fabric
                                from car in workshop
                                where !(from otherWorkshop in otherFact.fabric
                                        from otherCar in otherWorkshop
                                        select otherCar).Contains(car)
                                select $"Эксклюзивный авто {car.Brand} {car.Year} за {car.Cost} рублей";

            return string.Join("\n", exclusiveCars);
        }

        public string GetAverageTruckPriceUsingExtensions()
        {
            var averagePrice = fabric
                .SelectMany(workshop => workshop)
                .Where(car => car is Truck)
                .Average(car => car.Cost);

            return $"Cредняя цена грузовика - {averagePrice}";
        }

        public string GetAverageTruckPriceLinq()
        {
            var averagePrice = fabric
                .SelectMany(workshop => workshop)
                .Where(car => car is Truck)
                .Average(car => car.Cost);

            return $"Cредняя цена грузовика - {averagePrice}";
        }

        public string GetCodeLinq()
        {
            string[] lux = { "Bugatti", "Lamborgini", "Porche", "Audi" };
            var carCode = from workshop in fabric
                          from car in workshop
                          let code = $"{car.Brand.Substring(0, 3).ToUpper()}{car.Year}{car.Color.Substring(0, 4).ToUpper()}"
                          select $"{car.Brand} {car.Year} c кодом {code}";

            return string.Join("\n", carCode);
        }

        public string GetCodeUsingExtensions()
        {
            string[] lux = { "Bugatti", "Lamborgini", "Porche", "Audi" };
            var carCode = fabric
                .SelectMany(workshop => workshop)
                .Select(car => $"{car.Brand} {car.Year} c кодом {car.Brand.Substring(0, 3).ToUpper()}{car.Year}{car.Color.Substring(0, 4).ToUpper()}");

            return string.Join("\n", carCode);
        }

        public string GetShowroomsLinq(List<Showroom> company)
        {
            var res = from workshop in fabric
                      from car in workshop
                      join showroom in company
                      on car.Brand equals showroom.Brand
                      select $"Вы можете приобрести автомобиль {car.Color} {car.Brand} {car.Year} в автосалоне {showroom.CarClass}-класса в городе {showroom.City}";

            return string.Join("\n", res);
        }

        public string GetShowroomsUsingExtensions(List<Showroom> company)
        {
            var res = fabric
                .SelectMany(workshop => workshop)
                .Join(company,
                      car => car.Brand,
                      showroom => showroom.Brand,
                      (car, showroom) => $"Вы можете приобрести автомобиль {car.Color} {car.Brand} {car.Year} в автосалоне {showroom.CarClass}-класса в городе {showroom.City}");

            return string.Join("\n", res);
        }

        public string GroupByBrand()
        {
            var groupedCars = fabric
                .SelectMany(workshop => workshop)
                .GroupBy(car => car.Brand);

            var result = "";
            foreach (var group in groupedCars)
            {
                result += $"Марка: {group.Key}\n";
                result += $"{group.Count()}\n";
                foreach (var car in group)
                {
                    result += $"\t{car.Brand} {car.Year} за {car.Cost} рублей\n";
                }
            }
            return result;
        }

        public string GroupByBrandLinq()
        {
            var groupedCars = from workshop in fabric
                              from car in workshop
                              group car by car.Brand into g
                              select new
                              {
                                  Brand = g.Key,
                                  Cars = g.Select(car => $"{car.Brand} {car.Year} за {car.Cost} рублей")
                              };

            var result = "";
            foreach (var group in groupedCars)
            {
                result += $"Марка: {group.Brand}\n";
                foreach (var car in group.Cars)
                {
                    result += $"\t{car}\n";
                }
            }
            return result;
        }
    }

    public class Showroom
    {
        public string City;
        public string Brand;
        public string CarClass;

        Random rnd = new Random();

        public Showroom()
        {
            string[] brands = { "Volkswagen", "Skoda", "Audi", "Porshe", "Bugatti", "Lamborgini" };
            string[] cities = { "Москва", "Санкт-Петербург", "Пермь", "Екатеринбург" };

            City = cities[rnd.Next(cities.Length)];
            Brand = brands[rnd.Next(brands.Length)];
            CarClass = GetCarClass(Brand);
        }

        public Showroom(string brand)
        {
            string[] brands = { "Volkswagen", "Skoda", "Audi", "Porshe", "Bugatti", "Lamborgini" };
            string[] cities = { "Москва", "Санкт-Петербург", "Пермь", "Екатеринбург" };

            City = cities[rnd.Next(cities.Length)];
            Brand = brand;
            CarClass = GetCarClass(brand);
        }

        private string GetCarClass(string brand)
        {
            var carClasses = new Dictionary<string, string>()
            {
                { "Volkswagen", "Эконом" },
                { "Skoda", "Эконом" },
                { "Audi", "Бизнес" },
                { "Porshe", "Бизнес" },
                { "Bugatti", "Премиум" },
                { "Lamborgini", "Премиум" }
            };

            return carClasses.ContainsKey(brand) ? carClasses[brand] : "Неизвестный класс";
        }
    }
}