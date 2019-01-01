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
        List<List<int>> list = new List<List<int>>();
        list.Add(new List<int>());

        int n = int.Parse(Console.ReadLine()); // the number of relationships of influence

        string s = Console.ReadLine();

        list[0].Add(int.Parse(s.Split(' ')[0]));
        list[0].Add(int.Parse(s.Split(' ')[1]));

        int max = list[0].Count;

        for (int i = 1; i < n; i++)
        {

            for(int l = 0; l <list.Count/2; l++)
            {
                for(int l2 = list.Count/2; l2<list.Count; l2++)
                {
                    if(ListToString(list[l]).IndexOf(ListToString(list[l2])) != -1)
                    {
                        list.RemoveAt(l2);
                    }
                    else if (ListToString(list[l2]).IndexOf(ListToString(list[l])) != -1)
                    {
                        list.RemoveAt(l);
                    }
                }
            }

            string[] inputs = Console.ReadLine().Split(' ');

            int x = int.Parse(inputs[0]); // a relationship of influence between two people (x influences y)
            int y = int.Parse(inputs[1]);

            List<List<int>> tx = list.Where((b) => b.Contains(x)).ToList();
            List<List<int>> ty = list.Where((b) => b.Contains(y)).ToList();

            if(tx.Count == 0)
            {
                if (ty.Count != 0)
                {
                    foreach (List<int> ly in ty) {
                        List<int> l = new List<int>();
                        l.Add(x);
                        for (int c = ly.IndexOf(y); c<ly.Count; c++)
                        {
                            l.Add(ly[c]);
                        }
                        list.Add(l);
                    }
                }
                else
                {
                    List<int> l = new List<int>() { x, y };
                    list.Add(l);
                }
            }
            else
            {
                if (ty.Count != 0)
                {
                    foreach(List<int> lx in tx)
                    {
                        foreach(List<int> ly in ty)
                        {
                            List<int> l = new List<int>();
                            for(int c = 0; c<=lx.IndexOf(x); c++)
                            {
                                l.Add(lx[c]);
                            }
                            for(int c = ly.IndexOf(y); c<ly.Count; c++)
                            {
                                l.Add(ly[c]);
                            }
                            list.Add(l);
                        }
                    }
                }
                else
                {
                    foreach (List<int> lx in tx)
                    {
                        List<int> l = new List<int>();
                        for(int c =0; c <= lx.IndexOf(x); c++)
                        {
                            l.Add(lx[c]);
                        }
                        l.Add(y);
                        list.Add(l);
                    }
                }
            }
        }
        Console.WriteLine(list.Select((e) => e.Count).ToList().Max());
    }

    public static string ListToString(List<int> list)
    {
        string s = list[0] + " ";
        for (int i = 1; i<list.Count; i++)
        {
            s += list[i] + " "; 
        }
        s.Remove(s.Length - 1);
        return s;
    }
}