using System.Collections.Generic;

namespace EnigmaComponents
{
    public static class InitMachine
    {
        public static EnigmaMachine InitialiseMachine(string plugBoardConfig, string reflectorConfig, string rotorOneConfig, string rotorTwoConfig, string rotorThreeConfig, List<int> rotorPositions = null)
        {
            Rotor rotorOne = new Rotor(MakeRotorConfig(rotorOneConfig), 1);
            Rotor rotorTwo = new Rotor(MakeRotorConfig(rotorTwoConfig), 2);
            Rotor rotorThree = new Rotor(MakeRotorConfig(rotorThreeConfig), 3);
            Component plugBoard = new Component(MakeReflectorOrPlugboardConfig(plugBoardConfig));
            Component reflector = new Component(MakeReflectorOrPlugboardConfig(reflectorConfig));
            EnigmaMachine machine = new EnigmaMachine(rotorOne, rotorTwo, rotorThree, plugBoard, reflector, rotorPositions);
            return machine;
        }
        private static List<int> MakeRotorConfig(string configString)
        {
            List<int> config = new List<int>();
            foreach (char c in configString.ToLower())
            {
                config.Add(EnigmaMachine.ConvertLetterToNumber(c));
            }
            return config;
        }
        private static List<int> MakeReflectorOrPlugboardConfig(string configString)
        {
            List<int> config = new List<int>();
            foreach (char c in configString.ToLower())
            {
                int letterAsInt = EnigmaMachine.ConvertLetterToNumber(c);
                if (letterAsInt > 12)
                {
                    config.Add(letterAsInt);
                }
            }
            return config;
        }
    }
}