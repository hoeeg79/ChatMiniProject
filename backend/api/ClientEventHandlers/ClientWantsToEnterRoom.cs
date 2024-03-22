using System.Text.Json;
using api.Models.ServerResponses;
using api.Validators;
using Fleck;
using Infrastructure;
using Infrastructure.DomainModels;
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
    private readonly ChatDBRepository _repo;

    public ClientWantsToEnterRoom(ChatDBRepository repo)
    {
        _repo = repo;
    }
    public override Task Handle(ClientWantsToEnterRoomDto dto, IWebSocketConnection socket)
    {
        if (ConnectionStates.AddToRoom(socket, dto.RoomId))
        {
            var oldMessages = _repo.GetMessages(dto.RoomId);
            //var oldMessages = new List<MessagesInRooms>();
            socket.Send(JsonSerializer.Serialize(new ServerAddsClientToRoom()
            {
                message = $"Welcome to room with ID = {dto.RoomId}",
                oldMessages = oldMessages.ToList()
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



