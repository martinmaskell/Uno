using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardRuleValidators
{
	public class DrawTwoCardRuleValidator : BaseCardRuleValidator, ICardRuleValidator
	{
		public bool Validate(IGame game, IPlayer player, ICard card)
		{
			var lastCard = game.GetLastDiscardedCard();

            if (lastCard == null)
                return true;

			// GetLastDiscardedCard must be same type or same colour.
			return LastCardIsTheSameType(lastCard, card) || LastCardIsSameColor(lastCard, card);
		}
	}
}
