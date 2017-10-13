using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Test
    {

        Deck cleanDeck = new Deck();
        public bool Run()
        {
            bool ret = Test1_FullDeck();
            ret &= Test2_FullShuffledDeck();
            ret &= Test3_DealTest();
            ret &= Test4_ShuffleTest();     
                    
          
            return true;
        }
        private void WriteResult(string s)
        {
            Console.WriteLine("\t"+s);
        }

        private bool Test1_FullDeck()
        {
            Deck deck = new Deck();
            bool ret = true;
            Console.WriteLine("Full Deck Test....");
            ret = ContainsFullDeck(deck);
            WriteResult(ret ? "Passed" : "Failed");
            return ret;

        }
        private bool Test2_FullShuffledDeck()
        {
            Console.WriteLine("Test full shuffled deck....");

            Deck deck = new Deck();
            deck.Shuffle();
            bool ret = ContainsFullDeck(deck);
            WriteResult(ret ? "Pass" : "Fail");
            return ret;

        }

        private bool Test3_DealTest()
        {
            bool ret;
            Console.WriteLine("Running deal test....");
            Deck deck = new Deck();

            List<Card> discard = new List<Card>();

            ret = DrawCards(deck, 1, discard);
            ret &= DrawCards(deck, 5, discard);
            ret &= DrawCards(deck, 1, discard);
            ret &= DrawCards(deck, 3, discard);
            ret &= DrawCards(deck, 52 - 7, discard);
            ret &= DrawCards(deck, 1, discard);
            ret &= DrawCards(deck, 1, discard);
            return ret;
        }

        private bool Test4_ShuffleTest()
        {
            Console.WriteLine("Shuffle test.....");
            Deck deck = new Deck();
            Deck deck2 = new Deck();

            deck.Shuffle();
            deck2.Shuffle();
            //make sure do not match
            if (deck.Equals(deck2))
            {
                WriteResult("Fail - Shuffled decks match");
                return false;
            }

            List<Card> cards = deck.GetCards();
            deck.Shuffle();
            if (CardsMatch(cards, deck.GetCards()))
            {
                WriteResult("Fail - Deck matches after shuffle");
                return false;
            }

            List<Card> discard = new List<Card>();
            discard.Add(deck.DealOneCard());
            deck.Shuffle();
            if (ContainsDiscarded(deck, discard))
            {
                WriteResult("Fail - shuffled deck contains discarded cards");
                return false;
            }
            for (int i = 0; i < 14; i++)
            {
                discard.Add(deck.DealOneCard());
            }
            deck.Shuffle();
            if (ContainsDiscarded(deck, discard))
            {
                WriteResult("Fail - deck contains discarded cards after many discard");
                return false;
            }
            deck.Shuffle();
            deck.Shuffle();
            deck.Shuffle();
            if (ContainsDiscarded(deck, discard))
            {
                WriteResult("Fail - deck contains discarded cards after many shuffle");
                return false;
            }

            for (int i = 0; i < deck.RemainingCards; i++)
            {
                discard.Add(deck.DealOneCard());
            }
            try
            {
                deck.Shuffle();
            }
            catch
            {
                WriteResult("Fail - threw exception on shuffle empty deck");
                return false;
            }
            WriteResult("Passed");
            return true;

        }

        

        private bool DrawCards(Deck deck, int numDraw, List<Card> discard)
        {
            bool ret;
            WriteResult("Testing draw " + numDraw + " card....");
            int finalSize = deck.RemainingCards!=0 ? deck.RemainingCards - numDraw : 0;
            finalSize = finalSize < 0 ? 0 : finalSize;
            bool deckEmpty = deck.RemainingCards==0?true:false;

            for (int i = 0; i < numDraw; i++)
            {
                Card c = deck.DealOneCard();
                if (deckEmpty)
                {
                    if (c != null)
                    {
                        WriteResult("Failed - card returned on empty");
                        return false;
                    }
                    if (deck.RemainingCards != 0)
                    {
                        WriteResult("Failed - remaining cards added after empty deal");
                        return false;
                    }
                    continue;
                }
                deckEmpty = deck.RemainingCards == 0 ? true : false;
                discard.Add(c);
            }

            ret = deck.RemainingCards == finalSize;
            WriteResult("Deck Size = " + deck.RemainingCards);
            if (!ret)
            {
                WriteResult("Failed - Deck Size");
                return ret;
            }
            if (ContainsDiscarded(deck, discard))
            {
                WriteResult("Failed - Deck contains discarded cards");
                return false;
            }
            WriteResult("Passed");
            return true;
        }       

        
        private bool ContainsFullDeck(Deck d)
        {
            bool ret = true;
            List<Card> cards = d.GetCards();
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
                for (int i = 2; i < 13; i++)
                {
                    Card card = new Card(s, i);
                    if (!cards.Contains(card))
                    {
                        WriteResult("Missing " + card);
                        ret = false;
                    }
                }
            return ret;
        }
        private bool CardsMatch(List<Card> cards1, List<Card> cards2)
        {
            if (cards1.Count != cards2.Count)
                return false;
            for (int i = 0; i < cards1.Count; i++)
            {
                if (!cards1[i].Equals(cards2[i]))
                    return false;
            }
            return true;
        }
        private bool ContainsDiscarded(Deck deck, List<Card> discard)
        {
            if (deck.RemainingCards == 0)
                return false;
            List<Card> cards = deck.GetCards();
            foreach (Card c in discard)
            {
                if (cards.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
