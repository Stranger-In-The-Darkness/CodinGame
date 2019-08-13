using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace CodinGameSponsoredContest
{


    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    class Player
    {
        static void Main(string[] args)
        {
            string[,] map;
            //string answ = "EEEEEEEEE";
            int firstInitInput = int.Parse(Console.ReadLine());
            Console.Error.WriteLine($"1-init: {firstInitInput}"); //Upper bound of first digit ???
            int secondInitInput = int.Parse(Console.ReadLine());
            Console.Error.WriteLine($"2-init: {secondInitInput}"); //Upper bound of second digit ???

            map = new string[firstInitInput, secondInitInput];

            for (int i = 0; i<firstInitInput; i++)
            {
                for (int j = 0; j<secondInitInput; j++)
                {
                    map[i, j] = ".";
                }
            }

            int thirdInitInput = int.Parse(Console.ReadLine());
            Console.Error.WriteLine($"3-init: {thirdInitInput}"); //Always 5 (A, B, C, D, E)
            //Console.Error.WriteLine($"A: {(int)'A'}");
            //Console.Error.WriteLine($"B: {(int)'B'}");
            //Console.Error.WriteLine($"C: {(int)'C'}");
            //Console.Error.WriteLine($"D: {(int)'D'}");
            //Console.Error.WriteLine($"E: {(int)'E'}");

            //int c = 0;

            // game loop

            int x = 0, y = 0;

            while (true)
            {
                string firstInput = Console.ReadLine();
                Console.Error.WriteLine($"1: {firstInput}");
                string secondInput = Console.ReadLine();
                Console.Error.WriteLine($"2: {secondInput}");
                string thirdInput = Console.ReadLine();
                Console.Error.WriteLine($"3: {thirdInput}");
                string fourthInput = Console.ReadLine();
                Console.Error.WriteLine($"4: {fourthInput}");

                //string answ = "A";
                //int max = 0;

                bool answ = false;

                if (x>0 && x <firstInitInput-1 && y>0 && y < secondInitInput-1)
                {
                    map[x, y - 1] = firstInput;
                    map[x + 1, y] = secondInput;
                    map[x, y + 1] = thirdInput;
                    map[x - 1, y] = fourthInput;
                }

                List<Tuple<int, int>> vars = new List<Tuple<int, int>>();
                for (int i = 0; i < thirdInitInput; i++)
                {
                    Console.Error.WriteLine($"x: {x} y: {y}");
                    string[] inputs = Console.ReadLine().Split(' ');
                    Console.Error.WriteLine(inputs[0]);
                    int fifthInput = int.Parse(inputs[0]);
                    Console.Error.WriteLine($"5.{i}: {fifthInput}");
                    int sixthInput = int.Parse(inputs[1]);
                    Console.Error.WriteLine($"6.{i}: {sixthInput}");

                    vars.Add(new Tuple<int, int>(fifthInput, sixthInput));

                    //if (map[fifthInput-1, sixthInput-1] == "#" || answ)
                    //{
                    //    continue;
                    //}
                    //else if (map[fifthInput-1, sixthInput-1] == "."/* || map[fifthInput-1, sixthInput-1] == "."*/)
                    //{
                    //    Console.WriteLine(((char)('A' + i)).ToString());
                    //    x = fifthInput;
                    //    y = sixthInput;
                    //    answ = true;                        
                    //}

                }

                for (int i = 0; i < thirdInitInput; i++)
                {
                    int a = (vars[i].Item1 % vars[i].Item2) & 5;

                    if (firstInput == "#")
                    {
                        if (a == 1)
                        {
                            Console.WriteLine("A");
                            x = vars[0].Item1;
                            y = vars[0].Item2;
                            answ = true;
                            continue;
                        }
                    }
                    if (secondInput == "#")
                    {
                        if (a == 2)
                        {
                            Console.WriteLine("B");
                            answ = true;
                            x = vars[1].Item1;
                            y = vars[1].Item2;
                            continue;
                        }
                    }
                    if (thirdInput == "#")
                    {
                        if (a == 3)
                        {
                            Console.WriteLine("C");
                            answ = true;
                            x = vars[2].Item1;
                            y = vars[2].Item2;
                            continue;
                        }
                    }
                    if (fourthInput == "#")
                    {
                        if (a == 4)
                        {
                            Console.WriteLine("D");
                            answ = true;
                            x = vars[3].Item1;
                            y = vars[3].Item2;
                            continue;
                        }
                    }
                    if (a == 0)
                    {
                        Console.WriteLine(((char)('A' + i)).ToString());
                        x = vars[i].Item1;
                        y = vars[i].Item2;
                        answ = true;
                    }
                }

                //if (!answ)
                //{
                //    for (int i = 0; i < thirdInitInput; i++)
                //    {                        
                //        if (map[vars[i].Item1 - 1, vars[0].Item2 - 1] == "#" || answ)
                //        {
                //            continue;
                //        }
                //        else if (map[vars[i].Item1 - 1, vars[0].Item2 - 1] == "_"/* || map[fifthInput-1, sixthInput-1] == "."*/)
                //        {
                //            Console.WriteLine(((char)('A' + i)).ToString());
                //            x = vars[i].Item1;
                //            y = vars[0].Item2;
                //            answ = true;
                //        }
                //    }
                //}

                if (!answ)
                {
                    Console.WriteLine("A");
                    x = vars[0].Item1;
                    y = vars[0].Item2;
                }

                for (int i = 0; i<firstInitInput; i++)
                {
                    for (int j = 0; j<secondInitInput; j++)
                    {
                        Console.Error.Write(map[i, j]);
                    }
                    Console.Error.WriteLine();
                }

                    //if (firstInput == "#")
                    //{
                    //    if (a == 1)
                    //    {
                    //        Console.WriteLine("A");
                    //        Console.Error.WriteLine($"x {fifthInput} y {sixthInput}");
                    //        answ = true;
                    //        continue;
                    //    }
                    //}
                    //if (secondInput == "#")
                    //{
                    //    if (a == 2)
                    //    {
                    //        Console.WriteLine("B");
                    //        Console.Error.WriteLine($"x {fifthInput} y {sixthInput}");
                    //        answ = true;
                    //        continue;
                    //    }
                    //}
                    //if (thirdInput == "#")
                    //{
                    //    if (a == 3)
                    //    {
                    //        Console.WriteLine("C");
                    //        Console.Error.WriteLine($"x {fifthInput} y {sixthInput}");
                    //        answ = true;
                    //        continue;
                    //    }
                    //}
                    //if (fourthInput == "#")
                    //{
                    //    if (a == 4)
                    //    {
                    //        Console.WriteLine("D");
                    //        Console.Error.WriteLine($"x {fifthInput} y {sixthInput}");
                    //        answ = true;
                    //        continue;
                    //    }
                    //}
                    //if (a == 0)
                    //{
                    //    Console.WriteLine(((char)('A' + i)).ToString());
                    //    answ = true;
                    //}

                    //if (fifthInput + sixthInput > max)
                    //{
                    //    answ = ((char)('A' + i)).ToString();
                    //    max = fifthInput + sixthInput;
                    //}
                
                //if (!answ)
                //{
                //    Console.WriteLine("A");
                //}

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                //Console.WriteLine('A');
                //c++;
            }
        }
    }
}

//............................
//............................
//............................
//............................
//............................
//............................
//............................
//............................
//............................
//............................
//..............._............ 1 0
//..............#_#........... 1 2
//..............__#........... 2 1
//.............___#........... 3 1
//..._____..____#_#........... 10 2
//..____________#_............ 13 1
//.._###______#_.............. 8 4
//.._#_.______................ 8 1
//.._#_.______................ 8 1
//.._#_.___##................. 5 3
//.._______._____________..... 64
//..______________________.... 87
//..___###._###########___.... 80
//..___...._#_.........___.__. 89
//..___...._#_........._______ 100
//..________#_.........____##. 110
//..________#_.........____#_. 122
//...########...........####.. 110
//............................
//............................
//............................
//............................
//............................
//............................
//............................