using System.Text.Json;
using api.Models.ServerResponses;
using api.Validators;
using Fleck;
using Infrastructure;
using lib;

namespace api.ClientEventHandlers;

public class ClientWantsToSignInDto : BaseDto
{
    public string Username { get; set; }
}

[ValidateDataAnnotations]
public class ClientWantsToSignIn : BaseEventHandler<ClientWantsToSignInDto>
{

    //private readonly ChatDBRepository _repo;

    public ClientWantsToSignIn()
    { }
    public override Task Handle(ClientWantsToSignInDto dto, IWebSocketConnection socket)
    {
        ConnectionStates.Connections[socket.ConnectionInfo.Id].Username = dto.Username;
        ConnectionStates.Connections[socket.ConnectionInfo.Id].IsAuthenticated = true;
        
        //var rooms = _repo.GetRooms().ToList();
        var rooms = ConnectionStates.Rooms.Keys.ToList();
        
        var message = new ServerLogInResponse()
        {
            rooms = rooms
        };

        socket.Send(JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }
}