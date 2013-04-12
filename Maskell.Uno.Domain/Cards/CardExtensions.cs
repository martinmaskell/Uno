using Maskell.Uno.Interfaces;

namespace Maskell.Uno.Cards
{
	public static class CardExtensions
	{
		public static bool Compare(this ICard currentCard, ICard card)
		{
			return (currentCard.GetType() == card.GetType() && currentCard.Colour == card.Colour && currentCard.Value == card.Value);
		}

	}
}
