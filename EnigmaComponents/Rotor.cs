using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public class Rotor : Component
    {
        public int RotorNumber { get; set; }
        public int position;

        public Rotor(List<int> config, int rotorNumber, int position = 0) : base(config)
        {
            this.position = position;
            RotorNumber = rotorNumber;
            Rotate(position);
            Console.WriteLine("inside rotor");
            foreach (int i in config)
            {
                Console.WriteLine(i);
            }
        }

        public void Rotate(int steps = 1)
        {
            List<int> newCharList = new List<int>();
            for (int i = 0; i < config.Count; i++)
            {
                int newPosOfElement = i + steps;
                if (newPosOfElement >= config.Count)
                {
                    newPosOfElement -= config.Count;
                }
                newCharList.Add(config[newPosOfElement]);
            }
            config = newCharList;
        }
    }
}
