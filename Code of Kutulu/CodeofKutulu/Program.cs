using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using System.Threading.Tasks;

/**
 * Survive the wrath of Kutulu
 * Coded fearlessly by JohnnyYuge & nmahoude (ok we might have been a bit scared by the old god...but don't say anything)
 **/

class Map
{
    private static object o = new object();
    private static int[,] map;

    public int this[int index1, int index2]
    {
        get
        {
            return map[index1, index2];
        }
        set
        {
            lock (o)
            {
                map[index1, index2] = value;
            }
        }
    }

    public int[,] CMap
    {
        get
        {
            return map;
        }
        set
        {
            map = value;
        }
    }
}

class Player
{
    static int count = 0;

    static void Main(string[] args)
    {
        Map m = new Map();
        string[] inputs;
        int width = int.Parse(Console.ReadLine());
        int height = int.Parse(Console.ReadLine());

        string[] map = new string[height];
        int[,] iMap = new int[height, width];

        List<Task> taskList = new List<Task>();

        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine();
            map[i] = line;
            for (int j = 0; j < line.Length; j++)
            {
                switch (line[j])
                {
                    case '#':
                    iMap[i, j] = -10;
                    break;
                    case 'w':
                    iMap[i, j] = -5;
                    taskList.Add(Task.Factory.StartNew(() => CalcValues(iMap, height, width, j ,i)));
                    break;
                    case 'U':
                    iMap[i, j] = 10;
                    break;
                }
            }
        }

        taskList.Last().Wait();

        inputs = Console.ReadLine().Split(' ');
        int sanityLossLonely = int.Parse(inputs[0]); // how much sanity you lose every turn when alone, always 3 until wood 1
        int sanityLossGroup = int.Parse(inputs[1]); // how much sanity you lose every turn when near another player, always 1 until wood 1
        int wandererSpawnTime = int.Parse(inputs[2]); // how many turns the wanderer take to spawn, always 3 until wood 1
        int wandererLifeTime = int.Parse(inputs[3]); // how many turns the wanderer is on map after spawning, always 40 until wood 1

        int pID = -1;
        int pX = -1, pY = -1, prevX = -1, prevY = -1;
        string prevCommand = "";

        int lightsLeft = 0, plansLeft = 0;

