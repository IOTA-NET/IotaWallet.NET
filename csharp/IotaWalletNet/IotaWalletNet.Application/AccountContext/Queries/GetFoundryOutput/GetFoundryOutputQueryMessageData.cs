namespace IotaWalletNet.Application.AccountContext.Queries.GetFoundryOutput
{
    public class GetFoundryOutputQueryMessageData
    {
        public GetFoundryOutputQueryMessageData(string tokenId)
        {
                TokenId = tokenId;
        }
        public string TokenId { get; set; }
    }
}
