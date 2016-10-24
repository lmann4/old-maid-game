using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaidGame
{
    class Card : IComparable<Card>
    {
        private string _value;

        public int Value
        {
            get
            {
                return GetValue();
            }
        }


        public Card(string card)
        {
            _value = card;
        }

        public int GetValue()
        {
            char v = this._value[1];
            int val = 0;
            if (v == 'J')
            {
                val = 11;
            }
            else if (v == 'Q')
            {
                val = 12;
            }
            else if (v == 'K')
            {
                val = 13;
            }
            else if (v == 'A')
            {
                val = 1;
            }
            else if (v == 'T')
            {
                val = 10;
            }
            else
            {
                try
                {
                    val = Convert.ToInt32(v);
                }
                catch(Exception)
                {
                }
            }
            return val;
        }

        public int CompareTo(Card c)
        {
            if (this.Value > c.Value)
            {
                return 1;
            }
            else if (this.Value < c.Value)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
