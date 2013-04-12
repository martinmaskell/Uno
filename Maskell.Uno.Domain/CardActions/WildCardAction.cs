using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
    public class WildCardAction : ICardAction
    {
        public void Process(IGame game)
        {
            game.GoToNextPlayer();
        }

    }
}
