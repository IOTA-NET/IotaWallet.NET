namespace IotaWalletNet.Domain.Common.Models.Nft
{
    public class NFTIRC27Attribute
    {
        public NFTIRC27Attribute(string traitType, string value)
        {
            TraitType = traitType;
            Value = value;
        }

        public string TraitType { get; set; }

        public string Value { get; set; }
    }
}
