using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UI
{
    public static class EnigmaUI
    {
        private const int MaxRotorPosition = 25;
        private const int MinRotorPosition = 0;

        public static List<int> GetRotorPositions()
        {
            var rotorPositions = new List<int>();
            var rotorNames = new[] { "left", "middle", "right" };

            Console.WriteLine("=== Enigma Machine Configuration ===");
            Console.WriteLine($"Enter rotor positions (0-{MaxRotorPosition}):");

            for (int i = 0; i < 3; i++)
            {
                rotorPositions.Add(GetValidRotorPosition(rotorNames[i]));
            }

            return rotorPositions;
        }

        public static string GetTextToEncode()
        {
            Console.WriteLine("\n=== Text Encoding ===");
            Console.WriteLine("Enter text to encode (letters only, spaces and punctuation will be preserved):");
            
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input;
        }

        public static bool AskToContinue()
        {
            Console.WriteLine("\nWould you like to encode another message? (y/n):");
            string response = Console.ReadLine()?.Trim().ToLower();
            return response == "y" || response == "yes";
        }

        public static void DisplayConfiguration(string configuration)
        {
            Console.WriteLine($"\nCurrent configuration: {configuration}");
        }

        public static void DisplayResult(string original, string encoded)
        {
            Console.WriteLine("\n=== Encoding Result ===");
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Encoded:  {encoded}");
        }

        public static void DisplayWelcome()
        {
            Console.WriteLine("=== Enigma Machine Simulator ===");
            Console.WriteLine("This simulator encodes text using the same principles as the historical Enigma machine.");
            Console.WriteLine();
        }

        private static int GetValidRotorPosition(string rotorName)
        {
            while (true)
            {
                Console.Write($"Enter position for {rotorName} rotor (0-{MaxRotorPosition}): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int position))
                {
                    if (position >= MinRotorPosition && position <= MaxRotorPosition)
                    {
                        return position;
                    }
                    else
                    {
                        Console.WriteLine($"Position must be between {MinRotorPosition} and {MaxRotorPosition}.");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        public static void DisplayError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }
    }
}
