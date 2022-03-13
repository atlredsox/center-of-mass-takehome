// David Zobel
// Class: SignalStrengthGrid
// Description: A class that represents the a grid of X, Y coordinates with 
// each cell containing a specified strength value.
// 

using System;
using System.Collections.Generic;

namespace CentersOfMass
{
    public class SignalStrengthGrid : IWriteToLog
    {
        // two dimensional array representing the grid and strength values
        private int[,] m_grid = null;

        // threshold value for calculating the subregions
        private readonly int m_nThreshold = -1;

        // a grid that stores which cells have been visited when calculating the
        // subregions
        private VisitedGrid m_visited = null;

        // Method: Constructor
        // Description: Initializes the signal strength grid and all of the
        // member properties and variables.
        public SignalStrengthGrid(int[,] grid, int nThreshold)
        {
            m_grid = grid;
            m_nThreshold = nThreshold;
            m_visited = new VisitedGrid(m_grid.GetLength(0), m_grid.GetLength(1));
        }

        // Method: MeetsThreshold
        // Description: Returns true if the specified coordinate's strength value
        // meets the grids threshold.
        public bool MeetsThreshold(Coordinate coord)
        {
            int nStrength = GetStrength(coord);
            return (nStrength >= m_nThreshold);
        }

        // Method: GetStrength
        // Description: Returns the strength value for the specified coordinate.
        public int GetStrength(Coordinate coord)
        {
            int nStrength = 0;
            if (IsValidCoordinate(coord))
            {
                nStrength = m_grid[coord.X, coord.Y];
            }
            return nStrength;
        }

        // Method: IsValidCoordinate
        // Description: Returns true if the specified coordinate is valid
        // for the grid. For instance, (-1, -1) would be invalid.
        public bool IsValidCoordinate(Coordinate coord)
        {
            return IsValidCoordinate(coord.X, coord.Y);
        }

        // Method: IsValidCoordinate
        // Description: Returns true if the specified X and Y values are
        // valid for the grid. For instance, (-1, -1) would be invalid.
        public bool IsValidCoordinate(int x, int y)
        {
            bool bIsValid = (x >= 0 && x < m_grid.GetLength(0)) &&
                (y >= 0 && y < m_grid.GetLength(1));
            return bIsValid;
        }

        // Method: FindSubregions
        // Description: Finds all of the contiguous subregions inside the grid
        // with strength values that meet the threshold value.
        public SubregionList FindSubregions()
        {
            SubregionList subregions = new SubregionList();
            // loop through all of the cells in the grid
            for (int x=0; x < m_grid.GetLength(0); x++)
            {
                for (int y=0; y < m_grid.GetLength(1); y++)
                {
                    Coordinate coord = CreateCoordinate(x, y);
                    // if this cell meets the threshold and hasn't been visited yet...
                    if (MeetsThreshold(coord) && (!m_visited.HasVisited(coord)))
                    {
                        // create a new subregion, add this coordinate to it,
                        // and mark it as visited
                        Subregion subRegion = new Subregion();
                        subRegion.Add(coord);
                        m_visited.SetVisited(coord);
                        // loop through all of the neighbors of this coordinate to
                        // find cells that also meet the threshold
                        FindNeighbors(coord, subRegion);
                        // add the new subregion to the list of subregions
                        subregions.Add(subRegion);
                    }
                }
            }
            return subregions;
        }

        // Method: FindNeighbors
        // Description: Checks all of the neighbors of a given coordinate. If that
        // neighbor hasn't been visited and meets the threshold, then it will be
        // added to the subregion and then all it's neighbors will be checked.
        private void FindNeighbors(Coordinate coord, Subregion subRegion)
        {
            // loop through all nine coordinates surrounding this coordinate
            // (which includes this coordinate
            for (int x = -1; x <= 1 ; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Coordinate newCoord = CreateCoordinate(coord.X + x, coord.Y + y);
                    // if this is a valid, it's not the existing coord, and we
                    // haven't visited it...
                    if (IsValidCoordinate(newCoord) && !coord.Equals(newCoord) && 
                        !m_visited.HasVisited(newCoord))
                    {
                        // if it meets the threshold then we add this coordinate
                        // to the region, mark it as visited, and check it's neighbors
                        if (MeetsThreshold(newCoord))
                        {
                            subRegion.Add(newCoord);
                            m_visited.SetVisited(newCoord);
                            FindNeighbors(newCoord, subRegion);
                        }
                        // otherwise, even if it doesn't meet the threshold, we mark
                        // it was visited
                        else
                        {
                            m_visited.SetVisited(newCoord);
                        }
                    }
                }
            }
        }

        // Method: CreateCoordinate
        // Description: Creates a coordinate with the specified
        // X and Y values and the corresponding strength value in
        // the grid.
        private Coordinate CreateCoordinate(int x, int y)
        {
            int nStrength = 0;
            if (IsValidCoordinate(x, y))
            {
                nStrength = m_grid[x, y];
            }
            return new Coordinate(x, y, nStrength);
        }

        // Method: WriteToLog
        // Description: Writes the signal strength grid to the log.
        public void WriteToLog(string sHeader)
        {
            Log.Debug($"{sHeader}:\n");
            for (int y=m_grid.GetLength(0)-1; y >= 0; y--)
            {
                for (int x=0; x < m_grid.GetLength(0); x++)
                {
                    Log.Debug($"{m_grid[x, y]}".PadLeft(5));
                }
                Log.Debug("\n");
            }
            Log.Debug($"\nThreshold: {m_nThreshold}\n\n");
        }
    }
}
