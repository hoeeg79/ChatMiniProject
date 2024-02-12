using lib;

namespace api.Models.ServerResponses;

public class ServerBroadcastMessageWithUsername: BaseDto
{
    public string Message { get; set; }
    public string Username { get; set; }
}