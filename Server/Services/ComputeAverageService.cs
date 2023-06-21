using Grpc.Core;

namespace Server.Services;

public class ComputeAverageService: ComputeAverageApi.ComputeAverageApiBase
{
    public override async Task<ComputeAverageResponse> Compute(IAsyncStreamReader<ComputeAverageRequest> requestStream, ServerCallContext context)
    {
        var total = 0;
        var count = 0;
        while (await requestStream.MoveNext())
        {
            count += 1;
            var number = requestStream.Current.Number;
            total += number;
        }

        if (count == 0)
        {
            count = 1;
        }
        
        var average = total / count;

        return new ComputeAverageResponse
        {
            Result = average
        };
    }
}