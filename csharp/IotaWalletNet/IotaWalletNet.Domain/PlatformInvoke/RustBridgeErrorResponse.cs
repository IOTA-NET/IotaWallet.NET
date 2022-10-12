namespace IotaWalletNet.Domain.PlatformInvoke
{

    public class RustBridgeErrorResponse
    {
        public string Type { get; } = "error";
        public RustBridgeResponseErrorPayload? Payload { get; set; }
    }

}
