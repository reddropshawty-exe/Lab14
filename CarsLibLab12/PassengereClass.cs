using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab10FINLIB;

    public class PassengerCar : Car, IInit
    {

        //ПАРАМЕТРЫ
        private int seats;

        private double maxSpeed;

        //аксессоры

        public int Seats
        {
            get { return seats; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Seats", value, "Seats must be greater than or equal to 1");
                }
                seats = value;
            }
        }

        public double MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                if (value < 0 || value>300)
                {
                    throw new ArgumentOutOfRangeException("Скорость", value, "Скорость должна быть от 0 до 300 км/ч");
                }
                maxSpeed=value;

            }
        }

       //КОНСТРУКТОРЫ

        public PassengerCar() : base()
        {
            RandomInit();
        }

        // Конструктор с параметрами
        public PassengerCar(string brand, int year, string color, double cost, double groundClearance, int seats, double maxSpeed) : base(brand, year, color, cost, groundClearance)
        {
            this.Seats = seats;
            this.MaxSpeed = maxSpeed;
        }

        // Конструктор копирования
        public PassengerCar(PassengerCar otherPassengerCar) : base(otherPassengerCar)
        {
            Seats = otherPassengerCar.Seats;
            MaxSpeed = otherPassengerCar.MaxSpeed;
        }

        //ПЕРЕГРУЗКА Show()
        [ExcludeFromCodeCoverage]
        public override void Show()
        {
            Console.WriteLine($"Пассажирский автомобиль {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм. Количество мест: {Seats}. Макс. скорость: { maxSpeed}.\n" );
        }

        
        public override string ToString()
        {
            return $"Пассажирский автомобиль {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм. Количество мест: {Seats}. Макс. скорость: { maxSpeed}.";
        }

        //ИНИЦИАЛИЗАЦИИ
        [ExcludeFromCodeCoverage]
        public void Init()
        {
            base.Init();

            seats = EntryInt("число мест в автомобиле", 5);

            maxSpeed = EntryDouble("максимальную скорость", 150);
        }
        [ExcludeFromCodeCoverage]
        public void RandomInit()
        {
            base.RandomInit();
            Seats = rnd.Next(1, 8);
            MaxSpeed=rnd.Next(30, 290);
        }

        //СРАВНЕНИЯ
        public override bool Equals(object obj)
        {
            if (obj is PassengerCar)
            {
                PassengerCar otherPassengerCar = (PassengerCar)obj;
                return base.Equals(otherPassengerCar) && Seats.Equals(otherPassengerCar.Seats) && MaxSpeed.Equals(otherPassengerCar.MaxSpeed);
            }
            return false;
        }
    }


