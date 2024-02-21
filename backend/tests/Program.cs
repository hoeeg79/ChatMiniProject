using lib;
using NUnit.Framework;

[TestFixture]
public class Test
{
    [Test]
    public async Task MyTest()
    {
        //Initialize the WebSocketTestClient and connect to the server (default URL = ws://localhost:8181)
        var ws = await new WebSocketTestClient().ConnectAsync();
        
        //Send an object extending BaseDto to the server without asserting and waiting
        await ws.DoAndAssert(new ClientWantsToEchoDto() {message = "hey"});
   
        //Send an object extending BaseDto to the server and wait for assertions to be true. If not, exception is thrown
        await ws.DoAndAssert(new ClientWantsToEchoDto() { message = "hey"}, 
            receivedMessages => receivedMessages.Count == 2);
    }
}