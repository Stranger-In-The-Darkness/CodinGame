using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace War
{
    
    class Solution
    {
        static void Main(string[] args)
        {
            List<string> alph = new List<string>()
            {
             "2", "3", "4",
             "5", "6", "7",
             "8", "9", "10",
             "J", "Q", "K", "A"
            };

            int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
            Queue<string> deckp1 = new Queue<string>();
            for (int i = 0; i < n; i++)
            {
                string cardp1 = Console.ReadLine(); // the n cards of player 1
                deckp1.Enqueue(cardp1);
            }
            int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
            Queue<string> deckp2 = new Queue<string>();
            for (int i = 0; i < m; i++)
            {
                string cardp2 = Console.ReadLine(); // the m cards of player 2
                deckp2.Enqueue(cardp2);
            }

            bool war = false;

            string result = "PAT";
            int turns = 0;

            List<string> warp1 = new List<string>();
            List<string> warp2 = new List<string>();

            while (deckp1.Count >= 0 && deckp2.Count >= 0)
            {
                Console.Error.WriteLine(war ? "War" : "Turn");
                if (deckp1.Count == 0 && deckp2.Count == 0)
                {
                    result = "PAT";
                    break;
                }
                else if (!war && deckp1.Count == 0)
                {
                    result = "2";
                    break;
                }
                else if (!war && deckp2.Count == 0)
                {
                    result = "1";
                    break;
                }

                if (war)
                {
                    try
                    {
                        warp1.Add(deckp1.Dequeue());
                        warp1.Add(deckp1.Dequeue());
                        warp1.Add(deckp1.Dequeue());

                        warp2.Add(deckp2.Dequeue());
                        warp2.Add(deckp2.Dequeue());
                        warp2.Add(deckp2.Dequeue());
                    }
                    catch
                    {
                        result = "PAT";
                        break;
                    }

                    warp1.Add(deckp1.Dequeue());
                    warp2.Add(deckp2.Dequeue());
                    string c1 = warp1.Last();
                    string p1 = c1.Remove(c1.Length - 1);
                    string c2 = warp2.Last();
                    string p2 = c2.Remove(c2.Length - 1);

                    if (alph.IndexOf(p1) > alph.IndexOf(p2))
                    {
                        foreach(string s in warp1)
                        {
                            deckp1.Enqueue(s);
                        }
                        warp1.Clear();
                        foreach (string s in warp2)
                        {
                            deckp1.Enqueue(s);
                        }
                        warp2.Clear();

                        war = false;
                        turns++;
                        continue;
                    }
                    else if (alph.IndexOf(p2) > alph.IndexOf(p1))
                    {
                        foreach (string s in warp1)
                        {
                            deckp2.Enqueue(s);
                        }
                        warp1.Clear();
                        foreach (string s in warp2)
                        {
                            deckp2.Enqueue(s);
                        }
                        warp2.Clear();

                        war = false;
                        turns++;
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    warp1.Add(deckp1.Dequeue());
                    string c1 = warp1.Last();
                    string p1 = c1.Remove(c1.Length - 1);
                    warp2.Add(deckp2.Dequeue());
                    string c2 = warp2.Last();
                    string p2 = c2.Remove(c2.Length - 1);

                    if (alph.IndexOf(p1) > alph.IndexOf(p2))
                    {
                        foreach (string s in warp1)
                        {
                            deckp1.Enqueue(s);
                        }
                        warp1.Clear();
                        foreach (string s in warp2)
                        {
                            deckp1.Enqueue(s);
                        }
                        warp2.Clear();
                        turns++;
                        continue;
                    }
                    else if (alph.IndexOf(p2) > alph.IndexOf(p1))
                    {
                        foreach (string s in warp1)
                        {
                            deckp2.Enqueue(s);
                        }
                        warp1.Clear();
                        foreach (string s in warp2)
                        {
                            deckp2.Enqueue(s);
                        }
                        warp2.Clear();
                        turns++;
                        continue;
                    }
                    else
                    {
                        war = true;
                        continue;
                    }
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            Console.WriteLine(result == "PAT" ? "PAT" : result + " " + turns);
        }
    }
}
