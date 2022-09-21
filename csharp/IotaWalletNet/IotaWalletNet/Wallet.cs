using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class Wallet
    {
        [DllImport("bindings", EntryPoint = "create_wallet_manager", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateWalletManager(string password, string nodeUrl, UInt32 coinType);

        [DllImport("bindings", EntryPoint = "create_account", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CreateAccount(IntPtr accountManagerHandle, string accountName);

        [DllImport("bindings", EntryPoint = "get_usernames", CallingConvention = CallingConvention.Cdecl)]
        private static extern string GetUsernames(IntPtr accountManagerHandle);

        [DllImport("bindings", EntryPoint = "get_account", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetAccount(IntPtr accountManagerHandle, string username);

        public enum CoinType : UInt32
        {
            IOTA = 4218,
            SHIMMER = 4219
        }

        private CoinType _coinType;
        private string _nodeUrl;
        private SecretManager? _secretManager;
        private IntPtr _walletHandle;

        public static string DEFAULT_NODE_URL = "https://api.testnet.shimmer.network";
        public static CoinType DEFAULT_COIN_TYPE = CoinType.SHIMMER;

        public Wallet()
        {
            _coinType = DEFAULT_COIN_TYPE;
            _nodeUrl = DEFAULT_NODE_URL;
            _secretManager = null;
            _walletHandle = IntPtr.Zero;

        }

        public Wallet SetCoinType(CoinType coinType)
        {
            _coinType = coinType;
            return this;
        }

        public Wallet SetNodeUrl(string nodeUrl)
        {
            _nodeUrl = nodeUrl;
            return this;
        }

        public Wallet SetSecretManager(SecretManager secretManager)
        {
            _secretManager = secretManager;
            return this;
        }

        public Wallet Connect()
        {
            if (_secretManager == null)
                throw new Exception("SecretManager is not initialized. Try using the SetSecretManager() function.");
            
            _walletHandle = Wallet.CreateWalletManager(
                                        _secretManager.GetPassword(), 
                                        _nodeUrl, 
                                        (UInt32)_coinType);
            return this;
        }

        public List<string> GetUsernames()
        {
            if (_secretManager == null)
                throw new Exception("SecretManager is not initialized. Try using the SetSecretManager() function.");

            string jsonResponse = Wallet.GetUsernames(GetHandle());

            if (jsonResponse == null)
                return new List<string>();

            return JsonConvert.DeserializeObject<List<string>>(jsonResponse)!;
        }
        public void CreateAccount(string accountName) => Wallet.CreateAccount(GetHandle(), accountName);
        
        public Account GetAccount(string username)
        {
            IntPtr accountHandle = Wallet.GetAccount(GetHandle(), username);

            return new Account(accountHandle);
        }

        public IntPtr GetHandle() => _walletHandle;
    }
}
