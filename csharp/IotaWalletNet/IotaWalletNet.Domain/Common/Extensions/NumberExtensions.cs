namespace IotaWalletNet.Domain.Common.Extensions
{
    public static class NumberExtensions
    {
        public static string ToHexEncodedAmount(this Int64 input)
        {
            return $"0x{Convert.ToHexString(BitConverter.GetBytes(input).Reverse().ToArray())}";
        }

        public static string ToHexEncodedAmount(this UInt64 input)
        {
            return $"0x{Convert.ToHexString(BitConverter.GetBytes(input).Reverse().ToArray())}";
        }
        public static string ToHexEncodedAmount(this UInt32 input)
        {
            return $"0x{Convert.ToHexString(BitConverter.GetBytes(input).Reverse().ToArray())}";
        }

        public static string ToHexEncodedAmount(this Int32 input)
        {
            return $"0x{Convert.ToHexString(BitConverter.GetBytes(input).Reverse().ToArray())}";
        }

        private static string PreprocessHexEncodedAmountString(string hexEncodedAmount)
        {
            if (hexEncodedAmount.ToLower().StartsWith("0x"))
                hexEncodedAmount = hexEncodedAmount.Substring(2);

            if(hexEncodedAmount.Length % 2 != 0)
                hexEncodedAmount = "0" + hexEncodedAmount;

            return hexEncodedAmount;
            return new string(hexEncodedAmount.AsEnumerable().Reverse().ToArray());
        }

        /// <summary>
        /// This is to be used after PreprocessHexEncodedAmountString
        /// </summary>
        private static string PadFor64Bits(string hexEncodedAmount)
        {
            const int REQUIRED_PADDINGS = 16;
            hexEncodedAmount = hexEncodedAmount.PadLeft(REQUIRED_PADDINGS, '0');
            return hexEncodedAmount;
        }
        private static string PadFor32Bits(string hexEncodedAmount)
        {
            const int REQUIRED_PADDINGS = 8;
            hexEncodedAmount = hexEncodedAmount.PadLeft(REQUIRED_PADDINGS, '0');
            return hexEncodedAmount;
        }

        public static Int64 FromHexEncodedAmountToInt64(this string hexEncodedAmount)
        {
            hexEncodedAmount = PreprocessHexEncodedAmountString(hexEncodedAmount);

            return Int64.Parse(hexEncodedAmount, System.Globalization.NumberStyles.HexNumber);
        }

        public static UInt64 FromHexEncodedAmountToUInt64(this string hexEncodedAmount)
        {
            hexEncodedAmount = PreprocessHexEncodedAmountString(hexEncodedAmount);

            return UInt64.Parse(hexEncodedAmount, System.Globalization.NumberStyles.HexNumber);
        }

        public static Int32 FromHexEncodedAmountToInt32(this string hexEncodedAmount)
        {
            hexEncodedAmount = PreprocessHexEncodedAmountString(hexEncodedAmount);


            return Int32.Parse(hexEncodedAmount, System.Globalization.NumberStyles.HexNumber);
        }

        public static UInt32 FromHexEncodedAmountToUInt32(this string hexEncodedAmount)
        {
            hexEncodedAmount = PreprocessHexEncodedAmountString(hexEncodedAmount);


            return UInt32.Parse(hexEncodedAmount, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
