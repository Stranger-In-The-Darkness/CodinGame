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
    public struct Card
    {
        public int cardNumber;
        public int instanceID;
        public int location;
        public int cardType;
        public int cost;
        public int attack;
        public int defense;
        public string abilities;
        public int myHealthChange;
        public int opponentHealthChange;
        public int cardDraw;

        public Card(int cardNumber, int instanceID, int location, int cost, int attack, int defense)
        {
            this.cardNumber = cardNumber;
            this.instanceID = instanceID;
            this.location = location;
            this.cardType = 0;
            this.cost = cost;
            this.attack = attack;
            this.defense = defense;
            this.abilities = string.Empty;
            this.myHealthChange = 0;
            this.opponentHealthChange = 0;
            this.cardDraw = 0;
        }

        public Card(int cardNumber, int instanceID, int location, int cost, int attack, int defense, string abilities, int myHealthChange, int opponentHealthChange, int cardDraw)
        {
            this.cardNumber = cardNumber;
            this.instanceID = instanceID;
            this.location = location;
            this.cardType = 0;
            this.cost = cost;
            this.attack = attack;
            this.defense = defense;
            this.abilities = abilities;
            this.myHealthChange = myHealthChange;
            this.opponentHealthChange = opponentHealthChange;
            this.cardDraw = cardDraw;
        }

        public Card(int cardNumber, int instanceID, int location, int cardType, int cost, int attack, int defense, string abilities, int myHealthChange, int opponentHealthChange, int cardDraw)
        {
            this.cardNumber = cardNumber;
            this.instanceID = instanceID;
            this.location = location;
            this.cardType = cardType;
            this.cost = cost;
            this.attack = attack;
            this.defense = defense;
            this.abilities = abilities;
            this.myHealthChange = myHealthChange;
            this.opponentHealthChange = opponentHealthChange;
            this.cardDraw = cardDraw;
        }
    }
    static void Main(string[] args)
    {
        string[] inputs;

        int turns = 0;
        // game loop
        while (true)
        {
            int currentMana = 0, playerDeck = 0, playerHealth = 0;
            bool isGuarded = false;
            List<Card> cardList = new List<Card>();

            for (int i = 0; i < 2; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                playerHealth = int.Parse(inputs[0]);
                currentMana = int.Parse(inputs[1]);
                playerDeck = int.Parse(inputs[2]);
                int playerRune = int.Parse(inputs[3]);
            }
            int opponentHand = int.Parse(Console.ReadLine());
            int cardCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < cardCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int cardNumber = int.Parse(inputs[0]);
                int instanceId = int.Parse(inputs[1]);
                int location = int.Parse(inputs[2]);
                int cardType = int.Parse(inputs[3]);
                int cost = int.Parse(inputs[4]);
                int attack = int.Parse(inputs[5]);
                int defense = int.Parse(inputs[6]);
                string abilities = inputs[7];
                Console.Error.WriteLine(abilities);
                int myHealthChange = int.Parse(inputs[8]);
                int opponentHealthChange = int.Parse(inputs[9]);
                int cardDraw = int.Parse(inputs[10]);

                cardList.Add(new Card(cardNumber, instanceId, location, cost, attack, defense, abilities, myHealthChange, opponentHealthChange, cardDraw));
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            string command = string.Empty;

            if (turns < 30)
            {
                int max = cardList.Select((s) => s.attack + s.defense).ToList().Max();
                var el = cardList.Where((s) => (s.attack + s.defense) == max).ToList()[0];
                if (cardList.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                {
                    max = cardList.Where((s) => s.abilities.Contains("G")).Select((s) => s.attack + s.defense).ToList().Max();
                    el = cardList.Where((s) => s.abilities.Contains("G")).Where((s) => (s.attack + s.defense) == max).ToList()[0];
                }
                int id = cardList.IndexOf(el);
                Console.Error.WriteLine($"{max} {id} {cardList.Count}");
                command += $"PICK {id}";
            }
            else
            {
                Console.Error.WriteLine($"Card list: {cardList.Count}");

                int attackMax = 0, enemyMinDef = int.MaxValue;
                List<Card> hand = new List<Card>(),
                           greenItems = new List<Card>(),
                           redItems = new List<Card>(),
                           blueItems = new List<Card>(),
                           board = new List<Card>(),
                           enemyBoard = new List<Card>();

                foreach (Card c in cardList)
                {
                    if (c.location == -1)
                    {
                        enemyBoard.Add(c);
                        if (c.defense < enemyMinDef)
                        {
                            enemyMinDef = c.defense;
                        }
                    }
                    else if (c.location == 0)
                    {
                        if(c.cardType == 0)
                        {
                            hand.Add(c);
                        }
                        else if (c.cardType == 1)
                        {
                            greenItems.Add(c);
                        }
                        else if (c.cardType == 2)
                        {
                            redItems.Add(c);
                        }
                        else if (c.cardType == 3)
                        {
                            blueItems.Add(c);
                        }
                    }
                    else if (c.location == 1)
                    {
                        board.Add(c);
                        if (c.attack > attackMax)
                        {
                            attackMax = c.attack;
                        }
                        if (c.abilities.Contains("G"))
                        {
                            isGuarded = true;
                        }
                    }
                }

                Console.Error.WriteLine($"Board: {board.Count}\nHand: {hand.Count}");

                if (blueItems.Count != 0)
                {
                    Console.Error.WriteLine("Blue item action");
                    var av = blueItems.Where((s) => s.cost <= currentMana).ToList();
                    string c = "";
                    int summon = -1, attacked = -1;
                    if (playerHealth <= 30 && av.Where((s) => s.myHealthChange > 0).ToList().Count > 0)
                    {
                        summon = av.Where((s) => s.myHealthChange > 0).First().instanceID;
                        c = $"USE {summon} {attacked}";
                    }
                    else if (av.Where((s) => s.opponentHealthChange < 0).ToList().Count > 0)
                    {
                        summon = av.Where((s) => s.opponentHealthChange < 0).First().instanceID;
                        c = $"USE {summon} {attacked}";
                    }
                    if (c != "")
                    {
                        command += command == "" ? c : $";{c}";
                    }
                }

                if (hand != null && hand.Count != 0)
                {
                    Console.Error.WriteLine("Hand action");
                    var av = hand.Where((s) => s.cost <= currentMana).ToList();
                    if (av != null && av.Count != 0 && ((isGuarded && board.Count < 6) || (!isGuarded && board.Count < 4)))
                    {
                        int summon = -1;
                        string c = "";
                        if (!isGuarded && av.Where((s) => s.abilities.Contains("G")).ToList().Count > 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("G")).First().instanceID;
                            if (av.Where((s) => s.instanceID == summon).First().abilities.Contains("C"))
                            {
                                int attacked = -1;
                                if (enemyBoard != null && enemyBoard.Count > 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    }
                                    else
                                    {
                                        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    }
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                                else
                                {
                                    attacked = -1;
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                            }
                            else
                            {
                                c = $"SUMMON {summon}";
                                currentMana -= av.Where((s) => s.abilities.Contains("G")).First().cost;
                            }
                        }
                        else if(av.Where((s)=>s.abilities.Contains("W")).ToList().Count > 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("W")).First().instanceID;
                            c = $"SUMMON {summon}";
                            currentMana -= av.Where((s) => s.abilities.Contains("W")).First().cost;
                            if (av.Where((s) => s.instanceID == summon).First().abilities.Contains("C"))
                            {
                                int attacked = -1;
                                if (enemyBoard != null && enemyBoard.Count > 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    }
                                    else
                                    {
                                        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    }
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                                else
                                {
                                    attacked = -1;
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                            }
                            else
                            {
                                c = $"SUMMON {summon}";
                                currentMana -= av.Where((s) => s.abilities.Contains("G")).First().cost;
                            }
                        }
                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count > 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("B")).First().instanceID;
                            c = $"SUMMON {summon}";
                            currentMana -= av.Where((s) => s.abilities.Contains("B")).First().cost;
                            if (av.Where((s) => s.instanceID == summon).First().abilities.Contains("C"))
                            {
                                int attacked = -1;
                                if (enemyBoard != null && enemyBoard.Count > 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    }
                                    else
                                    {
                                        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    }
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                                else
                                {
                                    attacked = -1;
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                            }
                            else
                            {
                                c = $"SUMMON {summon}";
                                currentMana -= av.Where((s) => s.abilities.Contains("G")).First().cost;
                            }
                        }
                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count > 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("D")).First().instanceID;
                            c = $"SUMMON {summon}";
                            currentMana -= av.Where((s) => s.abilities.Contains("D")).First().cost;
                            if (av.Where((s) => s.instanceID == summon).First().abilities.Contains("C"))
                            {
                                int attacked = -1;
                                if (enemyBoard != null && enemyBoard.Count > 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    }
                                    else
                                    {
                                        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    }
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                                else
                                {
                                    attacked = -1;
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                            }
                            else
                            {
                                c = $"SUMMON {summon}";
                                currentMana -= av.Where((s) => s.abilities.Contains("G")).First().cost;
                            }
                        }
                        else if (av.Where((s) => s.abilities.Contains("L")).ToList().Count > 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("L")).First().instanceID;
                            c = $"SUMMON {summon}";
                            currentMana -= av.Where((s) => s.abilities.Contains("L")).First().cost;
                            if (av.Where((s) => s.instanceID == summon).First().abilities.Contains("C"))
                            {
                                int attacked = -1;
                                if (enemyBoard != null && enemyBoard.Count > 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    }
                                    else
                                    {
                                        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    }
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                                else
                                {
                                    attacked = -1;
                                    c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                    currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                                }
                            }
                            else
                            {
                                c = $"SUMMON {summon}";
                                currentMana -= av.Where((s) => s.abilities.Contains("G")).First().cost;
                            }
                        }
                        else if (av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).ToList().Count != 0)
                        {
                            summon = av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().instanceID;
                            int attacked = -1;
                            if (enemyBoard != null && enemyBoard.Count > 0)
                            {
                                if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                {
                                    attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                }
                                else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                {
                                    attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                }
                                else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                {
                                    attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                }
                                else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                {
                                    attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                }
                                else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                {
                                    attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                }
                                else
                                {
                                    attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                }
                                c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                            }
                            else
                            {
                                attacked = -1;
                                c = $"SUMMON {summon};ATTACK {summon} {attacked}";
                                currentMana -= av.Where((s) => s.abilities.Contains("C") && s.cost * 2 <= currentMana).First().cost * 2;
                            }
                        }
                        else
                        {
                            c = $"SUMMON {av.First().instanceID}";
                            currentMana -= av.First().cost;
                        }
                        command += command == "" ? c : $";{c}";
                    }
                    else
                    {
                        command += "PASS";
                    }
                }

                if (board != null && board.Count != 0)
                {
                    Console.Error.WriteLine("Board action");
                    while (currentMana > 0)
                    {
                        var av = board.Where((s) => s.cost <= currentMana).ToList();
                        if (av.Count != 0)
                        {
                            if (enemyBoard != null && enemyBoard.Count > 0)
                            {
                                Console.Error.WriteLine($"Enemy board not empty");
                                int attacker = -1, attacked = -1;

                                if (av != null && av.Count != 0)
                                {
                                    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                        if (av.Where((s)=> s.abilities.Contains("L")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("L")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("B")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("D")).ToList().First().instanceID;
                                        }
                                        else if (playerHealth >= 15 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                        }
                                        else
                                        {
                                            attacker = av.Where((s) => s.attack == attackMax).First().instanceID;
                                        }
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                        if (av.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("L")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("B")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("D")).ToList().First().instanceID;
                                        }
                                        else if (playerHealth >= 15 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                        }
                                        else
                                        {
                                            attacker = av.Where((s) => s.attack == attackMax).First().instanceID;
                                        }
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                        if (av.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("L")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("B")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("D")).ToList().First().instanceID;
                                        }
                                        else if (playerHealth >= 15 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                        }
                                        else
                                        {
                                            attacker = av.Where((s) => s.attack == attackMax).First().instanceID;
                                        }
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                        attacker = av.Where((s) => s.attack == attackMax).First().instanceID;                                        
                                    }
                                    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    {
                                        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                        if (av.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("L")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("B")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("D")).ToList().First().instanceID;
                                        }
                                        else if (playerHealth >= 15 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                        }
                                        else
                                        {
                                            attacker = av.Where((s) => s.attack == attackMax).First().instanceID;
                                        }
                                    }
                                    else
                                    {
                                        if (enemyBoard.Where((s) => s.attack > attackMax).ToArray().Length > 0)
                                        {
                                            attacked = enemyBoard.Where((s) => s.attack > attackMax).First().instanceID;
                                        }
                                        else if (enemyMinDef > 3)
                                        {
                                            attacked = enemyBoard.Where((s) => s.defense == enemyMinDef).First().instanceID;
                                        }
                                        else
                                        {
                                            attacked = -1;
                                        }
                                        if (av.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("L")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("B")).ToList().First().instanceID;
                                        }
                                        else if (av.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.abilities.Contains("D")).ToList().First().instanceID;
                                        }
                                        else if (playerHealth >= 15 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                        {
                                            attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                        }
                                        else
                                        {
                                            attacker = av.Where((s) => s.attack == attackMax).First().instanceID;
                                        }
                                    }
                                    //if (av.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    //{
                                    //    attacker = av.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    //    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    //    }
                                    //    else
                                    //    {
                                    //        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    //    }
                                    //}
                                    //else if (playerHealth <= 10 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                    //{
                                    //    attacker = av.Where((s) => s.myHealthChange > 0).First().instanceID;
                                    //    if(enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    //    }
                                    //    else
                                    //    {
                                    //        attacked = enemyBoard.Where((s3) => s3.defense == enemyMinDef).First().instanceID;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    attacker = av.Where((s) => s.attack == attackMax).ToList().Count != 0 ? av.Where((s) => s.attack == attackMax).ToList()[0].instanceID : av.First().instanceID;
                                    //    if (enemyBoard.Where((s) => s.abilities.Contains("G")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("G")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("L")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("L")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("D")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("D")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("W")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("W")).First().instanceID;
                                    //    }
                                    //    else if (enemyBoard.Where((s) => s.abilities.Contains("B")).ToList().Count != 0)
                                    //    {
                                    //        attacked = enemyBoard.Where((s) => s.abilities.Contains("B")).First().instanceID;
                                    //    }
                                    //    else
                                    //    {
                                    //        if (enemyBoard.Where((s) => s.attack > attackMax).ToArray().Length > 0)
                                    //        {
                                    //            attacked = enemyBoard.Where((s) => s.attack > attackMax).First().instanceID;
                                    //        }
                                    //        else if (enemyMinDef > 3)
                                    //        {
                                    //            attacked = enemyBoard.Where((s) => s.defense == enemyMinDef).First().instanceID;
                                    //        }
                                    //        else
                                    //        {
                                    //            attacked = -1;
                                    //        }
                                    //    }
                                    //}
                                    var c = $"ATTACK {attacker} {attacked}";
                                    if (command.Contains(c))
                                    {
                                        break;
                                    }
                                    command += command == "" ? c : $";{c}";

                                    Console.Error.WriteLine($"Cost: {board.Where((s) => s.instanceID == attacker).ToList().Count}");
                                    currentMana -= board.Where((s) => s.instanceID == attacker).ToList().First().cost;
                                    Console.Error.WriteLine(currentMana);

                                    Console.Error.WriteLine(c);
                                }
                                else
                                {
                                    if (command == "")
                                    {
                                        command = "PASS";
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                Console.Error.WriteLine($"Enemy board empty");
                                int attacker = -1, attacked = -1;
                                if (playerHealth <= 10 && av.Where((s) => s.myHealthChange > 0).ToList().Count != 0)
                                {
                                    attacker = av.Where((s) => s.myHealthChange > 0).ToList().First().instanceID;
                                }
                                else if (av.Where((s) => s.opponentHealthChange > 0).ToList().Count != 0)
                                {
                                    attacker = av.Where((s) => s.opponentHealthChange > 0).ToList().First().instanceID;
                                }
                                else
                                {
                                    attacker = av.Where((s) => s.attack == attackMax).ToList().Count != 0 ? av.Where((s) => s.attack == attackMax).ToList()[0].instanceID : av.First().instanceID;
                                }
                                var c = $"ATTACK {attacker} {attacked}";
                                currentMana -= board.Where((s) => s.instanceID == attacker).First().cost;
                                command += command == "" ? c : $";{c}";
                            }
                        }
                        else
                        {
                            command = command == "" ? "PASS" : command;
                            break;
                        }
                    }
                }
            }
            Console.Error.WriteLine($"Command: {command}");
            Console.WriteLine(command);
            turns++;
            Console.Error.WriteLine(turns);
            cardList.Clear();
        }
    }
}