using System;
using Maskell.Uno.Cards;
using NUnit.Framework;

namespace Maskell.Uno.Tests
{
	[TestFixture]
	public class CardTests
	{
		[Test]
		[TestCase(CardColour.Blue)]
		[TestCase(CardColour.Green)]
		[TestCase(CardColour.Red)]
		[TestCase(CardColour.Yellow)]
		public void FaceCard_ValidColour_ColourIsSetCorrectly(CardColour colour)
		{
			var card = new Number(0, colour);

			Assert.AreEqual(colour, card.Colour);
		}

		[Test]
		public void FaceCard_InvalidColour_ThrowException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new Number(0, CardColour.Black));
		}
	}
}
