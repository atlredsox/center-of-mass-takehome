// David Zobel
// Class: VisitedGrid
// Description: A grid that contains boolean values for each cell. This is
// used to track which cells have been visited while calculating the 
// subregions so that we don't reevaluate a cell we've already visited.

using System;

namespace CentersOfMass
{
    public class VisitedGrid
    {
        // two dimensional boolean array used to represent which cells have
        // been visited.
        private bool[,] m_visited = null;

        // Method: Constructor
        // Description: Creates and initializes the visited grid.
        public VisitedGrid(int nNumRows, int nNumColumns)
        {
            // create and initialize the grid.
            m_visited = new bool[nNumRows, nNumColumns];
            for (int x = 0; x < nNumRows; x++)
            {
                for (int y = 0; y < nNumColumns; y++)
                {
                    m_visited[x, y] = false;
                }
            }
        }

        // Method: HasVisited
        // Desciption: Returns true if the specified coordinate has already
        // been visited.
        public bool HasVisited(Coordinate coord)
        {
            if (IsValidCoordinate(coord))
            {
                return m_visited[coord.X, coord.Y];
            }
            return false;
        }

        // Method: SetVisited
        // Description: Sets the specified coordinate as having been visited.
        public void SetVisited(Coordinate coord)
        {
            if (IsValidCoordinate(coord))
            {
                m_visited[coord.X, coord.Y] = true;
            }
        }

        // Method: IsValidCoordinate
        // Decription: Returns true if the given coordinate is valid for the
        // grid. For example, -1, -1 would return false.
        private bool IsValidCoordinate(Coordinate coord)
        {
            bool bIsValid = (coord.X >= 0 && coord.X < m_visited.GetLength(0)) &&
                (coord.Y >= 0 && coord.Y < m_visited.GetLength(1));
            return bIsValid;
        }
    }
}
