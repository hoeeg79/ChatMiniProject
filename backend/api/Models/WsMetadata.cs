using Fleck;

namespace api.Models;

public class WsMetadata(IWebSocketConnection connection)
{
    public IWebSocketConnection Connection { get; set; } = connection;
    public string Username { get; set; }
    public bool IsAuthenticated { get; set; }
}