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
        string MESSAGE = Console.ReadLine();
        string encoded = "";
        string bitCode = "";
        foreach(char c in MESSAGE)
        {
            byte[] b = Encoding.ASCII.GetBytes(c.ToString());
            foreach(byte bt in b)
            {
                string s = Convert.ToString(bt, 2);
                while(s.Length < 7)
                {
                    s = $"0{s}";
                }
                //Console.Error.WriteLine($"{s} ");
                bitCode += s;
            }
        }
        while(bitCode.Length < 7)
        {
            bitCode = "0" + bitCode;   
        }
        
        List<string> split = new List<string>();
        
        bool endsWithOneSymbol = false;
        if(bitCode[bitCode.Length -2] != bitCode[bitCode.Length-1])
        {
            endsWithOneSymbol = true;   
        }
        do
        {
            split.Add
            (
                bitCode.Substring(0, bitCode[0] == '0' ? 
                (
                    bitCode.IndexOf('1') == -1 ?
                        bitCode.Length :
                        bitCode.IndexOf('1')
                    ) : 
                    (
                    bitCode.IndexOf('0') == -1 ?
                        bitCode.Length :
                        bitCode.IndexOf('0')
                    ) 
                )
            );
            
            bitCode = bitCode.Remove(0, split.Last().Length);//(0,  bitCode[0] == '0' ? bitCode.IndexOf('0') : bitCode.IndexOf('1'));
        }
        while(bitCode.Length > 1);
        if(endsWithOneSymbol)
        {
            split.Add(bitCode);   
        }
        
        foreach(string s in split)
        {
            encoded += Crypt(s) + " ";
        }
        encoded = encoded.Remove(encoded.Length - 1);
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(encoded);
    }
    
    static string Crypt(string s)
    {
        string res = "";
        if(s[0] == '0')
        {
            res += "00 ";
        }
        else
        {
            res += "0 ";
        }
        res += new string('0', s.Length);
        return res;
    }
}