namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class TaggedDataPayload
    {

        public TaggedDataPayload(string tag, string data)
        {
            Tag = tag;
            Data = data;
        }


        /// <summary>
        /// The tag used to categorize the data
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The indexed data
        /// </summary>
        public string Data { get; set; }
    }
}
