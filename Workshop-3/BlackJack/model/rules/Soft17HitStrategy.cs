namespace BlackJack.model.rules
{
    class Soft17HitStrategy : IHitStrategy
    {
        private const int g_hitLimit = 17;

        public bool DoHit(Player theDealer)
        {
            var hand = theDealer.GetHand();
            int score = theDealer.CalcScore();
            
            if (score == g_hitLimit)
            {
                foreach (var card in hand)
                {
                    if (card.GetValue() == Card.Value.Ace && score - 11 == 6)
                        return true;
                }
            }
            return score < g_hitLimit;
        }
    }
}