using IotaWalletNet.Application.AccountContext.Commands.SendAmount;
using IotaWalletNet.Application.AccountContext.Commands.SyncAccount;
using IotaWalletNet.Application.AccountContext.Queries.GetBalance;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetAccounts;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.Common.Interfaces;
using IotaWalletNet.Domain.Options;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Application
{
    public class Wallet : RustBridgeCommunicator, IWallet, IDisposable
    {

        #region Variables

        private StringBuilder _errorBuffer;

        private WalletOptions _walletOptions;

        private readonly IMediator _mediator;

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
        }

        

        public async Task<string> GetAccountsAsync()
        {
            return await _mediator.Send(new GetAccountsQuery(this));
        }

        public async Task<string> SyncAccountAsync(string username)
        {
            return await _mediator.Send(new SyncAccountCommand(this, username));
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
        public void Connect()
        {
            string walletOptions = JsonConvert.SerializeObject(GetWalletOptions());

            int errorBufferSize = 1024;

            _errorBuffer = new StringBuilder(errorBufferSize);

            _walletHandle = InitializeIotaWallet(walletOptions, _errorBuffer, errorBufferSize);

            if (!string.IsNullOrEmpty(_errorBuffer.ToString()))
                throw new Exception(_errorBuffer.ToString());

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
