using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UI
{
    public static class InputFunctions
    {
        public static List<int> GetRotorInput()
        {
            List<int> rotorPositions = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                bool isAnswerinRightFormat = false;
                while (!isAnswerinRightFormat)
                {
                    Console.WriteLine("Hello! Type the in the position of rotor 1 (number between 0 and 25):");
                    string answer = Console.ReadLine();
                    isAnswerinRightFormat = Int32.TryParse(answer, out int rotorPosition);
                    if (isAnswerinRightFormat && rotorPosition > -1 && rotorPosition < 26) //this way of doing it is a bit strange
                    {
                        rotorPositions.Add(rotorPosition);
                    }
                }
            }
            return rotorPositions;
        }
        public static string GetTextToEncode()
        {
            bool isTextInRightFormat = false;
            string textToEncode = "";
            while (!isTextInRightFormat)
            {
                Console.WriteLine("Type in the string you which to decode (only characters in the english alphabet)");
                textToEncode = Console.ReadLine();
                isTextInRightFormat = true;
                Regex rx = new Regex("^[a-zA-Z]*$");
                Console.WriteLine(rx.IsMatch(textToEncode));
                if (!rx.IsMatch(textToEncode))
                {
                    isTextInRightFormat = false;
                }
            }
            return textToEncode;
        }
    }
}
