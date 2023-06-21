using Grpc.Core;

namespace Server.Services;

public sealed class LongGreetService: Server.LongGreetService.LongGreetServiceBase
{
    public override async Task<LongGreetResponse> LongGreet(IAsyncStreamReader<LongGreetRequest> requestStream, ServerCallContext context)
    {
        var result = "";

        while (await requestStream.MoveNext())
        {
            result += string.Format("Hello {0} {1} {2}",
            requestStream.Current.Greet.FirstName,
                requestStream.Current.Greet.LastName,
                    Environment.NewLine
            );
        }

        return new LongGreetResponse
        {
            Result = result
        };
    }
}