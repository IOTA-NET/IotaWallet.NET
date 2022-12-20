using Newtonsoft.Json;

namespace IotaWalletNet.Domain.PlatformInvoke
{
    /// <summary>
    /// This is a response from the rust interface, after we send a message to it.
    /// </summary>
    public class RustBridgeGenericResponse
    {
        public RustBridgeGenericResponse(string response, bool isSuccess)
        {
            Response = response;
            IsSuccess = isSuccess;
        }

        public bool IsSuccess { get; }
        public string Response { get; }
        public T? As<T>() where T : RustBridgeResponseBase
        {
            if(IsSuccess)
                return JsonConvert.DeserializeObject<T>(Response);

            dynamic obj = JsonConvert.DeserializeObject(Response)!;

            var response = JsonConvert.DeserializeObject<T>(Response)!;

            response.Error = new RustBridgeErrorResponse(obj.payload.type.Value, obj.payload.error.Value);
            return response;
        }

    }
}
