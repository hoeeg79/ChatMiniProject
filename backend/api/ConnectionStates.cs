using api.Models;
using Fleck;

namespace api;

public static class ConnectionStates
{
    public static Dictionary<Guid, WsMetadata> Connections = new ();
    public static Dictionary<int,HashSet<Guid>> Rooms = new ();
    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, new WsMetadata(ws));
    }

    public static bool AddToRoom(IWebSocketConnection ws, int room)
    {
        if (!Rooms.TryGetValue(room, out var value))
        {
            value = new HashSet<Guid>();
            Rooms.Add(room, value);
        }
        return value.Add(ws.ConnectionInfo.Id);
    }

    public static void BroadcastToRoom(int room, string message)
    {
        var doesRoomExist = Rooms.TryGetValue(room, out var guids);
        if (doesRoomExist)
        {
            foreach (var guid in guids )
            {
                var doesConnectionExist = Connections.TryGetValue(guid, out var ws);
                if (doesConnectionExist)
                {
                    ws.Connection.Send(message);
                }
            }
        }
    }
}

