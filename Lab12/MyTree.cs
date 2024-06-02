using System;
using System.Collections.Generic;
using Lab10FINLIB;

namespace Lab12Tusk
{
    public static class Menu
    {
        public static void BinaryTreeMenu()
        {
            Console.WriteLine("Введите размер дерева:");
            int treeSize;
            while (!int.TryParse(Console.ReadLine(), out treeSize) || treeSize <= 0)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите положительное целое число:");
            }

            Tree<Car> originalTree = new Tree<Car>(treeSize);
            Tree<Car> clonedTree = originalTree.Clone();
            PointTree<Car> root = originalTree.Root;
            int choice = -1;
            while (choice != 8)
            {
                Console.WriteLine("1. Печать оригинального дерева");
                Console.WriteLine("2. Печать клонированного дерева");
                Console.WriteLine("3. Найти высоту оригинального дерева");
                Console.WriteLine("4. Найти высоту клонированного дерева");
                Console.WriteLine("5. Преобразовать оригинальное дерево в дерево поиска");
                Console.WriteLine("6. Добавить элемент в идеально-сбалансированное дерево(клон)");
                Console.WriteLine("7. Удалить копированное дерево");
                Console.WriteLine("8. Выход");

                bool ok = int.TryParse(Console.ReadLine(), out choice);
                if (!ok || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, выберите число от 1 до 8:");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        originalTree.ShowTree(root, treeSize);
                        break;
                    case 2:
                        clonedTree.ShowTree(clonedTree.Root, treeSize);
                        break;
                    case 3:
                        Console.WriteLine("Высота оригинального дерева = " + originalTree.TreeDeep(root));
                        break;
                    case 4:
                        Console.WriteLine("Высота клонированного дерева = " + clonedTree.TreeDeep(clonedTree.Root));
                        break;
                    case 5:
                        PointTree<Car> searchTreePoint = originalTree.first(originalTree.Root.Data);
                        Tree<Car> searchTree = new Tree<Car>(searchTreePoint);
                        searchTree.MakeSearch(originalTree.Root);
                        searchTree.ShowTree(searchTree.Root, treeSize);
                        break;
                    case 6:
                        Console.WriteLine("добавлен случайный элемент");
                        Random rnd = new Random();
                        int type = rnd.Next(0, 3);
                        Car addedCar;
                        if (type == 1)
                        {
                            addedCar = new Car();
                            addedCar.RandomInit();
                        }
                        else if (type == 2)
                        {
                            SUV addedSUV = new SUV();
                            addedSUV.RandomInit();
                            addedCar = addedSUV;
                        }
                        else
                        {
                            Truck addedTruck = new Truck();
                            addedTruck.RandomInit();
                            addedCar = addedTruck;
                        }
                        clonedTree.AddBalanced(addedCar);
                        treeSize++;
                        break;
                    case 7:
                        Console.WriteLine("Удаление дерева...");

                        clonedTree.DeleteTree();
                        Console.WriteLine("Дерево удалено.");
                        break;
                    case 8:
                        Console.WriteLine("Выход...");
                        break;
                }
            }
        }
    }

    public class PointTree<T> where T : IComparable
    {
        public T? Data;
        public PointTree<T>? Left { get; set; }
        public PointTree<T>? Right { get; set; }

        public PointTree()
        {
            this.Data = default(T);
            this.Left = null;
            this.Right = null;
        }

        public PointTree(T el)
        {
            this.Data = el;
            this.Left = null;
            this.Right = null;
        }

        public PointTree<T> Clone()
        {
            PointTree<T> newNode = new PointTree<T>(this.Data);
            if (this.Left != null)
                newNode.Left = this.Left.Clone();
            if (this.Right != null)
                newNode.Right = this.Right.Clone();
            return newNode;
        }

        public override string? ToString()
        {
            return Data?.ToString() ?? "";
        }

        public int CompareTo(PointTree<T> other)
        {
            return Data.CompareTo(other.Data);
        }
    }

    public class Tree<T> where T : IComparable, IInit, new()
    {
        public PointTree<T>? Root = null;
        int count = 0;
        public int Count => count;

        public Tree(int length)
        {
            Root = MakeTree(length, Root);
            count = length;
        }

        public Tree(PointTree<T> t)
        {
            Root = t;
        }

        public Tree<T> Clone()
        {
            return new Tree<T>(Root.Clone());
        }

        public PointTree<T>? MakeTree(int length, PointTree<T>? point)
        {
            T Data = new T();
            Data.RandomInit();

            PointTree<T> newItem = new PointTree<T>(Data);
            if (length == 0)
            {
                return null;
            }

            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left);
            newItem.Right = MakeTree(nr, newItem.Right);
            return newItem;
        }

        /*рекурсивная функция для печати дерева по уровням с обходом слева направо*/
        public void ShowTree(PointTree<T>? node, int indent = 0, int level = 0)
        {
            if (node != null)
            {
                ShowTree(node.Right, indent + 4, level + 1);

                // смена цвета на основе уровня
                Console.ForegroundColor = GetColorForLevel(level);

                if (indent > 0)
                {
                    Console.Write(new string(' ', indent));
                }

                Console.WriteLine(node.Data); // печать узла

                Console.ResetColor(); // сброс цвета к стандартному

                ShowTree(node.Left, indent + 4, level + 1);
            }
        }

        private ConsoleColor GetColorForLevel(int level)
        {
            // Используем разные цвета для уровней
            ConsoleColor[] colors = {
                ConsoleColor.Red,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.Yellow,
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.White,
                ConsoleColor.Gray
            };

            // Возвращаем цвет на основе уровня, используя остаток от деления
            return colors[level % colors.Length];
        }

        public PointTree<T> first(T tr)
        {
            return new PointTree<T>(tr);
        }

        public void AddBalanced(T data)
        {
            List<T> elements = new List<T>();
            InOrderTraversal(Root, elements);
            elements.Add(data);
            elements.Sort();
            Root = BuildBalancedTree(elements, 0, elements.Count - 1);
        }

        private void InOrderTraversal(PointTree<T>? node, List<T> elements)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, elements);
                elements.Add(node.Data);
                InOrderTraversal(node.Right, elements);
            }
        }

        private PointTree<T> BuildBalancedTree(List<T> elements, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            PointTree<T> node = new PointTree<T>(elements[mid]);
            node.Left = BuildBalancedTree(elements, start, mid - 1);
            node.Right = BuildBalancedTree(elements, mid + 1, end);
            return node;
        }

        public void Run(PointTree<T> t)
        {
            if (t != null)
            {
                Run(t.Left);
                Run(t.Right);
            }
        }

        public int TreeDeep(PointTree<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                return 1 + Math.Max(TreeDeep(root.Left), TreeDeep(root.Right));
            }
        }

        public void MakeSearch(PointTree<T> root)
        {
            if (root != null)
            {
                MakeSearch(root.Left);
                this.Add(root.Data);
                MakeSearch(root.Right);
                this.Add(root.Data);
            }
        }

        public void Add(T data)
        {
            PointTree<T>? point = Root;
            PointTree<T>? current = null;
            bool isExist = false;
            while (point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0)
                {
                    isExist = true;
                }
                else
                {
                    if (point.Data.CompareTo(data) > 0)
                    {
                        point = point.Right;
                    }
                    else
                    {
                        point = point.Left;
                    }
                }
            }
            if (isExist)
            {
                return;
            }
            PointTree<T> newPoint = new PointTree<T>(data);
            if (current != null && current.Data.CompareTo(data) < 0)
            {
                current.Left = newPoint;
            }
            else if (current != null)
            {
                current.Right = newPoint;
            }
            count++;
        }

        public void DeleteTree()
        {
            Root = null;
            count = 0;
        }
    }
}
