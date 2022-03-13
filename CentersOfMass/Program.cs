// David Zobel
// Class: Program
// Description: Contains the main method and runs the program with some
// test data. 

using System;

namespace CentersOfMass
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
        }

        // Method: RunTests
        // Description: Runs a suite of tests against the GetCentersOfMass method.
        // NOTE: I used a simple array of doubles to specify expected values. The
        // array will always be a multiple of 2, with the data being coordinate pairs.
        // The first entry is the expected X value and the second entry is the expected
        // Y value. This is very crude, but I did it this way because of time constraints.
        public static void RunTests()
        {
            // first check a grid with only one entry that meets the threshold
            int[,] testGrid = new int[1, 1] {
                { 150 }
            };
            double[] expected = new double[2] { 0.0, 0.0 };
            RunTest(testGrid, 100, expected);

            // check a grid with only one entry that does not meet the threshold
            expected = new double[0];
            RunTest(testGrid, 200, expected);

            // check a 2x2 grid with all entries meeting the threshold
            testGrid = new int[2, 2] {
                { 100, 100 },
                { 100, 100 }
            };
            expected = new double[2] { 0.5, 0.5 };
            RunTest(testGrid, 100, expected);

            // check a 2x2 grid with half the entries meething the threshold
            testGrid = new int[2, 2]
            {
                { 100, 200 },
                { 100, 200 }
            };
            expected = new double[2] { 0.5, 1.0 };
            RunTest(testGrid, 200, expected);

            // check a 3x3 grid identical entries that all meet the threshold
            testGrid = new int[3, 3]
            {
                { 150, 150, 150 },
                { 150, 150, 150 },
                { 150, 150, 150 }
            };
            expected = new double[2] { 1.0, 1.0 };
            RunTest(testGrid, 100, expected);

            // check a 3x3 grid with a donut of identical values, but the
            // center is "heavier"
            testGrid[1, 1] = 300;
            RunTest(testGrid, 100, expected);

            // check a 3x3 grid with a donut of identical values, but the
            // center is beneath the threshold. Here the center of mass
            // is a coordinate that doesn't meet the threshold
            testGrid[1, 1] = 50;
            RunTest(testGrid, 100, expected);

            // check a 3x3 grid with two subregions
            // and several "just below threshold" values
            testGrid = new int[3, 3]
            {
                { 500, 0, 99 },
                { 0, 98, 500 },
                { 97, 500, 1000 }
            };
            expected = new double[4] { 0.0, 0.0, 1.75, 1.75 };
            RunTest(testGrid, 100, expected);

            // check a 4x4 grid with two subregions
            testGrid = new int[4, 4] {
                { 150, 150, 99, 99 },
                { 150, 99, 99, 99 },
                { 99, 99, 150, 150 },
                { 99, 99, 150, 150 } };
            expected = new double[4] { 0.33, 0.33, 2.5, 2.5 };
            RunTest(testGrid, 100, expected);

            // check the sample 6x6 grid that was given in the problem description
            testGrid = new int[6, 6] {
                { 0, 115, 5, 15, 0, 5 },
                { 80, 210, 0, 5, 5, 0 },
                { 45, 60, 145, 175, 95, 25},
                { 95, 5, 250, 250, 115, 5 },
                { 170, 230, 245, 185, 165, 145 },
                { 145, 220, 140, 160, 250, 250 },
                };
            expected = new double[6] { 1.0, 1.0, 3.77, 1.83, 5.0, 4.5 };
            RunTest(testGrid, 200, expected);
        }

        // Method: RunTest
        // Descrition: Runs a single test against the GetCentersOfMass method.
        public static void RunTest(int[,] grid, int nThreshold, double[] expected)
        {
            FloatCoordinateList centers = GetCentersOfMass(grid, nThreshold);
            string sResult = "Unknown. No expected result specified.";
            if (expected != null)
            {
                FloatCoordinateList listExpected = new FloatCoordinateList(expected);
                sResult = centers.IsIdentical(listExpected) ? "Passed" : "Failed";
            }
            Log.Debug($"\nTest Result: {sResult}\n");
            Log.WaitForKeyPress();
        }

        // Method: GetCentersOfMass
        // Description: This provides the functionality given in the original problem set.
        // Given a grid that contains integer values representing signal strength and a 
        // threshold value, this method will a.) calculate the continguous regions containing
        // cells with strengths above the threshold, and then b.) return a list of
        // X,Y floating point coordinates that indicate the center of mass of each region.
        public static FloatCoordinateList GetCentersOfMass(int[,] grid, int nThreshold)
        {
            SignalStrengthGrid strengthGrid = new SignalStrengthGrid(grid, nThreshold);
            strengthGrid.WriteToLog("Strength Grid");
            SubregionList subregions = strengthGrid.FindSubregions();
            subregions.WriteToLog("Found Subregions");
            FloatCoordinateList centers = subregions.FindCentersOfMass();
            centers.WriteToLog("Centers of Mass");
            return centers;
        }
    }
}
