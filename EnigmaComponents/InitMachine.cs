using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public static class EnigmaMachineFactory
    {
        public static EnigmaMachine CreateMachine(string leftRotorName, string middleRotorName, string rightRotorName, 
            string reflectorName, string plugboardConfig = null, int leftPosition = 0, int middlePosition = 0, int rightPosition = 0,
            int leftRingSetting = 0, int middleRingSetting = 0, int rightRingSetting = 0)
        {
            // Validate rotor names
            if (!EnigmaConfiguration.RotorWirings.ContainsKey(leftRotorName))
                throw new ArgumentException($"Invalid left rotor name: {leftRotorName}", nameof(leftRotorName));
            if (!EnigmaConfiguration.RotorWirings.ContainsKey(middleRotorName))
                throw new ArgumentException($"Invalid middle rotor name: {middleRotorName}", nameof(middleRotorName));
            if (!EnigmaConfiguration.RotorWirings.ContainsKey(rightRotorName))
                throw new ArgumentException($"Invalid right rotor name: {rightRotorName}", nameof(rightRotorName));

            // Validate reflector name
            if (!EnigmaConfiguration.ReflectorWirings.ContainsKey(reflectorName))
                throw new ArgumentException($"Invalid reflector name: {reflectorName}", nameof(reflectorName));

            // Create rotors
            var leftRotor = CreateRotor(leftRotorName, leftPosition, leftRingSetting);
            var middleRotor = CreateRotor(middleRotorName, middlePosition, middleRingSetting);
            var rightRotor = CreateRotor(rightRotorName, rightPosition, rightRingSetting);

            // Create plugboard
            var plugboard = new Plugboard(plugboardConfig ?? EnigmaConfiguration.DefaultPlugboard);

            // Create reflector
            var reflector = new Reflector(EnigmaConfiguration.ReflectorWirings[reflectorName]);

            return new EnigmaMachine(leftRotor, middleRotor, rightRotor, plugboard, reflector);
        }

        private static Rotor CreateRotor(string rotorName, int position, int ringSetting)
        {
            var wiring = EnigmaConfiguration.RotorWirings[rotorName];
            var notchPosition = EnigmaConfiguration.RotorNotches[rotorName];
            var config = CreateRotorConfig(wiring);
            
            return new Rotor(rotorName, config, notchPosition, position, ringSetting);
        }

        private static List<int> CreateRotorConfig(string configString)
        {
            if (string.IsNullOrEmpty(configString) || configString.Length != EnigmaConfiguration.AlphabetSize)
            {
                throw new ArgumentException($"Rotor configuration must be exactly {EnigmaConfiguration.AlphabetSize} characters long", nameof(configString));
            }

            var config = new List<int>();
            foreach (char c in configString.ToUpper())
            {
                config.Add(EnigmaMachine.ConvertLetterToNumber(c));
            }
            return config;
        }

        public static EnigmaMachine CreateDefaultMachine()
        {
            return CreateMachine("I", "II", "III", "B");
        }
    }
}