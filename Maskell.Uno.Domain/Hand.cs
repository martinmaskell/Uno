using System.Collections.Generic;
using System.Linq;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public class Hand : IHand
	{
		private List<ICard> _cards;

		public ICard[] Cards { 
			get { return _cards.ToArray(); }
			internal set { _cards = value.ToList(); }
		}

		public int Score
		{
			get { return CalculateScore(); }
		}

		public Hand()
		{
			_cards = new List<ICard>();
		}

		public IHand AddCard(ICard card)
		{
			_cards.Add(card);
			return this;
		}

		public IHand RemoveCard(int index)
		{
			_cards.RemoveAt(index);
			return this;
		}

		public IHand RemoveCard(ICard card)
		{
			_cards.Remove(card);
			return this;
		}

		public int CalculateScore()
		{
			return _cards.Sum(c => c.Value);
		}
	}
}
