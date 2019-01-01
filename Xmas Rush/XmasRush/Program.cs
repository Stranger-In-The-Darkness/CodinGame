using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmasRush
{
    public struct Tile
    {
        int[] dir;
        List<int> passes;

        int value;
        bool passed;

        public Tile(int left = 0, int up = 0, int right = 0, int down = 0)
        {
            dir = new int[4] { up, right, down, left };
            value = 100;
            passed = false;
            passes = new List<int>(4);
        }

        public Tile(string tile)
        {
            if (tile.Length < 4)
            {
                Console.Error.WriteLine(tile);
                throw new ArgumentException("Not enought arguments");
            }
            else
            {
                dir = new int[4]
                {
                    int.Parse(tile[0].ToString()),
                    int.Parse(tile[1].ToString()),
                    int.Parse(tile[2].ToString()),
                    int.Parse(tile[3].ToString())
                };
            }
            value = 100;
            passed = false;
            passes = new List<int>(4);
        }

        public int this[int index]
        {
            get
            {
                index = index % 4;
                if (index < 0)
                {
                    index = 4 - index;
                }
                return dir[index];
            }
        }

        public bool Passed
        {
            get
            {
                return passed;
            }
        }

        public int Value
        {
            get
            {
                return value;
            }
        }

        public List<int> Passes
        {
            get
            {
                return passes;
            }
        }

        public int Pass(int value)
        {
            passed = true;
            this.value = value;
            return this.value;
        }

        public void Unpass()
        {
            passed = false;
            value = 100;
            ClearPasses();
        }

        public void AddPass(int direction)
        {
            passes.Add(direction);
        }

        private void ClearPasses()
        {
            passes.Clear();
            for (int i = 0; i<4; i++)
            {
                if (dir[i] == 0)
                {
                    passes.Add(i);
                }
            }
        }
    }

    public struct Item
    {
        int x, y, playerID;
        string name;

        public Item (string name, int x, int y, int playerID)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.playerID = playerID;
        }

        public Tuple<int, int> Location
        {
            get
            {
                return new Tuple<int, int>(x, y);
            }
            set
            {
                if (value.Item1 == x && value.Item2 == y)
                {
                    return;
                }
                else
                {
                    x = value.Item1;
                    y = value.Item2;
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int PlayerID
        {
            get
            {
                return playerID;
            }
        }
    }

    public static class Container
    {
        private static List<Tile[,]> targets = new List<Tile[,]>()
        {
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7]
        };
        private static List<Tile[,]> dangers = new List<Tile[,]>()
        {
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7]
        };

        private static List<Tile[,]> oldTargets = new List<Tile[,]>()
        {
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7]
        };
        private static List<Tile[,]> oldDangers = new List<Tile[,]>()
        {
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7],
            new Tile[7, 7]
        };

        private static int indexTarget = 0;
        private static int indexDanger = 0;
        private static object o = new object();

        public static int PickTarget()
        {
            lock (o)
            {
                return indexTarget++;
            }
        }

        public static int PickDanger()
        {
            lock (o)
            {
                return indexDanger++;
            }
        }

        public static Tile[,] GetTarget(int index)
        {
            return targets[index];
        }
        
        public static Tile[,] GetDanger(int index)
        {
            return dangers[index];
        }

        public static Tile[,] GetPackedTarget(int index)
        {
            return oldTargets[index];
        }

        public static Tile[,] GetPackedDanger(int index)
        {
            return oldDangers[index];
        }

        public static int GetDirectionFromIndex(int index, out int row)
        {
            row = index % 7;
            return index / 7;
        }

        public static void SetTarget(int index, Tile[,] value)
        {
            targets[index] = value;
        }

        public static void SetDanger(int index, Tile[,] value)
        {
            dangers[index] = value;
        }

        public static void PackTarget()
        {
            oldTargets = new List<Tile[,]>(targets);
        }

        public static void PackDanger()
        {
            oldDangers = new List<Tile[,]>(dangers);
        }
    }

    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    class Player
    {
        const int PUSH = 0;
        const int MOVE = 1;

        static Tile[,] map = new Tile[7, 7];

        static void Main(string[] args)
        {
            string[] inputs;

            int playerX = 0, playerY = 0,
                enemyX = 0, enemyY = 0;
            Tile playerTile = new Tile();

            List<string> quests = new List<string>();

            // game loop
            while (true)
            {
                List<Item> itemList = new List<Item>();
                int turnType = int.Parse(Console.ReadLine());
                for (int i = 0; i < 7; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    for (int j = 0; j < 7; j++)
                    {
                        string tile = inputs[j];
                        map[i, j] = new Tile(tile);
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int numPlayerCards = int.Parse(inputs[0]); // the total number of quests for a player (hidden and revealed)
                    if (i == 0)
                    {
                        playerX = int.Parse(inputs[1]);
                        playerY = int.Parse(inputs[2]);
                        playerTile = new Tile(inputs[3]);
                    }
                    else
                    {
                        enemyX = int.Parse(inputs[1]);
                        enemyY = int.Parse(inputs[2]);
                    }
                }
                int numItems = int.Parse(Console.ReadLine()); // the total number of items available on board and on player tiles
                
                for (int i = 0; i < numItems; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    string itemName = inputs[0];
                    int itemX = int.Parse(inputs[1]);
                    int itemY = int.Parse(inputs[2]);
                    int itemPlayerId = int.Parse(inputs[3]);

                    itemList.Add(new Item(itemName, itemX, itemY, itemPlayerId));

                    if (itemPlayerId == 0)
                    {
                        CalculateTarget(itemX, itemY, value: 0, target: false, danger: false);
                    }
                }
                int numQuests = int.Parse(Console.ReadLine()); // the total number of revealed quests for both players
                for (int i = 0; i < numQuests; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    string questItemName = inputs[0];
                    int questPlayerId = int.Parse(inputs[1]);
                    if (questPlayerId == 0)
                    {
                        quests.Add(questItemName);
                    }
                }

                string command = "";

                for(int i = 0; i<7; i++)
                {
                    string er = "";
                    for (int j = 0; j<7; j++)
                    {
                        string s = map[i, j].Value.ToString();
                        if(s.Length == 1)
                        {
                            s = "-" + s + "-";
                        }
                        else if (s.Length == 2)
                        {
                            s = "-" + s;
                        }
                        s += " ";
                        er += s;
                    }
                    Console.Error.WriteLine(er);
                }
                Console.Error.WriteLine();

                if (turnType == PUSH)
                {
                    List<Task> t = new List<Task>();

                    for (int i = 0; i<4; i++)
                    {
                        t.Add(Task.Run(() =>
                        {
                            for (int n = 0; n<7; n++)
                            {
                                Move(i, n, playerTile, target: true, index: i * 7 + n);
                                Move(i, n, playerTile, danger: true, index: i * 7 + n);

                                foreach (Item item in itemList)
                                {
                                    if (item.PlayerID == 0)
                                    {
                                        if (item.Location.Item1 > -1)
                                        {
                                            CalculateTarget(item.Location.Item1, item.Location.Item2, value: 0, target: true, danger: false);
                                        }
                                    }
                                    else 
                                    {
                                        if (item.Location.Item1 > -1)
                                        {
                                            CalculateTarget(item.Location.Item1, item.Location.Item2, value: 0, target: false, danger: true);
                                        }
                                    }
                                }
                            }
                        }));
                    }
                    try
                    {
                        Task.WaitAll(t.ToArray());
                    }
                    catch
                    {
                        Console.Error.WriteLine("Done");
                    }

                    int com = -1;
                    int gTarget = 100;
                    int gDanger = 0;
                    int newPlayerX = playerX, newPlayerY = playerY,
                                newEnemyX = enemyX, newEnemyY = enemyY;

                    for (int n = 0; n < 4; n++)
                    {
                        for (int m = 0; m < 7; m++)
                        {
                            if (n % 2 == 0)
                            {
                                if (playerX == m)
                                {
                                    newPlayerY += -1 + (n / 2) * 2;
                                    newPlayerY = newPlayerY < 0 ? 0 : newPlayerY;
                                    newPlayerY = newPlayerY > 6 ? 6 : newPlayerY;
                                }
                                if (enemyX == m)
                                {
                                    newEnemyY += -1 + (n / 2) * 2;
                                    newEnemyY = newEnemyY < 0 ? 0 : newEnemyY;
                                    newEnemyY = newEnemyY > 6 ? 6 : newEnemyY;
                                }
                            }
                            else
                            {
                                if (playerY == m)
                                {
                                    newPlayerX += 1 - (n / 2) * 2;
                                    newPlayerX = newPlayerX < 0 ? 0 : newPlayerX;
                                    newPlayerX = newPlayerX > 6 ? 6 : newPlayerX;
                                }
                                if (enemyY == m)
                                {
                                    newEnemyX += 1 - (n / 2) * 2;
                                    newEnemyX = newEnemyX < 0 ? 0 : newEnemyX;
                                    newEnemyX = newEnemyX > 6 ? 6 : newEnemyX;
                                }
                            }

                            Console.Error.WriteLine("Player {0}.{1}", newPlayerX, newPlayerY);
                            Console.Error.WriteLine("Enemy {0}.{1}", newEnemyX, newEnemyY);

                            int target = Container.GetTarget(n * 7 + m)[newPlayerY, newPlayerX].Value;
                            int danger = Container.GetDanger(n * 7 + m)[newEnemyY, newEnemyX].Value;

                            if (target <= danger)
                            {
                                if (target < gTarget || danger > gDanger)
                                {
                                    com = n * 7 + m;
                                    target = gTarget;
                                    danger = gDanger;
                                }
                            }
                            else if (danger > gDanger)                                
                            {
                                com = n * 7 + m;
                            }
                            else if (target < gTarget)
                            {
                                com = n * 7 + m;
                            }
                        }
                    }
                    if (com > -1)
                    {
                        int i = 0, n;
                        i = Container.GetDirectionFromIndex(com, out n);

                        Console.Error.WriteLine("{0} {1} {2}", i, n, com);

                        command = "PUSH " + n + " " + ((DIRECTION)i).ToString();
                    }
                    else
                    {
                        t = new List<Task>();

                        Container.PackTarget();
                        Container.PackDanger();

                        for (int v = 0; v < 28; v++)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                t.Add(Task.Run(() =>
                                {
                                    for (int n = 0; n < 7; n++)
                                    {
                                        Move(i, n, playerTile, target: true, index: i * 7 + n);
                                        Move(i, n, playerTile, danger: true, index: i * 7 + n);

                                        foreach (Item item in itemList)
                                        {
                                            if (item.PlayerID == 0)
                                            {
                                                if (item.Location.Item1 > -1)
                                                {
                                                    CalculateTarget(item.Location.Item1, item.Location.Item2, value: 0, target: true, danger: false);
                                                }
                                            }
                                            else
                                            {
                                                if (item.Location.Item1 > -1)
                                                {
                                                    CalculateTarget(item.Location.Item1, item.Location.Item2, value: 0, target: false, danger: true);
                                                }
                                            }
                                        }
                                    }
                                }));
                            }
                            try
                            {
                                Task.WaitAll(t.ToArray());
                            }
                            catch
                            {
                                Console.Error.WriteLine("Done");
                            }

                            for (int n = 0; n < 4; n++)
                            {
                                for (int m = 0; m < 7; m++)
                                {
                                    if (n % 2 == 0)
                                    {
                                        if (newPlayerX == m)
                                        {
                                            newPlayerY += -1 + (n / 2) * 2;
                                            newPlayerY = newPlayerY < 0 ? 0 : newPlayerY;
                                            newPlayerY = newPlayerY > 6 ? 6 : newPlayerY;
                                        }
                                        if (newEnemyX == m)
                                        {
                                            newEnemyY += -1 + (n / 2) * 2;
                                            newEnemyY = newEnemyY < 0 ? 0 : newEnemyY;
                                            newEnemyY = newEnemyY > 6 ? 6 : newEnemyY;
                                        }
                                    }
                                    else
                                    {
                                        if (newPlayerY == m)
                                        {
                                            newPlayerX += 1 - (n / 2) * 2;
                                            newPlayerX = newPlayerX < 0 ? 0 : newPlayerX;
                                            newPlayerX = newPlayerX > 6 ? 6 : newPlayerX;
                                        }
                                        if (newEnemyY == m)
                                        {
                                            newEnemyX += 1 - (n / 2) * 2;
                                            newEnemyX = newEnemyX < 0 ? 0 : newEnemyX;
                                            newEnemyX = newEnemyX > 6 ? 6 : newEnemyX;
                                        }
                                    }

                                    Console.Error.WriteLine("Player {0}.{1}", newPlayerX, newPlayerY);
                                    Console.Error.WriteLine("Enemy {0}.{1}", newEnemyX, newEnemyY);

                                    int target = Container.GetTarget(n * 7 + m)[newPlayerY, newPlayerX].Value;
                                    int danger = Container.GetDanger(n * 7 + m)[newEnemyY, newEnemyX].Value;

                                    if (target <= danger)
                                    {
                                        if (target < gTarget || danger > gDanger)
                                        {
                                            com = v;
                                            target = gTarget;
                                            danger = gDanger;
                                        }
                                    }
                                    else if (danger > gDanger)
                                    {
                                        com = v;
                                    }
                                    else if (target < gTarget)
                                    {
                                        com = v;
                                    }
                                }
                            }
                        }
                        if (com > -1)
                        {
                            int i = 0, n;
                            i = Container.GetDirectionFromIndex(com, out n);

                            Console.Error.WriteLine("{0} {1} {2}", i, n, com);

                            command = "PUSH " + n + " " + ((DIRECTION)i).ToString();
                        }
                    }
                }
                else if (turnType == MOVE)
                {
                    command = "MOVE";
                    for (int i = 0; i < 4; i++)
                    {
                        int nX = -1, nY = -1;
                        if (CanMove(map, i, playerX, playerY, out nX, out nY))
                        {
                            if (map[nY, nX].Value <= map[playerY, playerX].Value)
                            {
                                var hor = nX - playerX;
                                var ver = nY - playerY;
                                if (hor != 0)
                                {
                                    if (hor > 0)
                                    {
                                        command += " RIGHT";
                                        playerX++;
                                    }
                                    else
                                    {
                                        command += " LEFT";
                                        playerX--;
                                    }
                                }
                                else if (ver != 0)
                                {
                                    if (ver > 0)
                                    {
                                        command += " DOWN";
                                        playerY++;
                                    }
                                    else
                                    {
                                        command += " UP";
                                        playerY--;
                                    }
                                }
                            }
                        }
                    }
                    if (command.Length <= 4)
                    {
                        command = "PASS";
                    }
                }

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine(command); // PUSH <id> <direction> | MOVE <direction> | PASS
                for (int i = 0; i<7; i++)
                {
                    for (int j = 0; j<7; j++)
                    {
                        map[i, j].Unpass();
                    }
                }
            }
        }

        public static void CalculateTarget(int dX, int dY, int value = 0, bool target = false, bool danger = false)
        {
            if (target)
            {
                var index = Container.PickTarget();
                List<Task> t = new List<Task>();
                Tile[,] targets = Container.GetTarget(index);

                if (!targets[dY, dX].Passed || targets[dY, dX].Value > value)
                {
                    targets[dX, dY].Pass(value);
                    Container.SetTarget(index, targets);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!targets[dY, dX].Passes.Contains(i))
                    {
                        int nX, nY;
                        if (CanMove(targets, i, dX, dY, out nX, out nY))
                        {
                            targets[dY, dX].AddPass(i);
                            targets[nY, nX].AddPass((i + 2) % 4);

                            //Console.Error.WriteLine("{0} {1} / {2} {3}", dX, dY, nX, nY);
                            t.Add(Task.Run(() => CalculateTarget(nX, nY, value + 1, target, danger, index)));
                        }
                    }
                }

                try
                {
                    Task.WaitAll(t.ToArray());
                }
                catch
                {

                }

                if (targets[dY, dX].Passes.Count == 4)
                {
                    return;
                }
            }
            else if (danger)
            {
                var index = Container.PickDanger();
                List<Task> t = new List<Task>();
                Tile[,] dangers = Container.GetTarget(index);

                if (!dangers[dY, dX].Passed || dangers[dY, dX].Value > value)
                {
                    dangers[dX, dY].Pass(value);
                    Container.SetDanger(index, dangers);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!dangers[dY, dX].Passes.Contains(i))
                    {
                        int nX, nY;
                        if (CanMove(dangers, i, dX, dY, out nX, out nY))
                        {
                            dangers[dY, dX].AddPass(i);
                            dangers[nY, nX].AddPass((i + 2) % 4);

                            //Console.Error.WriteLine("{0} {1} / {2} {3}", dX, dY, nX, nY);
                            t.Add(Task.Run(() => CalculateTarget(nX, nY, value + 1, target, danger, index)));
                        }
                    }
                }

                try
                {
                    Task.WaitAll(t.ToArray());
                }
                catch
                {

                }

                if (dangers[dY, dX].Passes.Count == 4)
                {
                    return;
                }
            }
            else
            {
                List<Task> t = new List<Task>();

                if (!map[dY, dX].Passed || map[dY, dX].Value > value)
                {
                    map[dY, dX].Pass(value);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!map[dY, dX].Passes.Contains(i))
                    {
                        int nX, nY;
                        if (CanMove(map, i, dX, dY, out nX, out nY))
                        {
                            map[dY, dX].AddPass(i);
                            map[nY, nX].AddPass((i + 2) % 4);

                            //Console.Error.WriteLine("{0} {1} / {2} {3}", dX, dY, nX, nY);
                            t.Add(Task.Run(() => CalculateTarget(nX, nY, value + 1, false, false)));
                        }
                    }
                }

                try
                {
                    Task.WaitAll(t.ToArray());
                }
                catch
                {

                }

                if (map[dY, dX].Passes.Count == 4)
                {
                    return;
                }
            }
        }

        public static void CalculateTarget(int dX, int dY, int value = 0, bool target = false, bool danger = false, int index = -1)
        {
            if (target)
            {
                List<Task> t = new List<Task>();
                Tile[,] targets = Container.GetTarget(index);

                if (!targets[dY, dX].Passed || targets[dY, dX].Value > value)
                {
                    targets[dX, dY].Pass(value);
                    Container.SetTarget(index, targets);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!targets[dY, dX].Passes.Contains(i))
                    {
                        int nX, nY;
                        if (CanMove(targets, i, dX, dY, out nX, out nY))
                        {
                            targets[dY, dX].AddPass(i);
                            targets[nY, nX].AddPass((i + 2) % 4);

                            //Console.Error.WriteLine("{0} {1} / {2} {3}", dX, dY, nX, nY);
                            t.Add(Task.Run(() => CalculateTarget(nX, nY, value + 1, target, danger, index)));
                        }
                    }
                }

                try
                {
                    Task.WaitAll(t.ToArray());
                }
                catch
                {

                }

                if (targets[dY, dX].Passes.Count == 4)
                {
                    return;
                }
            }
            else if (danger)
            {
                List<Task> t = new List<Task>();
                Tile[,] dangers = Container.GetTarget(index);

                if (!dangers[dY, dX].Passed || dangers[dY, dX].Value > value)
                {
                    dangers[dX, dY].Pass(value);
                    Container.SetDanger(index, dangers);
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!dangers[dY, dX].Passes.Contains(i))
                    {
                        int nX, nY;
                        if (CanMove(dangers, i, dX, dY, out nX, out nY))
                        {
                            dangers[dY, dX].AddPass(i);
                            dangers[nY, nX].AddPass((i + 2) % 4);

                            //Console.Error.WriteLine("{0} {1} / {2} {3}", dX, dY, nX, nY);
                            t.Add(Task.Run(() => CalculateTarget(nX, nY, value + 1, target, danger, index)));
                        }
                    }
                }

                try
                {
                    Task.WaitAll(t.ToArray());
                }
                catch
                {

                }

                if (dangers[dY, dX].Passes.Count == 4)
                {
                    return;
                }
            }
        }

        public static bool CanMove(Tile[,] array, int direction, int pX, int pY, out int nX, out int nY)
        {
            int newX = pX, newY = pY;
            switch (direction)
            {
                case 0:
                newY--;
                break;
                case 1:
                newX++;
                break;
                case 2:
                newY++;
                break;
                case 3:
                newX--;
                break;
            }
            if (newX > 6 || newX < 0 || newY > 6 || newY < 0)
            {
                nX = pX;
                nY = pY;
                return false;
            }
            if (array[pY, pX][direction] + array[newY, newX][(direction + 2) % 4] == 2)
            {
                nX = newX;
                nY = newY;
                return true;
            }
            nX = -1;
            nY = -1;
            return false;
        }

        public static void Move(int direction, int n, Tile replace, bool target = false, bool danger = false, int index = -1)
        {
            Tile[,] array = new Tile[7,7];
            if (target)
            {
                array = Container.GetTarget(index);
            }
            else if (danger)
            {
                array = Container.GetDanger(index);
            }
            switch (direction)
            {
                case 0:
                {
                    for (int i = 1; i<7; i++)
                    {
                        array[i-1, n] = array[i, n];
                    }
                    array[6, n] = replace;
                }
                break;
                case 1:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        array[n, i + 1] = array[n, i];
                    }
                    array[n, 0] = replace;
                }
                break;
                case 2:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        array[i + 1, n] = array[i, n];
                    }
                    array[0, n] = replace;
                }
                break;
                case 3:
                {
                    for (int i = 0; i < 6; i++)
                    {
                        array[n, i] = array[n, i + 1];
                    }
                    array[n, 6] = replace;
                }
                break;
            }

            if (target)
            {
                Container.SetTarget(index, array);
            }
            else if (danger)
            {
                Container.SetDanger(index, array);
            }
        }
    }
}