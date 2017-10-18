using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RomanNumeral {

    private string value;

    public RomanNumeral(string startValue) {
        value = startValue.ToUpper();
    }

    public int ToInt() {
        //If the one before is lower, then subtract from the one after it.
        int converted = 0;
        char[] numerals = value.ToCharArray().Reverse().ToArray();
        for (int i = 0; i < numerals.Length; i++) {
            if (i != numerals.Length - 1) {
                //If the character after is larger, then it is not a 4 or 9.
                if (Numeral.charTable[numerals[i + 1]] < Numeral.charTable[numerals[i]]) {
                    converted += Numeral.charTable[numerals[i]] - Numeral.charTable[numerals[i + 1]];
                    i++;
                }
                //This numeral must be represending a 4 or 9 with the next numeral.
                else {
                    converted += Numeral.charTable[numerals[i]];
                } 
            }
            else {
                converted += Numeral.charTable[numerals[i]];
            }
        }
        return converted;
    }

    public static string FromInt(int toConvert) {
        //We need special cases for calculating the roman numerals for 4,9s. For 9s we take the value its just below and negate that / 10 from it. (100's)
        //For 4s we do similar using 50's and 5's.
        string newValue = "";
        List<char> numList = Numeral.charTable.Keys.ToList();
        numList.Reverse();
        foreach (KeyValuePair<char, int> numeral in Numeral.charTable.Reverse()) {
            int amount = toConvert / numeral.Value;
            int nextTest;
            //9
            if (numeral.Value != 1) {
                nextTest = toConvert / Numeral.charTable[numList[numList.IndexOf(numeral.Key) + 1]];
            }
            else {
                nextTest = 0;
            }
            if (amount == 1 && nextTest == 9) {
                string concat = numList[numList.IndexOf(numeral.Key) + 1].ToString() + numList[numList.IndexOf(numeral.Key) - 1].ToString();
                toConvert -= Numeral.charTable[numList[numList.IndexOf(numeral.Key) - 1]] - Numeral.charTable[numList[numList.IndexOf(numeral.Key) + 1]];
                newValue += concat;
            }
            //4
            else if (amount == 4) {
                string concatb = numeral.Key + numList[numList.IndexOf(numeral.Key) - 1].ToString();
                toConvert -= Numeral.charTable[numList[numList.IndexOf(numeral.Key) - 1]] - numeral.Value;
                newValue += concatb;
            }
            else if (amount >= 1) {
                for (int i = 0; i < amount; i++) {
                    newValue += numeral.Key.ToString();
                    toConvert -= numeral.Value;
                }
            }
            if (toConvert == 0) break;
        }

        return newValue;
    }

    public override string ToString() {
        return value;
    }
}