using api.Validators;
using Fleck;
using lib;

namespace api.ClientEventHandlers;

public class ClientWantsToSignInDto : BaseDto
{
    public string Username { get; set; }
}

[ValidateDataAnnotations]
public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
{
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        ConnectionStates.Connections[socket.ConnectionInfo.Id].Username = dto.Username;
        ConnectionStates.Connections[socket.ConnectionInfo.Id].IsAuthenticated = true;
        return Task.CompletedTask;
    }
}