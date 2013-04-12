namespace Maskell.Uno.Interfaces
{
	public interface IDiscardPile : IDeck
	{
		ICard LastDiscardedCard { get; }
	}
}
