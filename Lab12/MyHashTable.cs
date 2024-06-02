using System;
using static System.Net.Mime.MediaTypeNames;
using Lab10FINLIB;
namespace Lab12Tusk
{


    public class Pair<TK, TV> where TV : ICloneable, IInit, new()
    {
        public TK Key { get; set; }
        public TV Value { get; set; }

        public Pair(TK key, TV value)
        {
            Key = key;
            Value = value;
        }
    }

    public class HSTable<TK, TV> where TV : ICloneable, IInit, new()
    {
        public Pair<TK, TV>[] table;
        bool[] flags;
        int count;
        public double fillRatio;
        int newElSince = 0;

        public int Count => count;
        public int Capacity => table.Length;

        public HSTable(int length = 10, double fillRatio = 0.72)
        {
            table = new Pair<TK, TV>[length];
            flags = new bool[length];
            this.fillRatio = fillRatio;
        }

        public IEnumerator<Pair<TK, TV>> GetEnumerator()
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    yield return table[i];
                }
            }
        }
    

    public void AddCount()
        {
            count++;
        }//Добавить 1 к количеству элементов




        public bool Contains(TK data)
        {
            return !(FindElem(data) < 0);
        }//Проверить содержит ли элемент

        public void DelEl(TK data, bool InOther = false)
        {
            if (!Contains(data))
            {
                if (!InOther)
                {
                    Console.WriteLine("Этого элемента и так нет в таблице");
                }

                return;
            }
            else
            {
                int ind = GetIndex(data);//Изначальный индекс удаляемого элемента
                int deleted = FindElem(data);//Реальный индекс удаляемого

                //Удаляем элемент
                table[deleted] = default;
                flags[deleted] = true;

                if(ind == deleted)
                {
                    TurnOnPlace(deleted);
                }

                count--;
                Console.WriteLine($"Элемент с ключем{data.ToString()} удален из хэш-таблицы. Он находился на  {deleted} месте") ;
                return;
            }
        }

        void TurnOnPlace(int intSearch)
        {
            for (int i = intSearch; i<table.Length; i++)
            {
                if (table[i]!=null && GetIndex(table[i].Key) == intSearch)
                {
                    table[intSearch] = table[i];
                    table[i] = default;
                    Console.WriteLine($"При удалении элемент {table[intSearch].Key} был перетащен на {FindElem(table[intSearch].Key)}");
                    break;
                } 
            }
        }

        public void DelAllEl(TK data)
        {
            //удаляем, пока не удалим все
            while (Contains(data))
            {
                DelEl(data, true);
            }
            Console.WriteLine($"Элементы {data.ToString()} удалены из хэш-таблицы.");
            return;
        }

        public void AddItem(TK key, TV value)
        {
            if ((double)count / Capacity > fillRatio)
            {
                newElSince = Capacity;

                bool[] tempFlags = (bool[])flags.Clone();
                flags = new bool[flags.Length * 2];

                for (int i = 0; i < tempFlags.Length; i++)
                {
                    flags[i] = tempFlags[i];
                }

                Pair<TK, TV>[] temp = (Pair<TK, TV>[])table.Clone();
                table = new Pair<TK, TV>[temp.Length * 2];
                count = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    //AddData(temp[i].Key, temp[i].Value);
                    table[i] = temp[i];
                }
            }
            AddData(key, value);
        }

        public void PrintTable()
        {
            if (table.Length == 0 || count == 0)
            {
                Console.WriteLine("Таблица пуста!");
                return;
            }

            for (int i = 0; i < table.Length; i++)
{
    if (table[i] != null)
    {
        // Установка цвета для ключа
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{i}: КЛЮЧ: {table[i].Key.ToString()}");
        
        // Сброс цвета к значению по умолчанию
        Console.ResetColor();
        
        Console.Write("\n");

        // Установка цвета для значения
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"ЗНАЧЕНИЕ: {table[i].Value.ToString()}");

        // Сброс цвета к значению по умолчанию
        Console.ResetColor();

        Console.Write("\n");
    }
}
        }

        public int GetIndex(TK key)
        {
            return Math.Abs(key.GetHashCode()) % Capacity;
        }


        void AddData(TK key, TV value)
        {
            if ((key == null) || (value == null)) return;
            int index = GetIndex(key);
            int current = index;
            if (table[index] == null)
            {
                table[index] = new Pair<TK, TV>(key, value);
                count++;
                return;
            }

            if (table[index] != null)
            {

                while (current < table.Length && table[current] != null)
                {
                    current++;
                }

                if (current == table.Length)
                {
                    current = 0;
                    while (table[current] != null && current < index)
                    {
                        current++;
                    }
                    if (current == index)
                    {
                        throw new Exception("В таблице нет места!");
                    }
                }

            }

            if (FindElem(key) != -1 && table[FindElem(key)].Value.Equals(value))
            {
                Console.WriteLine("Этот элемент и так существует в хэш таблице и не может быть добавлен!");
                return;
            }
            table[current] = new Pair<TK, TV>(key, value); ;
            count++;

        }
        
        //public int FindElem(TK key)
        //{
        //    int index = GetIndex(key);
        //    int current = index;
        //    if ((table[index] == null && flags[index] == false) || key == null)
        //    {
        //        //Console.WriteLine("Такого элемента нет в таблице!");
        //        return -1;
        //    }
        //    if (table[index] != null && table[index].Key.Equals(key))
        //    {

        //        return index;
        //    }

        //    else
        //    {
        //        while (current < table.Length && ( (table[current] != null && !table[current].Key.Equals(key)) || (table[current] == null && !flags[current] == false) ))
        //        {
        //            current++;
        //        }

        //        if (current < table.Length&& flags[current] == false && current < newElSince)
        //        {
        //            return (-1);
        //        }

        //        if (current == table.Length || (flags[current] == false && current >= newElSince))
        //        {
        //            current = 0;
        //            while (current < index && (table[current] != null && !table[current].Key.Equals(key)))
        //            {
        //                current++;
        //            }
        //            if (current == index)
        //            {
        //                //Console.WriteLine("Такого элемента нет в таблице!");
        //                return -1;
        //            }
        //        }
        //        return current;
        //    }
        //}



        public int FindElem(TK key)
        {
            int index = GetIndex(key);
            int current = index;

            if (key == null)
            {
                return -1;
            }

            if (table[index] != null && table[index].Key.Equals(key))
            {
                return index;
            }

            while (current < table.Length && (table[current] == null || !table[current].Key.Equals(key)))
            {
                current++;
            }

            if (current == table.Length)
            {
                current = 0;
                while (current < index && (table[current] == null || !table[current].Key.Equals(key)))
                {
                    current++;
                }

                if (current == index)
                {
                    return -1;
                }
            }

            return table[current] != null && table[current].Key.Equals(key) ? current : -1;
        }




        }

    

}
