using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaidGame
{
    class Player
    {
        private List<Card> _hand;
        private bool _isHuman;
        private string _name;

        public List<Card> Hand
        {
            get
            {
               return _hand;
            }
        }

        public bool IsHuman
        {
            get
            {
                return _isHuman;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Player(bool isHuman, string name)
        {
            _isHuman = isHuman;
            _name = name;
            _hand = new List<Card>();
        }

        public void HumanPlayerTurn(Player nextPlayer)
        {
            Console.WriteLine("********** Now, User's Turn **********");

            Console.WriteLine(this);
            Console.WriteLine(nextPlayer);
            Console.Write("Index   :");
            for (int i = 0; i < nextPlayer._hand.Count; i++)
            {
                Console.Write("  " + i);
            }
            Console.WriteLine("");
            bool invalid = false;
            int index = -1;
            do
            {
                invalid = false;
                Console.Write("Pick One Card from " + nextPlayer._name + " : ");
                try
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    if (index > nextPlayer._hand.Count - 1 || index < 0)
                    {
                        invalid = true;
                    }
                }
                catch (Exception)
                {
                    invalid = true;
                }

            } while (invalid == true);
            Card cardToSelect = nextPlayer._hand[index];
            Console.WriteLine(this._name + "    picks up " + nextPlayer._name + "'s card at [" + index + "],   Card:" + cardToSelect);
            this._hand.Add(cardToSelect);
            nextPlayer._hand.Remove(cardToSelect);
            DiscardPairs();
        }

        public void PlayerTurn(Player nextPlayer, Random r)
        {
            int i = r.Next(nextPlayer._hand.Count);
            Card cardToSelect = nextPlayer._hand[i];
            Console.WriteLine(this._name + " picks up " + nextPlayer._name + "'s card at [" + i + "],   Card:" + cardToSelect);
            this._hand.Add(cardToSelect);
            nextPlayer._hand.Remove(cardToSelect);
            DiscardPairs();
        }
        public void DiscardPairs()
        {

            int toMatch = 0;
            while (toMatch < Hand.Count)
            {
                bool removed = false;
                for (int i = toMatch + 1; i < Hand.Count; i++)
                {
                    
                    if (_hand[toMatch].Value == Hand[i].Value)
                    {
                        Hand.RemoveAt(i);
                        Hand.RemoveAt(toMatch);
                        removed = true;
                        break;
                    }
                }
                if (removed == false)
                {
                    toMatch++;
                }
                removed = false;
            }
        }

        public int CheckStatus()
        {
            if (this.Hand.Count == 0)
            {
                return 0; // out of cards
            }
            else
            {
                return 1; //continue
            }
        }

        public override string ToString()
        {
            string playerHand = "";
            if (this._isHuman)
            {
                playerHand = this._name + "    :";
            }
            else
            {
                playerHand = this._name + " :";
            }
            foreach (Card c in this._hand)
            {
                playerHand += " " + c;
            }

            return playerHand;
        }
    }
}
