using System;
using System.Collections.Generic;
using Autofac;
using Maskell.Uno.CardActions;
using Maskell.Uno.CardRuleValidators;
using Maskell.Uno.CardValidators;
using Maskell.Uno.CardValidators.Interfaces;
using Maskell.Uno.Cards;
using Maskell.Uno.Interfaces;

namespace Maskell.Uno.Helpers
{
	public static class AutofacResolver
	{
	    public static IContainer Container { get; private set; }

	    public static void Init()
        {
            if (Container != null)
                return;

            var builder = new ContainerBuilder();
            RegisterCardColourValidators(builder);
            RegisterCardRuleValidators(builder);
            RegisterCardActions(builder);

            Container = builder.Build();
        }

	    private static void RegisterCardActions(ContainerBuilder builder)
	    {
            builder.RegisterType<DrawTwoCardAction>();
            builder.RegisterType<NumberCardAction>();
            builder.RegisterType<ReverseCardAction>();
            builder.RegisterType<SkipCardAction>();
	        builder.RegisterType<WildCardAction>();
	        builder.RegisterType<WildDrawFourCardAction>();

            builder.Register<ICardAction>((c, p) =>
            {
                var cardType = p.Named<Type>("cardType");

                if (cardType == typeof(DrawTwo))
                {
                    return c.Resolve<NumberCardAction>();
                }

                if (cardType == typeof(Number))
                {
                    return c.Resolve<NumberCardAction>();

                }

                if (cardType == typeof(Reverse))
                {
                    return c.Resolve<ReverseCardAction>();
                }

                if (cardType == typeof(Skip))
                {
                    return c.Resolve<SkipCardAction>();
                }

                if (cardType == typeof(Wild))
                {
                    return c.Resolve<WildCardAction>();
                }

                if (cardType == typeof(WildDrawFour))
                {
                    return c.Resolve<WildDrawFourCardAction>();
                }

                return null;

            }).As<ICardAction>();
	    }

	    private static void RegisterCardRuleValidators(ContainerBuilder builder)
		{
			builder.RegisterType<DrawTwoCardRuleValidator>();
			builder.RegisterType<NumberCardRuleValidator>();
			builder.RegisterType<ReverseCardRuleValidator>();
			builder.RegisterType<SkipCardRuleValidator>();
	        builder.RegisterType<WildCardRuleValidator>();
	        builder.RegisterType<WildDrawFourCardRuleValidator>();

			builder.Register<ICardRuleValidator>((c, p) =>
				{
					var cardType = p.Named<Type>("cardType");

					if (cardType == typeof(DrawTwo))
					{
						return c.Resolve<DrawTwoCardRuleValidator>();
					}

					if (cardType == typeof(Number))
					{
						return c.Resolve<NumberCardRuleValidator>();

					}

					if (cardType == typeof(Reverse))
					{
						return c.Resolve<ReverseCardRuleValidator>();
					}

					if (cardType == typeof(Skip))
					{
						return c.Resolve<SkipCardRuleValidator>();
					}

                    if (cardType == typeof(Wild))
                    {
                        return c.Resolve<WildCardRuleValidator>();
                    }

                    if (cardType == typeof(WildDrawFour))
                    {
                        return c.Resolve<WildDrawFourCardRuleValidator>();
                    }

					return null;

				}).As<ICardRuleValidator>();
		}

		private static void RegisterCardColourValidators(ContainerBuilder builder)
		{
			builder.RegisterType<ColouredCardColourValidator>();
			builder.RegisterType<WildCardColourValidator>();

			builder.Register<ICardColourValidator>((c, p) =>
				{
					var cardType = p.Named<Type>("cardType");

					if (cardType == typeof (Number) ||
					    cardType == typeof (DrawTwo) ||
					    cardType == typeof (Reverse) ||
					    cardType == typeof (Skip))
					{
						return c.Resolve<ColouredCardColourValidator>();
					}

					if (cardType == typeof (Wild) ||
					    cardType == typeof (WildDrawFour))
					{
						return c.Resolve<WildCardColourValidator>();
					}

					return null;
				}).As<ICardColourValidator>();
		}
	}
}
