namespace IotaWalletNet.Domain.Common.Models
{
    public class PayloadMethod<T> : PayloadMethod
    {
        public PayloadMethod(string payloadMethodName, T data)
            : base(payloadMethodName)
        {
            Data = data;
        }
        public T Data { get; set; }
    }

    public class PayloadMethod
    {
        public PayloadMethod(string payloadMethodName)
        {
            Name = payloadMethodName;
        }

        public string Name { get; set; }
    }
}
