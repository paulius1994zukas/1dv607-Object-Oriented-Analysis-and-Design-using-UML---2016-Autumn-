namespace BlackJack.model.rules
{
    class WinnerProPlayer : IWinStrategy
    {
        public int maxScore { get { return 21; } }

        public bool IsDealerWinner(Dealer theDealer, Player thePlayer)
        {
            if (thePlayer.CalcScore() > maxScore)
                return true;

            else if (theDealer.CalcScore() > maxScore)
                return false;

            return theDealer.CalcScore() > thePlayer.CalcScore();
        }
    }
}