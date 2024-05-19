using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab10FINLIB;

    public class Truck : Car, IInit
    {
        private double loadCapicity;

        public double LoadCapacity
        {
            get { return loadCapicity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Грузоподъемность", value, "Значение грузоподъемности должно быть положительным");
                }
                loadCapicity=value;
            }
        }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public Car GetBase
        {

            get => new Car(Brand, Year, Color, Cost, GroundClearance);//возвращает объект базового класса

        }


    //КОНСТРУКТОРЫ

    public Truck() : base()
        {
            RandomInit();
        }

        public Truck(string brand, int year, string color, double cost, double groundClearance, double loadCapicity) : base(brand, year, color, cost, groundClearance)
        {
            this.loadCapicity=loadCapicity;
        }
        public Truck(Truck otherTruck) : base(otherTruck)
        {
            loadCapicity=otherTruck.loadCapicity;
        }

        //Сравнения
        public override bool Equals(object obj)
        {
            Truck p = obj as Truck;
            if (p != null)
                return this.Brand == p.Brand && this.Year == p.Year && this.Color==p.Color && this.Cost == p.Cost && this.GroundClearance == p.GroundClearance && this.loadCapicity == p.loadCapicity;
            else
                return false;
        }

        //Перегрузка метода

        public override void Show()
        {
            Console.WriteLine($"грузовик {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм. Грузоподъемность {LoadCapacity} тонн.\n");
        }

        public override string ToString()
        {
            return $"грузовик {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм. Грузоподъемность {LoadCapacity} тонн.";
        }

        //ИНИЦИАЛИЗАЦИЯ

        public void Init()
        {
            base.Init();

            loadCapicity = EntryDouble("максимальную грузоподъемность в тоннах", 5);
        }

        public void RandomInit()
        {
            base.RandomInit();
            loadCapicity= rnd.Next(1, 100);

        }

    public override int GetHashCode()
    {
        int hashCode = base.GetHashCode();
        hashCode = (hashCode) ^ Brand.GetHashCode();
        hashCode = (hashCode) ^ Year.GetHashCode();
        hashCode = (hashCode) ^ Color.GetHashCode();
        hashCode = (hashCode) ^ Cost.GetHashCode();
        hashCode = (hashCode) ^ GroundClearance.GetHashCode();
        hashCode = (hashCode) ^ LoadCapacity.GetHashCode();
        return hashCode;
    }



}



