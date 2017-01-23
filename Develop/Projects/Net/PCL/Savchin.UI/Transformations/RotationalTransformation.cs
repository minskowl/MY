using System;
using System.ComponentModel;
using Savchin.UI.Shapes;

namespace Savchin.UI.Transformations
{
    public enum ERotationalUnit
    {
        Degrees,
        Radians,
        PiRadians

    }

    public class RotationalTransformation : INotifyPropertyChanged, ITransformation
    {



        private double mAngle;
        public double Angle
        {
            get { return mAngle; }
            set
            {
                mAngle = value;
                NotifyPropertyChanged("Angle");
            }


        }

        private Matrix mMatrix;
        private ERotationalUnit mRotationalUnit = ERotationalUnit.Degrees;

        public ERotationalUnit RotationalUnit
        {
            get { return mRotationalUnit; }
            set
            {
                mRotationalUnit = value;
                NotifyPropertyChanged("RotationalUnit");
            }

        }



        private bool mIsRotatedAboutCenter = true;

        public bool IsRotatedAboutCenter
        {
            get { return mIsRotatedAboutCenter; }
            set
            {
                mIsRotatedAboutCenter = value;
                NotifyPropertyChanged("IsRotatedAboutCenter");
            }


        }

        public Matrix Matrix
        {

            get { return mMatrix; }
            set { mMatrix = value; }
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

        public void RotateDegreesAroundOrigin(double degrees)
        {
            Double radians = Math.PI * degrees / 180.0;
            RotateRadiansAroundOrigin(radians);
        }


        public void RotatePiRadiansAroundOrigin(double radians)
        {

            radians = Math.PI * radians;
            RotateRadiansAroundOrigin(radians);

        }

        public void RotateRadiansAroundOrigin(double radians)
        {
            Matrix mat = new Matrix(2);

            mat.AddRow(new Double[2] { Math.Cos(radians), Math.Sin(radians) });
            mat.AddRow(new Double[2] { -Math.Sin(radians), Math.Cos(radians) });

            mMatrix.MultiplyRows(mat);

        }

        #region ITransformation Members

        public void Transform()
        {
            double xMid = 0;
            double yMid = 0;

            if (mIsRotatedAboutCenter)
            {
                double[] MinArray = mMatrix.MinArray();
                double[] MaxArray = mMatrix.MaxArray();
                xMid = (MinArray[0] + MaxArray[0]) / 2;
                yMid = (MinArray[1] + MaxArray[1]) / 2;

                mMatrix.Subtract(xMid, 0);
                mMatrix.Subtract(yMid, 1);

            }
            switch (mRotationalUnit)
            {
                case ERotationalUnit.Degrees:
                    RotateDegreesAroundOrigin(mAngle);
                    break;
                case ERotationalUnit.PiRadians:
                    RotatePiRadiansAroundOrigin(mAngle);
                    break;
                case ERotationalUnit.Radians:
                    RotateRadiansAroundOrigin(mAngle);
                    break;
            }

            if (mIsRotatedAboutCenter)
            {
                mMatrix.Add(xMid, 0);
                mMatrix.Add(yMid, 1);
            }

        }

        #endregion
    }
}
