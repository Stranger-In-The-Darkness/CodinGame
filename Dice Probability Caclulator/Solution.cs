using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice_Probability_Caclulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> inputs = new Queue<string>();
            inputs.Enqueue(Console.ReadLine());
            var res = Dice(inputs.Dequeue());
            foreach (string s in res)
            {
                inputs.Enqueue(s);
            }
            Dictionary<string, int> results = new Dictionary<string, int>();
            while (inputs.Any())
            {
                string s = inputs.Dequeue();
                Console.Error.WriteLine(s);

                if (s.Contains('('))
                {
                    s = s.Replace("(-", "(0-");
                    string innerCalc = s.Substring(s.IndexOf('(') + 1, s.IndexOf(')') - s.IndexOf('(') - 1);
                    Console.Error.WriteLine(s.Replace($"({innerCalc})", Calculate(innerCalc).ToString()));
                    inputs.Enqueue(s.Replace($"({innerCalc})", Calculate(innerCalc).ToString()));
                    continue;
                }
                results.Add(s, Calculate(s));
            }
            Dictionary<int, double> calc = new Dictionary<int, double>();
            foreach(var kv in results)
            {
                Console.Error.WriteLine($"{kv.Key} - {kv.Value}");
                if (calc.Keys.Contains(kv.Value))
                {
                    calc[kv.Value]++;
                }
                else
                {
                    calc.Add(kv.Value, 1d);
                }
            }
            var ord = calc.OrderBy(kv => kv.Key).ToList();
            var sum = ord.Sum(kv => kv.Value);
            for (int i = 0; i<ord.Count; i++)
            {
                Console.WriteLine($"{ord[i].Key} {Math.Round((ord[i].Value/sum)*100, 2).ToString("0.00")}");
            }
        }

        static int Calculate(string calc)
        {
            if (calc.First() == '-')
            {
                calc = "0" + calc;
            }
            calc = calc.Replace("+-", "-");
            string[] first = calc.Split('>');
            for(int i = 0; i<first.Length; i++)
            {
                string or = first[i];
                string[] second = first[i].Split('+');
                for (int i2 = 0; i2 < second.Length; i2++)
                {
                    string or2 = second[i2];
                    string[] third = second[i2].Split('-');
                    for (int i3 = 0; i3 < third.Length; i3++)
                    {
                        string or3 = third[i3];
                        string[] fourth = third[i3].Split('*');
                        List<int> r3 = fourth.Select((e) => int.Parse(e)).ToList();
                        int n3 = 1;
                        foreach (int a in r3)
                        {
                            n3 *= a;
                        }
                        third[i3] = third[i3].Replace(or3, n3.ToString());
                    }
                    List<int> r2 = third.Select((e) => int.Parse(e)).ToList();
                    int n2 = r2.First();
                    if (r2.Count > 1)
                    {
                        for (int a = 1; a<r2.Count; a++)
                        {
                            n2 -= r2[a];
                        }
                    }
                    second[i2] = second[i2].Replace(or2, n2.ToString());
                }
                List<int> r = second.Select((e) => int.Parse(e)).ToList();
                int n = 0;
                for (int a = 0; a<r.Count; a++)
                {
                    n += r[a];
                }
                first[i] = first[i].Replace(or, n.ToString());
            }
            if (first.Length > 1)
            {
                return int.Parse(first.First()) > int.Parse(first.Last()) ? 1 : 0;
            }
            else
            {
                return int.Parse(first.First());
            }
        }

        static IEnumerable<string> Dice(params string[] values)
        {
            if (values.Length == 0)
            {
                return null;
            }
            List<string> res = new List<string>();
            foreach (string s in values)
            {
                if (s.Contains('d'))
                {
                    int dStart = s.IndexOf('d');
                    string ds = s.Substring(s.IndexOf('d') + 1);
                    int dEnd = 0;
                    for (; dEnd < ds.Length; dEnd++)
                    {
                        if (!char.IsNumber(ds[dEnd]))
                        {
                            break;
                        }
                    }
                    ds = dEnd < ds.Length ? ds.Remove(dEnd) : ds;
                    int d = int.Parse(ds);
                    for (int c = 1; c <= d; c++)
                    {
                        string r = $"{s.Remove(dStart)}{c}{s.Substring(dStart + dEnd+1)}";
                        res.Add(r);
                    }
                }
                else
                {
                    res.Add(s);
                }
            }
            return res.Any(s => s.Contains('d')) ? Dice(res.ToArray()) : res;
        }
    }
}
