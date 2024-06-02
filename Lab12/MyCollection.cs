//using System;
//using System.Collections;
//using Lab10FINLIB;
//namespace Lab12Tusk
//{
//    public class MyHashTableCollection<TK, TV> : ICollection<Pair<TK, TV>>, IEnumerable<Pair<TK, TV>>, IEnumerable where TV : ICloneable, IInit, new()
//    {
//        private HSTable<TK, TV> hashTable;

//        public MyHashTableCollection(int length = 10, double fillRatio = 0.72)
//        {
//            hashTable = new HSTable<TK, TV>(length, fillRatio);
//        }

//        public int Count => hashTable.Count;
//        public int Capacity => hashTable.Capacity;
//        public bool IsReadOnly => false;

//        public void Add(Pair<TK, TV> item)
//        {
//            hashTable.AddItem(item.Key, item.Value);
//        }

//        //очищаем - делаем хэш таблицу пустой, заменяя на новую
//        public void Clear()
//        {
//            hashTable = new HSTable<TK, TV>(hashTable.Capacity, hashTable.fillRatio);
//        }

//        //вызываем идентичную функцию для пары
//        public bool Contains(Pair<TK, TV> item)
//        {
//            return hashTable.Contains(item.Key);
//        }

//        //вызываем идентичную функцию для ключа
//        public bool Contains(TK item)
//        {
//            return hashTable.Contains(item);
//        }



//        public void CopyTo(Pair<TK, TV>[] array, int arrayIndex)
//        {
//            if (array == null) throw new ArgumentNullException(nameof(array));
//            if (arrayIndex < 0 || arrayIndex > array.Length) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
//            if (array.Length - arrayIndex < Count) throw new ArgumentException("The number of elements in the source array is greater than the available space from arrayIndex to the end of the destination array.");

//            int copied = 0;
//            foreach (var pair in hashTable)
//            {
//                array[arrayIndex + copied] = pair;
//                copied++;
//            }
//        }

//        // дублируеем метод удаления из хэш таблицы, для пары элементов.
//        public bool Remove(Pair<TK, TV> item)
//        {
//            if (hashTable.Contains(item.Key))
//            {
//                hashTable.DelEl(item.Key);
//                return true;
//            }
//            return false;
//        }

//        // дублируеем метод удаления из хэш таблицы, для значения ключа
//        public bool Remove(TK item)
//        {
//            if (hashTable.Contains(item))
//            {
//                hashTable.DelEl(item);
//                return true;
//            }
//            return false;
//        }


//        public IEnumerator<Pair<TK, TV>> GetEnumerator()
//        {
//            return hashTable.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }


//    }
//}