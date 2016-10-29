namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            model.Game game = new model.Game();
            var swedishView = false;
            view.IView view;
            if (swedishView)
                view = new view.SwedishView();
            else
                view = new view.SimpleView();
            controller.PlayGame ctrl = new controller.PlayGame(game, view);

            while (ctrl.Play());
        }
    }
}