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
        int L = int.Parse(inputs[0]);
        int C = int.Parse(inputs[1]);
        int N = int.Parse(inputs[2]);

        int index = 0;
        int qCount = 0;
        List<int> queue = new List<int>();

        for (int i = 0; i < N; i++)
        {
            int pi = int.Parse(Console.ReadLine());
            qCount++;
            queue.Add(pi);
        }

        long answ = 0;
        for (int i = 0; i < C; i++)
        {
            int group = 0;
            for (int count = 0; count < N; count++)
            {
                int ind = (index + count) % qCount;
                int q = queue[ind];
                if (group + q <= L)
                {
                    group += q;
                }
                else
                {
                    index = ind;
                    break;
                }
            }
            answ += group;
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(answ);
    }
}