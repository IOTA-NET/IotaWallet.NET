use futures::executor::block_on;
use iota_wallet::{
    iota_client::{stronghold::StrongholdAdapter, Client},
    secret::{stronghold::StrongholdSecretManager, SecretManager,}, account_manager::AccountManager,
};
use libc::c_char;
use std::{ffi::{CString, CStr}, sync::Arc};
use crate::commons::{convert_c_ptr_to_string, create_stronghold_adapter};

#[no_mangle]
pub extern "C" fn generate_mnemonic() -> *const c_char
{
    let mnemonic: String = Client::generate_mnemonic().unwrap();
    let c_string: CString = CString::new(mnemonic).unwrap();
    
    c_string.into_raw()
}


#[no_mangle]
pub  extern "C" fn create_stronghold_secret_manager(password_ptr: *const c_char)
{
    let password: String = convert_c_ptr_to_string(password_ptr);
    
    create_stronghold_adapter(password.as_str());
}

#[no_mangle]
pub  extern "C" fn store_mnemonic(
    password_ptr: *const c_char, 
    mnemonic_ptr: *const c_char
)
{
    let password: String = convert_c_ptr_to_string(password_ptr);
    
    let mnemonic :String = convert_c_ptr_to_string(mnemonic_ptr);
    
    block_on(
            create_stronghold_adapter(password.as_str())
            .store_mnemonic(mnemonic)
        )
        .unwrap();
}