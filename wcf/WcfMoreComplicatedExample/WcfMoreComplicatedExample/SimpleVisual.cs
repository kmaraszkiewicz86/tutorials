using System.Windows;
using System.Windows.Media;

namespace WcfMoreComplicatedExample
{
    public class SimpleVisual: DrawingVisual
    {
        public SimpleVisual()
        {
            StreamGeometry geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(new Point(0, 0),
                    true, true);
                geometryContext.LineTo(new Point(100, 0),
                    true, true);
                geometryContext.ArcTo(new Point(0, 100),
                    new Size(100, 100), 0.0, false, SweepDirection.Clockwise,
                    true, true);
                geometryContext.LineTo(new Point(0, 0), false, false);

                using (DrawingContext context = RenderOpen())
                {
                    var pen = new Pen(Brushes.Red, 2.0);
                    context.DrawGeometry(Brushes.Red, pen, geometry);
                }
            }
        }
    }
}
