using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
    public class NumberCardAction : ICardAction
    {
        public void Process(IGame game)
        {
            game.GoToNextPlayer();
        }

    }
}
