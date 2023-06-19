using Client;
using Grpc.Net.Client;

var channel = GrpcChannel.ForAddress("http://localhost:5104");

var greeterClient = new Greeter.GreeterClient(channel);
var shipmentClient = new Shipment.ShipmentClient(channel);

var result = greeterClient.SayHello(new HelloRequest()
{
    Name = "SLY Teknoloji ve Yazılım Hizmetleri Limited Şirketi"
});

var createShipmentResult = shipmentClient.Create(new CreateShipmentRequest()
{
    Id = 1,
    PickupAddress = "Kadıköy",
    DropOffAddress = "Düsseldorf"
});

Console.WriteLine($"Result -> {result?.Message ?? "N/A"}");
Console.WriteLine($"Create shipment result -> {createShipmentResult.Result}");
Console.ReadKey();