using Grpc.Core;

namespace Server.Services;

public class MaximumNumberService: MaximumNumberApi.MaximumNumberApiBase
{
    public override async Task GetMaximumNumber(IAsyncStreamReader<MaximumNumberRequest> requestStream, IServerStreamWriter<MaximumNumberResponse> responseStream, ServerCallContext context)
    {
        var maxNumber = int.MinValue;

        while (await requestStream.MoveNext())
        {
            var currentNumber = requestStream.Current.Number;
            
            if (maxNumber >= currentNumber) continue;
            
            maxNumber = currentNumber;
            await responseStream.WriteAsync(new MaximumNumberResponse()
            {
                Result = maxNumber
            });
        }
    }
}