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
        string[] inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        char[,] lanes = new char[H, W];
        List<string> answ = new List<string>();
        for (int i = 0; i < H; i++)
        {
            string line = Console.ReadLine();
            for (int j = 0; j<line.Length; j++)
            {
                lanes[i, j] = line[j];
            }
        }
        
        for (int i = 0; i < W; i = i + 3)
        {
            answ.Add(lanes[0, i].ToString());
            int li = i;
            for (int j = 0; j<H; j++)
            {
                if (li == 0)
                {
                    if (lanes[j, li + 1] == '-')
                    {
                        Console.Error.WriteLine(li);
                        li = li + 3;
                    }
                }
                else if (li == W - 1)
                {
                    if (lanes[j, li - 1] == '-')
                    {
                        Console.Error.WriteLine(li);
                        li = li - 3;
                    }
                }
                else 
                {
                    if (lanes[j, li + 1] == '-')
                    {
                        Console.Error.WriteLine(li);
                        li = li + 3;
                    }
                    else if (lanes[j, li - 1] == '-')
                    {
                        Console.Error.WriteLine(li);
                        li = li - 3;
                    }
                }
            }
            answ[answ.Count - 1] = $"{answ.Last()}{lanes[H-1, li]}";
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        foreach(string s in answ)
        {
            Console.WriteLine(s);
        }
    }
}