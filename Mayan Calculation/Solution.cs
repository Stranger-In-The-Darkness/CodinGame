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
        int H = int.Parse(inputs[1]);
        
        List<string> alph = new List<string>();
        for(int i = 0; i<20; i++)
        {
            alph.Add("");
        }
        
        for(int i = 0; i<H; i++)
        {
            string str = Console.ReadLine();
            for(int j = 0; j<20; j++)
            {
                alph[j] += str.Substring(j*L, L);
            }
        }

        int S1 = int.Parse(Console.ReadLine());
        string num1 = "";
        for (int i = 0; i < S1; i++)
        {
            num1 += Console.ReadLine();
        }
        
        long n1 = 0;
        for(int i = 0; i<num1.Length/(L*H); i++)
        {
            n1 += alph.IndexOf(num1.Substring(i*L*H, L*H)) * (long)Math.Pow(20, num1.Length/(L*H) - i - 1);
        }
        
        int S2 = int.Parse(Console.ReadLine());
        string num2 = "";
        for (int i = 0; i < S2; i++)
        {
            num2 += Console.ReadLine();
        }
        
        long n2 = 0;
        for(int i = 0; i<num2.Length/(L*H); i++)
        {
            n2 += alph.IndexOf(num2.Substring(i*L*H, L*H)) * (long)Math.Pow(20, num2.Length/(L*H) - i - 1);
        }

        long result = 0;        
        string operation = Console.ReadLine();
        
        switch(operation)
        {
            case "+":
            {
                result = n1+n2;
            }
            break;
            case "-":
            {
                result = n1-n2;
            }
            break;
            case "*":
            {
                result = n1*n2;
            }
            break;
            case "/":
            {
                result = n1/n2;
            }
            break;
        }
        
        for(int i = 0; i<20; i++)
        {
            Console.Error.WriteLine($"alph[{i}] = {alph[i]}");
        }
        
        Console.Error.WriteLine(result);
        string res = "";
        do
        {
            Console.Error.WriteLine(int.Parse((result%20).ToString()));
            Console.Error.WriteLine($"alph[{int.Parse((result%20).ToString())}] - {alph[int.Parse((result%20).ToString())]}");
            res = alph[int.Parse((result%20).ToString())] + res;
            Console.Error.WriteLine(res);
            result /= 20;
            Console.Error.WriteLine(result);
        }
        while(result > 0);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        for(int i = 0; i< res.Length; i+=L)
        {
            Console.Write(res.Substring(i, L) + "\n");
        }
    }
}