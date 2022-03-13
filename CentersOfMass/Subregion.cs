// David Zobel
// Class: Subregion
// Description: A class that contains a list of coordinates that represents
// a contiguous subregion of a larger grid.
// 
// NOTE: A method could be added that verifies that the subregion is contiguous.
// This would be a great method to write so that UnitTests could verify the
// subregions that are created are valid.

using System;
using System.Collections.Generic;

namespace CentersOfMass
{
    public class Subregion : IEnumerable<Coordinate>, IWriteToLog
    {
        // list of coordinates
        private List<Coordinate> m_cells = new List<Coordinate>();

        // Method: Constructor
        // Description: Creates and initializes the subregion
        public Subregion()
        {
        }

        // Method: Add
        // Description: Adds the specified coordinate to the subregion.
        public void Add(Coordinate coord)
        {
            if (!Contains(coord))
            {
                m_cells.Add(coord);
            }
        }

        // Method: Contains
        // Description: Returns true if the subregion contains the coordinate
        // specified. This method is verifying the values of the coordinate 
        // instead of the reference.
        public bool Contains(Coordinate coord)
        {
            foreach (Coordinate cell in m_cells)
            {
                if (cell.Equals(coord))
                {
                    return true;
                }
            }
            return false;
        }

        // Method: GetEnumerator
        // Description: Required override by IEnumerator of the GetEnumerator method 
        // so you can enumerate through the region.
        public IEnumerator<Coordinate> GetEnumerator()
        {
            return m_cells.GetEnumerator();
        }

        // Method: IEnumerable.GetEnumerator
        // Description: Required override by IEnumerator of the generic IEnumerable.GetEnumerator
        // method so you can enumerate through the region.
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Property: CenterOfMass
        // Description: Returns the center of mass of the subregion.
        public FloatCoordinate CenterOfMass
        {
            get
            {
                // calculate the weighted total for all of the X coordinates, the
                // weighted total for all the Y coordinates, and the total strength
                // for all the cells in the subregion.
                double dXWeightedTotal = 0;
                double dYWeightedTotal = 0;
                double dStrengthTotal = 0;
                foreach (Coordinate coord in m_cells)
                {
                    dXWeightedTotal += coord.X * coord.Strength;
                    dYWeightedTotal += coord.Y * coord.Strength;
                    dStrengthTotal += coord.Strength;
                }

                // calculate the center of mass for the X and Y coordinates
                double dXCenterOfMass = dXWeightedTotal / dStrengthTotal;
                double dYCenterOfMass = dYWeightedTotal / dStrengthTotal;

                return new FloatCoordinate(dXCenterOfMass, dYCenterOfMass);
            }
        }

        // Method: WriteToLog
        // Description: Writes the subregion to the log.
        public void WriteToLog(string sHeader)
        {
            Log.Debug(sHeader);
            if (m_cells == null || m_cells.Count == 0)
            {
                Log.Debug("None");
            }
            else
            {
                int i = 0;
                foreach (Coordinate cell in m_cells)
                {
                    if (i != 0)
                    {
                        Log.Debug(", ");
                    }
                    cell.WriteToLog("");
                    i++;
                }
            }
            Log.Debug("\n");
        }
    }
}
