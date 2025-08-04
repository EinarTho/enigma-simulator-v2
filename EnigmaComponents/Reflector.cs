using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public class Reflector : Component
    {
        public Reflector(string configuration) : base(CreateReflectorConfig(configuration))
        {
        }

        private static List<int> CreateReflectorConfig(string configuration)
        {
            if (string.IsNullOrEmpty(configuration) || configuration.Length != EnigmaConfiguration.AlphabetSize)
            {
                throw new ArgumentException("Reflector configuration must be exactly 26 characters long", nameof(configuration));
            }

            var config = new List<int>();
            foreach (char c in configuration.ToUpper())
            {
                config.Add(EnigmaMachine.ConvertLetterToNumber(c));
            }

            // Validate that the reflector is properly configured (each letter maps to a different letter)
            for (int i = 0; i < config.Count; i++)
            {
                if (config[i] == i)
                {
                    throw new ArgumentException($"Reflector cannot map letter {i} to itself", nameof(configuration));
                }
                
                // Check that the mapping is bidirectional
                if (config[config[i]] != i)
                {
                    throw new ArgumentException($"Reflector mapping is not bidirectional for letter {i}", nameof(configuration));
                }
            }

            return config;
        }

        public override int Encode(int input, bool isBeforeReflector = true)
        {
            ValidateInput(input);
            
            // Reflector always works the same way regardless of direction
            return _config[input];
        }

        public override void Reset()
        {
            // Reflector doesn't need reset as it's static
        }
    }
} 