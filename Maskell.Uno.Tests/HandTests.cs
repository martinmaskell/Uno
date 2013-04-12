using Maskell.Uno.Cards;
using Maskell.Uno.Interfaces;
using NUnit.Framework;

namespace Maskell.Uno.Tests
{
	[TestFixture]
	public class HandTests
	{

		[Test]
		public void WhenCardsAreAddedToAHand_TheCalculateScoreMethodReturnsTheCorrectValue()
		{
			IHand hand = new Hand();
			hand
				.AddCard(new Number(3, CardColour.Blue))
				.AddCard(new Reverse(CardColour.Green))
				.AddCard(new Skip(CardColour.Red));

			Assert.AreEqual(43, hand.Score);
		}

	}
}
