using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.Common.Notifications;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Options;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Application
{
    public class Wallet : IWallet, IDisposable
    {

        #region Variables

        private IntPtr _walletHandle;

        private StringBuilder _errorBuffer;

        private WalletOptions _walletOptions;

        private readonly IMediator _mediator;

        private MessageReceivedCallback _messageReceivedCallback;

        private Action<string>? _endOfCallbackSignaller;

        #endregion

        public Wallet(IMediator mediator)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            _walletOptions = new WalletOptions();

            _walletHandle = IntPtr.Zero;

            _errorBuffer = new StringBuilder();
            _mediator = mediator;

            _messageReceivedCallback = WalletMessageReceivedCallback;

            _endOfCallbackSignaller = null;
        }

        public async Task<string> SendAmountAsync(string username, AddressesWithAmountAndTransactionOptions addressesWithAmount)
        {
            return await _mediator.Send(new SendAmountCommand(this, username, addressesWithAmount));
        }

        public async Task<string> GetBalanceAsync(string username)
        {
            return await _mediator.Send(new GetBalanceQuery(this, username));
        }

        public async Task<string> GetAccountAsync(string username)
        {
            return await _mediator.Send(new GetAccountQuery(this, username));
        }

        public async Task<string> GetNewMnemonicAsync()
        {
            return await _mediator.Send(new GetNewMnemonicQuery(this));
        }

        public async Task<string> StoreMnemonicAsync(string mnemonic)
        {
            return await _mediator.Send(new StoreMnemonicCommand(this, mnemonic));
        }

        public async Task<string> VerifyMnemonicAsync(string mnemonic)
        {
            return await _mediator.Send(new VerifyMnemonicCommand(this, mnemonic));
        }

        public async Task<string> CreateAccountAsync(string username)
        {
            return await _mediator.Send(new CreateAccountCommand(this, username));
        }

        public void WalletMessageReceivedCallback(string message, string error, IntPtr context)
        {
            string messageToSignal = String.IsNullOrEmpty(message) ? error : message;

            if (_endOfCallbackSignaller != null)
                _endOfCallbackSignaller(messageToSignal);
        }

        public void Connect()
        {
            string walletOptions = JsonConvert.SerializeObject(GetWalletOptions());

            int errorBufferSize = 1024;

            _errorBuffer = new StringBuilder(errorBufferSize);

            _walletHandle = InitializeIotaWallet(walletOptions, _errorBuffer, errorBufferSize);

            if (!string.IsNullOrEmpty(_errorBuffer.ToString()))
                throw new Exception(_errorBuffer.ToString());

        }
        public void SendMessage(string message)
        {
            SendMessageToRust(_walletHandle, message, _messageReceivedCallback, IntPtr.Zero);
        }
        public async Task<string> SendMessageAsync(string message)
        {
            
            var taskCompletionSource = new TaskCompletionSource<string>();
            var task = taskCompletionSource.Task;
            _endOfCallbackSignaller = taskCompletionSource.SetResult;

            await Task.Factory.StartNew(() =>
            {
                SendMessage(message);
            }, TaskCreationOptions.AttachedToParent);


            return await task;
        }

        public WalletOptions GetWalletOptions() => _walletOptions;


        public ClientOptionsBuilder ConfigureClientOptions()
            => new ClientOptionsBuilder(this);


        public SecretManagerOptionsBuilder ConfigureSecretManagerOptions()
            => new SecretManagerOptionsBuilder(this);

        public WalletOptionsBuilder ConfigureWalletOptions()
            => new WalletOptionsBuilder(this);

        public void Dispose()
        {
            CloseIotaWallet(_walletHandle);
            _walletHandle = IntPtr.Zero;
        }
    }
}
