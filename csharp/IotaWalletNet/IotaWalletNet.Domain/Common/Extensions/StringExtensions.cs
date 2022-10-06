using System.Text;

namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? input) => string.IsNullOrEmpty(input);

        public static bool IsNotNullAndEmpty(this string? input) => !input.IsNullOrEmpty();

        public static string ToHexString(this string input) => "0x" + Convert.ToHexString(Encoding.UTF8.GetBytes(input));
    }
}
