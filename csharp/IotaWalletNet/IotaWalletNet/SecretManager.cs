using System.Runtime.InteropServices;

namespace IotaWalletNet
{
    public class SecretManager
    {
        /// <summary>
        /// This function is used to create a secret manager.
        /// A secret manager internally creates a stronghold file.
        /// This stronghold file is named "mystronghold".
        /// </summary>
        /// <remarks>
        /// Do note for the current implementation, the mnemonic is hard coded in this function.
        /// As we call store mnemonic, it can generate an error if there already exists a stronghold file.
        /// Remember to delete stronghold file before proceeding.
        /// This is however temporary. It is planned to remove the hardcoded mnemonic.
        /// </remarks>
        /// <param name="password">A password for the stronghold file to encrypot your mnemonic and other data.</param>
        [DllImport("bindings", EntryPoint = "create_stronghold_secret_manager", CallingConvention = CallingConvention.Cdecl)]
        private static extern void CreateSecretManager(string password);

        [DllImport("bindings", EntryPoint = "generate_mnemonic", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GenerateMnemonic();

        public SecretManager(string password)
        {
            CreateSecretManager(password);
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
    }
}
