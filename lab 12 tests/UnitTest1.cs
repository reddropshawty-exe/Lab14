using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12Tusk;
using Lab10FINLIB;
using System;

namespace LinkListTests
{
    [TestClass]
    public class LinkListTests
    {
        [TestMethod]
        public void TestAppEnd()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);

            list.AppEnd(car1);
            list.AppEnd(car2);

            Assert.IsNotNull(list.beg);
            Assert.IsNotNull(list.end);
            Assert.AreEqual(car1, list.beg.Data);
            Assert.AreEqual(car2, list.end.Data);
            Assert.AreEqual(car2, list.beg.next.Data);
            Assert.AreEqual(car1, list.end.prev.Data);
        }

        [TestMethod]
        public void TestAppBeg()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);

            list.AppBeg(car1);
            list.AppBeg(car2);

            Assert.IsNotNull(list.beg);
            Assert.IsNotNull(list.end);
            Assert.AreEqual(car2, list.beg.Data);
            Assert.AreEqual(car1, list.end.Data);
            Assert.AreEqual(car1, list.beg.next.Data);
            Assert.AreEqual(car2, list.end.prev.Data);
        }

        [TestMethod]
        public void TestAppRandK()
        {
            var list = new LinkList<Car>();
            list.AppRandK(2);

            Assert.IsNotNull(list.beg);
            Assert.IsNotNull(list.end);
            Assert.AreEqual(3, GetListLength(list));
        }

        [TestMethod]
        public void TestAppInpK()
        {
            var list = new LinkList<Car>();
            list.AppInpK(2);

            Assert.IsNotNull(list.beg);
            Assert.IsNotNull(list.end);
            Assert.AreEqual(1, GetListLength(list));
        }

        [TestMethod]
        public void TestListCreate()
        {
            var list = LinkList<Car>.ListCreate(2);

            Assert.IsNotNull(list.beg);
            Assert.IsNotNull(list.end);
            Assert.AreEqual(3, GetListLength(list)); // 2+1 (ListCreate adds an extra element)
        }

        [TestMethod]
        public void TestDeepCopy()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);

            list.AppEnd(car1);
            list.AppEnd(car2);

            var copy = list.DeepCopy();

            Assert.AreEqual(GetListLength(list), GetListLength(copy));
            Assert.IsTrue(list.beg.Data.Equals(copy.beg.Data));
            Assert.IsTrue(list.end.Data.Equals(copy.end.Data));
        }

        [TestMethod]
        public void TestDelListEl()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);

            list.AppEnd(car1);
            list.AppEnd(car2);

            list.DelListEl("Brand", "Brand1");

            Assert.AreEqual(1, GetListLength(list));
            Assert.AreEqual(car2, list.beg.Data);
            Assert.AreEqual(car2, list.end.Data);
        }

        [TestMethod]
        public void TestDelList()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);

            list.AppEnd(car1);
            list.AppEnd(car2);

            LinkList<Car>.DelList(list);

            Assert.IsNull(list.beg);
            Assert.IsNull(list.end);
        }

        [TestMethod]
        public void TestPrintListEmpty()
        {
            var list = new LinkList<Car>();
            try
            {
                list.PrintList();
            }
            catch (Exception)
            {
                Assert.Fail("PrintList() threw an exception on empty list");
            }
        }

        [TestMethod]
        public void TestDelListElMiddle()
        {
            var list = new LinkList<Car>();
            var car1 = new Car("Brand1", 2010, "Red", 10000, 15);
            var car2 = new Car("Brand2", 2012, "Blue", 15000, 20);
            var car3 = new Car("Brand3", 2014, "Green", 20000, 25);

            list.AppEnd(car1);
            list.AppEnd(car2);
            list.AppEnd(car3);

            list.DelListEl("Brand", "Brand2");

            Assert.AreEqual(2, GetListLength(list));
            Assert.AreEqual(car1, list.beg.Data);
            Assert.AreEqual(car3, list.end.Data);
        }

        private int GetListLength<T>(LinkList<T> list) where T : ICloneable, IInit, new()
        {
            int length = 0;
            var current = list.beg;
            while (current != null)
            {
                length++;
                current = current.next;
            }
            return length;
        }
    }
}
