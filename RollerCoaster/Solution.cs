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

public class CircleEnumerator
{
    private List<int> _list = new List<int>();
    private int _index = -1;

    public CircleEnumerator(IEnumerable<int> list)
    {
        _list = list.ToList();
    }

    public void MoveNext() { _index = (_index + 1) % _list.Count; }

    public void MoveTo(int index)
    {
        _index = index;
    }

    public void Reset() { _index = -1; }

    public int Current
    {
        get => _list[_index];
    }

    internal int Index
    {
        get => _index;
    }

    public int Count { get => _list.Sum(); }
}

class Solution
{
    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int C = int.Parse(inputs[1]);
        int N = int.Parse(inputs[2]);

        List<int> q = new List<int>();

        for (int i = 0; i < N; i++)
        {
            int pi = int.Parse(Console.ReadLine());
            q.Add(pi);
        }

        CircleEnumerator enumerator = new CircleEnumerator(q);
        enumerator.MoveNext();

        Dictionary<int, long> mem = new Dictionary<int, long>();

        long answ = 0;

        int countRides = 0;
        while (countRides < C)
        {
            countRides++;
            answ += Calc(L, enumerator);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(answ);
    }

    static Dictionary<int, Tuple<long, int>> mem = new Dictionary<int, Tuple<long, int>>();

    static long Calc(int L, CircleEnumerator enumerator)
    {
        if (mem.Keys.Contains(enumerator.Index))
        {
            long v = mem[enumerator.Index].Item1;
            enumerator.MoveTo(mem[enumerator.Index].Item2);
            return v;
        }
        long group = 0;
        int ind = enumerator.Index;
        while (true)
        {
            if (group + enumerator.Current <= L)
            {
                if (group >= enumerator.Count)
                {
                    break;
                }
                group += enumerator.Current;
                enumerator.MoveNext();
            }
            else
            {
                break;
            }
        }
        mem.Add(ind, new Tuple<long, int>(group, enumerator.Index));
        return group;
    }
}