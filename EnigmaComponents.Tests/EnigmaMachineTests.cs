using System;
using Xunit;
using EnigmaComponents;

namespace EnigmaComponents.Tests
{
    public class EnigmaMachineTests
    {
        [Fact]
        public void TestLetterConversion()
        {
            // Test letter to number conversion
            Assert.Equal(0, EnigmaMachine.ConvertLetterToNumber('A'));
            Assert.Equal(25, EnigmaMachine.ConvertLetterToNumber('Z'));
            Assert.Equal(4, EnigmaMachine.ConvertLetterToNumber('E'));
            
            // Test number to letter conversion
            Assert.Equal('A', EnigmaMachine.ConvertNumberToLetter(0));
            Assert.Equal('Z', EnigmaMachine.ConvertNumberToLetter(25));
            Assert.Equal('E', EnigmaMachine.ConvertNumberToLetter(4));
        }

        [Fact]
        public void TestRotorCreation()
        {
            var rotor = new Rotor("I", new System.Collections.Generic.List<int> { 4, 10, 12, 5, 11, 6, 3, 16, 21, 25, 13, 19, 14, 22, 24, 7, 23, 20, 18, 15, 0, 8, 1, 17, 2, 9 }, 16, 0);
            
            Assert.Equal("I", rotor.Name);
            Assert.Equal(16, rotor.NotchPosition);
            Assert.Equal(0, rotor.Position);
        }

        [Fact]
        public void TestPlugboardCreation()
        {
            var plugboard = new Plugboard("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            Assert.Equal(0, plugboard.GetConnectionCount());
        }

        [Fact]
        public void TestReflectorCreation()
        {
            var reflector = new Reflector("EJMZALYXVBWFCRQUONTSPIKHGD");
            Assert.NotNull(reflector);
        }

        [Fact]
        public void TestEnigmaMachineFactory()
        {
            var machine = EnigmaMachineFactory.CreateDefaultMachine();
            Assert.NotNull(machine);
        }

        [Fact]
        public void TestConfigurationConstants()
        {
            Assert.Equal(26, EnigmaConfiguration.AlphabetSize);
            Assert.Equal(3, EnigmaConfiguration.RotorCount);
            Assert.True(EnigmaConfiguration.RotorWirings.ContainsKey("I"));
            Assert.True(EnigmaConfiguration.RotorWirings.ContainsKey("II"));
            Assert.True(EnigmaConfiguration.RotorWirings.ContainsKey("III"));
            Assert.True(EnigmaConfiguration.ReflectorWirings.ContainsKey("A"));
            Assert.True(EnigmaConfiguration.ReflectorWirings.ContainsKey("B"));
        }
    }
} 