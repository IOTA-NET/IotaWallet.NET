using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class SecretManager
    {
        #region PInvokes

        /// <summary>
        /// Generates a random valid mnemonic
        /// </summary>
        [DllImport("bindings", EntryPoint = "generate_mnemonic", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GenerateMnemonic();



        // <summary>
        // This function is used to create a secret manager.
        // A secret manager internally creates a stronghold file.
        // This stronghold file is named "mystronghold".
        // </summary>
        // <param name="password">A password for the stronghold file to encrypot your mnemonic and other data.</param>
        [DllImport("bindings", EntryPoint = "create_stronghold_secret_manager", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CreateSecretManager(string password);



        [DllImport("bindings", EntryPoint = "store_mnemonic", CallingConvention = CallingConvention.Cdecl)]
        private static extern void StoreMnemonic(string password, string mnemonic);
        #endregion

        private string? _password;

        public SecretManager SetPassword(string password)
        {
            _password = password.Trim();
            return this;
        }

        public SecretManager InitializeStronghold()
        {
            CreateSecretManager(GetPassword());

            return this;
        }
        public static string GenerateNewMnemonic()
        {
            IntPtr mnemonic_ptr = GenerateMnemonic();

            if (mnemonic_ptr == IntPtr.Zero)
                throw new Exception("Unable to generate a new mnemonic as ffi returned null pointer to mnemonic");


            string mnemonic = Marshal.PtrToStringAnsi(mnemonic_ptr)!;

            RustCommons.FreeCString(mnemonic_ptr);

            return mnemonic;
        }

        public string GetPassword()
        {
            VerifyPasswordIsSet();

            return _password!;
        }

        private bool VerifyPasswordIsSet()
        {
            if (string.IsNullOrEmpty(_password))
                throw new Exception("Null or empty password. Try setting the password with SetPassword() function first.");
            
            return true;
        }

        /// <summary>
        /// Encrypts and stores the mnemonic. Do only do this the first time you are creating
        /// the wallet.
        /// </summary>
        /// <param name="mnemonic"></param>
        /// <returns></returns>
        public SecretManager StoreMnemonic(string mnemonic)
        {
            SecretManager.StoreMnemonic(GetPassword(), mnemonic);

            return this;
        }



    }
}
