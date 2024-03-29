﻿using IotaWalletNet.Application.Common.Interfaces;
using MediatR;

namespace IotaWalletNet.Application.AccountContext.Queries.GetAddressesWithUnspentOutputs
{
    public class GetAddressesWithUnspentOutputsQuery : IRequest<GetAddressesWithUnspentOutputsResponse>
    {
        public GetAddressesWithUnspentOutputsQuery(string username, IAccount account)
        {
            Username = username;
            Account = account;
        }

        public string Username { get; set; }
        public IAccount Account { get; set; }

    }
}
