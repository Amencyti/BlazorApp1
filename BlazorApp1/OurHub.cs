using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace BlazorApp1
{
	public class OurHub : Hub
	{
		public static Dictionary<string, int> currentUser = new Dictionary<string, int>();
		public async Task NotifyEveryoneIn()
		{
			var username = Context.User.Claims.Where(s=>s.Type == ClaimTypes.NameIdentifier).First().Value;
			if (currentUser.ContainsKey(username))
			{
				currentUser[username]++;
			}
			else
			{
				currentUser.Add(username, 1);
			}
			await Clients.All.SendAsync("Update");
		}

		public async Task NotifyEveryoneOut()
		{
			var username = Context.User.Claims.Where(s=>s.Type == ClaimTypes.NameIdentifier).First().Value;
			if (currentUser.Where(s=>s.Key == username).First().Value==1)
			{
				currentUser.Remove(username);
			}
			else
			{
				currentUser.Add(username, 1);
			}
			await Clients.All.SendAsync("Update");
		}
		public async Task SendSpecific(string userName, string Message)
		{
			await Clients.User(userName).SendAsync("ReciveMessage", userName, Message);
		}
	}
}
