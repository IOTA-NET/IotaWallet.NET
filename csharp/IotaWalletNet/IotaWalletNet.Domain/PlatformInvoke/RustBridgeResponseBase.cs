using Newtonsoft.Json;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    /// <summary>
    /// Response objects have to inherit from this class if it does not contain a payload
    /// </summary>
    public abstract class RustBridgeResponseBase
    {

        public string Type { get; set; } = "ok";


        public RustBridgeErrorResponse?  Error { get; set; }

        public bool IsSuccess() => Type != "error" && Error == null;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

    /// <summary>
    /// Response objects have to inherit from this class if it contains a payload
    /// </summary>
    /// <typeparam name="TPayload">The payload</typeparam>
    public abstract class RustBridgeResponseBase<TPayload> : RustBridgeResponseBase
    {
        public TPayload? Payload { get; set; }
    }

}
