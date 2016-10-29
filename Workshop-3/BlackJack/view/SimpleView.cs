using System;
using System.Collections.Generic;
using System.Threading;

namespace BlackJack.view
{
    class SimpleView : IView
    {

        public void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hello Black Jack World");
            Console.WriteLine("Type 'p' to Play, 'h' to Hit, 's' to Stand or 'q' to Quit\n");
        }

        public MenuValue GetInput()
        {
            //char c = System.Console.ReadKey().KeyChar;
            int c = Console.In.Read();
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

        public void DisplayCard(model.Card theCard) { Console.WriteLine("{0} of {1}", theCard.GetValue(), theCard.GetColor()); }

        public void DisplayPlayerHand(IEnumerable<model.Card> theHand, int a_score) { DisplayHand("Player", theHand, a_score); }

        public void DisplayDealerHand(IEnumerable<model.Card> theHand, int a_score) { DisplayHand("Dealer", theHand, a_score); }

        private void DisplayHand(String a_name, IEnumerable<model.Card> theHand, int a_score)
        {
            Console.WriteLine("{0} Has: ", a_name);
            foreach (model.Card c in theHand)
                DisplayCard(c);
            Console.WriteLine("Score: {0}", a_score);
            Console.WriteLine(Environment.NewLine);
        }

        public void DisplayGameOver(bool theDealerIsWinner)
        {
            Console.Write("GameOver: ");
            if (theDealerIsWinner)
                Console.WriteLine("Dealer Won!");
            else
                Console.WriteLine("You Won!");
        }

        public void PauseGame()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write("Dealing.");
            Thread.Sleep(400);
            Console.Write(".");
            Thread.Sleep(400);
            Console.Write(".");
            Thread.Sleep(400);
        }
    }
}