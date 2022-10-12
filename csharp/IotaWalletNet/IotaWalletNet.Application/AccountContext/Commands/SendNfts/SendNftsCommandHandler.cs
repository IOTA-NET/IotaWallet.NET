using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendNfts
{
    public class SendNftsCommandHandler : IRequestHandler<SendNftsCommand, SendNftsResponse>
    {
        public async Task<SendNftsResponse> Handle(SendNftsCommand request, CancellationToken cancellationToken)
        {
            SendNftsCommandMessageData messageData = new SendNftsCommandMessageData(request.AddressesAndNftIds);
            SendNftsCommandMessage message = new SendNftsCommandMessage(request.Username, messageData);
            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            SendNftsResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<SendNftsResponse>()!
                                            : new SendNftsResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
