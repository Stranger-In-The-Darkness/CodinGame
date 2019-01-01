using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static void Main(string[] args)
    {
        int L = int.Parse(Console.ReadLine());
        int H = int.Parse(Console.ReadLine());
        string T = Console.ReadLine();
        T = T.ToUpper();
        string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string[] alphASCII = new string[H];
        for (int i = 0; i < H; i++)
        {
            string ROW = Console.ReadLine();
            alphASCII[i] = ROW;
        }
        string s = "";
        for(int i = 0; i<H; i++)
        {
            
            for(int i2 = 0; i2<T.Length; i2++)
            {
                Console.Error.WriteLine(i+" "+i2);
                s += alphASCII[i].Substring(
                    alph.IndexOf(T[i2]) == -1 ? alph.Length * L : alph.IndexOf(T[i2]) * L,
                    L);
            }
            s+="\n";
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(s);
    }
}