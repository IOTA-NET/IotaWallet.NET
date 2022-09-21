use futures::executor::block_on;
use iota_wallet::{
    iota_client::{stronghold::StrongholdAdapter, Client},
    secret::{stronghold::StrongholdSecretManager,},
};
use libc::c_char;
use std::ffi::{CString, CStr};
use crate::commons::convert_c_ptr_to_string;

#[no_mangle]
pub extern "C" fn generate_mnemonic() -> *const c_char
{
    let mnemonic: String = Client::generate_mnemonic().unwrap();
    let c_string: CString = CString::new(mnemonic).unwrap();
    
    c_string.into_raw()
}

#[no_mangle]
pub  extern "C" fn create_stronghold_secret_manager(password_ptr: *const c_char)
 -> *mut StrongholdAdapter
{
    let password: String = convert_c_ptr_to_string(password_ptr);
    
    let secret_manager: StrongholdAdapter 
        = StrongholdSecretManager::builder()
            .password(password.as_str())
            .build("./mystronghold")
            .unwrap();

    Box::into_raw(Box::new(secret_manager))
}

#[no_mangle]
pub  extern "C" fn store_mnemonic(
    secret_manager_ptr: *mut StrongholdAdapter, 
    mnemonic_ptr: *const c_char
)
{
    let secret_manager: &mut StrongholdAdapter = unsafe {
        assert!(!secret_manager_ptr.is_null());
        &mut *secret_manager_ptr
    };

    let mnemonic_string :String = convert_c_ptr_to_string(mnemonic_ptr);
    
    block_on(secret_manager.store_mnemonic(mnemonic_string)).unwrap();
}