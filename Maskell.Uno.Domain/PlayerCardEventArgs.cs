using System;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno
{
	public class PlayerCardEventArgs : EventArgs
	{
		public IPlayer Player { get; private set; }
		public ICard Card { get; private set; }
		public PlayerCardEventArgs(IPlayer player, ICard card)
		{
			Player = player;
			Card = card;
		}
	}
}
