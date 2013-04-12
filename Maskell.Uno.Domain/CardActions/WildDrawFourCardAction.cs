using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
    public class WildDrawFourCardAction : ICardAction
    {
        public void Process(IGame game)
        {
            game.GoToNextPlayer();
            var player = game.GetPlayerTurn();

            for (var i = 0; i < 4; i++)
            {
	            game.DrawCard(player);
                game.KeepDrawnCard(player);
            }

            game.GoToNextPlayer();


        }

    }
}
