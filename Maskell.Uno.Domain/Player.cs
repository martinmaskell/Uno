using System;
using System.Linq;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public class Player : IPlayer
	{
		public event EventHandler<PlayerCardEventArgs> CardDealt;
		public event EventHandler<PlayerCardEventArgs> CardKept;
		public event EventHandler<PlayerCardEventArgs> CardPlayed;

		private ICard DrawnCard { get; set; }
		public Guid ID { get; private set; }
		public string Name { get; private set; }
		public IHand Hand { get; private set; }

		internal Player(string name)
		{
			Name = name;
			ID = Guid.NewGuid();

			Hand = new Hand();
		}

		public void DealCard(ICard card)
		{
			Hand.AddCard(card);

			if (CardDealt != null)
			{
				CardDealt(null, new PlayerCardEventArgs(this, card));
			}
		}

		public void KeepDrawnCard()
		{
			if (DrawnCard == null)
				throw new Exception("Player does not have a drawn card to keep.");

			var newCard = DrawnCard.Clone();
			DrawnCard = null;

			Hand.AddCard(newCard);

			if (CardKept != null)
			{
				CardKept(null, new PlayerCardEventArgs(this, newCard));
			}
		}

		public void PlayCard(ICard card)
		{
			Hand.RemoveCard(card);

			if (CardPlayed != null)
			{
				CardPlayed(null, new PlayerCardEventArgs(this, card));
			}
		}

		public bool HasCard(ICard card)
		{
			return HasDrawnCard(card) || HasCardInHand(card);
		}

		public void DrawCard(ICard card)
		{
			if (DrawnCard != null)
				throw new Exception("Player already has a drawn card.");

			DrawnCard = card;
		}

		private bool HasCardInHand(ICard card)
		{
			return Hand.Cards.Any(c => c.Is(card));
		}

		private bool HasDrawnCard(ICard card)
		{
			return DrawnCard != null && DrawnCard.Is(card);
		}
	}
}
