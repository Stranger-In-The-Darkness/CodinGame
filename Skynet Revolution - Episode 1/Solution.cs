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
    static int[,] map;
    static int N;
    static int L;
    static int E;
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');

        N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        L = int.Parse(inputs[1]); // the number of links
        E = int.Parse(inputs[2]); // the number of exit gateways

        map = new int[N, N];
        List<int> exits = new List<int>();

        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            map[N1, N2] = 1;
            map[N2, N1] = 1;
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            
            exits.Add(EI);
        }

        for (int i = 0; i<N; i++)
        {
            for (int j = 0; j<N; j++)
            {
                Console.Error.Write(map[i, j] + " ");
            }
            Console.Error.WriteLine();
        }

        // game loop
        while (true)
        {
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Queue<string> links = new Queue<string>();

            for (int i = 0; i<N; i++)
            {
                if(map[i, SI] == 1 && i != SI)
                {
                    if (exits.IndexOf(i) != -1)
                    {
                        Console.WriteLine($"{SI} {i}");
                        map[SI, i] = 0;
                        map[i, SI] = 0;
                        links.Clear();
                        break;
                    }
                    else
                    {
                        links.Enqueue($"{SI} {i}");
                    }
                }
            }

            while (links.Count > 0)                
            {
                var link = links.Dequeue();

                var node = int.Parse(link.Split(' ')[1]);
                for (int i = 0; i < N; i++)
                {
                    if (map[i, node] == 1 && i != node)
                    {
                        if (exits.IndexOf(i) != -1)
                        {
                            Console.WriteLine($"{node} {i}");
                            map[node, i] = 0;
                            map[i, node] = 0;
                            links.Clear();
                            break;
                        }
                        else
                        {
                            links.Enqueue($"{node} {i}");
                        }
                    }
                }
            }

            //// Example: 0 1 are the indices of the nodes you wish to sever the link between
            //Console.WriteLine("0 1");
        }
    }
}