using Dapper;
using Infrastructure.DomainModels;
using Npgsql;

namespace Infrastructure;

public class ChatRepository
{
    private NpgsqlDataSource _dataSource;

    public ChatRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public MessagesInRooms InsertMessage(MessagesInRooms dataModels)
    {
        var sql = $@"
                    INSERT INTO main.messages (username, message, roomid) 
                    VALUES (@username, @message, @roomid)
                    RETURNING *";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<MessagesInRooms>(sql, new { dataModels.username, dataModels.message, dataModels.roomid});
        }
    }

    public IEnumerable<MessagesInRooms> GetMessages(int roomid)
    {
        var sql = @"SELECT id, username, message
                    FROM main.messages
                    WHERE roomid = @roomid;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<MessagesInRooms>(sql, new { roomid });
        }
    }

    public IEnumerable<int> GetRooms()
    {
        var sql = @"SELECT DISTINCT roomid
                    FROM main.messages;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<int>(sql);
        }
    }
}