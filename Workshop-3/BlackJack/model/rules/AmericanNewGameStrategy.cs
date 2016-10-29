namespace BlackJack.model.rules
{
    class AmericanNewGameStrategy : INewGameStrategy
    {
        public bool NewGame(Dealer theDealer, Player thePlayer)
        {
            theDealer.DealCard(true, thePlayer);
            theDealer.DealCard(true, theDealer);
            theDealer.DealCard(true, thePlayer);
            theDealer.DealCard(false, theDealer);
            return true;
        }
    }
}