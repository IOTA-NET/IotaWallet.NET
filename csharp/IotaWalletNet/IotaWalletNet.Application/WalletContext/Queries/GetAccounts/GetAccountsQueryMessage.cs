using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.WalletContext.Queries.GetAccounts
{
    public class GetAccountsQueryMessage : Message<string>
    {
        public const string COMMAND = "GetAccounts";

        public GetAccountsQueryMessage()
        {
            Cmd = COMMAND;
        }
    }
}
