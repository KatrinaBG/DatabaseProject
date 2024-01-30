using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kongsberg.AVL
{
    public class AVLTree
    {
        public AVLNode Add(byte nodeNumber, AVLLeaf actionObject)
        {
            if (HasRootNode)
            {
                AVLNode Y = null;
                AVLNode X = Root;
                AVLNode N = new AVLNode
                {
                    BalanceFactor = 0,
                    Leaf=actionObject,
                    Number=nodeNumber
                };
                AVLNode result = N;
                while (X!=null)
                {
                    if (N.Number == X.Number)
                    {
                        N = null;
                        return X;
                    }
                    Y = X;
                    if (N.Number < X.Number)
                    {
                        X = X.LeftNode;
                    }
                    else
                    {
                        X = X.RightNode;
                    }
                }
                N.Parent = Y;
                if (N.Number<Y.Number)
                {
                    Y.LeftNode = N;
                }
                else
                {
                    Y.RightNode = N;
                }
                if (Y.BalanceFactor != 0)
                {
                    Y.BalanceFactor = 0;
                    return result;
                }
                if (N == Y.LeftNode)
                {
                    Y.BalanceFactor=1;
                }
                else
                {
                    Y.BalanceFactor = -1;
                }
                var Z = Y.Parent;
                while (Z!=null)
                {
                    if (Z.BalanceFactor!=0)
                    {
                        break;
                    }
                    if (Z.LeftNode==Y)
                    {
                        Z.BalanceFactor = 1;
                    }
                    else
                    {
                        Z.BalanceFactor = -1;
                    }
                    Y = Z;
                    Z = Z.Parent;
                }
                if (Z == null)
                {
                    return result;
                }
                if (Z.BalanceFactor==1)
                {
                    if (Z.RightNode == Y)
                    {
                        Z.BalanceFactor = 0;
                        return result;
                    }
                    if (Y.BalanceFactor==-1)
                    {
                        LRRotation(this, Z);
                    }
                    else
                    {
                        LLRotation(this, Z);
                    }
                    return result;
                }
                else
                {
                    if (Z.LeftNode==Y)
                    {
                        Z.BalanceFactor = 0;
                        return result;
                    }
                    if (Y.BalanceFactor == 1)
                    {
                        RLRotation(this, Z);
                    }
                    else
                    {
                        RRRotation(this, Z);
                    }
                }
                return result;
            }
            else
            {
                Root = new AVLNode
                {
                    Number = nodeNumber,
                    Leaf = actionObject,
                    BalanceFactor = 0
                };
                return Root;
            }
        }

        private static void RRRotation(AVLTree tree, AVLNode A)
        {
            var B = A.RightNode;
            var P = A.Parent;
            A.RightNode = B.LeftNode;
            if (A.RightNode!=null)
            {
                A.RightNode.Parent = A;
            }
            B.LeftNode = A;
            B.Parent = P;
            A.Parent = B;
            if (P != null)
            {
                if (P.LeftNode == A)
                {
                    P.LeftNode = B;
                }
                else
                {
                    P.RightNode = B;
                }
            }
            else
            {
                tree.Root = B;
            }
            if (B.BalanceFactor == -1)
            {
                A.BalanceFactor = B.BalanceFactor = 0;
            }
            else
            {
                A.BalanceFactor = -1; B.BalanceFactor = 1;
            }
            return;
        }

        private static void RLRotation(AVLTree tree, AVLNode A)
        {
            var B = A.RightNode;
            var C = B.LeftNode;
            var P = A.Parent;
            B.LeftNode = C.RightNode;
            if (B.LeftNode!=null) B.LeftNode.Parent = B;
            A.RightNode = C.LeftNode;
            if (A.RightNode != null) { A.RightNode.Parent = A; }
            C.LeftNode = A;
            C.RightNode = B;
            A.Parent = B.Parent = C;
            C.Parent = P;
            if (P != null)
            {
                if (P.LeftNode == A) { P.LeftNode = C; }
                else { P.RightNode = C; }
            }
            else { tree.Root = C; }
            A.BalanceFactor = (C.BalanceFactor == -1) ? 1 : 0;
            B.BalanceFactor = (C.BalanceFactor == 1) ? -1 : 0;
            C.BalanceFactor=0;
            return;
        }

        private static void LLRotation(AVLTree tree, AVLNode A)
        {
            var B = A.LeftNode;
            var P = A.Parent;
            A.LeftNode = B.RightNode;
            if (A.LeftNode != null)
            {
                A.LeftNode.Parent = A;
            }
            B.RightNode = A;
            B.Parent = P;
            A.Parent = B;
            if (P==null)
            {
                tree.Root = B;
            }
            else
            {
                if (P.LeftNode==A)
                {
                    P.LeftNode = B;
                }
                else
                {
                    P.RightNode = B;
                }

            }
            if (B.BalanceFactor == 1)
            {
                A.BalanceFactor = B.BalanceFactor = 0;
            }
            else
            {
                A.BalanceFactor = 1;
                B.BalanceFactor = -1;
            }
            return;
        }

        private static void LRRotation(AVLTree tree, AVLNode A)
        {
            var B = A.LeftNode;
            var C = B.RightNode;
            var P = A.Parent;
            B.RightNode = C.LeftNode;
            if (B.RightNode != null) B.RightNode.Parent = B;
            A.LeftNode = C.RightNode;
            if (A.LeftNode != null) A.LeftNode.Parent = A;
            C.RightNode = A;
            C.LeftNode = B;
            A.Parent = B.Parent = C;
            C.Parent = P;
            if (P != null)
            {
                if (P.LeftNode == A) P.LeftNode = C;
                else P.RightNode = C;
            }
            else
            {
                tree.Root = C;
            }
            A.BalanceFactor = C.BalanceFactor == 1 ? -1 : 0;
            B.BalanceFactor = C.BalanceFactor == -1 ? 1 : 0;
            C.BalanceFactor = 0;
            return;
        }

        public AVLNode FindNode(byte nodeNumber)
        {
            if (Root==null)
            {
                return null;
            }
            return Root.FindNode(nodeNumber);
        }

        public bool HasRootNode { get { return Root != null; } }

        public AVLNode Root { get; set; }

        public void Add(byte[] nodePath, AVLLeaf actionObject)
        {
            AVLNode node=null;
            AVLTree tree = this;
            foreach (var item in nodePath)
            {
                node = tree.Add(item, actionObject);
                if (node.Leaf.Value == null || !(node.Leaf.Value is AVLTree))
                {
                    tree = new AVLTree();
                    node.Leaf = new AVLLeaf { Value = tree };
                }
                else { tree = node.Leaf.Value as AVLTree; }
            }
            if (node != null)
            {
                node.Leaf = actionObject;
            }
        }

        public AVLNode FindNode(byte[] nodePath)
        {
            AVLNode node = null;
            AVLTree tree = this;
            foreach (var item in nodePath)
            {
                node = tree.FindNode(item);
                if (!(node.Leaf.Value is AVLTree))
                {
                    return node;
                }
                tree = node.Leaf.Value as AVLTree;
            }
            return node;
        }
    }
}
