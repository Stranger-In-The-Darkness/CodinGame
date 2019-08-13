using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
    static void Main(string[] args)
    {
        int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        string[] grid = new string[height];
        string result = "";
        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine(); // width characters, each either 0 or .
            grid[i] = line;
        }
        for(int i = 0; i< height; i++)
        {
            for(int i2 = 0; i2<width; i2++)
            {
                if(grid[i][i2] == '0')
                {
                    result += $"{i2} {i} ";
                    if(i == height - 1 && i2 == width - 1)
                    {
                        result += "-1 -1 -1 -1\n";
                        continue;
                    }
                    bool found = false;
                    if(i2 == width - 1)
                    {
                        result += "-1 -1 ";
                        for(int i3 = i + 1; i3 < height; i3++)
                        {
                            if(grid[i3][i2] == '0')
                            {
                                result += $"{i2} {i3} ";   
                                found = true;
                                break;
                            }
                        }
                        if(!found)
                        {
                            result += "-1 -1 ";     
                        }
                        result = result.Remove(result.Length - 1 ) + "\n";
                        continue;
                    }
                    found = false;
                    if(i == height - 1)
                    {
                        for(int i3 = i2 + 1; i3 < width; i3++)
                        {
                            if(grid[i][i3] == '0')
                            {
                                result += $"{i3} {i} ";
                                found = true;
                                break;
                            }
                        }
                        if(!found)
                        {
                            result += "-1 -1 ";     
                        }
                        result += "-1 -1 ";
                        result = result.Remove(result.Length - 1 ) + "\n";
                        continue;
                    }
                    found = false;
                    for(int i3 = i2 + 1; i3<width; i3++)
                    {
                        if(grid[i][i3] == '0')
                        {
                            result += $"{i3} {i} ";
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        result += "-1 -1 ";
                    }   
                    found = false;
                    for(int i3 = i + 1; i3<height; i3++)
                    {
                        if(grid[i3][i2] == '0')
                        {
                            result += $"{i2} {i3} ";   
                            found = true;
                            break;
                        }
                    }
                    if(!found)
                    {
                        result += "-1 -1 ";
                    } 
                    result = result.Remove(result.Length - 1 ) + "\n";
                }
            }
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");


        // Three coordinates: a node, its right neighbor, its bottom neighbor
        Console.WriteLine(result);
    }
}