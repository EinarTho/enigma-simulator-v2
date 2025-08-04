using System;
using System.Collections.Generic;
using System.Linq;

namespace EnigmaComponents
{
    public class Plugboard : Component
    {
        private readonly Dictionary<int, int> _connections;

        public Plugboard(string configuration) : base(CreatePlugboardConfig(configuration))
        {
            _connections = ParseConnections(configuration);
        }

        private static List<int> CreatePlugboardConfig(string configuration)
        {
            var config = new List<int>();
            for (int i = 0; i < EnigmaConfiguration.AlphabetSize; i++)
            {
                config.Add(i); // Default: each letter maps to itself
            }
            return config;
        }

        private Dictionary<int, int> ParseConnections(string configuration)
        {
            var connections = new Dictionary<int, int>();
            
            if (string.IsNullOrEmpty(configuration) || configuration.Length != EnigmaConfiguration.AlphabetSize)
            {
                return connections;
            }

            // Parse the configuration string to find connections
            for (int i = 0; i < configuration.Length; i++)
            {
                int currentLetter = i;
                int mappedLetter = EnigmaMachine.ConvertLetterToNumber(configuration[i]);
                
                if (currentLetter != mappedLetter && !connections.ContainsKey(currentLetter) && !connections.ContainsKey(mappedLetter))
                {
                    connections[currentLetter] = mappedLetter;
                    connections[mappedLetter] = currentLetter;
                }
            }

            return connections;
        }

        public override int Encode(int input, bool isBeforeReflector = true)
        {
            ValidateInput(input);
            
            // If there's a connection for this letter, return the connected letter
            return _connections.TryGetValue(input, out int connectedLetter) ? connectedLetter : input;
        }

        public override void Reset()
        {
            // Plugboard doesn't need reset as connections are static
        }

        public int GetConnectionCount()
        {
            return _connections.Count / 2; // Each connection is stored twice
        }
    }
} 