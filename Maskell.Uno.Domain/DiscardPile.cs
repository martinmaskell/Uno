using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public class DiscardPile : Deck, IDiscardPile
	{
		public ICard LastDiscardedCard
		{
			get { return Cards.Count == 0 ? null : Cards.Peek(); }
		}
	}
}
