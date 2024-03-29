﻿using IotaWalletNet.Domain.Common.Models.Transaction;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;

namespace IotaWalletNet.Application.AccountContext.Commands.SendAmount
{
    public class SendAmountCommandHandler : IRequestHandler<SendAmountCommand, SendAmountResponse>
    {
        public async Task<SendAmountResponse> Handle(SendAmountCommand request, CancellationToken cancellationToken)
        {
            TransactionOptions transactionOptions = new TransactionOptions() { TaggedDataPayload = request.TaggedDataPayload };

            SendAmountCommandMessageData messageData = new SendAmountCommandMessageData(request.AddressesWithAmount, transactionOptions);

            SendAmountCommandMessage message = new SendAmountCommandMessage(request.Username, messageData);

            string json = JsonConvert.SerializeObject(message);

            RustBridgeGenericResponse genericResponse = await request.Account.SendMessageAsync(json);

            SendAmountResponse response = genericResponse.As<SendAmountResponse>()!;

            return response;
        }
    }
}
