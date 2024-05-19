using System;
using System.Diagnostics.CodeAnalysis;

namespace Lab10FINLIB;

public class SUV : PassengerCar, IInit, GetKey
{
    public bool fwd;

    public bool Fwd { get; set; }
    public string OffroadType { get; set; }

    public string GetWd(SUV p)
    {
        if (p.Fwd==true) return "4x4";
        else return "2x4";
    }


    public SUV() : base()
    {
        RandomInit();
    }

    // Конструктор с параметрами
    public SUV(string brand, int year, string color, double cost, double groundClearance, int seats, double maxSpeed, bool fwd, string offroadType) : base(brand, year, color, cost, groundClearance, seats, maxSpeed)
    {
        this.Fwd = fwd;
        this.OffroadType = offroadType;
    }

    // Конструктор копирования
    public SUV(SUV otherSuvCar) : base(otherSuvCar)
    {
        Fwd = otherSuvCar.Fwd;
        OffroadType = otherSuvCar.OffroadType;
    }

    public SUV DeepCopy()
    {
        return new SUV(Brand, Year, Color, Cost, GroundClearance, Seats, MaxSpeed, Fwd, OffroadType);
    }


    public override void Show()
    {
        Console.WriteLine($"Внедорожник {Color} {Brand} {Year} года выпуска стоимостью в {Cost} рублей.\nДорожный просвет: {GroundClearance} мм. Тип бездорожья: {OffroadType}, Привод: {GetWd(this)} Количество мест: {Seats}. Макс. скорость: {MaxSpeed} км/ч.\n");
    }



    public override string ToString()
    {
        return $"Внедорожник {Color} {Brand} {Year} года";
    }

    public string GetKey()
    {
        string wd;
        if (fwd == true)
        {
            wd = "4X4";
        }
        else
        {
            wd = "2X4";
        }
        return $"внедорожник {wd} {Color} {Brand} {Year} года выпуска";
    }

    //ИНИЦИАЛИЗАЦИЯ
    public void Init()
    {
        base.Init();

        Console.WriteLine("Автомобиль полноприводный? \n1)Да\n2)Нет ");
        string ans = Console.ReadLine();
        if (ans=="1")
        {
            Fwd=true;
        }
        else
        {
            Fwd=false;
        }

        Console.WriteLine("Введите тип бездорожья");
        OffroadType=Console.ReadLine();
    }

    static string[] offroadTypes = { "грязь", "песок", "любая поверхность", "горная местность" };
    [ExcludeFromCodeCoverage]
    public void RandomInit()
    {
        base.RandomInit();
        Fwd = rnd.Next(2)==1;
        OffroadType= offroadTypes[rnd.Next(offroadTypes.Length)];

    }

    //Cравнения
    public override bool Equals(object obj)
    {
        SUV p = obj as SUV;
        if (p != null)
            return this.Brand == p.Brand && this.Year == p.Year && this.Color==p.Color && this.Cost == p.Cost && this.GroundClearance == p.GroundClearance && this.Fwd == p.Fwd && this.OffroadType==p.OffroadType;
        else
            return false;
    }

}

