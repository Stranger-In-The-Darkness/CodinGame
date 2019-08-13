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
        int C = int.Parse(Console.ReadLine());
        List<int> budgets = new List<int>();
        string res = "";
        for (int i = 0; i < N; i++)
        {
            int B = int.Parse(Console.ReadLine());
            budgets.Add(B);
        }
        budgets.Sort();
        bool impossible = false;
        if(budgets.Sum() < C)
        {
            Console.WriteLine("IMPOSSIBLE");
            impossible = true;
        }
        else
        {
            int curSum = 0;
            int avr = C/N;
            Console.Error.WriteLine(avr);
            for(int i = 0; i<N-1; i++)
            {
                if(budgets[i] >= (C-curSum)/(N-i))
                {
                    res += $"{(C-curSum)/(N-i)}\n";
                    curSum += (C-curSum)/(N-i);
                }
                else
                {
                    res += $"{budgets[i]}\n";
                    curSum += budgets[i];
                }
            }
            res += $"{C - curSum}";
        }
        if(!impossible)
        {
            res.Remove(res.Length - 2);
            Console.WriteLine(res);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        }
    }
}