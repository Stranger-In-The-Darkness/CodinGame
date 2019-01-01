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
        List<int> list = new List<int>();
        for (int i = 0; i < N; i++)
        {
            int pi = int.Parse(Console.ReadLine());
            list.Add(pi);
        }
        list.Sort();
        int min = list[1] - list[0];
        for(int i = 2; i<N; i++)
        {
            if(list[i] - list[i-1] < min)
            {
                min = list[i] - list[i-1];   
            }
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(min);
    }
}