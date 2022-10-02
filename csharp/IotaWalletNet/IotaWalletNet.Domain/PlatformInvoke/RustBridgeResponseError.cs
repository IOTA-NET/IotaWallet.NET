namespace IotaWalletNet.Domain.PlatformInvoke
{
    public abstract class RustBridgeResponseBase<TPayload>
    {

        public string Type { get; set; } = "ok";
        public TPayload? Payload { get; set; }

        public RustBridgeResponseError? Error { get; set; }
    }

    public abstract class RustBridgeResponseBase
    {

        public string Type { get; set; } = "ok";

        public RustBridgeResponseError? Error { get; set; }

        public bool IsSuccess() => Error == null;
    }

    public class RustBridgeResponseError
    {
        public string Type { get; } = "error";
        public RustBridgeResponseErrorPayload? Payload { get; set; }
    }

    public class RustBridgeResponseErrorPayload
    {
        public string Type { get; set; }
        public string Error { get; set; }
    }
    
}
