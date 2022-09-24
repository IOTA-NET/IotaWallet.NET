﻿namespace IotaWalletNet.Application.Common.Models.Transaction.Strategy
{
    public class CustomAddressStrategy : RemainderValueStrategy
    {
        public CustomAddressStrategy(string value) : base("CustomAddress", value)
        {

        }
    }
}
