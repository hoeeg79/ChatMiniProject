using lib;
using NUnit.Framework;
using api.ClientEventHandlers;
using tests;

[TestFixture]
public class Test
{
    [SetUp]
    public void SetupThatShit()
    {
        HelperBitch.TriggerRebuild();
    }
    
    [Test]
    [Repeat(50)]
    public async Task MyTest()
    {
        //Initialize the WebSocketTestClient and connect to the server (default URL = ws://localhost:8181)
        var ws = await new WebSocketTestClient().ConnectAsync();

        await ws.DoAndAssert(new ClientWantsToSignInDto() { Username = "Dorit"});

        await ws.DoAndAssert(new ClientWantsToEnterRoomDto() { RoomId = 1 });
        
        //Send an object extending BaseDto to the server without asserting and waiting
        await ws.DoAndAssert(new ClientWantsToBroadcastToRoomDto() {Message = "hey", RoomId = 1});
   
        //Send an object extending BaseDto to the server and wait for assertions to be true. If not, exception is thrown
        await ws.DoAndAssert(new ClientWantsToBroadcastToRoomDto() {Message = "hey2", RoomId = 1}, 
            receivedMessages => receivedMessages.Count == 3);
    }
}