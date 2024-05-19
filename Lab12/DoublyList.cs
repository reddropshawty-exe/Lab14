using System.Drawing;
using System.Reflection;
using Lab10FINLIB;
namespace Lab12Tusk;
public class Point<T> where T : ICloneable, IInit, new()
{
    // Объект данных может быть любого типа, определенного пользователем
    //Поля для ввода
    public T? Data { get; set; }

    public Point<T>? next, prev;


    // Конструкторы
    public Point()
    {
        Data = new T();
        next = null;
        prev = null;
    }

    public Point(T p)
    {
        Data = p;
        next = null;
        prev = null;
    }

    public override int GetHashCode()
    {
        return Data == null ? 0 : Data.GetHashCode();


    }
}


 


public class LinkList<T> where T : ICloneable, IInit, new()
{
    public Point<T>? beg, end;

    public LinkList()
    {
        beg = null;
        end = null;
    } 



    public void AppEnd(T el)
    {
        Point<T> addedPoint = MakePoint(el);
        if (beg == null && end == null)
        {
            beg = addedPoint;
            end = addedPoint;
            return;
        }

        end.next = addedPoint;
        addedPoint.prev = end;
        end = addedPoint;
    }

    public void AppBeg(T el)
    {
        Point<T> addedPoint = MakePoint(el);
        if (beg == null && end == null)
        {
            beg = addedPoint;
            end = addedPoint;
            return;
        }

        beg.prev = addedPoint;
        addedPoint.next = beg;
        beg = addedPoint;
    }

    public void AppRandK(int k)
    {
        //Console.WriteLine("Выберите метод заполнени\n1)Случайно\n2)Вручную");


        for(int i = 0; i<k; i++)
        {
            T el = new T();
            el.RandomInit();
            Point<T> addedPoint = MakePoint(el);
            if (beg == null && end == null)
            {
                beg = addedPoint;
                end = addedPoint;
                k++;
            }
            else
            {
                beg.prev = addedPoint;
                addedPoint.next = beg;
                beg = addedPoint;
            }

        }
    }

    public void AppInpK(int k)
    {
        //Console.WriteLine("Выберите метод заполнени\n1)Случайно\n2)Вручную");
        
        for (int i = 0; i < k; i++)
        {
            T el = new T();
            el.Init();
            Point<T> addedPoint = MakePoint(el);
            if (beg == null && end == null)
            {
                beg = addedPoint;
                end = addedPoint;
                return;
            }

            beg.prev = addedPoint;
            addedPoint.next = beg;
            beg = addedPoint;
        }
    }


    public static LinkList<T> ListCreate(int size)
    {
        LinkList<T> res= new LinkList<T>();
        T el = new T();
        el.RandomInit();
        Point<T> addedPoint = MakePoint(el);
        res.beg = addedPoint;
        res.end = addedPoint;
        for(int i =1; i<=size; i++)
        {
            el = new T();
            el.RandomInit();
            res.AppEnd(el);
        }
        return res;
    }

    public void PrintList()
    {
        Point<T> current=beg;
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (beg == null)
        {
            Console.WriteLine("Список пустой!");
            return;
        }

        if (current.next == null)
        {
            Console.WriteLine(current.Data.ToString());
            return;
        }

        do
        {
            Console.WriteLine(current.Data.ToString());
            current = current.next;
        } while (current.next != null);
        Console.ResetColor();
        return;
    }

    // Метод создания нового элемента
    static Point<T> MakePoint(T d)
    {
        return new Point<T>(d);
    }

    //// Предполагаем, что T реализует интерфейс ICloneable
    //public LinkList<T> DeepCopy()
    //{
    //    Point<T>? current = beg;
    //    LinkList<T> copy = new LinkList<T>();
    //    copy.beg.data = (T)beg.data.Clone(); // Копируем первый элемент
    //    Point<T>? copyLast = copy.beg;

    //    while (current != null)
    //    {
    //        Point<T> copyCurrent = new Point<T>();
    //        copyCurrent.data = (T)current.data.Clone(); // Копируем текущий элемент
    //        copy.AppEnd(copyCurrent.data); // Добавляем копию в конец списка
    //        current = current.next; // Переходим к следующему элементу
    //    }

    //    return copy;
    //}

    //public static LinkList<T> DeepCopy(LinkList<T> orig)
    //{
    //    LinkList<T> copy = new LinkList<T>();
    //    copy.beg.data = (T)orig.beg.data.Clone(); // Используем метод Clone для создания копии объекта Car
    //    Point<T> p = orig.beg;
    //    p = p.next;
    //    Point<T> copyLast = copy.beg;
    //    while (p != null)
    //    {
    //        Point<T> copyAdded = new Point<T>();
    //        copyAdded.data = (T)p.data.Clone(); // Также используем метод Clone для создания копии объекта Car
    //        copy.AppEnd(copyAdded.data);
    //        p = p.next;
    //    }
    //    return copy;
    //}

    public LinkList<T> DeepCopy()
    {
        LinkList<T> copy = new LinkList<T>();

        if (beg == null)
        {
            return copy;
        }

        Point<T>? current = beg;
        while (current != null)
        {
            T newdata = (T)current.Data.Clone();
            copy.AppEnd(newdata);
            current = current.next;
        }
        return copy;
    }


    public void DelListEl(string propName, string strDelValue) {

        bool smthDeleted = false;
        //Проверяем список на пустоту
        if (beg == null)
        {
            Console.WriteLine("Список пуст!");
            return;
        }
        //Ставим маркер р на первый элемент
        Point<T> p = beg;

        //проходим, пока р не пуста
        while (p != null)
        {
            Type type = typeof(T);
            PropertyInfo myPropertyInfo = type.GetProperty(propName);
            bool isDel;
            string value=null;
            if (myPropertyInfo != null)
            {
                value = (string)myPropertyInfo.GetValue(p.Data);
            }

            //если нашли удаляемый элемент
            if (myPropertyInfo != null && value ==strDelValue)
            {
                smthDeleted = true;

                if(p.prev == null && p.next == null)
                {
                    beg = null;
                    end = null;
                }

                //если элемент в середине, связываем ссылками соседние элементы
                if (p.prev != null && p.next != null)
                {
                    //p.prev.next = null;
                    //p.prev.next = p.next.prev;
                    //p.next.prev = p.prev.next;
                    p = p.prev;
                    p.next = p.next.next;
                    p = p.next;
                    p.prev = p.prev.prev;
                    p = p.prev;
                }

                //если элемент последний, заменяем ссыллку на него, ссылкой на пустоту
                else if (p.prev != null)
                {
                    p = p.prev;
                    p.next = null;
                    p = end;
                }

                //если элемент первый заменям ссылку на него от следующего элемента на пустоту, и делаем тот элементом начальным
                else if (p.next != null)
                {
                    p = p.next;
                    p.prev = null;
                    beg = p;
                }
            }
            //смещаем маркер на следующий элемент
            p = p.next;
        }


        //если ничего не удалили, сообщаем об этом
        if (!smthDeleted)
        {
            Console.WriteLine("Удаляемый элемент не найден!");
        }
        //возвращаем ссылку на первый элемент

    }


   
    public static void DelList(LinkList<T> del)
    {
        del.beg = null;
        del.end = null;
    }

}