using Infrastructure.DomainModels;
using lib;

namespace api.Models.ServerResponses;

public class ServerAddsClientToRoom : BaseDto
{
    public string message { get; set; }
    public List<MessagesInRooms>? oldMessages { get; set; }
}