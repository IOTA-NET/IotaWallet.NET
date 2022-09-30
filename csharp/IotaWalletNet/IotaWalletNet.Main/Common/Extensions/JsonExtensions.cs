using Newtonsoft.Json;

namespace IotaWalletNet.Main.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string PrettyJson(this string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json)!;
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
}
