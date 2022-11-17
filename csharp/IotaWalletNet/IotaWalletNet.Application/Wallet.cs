using IotaWalletNet.Application.Common.Interfaces;
using IotaWalletNet.Application.Common.Options;
using IotaWalletNet.Application.WalletContext.Commands.CreateAccount;
using IotaWalletNet.Application.WalletContext.Commands.StoreMnemonic;
using IotaWalletNet.Application.WalletContext.Commands.VerifyMnemonic;
using IotaWalletNet.Application.WalletContext.Queries.GetAccount;
using IotaWalletNet.Application.WalletContext.Queries.GetAccounts;
using IotaWalletNet.Application.WalletContext.Queries.GetNewMnemonic;
using IotaWalletNet.Domain.PlatformInvoke;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using static IotaWalletNet.Application.WalletContext.Queries.GetAccount.GetAccountQueryHandler;
using static IotaWalletNet.Domain.PlatformInvoke.RustBridge;

namespace IotaWalletNet.Application
{
    public class Wallet : RustBridgeCommunicator, IWallet
    {

        #region Variables

        private StringBuilder _errorBuffer;

        private readonly WalletOptions _walletOptions;

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



        public async Task<GetAccountsResponse> GetAccountsAsync()
        {
            return await _mediator.Send(new GetAccountsQuery(this));
        }

        public async Task<(GetAccountResponse response, IAccount? account)> GetAccountAsync(string username)
        {
            GetAccountResponse response = await _mediator.Send(new GetAccountQuery(this, username));

            if (response.IsSuccess())
            {
                IAccount account = new Account(_mediator, username, this);

                return (response, account);
            }
            else
            {
                return (response, null);
            }
        }

        public async Task<GetNewMnemonicResponse> GetNewMnemonicAsync()
        {
            return await _mediator.Send(new GetNewMnemonicQuery(this));
        }

        public async Task<StoreMnemonicResponse> StoreMnemonicAsync(string mnemonic)
        {
            return await _mediator.Send(new StoreMnemonicCommand(this, mnemonic));
        }

        public async Task<VerifyMnemonicResponse> VerifyMnemonicAsync(string mnemonic)
        {
            return await _mediator.Send(new VerifyMnemonicCommand(this, mnemonic));
        }


        public async Task<(CreateAccountResponse response, IAccount? account)> CreateAccountAsync(string username)
        {

            CreateAccountResponse response = await _mediator.Send(new CreateAccountCommand(this, username));

            if (response.IsSuccess())
            {

                IAccount account = new Account(_mediator, username, this);

                return (response, account);
            }
            else
            {
                return (response, null);
            }


        }

        public IWallet ThenInitialize()
        {
            string walletOptions = JsonConvert.SerializeObject(GetWalletOptions());

            int errorBufferSize = 1024;

            _errorBuffer = new StringBuilder(errorBufferSize);

            _walletHandle = InitializeIotaWallet(walletOptions, _errorBuffer, errorBufferSize);

            if (!string.IsNullOrEmpty(_errorBuffer.ToString()))
                throw new Exception(_errorBuffer.ToString());

            return this;
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

        public IntPtr GetWalletHandle() => _walletHandle;
    }
}
