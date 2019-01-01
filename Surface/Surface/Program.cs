using System;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Surface
{
    //public class Cell
    //{
    //    private object o = new object();
    //    char value;
    //    bool processed = false;

    //    public Cell(char value)
    //    {
    //        this.value = value;
    //    }

    //    public char Value
    //    {
    //        get
    //        {
    //            return value;
    //        }
    //    }

    //    public bool Process()
    //    {
    //        lock (o)
    //        {
    //            if (processed)
    //            {
    //                throw new InvalidOperationException("Already been here");
    //            }
    //            processed = true;
    //            if (value == 'O')
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                throw new Exception("Reached the end of water");
    //            }
    //        }
    //    }
    //}

    //public static class Map
    //{
    //    public static Cell[,] map;
    //    public static int[,] vMap;
    //    public static int W;
    //    public static int H;

    //    public static int GetV (int x, int y)
    //    {
    //        lock (vMap.SyncRoot)
    //        {
    //            return vMap[y, x];
    //        }
    //    }

    //    public static void SetV(int x, int y)
    //    {
    //        lock (vMap.SyncRoot)
    //        {
    //            vMap[y, x] = 1;
    //        }
    //    }
    //}

    //public static class Res
    //{
    //    public static int[] res;
    //    public static List<List<Cell>> Water = new List<List<Cell>>();

    //    private static int index = 0;
    //    private static object o = new object();

    //    public static int GetIndex()
    //    {
    //        lock (o)
    //        {
    //            if (index > res.Length)
    //            {
    //                throw new Exception();
    //            }
    //            return index++;
    //        }
    //    }
    //}

    ///**
    // * Auto-generated code below aims at helping you parse
    // * the standard input according to the problem statement.
    // **/
    //class Solution
    //{
    //    static void Main(string[] args)
    //    {
    //        int L = int.Parse(Console.ReadLine());
    //        int H = int.Parse(Console.ReadLine());
    //        Map.map = new Cell[H, L];
    //        Map.H = H;
    //        Map.W = L;
    //        Map.vMap = new int[H, L];
    //        Console.Error.WriteLine(Map.map.Length);
    //        for (int i = 0; i < H; i++)
    //        {
    //            string row = Console.ReadLine();
    //            Console.Error.WriteLine(row);
    //            for (int j = 0; j < L; j++)
    //            {
    //                Map.map[i, j] = new Cell(row[j]);
    //            }
    //        }
    //        int N = int.Parse(Console.ReadLine());
    //        Tuple<int, int>[] coord = new Tuple<int, int>[N];
    //        Task[] t = new Task[N];
    //        for (int i = 0; i < N; i++)
    //        {
    //            string[] inputs = Console.ReadLine().Split(' ');
    //            int X = int.Parse(inputs[0]);
    //            int Y = int.Parse(inputs[1]);
    //            coord[i] = new Tuple<int, int>(X, Y);
    //            Console.Error.WriteLine(i + " " + X + "." + Y);
    //            t[i] = new Task(() => Calculate(X, Y, Res.GetIndex()));
    //            t[i].Start();
    //        }
    //        try
    //        {
    //            Task.WaitAll(t);
    //        }
    //        catch { }

    //        for (int i = 0; i < Res.Water.Count; i++)
    //        {
    //            for (int j = 0; j < Res.Water[i].Count; j++)
    //            {
    //                Res.Water[i].ForEach((e) => Console.Error.Write(e.Value));
    //                if (Res.Water.Where((e)=>e.Contains(Res.Water[i][j]) && e != Res.Water[i]).Count() > 0)
    //                {
    //                    Res.Water[i].Concat(Res.Water.Where((e) => e.Contains(Res.Water[i][j]) && e != Res.Water[i]).Last());
    //                    Res.Water.Remove(Res.Water.Where((e) => e.Contains(Res.Water[i][j]) && e != Res.Water[i]).Last());
    //                }
    //            }
    //        }

    //        for (int i = 0; i < N; i++)
    //        {

    //            int res = 0;
    //            try
    //            {
    //                res = Res.Water.Where((e) => e.Contains(Map.map[coord[i].Item2, coord[i].Item1])).First().Count();
    //            }
    //            catch { }

    //            // Write an action using Console.WriteLine()
    //            // To debug: Console.Error.WriteLine("Debug messages...");

    //            Console.WriteLine(res);
    //        }
    //    }

    //    static void Calculate(int x, int y, int index, Cell sender = null)
    //    {
    //        if (Res.Water.Count == 0 || Res.Water.Select((e) => e.Contains(sender)).ToList().IndexOf(true) == -1)
    //        {
    //            Res.Water.Add(new List<Cell>());
    //        }
    //        Console.Error.WriteLine("Run process " + index);
    //        Task[] t = new Task[4];
    //        if (Map.map[y, x].Value == '#')
    //        {
    //            return;
    //        }
    //        try
    //        {
    //            if (Map.map[y, x].Process())
    //            {
    //                if (Res.Water.Select((e) => e.Contains(sender)).ToList().IndexOf(true) != -1)
    //                {
    //                    Res.Water[Res.Water.Select((e) => e.Contains(sender)).ToList().IndexOf(true)].Add(Map.map[y, x]);
    //                }
    //                else
    //                {
    //                    Res.Water.Add(new List<Cell>() { sender, Map.map[y, x] });
    //                }
    //            }
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            Res.Water[Res.Water.Select((e) => e.Contains(sender)).ToList().IndexOf(true)].Add(Map.map[y, x]);
    //        }
    //        if (x - 1 >= 0)
    //        {
    //            Task task = new Task(() => Calculate(x - 1, y, index, Map.map[y, x]));
    //            t[0] = task;
    //            task.Start();
    //            Console.Error.WriteLine("Run process " + index + "." + 1);
    //        }
    //        if (y - 1 >= 0)
    //        {
    //            Task task = new Task(() => Calculate(x, y - 1, index, Map.map[y, x]));
    //            t[1] = task;
    //            task.Start();
    //            Console.Error.WriteLine("Run process " + index + "." + 2);
    //        }
    //        if (x + 1 < Map.W)
    //        {
    //            Task task = new Task(() => Calculate(x + 1, y, index, Map.map[y, x]));
    //            t[2] = task;
    //            task.Start();
    //            Console.Error.WriteLine("Run process " + index + "." + 3);
    //        }
    //        if (y + 1 < Map.H)
    //        {
    //            Task task = new Task(() => Calculate(x, y + 1, index, Map.map[y, x]));

    //            t[3] = task;
    //            task.Start();
    //            Console.Error.WriteLine("Run process " + index + "." + 4);
    //        }

    //        try
    //        {
    //            Task.WaitAll(t);
    //        }
    //        catch { }
    //        Console.Error.WriteLine("End process " + index);
    //        return;
    //    }
    //}

    public static class Map
    {
        public static int[,] map;
        public static int H;
        public static int W;
    }

    class Solution
    {
        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());
            Map.map = new int[H, L];
            Map.H = H;
            Map.W = L;
            for (int i = 0; i < H; i++)
            {
                string row = Console.ReadLine();
                Console.Error.WriteLine(row);
                for (int j = 0; j < L; j++)
                {
                    Map.map[i, j] = row[j] == 'O' ? 1 : 0;
                }
            }
            int N = int.Parse(Console.ReadLine());
            Tuple<int, int>[] coord = new Tuple<int, int>[N];
            List<Task> t = new List<Task>();
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int X = int.Parse(inputs[0]);
                int Y = int.Parse(inputs[1]);
                coord[i] = new Tuple<int, int>(X, Y);
                Console.Error.WriteLine(i + " " + X + "." + Y);
            }

            Console.Error.WriteLine();
            for (int i = 0; i < Map.H; i++)
            {
                for (int j = 0; j < Map.W; j++)
                {
                    Console.Error.Write(Map.map[j, i]);
                }
                Console.Error.WriteLine();
            }
            Console.Error.WriteLine();

            for (int i = 0; i < Map.W; i++)
            {
                Console.Error.WriteLine("Run Calculate with " + i);
                int v = i;
                Task task = new Task(() => Calculate(v, 0));
                task.Start();
                t.Add(task);
            }

            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }

            Console.Error.WriteLine();
            for (int i = 0; i < Map.H; i++)
            {
                for (int j = 0; j < Map.W; j++)
                {
                    Console.Error.Write(Map.map[j, i]);
                }
                Console.Error.WriteLine();
            }
            Console.Error.WriteLine();

            for (int i = 0; i < Map.H; i++)
            {
                Console.Error.WriteLine("Run Calculate 2 with " + i);
                int v = i;
                Task task = new Task(() => Calculate(v, 1));
                task.Start();
                t.Add(task);
            }
            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }

            Console.Error.WriteLine();
            for (int i = 0; i < Map.H; i++)
            {
                for (int j = 0; j < Map.W; j++)
                {
                    Console.Error.Write(Map.map[j, i]);
                }
                Console.Error.WriteLine();
            }
            Console.Error.WriteLine();

            for (int i = 0; i < Map.W; i++)
            {
                Console.Error.WriteLine("Run Assign with " + i);
                int v = i;
                Task task = new Task(() => Assign(v, 0));
                task.Start();
                t.Add(task);
            }

            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }

            for (int i = 0; i < Map.H; i++)
            {
                Console.Error.WriteLine("Run Assign 2 with " + i);
                int v = i;
                Task task = new Task(() => Assign(v, 1));
                task.Start();
                t.Add(task);
            }
            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }

            for (int i = 0; i < Map.W; i++)
            {
                Console.Error.WriteLine("Run Assign 3 with " + i);
                int v = i;
                Task task = new Task(() => Assign(v, 3));
                task.Start();
                t.Add(task);
            }

            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }

            for (int i = 0; i < Map.H; i++)
            {
                Console.Error.WriteLine("Run Assign 3 with " + i);
                int v = i;
                Task task = new Task(() => Assign(v, 2));
                task.Start();
                t.Add(task);
            }
            try
            {
                Task.WaitAll(t.ToArray());
            }
            catch { }



            for (int i = 0; i < Map.H; i++)
            {
                for (int j = 0; j < Map.W; j++)
                {
                    Console.Error.Write(Map.map[j, i]);
                }
                Console.Error.WriteLine();
            }

            for (int i = 0; i < N; i++)
            {

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine(Map.map[coord[i].Item2, coord[i].Item1]);
            }
        }

        public static void Calculate(int index, int direction)
        {
            switch (direction)
            {
                case 0:
                {
                    for (int i = Map.H - 1; i > 0; i--)
                    {
                        Console.Error.WriteLine(index + "." + i + " " + Map.map[index, i]);
                        Console.Error.WriteLine(index + "." + (i - 1) + " " + Map.map[index, i - 1]);
                        if (Map.map[index, i] != 0 && Map.map[index, i - 1] != 0)
                        {
                            Map.map[index, i - 1] += Map.map[index, i];
                        }
                        Console.Error.WriteLine(index + "." + i + " " + Map.map[index, i]);
                    }
                }
                break;
                case 1:
                {
                    for (int i = 0; i < Map.W - 1; i++)
                    {
                        Console.Error.WriteLine(i + "." + index + " " + Map.map[i, index]);
                        Console.Error.WriteLine((i+1) + "." + index + " " + Map.map[i + 1, index]);
                        if (Map.map[i, index] != 0 && Map.map[i + 1, index] != 0)
                        {
                            Map.map[i + 1, index] += Map.map[i, index];
                        }
                        Console.Error.WriteLine(i + "." + index + " " + Map.map[i, index]);
                    }
                }
                break;
                case 2:
                {
                    for (int i = 0; i < Map.H - 1; i++)
                    {
                        Console.Error.WriteLine(index + "." + i + " " + index + "." + (i + 1));
                        Console.Error.WriteLine(Map.map[index, i]);
                        Console.Error.WriteLine(Map.map[index, i + 1]);
                        if (Map.map[index, i] != 0 && Map.map[index, i + 1] != 0)
                        {
                            Map.map[index, i + 1] += Map.map[index, i];
                        }
                        Console.Error.WriteLine(Map.map[index, i]);
                    }
                }
                break;
                case 3:
                {
                    for (int i = Map.W - 1; i > 0; i--)
                    {
                        Console.Error.WriteLine(i + "." + index + " " + (i - 1) + "." + index);
                        Console.Error.WriteLine(Map.map[i, index]);
                        Console.Error.WriteLine(Map.map[i - 1, index]);
                        if (Map.map[i, index] != 0 && Map.map[i - 1, index] != 0)
                        {
                            Map.map[i - 1, index] += Map.map[i, index];
                        }
                        Console.Error.WriteLine(Map.map[i, index]);
                    }
                }
                break;
            }
            Console.Error.WriteLine("End Calculate " + index + " " + direction);
            Assign(index, (direction + 2) % 4);
        }

        public static void Assign(int index, int direction)
        {
            Console.Error.WriteLine("Assign " + index + " " + direction);
            switch (direction)
            {
                case 0:
                {
                    for (int i = Map.H - 1; i > 0; i--)
                    {
                        if (Map.map[index, i - 1] != 0 && Map.map[index, i] != 0 &&
                            Map.map[index, i - 1] < Map.map[index, i])
                        {
                            Map.map[index, i - 1] = Map.map[index, i];
                        }
                    }
                }
                break;
                case 1:
                {
                    for (int i = 0; i < Map.W - 1; i++)
                    {
                        if (Map.map[i + 1, index] != 0 && Map.map[index, i] != 0 &&
                            Map.map[i + 1, index] < Map.map[index, i])
                        {
                            Map.map[i + 1, index] = Map.map[i, index];
                        }
                    }
                }
                break;
                case 2:
                {
                    for (int i = 0; i < Map.H - 1; i++)
                    {
                        if (Map.map[index, i + 1] != 0 && Map.map[index, i] != 0 &&
                            Map.map[i + 1, index] < Map.map[index, i])
                        {
                            Map.map[index, i + 1] = Map.map[index, i];
                        }
                    }
                }
                break;
                case 3:
                {
                    for (int i = Map.W - 1; i > 0; i--)
                    {
                        if (Map.map[i - 1, index] != 0 && Map.map[index, i] != 0 &&
                            Map.map[i + 1, index] < Map.map[index, i])
                        {
                            Map.map[i - 1, index] = Map.map[i, index];
                        }
                    }
                }
                break;
            }
            Console.Error.WriteLine("End Assign " + index + " " + direction);
        }
    }
}