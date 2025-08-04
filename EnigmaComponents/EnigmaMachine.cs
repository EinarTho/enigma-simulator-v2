using System;
using System.Collections.Generic;
using System.Text;

namespace EnigmaComponents
{
    public class EnigmaMachine
    {
        private readonly IRotor _leftRotor;
        private readonly IRotor _middleRotor;
        private readonly IRotor _rightRotor;
        private readonly Plugboard _plugboard;
        private readonly Reflector _reflector;
        private int _keyPressCount;

        public static readonly char[] Alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        public EnigmaMachine(IRotor leftRotor, IRotor middleRotor, IRotor rightRotor, Plugboard plugboard, Reflector reflector)
        {
            _leftRotor = leftRotor ?? throw new ArgumentNullException(nameof(leftRotor));
            _middleRotor = middleRotor ?? throw new ArgumentNullException(nameof(middleRotor));
            _rightRotor = rightRotor ?? throw new ArgumentNullException(nameof(rightRotor));
            _plugboard = plugboard ?? throw new ArgumentNullException(nameof(plugboard));
            _reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
            _keyPressCount = 0;
        }

        public static int ConvertLetterToNumber(char c)
        {
            if (!char.IsLetter(c))
            {
                throw new ArgumentException("Input must be a letter", nameof(c));
            }
            return Array.IndexOf(Alphabet, char.ToUpper(c));
        }

        public static char ConvertNumberToLetter(int number)
        {
            if (number < 0 || number >= Alphabet.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(number), $"Number must be between 0 and {Alphabet.Length - 1}");
            }
            return Alphabet[number];
        }

        public char EncodeChar(char input)
        {
            if (!char.IsLetter(input))
            {
                throw new ArgumentException("Input must be a letter", nameof(input));
            }

            _keyPressCount++;
            RotateRotors();
            
            int currentSignal = ConvertLetterToNumber(input);
            
            // Forward path: Plugboard -> Right Rotor -> Middle Rotor -> Left Rotor -> Reflector
            currentSignal = _plugboard.Encode(currentSignal);
            currentSignal = _rightRotor.Encode(currentSignal, true);
            currentSignal = _middleRotor.Encode(currentSignal, true);
            currentSignal = _leftRotor.Encode(currentSignal, true);
            currentSignal = _reflector.Encode(currentSignal);
            
            // Return path: Left Rotor -> Middle Rotor -> Right Rotor -> Plugboard
            currentSignal = _leftRotor.Encode(currentSignal, false);
            currentSignal = _middleRotor.Encode(currentSignal, false);
            currentSignal = _rightRotor.Encode(currentSignal, false);
            currentSignal = _plugboard.Encode(currentSignal);
            
            return ConvertNumberToLetter(currentSignal);
        }

        public string EncodeString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    result.Append(EncodeChar(c));
                }
                else
                {
                    result.Append(c); // Keep non-letters as-is
                }
            }
            return result.ToString();
        }

        private void RotateRotors()
        {
            // Check if middle rotor is at notch position (will cause left rotor to step)
            bool middleAtNotch = _middleRotor.IsAtNotch();
            
            // Check if right rotor is at notch position (will cause middle rotor to step)
            bool rightAtNotch = _rightRotor.IsAtNotch();
            
            // Enigma stepping mechanism:
            // 1. If middle rotor is at notch, both left and middle rotors step
            // 2. If right rotor is at notch, middle rotor steps
            // 3. Right rotor always steps
            
            if (middleAtNotch)
            {
                _leftRotor.Rotate();
                _middleRotor.Rotate();
            }
            else if (rightAtNotch)
            {
                _middleRotor.Rotate();
            }
            
            // Right rotor always rotates
            _rightRotor.Rotate();
        }

        public void SetRotorPositions(int left, int middle, int right)
        {
            _leftRotor.SetPosition(left);
            _middleRotor.SetPosition(middle);
            _rightRotor.SetPosition(right);
        }

        public void Reset()
        {
            _keyPressCount = 0;
            _leftRotor.Reset();
            _middleRotor.Reset();
            _rightRotor.Reset();
            _plugboard.Reset();
            _reflector.Reset();
        }

        public string GetCurrentConfiguration()
        {
            return $"Rotors: {_leftRotor.Name}({_leftRotor.Position}) {_middleRotor.Name}({_middleRotor.Position}) {_rightRotor.Name}({_rightRotor.Position}) | Plugboard: {_plugboard.GetConnectionCount()} connections";
        }
    }
}