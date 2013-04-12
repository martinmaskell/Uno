using System.Collections.Generic;
using System.Linq;
using Maskell.Uno.Interfaces;
using NUnit.Framework;

namespace Maskell.Uno.Tests
{
	[TestFixture]
	public class DeckTests
	{
		[Test]
		public void WhenACardIsAddedToADeck_TheNextCardOffTheDeckIsTheLastCardAdded()
		{
			var deck = new Deck();

			var card = new Cards.Number(1, CardColour.Blue);
			
			deck.Push(card);

			Assert.AreEqual(deck.Pop(), card);
		}

		[Test]
		public void WhenADeckHasNoCards_PoppingACardReturnsNull()
		{
			var deck = new Deck();

			Assert.IsNull(deck.Pop());
		}

		[Test]
		public void WhenADeckIsShuffled_TheCardsAreNotInExactlyTheSameOrderAsBefore()
		{
			var deck = new Deck();
			var expectedCardString = string.Empty;

			// Add Cards to Deck
			for (var i = 1; i <= 9; i++)
			{
				var card = deck.Push(new Cards.Number(i, CardColour.Blue));
				expectedCardString += string.Format("{0}{1}", card.Colour, card.Value);
			}

			// Shuffle the Deck
			deck.Shuffle();

			// Remove Cards from Deck
			var cards = new List<ICard>();
			while (deck.NumberOfCardsInDeck > 0)
			{
				cards.Add(deck.Pop());
			}

			// Build Compare Strings in reverse order because the Deck is a Stack (LIFO).  (I understand due to the way shuffling occurs that in theory the decks could be identical in some universe)
			var actualCardString = string.Empty;
			for (var i = cards.Count - 1; i >= 0; i--)
			{
				actualCardString += string.Format("{0}{1}", cards[i].Colour, cards[i].Value);
			}

			Assert.AreNotEqual(actualCardString, expectedCardString);
		}

		[Test]
		public void WhenADeckIsFlipped_TheCardsAreInExactlyTheReverseOrder()
		{
			var deck = new Deck();
			var expectedCardString = string.Empty;

			// Add Cards to Deck
			for (var i = 1; i <= 9; i++)
			{
				var card = deck.Push(new Cards.Number(i, CardColour.Blue));
				expectedCardString += string.Format("{0}{1}{2}", card.GetType(), card.Colour, card.Value);
			}

			// Flip the Deck
			deck.Flip();

			// Remove Cards from Deck
			var cards = new List<ICard>();
			while (deck.NumberOfCardsInDeck > 0)
			{
				cards.Add(deck.Pop());
			}

			// Build Compare Strings in reverse order because the Deck is a Stack (LIFO).  (I understand due to the way shuffling occurs that in theory the decks could be identical in some universe)
			var actualCardString = cards.Aggregate(string.Empty,
			                                       (current, t) =>
			                                       current + string.Format("{0}{1}{2}", t.GetType(), t.Colour, t.Value));

			Assert.AreEqual(actualCardString, expectedCardString);
		}
	
		[Test]
		public void WhenANewGameIsCreate_TheNumberOfCardsInTheMainDeckAre108()
		{
			var game = Game.New();
			
			Assert.AreEqual(108, game.DrawPile.NumberOfCardsInDeck);
		}
	}
}
