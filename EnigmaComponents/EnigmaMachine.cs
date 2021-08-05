using System;
using System.Collections.Generic;

namespace EnigmaComponents
{
    public class EnigmaMachine
    {
        int keyboardClicks;
        List<Component> componentList;
        Rotor rotorOne;
        Rotor rotorTwo;
        Rotor rotorThree;
        public static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public EnigmaMachine(Rotor rotorOne, Rotor rotorTwo, Rotor rotorThree, Component plugBoard, Component reflector, List<int> rotorPositions = null)
        {
            componentList = new List<Component> { plugBoard, rotorOne, rotorTwo, rotorThree, reflector, rotorThree, rotorTwo, rotorOne, plugBoard };
            keyboardClicks = 0;
            this.rotorOne = rotorOne;
            this.rotorTwo = rotorTwo;
            this.rotorThree = rotorThree;
            if (rotorPositions != null)
            {
                this.rotorOne.Rotate(rotorPositions[0]);
                this.rotorTwo.Rotate(rotorPositions[1]);
                this.rotorThree.Rotate(rotorPositions[2]);
            }
        }

        public static int ConvertLetterToNumber(char c)
        {
            return Array.IndexOf(EnigmaMachine.alphabet, c);
        }

        public char EncodeChar(char keyPressed)
        {
            keyboardClicks++;
            Rotate();
            int currentLetterInNumber = EnigmaMachine.ConvertLetterToNumber(keyPressed);
            for (int i = 0; i < componentList.Count; i++)
            {
                bool isBeforeReflector = true;
                if (i > 4)
                {
                    isBeforeReflector = false;
                }
                if (i == 0 || i == 4 || i == 8)
                {
                    currentLetterInNumber = componentList[i].Encode(currentLetterInNumber);
                }
                else
                {
                    currentLetterInNumber = componentList[i].Encode(currentLetterInNumber, isBeforeReflector);
                }
            }
            return alphabet[currentLetterInNumber];
        }
        public string EncodeString(string inputString)

        {
            string encodedString = "Encoded text: ";
            string inputLowerCase = inputString.ToLower();
            for (int i = 0; i < inputLowerCase.Length; i++)
            {
                encodedString += EncodeChar(inputLowerCase[i]);
            }
            return encodedString;
        }

        public void Rotate()
        {
            if (keyboardClicks % (26 * 26) == 0)
            {
                rotorThree.Rotate();
            }
            if (keyboardClicks % 26 == 0)
            {
                rotorTwo.Rotate();
            }
            rotorOne.Rotate();
        }
    }
}