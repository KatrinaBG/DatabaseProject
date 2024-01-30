using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kongsberg.AVL
{
    public class AVLNode
    {
        public AVLLeaf Leaf { get; set; }

        public byte Number { get; set; }

        internal void Add(byte nodeNumber, AVLLeaf actionObject)
        {
            if (Number >= nodeNumber)
            {
                Add(nodeNumber, actionObject, ref LeftNode);
            }
            else
            {
                Add(nodeNumber, actionObject, ref RightNode);
            }
        }

        private void Add(byte nodeNumber, AVLLeaf actionObject, ref  AVLNode node)
        {
            if (node==null)
            {
                node = new AVLNode
                {
                    Number = nodeNumber,
                    Leaf = actionObject,
                    Parent = this,
                    BalanceFactor = 0
                };
                if (BalanceFactor!=0)
                {
                    BalanceFactor = 0;
                }
                CalculateBalanceFactor();
                
            }
            else
            {
                node.Add(nodeNumber, actionObject);
            }
        }

        private void CalculateBalanceFactor()
        {
            BalanceFactor = 0;
            if (LeftNode!=null)
            {
                BalanceFactor += Math.Abs(LeftNode.BalanceFactor)+1;
            }
            if (RightNode!=null)
            {
                BalanceFactor -=Math.Abs(RightNode.BalanceFactor)+1;
            }

            if (BalanceFactor == 2)
            {
                var A = this;
                var B= LeftNode;
                var C = LeftNode.RightNode;
                var P = Parent;
                B.RightNode = C.LeftNode;
                if (B.RightNode != null) B.RightNode.Parent = B;
                A.LeftNode = C.RightNode;
                if (A.LeftNode != null) A.LeftNode.Parent = A;
                C.RightNode = A;
                C.LeftNode = B;
                A.Parent = B.Parent = C;
                C.Parent = P;


            }

            if (Parent != null)
            {
                Parent.CalculateBalanceFactor();
            }
        }

        public AVLNode LeftNode;

        public AVLNode RightNode;
        public int BalanceFactor { get; set; }

        public AVLNode FindNode(byte nodeNumber)
        {
            if (Number == nodeNumber)
            {
                return this;
            }
            else
            {
                AVLNode node;
                if (Number>=nodeNumber)
                {
                    node=LeftNode;
                }
                else
                {
                    node = RightNode;
                }
                if (node==null)
                {
                    return null;
                }
                return node.FindNode(nodeNumber);
            }
        }

        public AVLNode Parent { get; set; }
    }
}
