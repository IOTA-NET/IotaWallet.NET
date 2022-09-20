use iota_wallet::{
    iota_client::{stronghold::StrongholdAdapter},
    secret::{stronghold::StrongholdSecretManager},
};
use libc::c_char;
use std::ffi::CStr;

use futures::{self, executor::block_on};

#[no_mangle]
pub  extern "C" fn create_stronghold_secret_manager(password_ptr: *const c_char)
{
    let c_str_password = unsafe{

        assert!(!password_ptr.is_null());
        CStr::from_ptr(password_ptr)
    };

    let password: &str = c_str_password.to_str().unwrap();

    let mut secret_manager: StrongholdAdapter 
        = StrongholdSecretManager::builder()
            .password(password)
            .build("./mystronghold")
            .unwrap();

    let mnemonic: String = String::from("sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf");

    block_on(secret_manager.store_mnemonic(mnemonic)).unwrap();

}