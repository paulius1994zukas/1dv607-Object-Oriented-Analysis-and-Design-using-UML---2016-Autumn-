using System.Collections.Generic;
using System.Linq;

namespace BlackJack.model
{
    class Player : ISubject
    {
        private List<Card> theHand = new List<Card>();
        List<BlackJObserver> theObservers;
        public Player() { theObservers = new List<BlackJObserver>(); }
        public void DealCard(Card theCard)
        {
            theHand.Add(theCard);
            Notify(theCard);
        }

        public IEnumerable<Card> GetHand() { return theHand.Cast<Card>(); }

        public void ClearHand() { theHand.Clear(); }

        public void ShowHand()
        {
            foreach (Card c in GetHand())
                c.Show(true);
        }

        public int CalcScore()
        {
            int[] cardScores = new int[(int)model.Card.Value.Count] {2, 3, 4, 5, 6, 7, 8, 9, 10, 10 ,10 ,10, 11};
            int score = 0;

            foreach (Card c in GetHand())
            {
                if (c.GetValue() != Card.Value.Hidden)
                    score += cardScores[(int)c.GetValue()];
            }

            if (score > 21)
            {
                foreach (Card c in GetHand())
                {
                    if (c.GetValue() == Card.Value.Ace && score > 21)
                        score -= 10;
                }
            }
            return score;
        }

        public void SubscribeToNewCard(BlackJObserver observer)
        {
            if (!theObservers.Contains(observer))
                theObservers.Add(observer);
        }

        public void Notify(Card card) { theObservers.ForEach(x => x.HasNewCard()); }
    }
}
