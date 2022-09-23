namespace IotaWalletNet.Application.Common.Models
{
    public class AccountPayload
    {
        public AccountPayload(string username, PayloadMethod method)
        {
            AccountId = username;
            Method = method;
        }
        public string AccountId { get; }
        public PayloadMethod Method { get; }
    }

    public class AccountPayload<TPayloadMethodData>
    {
        public AccountPayload(string username, PayloadMethod<TPayloadMethodData> method)
        {
            Method = method;
            AccountId = username;
        }
        public string AccountId { get; }
        public PayloadMethod<TPayloadMethodData> Method { get; }
    }
}
