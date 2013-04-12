using Maskell.Uno.Interfaces;

namespace Maskell.Uno.CardRuleValidators
{
	public class WildCardRuleValidator : BaseCardRuleValidator, ICardRuleValidator
	{
		public bool Validate(IGame game, IPlayer player, ICard card)
		{
			return true;
		}
	}
}
