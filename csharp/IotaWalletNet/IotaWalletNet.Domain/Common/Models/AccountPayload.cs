namespace IotaWalletNet.Domain.Common.Models
{
    public class AccountPayload
    {
        public AccountPayload(string username, string payloadMethodName)
        {
            AccountId = username;
            Method = new PayloadMethod(payloadMethodName);
        }
        public string AccountId { get; }
        public PayloadMethod Method { get; }
    }

    public class AccountPayload<TPayloadMethodData>
    {
        public AccountPayload(string username, string payloadMethodName, TPayloadMethodData? methodData)
        {
            AccountId = username;
            Method = new PayloadMethod<TPayloadMethodData>(payloadMethodName, methodData);
        }
        public string AccountId { get; }
        public PayloadMethod<TPayloadMethodData> Method { get; }
    }
}
