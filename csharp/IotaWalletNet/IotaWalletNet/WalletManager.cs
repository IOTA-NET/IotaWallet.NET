using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class WalletManager
    {
        [DllImport("bindings", EntryPoint = "create_wallet_manager", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateWalletManager(string password, string nodeUrl, UInt32 coinType);

        [DllImport("bindings", EntryPoint = "create_account", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CreateAccount(IntPtr accountManagerHandle, string accountName);


        public enum CoinType : UInt32
        {
            IOTA = 4218,
            SHIMMER = 4219
        }

        private CoinType _coinType;
        private string _nodeUrl;
        private SecretManager? _secretManager;
        private IntPtr _walletManagerHandle;

        public static string DEFAULT_NODE_URL = "https://api.testnet.shimmer.network";
        public static CoinType DEFAULT_COIN_TYPE = CoinType.SHIMMER;

        public WalletManager()
        {
            _coinType = DEFAULT_COIN_TYPE;
            _nodeUrl = DEFAULT_NODE_URL;
            _secretManager = null;
            _walletManagerHandle = IntPtr.Zero;

        }

        public WalletManager SetCoinType(CoinType coinType)
        {
            _coinType = coinType;
            return this;
        }

        public WalletManager SetNodeUrl(string nodeUrl)
        {
            _nodeUrl = nodeUrl;
            return this;
        }

        public WalletManager SetSecretManager(SecretManager secretManager)
        {
            _secretManager = secretManager;
            return this;
        }

        public WalletManager Connect()
        {
            if (_secretManager == null)
                throw new Exception("SecretManager is not initialized. Try using the SetSecretManager() function.");
            
            _walletManagerHandle = WalletManager.CreateWalletManager(
                                        _secretManager.GetPassword(), 
                                        _nodeUrl, 
                                        (UInt32)_coinType);
            return this;
        }

        public void CreateAccount(string accountName) => WalletManager.CreateAccount(GetHandle(), accountName);
        public IntPtr GetHandle() => _walletManagerHandle;
    }
}
