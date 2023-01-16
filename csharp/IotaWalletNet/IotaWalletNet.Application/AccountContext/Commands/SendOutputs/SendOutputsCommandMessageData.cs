using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Transaction;

namespace IotaWalletNet.Application.AccountContext.Commands.SendOutputs
{
    public class SendOutputsCommandMessageData
    {
        public SendOutputsCommandMessageData(List<IOutputType> outputs, TransactionOptions options)
        {
            Outputs = outputs;
            Options = options;
        }

        public List<IOutputType> Outputs { get; set; }

        public TransactionOptions Options { get; set; }
    }
}
