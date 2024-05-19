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


        //public static void Menu()
        //{
        //    Random rand = new Random();
        //    LinkList<Car> list1 = new LinkList<Car>();
        //    LinkList<Car> list2 = new LinkList<Car>();
        //    bool listExist = false;




        //    while (true)
        //    {
        //        Console.ForegroundColor = ConsoleColor.DarkRed;
        //        Console.WriteLine("Меню");
        //        Console.WriteLine("1. Создать список");
        //        Console.WriteLine("2. Добавить k элементов в начало списка");
        //        Console.WriteLine("3. Удалить элемент с заданным значением поля");
        //        Console.WriteLine("4. Удалить список");
        //        Console.WriteLine("5. Вывести список");
        //        Console.WriteLine("6. Клонировать");
        //        Console.WriteLine("7. Проверить на клонирование.");
        //        Console.WriteLine("8. Выход");
        //        Console.WriteLine("Выберите действие: ");
        //        Console.ResetColor();
        //        string input = Console.ReadLine();

        //        switch (input)
        //        {
        //            case "1":
        //                {
        //                    Console.WriteLine("Выберите метод создания списка:\n1)Рандомные объекты\n2)Заполнить вручную");
        //                    listExist = true;
        //                    int ans = EntryInt("метод создания списка:", 1, 2);
        //                    switch (ans)
        //                    {
        //                        case 1:
        //                            {
        //                                listExist = true;

        //                                int size = EntryInt("размер списка", 1);
        //                                list1 = LinkList<Car>.ListCreate(size);
        //                                Console.WriteLine("Созданный список:");
        //                                list1.PrintList();
        //                                break;
        //                            }

        //                        case 2:
        //                            {
        //                                int size = EntryInt("размер списка", 1);
        //                                for (int i = 0; i < size; i++)
        //                                {
        //                                    listExist = true;
        //                                    Console.WriteLine("Что вы хотите добавить?\n1) Автомобиль (базовый)\n2)Кроссовер\n3) Грузовик\n");
        //                                    ans = EntryInt("вариант", 1, 3);
        //                                    if(ans == 1)
        //                                    {
        //                                        Car added = new Car();
        //                                        added.Init();
        //                                        list1.AppEnd(added);
        //                                    }
        //                                    if (ans == 2)
        //                                    {
        //                                        SUV added = new SUV();
        //                                        added.Init();
        //                                        list1.AppEnd(added);
        //                                    }
        //                                    if (ans == 3)
        //                                    {
        //                                        Truck added = new Truck();
        //                                        added.Init();
        //                                        list1.AppEnd(added);
        //                                    }

        //                                }
        //                                Console.WriteLine("Созданный список:");
        //                                list1.PrintList();


        //                                break;
        //                            }

        //                        default:
        //                            break;
        //                    }
        //                    break;
        //                }

        //            case "2":
        //                // Добавление элемента в начало списка
        //                int k=EntryInt("Количество элементов для добавления",1);
        //                Console.WriteLine("Какие элементы вы хотите добавить?\n1)Случайные элементы 2)Ввести вручную\n ");
        //                int ansk = EntryInt("Номер варианта", 1, 2, "Такого варианта нет, повторите ввод!");
        //                listExist = true;
        //                if (ansk == 1)
        //                {
        //                    list1.AppRandK(k);
        //                }
        //                else
        //                {

        //                    list1.AppInpK(k);
        //                }
        //                Console.WriteLine("Созданный список:");
        //                list1.PrintList();
        //                break;
        //            case "3":
        //                Console.WriteLine("Введите имя поля для удаления");
        //                string propToDel = Console.ReadLine();
        //                Console.WriteLine("Введите значение поля для удаления");
        //                string strToDel = Console.ReadLine();

        //                Console.WriteLine("Список после удаления:");
        //                list1.DelListEl(propToDel, strToDel);
        //                list1.PrintList();
        //                break;
        //            case "4":
        //                // Удаление
        //                LinkList<Car>.DelList(list1);
        //                listExist = false;

        //                break;
        //            case "5":
        //                // Вывод списка
        //                list1.PrintList();
        //                break;
        //            case "6":
        //                // Клон списка
        //                list2 = list1.DeepCopy();
        //                Console.WriteLine("Cоздан клон!");
        //                list2.PrintList();
        //                break;
        //            case "7":
        //                Console.WriteLine("Изменяем первый элемент оригинального списка...");
        //                if (listExist == false)
        //                {
        //                    Console.WriteLine( "Списка не существует!");
        //                    break;
        //                }
        //                list1.beg.Data.RandomInit();
        //                Console.WriteLine("обновленный список:");    
        //                list1.PrintList();
        //                Console.WriteLine("Клон:");
        //                list2.PrintList();
        //                break;
        //            case "8":
        //                // Выход из программы
        //                return;
        //            default:
        //                Console.WriteLine("Неверный выбор. Попробуйте снова.");
        //                break;
        //        }
        //    }
        //}


        static void Main(string[] args)
        {
            HashMenu();
            //Menu.BinaryTreeMenu();

            //Menu();
            //Console.ReadKey();

            


            // Меню

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
    }
    }

