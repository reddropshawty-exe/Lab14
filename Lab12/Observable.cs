using System;
using System.Collections.Generic;
using Lab10FINLIB;

namespace Lab12Tusk
{

    // Класс для передачи данных события
    public class CollectionHandlerEventArgs : EventArgs
    {
        public object Key { get; }
        public object Value { get; }
        public string Action { get; }

        public CollectionHandlerEventArgs(object key, object value, string action)
        {
            Key = key;
            Value = value;
            Action = action;
        }

        public override string ToString()
        {
            return $"Action: {Action}, Key: {Key}, Value: {Value}";
        }
    }



    //Создаем коллекцию
    public class MyObservableCollection<TK, TV> : MyHashTableCollection<TK, TV> where TV : ICloneable, IInit, new()
    {
        //список ключей 
        private readonly List<TK> keys;

        public string name;

        //конструктор
        public MyObservableCollection(int length = 10, double fillRatio = 0.72) : base(length, fillRatio)
        {
            keys = new List<TK>();
        }

        // Делегат и события для уведомления об изменениях в коллекции
        public delegate void CollectionChangeHandler(object source, CollectionHandlerEventArgs args);

        // События для изменения количества и ссылок в коллекции, используют одинаковый тип делегата принимающий одинаковые аргументы
        public event CollectionChangeHandler CollectionCountChanged;
        public event CollectionChangeHandler CollectionReferenceChanged;

        // Метод вызывающий событие
        protected void OnCollectionCountChanged(TK key, TV value, string action)
        {
            //запускаем все делегированные методы
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(key, value, action));
        }

        // Метод для уведомления подписчиков о изменении ссылки в коллекции
        protected void OnCollectionReferenceChanged(TK key, TV value, string action)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(key, value, action));
        }



        // Добавление элемента в коллекцию вызывает событие
        public new void Add(TK key, TV value)
        {
            base.Add(key, value);
            if (!keys.Contains(key))
            {
                keys.Add(key);
            }
            OnCollectionCountChanged(key, value, "add");
        }

        // Удаление элемента из коллекции
        public new bool Remove(TK key)
        {
            if (TryGetValue(key, out TV value))
            {
                bool result = base.Remove(key);
                if (result)
                {
                    keys.Remove(key);
                    OnCollectionCountChanged(key, value, "remove");
                }
                return result;
            }
            return false;
        }

        // Очистка коллекции
        public new void Clear()
        {
            base.Clear();
            keys.Clear();
            OnCollectionCountChanged(default, default, "clear");
        }

        // Итератор для доступа к элементам коллекции
        public new IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            foreach (TK key in keys)
            {
                yield return new KeyValuePair<TK, TV>(key, this[key]);
            }
        }

        // Индексатор для изменения элементов коллекции
        public new TV this[TK key]
        {
            get => base[key];
            set
            {
                base[key] = value;
                OnCollectionReferenceChanged(key, value, "update");
            }
        }

        // Свойство Length для получения текущего количества элементов коллекции
        public int Length => keys.Count;
    }





    public class Journal
    {
        public List<JournalEntry> notes = new List<JournalEntry>();

        public void AddEntry(object source, CollectionHandlerEventArgs args)
        {
            notes.Add(new JournalEntry(source, args));
        }

        public void PrintJournal()
        {
            foreach (var note in notes)
            {
                Console.WriteLine(note);
            }
        }
    }

    public class JournalEntry
    {
        public string Name { get; }
        public string Type { get; }
        public string Data { get; }

        public JournalEntry(object source, CollectionHandlerEventArgs args)
        {
            Name = nameof(source).ToString();
            Type = args.Action;
            Data = $"Key: {args.Key}, Value: {args.Value}";
        }

        public override string ToString()
        {
            return $"Collection {Name} changed: {Type} - {Data}";
        }
    }
}
    
