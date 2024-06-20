//using System;
//namespace lab_12_tests
//{
//    using System;
//    using System.Collections.Generic;
//    using global::Lab12Tusk;
//    using Lab10FINLIB;
//    using Lab12Tusk;
//    using Microsoft.VisualStudio.TestTools.UnitTesting;

//    namespace Lab12Tusk.Tests
//    {
//        [TestClass]
//        public class MyObservableCollectionTests
//        {
//            [TestMethod]
//            public void Add_ShouldInvokeCollectionCountChanged()
//            {
//                // Arrange
//                MyObservableCollection<string, Car> collection = new MyObservableCollection<string, Car>(10);
//                Journal journal = new Journal();
//                collection.CollectionCountChanged += journal.AddEntry;
//                Car car = new Car();
//                car.RandomInit();
//                string key = car.GetKey();

//                // Act
//                collection.Add(key, car);

//                // Assert
//                Assert.AreEqual(1, journal.notes.Count);
//                Assert.AreEqual("add", journal.notes[0].Type);
//            }

//            [TestMethod]
//            public void Remove_ShouldInvokeCollectionCountChanged()
//            {
//                // Arrange
//                MyObservableCollection<string, Car> collection = new MyObservableCollection<string, Car>(10);
//                Journal journal = new Journal();
//                collection.CollectionCountChanged += journal.AddEntry;
//                Car car = new Car();
//                car.RandomInit();
//                string key = car.GetKey();
//                collection.Add(key, car);

//                // Act
//                bool result = collection.Remove(key);

//                // Assert
//                Assert.IsTrue(result);
//                Assert.AreEqual(2, journal.notes.Count);
//                Assert.AreEqual("remove", journal.notes[1].Type);
//            }

//            [TestMethod]
//            public void Clear_ShouldInvokeCollectionCountChanged()
//            {
//                // Arrange
//                MyObservableCollection<string, Car> collection = new MyObservableCollection<string, Car>(10);
//                Journal journal = new Journal();
//                collection.CollectionCountChanged += journal.AddEntry;
//                Car car1 = new Car();
//                Car car2 = new Car();
//                car1.RandomInit();
//                car2.RandomInit();
//                collection.Add(car1.GetKey(), car1);
//                collection.Add(car2.GetKey(), car2);

//                // Act
//                collection.Clear();

//                // Assert
//                Assert.AreEqual(3, journal.notes.Count);
//                Assert.AreEqual("clear", journal.notes[2].Type);
//            }

//            [TestMethod]
//            public void Indexer_ShouldInvokeCollectionReferenceChanged()
//            {
//                // Arrange
//                MyObservableCollection<string, Car> collection = new MyObservableCollection<string, Car>(10);
//                Journal journal = new Journal();
//                collection.CollectionReferenceChanged += journal.AddEntry;
//                Car car1 = new Car();
//                Car car2 = new Car();
//                car1.RandomInit();
//                car2.RandomInit();
//                string key = car1.GetKey();
//                collection.Add(key, car1);

//                // Act
//                collection[key] = car2;

//                // Assert
//                Assert.AreEqual(1, journal.notes.Count);
//                Assert.AreEqual("update", journal.notes[0].Type);
//            }
//        }

//        [TestClass]
//        public class JournalTests
//        {
//            [TestMethod]
//            public void AddEntry_ShouldAddNewJournalEntry()
//            {
//                // Arrange
//                Journal journal = new Journal();
//                object source = new object();
//                CollectionHandlerEventArgs args = new CollectionHandlerEventArgs("key", "value", "add");

//                // Act
//                journal.AddEntry(source, args);

//                // Assert
//                Assert.AreEqual(1, journal.notes.Count);
//                Assert.AreEqual("add", journal.notes[0].Type);
//                Assert.AreEqual("Key: key, Value: value", journal.notes[0].Data);
//            }

//            [TestMethod]
//            public void PrintJournal_ShouldPrintAllEntries()
//            {
//                // Arrange
//                Journal journal = new Journal();
//                object source = new object();
//                CollectionHandlerEventArgs args1 = new CollectionHandlerEventArgs("key1", "value1", "add");
//                CollectionHandlerEventArgs args2 = new CollectionHandlerEventArgs("key2", "value2", "remove");
//                journal.AddEntry(source, args1);
//                journal.AddEntry(source, args2);

//                // Act
//                journal.PrintJournal();

//                // The assert for console output is a bit tricky, often you can redirect Console output or use a mocking framework
//                // for simplicity, this test just checks if the method completes without exceptions
//            }
//        }

//        [TestClass]
//        public class CollectionHandlerEventArgsTests
//        {
//            [TestMethod]
//            public void Constructor_ShouldInitializeProperties()
//            {
//                // Arrange
//                string key = "testKey";
//                string value = "testValue";
//                string action = "testAction";

//                // Act
//                CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(key, value, action);

//                // Assert
//                Assert.AreEqual(key, args.Key);
//                Assert.AreEqual(value, args.Value);
//                Assert.AreEqual(action, args.Action);
//            }

//            [TestMethod]
//            public void ToString_ShouldReturnFormattedString()
//            {
//                // Arrange
//                string key = "testKey";
//                string value = "testValue";
//                string action = "testAction";
//                CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(key, value, action);

//                // Act
//                string result = args.ToString();

//                // Assert
//                Assert.AreEqual("Action: testAction, Key: testKey, Value: testValue", result);
//            }
//        }
//    }

//}

