using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes;
using IotaWalletNet.Domain.Common.Models.Transaction;
using static IotaWalletNet.Domain.Common.Models.Events.EventTypes;

namespace IotaWalletNet.Application.Common.Extensions
{
    public static class TransactionExtensions
    {
        private static SemaphoreSlim MUTEX = new SemaphoreSlim(1);
        private static string? TRANSACTION_ID = null;
        private static Action<IWalletEvent>? WAIT_CONFIRMATION_SET_RESULT;

        public static async Task WaitForConfirmationAsync(this Transaction transaction, IWallet wallet)
        {
            await MUTEX.WaitAsync();

            wallet.WalletEventReceived += Account_TransactionInclusionWalletEventReceived;
            TRANSACTION_ID = transaction.TransactionId;

            TaskCompletionSource<IWalletEvent> taskCompletionSource = new TaskCompletionSource<IWalletEvent>();
            Task<IWalletEvent> waitConfirmationTask = taskCompletionSource.Task;
            WAIT_CONFIRMATION_SET_RESULT = taskCompletionSource.SetResult;

            await waitConfirmationTask;

            wallet.WalletEventReceived -= Account_TransactionInclusionWalletEventReceived;
            WAIT_CONFIRMATION_SET_RESULT = null;
            TRANSACTION_ID = null;

            MUTEX.Release();
        }

        public static void Account_TransactionInclusionWalletEventReceived(object? sender, IWalletEvent? walletEvent)
        {
            if (walletEvent!.Event.Type == WalletEventTypes.TransactionInclusion.ToString())
            {
                if (WAIT_CONFIRMATION_SET_RESULT == null || TRANSACTION_ID == null)
                    return;

                TransactionInclusionWalletEventType? transactionInclusionWalletEvent = walletEvent!.Event as TransactionInclusionWalletEventType;

                if (transactionInclusionWalletEvent!.TransactionInclusion.TransactionId == TRANSACTION_ID && transactionInclusionWalletEvent.TransactionInclusion.InclusionState == "Confirmed")
                {
                    WAIT_CONFIRMATION_SET_RESULT(walletEvent);
                }
            }
        }
    }
}
