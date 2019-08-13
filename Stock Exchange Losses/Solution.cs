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
        int n = int.Parse(Console.ReadLine());
        string[] inputs = Console.ReadLine().Split(' ');
        int loss = 0;
        List<int> list = new List<int>();
        int currentMax = 0, currentLoss = 0;
        for (int i = 0; i < n; i++)
        {
            int v = int.Parse(inputs[i]);
            list.Add(v);
            if(v>currentMax)
            {
                currentMax = v;
                continue;
            }
            if(currentMax - v>currentLoss)
            {
                currentLoss = currentMax - v;
                if(currentLoss > loss)
                {
                    loss = currentLoss;
                }
            }
        }
        
        //for(int i = 0; i< list.Count - 1; i++)
        //{
        //    for(int i2 = i+1; i2<list.Count; i2++)
        //    {
        //        if(list[i] - list[i2] > loss)
        //        {
        //            loss = list[i] - list[i2];
        //        }
        //    }
        //}
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(-loss);
    }
}