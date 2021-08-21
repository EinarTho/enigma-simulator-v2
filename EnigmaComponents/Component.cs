using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
   public class Component
    {
        protected List<int> config;
        public Component(List<int> config)
        {
            this.config = config;
        }
        public int Encode(int letter, bool isBeforeReflector)
        {
            foreach(int i in config)
            {
                Console.WriteLine(i);
            }
            if (isBeforeReflector)
            {
                return config[letter];
            }
            return config.IndexOf(letter);
        }
        public int Encode(int letter)
        {
            if (letter < 13)
            {
                return config[letter];
            }
            return config.IndexOf(letter);
        }
    }
}