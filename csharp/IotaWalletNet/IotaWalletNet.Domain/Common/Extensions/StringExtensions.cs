using System.Text;
using Blake2Fast;

namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? input) => string.IsNullOrEmpty(input);

        public static bool IsNotNullAndEmpty(this string? input) => !input.IsNullOrEmpty();

        public static string ToHexString(this string input) => "0x" + Convert.ToHexString(Encoding.UTF8.GetBytes(input));

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
