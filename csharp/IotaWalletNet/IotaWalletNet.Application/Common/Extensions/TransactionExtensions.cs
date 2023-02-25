using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Common.Models.Events.WalletEventTypes;
using IotaWalletNet.Domain.Common.Models.Transaction;
using static IotaWalletNet.Domain.Common.Models.Events.EventTypes;

namespace IotaWalletNet.Application.Common.Extensions
{
    public static class TransactionExtensions
    {
        private static SemaphoreSlim CONFIRMATION_MUTEX = new SemaphoreSlim(1);
        private static string? CONFIRMATION_TRANSACTION_ID = null;
        private static Action<IWalletEvent>? WAIT_CONFIRMATION_SET_RESULT;

        private static SemaphoreSlim NEW_OUTPUT_MUTEX = new SemaphoreSlim(1);
        private static string? NEW_OUTPUT_TRANSACTION_ID = null;
        private static Action<IWalletEvent>? WAIT_NEW_OUTPUT_SET_RESULT;

        public static async Task WaitForNewOutputAsync(this Transaction transaction, IAccount account)
        {
            await NEW_OUTPUT_MUTEX.WaitAsync();
            
            IWallet wallet = account.Wallet;
            NEW_OUTPUT_TRANSACTION_ID = transaction.TransactionId;
            TaskCompletionSource<IWalletEvent> taskCompletionSource = new TaskCompletionSource<IWalletEvent>();
            Task<IWalletEvent> waitNewOutputTask = taskCompletionSource.Task;
            WAIT_NEW_OUTPUT_SET_RESULT = taskCompletionSource.SetResult;
            wallet.WalletEventReceived += Account_NewOuputWalletEventReceived;
            await waitNewOutputTask;

            wallet.WalletEventReceived -= Account_NewOuputWalletEventReceived;
            WAIT_NEW_OUTPUT_SET_RESULT = null;
            NEW_OUTPUT_TRANSACTION_ID = null;

            NEW_OUTPUT_MUTEX.Release();
        }
        public static async Task WaitForConfirmationAsync(this Transaction transaction, IAccount account)
        {
            await CONFIRMATION_MUTEX.WaitAsync();

            IWallet wallet = account.Wallet;

            CONFIRMATION_TRANSACTION_ID = transaction.TransactionId;

            TaskCompletionSource<IWalletEvent> taskCompletionSource = new TaskCompletionSource<IWalletEvent>();
            Task<IWalletEvent> waitConfirmationTask = taskCompletionSource.Task;
            WAIT_CONFIRMATION_SET_RESULT = taskCompletionSource.SetResult;
            wallet.WalletEventReceived += Account_TransactionInclusionWalletEventReceived;
            await waitConfirmationTask;

            wallet.WalletEventReceived -= Account_TransactionInclusionWalletEventReceived;
            WAIT_CONFIRMATION_SET_RESULT = null;
            CONFIRMATION_TRANSACTION_ID = null;

            CONFIRMATION_MUTEX.Release();
        }

        public static void Account_TransactionInclusionWalletEventReceived(object? sender, IWalletEvent? walletEvent)
        {
            if (walletEvent!.Event.Type == WalletEventTypes.TransactionInclusion.ToString())
            {
                if (WAIT_CONFIRMATION_SET_RESULT == null || CONFIRMATION_TRANSACTION_ID == null)
                    return;

                TransactionInclusionWalletEventType? transactionInclusionWalletEvent = walletEvent!.Event as TransactionInclusionWalletEventType;

                if (transactionInclusionWalletEvent!.TransactionInclusion.TransactionId == CONFIRMATION_TRANSACTION_ID && transactionInclusionWalletEvent.TransactionInclusion.InclusionState == "Confirmed")
                {
                    WAIT_CONFIRMATION_SET_RESULT(walletEvent);
                }
            }
        }

        public static void Account_NewOuputWalletEventReceived(object? sender, IWalletEvent? walletEvent)
        {
            if (walletEvent!.Event.Type == WalletEventTypes.NewOutput.ToString())
            {
                if (WAIT_NEW_OUTPUT_SET_RESULT == null || NEW_OUTPUT_TRANSACTION_ID == null)
                    return;

                NewOutputWalletEventType? newOutputWalletEventType = walletEvent!.Event as NewOutputWalletEventType;

                if(newOutputWalletEventType!.NewOutput.Output.Metadata.TransactionId == NEW_OUTPUT_TRANSACTION_ID)
                {
                    WAIT_NEW_OUTPUT_SET_RESULT(walletEvent);
                }
            }
        }
    }
}
