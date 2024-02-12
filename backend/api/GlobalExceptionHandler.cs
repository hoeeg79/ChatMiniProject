using System.Text.Json;
using api.Models.ServerResponses;
using Fleck;

namespace api;

public static class GlobalExceptionHandler
{
    public static void Handle(
        this Exception exception,
        IWebSocketConnection socket,
        string? message)
    {
        socket.Send(JsonSerializer.Serialize(new ServerSendsErrorMessageToClient()
        {
            receivedMessage = message,
            errorMessage = exception.Message
        }, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
                
        if (exception is JwtVerificationException)
        {
            // TODO Implement this shit
            //socket.UnAuthenticate();
            //socket.SendDto(new ServerRejectsClientJwt());
        }
    }
}

public class JwtVerificationException(string message) : Exception(message);