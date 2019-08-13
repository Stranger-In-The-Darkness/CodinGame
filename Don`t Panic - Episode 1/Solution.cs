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
        int nbFloors = int.Parse(inputs[0]); // number of floors
        int width = int.Parse(inputs[1]); // width of the area
        char[,] level = new char[nbFloors, width];
        for(int i = 0; i< nbFloors; i++)
        {
            for(int i2 = 0; i2< width; i2++)
            {
                level[i, i2] = ' ';   
            }
        }
        int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
        int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
        int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
        
        level[exitFloor, exitPos] = 'e';
        
        int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
        int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
        int nbElevators = int.Parse(inputs[7]); // number of elevators
        for (int i = 0; i < nbElevators; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
            int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
            level[elevatorFloor, elevatorPos] = 'l';
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
            int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
            string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT
            if(cloneFloor == -1 
            && clonePos == -1
            && direction == "NONE")
            {
                Console.WriteLine("WAIT");
                continue;
            }
            if(cloneFloor == exitFloor)
            {
                if(clonePos > exitPos)
                {
                    if(direction == "LEFT")
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    else if (direction == "RIGHT")
                    {
                        if(exitFloor > 0)
                        {
                            if(level[exitFloor - 1, clonePos] != 'l')
                            {
                                Console.WriteLine("BLOCK");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("WAIT");
                                continue;
                            }
                        }
                        else 
                        {
                            Console.WriteLine("BLOCK");
                            continue;
                        }
                    }    
                }
                else if(clonePos < exitPos)
                {
                    if(direction == "RIGHT")
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    else if (direction == "LEFT")
                    {
                        if(exitFloor > 0)
                        {
                            if(level[exitFloor - 1, clonePos] != 'l')
                            {
                                Console.WriteLine("BLOCK");
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("WAIT");
                                continue;
                            }
                        }
                        else 
                        {
                            Console.WriteLine("BLOCK");
                            continue;
                        }
                    } 
                }
                else
                {
                    Console.WriteLine("WAIT");
                    continue;
                }
            }
            if((clonePos == width-1 && level[cloneFloor, clonePos] != 'e')
            || (clonePos == 0 && level[cloneFloor, clonePos] != 'e'))
            {
                if((direction == "LEFT" && (clonePos - exitPos) > 0) 
                || (direction == "RIGHT" && (clonePos - exitPos) < 0))
                {
                    Console.WriteLine("WAIT");
                    continue;
                }
                else
                {
                    Console.WriteLine("BLOCK");
                    continue;
                }
            }
            else
            {
                int index = 0;
                for(int i = 0; i<width; i++)
                {
                    if(level[cloneFloor, i] == 'l')
                    {
                        index = i;
                        break;
                    }
                }
                if(index == clonePos)
                {
                    Console.WriteLine("WAIT");
                    continue;
                }
                else if(index > clonePos)
                {
                    if(direction == "RIGHT")
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    else if(direction == "LEFT")
                    {
                        if(cloneFloor > 0)
                        {
                            if(level[cloneFloor -1, clonePos] == 'l')
                            {
                                Console.WriteLine("WAIT");  
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("BLOCK");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("BLOCK");
                            continue;
                        }
                    }
                }
                else
                {
                    if(direction == "LEFT")
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    else if(direction == "RIGHT")
                    {
                        if(cloneFloor > 0)
                        {
                            if(level[cloneFloor -1, clonePos] == 'l')
                            {
                                Console.WriteLine("WAIT");  
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("BLOCK");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("BLOCK");
                            continue;
                        }
                    }
                }
            }
            
            
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine("WAIT"); // action: WAIT or BLOCK
        }
    }
}