        if (taskList.Where((t) => t.Status == TaskStatus.Running).Count() == 0)
            taskList.Clear();
        // game loop
        while (true)
        {
            int wanderersTargeted = 0;
            string command = "WAIT";

            int pSanity = 250;
            string currentEffect = "NONE";

            #region Entry
            int entityCount = int.Parse(Console.ReadLine()); // the first given entity corresponds to your explorer
            for (int i = 0; i < entityCount; i++)
            {
                if (i == 0)
                {
                    inputs = Console.ReadLine().Split(' ');
                    string entityType = inputs[0];
                    if (pID == -1)
                        pID = int.Parse(inputs[1]);
                    int x = int.Parse(inputs[2]);
                    if (pX != -1)
                    {
                        prevX = pX;
                        pX = x;
                    }
                    else
                    {
                        pX = x;
                    }
                    int y = int.Parse(inputs[3]);
                    if (pY != -1)
                    {
                        prevY = pY;
                        pY = y;
                    }
                    else
                    {
                        pY = y;
                    }
                    pSanity = int.Parse(inputs[4]);
                    plansLeft = int.Parse(inputs[5]);
                    lightsLeft = int.Parse(inputs[6]);
                }
                else
                {
                    inputs = Console.ReadLine().Split(' ');
                    string entityType = inputs[0];
                    int id = int.Parse(inputs[1]);
                    int x = int.Parse(inputs[2]);
                    int y = int.Parse(inputs[3]);
                    int param0 = int.Parse(inputs[4]);
                    int param1 = int.Parse(inputs[5]);
                    int param2 = int.Parse(inputs[6]);

                    if (entityType == "WANDERER")
                    {
                        m.CMap[y, x] = -5;
                        taskList.Add(Task.Factory.StartNew(() => CalcValues(m, height, width, x, y, 2)));
                        if (param2 == pID)
                        {
                            wanderersTargeted++;
                        }
                    }
                    else if (entityType == "SLASHER")
                    {

                    }
                    else if (entityType == "EFFECT_PLAN")
                    {
                        if (param1 == pID)
                        {
                            currentEffect = "EFFECT_PLAN";
                        }
                    }
                    else if (entityType == "EFFECT_LIGHT")
                    {
                        if (param1 == pID)
                        {
                            currentEffect = "EFFECT_LIGHT";
                        }
                    }
                    else if (entityType == "EFFECT_SHELTER")
                    {

                    }
                    else if (entityType == "EXPLORER")
                    {

                    }
                }
            }
            #endregion

            Console.Error.WriteLine($"X = {pX} Y = {pY}");

            if (taskList.Count > 0)
            {
                taskList.Last().Wait();
            }

            if (pX == 0)
            {
                if (pY == 0)
                {
                    if (m.CMap[pY, pX + 1] > m.CMap[pY + 1, pX])
                    {
                        if (map[pY][pX + 1] != '#')
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command && map[pY + 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY + 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command && map[pY][pX + 1] != '#')
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                    else if (m.CMap[pY, pX + 1] < m.CMap[pY + 1, pX])
                    {
                        if (map[pY + 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command && map[pY][pX + 1] != '#')
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY][pX + 1] != '#')
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command && map[pY + 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                }
                else if (pY == height - 1)
                {
                    if (m.CMap[pY, pX + 1] > m.CMap[pY - 1, pX])
                    {
                        if (map[pY][pX + 1] != '#')
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command && map[pY - 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY - 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command && map[pY][pX + 1] != '#')
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                    else if (m.CMap[pY, pX + 1] < m.CMap[pY - 1, pX])
                    {
                        if (map[pY - 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command && map[pY][pX + 1] != '#')
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY][pX + 1] != '#')
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command && map[pY - 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                }
                else
                {
                    int max = Math.Max(Math.Max(m.CMap[pY + 1, pX],
                        m.CMap[pY, pX + 1]),
                        m.CMap[pY - 1, pX]);
                    if (max != -1)
                    {
                        if (max == m.CMap[pY + 1, pX])
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX + 1])
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                        }
                        else if (max == m.CMap[pY - 1, pX])
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                        }
                    }
                    else
                    {
                        command = "WAIT";
                    }
                }
            }
            else if (pX == width - 1)
            {
                if (pY == 0)
                {
                    if (m.CMap[pY, pX - 1] > m.CMap[pY + 1, pX])
                    {
                        if (map[pY][pX - 1] != '#')
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command && map[pY + 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY + 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command && map[pY][pX + 1] != '#')
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                    else if (m.CMap[pY, pX - 1] < m.CMap[pY + 1, pX])
                    {
                        if (map[pY + 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command && map[pY][pX - 1] != '#')
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY][pX - 1] != '#')
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command && map[pY - 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                }
                else if (pY == height - 1)
                {
                    if (m.CMap[pY, pX - 1] > m.CMap[pY - 1, pX])
                    {
                        if (map[pY][pX - 1] != '#')
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command && map[pY - 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY - 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command && map[pY][pX - 1] != '#')
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                    else if (m.CMap[pY, pX - 1] < m.CMap[pY - 1, pX])
                    {
                        if (map[pY - 1][pX] != '#')
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command && map[pY][pX - 1] != '#')
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else if (map[pY][pX - 1] != '#')
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command && map[pY - 1][pX] != '#')
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                            else
                            {
                                command = "WAIT";
                            }
                        }
                        else
                        {
                            command = "WAIT";
                        }
                    }
                }
                else
                {
                    int max = Math.Max(Math.Max(m.CMap[pY + 1, pX],
                        m.CMap[pY, pX - 1]),
                        m.CMap[pY - 1, pX]);
                    if (max != -1)
                    {
                        if (max == m.CMap[pY + 1, pX])
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX - 1])
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                        }
                        else if (max == m.CMap[pY - 1, pX])
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                        }
                    }
                    else
                    {
                        command = "WAIT";
                    }
                }
            }
            else
            {
                if (pY == 0)
                {
                    int max = Math.Max(Math.Max(m.CMap[pY, pX - 1],
                        m.CMap[pY + 1, pX]),
                        m.CMap[pY, pX + 1]);
                    if (max != -1)
                    {
                        if (max == m.CMap[pY + 1, pX])
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX + 1])
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX - 1])
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                        }
                    }
                    else
                    {
                        command = "WAIT";
                    }
                }
                else if (pY == height - 1)
                {
                    int max = Math.Max(Math.Max(m.CMap[pY, pX - 1],
                        m.CMap[pY, pX + 1]),
                        m.CMap[pY - 1, pX]);
                    if (max > -1)
                    {
                        if (max == m.CMap[pY - 1, pX])
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX + 1])
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX - 1])
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                        }
                    }
                    else
                    {
                        command = "WAIT";
                    }
                }
                else
                {
                    int max = Math.Max(
                        Math.Max(
                        Math.Max(m.CMap[pY, pX - 1],
                        m.CMap[pY + 1, pX]),
                        m.CMap[pY, pX + 1]),
                        m.CMap[pY - 1, pX]);
                    if (max > -1)
                    {
                        if (max == m.CMap[pY + 1, pX])
                        {
                            command = $"MOVE {pX} {pY + 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX + 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX + 1])
                        {
                            command = $"MOVE {pX + 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY - 1}";
                            }
                        }
                        else if (max == m.CMap[pY - 1, pX])
                        {
                            command = $"MOVE {pX} {pY - 1}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX - 1} {pY}";
                            }
                        }
                        else if (max == m.CMap[pY, pX - 1])
                        {
                            command = $"MOVE {pX - 1} {pY}";
                            if (prevCommand == command)
                            {
                                command = $"MOVE {pX} {pY + 1}";
                            }
                        }
                    }
                    else
                    {
                        command = "WAIT";
                    }
                }
            }

            if (pSanity < 150 || pSanity < 50)
            {
                if (currentEffect == "NONE")
                {
                    if (plansLeft > 0)
                    {
                        command = "PLAN";
                    }
                    else if (lightsLeft > 0 && m.CMap[pY, pX] < -3 || wanderersTargeted >= 2)
                    {
                        command = "LIGHT";
                    }
                }
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");

            prevCommand = command == "" ? "WAIT" : command;
            Console.WriteLine(command == "" ? "WAIT" : command); // MOVE <x> <y> | WAIT
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Error.Write(m.CMap[i, j]);
                }
                Console.Error.Write("\n");
            }
            m.CMap = iMap;
        }
    }

    static void CalcValues(Map m, int height, int width, int x, int y, int offset = 3)
    {

        for (int i = x - 1; i >= (x - offset < 0 ? 0 : x - offset); i--)
        {
            if (m.CMap[y, i] != -1)
            {
                m.CMap[y, i] = m.CMap[y, i + 1] - 1;
                for (int j = y - 1; j >= (y - offset < 0 ? 0 : y - offset); j--)
                {
                    if (m.CMap[j, i] != -1)
                    {
                        m.CMap[j, i] = m.CMap[j + 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = y + 1; j < (y + offset > height ? height : y + offset); j++)
                {
                    if (m.CMap[j, i] != -1)
                    {
                        m.CMap[j, i] = m.CMap[j - 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
        for (int i = x + 1; i < (x + offset > width ? width : x + offset); i++)
        {
            if (m.CMap[y, i] != -1)
            {
                m.CMap[y, i] = m.CMap[y, i - 1] - 1;
                for (int j = y - 1; j >= (y - offset < 0 ? 0 : y - offset); j--)
                {
                    if (m.CMap[j, i] != -1)
                    {
                        m.CMap[j, i] = m.CMap[j + 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = y + 1; j < (y + offset > height ? height : y + offset); j++)
                {
                    if (m.CMap[j, i] != -1)
                    {
                        m.CMap[j, i] = m.CMap[j - 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
    static void CalcValues(int[,] map, int height, int width, int x, int y, int offset = 3)
    {

        for (int i = x - 1; i >= (x - offset < 0 ? 0 : x - offset); i--)
        {
            if (map[y, i] != -1)
            {
                map[y, i] = map[y, i + 1] - 1;
                for (int j = y - 1; j >= (y - offset < 0 ? 0 : y - offset); j--)
                {
                    if (map[j, i] != -1)
                    {
                        map[j, i] = map[j + 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = y + 1; j < (y + offset > height ? height : y + offset); j++)
                {
                    if (map[j, i] != -1)
                    {
                        map[j, i] = map[j - 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
        for (int i = x + 1; i < (x + offset > width ? width : x + offset); i++)
        {
            if (map[y, i] != -1)
            {
                map[y, i] = map[y, i - 1] - 1;
                for (int j = y - 1; j >= (y - offset < 0 ? 0 : y - offset); j--)
                {
                    if (map[j, i] != -1)
                    {
                        map[j, i] = map[j + 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = y + 1; j < (y + offset > height ? height : y + offset); j++)
                {
                    if (map[j, i] != -1)
                    {
                        map[j, i] = map[j - 1, i] + 1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }
}