using System;
using System.Diagnostics;
using Maskell.Uno.Cards;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno.Helpers
{
	public class DeckHelper
	{
		private static readonly CardColour[] CardColours = new[] {CardColour.Blue, CardColour.Green, CardColour.Yellow, CardColour.Red};

		public Deck CreateDefaultDeck()
		{
			var deck = new Deck();

			AddNumberedCards(deck);
			AddDrawTwoCards(deck);
			AddReverseCards(deck);
			AddSkipCards(deck);
			AddWildCards(deck);

			return deck;
		}

		private static void AddSkipCards(IDeck deck)
		{
			var deckCount = deck.NumberOfCardsInDeck;

			foreach (var cardColour in CardColours)
			{
				deck.Push(new Skip(cardColour));
				deck.Push(new Skip(cardColour));
			}

			Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, "Skip");
		}

		private static void AddReverseCards(IDeck deck)
		{
			var deckCount = deck.NumberOfCardsInDeck;

			foreach (var cardColour in CardColours)
			{
				deck.Push(new Reverse(cardColour));
				deck.Push(new Reverse(cardColour));
			}

			Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, "Reverse");
		}

		private static void AddDrawTwoCards(IDeck deck)
		{
			var deckCount = deck.NumberOfCardsInDeck;
			
			foreach (var cardColour in CardColours)
			{
				deck.Push(new DrawTwo(cardColour));
				deck.Push(new DrawTwo(cardColour));
			}

			Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, "DrawTwo");
		}
		
		private static void AddWildCards(IDeck deck)
		{
			var deckCount = deck.NumberOfCardsInDeck;

			for (var i = 0; i < CardColours.Length; i++)
			{
				deck.Push(new WildDrawFour());
			}

			Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, "WildDrawFour");

			deckCount = deck.NumberOfCardsInDeck;

			for (var i = 0; i < CardColours.Length; i++)
			{
				deck.Push(new Wild());
			}

			Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, "Wild");
		}

		private static void AddNumberedCards(IDeck deck)
		{
			if (deck == null)
				throw new ArgumentNullException("deck");

			AddNumberedCards(deck, CardColour.Blue);
			AddNumberedCards(deck, CardColour.Green);
			AddNumberedCards(deck, CardColour.Red);
			AddNumberedCards(deck, CardColour.Yellow);
		}

		private static void AddNumberedCards(IDeck deck, CardColour cardColour)
		{
			var deckCount = deck.NumberOfCardsInDeck;

			// Add Numbers 1 to 9 (twice)
			for (var i = 1; i <= 9; i++)
			{
				deck.Push(new Number(i, cardColour));
				deck.Push(new Number(i, cardColour));
			}

			// Add One Zero card for this colour
			deck.Push(new Number(0, cardColour));

			//Debug.WriteLine(deck.NumberOfCardsInDeck - deckCount, string.Format("{0} Cards", cardColour));
		}
	}
}

