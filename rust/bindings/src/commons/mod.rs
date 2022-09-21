use futures::executor::block_on;
use iota_wallet::{secret::{stronghold::StrongholdSecretManager, SecretManager}, iota_client::stronghold::StrongholdAdapter, ClientOptions, account_manager::AccountManager};
use libc::c_char;
use std::ffi::{CStr};


#[no_mangle]
pub extern "C" fn free_c_string(c_char_ptr: *mut c_char) {
    unsafe {
        if c_char_ptr.is_null() {
            return;
        }

        let c_str: &CStr = CStr::from_ptr(c_char_ptr);
        let bytes_len: usize = c_str.to_bytes_with_nul().len();
        let _: Vec<c_char> = Vec::from_raw_parts(c_char_ptr, bytes_len, bytes_len);
    }
}

pub fn convert_c_ptr_to_string(c_char_ptr: *const c_char) -> String
{
    let c_str = unsafe{

        assert!(!c_char_ptr.is_null());
        CStr::from_ptr(c_char_ptr)
    };

    let str = c_str
                        .to_str()
                        .unwrap();
    String::from(str)
}


pub fn create_stronghold_adapter(password:&str) -> StrongholdAdapter
{
        StrongholdSecretManager::builder()
                .password(password)
                .build("./mystronghold")
                .unwrap()
}

pub fn create_client_options(node_url:&str) -> ClientOptions
{
    ClientOptions::new()
        .with_node(node_url)
        .unwrap()
}

pub fn create_account_manager(password:&str, node_url:&str, coin_type:u32) -> AccountManager
{

   block_on(AccountManager::builder()
                .with_secret_manager(SecretManager::Stronghold(create_stronghold_adapter(password)))
                .with_client_options(create_client_options(node_url))
                .with_coin_type(coin_type)
                .finish())
            .unwrap()
}