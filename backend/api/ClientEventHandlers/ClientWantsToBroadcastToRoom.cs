using System.Text.Json;
using api.Models.ServerResponses;
using api.Validators;
using Fleck;
using Infrastructure;
using Infrastructure.DomainModels;
using lib;

namespace api.ClientEventHandlers;

public class ClientWantsToBroadcastToRoomDto : BaseDto
{
    public string Message { get; set; }
    public int RoomId { get; set; }
}

[ValidateDataAnnotations]
[AuthenticationValidation]
public class ClientWantsToBroadcastToRoom : BaseEventHandler<ClientWantsToBroadcastToRoomDto>
{
    private readonly ChatRepository _repo;

    public ClientWantsToBroadcastToRoom(ChatRepository repo)
    {
        _repo = repo;
    }
    public override Task Handle(ClientWantsToBroadcastToRoomDto dto, IWebSocketConnection socket)
    {
        var _username = ConnectionStates.Connections[socket.ConnectionInfo.Id].Username;
        var newMessage = _repo.InsertMessage(new MessagesInRooms()
        {
            message = dto.Message,
            username = _username,
            roomid = dto.RoomId
        });
        var message = new ServerBroadcastMessageWithUsername()
        {
            message = newMessage
        };
        ConnectionStates.BroadcastToRoom(dto.RoomId, JsonSerializer.Serialize(message));
        return Task.CompletedTask;
    }
}