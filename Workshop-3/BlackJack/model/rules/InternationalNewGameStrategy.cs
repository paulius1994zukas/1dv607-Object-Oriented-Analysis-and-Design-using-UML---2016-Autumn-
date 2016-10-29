namespace BlackJack.model.rules
{
    class InternationalNewGameStrategy : INewGameStrategy
    {
        public bool NewGame(Dealer theDealer, Player thePlayer)
        {
            var useSoft17Strategy = false;
            if (useSoft17Strategy)
            {
                Card card1 = new Card(Card.Color.Clubs, Card.Value.Three);
                Card card2 = new Card(Card.Color.Hearts, Card.Value.Three);
                Card card3 = new Card(Card.Color.Hearts, Card.Value.Ace);
                card1.Show(true);
                card2.Show(true);
                card3.Show(true);
                theDealer.DealCard(card1);
                theDealer.DealCard(card2);
                theDealer.DealCard(card3);
            }

            else
            {
                theDealer.DealCard(true, thePlayer);
                theDealer.DealCard(true, theDealer);
                theDealer.DealCard(true, thePlayer);
            }
            return true;
        }
    }
}