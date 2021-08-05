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
            Console.WriteLine("Line 15");
            Console.WriteLine(letter);
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
            Console.WriteLine("Do I ever get called?");
            Console.WriteLine(letter);
            if (letter < 13)
            {
                return config[letter];
            }
            return config.IndexOf(letter);
        }
    }
}