using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
    public class SkipCardAction : ICardAction
    {
        public void Process(IGame game)
        {
            game.GoToNextPlayer();
            game.GoToNextPlayer();
        }

    }
}
