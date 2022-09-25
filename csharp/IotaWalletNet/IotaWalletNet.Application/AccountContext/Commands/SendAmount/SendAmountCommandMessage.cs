﻿using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandMessage : AccountMessage<AddressesWithAmountAndTransactionOptions>
    {

        private const string METHOD_NAME = "SendAmount";

        public SendAmountCommandMessage(string username, AddressesWithAmountAndTransactionOptions methodData) 
            : base(username, METHOD_NAME, methodData)
        {
        }
    }
}