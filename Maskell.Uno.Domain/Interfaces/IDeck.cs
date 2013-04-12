using System;

namespace Maskell.Uno.Interfaces
{
	public interface IDeck
	{
		event EventHandler<PlayerCardEventArgs> CardPopped;
		event EventHandler<PlayerCardEventArgs> CardPushed;
		ICard Push(ICard card);
		ICard Push(IPlayer player, ICard card);
		ICard Pop(IPlayer player);
		void Shuffle();
		void Flip();
		int NumberOfCardsInDeck { get; }
	}
}

