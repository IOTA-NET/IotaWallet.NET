using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.DestroyFoundry
{
    public class DestroyFoundryCommandMessageData
    {
        public DestroyFoundryCommandMessageData(string foundryId)
        {
            FoundryId = foundryId;
        }
        public string FoundryId { get; set; }

        public TransactionOptions Options { get; set; } = new TransactionOptions();
    }
}
