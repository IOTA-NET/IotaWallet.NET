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

            const string HRP_CONSTANT = "1";

            return $"{hrp}{HRP_CONSTANT}{bech32}";
        }

        public static string DecodeBech32IntoEd25519Hash(this string bech32, NetworkType networkType, TypeOfCoin typeOfCoin)
        {
            bech32 = bech32.Trim().ToLower();

            string hrp = HumanReadablePart.GetHumanReadablePart(networkType, typeOfCoin);

            //string bech32OfInterest = bech32.Substring(hrp.Length+1); //eg remove iota1 or smr1 or atoi1 or rms1

            byte[] decoded = Bech32Engine.Decode(bech32, hrp);

            return "0x" + Convert.ToHexString(decoded.Skip(1).ToArray()); //skip the 1st byte "00" which represents ed25519 type
        }
    }
}
