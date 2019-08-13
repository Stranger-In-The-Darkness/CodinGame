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
    public static class Direction
    {
        private static Queue<string> direction = new Queue<string>();
        private static bool reversed = false;

        public static void Reverse()
        {
            direction = new Queue<string>(direction.Reverse());
            reversed = !reversed;
        }

        public static string Get()
        {
            string dir = direction.Dequeue();
            direction.Enqueue(dir);
            return dir;
        }

        public static void Reset()
        {
            try
            {
                direction.Dequeue();
                direction.Dequeue();
                direction.Dequeue();
                direction.Dequeue();
            }
            catch { }

            if (reversed)
            {
                direction.Enqueue("WEST");
                direction.Enqueue("NORTH");
                direction.Enqueue("EAST");
                direction.Enqueue("SOUTH");
            }
            else
            {
                direction.Enqueue("SOUTH");
                direction.Enqueue("EAST");
                direction.Enqueue("NORTH");
                direction.Enqueue("WEST");
            }
        }

        public static void Show()
        {
            //foreach(string s in direction)
            //{
            //	Console.Error.Write($"{s} ");
            //}
        }

        public static string IntToDirection(int direction)
        {
            switch (direction)
            {
                case 0:
                return "SOUTH";
                case 1:
                return "EAST";
                case 2:
                return "NORTH";
                case 3:
                return "WEST";
                default:
                return "";
            }
        }

        public static int DirectionToInt(string direction)
        {
            switch (direction)
            {
                case "SOUTH":
                return 0;
                case "EAST":
                return 1;
                case "NORTH":
                return 2;
                case "WEST":
                return 3;
                default:
                return -1;
            }
        }

        public static int ReverseDirection(string direction)
        {
            switch (direction)
            {
                case "SOUTH":
                return 2;
                case "EAST":
                return 3;
                case "NORTH":
                return 0;
                case "WEST":
                return 1;
                default:
                return -1;
            }
        }

        public static bool Reversed
        {
            get
            {
                return reversed;
            }
        }
    }

    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int C = int.Parse(inputs[1]);

        Direction.Reset();

        string dir = "SOUTH";

        int startX = 0, startY = 0, posX = 0, posY = 0, endX = 0, endY = 0, telX1 = 0, telY1 = 0, telX2 = 0, telY2 = 0;

        char[,] map = new char[L, C];

        List<string> tiles = new List<string>();

        for (int i = 0; i < L; i++)
        {
            string row = Console.ReadLine();            
            for (int j = 0; j < C; j++)
            {
                map[i, j] = row[j];
                if (row[j] == '@')
                {
                    posX = j;
                    posY = i;
                    map[i, j] = ' ';
                }
                else if (row[j] == '$')
                {
                    endX = j;
                    endY = i;
                }
                else if (row[j] == 'T')
                {
                    if (telX1 != 0 && telY1 != 0)
                    {
                        telX2 = j;
                        telY2 = i;
                    }
                    else
                    {
                        telX1 = j;
                        telY1 = i;
                    }
                }
            }
            //Console.Error.WriteLine();
        }
        startX = posX;
        startY = posY;

        bool breaker = false;

        List<string> pass = new List<string>();

        bool condition = true;

        int loops = 0;
        int mapChanges = 0;
        bool loop = false;
        bool looping = false;

        for (int i = 0; i < L; i++)
        {
            for (int j = 0; j < C; j++)
            {
                Console.Error.Write(map[i, j]);
            }
            Console.Error.WriteLine();
        }

        while (condition)
        {
            Console.Error.WriteLine($"Pos ({posY}, {posX}) End({endY}, {endX}) Breaker: {breaker} Inverted: {Direction.Reversed}");
            Direction.Show();
            int x = 0, y = 0;
            if (dir == "SOUTH")
            {
                x = posX;
                y = posY + 1;
            }
            else if (dir == "EAST")
            {
                x = posX + 1;
                y = posY;
            }
            else if (dir == "NORTH")
            {
                x = posX;
                y = posY - 1;
            }
            else if (dir == "WEST")
            {
                x = posX - 1;
                y = posY;
            }
            Console.Error.WriteLine($"{posX}-{x} {posY}-{y} Loops {loops} Changes {mapChanges}");
            switch (map[y, x])
            {
                #region ChangeDirectionPoints
                case 'S':
                {
                    pass.Add(dir);
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }

                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                    dir = "SOUTH";
                }
                break;
                case 'E':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                    dir = "EAST";
                }
                break;
                case 'N':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                    dir = "NORTH";
                }
                break;
                case 'W':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                    dir = "WEST";
                }
                break;
                case 'I':
                {
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reverse();
                    pass.Add(dir);
                    posX = x;
                    posY = y;
                }
                break;
                #endregion
                case 'T':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    if (x == telX1 && y == telY1)
                    {
                        posX = telX2;
                        posY = telY2;
                    }
                    else if (x == telX2 && y == telY2)
                    {
                        posX = telX1;
                        posY = telY1;
                    }
                }
                break;
                case 'B':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                    breaker = !breaker;
                }
                break;
                case 'X':
                {
                    if (breaker)
                    {
                        pass.Add(dir);
                        mapChanges++;
                        tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                        if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                        {
                            if (!looping)
                            {
                                loops++;
                                looping = true;
                            }
                            if (loops > mapChanges)
                            {
                                loop = true;
                                goto end;
                            }
                        }
                        else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                        {
                            loop = true;
                            goto end;
                        }
                        else
                        {
                            looping = false;
                        }
                        Direction.Reset();
                        map[y, x] = ' ';
                        posX = x;
                        posY = y;
                    }
                    else
                    {
                        dir = Direction.Get();
                    }
                }
                break;
                case ' ':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    posX = x;
                    posY = y;
                }
                break;
                case '#':
                {
                    dir = Direction.Get();
                }
                break;
                case '$':
                {
                    pass.Add(dir);
                    tiles.Add($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}");
                    if (tiles.IndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}") != tiles.LastIndexOf($"{Direction.DirectionToInt(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}"))
                    {
                        if (!looping)
                        {
                            loops++;
                            looping = true;
                        }
                        if (loops > mapChanges)
                        {
                            loop = true;
                            goto end;
                        }
                    }
                    else if (tiles.Count >= 2 && tiles[tiles.Count - 2] == $"{Direction.ReverseDirection(dir)}.{(breaker ? 1 : 0)}.{(Direction.Reversed ? 1 : 0)}.{x}.{y}")
                    {
                        loop = true;
                        goto end;
                    }
                    else
                    {
                        looping = false;
                    }
                    Direction.Reset();
                    condition = false;
                }
                break;
            }
            Console.Error.WriteLine(pass.Count != 0 ? pass.Last() : "...");
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        end:
        if (loop)
        {
            Console.WriteLine("LOOP");
        }
        else
        {
            foreach (string s in pass)
            {
                Console.WriteLine(s);
            }
        }
        Console.ReadKey();
    }
}