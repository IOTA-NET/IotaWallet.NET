namespace IotaWalletNet.Domain.Common.Models.Transaction
{
    public class RequiredStorageDeposit
    {
        public RequiredStorageDeposit(string alias, string basic, string foundry, string nft)
        {
            Alias = alias;
            Basic = basic;
            Foundry = foundry;
            Nft = nft;
        }

        public string Alias { get; set; }

        public string Basic { get; set; }

        public string Foundry { get; set; }

        public string Nft { get; set; }
    }
}
