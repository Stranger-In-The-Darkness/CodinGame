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
        int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        Hashtable table  = new Hashtable();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            string EXT = inputs[0]; // file extension
            Console.Error.WriteLine(EXT);
            string MT = inputs[1]; // MIME type.
            table.Add(EXT.ToLower(), MT);
        }
        string s = "";
            Console.Error.WriteLine(Q);
        for (int i = 0; i < Q; i++)
        {
            string FNAME = Console.ReadLine(); // One file name per line.
            Console.Error.WriteLine(FNAME);
            if(FNAME.IndexOf('.') == -1)
            {
                s += "UNKNOWN\n";   
            }
            else
            {
                if(table.ContainsKey(FNAME.Substring(FNAME.LastIndexOf('.') + 1).ToLower()))
                {
                    s += table[FNAME.Substring(FNAME.LastIndexOf('.') + 1).ToLower()] + "\n";   
                }
                else
                {
                    s+= "UNKNOWN\n";
                }
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");


        // For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN.
        Console.WriteLine(s);
    }
}