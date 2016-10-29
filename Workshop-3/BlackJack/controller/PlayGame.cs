using BlackJack.model;
using BlackJack.view;

namespace BlackJack.controller
{
    class PlayGame : BlackJObserver
    {
        Game theGame = new Game();
        IView theView;

        public PlayGame(Game theGame, IView theView) { this.theGame = theGame; this.theView = theView; }

        public void HasNewCard() { theView.PauseGame(); RenderPlayground(); }

        public bool Play()
        {
            theGame.SubscribeToNewCard(this);
            RenderPlayground();

            if (theGame.IsGameOver())
                theView.DisplayGameOver(theGame.IsDealerWinner());

            var input = theView.GetInput();

            if (input == MenuValue.Start) { theGame.NewGame(); input = MenuValue.None; }
            else if (input == MenuValue.Hit) { theGame.Hit(); input = MenuValue.None; }
            else if (input == MenuValue.Stand) { theGame.Stand(); input = MenuValue.None; }
            return input != MenuValue.Quit;
        }

        private void RenderPlayground()
        {
            theView.DisplayWelcomeMessage();
            theView.DisplayDealerHand(theGame.GetDealerHand(), theGame.GetDealerScore());
            theView.DisplayPlayerHand(theGame.GetPlayerHand(), theGame.GetPlayerScore());
        }
    }
}