// David Zobel
// Class: FloatCoordinateList
// Description: A list of floating point coordinates. This class is used to
// return the list of the center of mass for each subregion. This class isn't 
// strictly necessary since the code could just contain List<FloatCoordinate>.
// But implementing it this way allows us to implement the WriteToLog method
// inside this class, which makes the SignalStrengthGrid class simpler.

using System;
using System.Collections.Generic;

namespace CentersOfMass
{
    public class FloatCoordinateList : IEnumerable<FloatCoordinate>, IWriteToLog
    {
        // List of floating point coordinates
        private List<FloatCoordinate> m_cells = new List<FloatCoordinate>();

        // Method: Constructor
        // Description: Creates an empty list of floating point coordinates.
        public FloatCoordinateList()
        {
        }

        // Method: Constructor
        // Description: Creates a list of floating point coordinates from the
        // passed floating point array
        public FloatCoordinateList(double[] initialValues)
        {
            if ((initialValues != null) && (initialValues.Length % 2 == 0))
            {
                for(int i=0; i<initialValues.Length;)
                {
                    m_cells.Add(new FloatCoordinate(initialValues[i], initialValues[i + 1]));
                    i += 2;
                }
            }
        }

        // Method: Add
        // Description: Adds the specified floating point coordinate to the list.
        public void Add(FloatCoordinate coord)
        {
            if (!Contains(coord))
            {
                m_cells.Add(coord);
            }
        }

        // Method: Count
        // Description: Returns the number of cells in the list
        public int Count
        {
            get
            {
                if (m_cells != null)
                {
                    return m_cells.Count;
                }
                return 0;
            }
        }

        // Method: Contains
        // Description: Returns true if the subregion contains the coordinate
        // specified. This method is verifying the values of the coordinate 
        // instead of the reference.
        public bool Contains(FloatCoordinate coord)
        {
            foreach (FloatCoordinate cell in m_cells)
            {
                if (cell.Equals(coord))
                {
                    return true;
                }
            }
            return false;
        }

        // Method: IsIdentical
        // Description: Returns true if the FloatCoordinateList is
        // identical to the current list.
        // NOTE: I should override the Equals operator here, but honestly 
        // I'm only ever going to compare this to a FloatCoordinateList, 
        // plus I wasn't quite sure how to implement the GetHashCode method 
        // for a List of objects. Because of time constraints I just decided 
        // to make this simple.
        public bool IsIdentical(FloatCoordinateList compare)
        {
            bool bEqual = false;
            if (Count == compare.Count)
            {
                bEqual = true;
                foreach (FloatCoordinate entry in compare)
                {
                    if (!Contains(entry))
                    {
                        bEqual = false;
                        break;
                    }
                }
            }
            return bEqual;
        }

        // Method: GetEnumerator
        // Description: Required override by IEnumerator of the GetEnumerator method 
        // so you can enumerate through the list.
        public IEnumerator<FloatCoordinate> GetEnumerator()
        {
            return m_cells.GetEnumerator();
        }

        // Method: IEnumerable.GetEnumerator
        // Description: Required override by IEnumerator of the generic IEnumerable.GetEnumerator
        // method so you can enumerate through the list.
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Method: WriteToLog
        // Description: Writes the list of coordinates to the log.
        public void WriteToLog(string sHeader)
        {
            Log.Debug($"{sHeader}: ");
            if ((m_cells == null) || (m_cells.Count == 0))
            {
                Log.Debug("None");
            }
            else
            {
                int i = 0;
                foreach (FloatCoordinate cell in m_cells)
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
