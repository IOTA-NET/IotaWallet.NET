using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Output;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetOutputs
{
    public class GetOutputsQuery : IRequest<GetOutputsResponse>
    {
        public string Username { get; set; }

        public OutputFilterOptions? FilterOptions { get; set; }

        public IAccount Account{ get; set; }
        public GetOutputsQuery(string username, IAccount account, OutputFilterOptions? outputFilterOptions=null)
        {
            Username = username;
            Account = account;
            FilterOptions = outputFilterOptions;
        }
    }
}
