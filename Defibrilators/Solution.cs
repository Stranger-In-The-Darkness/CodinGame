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
        string LON = Console.ReadLine();
        string LAT = Console.ReadLine();
        double lon = (Convert.ToDouble(LON.Replace(',', '.')) * Math.PI)/180;
        
        double lat = (Convert.ToDouble(LAT.Replace(',', '.')) * Math.PI)/180;
        
        double minDist = -1;
        string name = "";
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string DEFIB = Console.ReadLine();
            
            double defLon = (Convert.ToDouble(DEFIB.Split(';')[4].Replace(',', '.')) * Math.PI)/180;
        
            double defLat = (Convert.ToDouble(DEFIB.Split(';')[5].Replace(',', '.')) * Math.PI)/180;
        
            double d = Math.Sqrt(
                Math.Pow(
                    (defLon - lon) *
                    Math.Cos(
                        (lat + defLat)/2)
                        ,2) +
                Math.Pow(
                    (defLat - lat)
                    ,2)
                ) * 6371;   
            if(minDist == -1)
            {
                minDist = d;
                name = DEFIB.Split(';')[1];
            }
            else if(d < minDist)
            {
                minDist = d;
                name = DEFIB.Split(';')[1];
            }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(name);
    }
}