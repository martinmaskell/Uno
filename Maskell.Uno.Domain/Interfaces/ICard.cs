namespace Maskell.Uno.Interfaces
{
	public interface ICard
	{
		CardColour Colour { get; }
		int Value { get; }

		bool Is(ICard card);
		bool IsSameType(ICard card);
		ICard Clone();
	}
}
