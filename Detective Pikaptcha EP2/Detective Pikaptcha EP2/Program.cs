using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detective_Pikaptcha_EP2
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        Unknown
    }
    class Program
    {
        static void Main(string[] args)
        {
            int width = int.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());
            char[,] map = new char[height, width];
            int[,] mapCalc = new int[height, width];
            int pIX = 0, pIY = 0, pCX = 0, pCY = 0;
            Direction dir = Direction.Unknown;
            for (int i = 0; i<height; i++)
            {
                string line = Console.ReadLine();
                Console.Error.WriteLine(line);
                for (int j = 0; j<width; j++)
                {
                    if (line[j] == '<')
                    {
                        dir = Direction.Left;
                        pIX = pCX = j;
                        pIY = pCY = i;
                        map[i, j] = '0';
                    }
                    else if (line[j] == '^')
                    {
                        dir = Direction.Up;
                        pIX = pCX = j;
                        pIY = pCY = i;
                        map[i, j] = '0';
                    }
                    else if (line[j] == '>')
                    {
                        dir = Direction.Right;
                        pIX = pCX = j;
                        pIY = pCY = i;
                        map[i, j] = '0';
                    }
                    else if (line[j] == 'v')
                    {
                        dir = Direction.Down;
                        pIX = pCX = j;
                        pIY = pCY = i;
                        map[i, j] = '0';
                    }
                    map[i, j] = line[j];
                }
            }
            string side = Console.ReadLine();
            switch (side)
            {
                case "L":
                {

                }
                break;
                case "R":
                {

                }
                break;
            }
        }
    }
}
