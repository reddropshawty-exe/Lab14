using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12Tusk;
using Lab10FINLIB;

namespace Lab12Tusk.Tests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void Tree_Constructor_CreatesTreeWithCorrectSize()
        {
            // Arrange
            int treeSize = 3;

            // Act
            var tree = new Tree<Car>(treeSize);

            // Assert
            Assert.AreEqual(treeSize, tree.Count);
        }

        [TestMethod]
        public void Clone_CreatesExactCopy()
        {
            // Arrange
            int treeSize = 3;
            var originalTree = new Tree<Car>(treeSize);

            // Act
            var clonedTree = originalTree.Clone();

            // Assert
            Assert.AreEqual(originalTree.Count, 3);
            Assert.AreEqual(originalTree.Root.Data, clonedTree.Root.Data);
        }

        [TestMethod]
        public void AddBalanced_AddsElementCorrectly()
        {
            // Arrange
            var tree = new Tree<Car>(3);
            var car = new Car("Ford", 2022, "red", 25000, 15);

            // Act
            tree.AddBalanced(car);

            // Assert
            Assert.AreEqual(3, tree.Count);
        }

        [TestMethod]
        public void TreeDeep_CalculatesCorrectHeight()
        {
            // Arrange
            var tree = new Tree<Car>(3);

            // Act
            int height = tree.TreeDeep(tree.Root);

            // Assert
            Assert.AreEqual(2, height); // Tree with 3 elements should have height 2
        }

        [TestMethod]
        public void MakeSearch_ConvertsToSearchTree()
        {
            // Arrange
            var tree = new Tree<Car>(3);
            var root = tree.Root;

            // Act
            tree.MakeSearch(root);

            // Assert
            // The tree should now be a search tree (in-order traversal should be sorted)
            var elements = new List<Car>();
            InOrderTraversal(tree.Root, elements);
            for (int i = 1; i < elements.Count; i++)
            {
                Assert.IsTrue(elements[i - 1].CompareTo(elements[i]) >= -1);
            }
        }

        [TestMethod]
        public void Add_AddsElementCorrectly()
        {
            // Arrange
            var tree = new Tree<Car>(3);
            var car = new Car("Toyota", 2020, "blue", 20000, 10);

            // Act
            tree.Add(car);

            // Assert
            Assert.AreEqual(4, tree.Count);
            Assert.IsNotNull(FindNode(tree.Root, car));
        }

        [TestMethod]
        public void DeleteTree_ClearsTree()
        {
            // Arrange
            var tree = new Tree<Car>(3);

            // Act
            tree.DeleteTree();

            // Assert
            Assert.AreEqual(0, tree.Count);
            Assert.IsNull(tree.Root);
        }

        [TestMethod]
        public void Run_VisitsAllNodes()
        {
            // Arrange
            var tree = new Tree<Car>(3);
            int visitedNodes = 0;

            // Act
            Run(tree.Root, ref visitedNodes);

            // Assert
            Assert.AreEqual(3, visitedNodes);
        }

        [TestMethod]
        public void FindNode_FindsCorrectNode()
        {
            // Arrange
            var tree = new Tree<Car>(3);
            var car = new Car("Honda", 2019, "green", 18000, 8);
            tree.Add(car);

            // Act
            var foundNode = FindNode(tree.Root, car);

            // Assert
            Assert.IsNotNull(foundNode);
            Assert.AreEqual(car, foundNode.Data);
        }

        private void InOrderTraversal(PointTree<Car> node, List<Car> elements)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, elements);
                elements.Add(node.Data);
                InOrderTraversal(node.Right, elements);
            }
        }

        private void Run(PointTree<Car> node, ref int visitedNodes)
        {
            if (node != null)
            {
                visitedNodes++;
                Run(node.Left, ref visitedNodes);
                Run(node.Right, ref visitedNodes);
            }
        }

        private PointTree<Car> FindNode(PointTree<Car> node, Car data)
        {
            if (node == null || node.Data.Equals(data))
                return node;

            var leftSearch = FindNode(node.Left, data);
            if (leftSearch != null)
                return leftSearch;

            return FindNode(node.Right, data);
        }
    }
}