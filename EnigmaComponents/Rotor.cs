using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public class Rotor : Component, IRotor
    {
        public int Position { get; private set; }
        public int NotchPosition { get; }
        public string Name { get; }
        public int RingSetting { get; private set; }

        public Rotor(string name, List<int> config, int notchPosition, int position = 0, int ringSetting = 0) : base(config)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            NotchPosition = notchPosition;
            Position = position;
            RingSetting = ringSetting;
            SetPosition(position);
        }

        public void Rotate(int steps = 1)
        {
            if (steps <= 0) return;

            for (int step = 0; step < steps; step++)
            {
                Position = (Position + 1) % EnigmaConfiguration.AlphabetSize;
            }
        }

        public void SetPosition(int position)
        {
            if (position < 0 || position >= EnigmaConfiguration.AlphabetSize)
            {
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 0 and {EnigmaConfiguration.AlphabetSize - 1}");
            }

            Position = position;
        }

        public void SetRingSetting(int ringSetting)
        {
            if (ringSetting < 0 || ringSetting >= EnigmaConfiguration.AlphabetSize)
            {
                throw new ArgumentOutOfRangeException(nameof(ringSetting), $"Ring setting must be between 0 and {EnigmaConfiguration.AlphabetSize - 1}");
            }

            RingSetting = ringSetting;
        }

        public bool IsAtNotch()
        {
            // Check if the rotor is at the notch position (will cause the next rotor to step)
            return Position == NotchPosition;
        }

        public override void Reset()
        {
            base.Reset();
            Position = 0;
        }

        public override int Encode(int input, bool isBeforeReflector = true)
        {
            ValidateInput(input);
            
            // For a real Enigma rotor, the encoding depends on the direction
            // and involves the ring setting and position
            
            // Apply ring setting and position offsets
            int adjustedInput = (input + RingSetting + Position) % EnigmaConfiguration.AlphabetSize;
            
            // Encode through the rotor wiring
            int encoded = base.Encode(adjustedInput, isBeforeReflector);
            
            // Remove the offsets
            int result = (encoded - RingSetting - Position + EnigmaConfiguration.AlphabetSize * 2) % EnigmaConfiguration.AlphabetSize;
            
            return result;
        }
    }
}
