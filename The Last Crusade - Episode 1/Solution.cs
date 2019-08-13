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
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // number of columns.
        int H = int.Parse(inputs[1]); // number of rows.
        int[,] grid = new int[H,W];
        Console.Error.WriteLine($"{H} {W}");
        for (int i = 0; i < H; i++)
        {
            string LINE = Console.ReadLine(); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            for(int i2 = 0; i2<LINE.Split(' ').Length; i2++)
            {
                grid[i, i2] = int.Parse(LINE.Split(' ')[i2]);
                Console.Error.Write(grid[i,i2] + " ");
            }
            Console.Error.Write("\n");
        }
        int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int XI = int.Parse(inputs[0]);
            int YI = int.Parse(inputs[1]);
            string POS = inputs[2];
            switch(grid[YI, XI])
            {
                case 1:
                case 3:
                case 8:
                {
                    Console.WriteLine($"{XI} {YI+1}");
                }
                break;
                
                case 2:
                case 6:
                {
                    if(POS == "LEFT")
                    {
                        Console.WriteLine($"{XI + 1} {YI}");
                        continue;
                    }
                    else if(POS == "RIGHT")
                    {
                        Console.WriteLine($"{XI - 1} {YI}");
                        continue;
                    }
                }
                break;
                
                case 4:
                {
                    if(POS == "TOP")
                    {
                        Console.WriteLine($"{XI-1} {YI}");
                        continue;
                    }
                    else if(POS == "RIGHT")
                    {
                        Console.WriteLine($"{XI} {YI + 1}");
                        continue;
                    }
                }
                break;
                case 5:
                {
                    if(POS == "LEFT")
                    {
                        Console.WriteLine($"{XI} {YI+1}");
                        continue;
                    }
                    else if(POS == "TOP")
                    {
                        Console.WriteLine($"{XI + 1} {YI}");
                        continue;
                    }
                }
                break;
                case 7:
                {
                    if(POS == "TOP"||POS == "RIGHT")
                    {
                        Console.WriteLine($"{XI} {YI+1}");
                    }
                }
                break;
                case 9:
                {
                    if(POS == "LEFT"||POS == "TOP")
                    {
                        Console.WriteLine($"{XI} {YI+1}");
                    }
                }
                break;
                case 10:
                {
                    if(POS == "TOP")
                    {
                        Console.WriteLine($"{XI -1} {YI}");
                    }
                }
                break;
                case 11:
                {
                    {
                        if(POS == "TOP")
                        {
                            Console.WriteLine($"{XI + 1} {YI}");
                        }
                    }
                }
                break;
                case 12:
                {
                    if(POS == "RIGHT")
                    {
                        Console.WriteLine($"{XI} {YI + 1}");
                    }
                }
                break;
                case 13:
                {
                    if(POS == "LEFT")
                    {
                        Console.WriteLine($"{XI} {YI + 1}");
                    }
                }
                break;
            }
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn.
        }
    }
}