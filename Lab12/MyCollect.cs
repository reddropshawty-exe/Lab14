using System;
using Lab10FINLIB;

namespace Lab12Tusk
{
    public class MyHashTableCollection<TK, TV> : IDictionary<TK, TV> where TV : ICloneable, IInit, new()
    {
        private readonly HSTable<TK, TV> hashtable;



        public MyHashTableCollection(int length = 10, double fillRatio = 0.72)
        {
            hashtable = new HSTable<TK, TV>(length, fillRatio);
        }

        public TV this[TK key]
        {
            get
            {
                int index = hashtable.FindElem(key);
                if (index == -1)
                {
                    throw new KeyNotFoundException();
                }
                return hashtable.table[index].Value;
            }
            set
            {
                int index = hashtable.FindElem(key);
                if (index == -1)
                {
                    hashtable.AddItem(key, value);
                }
                else
                {
                    hashtable.table[index].Value = value;
                }
            }
        }

        public ICollection<TK> Keys
        {
            get
            {
                List<TK> keys = new List<TK>();
                foreach (Pair<TK, TV> pair in hashtable)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }

        public ICollection<TV> Values
        {
            get
            {
                List<TV> values = new List<TV>();
                foreach (Pair<TK, TV> pair in hashtable)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }

        public int Count => hashtable.Count;
        public int Capacity => hashtable.Capacity;

        public bool IsReadOnly => false;

        public void Add(TK key, TV value)
        {
            hashtable.AddItem(key, value);
        }

        public void Add(KeyValuePair<TK, TV> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            foreach (TK key in Keys)
            {
                hashtable.DelAllEl(key);
            }
        }

        public bool Contains(KeyValuePair<TK, TV> item)
        {
            return ContainsKey(item.Key) && this[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(TK key)
        {
            return hashtable.Contains(key);
        }

        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            foreach (Pair<TK, TV> pair in hashtable)
            {
                array[arrayIndex++] = new KeyValuePair<TK, TV>(pair.Key, pair.Value);
            }
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            foreach (Pair<TK, TV> pair in hashtable)
            {
                yield return new KeyValuePair<TK, TV>(pair.Key, pair.Value);
            }
        }

        public bool Remove(TK key)
        {
            int index = hashtable.FindElem(key);
            if (index != -1)
            {
                hashtable.DelEl(key);
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<TK, TV> item)
        {
            return Contains(item) && Remove(item.Key);
        }

        public bool TryGetValue(TK key, out TV value)
        {
            int index = hashtable.FindElem(key);
            if (index != -1)
            {
                value = hashtable.table[index].Value;
                return true;
            }
            value = default(TV);
            return false;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

