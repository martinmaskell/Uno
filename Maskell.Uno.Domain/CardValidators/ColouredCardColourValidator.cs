using System;
using Maskell.Uno.CardValidators.Interfaces;

namespace Maskell.Uno.CardValidators
{
	class ColouredCardColourValidator : ICardColourValidator
	{
		public CardColour Validate(CardColour cardColour)
		{
			switch (cardColour)
			{
				case CardColour.Blue:
				case CardColour.Green:
				case CardColour.Red:
				case CardColour.Yellow:
					break;
				default:
					throw new ArgumentOutOfRangeException("cardColour");
			}

			return cardColour;
		}

	}
}
