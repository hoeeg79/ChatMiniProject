using System.Security.Authentication;
using Fleck;
using lib;

namespace api.Validators;

public class AuthenticationValidation : BaseEventFilter
{
    public override Task Handle<T>(IWebSocketConnection socket, T dto)
    {
        var authenticated = ConnectionStates.Connections.ContainsKey(socket.ConnectionInfo.Id)
                            && ConnectionStates.Connections[socket.ConnectionInfo.Id].IsAuthenticated;
        
        if (!authenticated)
        {
            throw new AuthenticationException("Client is not authenticated!");
        }

        return Task.CompletedTask;
    }
}