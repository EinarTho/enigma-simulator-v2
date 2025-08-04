using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnigmaComponents;
using UI;

namespace EnigmaCracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Enigma Machine Cracker ===\n");
            Console.WriteLine("This program attempts to crack Enigma-encoded messages by trying different configurations.\n");

            try
            {
                // Get the encoded message from user
                Console.WriteLine("Enter the encoded message to crack:");
                string encodedMessage = Console.ReadLine()?.Trim().ToUpper();

                if (string.IsNullOrEmpty(encodedMessage))
                {
                    Console.WriteLine("No message provided. Exiting.");
                    return;
                }

                // Get cracking parameters
                var parameters = GetCrackingParameters();

                Console.WriteLine("\nStarting crack attempt...");
                Console.WriteLine($"Message: {encodedMessage}");
                Console.WriteLine($"Strategy: {parameters.Strategy}");
                Console.WriteLine($"Max attempts: {parameters.MaxAttempts:N0}");
                Console.WriteLine();

                // Start the cracking process
                var cracker = new EnigmaCrackerEngine();
                var result = await cracker.CrackMessageAsync(encodedMessage, parameters);

                // Display results
                DisplayResults(result, encodedMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static CrackingParameters GetCrackingParameters()
        {
            var parameters = new CrackingParameters();

            Console.WriteLine("Select cracking strategy:");
            Console.WriteLine("1. Brute force (try all rotor combinations)");
            Console.WriteLine("2. Known rotor types (try different positions)");
            Console.WriteLine("3. Known rotor positions (try different types)");
            Console.WriteLine("4. Quick test (limited attempts)");

            while (true)
            {
                Console.Write("Enter choice (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
                {
                    parameters.Strategy = (CrackingStrategy)(choice - 1);
                    break;
                }
                Console.WriteLine("Please enter a number between 1 and 4.");
            }

            if (parameters.Strategy == CrackingStrategy.QuickTest)
            {
                parameters.MaxAttempts = 1000;
            }
            else
            {
                Console.Write("Enter maximum attempts (default 100,000): ");
                if (int.TryParse(Console.ReadLine(), out int maxAttempts) && maxAttempts > 0)
                {
                    parameters.MaxAttempts = maxAttempts;
                }
            }

            return parameters;
        }

        static void DisplayResults(CrackingResult result, string originalMessage)
        {
            Console.WriteLine("\n=== Cracking Results ===\n");

            if (result.Success)
            {
                Console.WriteLine("✅ CRACKED SUCCESSFULLY!");
                Console.WriteLine($"Decoded message: {result.DecodedMessage}");
                Console.WriteLine($"Configuration: {result.Configuration}");
                Console.WriteLine($"Attempts made: {result.AttemptsMade:N0}");
                Console.WriteLine($"Time taken: {result.TimeElapsed.TotalSeconds:F2} seconds");
            }
            else
            {
                Console.WriteLine("❌ Failed to crack the message.");
                Console.WriteLine($"Attempts made: {result.AttemptsMade:N0}");
                Console.WriteLine($"Time taken: {result.TimeElapsed.TotalSeconds:F2} seconds");
                
                if (result.BestGuess != null)
                {
                    Console.WriteLine($"\nBest guess:");
                    Console.WriteLine($"Message: {result.BestGuess.DecodedMessage}");
                    Console.WriteLine($"Configuration: {result.BestGuess.Configuration}");
                    Console.WriteLine($"Score: {result.BestGuess.Score:F2}");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
} 