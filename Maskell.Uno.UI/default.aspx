<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Maskell.Uno.UI._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
	<script src="Scripts/jquery.signalR-1.0.0-alpha2.min.js" type="text/javascript"></script>
	<!--  If this is an MVC project then use the following -->
	<!--  <script src="~/signalr/hubs" type="text/javascript"></script> -->
	<script src="/signalr/hubs" type="text/javascript"></script>
	<script type="text/javascript">
		$(function () {
			// Proxy created on the fly          
			var chat = $.connection.chat;

			// Declare a function on the chat hub so the server can invoke it          
			chat.client.addMessage = function (message) {
				$('#messages').append('<li>' + message + '</li>');
			};

			$("#broadcast").click(function () {

				var message = $('#msg').val();
				var id = chat.connection.id;

				chat.server.send(id, message);
			});

			// Start the connection
			$.connection.hub.start();
		});
	</script>
  

</head>
<body>
    <form id="form1" runat="server">
    
	
  <div>
    <input type="text" id="msg" />
<input type="button" id="broadcast" value="broadcast" />

<ul id="messages">
</ul>
  </div>
	

    </form>
</body>
</html>
