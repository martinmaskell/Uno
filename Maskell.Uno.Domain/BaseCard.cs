using System;
using Autofac;
using Maskell.Uno.CardValidators.Interfaces;
using Maskell.Uno.Helpers;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public abstract class BaseCard : ICard
	{
		public CardColour Colour { get; private set; }
		public int Value { get; set; }

		protected BaseCard(CardColour colour, int value)
		{
			Colour = ValidateCardColour(colour);

			Value = ValidateCardValue(value);
		}

		public bool Is(ICard card)
		{
			return (GetType() == card.GetType() && Colour == card.Colour && Value == card.Value);
		}

		public bool IsSameType(ICard card)
		{
			return (GetType() == card.GetType());
		}

		private int ValidateCardValue(int value)
		{
			var cardType = GetType();

			if (cardType == typeof(Cards.Number) && (value < 0 || value > 9))
				throw new ArgumentOutOfRangeException("value", string.Format("The value '{0}' is out of range.", value));

			return value;
		}

		private CardColour ValidateCardColour(CardColour colour)
		{
			return AutofacResolver.Container.Resolve<ICardColourValidator>(new NamedParameter("cardType", GetType())).Validate(colour);
		}

		public ICard Clone()
		{
			return (ICard) MemberwiseClone();
		}
	}
}
