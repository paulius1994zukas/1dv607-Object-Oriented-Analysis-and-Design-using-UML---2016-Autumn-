namespace BlackJack.model
{
    interface ISubject
    {
        void SubscribeToNewCard(BlackJObserver observer);
        void Notify(Card card);
    }
}