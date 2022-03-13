// David Zobel
// Class: Coordinate
// Description: A coordinate on a grid with both X and Y as integer point values.
// The Strength value represents a signal strength value for the cell at this position.
//
// Note: Instead  of using this class, the code could have been implemented using a Tuple. 
// However, Tuples can be hard to read. For instance, instead of the code reading "coord.X" 
// and "coord.Y", it would read "coord.Item1" and "coord.Item2", respectively. to me, this
// is not nearly as readable.

using System;

namespace CentersOfMass
{
    public class Coordinate : IWriteToLog
    {
        // X Coordinate
        public int X
        {
            get;
            private set;
        } = -1;

        // Y Coordinate
        public int Y
        {
            get;
            private set;
        } = -1;

        // Signal Strength
        public int Strength
        {
            get;
            set;
        } = 0;

        // Method: Constructor
        // Description: Creates a coordinate with the specified X and Y values.
        // the strength value is initialized to 0.
        public Coordinate (int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Strength = 0;
        }

        // Method: Constructor
        // Description: Creates a coordinate with the specified X, Y, and 
        // strength values.
        public Coordinate(int x, int y, int strength)
        {
            this.X = x;
            this.Y = y;
            this.Strength = strength;
        }

        // Method: Equals
        // Description: This overrides the default Equals operator since that
        // simply compares references instead of values. This method onoly returns 
        // true if the specified object is a coordinate with identical values for 
        // X and Y. The strength value could be compared as well, but it's not
        // strictly necessary for now.
        public override bool Equals(object obj)
        {
            if (obj is Coordinate)
            {
                Coordinate dest = obj as Coordinate;
                return (X == dest.X) && (Y == dest.Y);
            }
            return false;
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
        // Description: Writes the coordinate to the log. This could easily
        // be extended to include the strength value as well.
        public void WriteToLog(string sHeader)
        {
            Log.Debug($"({X}, {Y})");
        }
    }
}
