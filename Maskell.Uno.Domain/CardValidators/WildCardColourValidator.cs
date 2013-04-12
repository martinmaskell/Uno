using System;
using Maskell.Uno.CardValidators.Interfaces;

namespace Maskell.Uno.CardValidators
{
	class WildCardColourValidator : ICardColourValidator
	{
		public CardColour Validate(CardColour cardColour)
		{
			switch (cardColour)
			{
				case CardColour.Black:
					break;
				default:
					throw new ArgumentOutOfRangeException("cardColour");
			}

			return cardColour;
		}
	}
}
