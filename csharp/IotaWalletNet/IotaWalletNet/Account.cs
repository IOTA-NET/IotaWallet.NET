using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class Account
    {
        private readonly IntPtr _accountHandle;

        public Account(IntPtr accountHandle)
        {
            _accountHandle = accountHandle;
        }

        public IntPtr GetHandle() => _accountHandle;
    }
}
