using System;
using System.Collections.Generic;

namespace AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl.BinaryTree
{
    public class BinaryTree<T>
    {
        public BinaryTreeNode<T> Root { get; set; }

        public int Count
        {
            get
            {
                if (Root == null)
                    return 0;

                if (Root.Children?.Count == 0)
                    return 1;

                var height = 0;

                foreach (var item in Traverse(BinaryTreeOrderType.PreOrder))
                {
                    height = Math.Max(height, item.GetHeight());
                }

                return 1;
            }
        }

        public List<BinaryTreeNode<T>> Traverse (BinaryTreeOrderType type)
        {
            var result = new List<BinaryTreeNode<T>>();

            switch (type)
            {
                case BinaryTreeOrderType.PreOrder:
                    TraversePreOrder(Root, result);
                    break;

                case BinaryTreeOrderType.InOrder:
                    TraverseInOrder(Root, result);
                    break;

                case BinaryTreeOrderType.PostOrder:
                    TraversePostOrder(Root, result);
                    break;
            }

            return result;
        }

        private List<BinaryTreeNode<T>> TraversePreOrder (BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                result.Add(node);
                TraversePreOrder(node.LeftNode, result);
                TraversePreOrder(node.RightNode, result);
            }

            return result;
        }

        private List<BinaryTreeNode<T>> TraverseInOrder (BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                TraverseInOrder(node.LeftNode, result);
                result.Add(node);
                TraverseInOrder(node.RightNode, result);
            }

            return result;
        }

        private List<BinaryTreeNode<T>> TraversePostOrder (BinaryTreeNode<T> node, List<BinaryTreeNode<T>> result)
        {
            if (node != null)
            {
                TraversePostOrder(node.LeftNode, result);
                TraversePostOrder(node.RightNode, result);
                result.Add(node);
            }

            return result;
        }

       

    }
}
