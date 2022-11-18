using Blake2Fast;
using System.Text;

namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? input) => string.IsNullOrEmpty(input);

        public static bool IsNotNullAndEmpty(this string? input) => !input.IsNullOrEmpty();

        public static string ToHexString(this string input) => "0x" + Convert.ToHexString(Encoding.UTF8.GetBytes(input));

        public static string FromHexString(this string hexString)
        {
            hexString = hexString.Substring(2); // remove the 0x of a hexstring eg 0x1337

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
    }
}
