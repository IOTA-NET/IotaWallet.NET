using Newtonsoft.Json;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    public class RustBridgeGenericResponse
    {
        public RustBridgeGenericResponse(string response, bool isSuccess)
        {
            Response = response;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public string Response { get; }
        public T? As<T>() => JsonConvert.DeserializeObject<T>(Response);

    }
}
