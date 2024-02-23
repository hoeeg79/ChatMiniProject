using lib;

namespace api.Models.ServerResponses;

public class ServerLogInResponse : BaseDto
{
    public List<int> rooms { get; set; }
}