namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck theDeck = null;

        private rules.INewGameStrategy theNewGameRule;
        private rules.IHitStrategy theHitRule;
        private rules.IWinStrategy theWinnerRule;


        public Dealer(rules.RulesFactory theRulesFactory)
        {
            theNewGameRule = theRulesFactory.GetNewGameRule();
            theHitRule = theRulesFactory.GetHitRule();
            theWinnerRule = theRulesFactory.GetWinnerRule();
        }

        public bool NewGame(Player thePlayer)
        {
            if (theDeck == null || IsGameOver())
            {
                theDeck = new Deck();
                ClearHand();
                thePlayer.ClearHand();
                return theNewGameRule.NewGame(this, thePlayer);
            }
            return false;
        }

        public bool Hit(Player thePlayer)
        {
            if (theDeck != null && thePlayer.CalcScore() < theWinnerRule.maxScore && !IsGameOver())
            {
                DealCard(true, thePlayer);
                return true;
            }
            return false;
        }

        public bool Stand()
        {
            if (theDeck != null)
            {
                ShowHand();
                while (theHitRule.DoHit(this))
                    DealCard(true, this);
            }
            return true;
        }
        public bool IsDealerWinner(Player thePlayer) { return theWinnerRule.IsDealerWinner(this, thePlayer); }

        public bool IsGameOver()
        {
            if (theDeck != null && theHitRule.DoHit(this) != true)
                return true;
            return false;
        }

        public void DealCard(bool show, Player thePlayer)
        {
            Card c;
            c = theDeck.GetCard();
            c.Show(show);
            thePlayer.DealCard(c);
        }
    }
}