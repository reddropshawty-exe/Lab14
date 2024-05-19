//using System;
//using Lab10FINLIB;

//namespace Lab12Tusk
//{


//    internal class HSTable<TK, TV> where TK : ICloneable, IInit, new()
//    {
//        private KeyValue<TK, TV>[] table;
//        private bool[] flags;
//        private int count;
//        private double fillRatio;

//        public int Count => count;
//        public int Capacity => table.Length;

//        public HSTable(int length = 10, double fillRatio = 0.72)
//        {
//            table = new KeyValue<TK, TV>[length];
//            flags = new bool[length];
//            this.fillRatio = fillRatio;
//        }

//        public bool Contains(TK key)
//        {
//            int index = GetIndex(key);
//            int start = index;
//            while (table[index] != null)
//            {
//                if (table[index].Key.Equals(key))
//                {
//                    return true;
//                }
//                index = (index + 1) % Capacity;
//                if (index == start)
//                {
//                    return false; // Места больше нет
//                }
//            }
//            return false;
//        }

//        public void DelEl(TK key)
//        {
//            int index = GetIndex(key);
//            int start = index;
//            while (table[index] != null)
//            {
//                if (table[index].Key.Equals(key))
//                {
//                    table[index] = null;
//                    flags[index] = true;
//                    count--;
//                    return;
//                }
//                index = (index + 1) % Capacity;
//                if (index == start)
//                {
//                    return; // Места больше нет
//                }
//            }
//        }

//        public void AddItem(TK key, TV value)
//        {
//            if ((double)count / Capacity > fillRatio)
//            {
//                KeyValue<TK, TV>[] temp = table.Clone() as KeyValue<TK, TV>[];
//                table = new KeyValue<TK, TV>[temp.Length * 2];
//                flags = new bool[table.Length];
//                count = 0;
//                foreach (var kv in temp)
//                {
//                    if (kv != null)
//                    {
//                        AddData(kv.Key, kv.Value);
//                    }
//                }
//            }
//            AddData(key, value);
//        }

//        public void PrintTable()
//        {
//            if (table.Length == 0 || count == 0)
//            {
//                Console.WriteLine("Таблица пуста!");
//                return;
//            }

//            for (int i = 0; i < table.Length; i++)
//            {
//                if (table[i] != null)
//                {
//                    Console.WriteLine($"{i}: {table[i].Key.ToString()}, {table[i].Value.ToString()}");
//                }
//            }
//        }

//        private int GetIndex(TK key)
//        {
//            return Math.Abs(key.GetHashCode()) % Capacity;
//        }

//        private void AddData(TK key, TV value)
//        {
//            int index = GetIndex(key);
//            while (table[index] != null)
//            {
//                index = (index+1) % Capacity;
//            }
//            table[index] = new KeyValue<TK, TV>(key, value);
//            count++;
//        }
//    }
//}
