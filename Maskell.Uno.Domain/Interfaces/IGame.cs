using System;
using System.Collections.Generic;

namespace Maskell.Uno.Interfaces
{
	public interface IGame
	{
		Guid GameId { get; }
		IDeck DrawPile { get; }
		IDiscardPile DiscardPile { get; }
		IEnumerable<IPlayer> Players { get; }
		GameState State { get; }

		IPlayer Join(string name);
		ICard[] GetHand(IPlayer player);

		ICard DrawCard(IPlayer player);
		void KeepDrawnCard(IPlayer player);
		void PlayCard(IPlayer player, ICard card);

		ICard GetLastDiscardedCard();
		IPlayer GetPlayerTurn();

		void Start();
		void ReverseTurnDirection();
	    void GoToNextPlayer();
	}


}
