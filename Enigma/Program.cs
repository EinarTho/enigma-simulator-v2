using System;
using EnigmaComponents;
using UI;

namespace Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            // First, run validation tests to ensure the Enigma machine works correctly
            RunValidationTests();
            
            Console.WriteLine("\nPress any key to start the interactive Enigma simulator...");
            Console.ReadKey();
            Console.Clear();
            
            try
            {
                EnigmaUI.DisplayWelcome();

                // Create a default Enigma machine
                var enigmaMachine = EnigmaMachineFactory.CreateDefaultMachine();

                bool continueEncoding = true;
                while (continueEncoding)
                {
                    try
                    {
                        // Get rotor positions from user
                        var rotorPositions = EnigmaUI.GetRotorPositions();
                        enigmaMachine.SetRotorPositions(rotorPositions[0], rotorPositions[1], rotorPositions[2]);

                        // Display current configuration
                        EnigmaUI.DisplayConfiguration(enigmaMachine.GetCurrentConfiguration());

                        // Get text to encode
                        string textToEncode = EnigmaUI.GetTextToEncode();
                        if (!string.IsNullOrEmpty(textToEncode))
                        {
                            // Encode the text
                            string encodedText = enigmaMachine.EncodeString(textToEncode);
                            
                            // Display results
                            EnigmaUI.DisplayResult(textToEncode, encodedText);
                        }

                        // Ask if user wants to continue
                        continueEncoding = EnigmaUI.AskToContinue();
                    }
                    catch (Exception ex)
                    {
                        EnigmaUI.DisplayError(ex.Message);
                        Console.WriteLine("Please try again.");
                    }
                }

                Console.WriteLine("Thank you for using the Enigma Machine Simulator!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void RunValidationTests()
        {
            Console.WriteLine("=== Validating Enigma Machine Implementation ===\n");
            
            try
            {
                // Test 1: Basic encoding/decoding
                Console.WriteLine("Test 1: Basic encoding/decoding");
                var enigma1 = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
                enigma1.SetRotorPositions(0, 0, 0);
                
                string original1 = "HELLOWORLD";
                string encoded1 = enigma1.EncodeString(original1);
                Console.WriteLine($"Original: {original1}");
                Console.WriteLine($"Encoded:  {encoded1}");
                
                enigma1.Reset();
                enigma1.SetRotorPositions(0, 0, 0);
                string decoded1 = enigma1.EncodeString(encoded1);
                Console.WriteLine($"Decoded:  {decoded1}");
                Console.WriteLine($"Success: {original1 == decoded1}\n");
                
                // Test 2: Different rotor positions
                Console.WriteLine("Test 2: Different rotor positions");
                var enigma2 = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
                enigma2.SetRotorPositions(5, 10, 15); // F, K, P
                
                string original2 = "SECRETMESSAGE";
                string encoded2 = enigma2.EncodeString(original2);
                Console.WriteLine($"Original: {original2}");
                Console.WriteLine($"Encoded:  {encoded2}");
                
                enigma2.Reset();
                enigma2.SetRotorPositions(5, 10, 15);
                string decoded2 = enigma2.EncodeString(encoded2);
                Console.WriteLine($"Decoded:  {decoded2}");
                Console.WriteLine($"Success: {original2 == decoded2}\n");
                
                // Test 3: Rotor stepping
                Console.WriteLine("Test 3: Rotor stepping (each 'A' should encode differently)");
                var enigma3 = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
                enigma3.SetRotorPositions(0, 0, 0);
                
                string result1 = enigma3.EncodeString("A");
                string result2 = enigma3.EncodeString("A");
                string result3 = enigma3.EncodeString("A");
                
                Console.WriteLine($"First 'A':  {result1}");
                Console.WriteLine($"Second 'A': {result2}");
                Console.WriteLine($"Third 'A':  {result3}");
                Console.WriteLine($"All different: {result1 != result2 && result2 != result3 && result1 != result3}\n");
                
                Console.WriteLine("=== Validation tests completed successfully! ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
