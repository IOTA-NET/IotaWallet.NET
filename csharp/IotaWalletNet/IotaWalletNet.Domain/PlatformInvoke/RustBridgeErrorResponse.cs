namespace IotaWalletNet.Domain.PlatformInvoke
{
    public class RustBridgeErrorResponse
    {
        public RustBridgeErrorResponse(string type, string error)
        {
            Type = type;
            Error = error;
        }

        public string Type { get; set; }
        public string Error { get; set; }
    }

}
