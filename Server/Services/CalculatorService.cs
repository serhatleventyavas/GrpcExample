using Grpc.Core;

namespace Server.Services;

public class CalculatorService: Calculator.CalculatorBase
{
    public override async Task Prime(PrimeRequest request, IServerStreamWriter<PrimeResponse> responseStream, ServerCallContext context)
    {
        var k = 2;
        var n = request.Number;

        while (n > 1) {
            if (n % k == 0)
            {
                await responseStream.WriteAsync(new PrimeResponse() { Result = k });
                n /= k;
            }
            else
            {
                k++;
            }
            await Task.Delay(500);
        }
    }
}
