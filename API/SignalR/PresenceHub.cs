using System;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    [Authorize]
    public class PresenceHub : Hub
    {
        private readonly PresenceTracker _tracker;
        public PresenceHub(PresenceTracker tracker)
        {
            _tracker = tracker;

        }
        public override async Task OnConnectedAsync()
        {
            var isOnline = await _tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);

            //These are clients that are connected to this hub
            //And what we're going to do here is send the message to all of the others.
            //So this is everybody except the connection that triggered the current invocation.

            if (isOnline)
                await Clients.Others.SendAsync("UserIsOnline", Context.User.GetUsername());

            var currentUser = await _tracker.GetOnlineUsers();

            //send this to everyone that's connected
            await Clients.Caller.SendAsync("GetOnlineUsers", currentUser);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var isOffline = await _tracker.UserConnected(Context.User.GetUsername(), Context.ConnectionId);

            if (isOffline)
                await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());

            await base.OnDisconnectedAsync(exception);
        }
    }
}