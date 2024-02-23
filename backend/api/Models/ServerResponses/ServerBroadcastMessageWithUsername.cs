using Infrastructure.DomainModels;
using lib;

namespace api.Models.ServerResponses;

public class ServerBroadcastMessageWithUsername: BaseDto
{
    public MessagesInRooms message { get; set; }
}