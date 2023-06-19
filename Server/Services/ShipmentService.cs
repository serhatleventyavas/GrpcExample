using Grpc.Core;

namespace Server.Services;

public class ShipmentService: Shipment.ShipmentBase
{
    public override Task<CreateShipmentResponse> Create(CreateShipmentRequest request, ServerCallContext context)
    {
        return Task.FromResult(new CreateShipmentResponse()
        {
            Result = true
        });
    }

    public override async Task CheckExistsShipments(CheckExistsShipmentRequest request, IServerStreamWriter<CheckExistsShipmentResponse> responseStream, ServerCallContext context)
    {
        var count = 0;

        while(count < 10)
        {
            count++;
            await Task.Delay(100);
            await responseStream.WriteAsync(new CheckExistsShipmentResponse() { IsExists = false });
        }
        await responseStream.WriteAsync(new CheckExistsShipmentResponse() { IsExists = true });
    }
}