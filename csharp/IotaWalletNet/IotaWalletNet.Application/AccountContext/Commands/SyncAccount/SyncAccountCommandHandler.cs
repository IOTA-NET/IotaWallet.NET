using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SyncAccount
{
    public class SyncAccountCommandHandler : IRequestHandler<SyncAccountCommand, string>
    {
        public async Task<string> Handle(SyncAccountCommand request, CancellationToken cancellationToken)
        {
            SyncAccountCommandMessage message = new SyncAccountCommandMessage(request.Username, new AccountSyncOptions());
            string json = JsonConvert.SerializeObject(message);
            RustBridgeGenericResponse response = await request.Account.SendMessageAsync(json);

            return "";
        }
    }
}
