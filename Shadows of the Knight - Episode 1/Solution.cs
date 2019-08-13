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
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.
        int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int X0 = int.Parse(inputs[0]);
        int Y0 = int.Parse(inputs[1]);
        int X = X0, leftX = 0, rightX = W, Y = Y0, upY = 0, downY = H;

        // game loop
        while (true)
        {
            string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            switch(bombDir)
            {
                case "U":
                if(Y<downY) downY = Y;
                Y = (upY+downY)/2;
                break;
                
                case "UR":
                if(X>leftX) leftX = X;
                X = (leftX+rightX)/2;
                if(Y<downY) downY = Y;
                Y = (upY+downY)/2;
                break;
                
                case "R":
                if(X>leftX) leftX = X;
                X = (leftX+rightX)/2;
                break;
                
                case "DR":
                if(X>leftX) leftX = X;
                X = (leftX+rightX)/2;
                if(Y>upY) upY = Y;
                Y = (upY+downY)/2;
                break;
                
                case "D":
                if(Y>upY) upY = Y;
                Y = (upY+downY)/2;
                break;
                
                case "DL":
                if(X<rightX) rightX = X;
                X = (leftX+rightX)/2;
                if(Y>upY) upY = Y;
                Y = (upY+downY)/2;
                break;
                
                case "L":
                if(X<rightX) rightX = X;
                X = (leftX+rightX)/2;
                break;
                
                case "UL":
                if(X<rightX) rightX = X;
                X = (leftX+rightX)/2;
                if(Y<downY) downY = Y;
                Y = (upY+downY)/2;
                break;
            }

            // the location of the next window Batman should jump to.
            Console.WriteLine($"{X} {Y}");
        }
    }
}