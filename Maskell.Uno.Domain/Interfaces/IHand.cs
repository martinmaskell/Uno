namespace Maskell.Uno.Interfaces
{
	public interface IHand
	{
		ICard[] Cards { get; }

		int Score { get; }

		IHand AddCard(ICard card);
		IHand RemoveCard(int index);
		IHand RemoveCard(ICard card);
	}
}
