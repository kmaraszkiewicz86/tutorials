using System;
using System.Collections.Generic;
using System.Linq;

namespace AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl
{
    public class TreeNode<T>
    {
        public T Data { get; set; }

        public TreeNode<T> Parent { get; set; }

        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();

        public void AddChildren (IEnumerable<TreeNode<T>> children)
        {
            if (children?.Count() == 0)
                return;

            children.ToList().ForEach(item =>
            {
                item.Parent = this;
                Children.Add(item);
            });
        }

        public int GetHeight()
        {
            var height = 1;
            TreeNode<T> current = this;

            while (current.Parent != null)
            {
                current = current.Parent;
            }

            return height;
        }
    }
}
