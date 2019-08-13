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
        int R = int.Parse(Console.ReadLine());
        int L = int.Parse(Console.ReadLine());

        Console.Error.WriteLine($"R {R} \nL {L}");
        string result = L == 1 ? R.ToString() : ConvertToSequence(R.ToString(), L, 1);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(result);
    }
    
    static string ConvertToSequence(string R, int L, int index)
    {
        int ind = index;
        string res = "";
        List<string> split = R.Split(' ').ToList();
        Console.Error.WriteLine("R " + R);
        int count = 1;
        string current = split[0];
        for(int i = 1; i< split.Count; i++)
        {
                if(split[i] != current)
                {
                    res += count + " " + current + " ";
                    current = split[i];
                    count = 1;
                }
                else
                {
                    count++;
                }
        }
        res += count + " " + current;
        Console.Error.WriteLine($"Index {ind} \nResult {res}");
        ind++;
        if(ind >= L)
        {
            return res;   
        }
        else
        {
            return ConvertToSequence(res, L, ind);
        }
    }
}