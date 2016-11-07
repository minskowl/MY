using System.Globalization;

namespace Savchin.Core
{
    public static  class DecimalHelper
    {

        // Be aware of how this method handles edge cases.
        // A few are counterintuitive, like the 0.0 case.
        // Also note that the goal is to report a precision
        // and scale that can be used to store the number in
        // an SQL DECIMAL type, so this does not correspond to
        // how precision and scale are defined for scientific
        // notation. The minimal precision SQL decimal can
        // be calculated by subtracting TrailingZeros as follows:
        // DECIMAL(Precision - TrailingZeros, Scale - TrailingZeros).
        //
        //     dec Precision Scale TrailingZeros
        // ------- --------- ----- -------------
        //   0             1     0             0
        // 0.0             2     1             1
        // 0.1             1     1             0
        // 0.01            2     2             0 [Diff result than ShowInfo]
        // 0.010           3     3             1 [Diff result than ShowInfo]
        // 12.45           4     2             0
        // 12.4500         6     4             2
        // 770             3     0             0
        public static DecimalInfo GetInfo(this decimal dec)
        {
            string s = dec.ToString(CultureInfo.InvariantCulture);

            int precision = 0;
            int scale = 0;
            int trailingZeros = 0;
            bool inFraction = false;
            bool nonZeroSeen = false;

            foreach (char c in s)
            {
                if (inFraction)
                {
                    if (c == '0')
                        trailingZeros++;
                    else
                    {
                        nonZeroSeen = true;
                        trailingZeros = 0;
                    }

                    precision++;
                    scale++;
                }
                else
                {
                    if (c == '.')
                    {
                        inFraction = true;
                    }
                    else if (c != '-')
                    {
                        if (c != '0' || nonZeroSeen)
                        {
                            nonZeroSeen = true;
                            precision++;
                        }
                    }
                }
            }

            // Handles cases where all digits are zeros.
            if (!nonZeroSeen)
                precision += 1;

            return new DecimalInfo(precision, scale, trailingZeros);
        }

    }


    public struct DecimalInfo
    {
        public int Precision { get; private set; }
        public int Scale { get; private set; }
        public int TrailingZeros { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecimalInfo"/> struct.
        /// </summary>
        /// <param name="precision">The precision.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="trailingZeros">The trailing zeros.</param>
        public DecimalInfo(int precision, int scale, int trailingZeros)
            : this()
        {
            Precision = precision;
            Scale = scale;
            TrailingZeros = trailingZeros;
        }
    }
}
