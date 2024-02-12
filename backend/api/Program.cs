
using System.Data;
using System.Reflection;
using api;
using Fleck;
using Infrastructure;
using lib;



var builder = WebApplication.CreateBuilder(args);
var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
builder.Services.AddSingleton<ChatRepository>();

var app = builder.Build(); 

var server = new WebSocketServer("ws://0.0.0.0:8181");
server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        Console.WriteLine("New connection!");
        ConnectionStates.AddConnection(ws);
    };
    ws.OnClose = () =>
    {
        Console.WriteLine("Connection closed!");
        ConnectionStates.Connections.Remove(ws.ConnectionInfo.Id);
    };
    ws.OnMessage = message =>
    {
        try
        {
            app.InvokeClientEventHandler(clientEventHandlers, ws, message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.InnerException);
            Console.WriteLine(ex.StackTrace);
            ex.Handle(ws, message);
        }
    };
});

Console.ReadLine();