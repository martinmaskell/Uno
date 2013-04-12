using System;

namespace Maskell.Uno.Interfaces
{
	public interface IPlayer
	{
		event EventHandler<PlayerCardEventArgs> CardDealt;
		event EventHandler<PlayerCardEventArgs> CardKept;
		event EventHandler<PlayerCardEventArgs> CardPlayed;

		Guid ID { get; }
		string Name { get; }

		IHand Hand { get; }
		void DealCard(ICard card);
		void KeepDrawnCard();
		void PlayCard(ICard card);
		bool HasCard(ICard card);
		void DrawCard(ICard card);
	}
}
