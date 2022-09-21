use futures::executor::block_on;
use iota_wallet::{
    account_manager::{AccountManager, self},
    ClientOptions,
    iota_client::{stronghold::StrongholdAdapter, Client},
    secret::{stronghold::StrongholdSecretManager, SecretManager,}, account::AccountHandle,
};

use libc::c_char;
use std::ffi::{CString, CStr};
use crate::commons::{convert_c_ptr_to_string, create_account_manager, convert_string_to_c_ptr};

#[no_mangle]
pub extern "C" fn get_usernames(account_manager_ptr: *mut AccountManager) -> *const c_char
{
    let account_manager: &mut AccountManager = unsafe {
        assert!(!account_manager_ptr.is_null());
        &mut *account_manager_ptr
    };
    
    let account_handles: Vec<AccountHandle> = block_on(account_manager.get_accounts()).unwrap();

    let mut alias_vector: Vec<String> = Vec::new();
    
    for account_handle in account_handles
    {
        let alias: String  = block_on(account_handle.alias());
        alias_vector.push(alias);
    }

    let json: String = serde_json::to_string(&alias_vector).unwrap();

    convert_string_to_c_ptr(json)
}


#[no_mangle]
pub extern "C" fn create_wallet_manager
(
    password_ptr: *const c_char,
    node_url_ptr: *const c_char,
    coin_type: u32
) -> *mut AccountManager
{
    let password: String = convert_c_ptr_to_string(password_ptr);
    
    let node_url: String = convert_c_ptr_to_string(node_url_ptr);

    let account_manager: AccountManager = create_account_manager(password.as_str(), node_url.as_str(), coin_type);
    
    Box::into_raw(Box::new(account_manager))
}

#[no_mangle]
pub extern "C" fn create_account(
    account_manager_ptr: *mut AccountManager,
    account_name_ptr: *const c_char,
)
{
    let account_name: String = convert_c_ptr_to_string(account_name_ptr);
    let account_manager: &mut AccountManager = unsafe {
        assert!(!account_manager_ptr.is_null());
        &mut *account_manager_ptr
    };

    block_on(account_manager
                .create_account()
                .with_alias(account_name)
                .finish()
            )
            .unwrap();


}