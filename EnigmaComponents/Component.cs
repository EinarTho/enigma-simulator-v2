using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public class Component : IComponent
    {
        protected readonly List<int> _config;
        protected readonly List<int> _originalConfig;

        public Component(List<int> config)
        {
            _config = new List<int>(config);
            _originalConfig = new List<int>(config);
        }

        public virtual int Encode(int input, bool isBeforeReflector = true)
        {
            if (input < 0 || input >= _config.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(input), "Input must be between 0 and 25");
            }

            return isBeforeReflector ? _config[input] : _config.IndexOf(input);
        }

        public virtual void Reset()
        {
            _config.Clear();
            _config.AddRange(_originalConfig);
        }

        protected void ValidateInput(int input)
        {
            if (input < 0 || input >= EnigmaConfiguration.AlphabetSize)
            {
                throw new ArgumentOutOfRangeException(nameof(input), $"Input must be between 0 and {EnigmaConfiguration.AlphabetSize - 1}");
            }
        }
    }
}