using System.Globalization;
using System.Linq;
using Maskell.Uno.Cards;
using Maskell.Uno.Interfaces;
using NUnit.Framework;

namespace Maskell.Uno.Tests
{
	[TestFixture]
	public class GameTests
	{
		[Test]
		[ExpectedException]
		public void WhenAGameIsStarted_AndThereAreNoPlayers_AnExceptionIsThrown()
		{
			var game = Game.New();
			game.Start();
		}

		[Test]
		[ExpectedException]
		public void WhenAGameIsCreated_AndMoreThan10PlayersAreAdded_AnExceptionIsThrown()
		{
			var game = Game.New();

			for (var i = 0; i < 11; i++)
			{
				game.Join(i.ToString(CultureInfo.InvariantCulture));
			}
		}

		[Test]
		public void WhenAGameIsCreated_AndThereAreNoPlayers_TheGameStateIsNotStarted()
		{
			var game = Game.New();
			Assert.AreEqual(GameState.NotStarted, game.State);
		}

		[Test]
		public void WhenAGameIsStarted_AndHasValidPlayerAndStarted_TheGameStateIsWaitingForPlayer()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Join("Player3");
			game.Join("Player4");
			game.Start();

			Assert.AreEqual(GameState.WaitingForPlayer, game.State);
		}

		[Test]
		public void WhenAGameIsStarted_AndOnePlayerHasNoCardsAndTheOtherPlayersStillHaveCards_TheGameStateIsFinished()
		{
			var game = (Game)Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			game._players[1] = new Player("test1");

			Assert.AreEqual(GameState.Finished, game.State);
		}

		[Test]
		[ExpectedException]
		public void WhenJoiningAFinishedGame_AnExceptionIsThrown()
		{
			var game = (Game)Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			game._players[1] = new Player("test1");
			game.Join("test2");
		}

		[Test]
		[ExpectedException]
		public void WhenJoiningAGame_ThatHasAlreadyStarted_AnExceptionIsThrown()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			game.Join("Player3");
		}

		[Test]
		public void WhenAGameIsStarted_EachPlayerHasSevenCards()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			Assert.IsTrue(game.Players.All(p => p.Hand.Cards.Count() == 7));
		}

		[Test]
		public void WhenAPlayerDrawsACard_TheirHandDoesNotChange()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			var player = game.Players.First();

			var handCount = player.Hand.Cards.Count();

			game.DrawCard(player);

			Assert.AreEqual(handCount, player.Hand.Cards.Count());
		}

		[Test]
		public void WhenAPlayerKeepsACard_ItIsAddedToTheirHand()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			var player = game.Players.First();

			var handCount = player.Hand.Cards.Count();
			game.DrawCard(player);
			game.KeepDrawnCard(player);

			Assert.AreEqual(handCount+1, player.Hand.Cards.Count());
		}

		[Test]
		[ExpectedException]
		public void WhenAPlayerKeepsACard_AndTheyTryToKeepItAgain_AnExceptionIsThrown()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			var player = game.Players.First();

			game.DrawCard(player);
			game.KeepDrawnCard(player);
			game.KeepDrawnCard(player);
		}

		[Test]
		[ExpectedException]
		public void WhenAPlayerPlaysACardTheyDontHave_AnExceptionIsThrown()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			var player = game.Players.First();

			((Hand) player.Hand).Cards = new ICard[] { new Number(1, CardColour.Green), new Number(2, CardColour.Green) };

			game.PlayCard(player, new WildDrawFour());
		}

		[Test]
		public void WhenAPlayerDrawsACard_ItIsStillTheirTurn()
		{
			var game = Game.New();
			game.Join("Player1");
			game.Join("Player2");
			game.Start();

			var player = game.Players.First();
			var playerTurnGuid = player.ID.ToString();
			game.DrawCard(player);

			Assert.AreEqual(playerTurnGuid, game.GetPlayerTurn().ID.ToString());
		}

		[Test]
		public void WhenAPlayerPlaysADrawnCard_ThenAfterwardsItIsNoLongerTheirTurn()
		{
			var game = Game.New();
			var firstPlayer = game.Join("Player1");
			game.Join("Player2");
			game.Start();

		    var card = game.DrawCard(firstPlayer);
			game.PlayCard(firstPlayer, card);

		    var currentPlayer = game.GetPlayerTurn();

			Assert.AreEqual("Player2", currentPlayer.Name);
		}

		[Test]
		public void WhenThereAreTwoPlayersInAGameAndBothPlayersDrawACard_TheFirstPlayerHasTheirTurnAgain()
		{
			var game = Game.New();
			var firstPlayer = game.Join("Player1");
			var secondPlayer = game.Join("Player2");
			game.Start();

			var drawPile = new Deck();
			drawPile.Push(new Number(1, CardColour.Red));
			drawPile.Push(new Number(2, CardColour.Red));
			drawPile.Push(new Number(3, CardColour.Red));
			drawPile.Push(new Number(4, CardColour.Red));
			((Game)game).DrawPile = drawPile;

			var discardPile = new DiscardPile();
			discardPile.Push(new Number(5, CardColour.Red));
			((Game)game).DiscardPile = discardPile;

			((Game)game).SetupDeckEvents();

			game.PlayCard(firstPlayer, game.DrawCard(firstPlayer));
			game.PlayCard(secondPlayer, game.DrawCard(secondPlayer));

		    var currentPlayer = game.GetPlayerTurn();

			Assert.AreEqual(firstPlayer.ID, currentPlayer.ID);
		}
	}
}
