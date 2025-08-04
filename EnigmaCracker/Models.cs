using System;

namespace EnigmaCracker
{
    public enum CrackingStrategy
    {
        BruteForce,        // Try all possible rotor combinations
        KnownRotorTypes,   // Assume we know rotor types but not positions
        KnownRotorPositions, // Assume we know positions but not rotor types
        QuickTest          // Limited attempts for testing
    }

    public class CrackingParameters
    {
        public CrackingStrategy Strategy { get; set; } = CrackingStrategy.KnownRotorTypes;
        public int MaxAttempts { get; set; } = 100000;
        public double ConfidenceThreshold { get; set; } = 0.7;
    }

    public class CrackingResult
    {
        public bool Success { get; set; }
        public string DecodedMessage { get; set; }
        public string Configuration { get; set; }
        public int AttemptsMade { get; set; }
        public TimeSpan TimeElapsed { get; set; }
        public CrackingGuess BestGuess { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class CrackingGuess
    {
        public string DecodedMessage { get; set; }
        public string Configuration { get; set; }
        public double Score { get; set; }
    }
} 