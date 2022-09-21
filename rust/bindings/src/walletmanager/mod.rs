use futures::executor::block_on;
use iota_wallet::{
    account_manager::{AccountManager, self},
    ClientOptions,
    iota_client::{stronghold::StrongholdAdapter, Client},
    secret::{stronghold::StrongholdSecretManager, SecretManager,},
};

use libc::c_char;
use std::ffi::{CString, CStr};
use crate::commons::{convert_c_ptr_to_string, create_stronghold_adapter};

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

    let secret_manager: StrongholdAdapter = create_stronghold_adapter(password);

    
    let client_options: ClientOptions = 
        ClientOptions::new()
        .with_node(node_url.as_str())
        .unwrap();

    let account_manager: AccountManager = 
            block_on(AccountManager::builder()
                        .with_secret_manager(SecretManager::Stronghold(secret_manager))
                        .with_client_options(client_options)
                        .with_coin_type(coin_type)
                        .finish()
                    )
                    .unwrap();

    Box::into_raw(Box::new(account_manager))
}