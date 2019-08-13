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
        string s = Console.ReadLine();
        string res = s;
        while(res.Length % 2 == 0)
        {
            string r = "";
            for(int i = 0; i<res.Length; i+=2)
            {
                int count = Convert.ToInt32(res[i].ToString());
                r += new String(res[i+1], count);
            }
            if (res == r) break;
            if(Reverse(r, res))
            {
                res = r;
            }
            else 
            {
                break;
            }
        }
        Console.WriteLine(res);
    }
    
    static bool Reverse(string from, string to)
    {
        string r = "";
        Console.Error.WriteLine($"{from} {to}");
        for(int i = 0; i<from.Length; )
            {
                int count = 1;
                for(int j = i; j<from.Length-1 && from[j] == from[j+1]; j++) 
                {
                    count++;
                }
                r += count.ToString() + from[i];
                Console.Error.WriteLine($"{i} {count} {from[i]} {r}");
                i+=count;
            }
        if(r != to) return false;
        return true;
    }
}