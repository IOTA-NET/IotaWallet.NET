namespace IotaWalletNet.Domain.PlatformInvoke
{

    public class RustBridgeResponseError
    {
        public string Type { get; } = "error";
        public RustBridgeResponseErrorPayload? Payload { get; set; }
    }

}
