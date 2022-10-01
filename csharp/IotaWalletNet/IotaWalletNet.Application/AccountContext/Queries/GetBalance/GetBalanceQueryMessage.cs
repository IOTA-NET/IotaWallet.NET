using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "GetBalance";
        public GetBalanceQueryMessage(string username)
            : base(username, METHOD_NAME)
        {

        }
    }
}
