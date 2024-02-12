using lib;

namespace api.Models.ServerResponses;

public class ServerSendsErrorMessageToClient: BaseDto
{
    public string? errorMessage { get; set; }
    public string? receivedMessage { get; set; }
}