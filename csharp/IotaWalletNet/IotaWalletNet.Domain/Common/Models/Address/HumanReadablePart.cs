using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;

namespace IotaWalletNet.Domain.Common.Models.Address
{
    /// <summary>
    /// 
    ///    Bech32 for human-readable encoding
    ///     The human-readable encoding of the address is Bech32(as described in BIP-0173). A Bech32 string is at most 90 characters long and consists of:

    ///     The human-readable part(HRP), which conveys the IOTA protocol and distinguishes between Mainnet(the IOTA token) and Testnet(testing version):
    ///     iota is the human-readable part for Mainnet addresses
    ///     atoi is the human-readable part for Testnet addresses
    ///     The separator, which is always 1.
    ///     The data part, which consists of the Base32 encoded serialized address and the 6-character checksum.
    /// </summary>
    public static class HumanReadablePart
    {
        public enum HRP
        {
            iota,
            atoi,
            smr,
            rms
        }

        private static IReadOnlyDictionary<TypeOfCoin, string> MainnetToHrp = new Dictionary<TypeOfCoin, string>()
        {
            { TypeOfCoin.Iota, HRP.iota.ToString() },
            { TypeOfCoin.Shimmer, HRP.smr.ToString() },

        };
        private static IReadOnlyDictionary<TypeOfCoin, string> TestnetToHrp = new Dictionary<TypeOfCoin, string>()
        {
            { TypeOfCoin.Iota, HRP.atoi.ToString() },
            { TypeOfCoin.Shimmer, HRP.rms.ToString() },

        };

        public static string GetHumanReadablePart(NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            if (networkType == NetworkType.Mainnet)
                return MainnetToHrp[typeOfCoin];
            else
                return TestnetToHrp[typeOfCoin];
        }

        public static (NetworkType, TypeOfCoin) DetermineNetworkAndTypeOfCoin(string bech32)
        {
            bech32 = bech32.ToLower();

            if (bech32.StartsWith(MainnetToHrp[TypeOfCoin.Iota]))
                return (NetworkType.Mainnet, TypeOfCoin.Iota);
            else if (bech32.StartsWith(MainnetToHrp[TypeOfCoin.Shimmer]))
                return (NetworkType.Mainnet, TypeOfCoin.Shimmer);
            else if (bech32.StartsWith(TestnetToHrp[TypeOfCoin.Iota]))
                return (NetworkType.Testnet, TypeOfCoin.Iota);
            else if (bech32.StartsWith(TestnetToHrp[TypeOfCoin.Shimmer]))
                return (NetworkType.Testnet, TypeOfCoin.Shimmer);
            else
                throw new ArgumentException("Unknown type of bech32 address supplied");
        }
    }
}
