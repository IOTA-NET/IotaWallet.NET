using Newtonsoft.Json;

namespace IotaWalletNet.Main.Examples
{
    public static class JsonHelper
    {
        public static string PrettyJson(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json)!;
            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
}
