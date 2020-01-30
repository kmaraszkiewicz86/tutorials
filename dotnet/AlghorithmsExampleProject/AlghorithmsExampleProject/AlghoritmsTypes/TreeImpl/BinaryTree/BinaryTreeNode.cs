using System.Collections.Generic;

namespace AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl.BinaryTree
{
    public class BinaryTreeNode<T>: TreeNode<T>
    {
        public BinaryTreeNode()
        {
            Children = new List<TreeNode<T>>(new BinaryTreeNode<T>[] {
                null, null
            });
        }

        public BinaryTreeNode<T> LeftNode
        {
            get { return (BinaryTreeNode<T>)Children[0]; }
            set
            {
                Children[0] = value;
            }
        }
            

        public BinaryTreeNode<T> RightNode
        {
            get { return (BinaryTreeNode<T>)Children[1]; }
            set
            {
                Children[1] = value;
            }
        }
            
    }
}