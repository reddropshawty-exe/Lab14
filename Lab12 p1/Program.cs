using System;
using System.Collections;
using Lab10FINLIB;
using Lab12Tusk;
namespace Lab12_p1
{





    class Program
    {
        static int EntryInt(string intElement = "число", int min = -100, int max = 100, string errorMessage = "Повторите ввод!")
        {
            while (true)
            {
                Console.WriteLine($"\nВведите {intElement}!");
                string s = Console.ReadLine();
                int n;
                bool res = int.TryParse(s, out n);
                if (res && n >= min && n <= max)
                {
                    return n;
                }
                Console.WriteLine(errorMessage);
            }
        }


        static void Main(string[] args)
        {
            MyObservableCollection<string, Car> collection1 = new MyObservableCollection<string, Car>(10);
            MyObservableCollection<string, Car> collection2 = new MyObservableCollection<string, Car>(10);

            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            collection1.CollectionCountChanged += journal1.AddEntry;
            //collection1.CollectionReferenceChanged += journal1.AddEntry;
            //collection1.CollectionReferenceChanged += journal2.AddEntry;
            collection2.CollectionReferenceChanged += journal2.AddEntry;

            // Внесение изменений в коллекции
            Random rnd = new Random();
            for (int i = 0; i < 5; i++)
            {
                Car newCar = new Car();
                newCar.RandomInit();
                string key = newCar.GetKey();
                collection1.Add(key, newCar);
                collection2.Add(key, newCar);
            }

            
            if (collection2.Length > 0)
            {
                foreach (var item in collection2)
                {
                    
                    Car updatedCar = new Car();
                    updatedCar.RandomInit();
                    var key = item.Key;
                    collection2[key] = updatedCar;
                }

            }

            // Вывод данных журналов
            Console.WriteLine("Journal 1:");
            journal1.PrintJournal();

            Console.WriteLine("Journal 2:");
            journal2.PrintJournal();

            Console.ReadKey();

        } 
        
        static double EntryDouble(string doubleElement = "число", double min = double.MinValue, double max = double.MaxValue, string errorMessage = "Повторите ввод!")
        {
            while (true)
            {
                Console.WriteLine($"\nВведите {doubleElement}!");
                string s = Console.ReadLine();
                double d;
                bool res = double.TryParse(s, out d);
                if (res && d >= min && d <= max)
                {
                    return d;
                }
                Console.WriteLine(errorMessage);
            }
        }

        static string EntryString(string message = "строку")
        {
            Console.WriteLine($"\nВведите {message}!");
            return Console.ReadLine();
        }


        public static void HashMenu()
        {
            Console.WriteLine("Создание хэш-таблицы:");
            int length = EntryInt("длину таблицы", 1);
            double fillRatio = EntryDouble("fillRatio (от 0 до 1)", 0, 1);
            HSTable<string, Car> hashtable = new HSTable<string, Car>(length, fillRatio);
            Console.WriteLine($"Хэш-таблица создана: длина = {hashtable.Capacity}");
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Проверить наличие элемента");
                Console.WriteLine("3. Удалить элемент");
                Console.WriteLine("4. Вывести хэш-таблицу");
                Console.WriteLine("5. Выйти");
                Console.WriteLine("6. заполнить автоматически");

                int choice = EntryInt("номер пункта меню", 1, 6);

                switch (choice)
                {
                    case 1:
                        // Добавление элемента
                        Console.WriteLine("\nДобавление элемента:");
                        Car added = new Car();
                        added.Init();
                        string key = added.GetKey();
                        Car value = added;
                        hashtable.AddItem(key, value);
                        Console.WriteLine($"Элемент ({key}, {value.ToString()}) добавлен в хэш-таблицу.");
                        break;

                    case 2:
                        // Проверка наличия элемента
                        Console.WriteLine("\nПроверка наличия элемента:");
                        string searchKey = EntryString("ключ");
                        if (hashtable.Contains(searchKey))
                        {
                            Console.WriteLine($"Элемент с ключом {searchKey} найден в хэш-таблице.");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент с ключом {searchKey} не найден в хэш-таблице.");
                        }
                        break;

                    case 3:
                        // Удаление элемента
                        Console.WriteLine("\nУдаление элемента:");

                        string deleteKey = EntryString("ключ для удаления");
                        Console.WriteLine("1.Удалить первый элемент с этим ключом\n2.Удалить все элементы с заданным ключом\n");
                        int ans = EntryInt("вариант", 1, 2);
                        if (ans == 1)
                        {
                            hashtable.DelEl(deleteKey);

                        }
                        else
                        {
                            hashtable.DelAllEl(deleteKey);

                        }
                        break;

                    case 4:
                        // Вывод хэш-таблицы
                        Console.WriteLine("\nХэш-таблица:");
                        hashtable.PrintTable();
                        break;

                    case 5:
                        // Выход из программы
                        Console.WriteLine("Программа завершена.");
                        return;

                    case 6:
                        for (int i = 0; i < hashtable.table.Length; i++)
                        {
                            if (hashtable.table[i] == null)
                            {
                                Random rnd = new Random();
                                int type = rnd.Next(0, 3);
                                Car addedCar;
                                Pair<string, Car> addedItem;
                                if (type == 1)
                                {
                                    addedCar = new Car();
                                    addedCar.RandomInit();
                                    addedItem = new Pair<string, Car>(addedCar.GetKey(), addedCar);
                                }

                                if (type == 2)
                                {
                                    SUV addedSUV = new SUV();
                                    addedSUV.RandomInit();
                                    addedItem = new Pair<string, Car>(addedSUV.GetKey(), addedSUV);
                                }

                                else
                                {
                                    Truck addedTruck = new Truck();
                                    addedTruck.RandomInit();
                                    addedItem = new Pair<string, Car>(addedTruck.GetKey(), addedTruck);
                                }

                                hashtable.table[i] = addedItem;
                                hashtable.AddCount();
                            }
                        }
                        Console.WriteLine("Таблица заполне!");
                        break;
                }
            }
        }

