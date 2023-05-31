using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    internal class GetAccountQueryMessage : Message<GetAccountQueryMessageData>
    {
        private const string COMMAND = "getAccount";

        public GetAccountQueryMessage(GetAccountQueryMessageData getAccountQueryMessageData)
        {
            Cmd = COMMAND;
            Payload = getAccountQueryMessageData;
        }
    }
}
