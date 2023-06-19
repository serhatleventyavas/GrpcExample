using Grpc.Core;
using Grpc.Net.Client;

namespace Client;

public class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5104");

        var greeterClient = new Greeter.GreeterClient(channel);
        var shipmentClient = new Shipment.ShipmentClient(channel);
        var calculatorClient = new Calculator.CalculatorClient(channel);

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

        var isExistsShipmentIdResponse = shipmentClient.CheckExistsShipments(new CheckExistsShipmentRequest() { Id = 1 });
        while (await isExistsShipmentIdResponse.ResponseStream.MoveNext())
        {
            Console.WriteLine($"Shipment is exists -> {isExistsShipmentIdResponse.ResponseStream.Current.IsExists}");

        }


        var primeValueResponse = calculatorClient.Prime(new PrimeRequest() { Number = 120 });
        while (await primeValueResponse.ResponseStream.MoveNext())
        {
            Console.WriteLine($"Prime result is-> {primeValueResponse.ResponseStream.Current.Result}");

        }
        Console.ReadKey();
    }
}