using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.MintNfts
{
    public class MintNftsCommandHandler : IRequestHandler<MintNftsCommand, MintNftsResponse>
    {
        public async Task<MintNftsResponse> Handle(MintNftsCommand request, CancellationToken cancellationToken)
        {
            MintNftsCommandMessageData data = new MintNftsCommandMessageData(request.NftsOptions, new TransactionOptions());

            MintNftsCommandMessage message = new MintNftsCommandMessage(request.Username, data);

            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            MintNftsResponse response = genericResponse.IsSuccess
                                            ? genericResponse.As<MintNftsResponse>()!
                                            : new MintNftsResponse() { Error = genericResponse.As<RustBridgeResponseError>(), Type = "error" };

            return response;
        }
    }
}
