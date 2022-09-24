using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccount
{
    public class GetAccountQueryMessage : Message<string>
    {
        private const string COMMAND = "GetAccount";

        public GetAccountQueryMessage(string username)
        {
            Cmd = COMMAND;
            Payload = username;
        }
    }
}
