using System;
using System.Collections.Generic;
using Xunit;

namespace EnigmaComponents.Tests
{
    public class MachineTestEncoding
    {
        string plugboard = "ADOBJNTHVEKMLFCWQRXGYIPSUZ"; //letter on the position representing itself is not encoded
        string reflector = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        string rotorOne = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        string rotorTwo = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        string rotorThree = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        List<int> rotorPositions = new List<int>{1,2,3};

        [Fact]
        public void Test1()
        {
            EnigmaMachine clapperOne = InitMachine.InitialiseMachine(plugboard, reflector, rotorOne, rotorTwo, rotorThree, rotorPositions);
            bool isEncodedCorrectly = clapperOne.EncodeString("hei")=="Encoded text: fjm";
            Assert.True(isEncodedCorrectly, "hei should become fjm");
        }
    }
}
