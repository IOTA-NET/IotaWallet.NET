namespace IotaWalletNet.Main.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string? input) => string.IsNullOrEmpty(input);

        public static bool IsNotNullAndEmpty(this string? input) => !input.IsNullOrEmpty();
    }
}