        public static void HashCollectMenu()
        {
            Console.WriteLine("Создание коллекции на основе хэш-таблицы:");
            int length = EntryInt("длину таблицы", 1);
            double fillRatio = EntryDouble("fillRatio (от 0 до 1)", 0, 1);
            MyHashTableCollection<string, Car> collection = new MyHashTableCollection<string, Car>(length, fillRatio);
            Console.WriteLine($"Коллекция создана: длина = {collection.Count}, fillRatio = {fillRatio}");

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Проверить наличие элемента");
                Console.WriteLine("3. Удалить элемент");
                Console.WriteLine("4. Вывести коллекцию");
                Console.WriteLine("5. Выйти");
                Console.WriteLine("6. Заполнить автоматически");
                Console.WriteLine("7. Получить значение по ключу");
                Console.WriteLine("8. Получить все ключи");
                Console.WriteLine("9. Получить все значения");
                Console.WriteLine("10. Попробовать получить значение по ключу");

                int choice = EntryInt("номер пункта меню", 1, 10);

                switch (choice)
                {
                    case 1:
                        // Добавление элемента
                        Console.WriteLine("\nДобавление элемента:");
                        Car car = new Car();
                        car.Init();
                        string key = car.GetKey();
                        collection.Add(key, car);
                        Console.WriteLine($"Элемент ({key}, {car.ToString()}) добавлен в коллекцию.");
                        break;

                    case 2:
                        // Проверка наличия элемента
                        Console.WriteLine("\nПроверка наличия элемента:");
                        string searchKey = EntryString("ключ");
                        if (collection.ContainsKey(searchKey))
                        {
                            Console.WriteLine($"Элемент с ключом {searchKey} найден в коллекции.");
                        }
                        else
                        {
                            Console.WriteLine($"Элемент с ключом {searchKey} не найден в коллекции.");
                        }
                        break;

                    case 3:
                        // Удаление элемента
                        Console.WriteLine("\nУдаление элемента:");
                        string deleteKey = EntryString("ключ для удаления");
                        Console.WriteLine("1. Удалить первый элемент с этим ключом\n2. Удалить все элементы с заданным ключом");
                        int ans = EntryInt("вариант", 1, 2);
                        if (ans == 1)
                        {
                            collection.Remove(deleteKey);
                            Console.WriteLine($"Элемент с ключом {deleteKey} удален из коллекции.");
                        }
                        else
                        {
                            while (collection.Remove(deleteKey)) { }
                            Console.WriteLine("Все элементы с заданным ключом удалены из коллекции.");
                        }
                        break;

                    case 4:
                        // Вывод коллекции
                        Console.WriteLine("\nКоллекция:");
                        foreach (var item in collection)
                        {
                            Console.WriteLine($"\nКЛЮЧ: {item.Key}\nЗНАЧЕНИЕ: {item.Value}\n");
                        }
                        break;

                    case 5:
                        // Выход из программы
                        Console.WriteLine("Программа завершена.");
                        return;

                    case 6:
                        // Автозаполнение коллекции
                        Random rnd = new Random();
                        for (int i = 0; i < length; i++)
                        {
                            int type = rnd.Next(0, 3);
                            Car newCar;
                            if (type == 0)
                            {
                                newCar = new Car();
                            }
                            else if (type == 1)
                            {
                                newCar = new SUV();
                            }
                            else
                            {
                                newCar = new Truck();
                            }
                            newCar.RandomInit();
                            string newKey = newCar.GetKey();
                            collection.Add(newKey, newCar);
                        }
                        Console.WriteLine("Коллекция заполнена автоматически.");
                        break;

                    case 7:
                        // Получение значения по ключу
                        Console.WriteLine("\nПолучение значения по ключу:");
                        string getKey = EntryString("ключ");
                        try
                        {
                            Car getValue = collection[getKey];
                            Console.WriteLine($"Значение по ключу {getKey}: {getValue}");
                        }
                        catch (KeyNotFoundException)
                        {
                            Console.WriteLine($"Ключ {getKey} не найден в коллекции.");
                        }
                        break;

                    case 8:
                        // Получение всех ключей
                        Console.WriteLine("\nВсе ключи:");
                        foreach (var k in collection.Keys)
                        {
                            Console.WriteLine(k);
                        }
                        break;

                    case 9:
                        // Получение всех значений
                        Console.WriteLine("\nВсе значения:");
                        foreach (var v in collection.Values)
                        {
                            Console.WriteLine(v);
                        }
                        break;

                    case 10:
                        // Попробовать получить значение по ключу
                        Console.WriteLine("\nПопробовать получить значение по ключу:");
                        string tryGetKey = EntryString("ключ");
                        if (collection.TryGetValue(tryGetKey, out Car tryGetValue))
                        {
                            Console.WriteLine($"Значение по ключу {tryGetKey}: {tryGetValue}");
                        }
                        else
                        {
                            Console.WriteLine($"Ключ {tryGetKey} не найден в коллекции.");
                        }
                        break;
                }
            }
        }


    }
}

