using System;
using System.Collections.Generic;
using System.Linq;
using Maskell.Uno.Helpers;
using Maskell.Uno.Interfaces;
using Maskell.Uno.Rules;

namespace Maskell.Uno
{
	public class Game : IGame
	{
		#region private fields

		internal List<IPlayer> _players;
		private int _currentPlayerIndex = -1;
		private int _turnDirection = 1;

		#endregion
		
		#region public properties

		public Guid GameId { get; private set; }
		public IDeck DrawPile { get; internal set; }
		public IDiscardPile DiscardPile { get; internal set; }

		public IEnumerable<IPlayer> Players
		{
			get { return _players; }
		}

		public GameState State
		{
			get { return CalculateGameState(); }
		}
		
		#endregion

		#region private properties

		private int CurrentPlayerIndex
		{
			get { return _currentPlayerIndex; }
			set { _currentPlayerIndex = value; }
		}

		#endregion

		private Game()
		{
		}

		public static IGame New()
		{
			var game = new Game
				{
					GameId = Guid.NewGuid(),
					DrawPile = new DeckHelper().CreateDefaultDeck(),
					DiscardPile = new DiscardPile(),
					_players = new List<IPlayer>()
				};

			return game;
		}

		#region public methods

		public IPlayer Join(string name)
		{
			if (MaximumPlayerLimitReached())
				throw new Exception("The Maxmimum number of players for this game has been reached.");

			if (State == GameState.WaitingForPlayer)
				throw new Exception("This game has already started");

			if (State == GameState.Finished)
				throw new Exception("This game has finished");

			IPlayer player = new Player(name);
			_players.Add(player);

			return player;
		}

		public void Start()
		{
			if (NotEnoughPlayers())
				throw new Exception("Not enough players for this Game");

			DrawPile.Shuffle();
			DealCards();

			SetupInitialGame();
		}

		public ICard[] GetHand(IPlayer player)
		{
			return GetPlayer(player).Hand.Cards;
		}

		public ICard DrawCard(IPlayer player)
		{
			return DrawPile.Pop(GetPlayer(player));
		}

		public void KeepDrawnCard(IPlayer player)
		{
			GetPlayer(player).KeepDrawnCard();
		}

		public void PlayCard(IPlayer player, ICard card)
		{
			var matchingPlayer = GetPlayer(player);
			if (matchingPlayer != GetPlayerTurn())
				throw new Exception("It's not your turn!");

			if (!player.HasCard(card))
			{
				throw new Exception("Player does not have this Card");
			}

			var result = new CardRuleEngine(this, matchingPlayer, card).Process();
			
            if (result.State == CardResultResultState.Fail)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Cannot play card {0}. Reason: {1}", card.GetType(), result.Reason));
                return;
            }

			if (result.State == CardResultResultState.Success)
				matchingPlayer.PlayCard(card);
		}

		public ICard GetLastDiscardedCard()
		{
			return DiscardPile.LastDiscardedCard;
		}

		public IPlayer GetPlayerTurn()
		{
			return State == GameState.WaitingForPlayer ? _players[CurrentPlayerIndex] : null;
		}

	    public void ReverseTurnDirection()
		{
			_turnDirection *= -1;
		}

	    public void GoToNextPlayer()
	    {
	        CurrentPlayerIndex += _turnDirection;

	        if (CurrentPlayerIndex < 0)
	            CurrentPlayerIndex = Players.Count() - 1;

	        if (CurrentPlayerIndex > Players.Count() - 1)
	            CurrentPlayerIndex = 0;
	    }

	    #endregion

	    #region events

		private static void DrawPile_CardPopped(object sender, PlayerCardEventArgs e)
		{
			e.Player.DrawCard(e.Card);
		}

	    private static void DiscardPile_CardPushed(object sender, PlayerCardEventArgs e)
		{
			
		}

	    private void player_CardPlayed(object sender, PlayerCardEventArgs e)
		{
			// TODO: Notify Players a card was played

			DiscardPile.Push(e.Player, e.Card);
		}

	    #endregion

	    #region private methods

	    private IPlayer GetPlayer(IPlayer player)
		{
			var existingPlayer = Players.FirstOrDefault(p => p.ID == player.ID);
			if (existingPlayer == null)
				throw new Exception(string.Format("Player with ID '{0}' does not exist.", player.ID));
			return existingPlayer;
		}

	    private void DealCards()
		{
			const int maxCardsDealt = 7;

			for (var i = 0; i < maxCardsDealt; i++)
			{
				foreach (var player in Players)
				{
					player.DealCard(DrawPile.Pop(player));
				}
			}
		}

	    private GameState CalculateGameState()
		{
			// Not Started
			// 1. Zero players OR
			// 2. all players have no cards
			if (!Players.Any() || Players.All(p => !p.Hand.Cards.Any()))
				return GameState.NotStarted;

			// Finished
			// 1. One player has no cards and the other players have one or more cards
			if (Players.Any(p => p.Hand.Cards.Any()) && Players.Any(p => !p.Hand.Cards.Any()))
				return GameState.Finished;

			// Waiting For Player
			// 1. Current Player Index > -1
			if (CurrentPlayerIndex > -1)
				return GameState.WaitingForPlayer;

			throw new Exception("Unable to determine game state!");
		}

	    private bool NotEnoughPlayers()
		{
			return Players.Count() < (int)GamePlayerBoundary.MinimumPlayers;
		}

	    private bool MaximumPlayerLimitReached()
		{
			return Players.Count() == (int)GamePlayerBoundary.MaximumPlayers;
		}

	    private void SetupInitialGame()
		{
			CurrentPlayerIndex = 0;
			SetupGameEvents();

	        var card = DrawPile.Pop(null);
	        DiscardPile.Push(card);
		}

	    private void SetupGameEvents()
	    {
		    SetupDeckEvents();

		    foreach (var player in Players)
			{
				player.CardPlayed += player_CardPlayed;
			}
	    }

		internal void SetupDeckEvents()
		{
			DrawPile.CardPopped += DrawPile_CardPopped;
			DiscardPile.CardPushed += DiscardPile_CardPushed;
		}

		#endregion
	}
}
