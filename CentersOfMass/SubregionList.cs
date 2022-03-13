// David Zobel
// Class: SubregionList
// Description: A class that contains a list of subregions.

using System;
using System.Collections.Generic;

namespace CentersOfMass
{
    public class SubregionList : IEnumerable<Subregion>, IWriteToLog
    {
        // list of subregions
        private List<Subregion> m_regions = new List<Subregion>();

        // Method: Constructor
        // Description: Initializes the list of regions
        public SubregionList()
        {
        }

        // Method: Add
        // Description: Adds the specified subregion to the list.
        // NOTE: I could implement a check to make sure that subregion
        // specified doesn't already exist in the list, but it's not strictly
        // necessary for now.
        public void Add(Subregion subregion)
        {
            m_regions.Add(subregion);
        }

        // Method: GetEnumerator
        // Description: Required override by IEnumerator of the GetEnumerator method 
        // so you can enumerate through the list.
        public IEnumerator<Subregion> GetEnumerator()
        {
            return m_regions.GetEnumerator();
        }

        // Method: IEnumerable.GetEnumerator
        // Description: Required override by IEnumerator of the generic IEnumerable.GetEnumerator
        // method so you can enumerate through the list.
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Method: FindCentersOfMass
        // Description: Returns a list of floating point coordinates that 
        // represents the center of mass for each region in the list.
        public FloatCoordinateList FindCentersOfMass()
        {
            FloatCoordinateList centersOfMass = new FloatCoordinateList();

            foreach (Subregion region in m_regions)
            {
                centersOfMass.Add(region.CenterOfMass);
            }
            return centersOfMass;
        }

        // Method: WriteToLog
        // Description: Write the subregions to the log.
        public void WriteToLog(string sHeader)
        {
            Log.Debug($"{sHeader}: ");
            int i = 1;
            if ((m_regions == null) || (m_regions.Count == 0))
            {
                Log.Debug("None\n");
            }
            else
            {
                Log.Debug("\n");
                foreach (Subregion region in m_regions)
                {
                    region.WriteToLog($"Subregion {i}: ");
                    i++;
                }
            }
            Log.Debug("\n");
        }

    }
}
