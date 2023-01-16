using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Transaction.PayloadTypes;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Commands.SendOutputs
{
    public class SendOutputsCommand : IRequest<SendOutputsResponse>
    {
        public SendOutputsCommand(IAccount account, string username, List<IOutputType> outputs, TaggedDataPayload? taggedDataPayload=null)
        {
            Account = account;
            Username = username;
            Outputs = outputs;
            TaggedDataPayload = taggedDataPayload;
        }

        public IAccount Account { get; set; }

        public string Username { get; set; }
        
        public List<IOutputType> Outputs { get; set; }
        
        public TaggedDataPayload? TaggedDataPayload { get; set; }

    }
}
