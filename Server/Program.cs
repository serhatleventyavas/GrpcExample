using Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ShipmentService>();
app.MapGrpcService<CalculatorService>();
app.MapGrpcService<LongGreetService>();

app.Run();
