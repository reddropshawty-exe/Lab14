using System;
using Lab10FINLIB;
namespace Lab12Tusk
{
	public class MyCollection<TK, TV> : IDictionary<TK, TV> where TV : ICloneable, IInit, new()
    {
		private HSTable<TK, TV> table;

        public MyCollection()
        {
            table = new HSTable<TK, TV>();
        }



        public ICollection<TK> Keys => throw new NotImplementedException();

        public ICollection<TV> Values => throw new NotImplementedException();

        public int Count => table.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public TV this[TK key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Add(TK key, TV value)
        {
            table.AddItem(key, value);
        }

        public void Add(KeyValuePair<TK, TV> item)
        {
            Add(item.Key, item.Value);
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            foreach (var pair in table)
            {
                yield return new KeyValuePair<TK, TV>(pair.Key, pair.Value);
            }
        }

        public void Clear()
        {
            table = new HSTable<TK, TV>();
        }

        public bool Contains(KeyValuePair<TK, TV> item)
        {
            return ContainsKey(item.Key);
        }

        public bool ContainsKey(TK key)
        {
            return table.Contains(key);
        }

        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

 
        

        public bool Remove(TK key)
        {
            if (!ContainsKey(key))
            {
                return false;
            }
            table.DelEl(key);
            return true;
        }

        public bool Remove(KeyValuePair<TK, TV> item)
        {
            if (!Contains(item))
            {
                return false;
            }
            return Remove(item.Key);
        }

        public bool TryGetValue(TK key, out TV value)
        {
            int index = table.FindElem(key);
            if (index != -1)
            {
                value = table.table[index].Value;
                return true;
            }
            value = default;
            return false;
        }



        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

 