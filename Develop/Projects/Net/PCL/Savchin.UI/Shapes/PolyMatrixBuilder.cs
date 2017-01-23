using System;
using Savchin.UI.Transformations;

namespace Savchin.UI.Shapes
{
    public enum EShapeType
    {
        Polygon,
        Star

    }
    public class PolyMatrixBuilder
    {

        private int mSides;
        private double mRadius;
        private double mInnerRadius;
        private EShapeType mShapeType;
        bool mIsAngularlyOffset;
        private double mAngularOffset;
        private bool mIsBeveled;
        private double mBevelOffset;

        public bool IsBeveled
        {
            get { return mIsBeveled; }
            set
            {
                mIsBeveled = value;
            }

        }
        public int Sides
        {
            get { return mSides; }
            set
            {
                mSides = value;
            }
        }


        public double Radius
        {
            get { return mRadius; }
            set
            {
                mRadius = value;
            }

        }

        public double BevelOffset
        {
            get { return mBevelOffset; }
            set
            {
                mBevelOffset = value;
            }

        }

        public bool IsAngularlyOffset
        {
            get { return mIsAngularlyOffset; }
            set
            {
                mIsAngularlyOffset = value;
            }

        }

        public double AngularOffset
        {
            get { return mAngularOffset; }
            set
            {
                mAngularOffset = value;
            }

        }


        public EShapeType ShapeType
        {
            get { return mShapeType; }
            set
            {
                mShapeType = value;
            }

        }
        public double InnerRadius
        {
            get { return mInnerRadius; }
            set
            {
                mInnerRadius = value;

            }


        }

        public Matrix GeneratePolygon()
        {


            if (mShapeType == EShapeType.Polygon)
            {
                Matrix mat = new Matrix(2);
                for (int i = 0; i < mSides; i++)
                {
                    double x = mRadius * Math.Cos((2 * Math.PI / mSides) * i);
                    double y = mRadius * Math.Sin((2 * Math.PI / mSides) * i);
                    mat.AddRow(new Double[] { x, y });
                }


                if (mIsBeveled)
                {
                    Matrix bevelmat = (Matrix)mat.Clone();
                    RotationalTransformation rot = new RotationalTransformation() { Matrix = bevelmat };
                    rot.Matrix = bevelmat;
                    rot.RotateDegreesAroundOrigin(mBevelOffset);
                    mat.Interleave(bevelmat, false);
                    rot.Matrix = mat;
                    rot.RotateDegreesAroundOrigin(-mBevelOffset / 2);
                }


                return mat;
            }
            else
            {


                Matrix outerMat = new Matrix(2);
                for (int i = 0; i < mSides; i++)
                {
                    double x = mRadius * Math.Cos((2 * Math.PI / mSides) * i);
                    double y = mRadius * Math.Sin((2 * Math.PI / mSides) * i);
                    outerMat.AddRow(new Double[] { x, y });
                }

                Matrix innerMat = (Matrix)outerMat.Clone();
                innerMat.Multiply(mInnerRadius / mRadius); // Will work because centered about origin
                RotationalTransformation rot = new RotationalTransformation() { Matrix = innerMat };
                rot.RotateRadiansAroundOrigin(Math.PI / mSides); //Circle is 2PI radians 
                if (mIsAngularlyOffset)
                {
                    rot.RotateDegreesAroundOrigin(mAngularOffset);
                }
                outerMat.Interleave(innerMat, false);

                if (mIsBeveled)
                {
                    Matrix bevelmat = (Matrix)outerMat.Clone();
                    rot.Matrix = bevelmat;
                    rot.RotateDegreesAroundOrigin(mBevelOffset);
                    outerMat.Interleave(bevelmat, false);
                    rot.Matrix = outerMat;
                    rot.RotateDegreesAroundOrigin(-mBevelOffset / 2);//Remove appearance of rotation 
                }

                return outerMat;

            }
        }



    }
}
