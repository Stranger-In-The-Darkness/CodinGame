using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Code4Life
{
    /**
     * Bring data on patient samples from the diagnosis machine to the laboratory with enough molecules to produce medicine!
     **/
    class Player
    {
        class Sample
        {
            public int sampleId = 0;
            public int carriedBy = -1;
            public int rank = 1;
            public string expertiseGain = "";
            public int health = 0;
            public int costA = 0;
            public int costB = 0;
            public int costC = 0;
            public int costD = 0;
            public int costE = 0;
            public int totalCost = 0;

            public bool Check(int A, int B, int C, int D, int E)
            {
                if (costA <= A &&
                    costB <= B &&
                    costC <= C &&
                    costD <= D &&
                    costE <= E)
                {
                    Console.Error.WriteLine($"{costA} <= {A} = {costA <= A}\n{costB} <= {B} = {costB <= B}\n{costC} <= {C} = {costC <= C}\n{costD} <= {D} = {costD <= D}\n{costE} <= {E} = {costE <= E}");
                    return true;
                }
                return false;
            }

            public double Suitability (int A, int B, int C, int D, int E)
            {
                double result = 0.0;
                result += (costA == 0 ? 0.0 : A / costA > 1 ? 1.0 : A / costA);
                result += (costB == 0 ? 0.0 : B / costB > 1 ? 1.0 : B / costB);
                result += (costC == 0 ? 0.0 : C / costC > 1 ? 1.0 : C / costC);
                result += (costD == 0 ? 0.0 : D / costD > 1 ? 1.0 : D / costD);
                result += (costE == 0 ? 0.0 : E / costE > 1 ? 1.0 : E / costE);
                result /= 5.0;
                return result;
            }

            public int Max()
            {
                int max = costA;
                if (costB > max)
                    max = costB;
                if (costC > max)
                    max = costC;
                if (costD > max)
                    max = costD;
                if (costE > max)
                    max = costE;
                return max;
            }
        }

        static void Main(string[] args)
        {
            string[] inputs;
            int projectCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < projectCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int a = int.Parse(inputs[0]);
                int b = int.Parse(inputs[1]);
                int c = int.Parse(inputs[2]);
                int d = int.Parse(inputs[3]);
                int e = int.Parse(inputs[4]);
            }

            bool carryingSample = false;

            int countMolecules = 0;

            List<int> sampleID = new List<int>();
            List<int> diagSamples = new List<int>();

            string command = "";

            int eta = 0;

            bool addRank = false;
            // game loop
            while (true)
            {
                int totalExpertise = 0;

                string target = "";
                int storageA = 0, storageB = 0, storageC = 0, storageD = 0, storageE = 0,
                    expertiseA = 0, expertiseB = 0, expertiseC = 0, expertiseD = 0, expertiseE = 0;
                for (int i = 0; i < 2; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    if (i == 0)
                    {
                        target = inputs[0];
                        eta = int.Parse(inputs[1]);
                        int score = int.Parse(inputs[2]);
                        storageA = int.Parse(inputs[3]);
                        storageB = int.Parse(inputs[4]);
                        storageC = int.Parse(inputs[5]);
                        storageD = int.Parse(inputs[6]);
                        storageE = int.Parse(inputs[7]);

                        countMolecules = storageA + storageB + storageC + storageD + storageE;

                        expertiseA = int.Parse(inputs[8]);
                        expertiseB = int.Parse(inputs[9]);
                        expertiseC = int.Parse(inputs[10]);
                        expertiseD = int.Parse(inputs[11]);
                        expertiseE = int.Parse(inputs[12]);

                        totalExpertise += expertiseA + expertiseB + expertiseC + expertiseD + expertiseE;
                        Console.Error.WriteLine($"Expertise = {totalExpertise} {expertiseA} {expertiseB} {expertiseC} {expertiseD} {expertiseE}");
                    }
                }
                inputs = Console.ReadLine().Split(' ');
                int availableA = int.Parse(inputs[0]);
                int availableB = int.Parse(inputs[1]);
                int availableC = int.Parse(inputs[2]);
                int availableD = int.Parse(inputs[3]);
                int availableE = int.Parse(inputs[4]);

                int sampleCount = int.Parse(Console.ReadLine());
                Dictionary<int, Sample> sampleList = new Dictionary<int, Sample>();

                for (int i = 0; i < sampleCount; i++)
                {
                    Sample spl = new Sample();
                    inputs = Console.ReadLine().Split(' ');
                    spl.sampleId = int.Parse(inputs[0]);
                    spl.carriedBy = int.Parse(inputs[1]);
                    spl.rank = int.Parse(inputs[2]);
                    spl.expertiseGain = inputs[3];
                    spl.health = int.Parse(inputs[4]);
                    spl.totalCost = 0;
                    spl.costA = int.Parse(inputs[5]) - expertiseA;
                    spl.costA = spl.costA < 0 ? 0 : spl.costA;
                    spl.totalCost += spl.costA;
                    spl.costB = int.Parse(inputs[6]) - expertiseB;
                    spl.costB = spl.costB < 0 ? 0 : spl.costB;
                    spl.totalCost += spl.costB;
                    spl.costC = int.Parse(inputs[7]) - expertiseC;
                    spl.costC = spl.costC < 0 ? 0 : spl.costC;
                    spl.totalCost += spl.costC;
                    spl.costD = int.Parse(inputs[8]) - expertiseD;
                    spl.costD = spl.costD < 0 ? 0 : spl.costD;
                    spl.totalCost += spl.costD;
                    spl.costE = int.Parse(inputs[9]) - expertiseE;
                    spl.costE = spl.costE < 0 ? 0 : spl.costE;
                    spl.totalCost += spl.costE;

                    sampleList.Add(spl.sampleId, spl);
                }

                Console.Error.WriteLine(target);

                sampleID = sampleList.Where((s) => s.Value.carriedBy == 0).Select((s) => s.Key).ToList();

                if (eta == 0)
                {
                    switch (target)
                    {
                        case "SAMPLES":
                        {
                            int rank = 1;
                            if (totalExpertise > 2)
                            {
                                rank = 2;
                            }
                            if (totalExpertise > 4)
                            {
                                rank = 3;
                            }
                            if (!carryingSample)
                            {
                                command = $"CONNECT {rank}";
                                carryingSample = true;
                                break;
                            }
                            else
                            {
                                if (sampleID.Count < 3)
                                {
                                    command = $"CONNECT {rank}";
                                    if (addRank)
                                    {
                                        command = $"CONNECT {(rank - 1 == 0 ? 1 : rank - 1)}";
                                        addRank = false;
                                    }
                                    break;
                                }
                                else
                                {
                                    command = "GOTO DIAGNOSIS";
                                    break;
                                }
                            }
                        }
                        case "DIAGNOSIS":
                        {
                            if (addRank)
                            {
                                if (sampleID.Count < 3)
                                {
                                    command = "GOTO SAMPLES";
                                }
                                var w = sampleList.
                                        Where((s) => sampleID.Contains(s.Key));
                                double min = w.Min((s) => s.Value.Suitability(storageA, storageB, storageC, storageD, storageE));
                                w = w.Where((s) => s.Value.Suitability(storageA, storageB, storageC, storageD, storageE) <= min);
                                if (w.Count() > 0)
                                {
                                    command = $"CONNECT {w.First().Key}";
                                    sampleID.Remove(w.First().Key);
                                    break;
                                }
                                else
                                {
                                    command = $"COMMAND {w.First().Key}";
                                    sampleID.Remove(w.First().Key);
                                    break;
                                }
                            }
                            var l = sampleList.Where((s) => sampleID.Contains(s.Key)).Where((s) => !diagSamples.Contains(s.Key));
                            if (l.Count() > 0)
                            {
                                command = $"CONNECT {l.First().Key}";
                                diagSamples.Add(l.First().Key);
                                break;
                            }
                            else
                            {
                                var d = sampleList.
                                    Where((s) => sampleID.Contains(s.Key)).
                                    Where((s) => s.Value.totalCost > 10 || s.Value.Max() > 5);
                                Console.Error.WriteLine(d.Count());
                                if (d.Count() > 0)
                                {
                                    command = $"CONNECT {d.First().Key}";
                                    sampleID.Remove(d.First().Key);
                                    if (sampleID.Count == 0)
                                    {
                                        command = "GOTO SAMPLES";
                                        break;
                                    }
                                    break;
                                }
                                command = "GOTO MOLECULES";
                                break;
                            }
                        }
                        case "MOLECULES":
                        {
                            if (!carryingSample)
                            {
                                command = "GOTO SAMPLES";
                                break;
                            }
                            else
                            {
                                if (countMolecules < 10)
                                {
                                    int totalA = 0,
                                        totalB = 0,
                                        totalC = 0,
                                        totalD = 0,
                                        totalE = 0;
                                    if (sampleID.Count >= 2)
                                    {
                                        if (sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.totalCost).Sum() <= 10)
                                        {
                                            totalA = sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.costA).Sum();
                                            totalA = totalA < 0 ? 0 : totalA;
                                            totalB = sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.costB).Sum();
                                            totalB = totalB < 0 ? 0 : totalB;
                                            totalC = sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.costC).Sum();
                                            totalC = totalC < 0 ? 0 : totalC;
                                            totalD = sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.costD).Sum();
                                            totalD = totalD < 0 ? 0 : totalD;
                                            totalE = sampleList.Where((s) => sampleID.Contains(s.Key)).Select((s) => s.Value.costE).Sum();
                                            totalE = totalE < 0 ? 0 : totalE;
                                        }
                                        else
                                        {
                                            int min = 20, id = sampleID.Last();
                                            var l = sampleList.Where((s) => sampleID.Contains(s.Key));
                                            foreach (KeyValuePair<int, Sample> k in l)
                                            {
                                                if (k.Value.totalCost < min)
                                                {
                                                    min = k.Value.totalCost;
                                                    id = k.Key;
                                                }
                                            }
                                            totalA = sampleList[id].costA;
                                            totalB = sampleList[id].costB;
                                            totalC = sampleList[id].costC;
                                            totalD = sampleList[id].costD;
                                            totalE = sampleList[id].costE;
                                        }
                                        if (totalA > 5 ||
                                        totalB > 5 ||
                                        totalC > 5 ||
                                        totalD > 5 ||
                                        totalE > 5 ||
                                        totalA + totalB + totalC + totalD + totalE > 10)
                                        {
                                            totalA = sampleList[sampleID.Last()].costA;
                                            totalB = sampleList[sampleID.Last()].costB;
                                            totalC = sampleList[sampleID.Last()].costC;
                                            totalD = sampleList[sampleID.Last()].costD;
                                            totalE = sampleList[sampleID.Last()].costE;
                                        }
                                    }
                                    else
                                    {
                                        totalA = sampleList[sampleID.Last()].costA;
                                        totalB = sampleList[sampleID.Last()].costB;
                                        totalC = sampleList[sampleID.Last()].costC;
                                        totalD = sampleList[sampleID.Last()].costD;
                                        totalE = sampleList[sampleID.Last()].costE;
                                    }
                                    Console.Error.WriteLine($"Total A = {totalA}\nTotal B = {totalB}\nTotal C = {totalC}\nTotal D = {totalD}\nTotal E = {totalE}");

                                    if (storageA < totalA)
                                    {
                                        if (availableA > 0)
                                        {
                                            command = "CONNECT A";
                                            countMolecules++;
                                            break;
                                        }
                                        else
                                        {
                                            if (sampleID.Count < 3)
                                            {
                                                addRank = true;
                                                command = "GOTO SAMPLES";
                                                break;
                                            }
                                            
                                            command = "WAIT";
                                            break;
                                        }
                                    }
                                    if (storageB < totalB)
                                    {
                                        if (availableB > 0)
                                        {
                                            command = "CONNECT B";
                                            countMolecules++;
                                            break;
                                        }
                                        else
                                        {
                                            if (sampleID.Count < 3)
                                            {
                                                addRank = true;
                                                command = "GOTO SAMPLES";
                                                break;
                                            }
                                            
                                            command = "WAIT";
                                            break;
                                        }
                                    }
                                    if (storageC < totalC)
                                    {
                                        if (availableC > 0)
                                        {
                                            command = "CONNECT C";
                                            countMolecules++;
                                            break;
                                        }
                                        else
                                        {
                                            if (sampleID.Count < 3)
                                            {
                                                addRank = true;
                                                command = "GOTO SAMPLES";
                                                break;
                                            }
                                            
                                            command = "WAIT";
                                            break;
                                        }
                                    }
                                    if (storageD < totalD)
                                    {
                                        if (availableD > 0)
                                        {
                                            command = "CONNECT D";
                                            countMolecules++;
                                            break;
                                        }
                                        else
                                        {
                                            if (sampleID.Count < 3)
                                            {
                                                addRank = true;
                                                command = "GOTO SAMPLES";
                                                break;
                                            }
                                            
                                            command = "WAIT";
                                            break;
                                        }
                                    }
                                    if (storageE < totalE)
                                    {
                                        if (availableE > 0)
                                        {
                                            command = "CONNECT E";
                                            countMolecules++;
                                            break;
                                        }
                                        else
                                        {
                                            if (sampleID.Count < 3)
                                            {
                                                addRank = true;
                                                command = "GOTO SAMPLES";
                                                break;
                                            }
                                            
                                            command = "WAIT";
                                            break;
                                        }
                                    }
                                }

                                command = "GOTO LABORATORY";
                                break;
                            }
                        }
                        case "LABORATORY":
                        {
                            if (carryingSample)
                            {
                                var l = sampleList.Where((s) => sampleID.Contains(s.Key));
                                foreach (KeyValuePair<int, Sample> pair in l)
                                {
                                    Console.Error.WriteLine($"Sample check = {pair.Value.sampleId} = {pair.Value.Check(storageA, storageB, storageC, storageD, storageE)}");
                                }
                                var r = l.Where((s) => s.Value.Check(storageA, storageB, storageC, storageD, storageE));
                                Console.Error.WriteLine($"{sampleID.Count} {l.Count()} {r.Count()}");
                                if (r.Count() > 0)
                                {
                                    int id = r.First().Key;
                                    command = $"CONNECT {id}";
                                    sampleID.Remove(id);
                                    countMolecules -= sampleList[id].totalCost;
                                    if (sampleID.Count == 0)
                                    {
                                        carryingSample = false;
                                    }
                                    break;
                                }
                                else if (countMolecules == 10)
                                {
                                    addRank = true;
                                    command = "GOTO DIAGNOSIS";
                                    break;
                                }
                                else
                                {
                                    command = "GOTO MOLECULES";
                                    break;
                                }
                            }
                            else
                            {
                                command = "GOTO SAMPLES";
                                break;
                            }
                        }
                        case "START_POS":
                        {
                            command = "GOTO SAMPLES";
                            break;
                        }
                    }
                }
                else
                {
                    command = " ";
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");
                Console.Error.WriteLine($"eta = {eta}\nSamples = {sampleID.Count}\nMolecules = {countMolecules}\nA = {storageA}\nB = {storageB}\nC = {storageC}\nD = {storageD}\nE = {storageE}");
                Console.WriteLine(command);
            }
        }
    }
}