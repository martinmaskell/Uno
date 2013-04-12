using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
    public class ReverseCardAction : ICardAction
    {
        public void Process(IGame game)
        {
            game.ReverseTurnDirection();
            game.GoToNextPlayer();
        }

    }
}
