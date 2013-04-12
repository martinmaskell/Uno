using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardRuleValidators
{
	public class BaseCardRuleValidator
	{
		public bool LastCardIsSameColor(ICard lastCard, ICard card)
		{
			return lastCard.Colour == card.Colour;
		}

		public bool LastCardIsTheSameType(ICard lastCard, ICard card)
		{
			return lastCard.IsSameType(card);
		}

	}
}
