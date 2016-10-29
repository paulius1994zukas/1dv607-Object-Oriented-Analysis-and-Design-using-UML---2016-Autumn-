using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackJack.view
{
    class SwedishView : IView
    {
        public void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hej Black Jack Världen");
            Console.WriteLine("----------------------");
            Console.WriteLine("Skriv 'p' för att Spela, 'h' för nytt kort, 's' för att stanna 'q' för att avsluta\n");
        }
        public MenuValue GetInput()
        {
            char c = Console.ReadKey().KeyChar;
            if (c == 'q')
                return MenuValue.Quit;
            if (c == 'h')
                return MenuValue.Hit;
            if (c == 'p')
                return MenuValue.Start;
            if (c == 's')
                return MenuValue.Stand;

            return MenuValue.None;
        }
        public void DisplayCard(model.Card theCard)
        {
            if (theCard.GetColor() == model.Card.Color.Hidden)
                Console.WriteLine("Dolt Kort");
            else
            {
                string[] colors = new string[(int)model.Card.Color.Count]
                    { "Hjärter", "Spader", "Ruter", "Klöver" };
                string[] values = new string[(int)model.Card.Value.Count]
                    { "två", "tre", "fyra", "fem", "sex", "sju", "åtta", "nio", "tio", "knekt", "dam", "kung", "ess" };
                Console.WriteLine("{0} {1}", colors[(int)theCard.GetColor()], values[(int)theCard.GetValue()]);
            }
        }
        public void DisplayPlayerHand(IEnumerable<model.Card> theHand, int a_score) { DisplayHand("Spelare", theHand, a_score); }

        public void DisplayDealerHand(IEnumerable<model.Card> theHand, int a_score) { DisplayHand("Croupier", theHand, a_score); }

        public void DisplayGameOver(bool theDealerIsWinner)
        {
            Console.Write("Slut: ");
            if (theDealerIsWinner)
                Console.WriteLine("Croupiern Vann!");
            else
                Console.WriteLine("Du vann!");
        }

        private void DisplayHand(string a_name, IEnumerable<model.Card> theHand, int a_score)
        {
            Console.WriteLine("{0} Har: ", a_name);
            foreach (model.Card c in theHand)
                DisplayCard(c);
            Console.WriteLine("Poäng: {0}", a_score);
            Console.WriteLine("");
        }

        public void PauseGame()
        {
            Console.WriteLine("\nDelar ut...");
            Thread.Sleep(1000);
        }
    }
}