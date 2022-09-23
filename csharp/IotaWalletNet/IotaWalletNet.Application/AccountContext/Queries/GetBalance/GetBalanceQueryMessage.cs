using IotaWalletNet.Application.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Queries.GetBalance
{
    public class GetBalanceQueryMessage : AccountMessage
    {
        private const string METHOD_NAME = "GetBalance";
        public GetBalanceQueryMessage(string username)
        {
            PayloadMethod payloadMethod = new PayloadMethod(METHOD_NAME);
            AccountPayload accountPayload = new AccountPayload(username, payloadMethod);
            
            Payload = accountPayload;
        }
    }
}
