// David Zobel
// Class: FloatCoordinate
// Description: A coordinate on a grid with both X and Y as floating point values.

using System;

namespace CentersOfMass
{
    public class FloatCoordinate : IWriteToLog
    {
        // X Coordinate
        public Double X
        {
            get;
            private set;
        } = -1.0;

        // Y Coordinate
        public Double Y
        {
            get;
            private set;
        } = -1.0;

        // Method: Constructor
        // Description: Creates a coordinate with the specified X and Y values.
        public FloatCoordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        // Method: Equals
        // Description: This overrides the default Equals operator since that
        // simply compares references instead of values. This method onoly returns 
        // true if the specified object is a coordinate with identical values for 
        // X and Y. The strength value could be compared as well, but it's not
        // strictly necessary for now.
        public override bool Equals(object obj)
        {
            if (obj is FloatCoordinate)
            {
                FloatCoordinate dest = obj as FloatCoordinate;
                return (AreDoublesEqual(X, dest.X) && AreDoublesEqual(Y, dest.Y));
            }
            return false;
        }

        // Method: AreDoublesEqual
        // Description: Returns true if the two double values are equal. The problem is 
        // that when comparing two floating points 0.333 is not equal to 0.33. So here
        // this method simply makes sure that they are the same up to two decimal places.
        public bool AreDoublesEqual(double value1, double value2)
        {
            // Handle comparisons of floating point values that may not be exactly the same
            // here we 
            return (Math.Abs(value1 - value2) < 0.005);
        }

        // Method: GetHashCode
        // Description: Override of the GetHashCode method that returns a hash
        // code for the Coordinate.
        // NOTE: When overriding Equals it is recommended that you also override
        // GetHashCode.
        public override int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }

        // Method: WriteToLog
        // Description: Writes the coordinate to the log.
        public void WriteToLog(string sHeader)
        {
            Log.Debug($"({X.ToString("F")}, {Y.ToString("F")})");
        }
    }
}
