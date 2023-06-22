using Grpc.Core;
using Grpc.Net.Client;

namespace Client;

public class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5104");
        /*
        var greeterClient = new Greeter.GreeterClient(channel);
        var shipmentClient = new Shipment.ShipmentClient(channel);
        var calculatorClient = new Calculator.CalculatorClient(channel);
        var longGreetClient = new LongGreetService.LongGreetServiceClient(channel);
        var computeAverageClient = new ComputeAverageApi.ComputeAverageApiClient(channel);
        */
        var maximumNumberClient = new MaximumNumberApi.MaximumNumberApiClient(channel);

        /*
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


        var longGreetRequest = new LongGreetRequest()
        {
            Greet = new LongGreet()
            {
                FirstName = "Serhat Levent",
                LastName = "Yavaş"
            }
        };

        var longGreetClientStream = longGreetClient.LongGreet();
        foreach (var index in Enumerable.Range(1, 100))
        {
            await longGreetClientStream.RequestStream.WriteAsync(longGreetRequest);
        }

        await longGreetClientStream.RequestStream.CompleteAsync();
        var response = await longGreetClientStream.ResponseAsync;
        Console.WriteLine($"Long Greet reponse is -> {response.Result}");


        var computeAverageStream = computeAverageClient.Compute();

        var numbers = new List<int>
        {
            1,
            2,
            3,
            4,
            5
        };

        foreach (var number in numbers)
        {
            await computeAverageStream.RequestStream.WriteAsync(new ComputeAverageRequest
            {
                Number = number
            });
            await Task.Delay(500);
        }

        await computeAverageStream.RequestStream.CompleteAsync();
        var computeAverageResponse = await computeAverageStream.ResponseAsync;
        Console.WriteLine($"Average -> {computeAverageResponse.Result}");
        */

        var maximumNumberStream = maximumNumberClient.GetMaximumNumber();

        var maximumNumberResponseTask = Task.Run(async () =>
        {
            while (await maximumNumberStream.ResponseStream.MoveNext())
            {
                var maxNumber = maximumNumberStream.ResponseStream.Current.Result;
                Console.WriteLine($"Maximum Number is: {maxNumber}");
            }
        });

        var numbers = new List<int>()
        {
            1, 5, 3, 6, 2, 20
        };

        foreach (var number in numbers)
        {
            Console.WriteLine($"Sending number: {number}");
            await maximumNumberStream.RequestStream.WriteAsync(new MaximumNumberRequest()
            {
                Number = number
            });
            await Task.Delay(500);
        }

        await maximumNumberStream.RequestStream.CompleteAsync();
        await maximumNumberResponseTask;
        Console.ReadKey();
    }
}