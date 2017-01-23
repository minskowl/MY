using System;
using Savchin.UI.Shapes;

namespace Savchin.UI.Transformations
{
    internal class OverallTransformation
    {
        private bool mIsRotated;
        private bool mIsAspected;
        private double mRotation;
        private double mAspect;
        public Double Rotation
        {
            get { return mRotation; }
            set
            {
                mRotation = value;

            }
        }

        public bool IsRotated
        {

            get { return mIsRotated; }
            set
            {
                mIsRotated = value;

            }

        }

        public bool IsAspected
        {
            get { return mIsAspected; }
            set
            {
                mIsAspected = value;



            }
        }

        public double Aspect
        {
            get { return mAspect; }
            set
            {
                mAspect = value;

            }

        }

        public void Transform(Matrix mat)
        {

            RotationalTransformation rot = new RotationalTransformation();
            rot.Matrix = mat;
            rot.RotateDegreesAroundOrigin(mRotation);

            LinearTransformation lt = new LinearTransformation();
            lt.Matrix = mat;
            if (mAspect < 0)
            {
                lt.StretchX = 1 + mAspect;
                lt.Transform();
            }
            if (mAspect > 0)
            {
                lt.StretchY = 1 - +mAspect;
                lt.Transform();
            }

        }






    }
}
