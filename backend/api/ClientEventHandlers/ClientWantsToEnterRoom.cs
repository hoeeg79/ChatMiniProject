using System.Text.Json;
using api.Models.ServerResponses;
using api.Validators;
using Fleck;
using Infrastructure;
using lib;

namespace api.ClientEventHandlers;

public class ClientWantsToEnterRoomDto : BaseDto
{
    public int RoomId { get; set; }
}

[ValidateDataAnnotations]
[AuthenticationValidation]
public class ClientWantsToEnterRoom : BaseEventHandler<ClientWantsToEnterRoomDto>
{
    private readonly ChatRepository _repo;

    public ClientWantsToEnterRoom(ChatRepository repo)
    {
        _repo = repo;
    }
    public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        if (ConnectionStates.AddToRoom(socket, dto.RoomId))
        {
            var _oldMessages = _repo.GetMessages(dto.RoomId);
            socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
            {
                message = $"Welcome to room with ID = {dto.RoomId}",
                oldMessages = _oldMessages.ToList()
            }));
            return Task.CompletedTask;
        }
        else
        {
            socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
            {
                message = $"Failed to add you to room with ID = {dto.RoomId}",
            }));
            return Task.CompletedTask;
        }
    }
}



