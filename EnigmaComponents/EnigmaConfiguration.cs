using System.Collections.Generic;

namespace EnigmaComponents
{
    public static class EnigmaConfiguration
    {
        public const int AlphabetSize = 26;
        public const int RotorCount = 3;
        
        // Standard Enigma rotor wirings
        public static readonly Dictionary<string, string> RotorWirings = new Dictionary<string, string>
        {
            { "I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ" },
            { "II", "AJDKSIRUXBLHWTMCQGZNPYFVOE" },
            { "III", "BDFHJLCPRTXVZNYEIWGAKMUSQO" },
            { "IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB" },
            { "V", "VZBRGITYUPSDNHLXAWMJQOFECK" }
        };

        // Standard Enigma reflectors
        public static readonly Dictionary<string, string> ReflectorWirings = new Dictionary<string, string>
        {
            { "A", "EJMZALYXVBWFCRQUONTSPIKHGD" },
            { "B", "YRUHQSLDPXNGOKMIEBFZCWVJAT" },
            { "C", "FVPJIAOYEDRZXWGCTKUQSBNMHL" }
        };

        // Default plugboard (no connections)
        public static readonly string DefaultPlugboard = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Notch positions for rotor stepping
        public static readonly Dictionary<string, int> RotorNotches = new Dictionary<string, int>
        {
            { "I", 16 },   // Q
            { "II", 4 },   // E
            { "III", 21 }, // V
            { "IV", 9 },   // J
            { "V", 25 }    // Z
        };
    }
} 