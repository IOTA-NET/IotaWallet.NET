namespace IotaWalletNet.Application.Common.Models
{
    public abstract class Message<T>
    {
        public string Cmd { get; set; } = "";
        public T? Payload { get; set; }
    }
}
