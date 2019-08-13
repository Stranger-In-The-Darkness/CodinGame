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
        int Y = int.Parse(Console.ReadLine());
        List<int[]> cages = new List<int[]>();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int S = int.Parse(inputs[0]);
            int H = int.Parse(inputs[1]);
            int A = int.Parse(inputs[2]);
            cages.Add(new int[] {S, H, A});
        }
        for(int i = 0; i<Y; i++)
        {
            int answ = 0;
            for(int j = 0; j<N; j++)
            {
                if(cages[j][2] > 0)
                {
                cages[j][2] = cages[j][1];
                cages[j][0] *= 2;
                cages[j][1] -= cages[j][0];
                }
                if(cages[j][2] < 0) cages[j][2] = 0;
                
                answ += cages[j][2];
            }
            Console.WriteLine(answ);
            if(answ == 0) break;
        }
    }
}