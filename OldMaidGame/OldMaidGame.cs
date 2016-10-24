using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaidGame
{
    static class OldMaidGame
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            bool invalid = false;
            int numberPlayers = -1;
            do
            {
                invalid = false;
                Console.Write("Input Number of Computer Players (between 2 and 5) : ");
                try
                {
                    numberPlayers = Convert.ToInt32(Console.ReadLine());
                    if (numberPlayers > 6 || numberPlayers < 2)
                    {
                        invalid = true;
                    }
                }
                catch (Exception)
                {
                    invalid = true;
                }

            } while (invalid == true);
            int again = 0; // 0 = do not play again; 1 = play again; -1 = error;
            do
            {
                List<Player> players = CreatePlayers(numberPlayers, random);
                List<Card> deck = CreateDeck();
                DealCards(players, deck);

                Console.WriteLine("\n**** After the Deal ****");
                foreach (Player p in players)
                {
                    Console.WriteLine(p);
                    p.DiscardPairs();
                    Shuffle<Card>(p.Hand);
                }

                Console.WriteLine("\n++++ After Discarding Pairs and Shuffling each hand ++++");
                foreach (Player p in players)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine("");
                Play(players);

                do
                {
                    Console.Write("\nDo you want to play again (Y/N) ? ");
                    string response = Console.ReadLine().ToLower();
                    char ans = response[0];
                    if (ans == 'y')
                    {
                        again = 1;
                    }
                    else if (ans == 'n')
                    {
                        again = 0;
                    }
                    else
                    {
                        again = -1;
                    }

                }
                while (again == -1);
            } while (again == 1);
        }

        public static List<Player> CreatePlayers(int num, Random random)
        {
            List<Player> players = new List<Player>();
            int userIndex = random.Next(num + 1);
            for (int i = 0; i < num + 1; i++)
            {                
                if (i == userIndex)
                {
                    players.Add(new Player(true, "User"));
                }
                else
                {
                    players.Add(new Player(false, "Player" + i));
                }                
            }
            
            return players;
        }

        public static List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            string[] suites = { "D", "H", "C", "S" };
            string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
            for (int s = 0; s < suites.Length; s++)
            {
                string suite = suites[s];
                for (int v = 0; v < 13; v++)
                {
                    string value = values[v];
                    string card = suite + value;
                    deck.Add(new Card(card));
                }
            }
            deck.Add(new Card("OM"));
            return deck;
        }

        public static void DealCards(List<Player> players, List<Card> deck)
        {
            Shuffle<Card>(deck);
            while (deck.Count > 0)
            {
                int i = deck.Count - 1;
                foreach (Player p in players)
                {
                    p.Hand.Add(deck[i]);
                    deck.RemoveAt(i);
                    if (deck.Count == 0)
                    {
                        break;
                    }
                    i--;
                }
            }
        }

        public static void Play(List<Player> players)
        {
            bool gameOver = false;
            while (gameOver == false)
            {
                Console.WriteLine("<Return> to Continue : ");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                for (int i = 0; i < players.Count; i++)
                {
                    int status = 1;
                    int nextStatus = status = 1;
                    Player active = players[i];
                    Player next = players[(i + 1) % players.Count];
                    if (active.IsHuman)
                    {
                        active.HumanPlayerTurn(next);
                        status = active.CheckStatus();
                        nextStatus = next.CheckStatus();

                    }
                    else
                    {
                        active.PlayerTurn(next, random);
                        status = active.CheckStatus();
                        nextStatus = next.CheckStatus();
                    }
                    if (status == 0)
                    {
                        
                        players.Remove(active);
                        Console.WriteLine("+++ " + active.Name + " has finished +++");
                        
                    }
                    if (nextStatus == 0)
                    {

                        players.Remove(next);
                        Console.WriteLine("+++ " + next.Name + " has finished +++");

                    }
                    if (i < players.Count - 1)
                    {
                        Console.WriteLine("<Return> to Continue : ");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    }
                }
                Console.WriteLine("==== After the Pick ====");
                foreach (Player p in players)
                {
                    Console.WriteLine(p);
                }
                if (players.Count == 1)
                {
                    Console.WriteLine("\nXXXXX " + players[0].Name + " is the loser XXXXX");
                    gameOver = true;
                    break;
                }
                Console.WriteLine("\n@@@@ One Round Has Finished --- Let each player shuffle his hand @@@@");
                foreach (Player p in players)
                {
                    Shuffle<Card>(p.Hand);
                    Console.WriteLine(p);
                }
                Console.WriteLine("");
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
