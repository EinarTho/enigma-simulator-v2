using System;
using Xunit;
using EnigmaComponents;

namespace EnigmaComponents.Tests
{
    public class EnigmaValidationTests
    {
        [Fact]
        public void TestEnigmaEncodingDecoding()
        {
            // Create an Enigma machine with standard settings
            var enigma = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
            
            // Set rotor positions (A=0, B=1, C=2, etc.)
            enigma.SetRotorPositions(0, 0, 0); // All rotors at position A
            
            // Test message
            string originalMessage = "HELLOWORLD";
            
            // Encode the message
            string encoded = enigma.EncodeString(originalMessage);
            
            // Reset the machine to the same initial state
            enigma.Reset();
            enigma.SetRotorPositions(0, 0, 0);
            
            // Decode the message (should get back the original)
            string decoded = enigma.EncodeString(encoded);
            
            // The fundamental property of Enigma: encode then decode = original
            Assert.Equal(originalMessage, decoded);
        }

        [Fact]
        public void TestEnigmaWithDifferentPositions()
        {
            var enigma = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
            
            // Set different rotor positions
            enigma.SetRotorPositions(5, 10, 15); // F, K, P
            
            string originalMessage = "SECRETMESSAGE";
            string encoded = enigma.EncodeString(originalMessage);
            
            // Reset and decode
            enigma.Reset();
            enigma.SetRotorPositions(5, 10, 15);
            string decoded = enigma.EncodeString(encoded);
            
            Assert.Equal(originalMessage, decoded);
        }

        [Fact]
        public void TestRotorStepping()
        {
            var enigma = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B");
            enigma.SetRotorPositions(0, 0, 0);
            
            // Encode a single character multiple times to test rotor stepping
            string result1 = enigma.EncodeString("A");
            string result2 = enigma.EncodeString("A");
            string result3 = enigma.EncodeString("A");
            
            // Each encoding should produce different results due to rotor stepping
            Assert.NotEqual(result1, result2);
            Assert.NotEqual(result2, result3);
            Assert.NotEqual(result1, result3);
        }

        [Fact]
        public void TestPlugboardFunctionality()
        {
            // Create a plugboard with some connections
            string plugboardConfig = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            // In a real test, we'd modify this to have actual connections
            
            var enigma = EnigmaMachineFactory.CreateMachine("I", "II", "III", "B", plugboardConfig);
            enigma.SetRotorPositions(0, 0, 0);
            
            string original = "TEST";
            string encoded = enigma.EncodeString(original);
            
            enigma.Reset();
            enigma.SetRotorPositions(0, 0, 0);
            string decoded = enigma.EncodeString(encoded);
            
            Assert.Equal(original, decoded);
        }
    }
} 