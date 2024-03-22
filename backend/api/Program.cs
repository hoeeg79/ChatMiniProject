using System.Reflection;
using api;
using Fleck;
using Infrastructure;
using lib;

public static class Startup
{

    public static void Main(string[] args)
    {
        var app = Statup(args);
        app.Run();
    }

    public static WebApplication Statup(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var clientEventHandlers = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

        builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
            dataSourceBuilder => dataSourceBuilder.EnableParameterLogging());
        builder.Services.AddSingleton<ChatDBRepository>();

        var app = builder.Build();
        builder.WebHost.UseUrls("http://*:9999");

        var port = Environment.GetEnvironmentVariable("PORT") ?? "8181";
        
        var server = new WebSocketServer("ws://0.0.0.0:" + port);
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

        return app;
    }
}