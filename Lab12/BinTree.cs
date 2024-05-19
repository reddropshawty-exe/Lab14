//using System;
//using Lab10FINLIB;
//namespace Lab12Tusk;
//public class TreePoint<T> where T: IComparable
//{
//    public T? data;
//    TreePoint<T> right;
//    TreePoint<T> left;
//    public TreePoint<T> Right
//    {
//        get => right;
//        set => right = value;
//    }

//    public TreePoint<T> Left
//    {
//        get => left;
//        set => left = value;
//    }

//    public TreePoint()
//    {
//        data = default(T);
//        left = null;
//        right = null;
//    }

//    public TreePoint(T el)
//    {
//        data = el;
//        left = null;
//        right = null;
//    }

//    public override string ToString()
//    {
//        return data.ToString() + " ";
//    }
//}

//public class Tree<T> where T : IComparable
//{
//    public TreePoint<T> root;
//    static public void Add(TreePoint<T> root, T el)
//    {
//        TreePoint<T> added = new TreePoint<T>(el);
//        if (el.CompareTo(root.data) <0)
//        {
//            root.Left = added;
//        }

//        else if (el.CompareTo(root.data) > 0)
//        {
//            root.Right = added;
//        }

//        else
//        {
//            throw new ArgumentException("Данный элемент уже существует в дереве!");
//        }
//    }


//}