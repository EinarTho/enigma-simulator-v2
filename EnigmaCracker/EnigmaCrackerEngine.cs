using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EnigmaComponents;

namespace EnigmaCracker
{
    public class EnigmaCrackerEngine
    {
        private readonly Dictionary<string, double> _wordScores;
        private readonly Dictionary<string, double> _bigramScores;
        private readonly Dictionary<string, double> _trigramScores;

        public EnigmaCrackerEngine()
        {
            // Initialize language statistics for scoring
            _wordScores = InitializeWordScores();
            _bigramScores = InitializeBigramScores();
            _trigramScores = InitializeTrigramScores();
        }

        public async Task<CrackingResult> CrackMessageAsync(string encodedMessage, CrackingParameters parameters)
        {
            var stopwatch = Stopwatch.StartNew();
            var result = new CrackingResult();
            var bestGuess = new CrackingGuess { Score = double.MinValue };

            // Filter out non-letters for processing
            var cleanMessage = new string(encodedMessage.Where(char.IsLetter).ToArray());
            
            if (string.IsNullOrEmpty(cleanMessage))
            {
                result.Success = false;
                result.ErrorMessage = "No letters found in the message.";
                return result;
            }

            var attempts = 0;
            var maxAttempts = parameters.MaxAttempts;

            try
            {
                switch (parameters.Strategy)
                {
                    case CrackingStrategy.BruteForce:
                        var bruteResult = await BruteForceCrack(cleanMessage, maxAttempts, attempts, bestGuess);
                        attempts = bruteResult.attempts;
                        bestGuess = bruteResult.bestGuess;
                        break;
                    case CrackingStrategy.KnownRotorTypes:
                        var knownTypesResult = await KnownRotorTypesCrack(cleanMessage, maxAttempts, attempts, bestGuess);
                        attempts = knownTypesResult.attempts;
                        bestGuess = knownTypesResult.bestGuess;
                        break;
                    case CrackingStrategy.KnownRotorPositions:
                        var knownPosResult = await KnownRotorPositionsCrack(cleanMessage, maxAttempts, attempts, bestGuess);
                        attempts = knownPosResult.attempts;
                        bestGuess = knownPosResult.bestGuess;
                        break;
                    case CrackingStrategy.QuickTest:
                        var quickResult = await QuickTestCrack(cleanMessage, maxAttempts, attempts, bestGuess);
                        attempts = quickResult.attempts;
                        bestGuess = quickResult.bestGuess;
                        break;
                }

                stopwatch.Stop();
                result.TimeElapsed = stopwatch.Elapsed;
                result.AttemptsMade = attempts;

                // Check if we found a good solution
                if (bestGuess.Score > 0.7) // High confidence threshold
                {
                    result.Success = true;
                    result.DecodedMessage = bestGuess.DecodedMessage;
                    result.Configuration = bestGuess.Configuration;
                }
                else
                {
                    result.Success = false;
                    result.BestGuess = bestGuess;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private async Task<(int attempts, CrackingGuess bestGuess)> BruteForceCrack(string message, int maxAttempts, int attempts, CrackingGuess bestGuess)
        {
            var rotorTypes = EnigmaConfiguration.RotorWirings.Keys.ToArray();
            var reflectorTypes = EnigmaConfiguration.ReflectorWirings.Keys.ToArray();

            foreach (var leftRotor in rotorTypes)
            {
                foreach (var middleRotor in rotorTypes.Where(r => r != leftRotor))
                {
                    foreach (var rightRotor in rotorTypes.Where(r => r != leftRotor && r != middleRotor))
                    {
                        foreach (var reflector in reflectorTypes)
                        {
                            for (int leftPos = 0; leftPos < 26; leftPos++)
                            {
                                for (int middlePos = 0; middlePos < 26; middlePos++)
                                {
                                    for (int rightPos = 0; rightPos < 26; rightPos++)
                                    {
                                        if (attempts >= maxAttempts) return (attempts, bestGuess);

                                        var enigma = EnigmaMachineFactory.CreateMachine(leftRotor, middleRotor, rightRotor, reflector);
                                        enigma.SetRotorPositions(leftPos, middlePos, rightPos);

                                        var decoded = enigma.EncodeString(message);
                                        var score = ScoreMessage(decoded);

                                        if (score > bestGuess.Score)
                                        {
                                            bestGuess = new CrackingGuess
                                            {
                                                DecodedMessage = decoded,
                                                Configuration = $"Rotors: {leftRotor}({leftPos}) {middleRotor}({middlePos}) {rightRotor}({rightPos}) Reflector: {reflector}",
                                                Score = score
                                            };
                                        }

                                        attempts++;

                                        if (attempts % 1000 == 0)
                                        {
                                            await Task.Delay(1); // Allow UI updates
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return (attempts, bestGuess);
        }

        private async Task<(int attempts, CrackingGuess bestGuess)> KnownRotorTypesCrack(string message, int maxAttempts, int attempts, CrackingGuess bestGuess)
        {
            // Assume we know the rotor types but not positions
            var rotorTypes = new[] { "I", "II", "III" }; // Common configuration
            var reflector = "B";

            for (int leftPos = 0; leftPos < 26; leftPos++)
            {
                for (int middlePos = 0; middlePos < 26; middlePos++)
                {
                    for (int rightPos = 0; rightPos < 26; rightPos++)
                    {
                        if (attempts >= maxAttempts) return (attempts, bestGuess);

                        var enigma = EnigmaMachineFactory.CreateMachine(rotorTypes[0], rotorTypes[1], rotorTypes[2], reflector);
                        enigma.SetRotorPositions(leftPos, middlePos, rightPos);

                        var decoded = enigma.EncodeString(message);
                        var score = ScoreMessage(decoded);

                        if (score > bestGuess.Score)
                        {
                            bestGuess = new CrackingGuess
                            {
                                DecodedMessage = decoded,
                                Configuration = $"Rotors: {rotorTypes[0]}({leftPos}) {rotorTypes[1]}({middlePos}) {rotorTypes[2]}({rightPos}) Reflector: {reflector}",
                                Score = score
                            };
                        }

                        attempts++;

                        if (attempts % 1000 == 0)
                        {
                            await Task.Delay(1);
                        }
                    }
                }
            }
            
            return (attempts, bestGuess);
        }

        private async Task<(int attempts, CrackingGuess bestGuess)> KnownRotorPositionsCrack(string message, int maxAttempts, int attempts, CrackingGuess bestGuess)
        {
            // Assume we know the rotor positions but not types
            var positions = new[] { 0, 0, 0 }; // Common starting position
            var rotorTypes = EnigmaConfiguration.RotorWirings.Keys.ToArray();
            var reflectorTypes = EnigmaConfiguration.ReflectorWirings.Keys.ToArray();

            foreach (var leftRotor in rotorTypes)
            {
                foreach (var middleRotor in rotorTypes.Where(r => r != leftRotor))
                {
                    foreach (var rightRotor in rotorTypes.Where(r => r != leftRotor && r != middleRotor))
                    {
                        foreach (var reflector in reflectorTypes)
                        {
                            if (attempts >= maxAttempts) return (attempts, bestGuess);

                            var enigma = EnigmaMachineFactory.CreateMachine(leftRotor, middleRotor, rightRotor, reflector);
                            enigma.SetRotorPositions(positions[0], positions[1], positions[2]);

                            var decoded = enigma.EncodeString(message);
                            var score = ScoreMessage(decoded);

                            if (score > bestGuess.Score)
                            {
                                bestGuess = new CrackingGuess
                                {
                                    DecodedMessage = decoded,
                                    Configuration = $"Rotors: {leftRotor}({positions[0]}) {middleRotor}({positions[1]}) {rightRotor}({positions[2]}) Reflector: {reflector}",
                                    Score = score
                                };
                            }

                            attempts++;

                            if (attempts % 1000 == 0)
                            {
                                await Task.Delay(1);
                            }
                        }
                    }
                }
            }
            
            return (attempts, bestGuess);
        }

        private async Task<(int attempts, CrackingGuess bestGuess)> QuickTestCrack(string message, int maxAttempts, int attempts, CrackingGuess bestGuess)
        {
            // Quick test with limited combinations
            var commonConfigs = new[]
            {
                ("I", "II", "III", "B"),
                ("II", "III", "I", "B"),
                ("III", "I", "II", "B"),
                ("I", "II", "III", "A"),
                ("II", "III", "I", "A")
            };

            foreach (var (left, middle, right, reflector) in commonConfigs)
            {
                for (int leftPos = 0; leftPos < 26; leftPos += 2) // Skip every other position
                {
                    for (int middlePos = 0; middlePos < 26; middlePos += 2)
                    {
                        for (int rightPos = 0; rightPos < 26; rightPos += 2)
                        {
                            if (attempts >= maxAttempts) return (attempts, bestGuess);

                            var enigma = EnigmaMachineFactory.CreateMachine(left, middle, right, reflector);
                            enigma.SetRotorPositions(leftPos, middlePos, rightPos);

                            var decoded = enigma.EncodeString(message);
                            var score = ScoreMessage(decoded);

                            if (score > bestGuess.Score)
                            {
                                bestGuess = new CrackingGuess
                                {
                                    DecodedMessage = decoded,
                                    Configuration = $"Rotors: {left}({leftPos}) {middle}({middlePos}) {right}({rightPos}) Reflector: {reflector}",
                                    Score = score
                                };
                            }

                            attempts++;

                            if (attempts % 100 == 0)
                            {
                                await Task.Delay(1);
                            }
                        }
                    }
                }
            }
            
            return (attempts, bestGuess);
        }

        private double ScoreMessage(string message)
        {
            if (string.IsNullOrEmpty(message)) return 0;

            var score = 0.0;
            var words = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Score individual words
            foreach (var word in words)
            {
                if (_wordScores.ContainsKey(word.ToUpper()))
                {
                    score += _wordScores[word.ToUpper()];
                }
            }

            // Score bigrams
            for (int i = 0; i < message.Length - 1; i++)
            {
                var bigram = message.Substring(i, 2).ToUpper();
                if (_bigramScores.ContainsKey(bigram))
                {
                    score += _bigramScores[bigram];
                }
            }

            // Score trigrams
            for (int i = 0; i < message.Length - 2; i++)
            {
                var trigram = message.Substring(i, 3).ToUpper();
                if (_trigramScores.ContainsKey(trigram))
                {
                    score += _trigramScores[trigram];
                }
            }

            // Normalize by message length
            return score / Math.Max(message.Length, 1);
        }

        private Dictionary<string, double> InitializeWordScores()
        {
            // Common English words with their frequencies
            return new Dictionary<string, double>
            {
                {"THE", 6.42}, {"BE", 3.76}, {"TO", 3.76}, {"OF", 3.76}, {"AND", 3.76},
                {"A", 3.76}, {"IN", 3.76}, {"THAT", 3.76}, {"HAVE", 3.76}, {"I", 3.76},
                {"IT", 3.76}, {"FOR", 3.76}, {"NOT", 3.76}, {"ON", 3.76}, {"WITH", 3.76},
                {"HE", 3.76}, {"AS", 3.76}, {"YOU", 3.76}, {"DO", 3.76}, {"AT", 3.76},
                {"THIS", 3.76}, {"BUT", 3.76}, {"HIS", 3.76}, {"BY", 3.76}, {"FROM", 3.76},
                {"THEY", 3.76}, {"WE", 3.76}, {"SAY", 3.76}, {"HER", 3.76}, {"SHE", 3.76},
                {"OR", 3.76}, {"AN", 3.76}, {"WILL", 3.76}, {"MY", 3.76}, {"ONE", 3.76},
                {"ALL", 3.76}, {"WOULD", 3.76}, {"THERE", 3.76}, {"THEIR", 3.76}, {"WHAT", 3.76}
            };
        }

        private Dictionary<string, double> InitializeBigramScores()
        {
            // Common English bigrams
            return new Dictionary<string, double>
            {
                {"TH", 3.88}, {"HE", 3.68}, {"IN", 2.43}, {"ER", 2.05}, {"AN", 1.99},
                {"RE", 1.85}, {"ON", 1.76}, {"AT", 1.49}, {"EN", 1.45}, {"ND", 1.35},
                {"TI", 1.34}, {"ES", 1.34}, {"OR", 1.28}, {"TE", 1.20}, {"OF", 1.17},
                {"ED", 1.17}, {"IS", 1.13}, {"IT", 1.12}, {"AL", 1.09}, {"AR", 1.07},
                {"ST", 1.05}, {"TO", 1.04}, {"NT", 1.04}, {"NG", 0.95}, {"SE", 0.93},
                {"HA", 0.93}, {"AS", 0.87}, {"OU", 0.87}, {"IO", 0.83}, {"LE", 0.83},
                {"VE", 0.83}, {"CO", 0.79}, {"ME", 0.79}, {"DE", 0.76}, {"HI", 0.76},
                {"RI", 0.73}, {"RO", 0.73}, {"IC", 0.70}, {"NE", 0.69}, {"EA", 0.69}
            };
        }

        private Dictionary<string, double> InitializeTrigramScores()
        {
            // Common English trigrams
            return new Dictionary<string, double>
            {
                {"THE", 1.81}, {"AND", 0.73}, {"THA", 0.33}, {"ENT", 0.42}, {"ING", 0.72},
                {"ION", 0.42}, {"TIO", 0.36}, {"FOR", 0.34}, {"NDE", 0.35}, {"HAS", 0.30},
                {"NCE", 0.35}, {"EDT", 0.31}, {"TIS", 0.27}, {"OFT", 0.22}, {"STH", 0.21},
                {"MEN", 0.24}, {"CSE", 0.20}, {"HIS", 0.24}, {"ETH", 0.20}, {"FTH", 0.19},
                {"STI", 0.19}, {"HEN", 0.19}, {"HES", 0.19}
            };
        }
    }
} 