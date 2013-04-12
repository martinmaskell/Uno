namespace Maskell.Uno
{
	public enum CardColour
	{
		Black,
		Blue,
		Green,
		Red,
		Yellow
	}

	public enum GameState
	{
		NotStarted,
		WaitingForPlayer,
		Finished
	}

	public enum GamePlayerBoundary
	{
		MinimumPlayers = 2,
		MaximumPlayers = 10
	}

}
