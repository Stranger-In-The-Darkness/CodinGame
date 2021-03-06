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
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string[] inputs = Console.ReadLine().Split(' ');
        

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        if(n==0)
        {
            Console.WriteLine(0);
        }
        else
        {
            int min = int.Parse(inputs[0]);
        for (int i = 0; i < n; i++)
        {
            int t = int.Parse(inputs[i]); // a temperature expressed as an integer ranging from -273 to 5526
            if(Math.Abs(t)<Math.Abs(min))
            {
                min = t;   
            }
            if(Math.Abs(t)==Math.Abs(min) && min < 0 && t > 0)
            {
                min = t;   
            }
        }
            Console.WriteLine(min);
        }   
    }
}