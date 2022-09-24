namespace IotaWalletNet.Domain.Common.Models
{
    /// <summary>
    /// The message that should be serialized
    /// and be sent to the rust interface.
    /// </summary>
    /// <typeparam name="T">Type of Payload</typeparam>
    public abstract class Message<T>
    {
        public string Cmd { get; set; } = "";
        public T? Payload { get; set; }
    }
}
