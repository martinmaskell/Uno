using System.Linq;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardRuleValidators
{
	public class WildDrawFourCardRuleValidator : BaseCardRuleValidator, ICardRuleValidator
	{
		public bool Validate(IGame game, IPlayer player, ICard card)
		{
		    var lastCard = game.GetLastDiscardedCard();
            if (lastCard == null)
                return false; // can't lay this card if this is the first card.

			// Can't have a card in the player's hand that is the same colour as GetLastDiscardedCard.
			return player.Hand.Cards.All(c => c.Colour != game.GetLastDiscardedCard().Colour);
		}
	}
}
