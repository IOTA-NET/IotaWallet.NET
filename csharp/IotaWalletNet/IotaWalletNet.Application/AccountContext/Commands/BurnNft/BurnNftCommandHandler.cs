using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.BurnNft
{
    public class BurnNftCommandHandler : IRequestHandler<BurnNftCommand, BurnNftResponse>
    {
        public async Task<BurnNftResponse> Handle(BurnNftCommand request, CancellationToken cancellationToken)
        {
            BurnNftCommandMessageData messageData = new BurnNftCommandMessageData(request.NftId, new TransactionOptions());
            BurnNftCommandMessage message = new BurnNftCommandMessage(request.Username, messageData);

            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            BurnNftResponse response = genericResponse.IsSuccess
                                        ? genericResponse.As<BurnNftResponse>()!
                                        : new BurnNftResponse() { Error = genericResponse.As<RustBridgeErrorResponse>(), Type = "error" };

            return response;
        }
    }
}
