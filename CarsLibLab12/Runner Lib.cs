using System;
namespace Lab10FINLIB;

    public class IdNumber
    {
        public int number;
        public IdNumber(int number)
        {
            this.number = number;
        }
        public override string ToString()
        {
            return number.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is IdNumber n)
            {
                return (this.number == n.number);
            }

            return false;
        }
    }

    public class Runner : IInit, ICloneable
    {
        private double _speed;
        private double _distance;

        public IdNumber id;

        public double EntryDouble(string message, double baseValue)
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
                value=baseValue;
            }
            return value;
        }

        public int EntryInt(string message, int baseValue)
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
                value=baseValue;
            }
            return value;
        }

        Random rnd = new Random();


        public void Init()
        {
            Speed = EntryDouble("Скорость Бегуна", 8);
            Distance = EntryDouble("Расстояние", 16);
            id = new IdNumber(EntryInt("Id", 0));
        }

        public void RandomInit()
        {
            _speed = rnd.Next(1, 20);
            _distance = rnd.Next(1, 10);
            id = new IdNumber(rnd.Next(1, 100));
        }





        //получение времени методом класса
        public static double GetTime(Runner m)
        {
            return Math.Round(m._distance / m._speed, 2);
        }
        // счетчик для класса
        public static int count;

        //УНАРНЫЕ ОПЕРАЦИИ
        public static Runner operator ++(Runner m)
        {
            m._distance = m._distance+0.1;
            return m;
        }

        public static Runner operator --(Runner m)
        {
            m._speed = m._speed - 0.05;
            return m;
        }

        //БИНАРНЫЕ ОПЕРАЦИИ
        public static Runner operator ^(Runner r1, double sp)
        {
            r1._speed += sp;
            return r1;
        }

        public static bool operator >(Runner r1, Runner r2)
        {
            if (r1._distance > r2._distance || (r1._distance == r2._distance && (Runner.GetTime(r1)> Runner.GetTime(r2))))
            {
                return true;
            }
            return false;
        }

        public static bool operator <(Runner r1, Runner r2)
        {
            if (r2._distance > r1._distance || (r2._distance == r1._distance && (Runner.GetTime(r2) > Runner.GetTime(r1))))
            {
                return true;
            }
            return false;
        }

        public static double operator -(Runner r1, Runner r2)
        {
            if (r1._distance + r2._distance < 15)
            {
                return -1;
            }

            //Находим время через которое встретятся
            double meetTime = 15 / (r1._speed + r2._speed);
            //Находим дистанцию до встречи
            double firstRunnerDistToMeet = r1._speed * meetTime;
            double secondRunnerDistToMeet = r2._speed * meetTime;
            //Проверяем точно ли каждый бегун пробежит такую дистанцию
            double extraDist;
            if (r1._distance > firstRunnerDistToMeet && r2._distance > secondRunnerDistToMeet)
            {
                return firstRunnerDistToMeet;
            }

            //если первый не добежит, считаем сколько нужно добежать второму
            if (r1._distance < r2._distance)
            {
                extraDist = firstRunnerDistToMeet - r1._distance;
                secondRunnerDistToMeet += extraDist;
                return firstRunnerDistToMeet;
            }

            //если второй не добежит, считаем сколько нужно добежать первому
            else
            {
                Runner rmin = new Runner(r2);
                extraDist = secondRunnerDistToMeet - r2._distance;
                firstRunnerDistToMeet += extraDist;
                return firstRunnerDistToMeet;
            }




        }

        //ОПЕРАЦИИ ПРИВЕДЕНИЯ ТИПОВ
        public static implicit operator double(Runner r1)
        {
            double currentTime = Runner.GetTime(r1);
            double newTime = currentTime * 0.95;
            double newSpeed = r1._distance / newTime;
            double speedBust = newSpeed - r1._speed;
            return Math.Round(speedBust, 2);
        }


        public static explicit operator string(Runner r1)
        {
            double time = Runner.GetTime(r1) * 3600;
            double hours = Math.Floor(time/3600);
            string normHours, normMinutes, normSeconds;
            if (hours < 10)
            {
                normHours = $"0{hours}:";
            }
            else
            {
                normHours = $"{hours}:";
            }
            time = time % 3600;
            double minutes = Math.Floor(time / 60);
            if (minutes < 10)
            {
                normMinutes = $"0{minutes}:";
            }
            else
            {
                normMinutes = $"{minutes}:";
            }
            double seconds = time % 60;
            if (seconds < 10)
            {
                normSeconds = $"0{Math.Floor(seconds)}";
            }
            else
            {
                normSeconds = $"{Math.Floor(seconds)}";
            }
            string timetable = normHours+normMinutes+normSeconds;
            return timetable;
        }

        //КОНСТРУКТОРЫ
        public Runner(double distance, double speed, int id)
        {
            Speed = speed;
            Distance = distance;
            this.id = new IdNumber(id);
            count++;
        }

        public Runner()
        {
            _speed = 13;
            _distance = 5;
            id = new IdNumber(0);
            count++;
        }

        public Runner(Runner m)
        {
            _speed = m._speed;
            _distance = m._distance;
            id = m.id;
            this.id.number = m.id.number;
            count++;
        }

        public double Speed
        {
            get { return _speed; }
            set
            {
                if (!((_distance>0 && _distance < 0.4 && value < 40) || (_distance < 2 && value < 20) || (_distance < 50 && value < 15)))
                {
                    Console.WriteLine("Такое соотношение скорости и времени невозможно! Будет заданно значение по умолчанию - 13 км/ч");
                    _speed = 13;
                }
                else
                {
                    _speed = value;
                }
            }

        }

        public double Distance
        {
            get { return _distance; }
            set
            {
                if (value>50)
                {
                    Console.WriteLine("Никто физически не сможет столько пробежать! Будет заданно значение по умолчанию - 50 км.");
                    _distance = 50;
                }
                else
                {
                    _distance = value;
                }
            }

        }


        //выводит инфу о времени строкой
        public void GetTimeInf()
        {
            Console.WriteLine($"Бегун пробежит за {Math.Round(Distance / Speed, 2)} !");
        }

        //возвращает значение времени
        public double GetTime()
        {
            return Math.Round(Distance / Speed, 2);
        }

        //печатает информацию о скорости и дистанции
        public void GetParams()
        {
            Console.WriteLine($"Бегун бежит {_distance} км. со скоростью {_speed} км/ч");
        }

        //возвращает скорость
        public double GetSpeed()
        {
            return _speed;
        }
        //возвращает дистанцию
        public double GetDistance()
        {
            return _distance;
        }
        //возвращает информацию о бегуне в формате строки
        public override string ToString()
        {
            return $"бегун {id.number} пробежит {_distance} км со скоростью {_speed} км/ч за {(string)this}";
        }

        public object Clone()
        {
            return new Runner(_distance, _speed, id.number);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }


