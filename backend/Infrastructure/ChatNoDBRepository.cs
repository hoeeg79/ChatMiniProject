using Infrastructure.DomainModels;

namespace Infrastructure;

public class ChatNoDBRepository : IRepo
{
    
    public MessagesInRooms InsertMessage(MessagesInRooms dataModels)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MessagesInRooms> GetMessages(int roomid)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<int> GetRooms()
    {
        throw new NotImplementedException();
    }
}