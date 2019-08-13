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
        int N = int.Parse(Console.ReadLine());
        List<string> dict = new List<string>();
        for (int i = 0; i < N; i++)
        {
            string W = Console.ReadLine();
            dict.Add(W);
        }
        string LETTERS = Console.ReadLine();
        string res = "";
        int currentValue = 0;
        foreach(string s in dict)
        {
            bool valid = true;
            foreach(char c in s)
            {
                if(LETTERS.IndexOf(c) == -1)
                {
                    valid = false;
                    break;
                }
            }
            foreach(char c in LETTERS)
            {
                if(CountChar(s, c) > CountChar(LETTERS, c))
                {
                    valid = false;
                    break;
                }
            }
            if(valid)
            {
                if(Scrabble(s) > currentValue)
                {
                    res = s;
                    currentValue = Scrabble(s);
                }
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(res);
    }
    
    public static int Scrabble(string s)
    {
        int res = 0;
        foreach(char c in s)
        {
            switch(c)
            {
                case 'e':
                case 'a':
                case 'i':
                case 'o':
                case 'n':
                case 'r':
                case 't':
                case 'l':
                case 's':
                case 'u':
                {
                    res += 1;
                }
                break;
                
                case 'd':
                case 'g':
                {
                    res += 2;
                }
                break;
                
                case 'b':
                case 'c':
                case 'm':
                case 'p':
                {
                    res += 3;
                }
                break;
                
                case 'f':
                case 'h':
                case 'v':
                case 'w':
                case 'y':
                {
                    res += 4;
                }
                break;
                
                case 'k':
                {
                    res += 5;
                }
                break;
                
                case 'j':
                case 'x':
                {
                    res += 8;
                }
                break;
                
                case 'q':
                case 'z':
                {
                    res += 10;
                }
                break;
            }
        }
        return res;
    }
    
    public static int CountChar(string s, char c)
    {
        int count = 0;
        foreach(char chr in s)
        {
            count += chr == c ? 1:0; 
        }
        return count;
    }
}