namespace IotaWalletNet.Application.Common.Exceptions
{

    [Serializable]
    public class RustBridgeException : Exception
    {
        public RustBridgeException() { }
        public RustBridgeException(string message) : base(message) { }
        public RustBridgeException(string message, Exception inner) : base(message, inner) { }
        protected RustBridgeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
