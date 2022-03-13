// David Zobel
// Class: Log
// Description: A class that provides basic logging. Currently it only writes 
// to the console, but it can easily be extended to write to a file.

using System;

namespace CentersOfMass
{
    public static class Log
    {
        // Enables or disables writing to the console
        public static bool WriteToConsoleEnabled
        {
            get;
            set;
        } = true;

        // Enables or disables waiting for a key press
        public static bool WaitForKeyPressEnabled
        {
            get;
            set;
        } = true;

        // Method: Debug
        // Description: Writes a debug message to the log.
        public static void Debug(string sMessage)
        {
            if (WriteToConsoleEnabled)
            {
                Console.Write(sMessage);
            }
        }

        // Method: Error
        // Description: Writes an error message to the log.
        public static void Error(string sMessage)
        {
            if (WriteToConsoleEnabled)
            {
                Console.Write($"Error: {sMessage}");
            }
        }

        // Method: WaitForKeyPress
        // Description: Waits for the user to press a key before continuing.
        public static void WaitForKeyPress()
        {
            if (WaitForKeyPressEnabled)
            {
                Console.Write("\n>> Press Any Key to Continue ");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
