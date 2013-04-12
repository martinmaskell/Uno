using System;
using System.Collections.Generic;
using System.Linq;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public class Deck : IDeck
	{
		protected Stack<ICard> Cards;

		public event EventHandler<PlayerCardEventArgs> CardPopped;
		public event EventHandler<PlayerCardEventArgs> CardPushed;

		public int NumberOfCardsInDeck
		{
			get { return Cards.Count; }
		}

		public Deck()
		{
			Cards = new Stack<ICard>();
		}

		public ICard Push(ICard card)
		{
			return Push(null, card);
		}

		public ICard Push(IPlayer player, ICard card)
		{
			Cards.Push(card);

			if (player != null && card != null && CardPushed != null)
			{
				CardPushed(this, new PlayerCardEventArgs(player, card));
			}

			return card;
		}

		public ICard Pop(IPlayer player = null)
		{
			var card = Cards.Count == 0 ? null : Cards.Pop();

			if (player != null && card != null && CardPopped != null)
			{
				CardPopped(this, new PlayerCardEventArgs(player, card));
			}

			return card;
		}

		public void Shuffle()
		{
			var cards = Cards.ToArray().Select(c => c).ToList();
			var shuffledCards = new Stack<ICard>();

			var r = new Random();

			while (cards.Count > 0)
			{
				var index = r.Next(0, cards.Count);

				shuffledCards.Push(cards[index]);
				cards.RemoveAt(index);
			}

			Cards = shuffledCards;
		}

		public void Flip()
		{
			var flippedCards = new Stack<ICard>();

			while (Cards.Count > 0)
			{
				flippedCards.Push(Cards.Pop());
			}

			Cards = flippedCards;
		}

	}
}
