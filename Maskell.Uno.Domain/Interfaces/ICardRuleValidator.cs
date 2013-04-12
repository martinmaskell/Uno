namespace Maskell.Uno.Interfaces
{
	interface ICardRuleValidator
	{
		bool Validate(IGame game, IPlayer player, ICard card);
	}
}
