using System;
using System.Windows;
using System.Windows.Media;

namespace WcfMoreComplicatedExample
{
    public class MyVisualHost : FrameworkElement
    {
        private readonly VisualCollection _children;

        protected override int VisualChildrenCount => _children.Count;

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }

        public MyVisualHost()
        {
            _children = new VisualCollection(this)
            {
                new SimpleVisual()
            };
        }
    }
}
