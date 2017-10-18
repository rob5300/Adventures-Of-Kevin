using System.Collections.Generic;
using System.Linq;

public class Numeral {

    public static Dictionary<string, Numeral> NumeralTable = new Dictionary<string, Numeral>();
    public static Dictionary<char, int> charTable = new Dictionary<char, int>();

    public char RomanNumeral;
    public int IntValue;

    static Numeral() {
        NumeralTable.Add("I", new Numeral('I', 1));
        NumeralTable.Add("V", new Numeral('V', 5));
        NumeralTable.Add("X", new Numeral('X', 10));
        NumeralTable.Add("L", new Numeral('L', 50));
        NumeralTable.Add("C", new Numeral('C', 100));
        NumeralTable.Add("D", new Numeral('D', 500));
        NumeralTable.Add("M", new Numeral('M', 1000));

        NumeralTable.Reverse();

        charTable.Add('I',  1);
        charTable.Add('V',  5);
        charTable.Add('X',  10);
        charTable.Add('L',  50);
        charTable.Add('C',  100);
        charTable.Add('D',  500);
        charTable.Add('M',  1000);

    }

    public Numeral(char romanNumeral, int intValue) {
        RomanNumeral = romanNumeral;
        IntValue = intValue;
    }
}
