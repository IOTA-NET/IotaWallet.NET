using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class SecretManager
    {
        #region PInvokes
        /// <summary>
        /// This function is used to create a secret manager.
        /// A secret manager internally creates a stronghold file.
        /// This stronghold file is named "mystronghold".
        /// </summary>
        /// <param name="password">A password for the stronghold file to encrypot your mnemonic and other data.</param>
        [DllImport("bindings", EntryPoint = "create_stronghold_secret_manager", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateSecretManager(string password);

        [DllImport("bindings", EntryPoint = "generate_mnemonic", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GenerateMnemonic();

        [DllImport("bindings", EntryPoint = "store_mnemonic", CallingConvention = CallingConvention.Cdecl)]
        private static extern void StoreMnemonic(IntPtr secretManagerPtr, string mnemonic);
        #endregion

        private IntPtr secretManagerHandle;

        public SecretManager(string password)
        {
            secretManagerHandle = CreateSecretManager(password);
        }

        /// <summary>
        /// Encrypts and stores the mnemonic. Do only do this the first time you are creating
        /// the wallet.
        /// </summary>
        /// <param name="mnemonic"></param>
        /// <returns></returns>
        public SecretManager StoreMnemonic(string mnemonic)
        {
            SecretManager.StoreMnemonic(secretManagerHandle, mnemonic);

            return this;
        }

        #region Static Functions
        public static string GenerateNewMnemonic()
        {
            IntPtr mnemonic_ptr = GenerateMnemonic();

            if (mnemonic_ptr == IntPtr.Zero)
                throw new Exception("Unable to generate a new mnemonic as ffi returned null pointer to mnemonic");
            
            
            string mnemonic = Marshal.PtrToStringAnsi(mnemonic_ptr)!;

            RustCommons.FreeCString(mnemonic_ptr);
            
            return mnemonic;
        }

        #endregion
    }
}
