using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQueryMessage : Message<string>
    {
        public const string COMMAND = "getAccounts";

        public GetAccountsQueryMessage()
        {
            Cmd = COMMAND;
        }
    }
}
