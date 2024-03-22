using Infrastructure.DomainModels;

namespace Infrastructure;

public interface IRepo
{
    /// <summary>
    /// Adds message to room.
    /// </summary>
    /// <param name="dataModels"></param>
    /// <returns></returns>
    public MessagesInRooms InsertMessage(MessagesInRooms dataModels);

    /// <summary>
    /// Retrieves messages in a room.
    /// </summary>
    /// <param name="roomid"></param>
    /// <returns></returns>
    public IEnumerable<MessagesInRooms> GetMessages(int roomid);

    /// <summary>
    /// Gets rooms that exist at current time.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<int> GetRooms();
}