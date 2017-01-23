using System;
using System.ComponentModel;
using Savchin.UI.Shapes;

namespace Savchin.UI.Transformations
{
    public class LinearTransformation : INotifyPropertyChanged, ITransformation
    {
        private double mLeft;
        private double mTop;
        private double mBottom;
        private double mRight;
        private double mShiftX;
        private double mShiftY;
        private double mStretchX = 1;
        private double mStretchY = 1;


        private bool mUniformStretch;
        private bool mTopLeftOrigin;
        private bool mBoundingBox;
        private bool mFixTopLeft;

        private Matrix mMatrix;

        public Matrix Matrix
        {

            get { return mMatrix; }
            set { mMatrix = value; }
        }


        public bool FixTopLeft
        {
            get { return mFixTopLeft; }
            set
            {
                mFixTopLeft = value;
                NotifyPropertyChanged("FixTopLeft");
            }


        }
        public double StretchX
        {
            get { return mStretchX; }
            set
            {
                mStretchX = value;
                NotifyPropertyChanged("StretchX");
            }
        }
        public double StretchY
        {
            get { return mStretchY; }
            set
            {
                mStretchY = value;
                NotifyPropertyChanged("StretchY");
            }
        }


        public double ShiftX
        {
            get { return mShiftX; }
            set
            {
                mShiftX = value;
                NotifyPropertyChanged("ShiftX");
            }


        }

        public double ShiftY
        {
            get { return mShiftY; }
            set
            {
                mShiftY = value;
                NotifyPropertyChanged("ShiftY");
            }


        }

        public bool BoundingBox
        {
            get { return mBoundingBox; }
            set
            {
                mBoundingBox = value;
                NotifyPropertyChanged("BoundingBox");
            }
        }



        public bool UniformStretch
        {
            get { return mUniformStretch; }
            set
            {
                mUniformStretch = value;
                NotifyPropertyChanged("UniformStretch");
            }
        }
        public bool TopLeftOrigin
        {
            get { return mTopLeftOrigin; }
            set
            {
                mTopLeftOrigin = value;
                NotifyPropertyChanged("TopLeftOrigin");
            }
        }



        public double Left
        {
            get { return mLeft; }
            set
            {
                mLeft = value;
                NotifyPropertyChanged("Left");
            }
        }
        public double Top
        {
            get { return mTop; }
            set
            {
                mTop = value;
                NotifyPropertyChanged("Top");
            }
        }
        public double Bottom
        {
            get { return mBottom; }
            set
            {
                mBottom = value;
                NotifyPropertyChanged("Bottom");
            }
        }

        public double Right
        {
            get { return mRight; }
            set
            {
                mRight = value;
                NotifyPropertyChanged("Right");
            }
        }


        public double Height
        {
            get { return Bottom - Top; }
            set
            {
                if (value > 0)
                {
                    Bottom = value + Top;
                    NotifyPropertyChanged("Bottom");
                    NotifyPropertyChanged("Height");
                }
            }
        }
        public double Width
        {
            get { return Right - Left; }
            set
            {
                if (value > 0)
                {
                    Right = value + Left;
                    NotifyPropertyChanged("Right");
                    NotifyPropertyChanged("Width");
                }
            }

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }


        #endregion



        #region shift

        public void ShiftMinTo()
        {
            for (int iCol = 0; iCol < mMatrix.Width; iCol++)
            {
                ShiftMinTo(0, iCol);
            }
        }
        public void ShiftMinTo(int col)
        {
            ShiftMinTo(col, 0);
        }
        public void ShiftMinTo(double min, int col)
        {
            double existingMin = mMatrix.Min(col);
            double shift;
            if (existingMin > min)
            {
                shift = existingMin - min;
                ShiftMinus(shift, col);

            }
            else if (existingMin < min)
            {
                shift = min - existingMin;
                ShiftPlus(shift, col);
            }

        }

        public void ShiftMinus(double shift, int col)
        {
            mMatrix.Subtract(shift, col);


        }

        public void ShiftPlus(double shift, int col)
        {
            mMatrix.Add(shift, col);

        }


        #endregion


        #region Scale


        public void ScaleTo(double value, int col)
        {

            double min = mMatrix.Min(col);
            double max = mMatrix.Max(col);

            double originalDimension = max - min;

            double scale = value / originalDimension;
            mMatrix.Multiply(scale, col);
            ShiftMinus(mMatrix.Min(col) - min, col);
        }


        public void ScaleUniform(double amount, bool upperLeftFixed)
        {
            if (upperLeftFixed)
            {
                Double[] mins = mMatrix.MinArray();
                mMatrix.Multiply(amount);
                for (int iCol = 0; iCol < mMatrix.Width; iCol++)
                {
                    ShiftMinTo(mins[iCol], iCol);
                }
            }
            else
            {
                mMatrix.Multiply(amount);
            }
        }


        public void ScaleTo(double[] amounts)
        {
            if (amounts.Length != mMatrix.Width) { throw new Exception("wrong Lenght Array"); }
            for (int iCol = 0; iCol < Width; iCol++)
            {
                mMatrix.Multiply(amounts[iCol], iCol);

            }


        }

        public void ScaleUniformTo(double[] amounts, bool upperLeftFixed)
        {
            Double[] mins = mMatrix.MinArray();
            Double[] maxes = mMatrix.MaxArray();
            Double[] scales = new Double[mMatrix.Width];
            for (int iCol = 0; iCol < Width; iCol++)
            {
                scales[iCol] = amounts[iCol] / (maxes[iCol] - mins[iCol]);
            }

            double minScale = Double.MaxValue;
            foreach (double d in scales)
            {
                if (d < minScale) { minScale = d; }
            }
            ScaleUniform(minScale, upperLeftFixed);
        }



        #endregion


        public void Reset()
        {
            Double[] mins = mMatrix.MinArray();
            Double[] maxes = mMatrix.MaxArray();
            Left = mins[0];
            Top = mins[1];
            Bottom = maxes[1];
            Right = maxes[0];
            ShiftX = 0;
            ShiftY = 0;
            StretchX = 1;
            StretchY = 1;
        }

        public void Transform()
        {
            if (mBoundingBox)
            {
                Double currentWidth = mMatrix.Max(0) - mMatrix.Min(0);
                Double currentHeight = mMatrix.Max(1) - mMatrix.Min(1);
                Double ScaleX = (Right - Left) / currentWidth;
                Double ScaleY = (Bottom - Top) / currentHeight;

                if (mUniformStretch)
                {
                    mMatrix.Multiply(Math.Min(ScaleX, ScaleY));
                }
                else
                {
                    mMatrix.Multiply(ScaleX, 0);
                    mMatrix.Multiply(ScaleY, 1);
                }

                if (mTopLeftOrigin)
                {
                    ShiftMinTo();
                }
                else
                {
                    ShiftMinTo(Left, 0);
                    ShiftMinTo(Top, 1);
                }
            }
            else
            {
                if (mTopLeftOrigin)
                {
                    ShiftMinTo();
                }
                else
                {
                    ShiftPlus(mShiftX, 0);
                    ShiftPlus(mShiftY, 1);
                }

                if (mUniformStretch)
                {
                    ScaleUniform(StretchX, mFixTopLeft);
                }
                else
                {
                    mMatrix.Multiply(StretchX, 0);
                    mMatrix.Multiply(StretchY, 1);
                }
            }

        }
    }
}
