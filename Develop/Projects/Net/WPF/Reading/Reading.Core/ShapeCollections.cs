using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;
using Savchin.Wpf.Shapes;

namespace Reading.Core
{
   public  class ShapeCollections : List<Shape>
    {
        private Brush[] _brashes=new Brush[]
                                     {
                                         new SolidColorBrush(Colors.Red),
                                         new SolidColorBrush(Colors.Green),
                                         new SolidColorBrush(Colors.Blue),
                                          new SolidColorBrush(Colors.Yellow)
                                     };
        public ShapeCollections()
        {
            Add(null);
            foreach (var brush in _brashes)
            {
                Add(new Rectangle
                {
                    Height = 20,
                    Width = 20,
                    Fill = brush
                });
                Add(new Ellipse
                {
                    Height = 20,
                    Width = 20,
                    Fill = brush
                });
                Add(new PolyStar
                       {
                           Sides = 5,
                           Radius = 20,
                           ShapeType = EShapeType.Star,
                           InnerRadius = 10,
                           IsRotated = true,
                           Rotation = -19,
                           Fill =brush
                       });
            }
  
        }
    }
}
