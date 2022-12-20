using IotaWalletNet.Domain.Common.Models.Account;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public partial class SyncAccountCommandHandler : IRequestHandler<SyncAccountCommand, SyncAccountResponse>
    {
        public async Task<SyncAccountResponse> Handle(SyncAccountCommand request, CancellationToken cancellationToken)
        {
            SyncAccountCommandMessage message = new SyncAccountCommandMessage(request.Username, new AccountSyncOptions());
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            SyncAccountResponse response = genericResponse.As<SyncAccountResponse>()!;
            return response;
        }
    }
}
