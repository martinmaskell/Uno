using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace Maskell.Uno.UI
{
	public class Chat : Hub
	{
		public void Send(string connectionId, string message)
		{
			Clients.All.addMessage(string.Format("{0} : {1}", connectionId, message));
		}
	}
}
