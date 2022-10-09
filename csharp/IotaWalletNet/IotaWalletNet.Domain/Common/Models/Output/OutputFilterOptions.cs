namespace IotaWalletNet.Domain.Common.Models.Output
{
    public class OutputFilterOptions
    {
        /// <summary>
        /// Filter all outputs where the booked milestone index is below the specified timestamp
        /// </summary>
        public uint LowerBoundBookedTimestamp { get; set; }

        /// <summary>
        /// Filter all outputs where the booked milestone index is above the specified timestamp
        /// </summary>
        public uint UpperBoundBookedTimestamp { get; set; }
    }
}
