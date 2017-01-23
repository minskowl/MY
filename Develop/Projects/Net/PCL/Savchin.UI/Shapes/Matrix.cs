using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.UI.Shapes
{
    public class Matrix //: ICloneable
    {


        private List<double[]> mMatrix = new List<double[]>();
        private int mWidth;

        public int Width
        {
            get { return mWidth; }
        }



        public int Height
        {
            get { return mMatrix.Count; }
        }

        public Matrix(int width)
        {

            mWidth = width;
        }



        public void AddRow(double[] row)
        {
            if (row.Length != mWidth)
            {
                throw new Exception("Wrong Size Array");
            }
            mMatrix.Add(row);
        }

        public override string ToString()
        {

            switch (mMatrix.Count)
            {
                case 0:
                    return "";

                default:
                    StringBuilder sb = new StringBuilder();
                    for (int iRow = 0; iRow < mMatrix.Count; iRow++)
                    {
                        for (int iCol = 0; iCol < mWidth; iCol++)
                        {
                            sb.Append(mMatrix[iRow][iCol].ToString());

                            if (iCol != mWidth - 1) sb.Append(",");
                        }
                        if (iRow != mMatrix.Count - 1) sb.Append(" ");

                    }

                    return sb.ToString();
            }

        }

        public Double[] Row(int value)
        {
            return mMatrix[value];


        }

        public Double[] Column(int col)
        {
            Double[] column = new Double[Height];
            for (int iRow = 0; iRow < mMatrix.Count; iRow++)
            {
                column[iRow] = mMatrix[iRow][col];

            }
            return column;

        }


        #region Bounds

        public Double[] MinArray()
        {
            double[] mins = new double[mWidth];
            for (int iCol = 0; iCol < mWidth; iCol++)
            {
                mins[iCol] = Double.MaxValue;
            }
            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < row.Length; iCol++)
                {
                    if (row[iCol] < mins[iCol])
                    {
                        mins[iCol] = row[iCol];

                    }

                }

            }
            return mins;

        }


        public Double[] MaxArray()
        {
            double[] maxes = new double[mWidth];
            for (int iCol = 0; iCol < mWidth; iCol++)
            {
                maxes[iCol] = Double.MinValue;
            }
            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < row.Length; iCol++)
                {
                    if (row[iCol] > maxes[iCol])
                    {
                        maxes[iCol] = row[iCol];

                    }

                }

            }
            return maxes;

        }


        public Double Min(int column)
        {
            if (mMatrix.Count < 0) return Double.NaN;

            double minVal = Double.MaxValue;
            foreach (double[] row in mMatrix)
            {
                if (row[column] < minVal)
                {
                    minVal = row[column];

                }

            }

            return minVal;
        }

        public Double Max(int column)
        {
            if (mMatrix.Count < 0) return Double.NaN;

            double maxVal = Double.MinValue;
            foreach (double[] row in mMatrix)
            {
                if (row[column] > maxVal)
                {
                    maxVal = row[column];

                }

            }

            return maxVal;


        }


        #endregion

        public void TransposeColumns(int col1, int col2)
        {
            double temp;
            foreach (Double[] row in mMatrix)
            {
                temp = row[col1];
                row[col1] = row[col2];
                row[col2] = temp;
            }


        }


        #region Scalar operations

        public void Add(double amount)
        {

            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < mWidth; iCol++)
                {
                    row[iCol] += amount;
                }
            }

        }


        public void Add(Double amount, int col)
        {
            foreach (double[] row in mMatrix)
            {
                row[col] += amount;
            }
        }


        public void Subtract(double amount)
        {


            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < mWidth; iCol++)
                {
                    row[iCol] -= amount;
                }
            }
        }


        public void Subtract(Double amount, int col)
        {


            foreach (double[] row in mMatrix)
            {
                row[col] -= amount;
            }



        }


        public void Multiply(double amount, int col)
        {
            foreach (double[] row in mMatrix)
            {
                row[col] *= amount;
            }

        }


        public void Multiply(double amount)
        {
            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < mWidth; iCol++)
                {
                    row[iCol] *= amount;
                }
            }

        }

        public void Divide(double amount)
        {

            foreach (double[] row in mMatrix)
            {
                for (int iCol = 0; iCol < mWidth; iCol++)
                {
                    row[iCol] /= amount;
                }
            }

        }


        public void Divide(double amount, int col)
        {
            foreach (double[] row in mMatrix)
            {
                row[col] /= amount;
            }
        }






        #endregion

        public void MultiplyRows(Matrix mat)
        {
            if (mat.Height == mat.Width && mat.Width == Width)
            {
                List<Double[]> Columns = new List<double[]>();
                for (int iCol = 0; iCol < Width; iCol++)
                {
                    Columns.Add(mat.Column(iCol));
                }
                foreach (Double[] row in mMatrix)
                {
                    Double[] multRow = new Double[mWidth];
                    for (int iCol = 0; iCol < Width; iCol++)
                    {
                        multRow[iCol] = ScalarProduct(row, Columns[iCol]);
                    }

                    for (int iCol = 0; iCol < Width; iCol++)
                    {
                        row[iCol] = multRow[iCol];

                    }
                }
            }
            else
            {
                throw new Exception("Wrong Dimensions");
            }


        }

        private static double ScalarProduct(Double[] vect1, Double[] vect2)
        {
            if (vect1.Length != vect2.Length) { throw new Exception("Vectors not of same length"); }
            double product = 0;
            for (int i = 0; i < vect1.Length; i++)
            {
                product += vect1[i] * vect2[i];

            }
            return product;

        }

        public void Interleave(Matrix mat, bool placeFirst)
        { //Note that the matrix mat is not changed
            if (mat.Width != mWidth | mat.Height != mMatrix.Count) { throw new Exception("Wrong Dimensions"); }
            List<double[]> newMatrix = new List<double[]>();
            if (placeFirst)
            {
                for (int iRow = 0; iRow < mMatrix.Count; iRow++)
                {
                    newMatrix.Add(((double[])mat.Row(iRow).Clone()));
                    newMatrix.Add(mMatrix[iRow]);
                }
            }
            else
            {
                for (int iRow = 0; iRow < mMatrix.Count; iRow++)
                {
                    newMatrix.Add(mMatrix[iRow]);
                    newMatrix.Add(((double[])mat.Row(iRow).Clone()));
                }
            }
            mMatrix = newMatrix;
        }






        #region ICloneable Members

        public object Clone()
        {
            Matrix mat = new Matrix(mWidth);
            foreach (double[] row in mMatrix)
            {
                mat.AddRow((double[])row.Clone());

            }
            return mat;
        }

        #endregion
    }
}
