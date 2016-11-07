using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Matrix = Savchin.Wpf.Drawing.Matrix;

namespace Savchin.Wpf.Shapes
{
    public sealed class PolyStar : Shape
    {
        static PolyStar()
        {


        }

        private static DependencyProperty DependencyProp(string name, System.Type type, object defaultValue)
        {
            FrameworkPropertyMetadata fpm = new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.AffectsRender);
            DependencyProperty dp = DependencyProperty.Register(name, type, typeof(PolyStar), fpm);
            return dp;
        }

        private static DependencyProperty DependencyProp(string name, System.Type type, object defaultValue, PropertyChangedCallback callback)
        {
            FrameworkPropertyMetadata fpm = new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.AffectsRender, callback);
            DependencyProperty dp = DependencyProperty.Register(name, type, typeof(PolyStar), fpm);
            return dp;
        }



        PolyMatrixBuilder pBuilder = new PolyMatrixBuilder();
        OverallTransformation pOverall = new OverallTransformation();
        Matrix mat;

        public Matrix PointMatrix
        {
            get { return mat; }

        }

        #region PolyMatrixBuilder Props

        public static DependencyProperty SidesProperty = DependencyProp("Sides", typeof(int), 3);
        public int Sides
        {
            get { return (int)GetValue(SidesProperty); }
            set
            {
                SetValue(SidesProperty, value);
            }


        }
        public static DependencyProperty RadiusProperty = DependencyProp("Radius", typeof(double), 30.0);
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set
            {
                SetValue(RadiusProperty, value);
            }

        }



        public static DependencyProperty ShapeTypeProperty = DependencyProp("ShapeType", typeof(EShapeType), EShapeType.Polygon);
        public EShapeType ShapeType
        {
            get { return (EShapeType)GetValue(ShapeTypeProperty); }
            set
            {
                SetValue(ShapeTypeProperty, value);

            }

        }

        public static DependencyProperty InnerRadiusProperty = DependencyProp("InnerRadius", typeof(double), 10.0);
        public double InnerRadius
        {
            get { return (double)GetValue(InnerRadiusProperty); }
            set
            {
                SetValue(InnerRadiusProperty, value);

            }


        }
        public static DependencyProperty IsAngularlyOffsetProperty = DependencyProp("IsAngularlyOffset", typeof(bool), false, IsAngularlyOffsetCallBack);
        public bool IsAngularlyOffset
        {
            get { return (bool)GetValue(IsAngularlyOffsetProperty); }
            set
            {
                SetValue(IsAngularlyOffsetProperty, value);
            }

        }

        static void IsAngularlyOffsetCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            if (!(bool)args.NewValue)
            {
                PolyStar pTemp = (PolyStar)property;
                pTemp.AngularOffset = 0;
            }
        }



        public static DependencyProperty AngularOffsetProperty = DependencyProp("AngularOffset", typeof(double), 0.0);
        public double AngularOffset
        {
            get { return (double)GetValue(AngularOffsetProperty); }
            set
            {
                SetValue(AngularOffsetProperty, value);

            }

        }
        public static DependencyProperty IsBeveledProperty = DependencyProp("IsBeveled", typeof(bool), false, IsBeveledCallBack);
        public Boolean IsBeveled
        {
            get { return (bool)GetValue(IsBeveledProperty); }
            set
            {
                SetValue(IsBeveledProperty, value);
            }
        }


        static void IsBeveledCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            if (!(bool)args.NewValue)
            {
                PolyStar pTemp = (PolyStar)property;
                pTemp.BevelOffset = 0;
            }
        }


        public static DependencyProperty BevelOffsetProperty = DependencyProp("BevelOffset", typeof(double), 0.0);
        public double BevelOffset
        {
            get { return (double)GetValue(BevelOffsetProperty); }
            set
            {
                SetValue(BevelOffsetProperty, value);

            }
        }

        #endregion

        #region OverallTransformationProps

        public static DependencyProperty IsRotatedProperty = DependencyProp("IsRotated", typeof(bool), false, IsRotatedCallBack);
        public bool IsRotated
        {
            get { return (bool)GetValue(IsRotatedProperty); }
            set
            {
                SetValue(IsRotatedProperty, value);
            }

        }
        static void IsRotatedCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            if (!(bool)args.NewValue)
            {
                PolyStar pTemp = (PolyStar)property;
                pTemp.Rotation = 0;
            }
        }


        public static DependencyProperty IsAspectedProperty = DependencyProp("IsAspected", typeof(bool), false, IsAspectedCallBack);
        public bool IsAspected
        {
            get { return (bool)GetValue(IsAspectedProperty); }
            set
            {
                SetValue(IsAspectedProperty, value);
            }

        }
        static void IsAspectedCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            if (!(bool)args.NewValue)
            {
                PolyStar pTemp = (PolyStar)property;
                pTemp.Aspect = 0;
            }
        }
        public static DependencyProperty RotationProperty = DependencyProp("Rotation", typeof(double), 0.0);
        public double Rotation
        {

            get { return (double)GetValue(RotationProperty); }
            set
            {
                SetValue(RotationProperty, value);
            }
        }

        public static DependencyProperty AspectProperty = DependencyProp("Aspect", typeof(double), 0.0);
        public double Aspect
        {

            get { return (double)GetValue(AspectProperty); }
            set
            {
                SetValue(AspectProperty, value);
            }
        }


        #endregion





        protected override System.Windows.Media.Geometry DefiningGeometry
        {
            get
            {


                PathGeometry geom = new PathGeometry();
                PathFigureCollection pfc = geom.Figures;
                PathFigure pf = new PathFigure();
                pf.IsClosed = true;

                pBuilder.Sides = Sides;
                pBuilder.Radius = Radius;
                pBuilder.IsAngularlyOffset = IsAngularlyOffset;
                pBuilder.AngularOffset = AngularOffset;
                pBuilder.InnerRadius = InnerRadius;
                pBuilder.ShapeType = ShapeType;
                pBuilder.IsBeveled = IsBeveled;
                pBuilder.BevelOffset = BevelOffset;


                mat = pBuilder.GeneratePolygon();

                pOverall.IsAspected = IsAspected;
                pOverall.Aspect = Aspect;
                pOverall.IsRotated = IsRotated;
                pOverall.Rotation = Rotation;
                pOverall.Transform(mat);

                //double[] mins = mat.MinArray();
                mat.Add(Radius, 0);
                mat.Add(Radius, 1);


                double[] startpoint = mat.Row(0);
                pf.StartPoint = new System.Windows.Point(startpoint[0], startpoint[1]);
                for (int i = 1; i < mat.Height; i++)
                {
                    double[] cRow = mat.Row(i);
                    pf.Segments.Add(new LineSegment(new System.Windows.Point(cRow[0], cRow[1]), true));
                }


                pfc.Add(pf);
                return geom;

            }
        }


    }
}
