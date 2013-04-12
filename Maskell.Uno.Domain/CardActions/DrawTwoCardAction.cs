using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardActions
{
	public class DrawTwoCardAction : ICardAction
	{
		public void Process(IGame game)
		{
			game.GoToNextPlayer();

			var player = game.GetPlayerTurn();
			
			game.DrawCard(player);
			game.KeepDrawnCard(player);
			
			game.DrawCard(player);
			game.KeepDrawnCard(player);

			game.GoToNextPlayer();
		}

	}
}
