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
}