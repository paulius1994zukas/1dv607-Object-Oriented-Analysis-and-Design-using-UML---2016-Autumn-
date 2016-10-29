using BlackJack.model;
using System.Collections.Generic;
namespace BlackJack.view
{
    interface IView
    {
        MenuValue GetInput();
        void DisplayWelcomeMessage();
        void DisplayCard(Card theCard);
        void DisplayPlayerHand(IEnumerable<Card> theHand, int a_score);
        void DisplayDealerHand(IEnumerable<Card> theHand, int a_score);
        void DisplayGameOver(bool theDealerIsWinner);
        void PauseGame();
    }
}