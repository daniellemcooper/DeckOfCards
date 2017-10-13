using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Deck
    {
        public const int FULL_DECK_SIZE = 52;

        /// <summary>
        /// Array of Card representing a full deck.
        /// </summary>
        private Card[] cards = new Card[52];

        private static Random random = new Random();


        /// <summary>
        /// Cards left in the deck.
        /// </summary>
        private int numCards = 0;

        /// <summary>
        /// Read-only property returning the number of cards left to be dealt in the deck;
        /// </summary>
        public int RemainingCards { get { return numCards; } }
        
        /// <summary>
        /// Initializes a deck of poker-style cards.
        /// </summary>
        public Deck()
        {
            for (int i = 0; i < FULL_DECK_SIZE; i++)
            {
                cards[i] = new Card((Suit)(i / 13), (i % 13) + 2);
                numCards++;
            }
        }

        /// <summary>
        /// Returns the list of ordered cards remaining in the deck. The order the cards are returned represents
        /// the order in which the cards will be returned upon sequential calls to 'GetOneCard'.
        /// </summary>
        /// <returns></returns>
        public List<Card> GetCards()
        {
            return new List<Card>(cards.Skip(FULL_DECK_SIZE - RemainingCards));
        }

        /// <summary>
        /// Randomizes the order in which the cards are dealt.
        /// </summary>
        public void Shuffle()
        {
            Card temp = null;
            int location = 0;
            for (int i = nextCardIndex; i < FULL_DECK_SIZE; i++)
            {
                temp = cards[i];
                location = random.Next(nextCardIndex, FULL_DECK_SIZE);
                cards[i] = cards[location];
                cards[location] = temp;
            }
        }

        /// <summary>
        /// Index of the next card to be returned from the deck.
        /// </summary>
        private int nextCardIndex
         {
             get{ return FULL_DECK_SIZE - numCards; }
         }

        /// <summary>
        /// Function that deals the next card from the deck. Decrements the number of cards remaining in the deck by 1.
        /// </summary>
        /// <returns>A 'Card' object representing the next card in the deck. Returns null when no cards remaining in the deck.</returns>
        public Card DealOneCard()
        {
            try
            {
                if (numCards <= 0 )
                    return null;
                return cards[FULL_DECK_SIZE - numCards--];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Returns a string represting the cards remaining in the deck.
        /// </summary>
        /// <returns>A string of the cards remaining in the deck.</returns>
        public override string ToString()
        {
            string ret = "";
            
            for (int i = 0; i < numCards; i++ )
            {
                if (i % 4 == 0)
                    ret += "\n";
                ret += cards[i].ToString() + "\t";
                                          
            }
            return ret + "\n";

        }

        /// <summary>
        /// Compares 2 deck objects.
        /// </summary>
        /// <param name="obj">
        /// Deck object to be compared to this instance of a deck.
        /// </param>
        /// <returns>True if the cards in both decks are the same. False otherwise.</returns>
        public override bool Equals(object obj)
        {
            Deck deckComp = (Deck)obj;
            if (this.RemainingCards != deckComp.RemainingCards)
                return false;
            List<Card> cardsComp = deckComp.GetCards();
            for(int i=0; i < numCards; i++)
            {
                if (!this.cards[i].Equals(cardsComp[i]))
                    return false;
            }
            return true;
            
        }
    }
}
