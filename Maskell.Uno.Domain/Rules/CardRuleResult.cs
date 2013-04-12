namespace Maskell.Uno.Rules
{
	public class CardRuleResult
	{
		public CardResultResultState State { get; set; }
		public string Reason { get; set; }

		public CardRuleResult(CardResultResultState state, string reason)
		{
			State = state;
			Reason = reason;
		}
	}

	public enum CardResultResultState
	{
		Success,
		Fail
	}
}
