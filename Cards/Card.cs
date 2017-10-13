using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    enum Suit
    {
        Clubs,
        Space,
        Diamod,
        Heart
    };
    enum Name
    {
        J = 11,
        Q,
        K,
        A
    };
    class Card
    {        

        public Card() { }
        public Card(Suit s, int r) 
        {
            suit = s;
            rank = (UInt16) r;
        }
        public Suit suit { get; }
        public UInt16 rank { get; }


        public override string ToString()
        {
            char[] SuitSymbols =  {
                                '\u2663', //club
                                '\u2660', //spade
                                '\u2666', //diamod
                                '\u2665'}; //heart
            return (Name)rank +""+ SuitSymbols[(int)suit];
        }

        public override bool Equals(object obj)
        {
            Card c = (Card)obj;
            return (c.rank == this.rank) && (c.suit == this.suit);
        }
    }
}
