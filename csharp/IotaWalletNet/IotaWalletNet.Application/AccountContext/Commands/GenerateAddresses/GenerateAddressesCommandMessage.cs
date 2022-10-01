﻿using IotaWalletNet.Domain.Common.Models;

namespace IotaWalletNet.Application.AccountContext.Commands.GenerateAddresses
{
    public class GenerateAddressesCommandMessage : AccountMessage<GenerateAddressesData>
    {
        private const string METHOD_NAME = "generateAddresses";
        public GenerateAddressesCommandMessage(string username, GenerateAddressesData methodData)
            : base(username, METHOD_NAME, methodData)
        {

        }
    }
}
