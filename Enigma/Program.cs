using System;
using System.Collections.Generic;
using EnigmaComponents;
using UI;

namespace Enigma
{
    class Program
    {
        static void Main(string[] args)
        {
            var plugboard = "ADOBJNTHVEKMLFCWQRXGYIPSUZ"; //letter on the position representing itself is not encoded
            var reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
            var rotorOne = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
            var rotorTwo = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
            var rotorThree = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
            var rotorFour = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
            var rotorFive = "BDFHJLCPRTXVZNYEIWGAKMUSQO";§


            bool programClose = false;
            while (!programClose)
            {
                List<int> rotorPositions = InputFunctions.GetRotorInput();
                string textToEncode = InputFunctions.GetTextToEncode();
                EnigmaMachine clapperOne = InitMachine.InitialiseMachine(plugboard, reflector, rotorOne, rotorTwo, rotorThree, rotorPositions);
                Console.WriteLine(clapperOne.EncodeString(textToEncode));
                Console.WriteLine("Would you like to encode another text? Press y for yes any other key for no");
                string shouldContinueInput = Console.ReadLine();
                if (shouldContinueInput != "y")
                {
                    programClose = true;
                }
            }
        }
    }
}
