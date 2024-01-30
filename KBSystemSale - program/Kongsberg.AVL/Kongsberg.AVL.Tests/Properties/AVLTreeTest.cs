using Kongsberg.AVL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kongsberg.AVL.Tests
{
    
    
    /// <summary>
    ///This is a test class for AVLTreeTest and is intended
    ///to contain all AVLTreeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AVLTreeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AVLTree Constructor
        ///</summary>
        [TestMethod()]
        public void AssertLeafEqualToActionObjectWhenOneNodeToSimpleTreeAdded()
        {
            AVLTree target = new AVLTree();
            byte nodeNumber =0x30 ;
            AVLLeaf actionObject = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber,actionObject);
            AVLNode node = target.FindNode(nodeNumber);
            AVLLeaf result = node.Leaf;
            Assert.AreEqual(actionObject.Value, result.Value);
        }

        [TestMethod]
        public void AssertLeafsEqualToActionObjectWhenThreeNodesToTreeAdded()
        {

            AVLTree target = new AVLTree();

            byte nodeNumber1 = 0x50;
            AVLLeaf actionObject1 = new AVLLeaf { Value = "0x50" };
            target.Add(nodeNumber1, actionObject1);

            byte nodeNumber2 = 0x30;
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber2, actionObject2);

            byte nodeNumber3 = 0x70;
            AVLLeaf actionObject3 = new AVLLeaf { Value = "0x70" };
            target.Add(nodeNumber3, actionObject3);



            AVLNode node1 = target.FindNode(nodeNumber1);
            AVLLeaf result1 = node1.Leaf;
            Assert.AreEqual(actionObject1.Value, result1.Value);

            AVLNode node2 = target.FindNode(nodeNumber2);
            AVLLeaf result2 = node2.Leaf;
            Assert.AreEqual(actionObject2.Value, result2.Value);

            AVLNode node3 = target.FindNode(nodeNumber3);
            AVLLeaf result3 = node3.Leaf;
            Assert.AreEqual(actionObject3.Value, result3.Value);
        }

        [TestMethod]
        public void AssertLLTreeRotationDone()
        {
            AVLTree target = new AVLTree();

            byte nodeNumber1 = 0x50;
            AVLLeaf actionObject1 = new AVLLeaf { Value = "0x50" };
            target.Add(nodeNumber1, actionObject1);
            AVLNode node1 = target.FindNode(nodeNumber1);


            byte nodeNumber2 = 0x30;
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber2, actionObject2);
            AVLNode node2 = target.FindNode(nodeNumber2);
            Assert.AreEqual(node1, node2.Parent);


            byte nodeNumber4 = 0x20;
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x20" };
            target.Add(nodeNumber4, actionObject4);
            AVLNode node4 = target.FindNode(nodeNumber4);
            Assert.AreEqual(node2, node1.Parent);
            Assert.AreEqual(node2, node4.Parent);
            Assert.AreEqual(node2, target.Root);



        }

        [TestMethod]
        public void AssertRRTreeRotationDone()
        {
            AVLTree target = new AVLTree();

            byte nodeNumber1 = 0x20;
            AVLLeaf actionObject1 = new AVLLeaf { Value = "0x20" };
            target.Add(nodeNumber1, actionObject1);
            AVLNode node1 = target.FindNode(nodeNumber1);


            byte nodeNumber2 = 0x30;
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber2, actionObject2);
            AVLNode node2 = target.FindNode(nodeNumber2);
            Assert.AreEqual(node1, node2.Parent);


            byte nodeNumber4 = 0x50;
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x50" };
            target.Add(nodeNumber4, actionObject4);
            AVLNode node4 = target.FindNode(nodeNumber4);
            Assert.AreEqual(node2, node1.Parent);
            Assert.AreEqual(node2, node4.Parent);
            Assert.AreEqual(node2, target.Root);
        }
        [TestMethod]
        public void AssertRLTreeRotationDone()
        {
            AVLTree target = new AVLTree();

            byte nodeNumber1 = 0x20;
            AVLLeaf actionObject1 = new AVLLeaf { Value = "0x20" };
            target.Add(nodeNumber1, actionObject1);
            AVLNode node1 = target.FindNode(nodeNumber1);


            byte nodeNumber4 = 0x50;
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x50" };
            target.Add(nodeNumber4, actionObject4);
            AVLNode node4 = target.FindNode(nodeNumber4);
            Assert.AreEqual(node1, node4.Parent);

            byte nodeNumber2 = 0x30;
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber2, actionObject2);
            AVLNode node2 = target.FindNode(nodeNumber2);
            Assert.AreEqual(node2, node1.Parent);
            Assert.AreEqual(node2, node4.Parent);
            Assert.AreEqual(node2, target.Root);

        }
        [TestMethod]
        public void AssertLRTreeRotationDone()
        {
            AVLTree target = new AVLTree();

            byte nodeNumber1 = 0x50;
            AVLLeaf actionObject1 = new AVLLeaf { Value = "0x50" };
            target.Add(nodeNumber1, actionObject1);
            AVLNode node1 = target.FindNode(nodeNumber1);


            byte nodeNumber4 = 0x20;
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x20" };
            target.Add(nodeNumber4, actionObject4);
            AVLNode node4 = target.FindNode(nodeNumber4);
            Assert.AreEqual(node1, node4.Parent);

            byte nodeNumber2 = 0x30;
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x30" };
            target.Add(nodeNumber2, actionObject2);
            AVLNode node2 = target.FindNode(nodeNumber2);
            Assert.AreEqual(node2, node1.Parent);
            Assert.AreEqual(node2, node4.Parent);
            Assert.AreEqual(node2, target.Root);

        }

        [TestMethod]
        public void AssertLeafEqualToActionObjectWhenOneNodeToDoubleLevelTreeAdded()
        {
            AVLTree target = new AVLTree();
            byte[] nodePath = new byte[2] { 0x30, 0x40 };
            AVLLeaf actionObject = new AVLLeaf { Value = "0x30" };
            target.Add(nodePath, actionObject);
            AVLNode node = target.FindNode(nodePath);
            AVLLeaf result = node.Leaf;
            Assert.AreEqual(actionObject.Value, result.Value);
        }
        [TestMethod]
        public void AssertLeafEqualToActionObjectWhenOneLongNodeToMultiLevelTreeAdded()
        {
            AVLTree target = new AVLTree();
            byte[] nodePath = new byte[7] { 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90 };
            AVLLeaf actionObject = new AVLLeaf { Value = "0x30" };
            target.Add(nodePath, actionObject);
            AVLNode node = target.FindNode(nodePath);
            AVLLeaf result = node.Leaf;
            Assert.AreEqual(actionObject.Value, result.Value);
        }
        [TestMethod]
        public void AssertLeafEqualToActionObjectWhenMultipleNodesToMultiLevelTreeAdded()
        {
            AVLTree target = new AVLTree();
            byte[] nodePath = new byte[3] { 30, 40, 50} ;
            byte[] nodePath2 = new byte[3] {20, 40, 50};
            byte[] nodePath3 = new byte[3] {10, 40, 50};
            byte[] nodePath4 = new byte[3] {30, 10, 50};
            byte[] nodePath5 = new byte[3] {30, 20, 50};
            AVLLeaf actionObject = new AVLLeaf { Value = "0x30" };
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x40" };
            AVLLeaf actionObject3 = new AVLLeaf { Value = "0x50" };
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x60" };
            AVLLeaf actionObject5 = new AVLLeaf { Value = "0x70" };
            target.Add(nodePath, actionObject);
            target.Add(nodePath2, actionObject2);
            target.Add(nodePath3, actionObject3);
            target.Add(nodePath4, actionObject4);
            target.Add(nodePath5, actionObject5);
            AVLNode node = target.FindNode(nodePath);
            AVLNode node2 = target.FindNode(nodePath2);
            AVLNode node3 = target.FindNode(nodePath3);
            AVLNode node4 = target.FindNode(nodePath4);
            AVLNode node5 = target.FindNode(nodePath5);
            AVLLeaf result = node.Leaf;
            AVLLeaf result2 = node2.Leaf;
            AVLLeaf result3 = node3.Leaf;
            AVLLeaf result4 = node4.Leaf;
            AVLLeaf result5 = node5.Leaf;
            Assert.AreEqual(actionObject.Value, result.Value);
            Assert.AreEqual(actionObject2.Value, result2.Value);
            Assert.AreEqual(actionObject3.Value, result3.Value);
            Assert.AreEqual(actionObject4.Value, result4.Value);
            Assert.AreEqual(actionObject5.Value, result5.Value);
        }

        [TestMethod]
        public void AssertResultKongsbergUsage()
        {
            AVLTree target = new AVLTree();
            byte[] nodePath = new byte[5] {1,5, 30, 40, 50 };
            byte[] nodePath2 = new byte[5] {1,5, 20, 40, 50 };
            byte[] nodePath3 = new byte[5] {1,5, 10, 40, 50 };
            byte[] nodePath4 = new byte[5] {1,5, 30, 10, 50 };
            byte[] nodePath5 = new byte[5] { 1, 5, 30, 20, 50 };
            byte[] nodePath6Long = new byte[5] { 1, 5, 30, 30, 20 };
            byte[] nodePath6Short = new byte[4] { 1, 5, 30, 30};
            AVLLeaf actionObject = new AVLLeaf { Value = "0x30" };
            AVLLeaf actionObject2 = new AVLLeaf { Value = "0x40" };
            AVLLeaf actionObject3 = new AVLLeaf { Value = "0x50" };
            AVLLeaf actionObject4 = new AVLLeaf { Value = "0x60" };
            AVLLeaf actionObject5 = new AVLLeaf { Value = "0x70" };
            AVLLeaf actionObject6 = new AVLLeaf { Value = "0x10" };
            target.Add(nodePath, actionObject);
            target.Add(nodePath2, actionObject2);
            target.Add(nodePath3, actionObject3);
            target.Add(nodePath4, actionObject4);
            target.Add(nodePath5, actionObject5);
            target.Add(nodePath6Short, actionObject6);
            AVLNode node = target.FindNode(nodePath);
            AVLNode node2 = target.FindNode(nodePath2);
            AVLNode node3 = target.FindNode(nodePath3);
            AVLNode node4 = target.FindNode(nodePath4);
            AVLNode node5 = target.FindNode(nodePath5);
            AVLNode node6 = target.FindNode(nodePath6Long);
            AVLLeaf result = node.Leaf;
            AVLLeaf result2 = node2.Leaf;
            AVLLeaf result3 = node3.Leaf;
            AVLLeaf result4 = node4.Leaf;
            AVLLeaf result5 = node5.Leaf;
            AVLLeaf result6 = node6.Leaf;
            Assert.AreEqual(actionObject.Value, result.Value);
            Assert.AreEqual(actionObject2.Value, result2.Value);
            Assert.AreEqual(actionObject3.Value, result3.Value);
            Assert.AreEqual(actionObject4.Value, result4.Value);
            Assert.AreEqual(actionObject5.Value, result5.Value);
            Assert.AreEqual(actionObject6.Value, result6.Value);
        }
    }
}
