using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kongsberg.AVL
{
    public class AVLWrapper
    {
        public static AVLTree RxTxTree = new AVLTree();
        public static void AddRxTx(int stage, int length, byte[] path, Action<object> tx)
        {
            //RxTxTree.Add((byte)stage, new AVLLeaf { Value = lengthTree });
            var newPath = new byte[path.Length + 2];
            newPath[0] = (byte)stage;
            newPath[1] = (byte)length;
            for (int i = 0; i < path.Length; i++)
            {
                newPath[i + 2] = path[i];
            }
            RxTxTree.Add(newPath, new AVLLeaf { Value = tx });
        }
        public static void AddRxTx(int stage, byte[] path, Action<object> tx)
        {
            AddRxTx(stage, path.Length, path, tx);
        }
    }
}
