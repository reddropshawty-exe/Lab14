using System;
using System.Collections;
namespace Lab10FINLIB;

    public class SortByYear : IComparer
    {
        public int Compare(object x, object y)
        {
            Car c1 = x as Car;
            Car c2 = y as Car;
            if (c1 == null || c2 == null) return -1;
            if (c1.Year > c2.Year) return 1;
            else if (c1.Year == c2.Year) return 0;
            else return -1;

        }
    }


