using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Maskell.Uno.Helpers;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno.Rules
{
	public class CardRuleEngine
	{
		internal IGame Game { get; set; }
		internal IPlayer Player { get; set; }
		internal ICard Card { get; set; }

		public CardRuleEngine(IGame game, IPlayer player, ICard card)
		{
			Game = game;
			Player = player;
			Card = card;
		}

		public CardRuleResult Process()
		{
			// Apply Validation Rules for the Current Card
			var result = AutofacResolver.Container.Resolve<ICardRuleValidator>(new NamedParameter("cardType", Card.GetType())).Validate(Game, Player, Card);

			if (!result)
			{
				return new CardRuleResult(CardResultResultState.Fail, "Card Validation Failed");
			}

            // Apply Card Actions for the Current Card
            AutofacResolver.Container.Resolve<ICardAction>(new NamedParameter("cardType", Card.GetType())).Process(Game);

            return new CardRuleResult(CardResultResultState.Success, null);
		}


	}
}
