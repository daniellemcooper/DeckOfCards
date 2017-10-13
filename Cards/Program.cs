using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Deck deck = new Deck();
            Console.WriteLine("Cards in Deck:");
            Console.Write(deck.ToString());
            Console.WriteLine("\nPress any key to shuffle deck. \n");
            System.Console.ReadKey();
            Console.WriteLine( "Shuffled deck:");
            deck.Shuffle();
            Console.Write(deck.ToString());

            Console.WriteLine("Pres any key to run test.");
            Console.ReadKey();
                       
            Test t = new Test();
            bool ret = t.Run();

            if (ret)
                Console.WriteLine("All tests passed");
            else
                Console.WriteLine("Tests did not pass.");
            System.Console.ReadLine();

        }
    }
}
