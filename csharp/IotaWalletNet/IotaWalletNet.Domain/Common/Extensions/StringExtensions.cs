using Bech32;
using Blake2Fast;
using IotaWalletNet.Domain.Common.Models.Address;
using IotaWalletNet.Domain.Common.Models.Coin;
using IotaWalletNet.Domain.Common.Models.Network;
using System.Text;

namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? input) => string.IsNullOrEmpty(input);

        public static bool IsNotNullAndEmpty(this string? input) => !input.IsNullOrEmpty();

        public static string ToHexString(this string input)
            => "0x" + Convert.ToHexString(Encoding.UTF8.GetBytes(input));


        public static string FromHexString(this string hexString)
        {
            if (hexString.ToLower().StartsWith("0x"))
                hexString = hexString.Substring(2); // remove the 0x of a hexstring eg 0x1337

            // eg 0x0 becomes 0 then becomes 00, to be able to use fromhexstring, we need length of hexstring to be % 2
            if (hexString.Length % 2 != 0)
                hexString = $"0{hexString}";

            byte[] utf8StringBytes = Convert.FromHexString(hexString);

            return Encoding.UTF8.GetString(utf8StringBytes);
        }

        public static string ComputeBlake2bHash(this string hexEncoded)
        {
            if (hexEncoded.StartsWith("0x") == false)
            {
                throw new ArgumentException("Hexencoded strings must begin with 0x...");
            }

            hexEncoded = hexEncoded.Substring(2);

            byte[] decodedData = Convert.FromHexString(hexEncoded);

            byte[] hash = Blake2b.ComputeHash(32, decodedData);

            return "0x" + Convert.ToHexString(hash);
        }

        public static string EncodeEd25519HashIntoBech32(this string blake2bHashOfEd25519, NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            blake2bHashOfEd25519 = blake2bHashOfEd25519.Trim().ToLower();
            if (blake2bHashOfEd25519.StartsWith("0x"))
                blake2bHashOfEd25519 = blake2bHashOfEd25519.Substring(2); // remove the 0x of a hexstring eg 0x1337

            string hrp = HumanReadablePart.GetHumanReadablePart(networkType, typeOfCoin);
            const string ED25519_TYPE = "00";
            blake2bHashOfEd25519 = ED25519_TYPE + blake2bHashOfEd25519;

            string bech32 = Bech32Engine.Encode(hrp, Convert.FromHexString(blake2bHashOfEd25519));

            return bech32;
        }

        public static string EncodeAliasIdIntoBech32(this string aliasID, NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            aliasID = aliasID.Trim().ToLower();
            if (aliasID.StartsWith("0x"))
                aliasID = aliasID.Substring(2); // remove the 0x of a hexstring eg 0x1337

            string hrp = HumanReadablePart.GetHumanReadablePart(networkType, typeOfCoin);
            const string ALIAS_TYPE = "08";
            aliasID = ALIAS_TYPE + aliasID;

            string bech32 = Bech32Engine.Encode(hrp, Convert.FromHexString(aliasID));

            return bech32;
        }

        public static string EncodeNftIdIntoBech32(this string nftId, NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            nftId = nftId.Trim().ToLower();
            if (nftId.StartsWith("0x"))
                nftId = nftId.Substring(2); // remove the 0x of a hexstring eg 0x1337

            string hrp = HumanReadablePart.GetHumanReadablePart(networkType, typeOfCoin);
            const string NFT_TYPE = "10";
            nftId = NFT_TYPE + nftId;

            string bech32 = Bech32Engine.Encode(hrp, Convert.FromHexString(nftId));

            return bech32;
        }

        public static string DecodeBech32(this string bech32, NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            bech32 = bech32.Trim().ToLower();

            string hrp = HumanReadablePart.GetHumanReadablePart(networkType, typeOfCoin);


            byte[] decoded = Bech32Engine.Decode(bech32, hrp+"1");
            
            //Remove type bytes
            decoded = decoded.Skip(1).ToArray();

            return "0x" + Convert.ToHexString(decoded);
        }
    }
}
