use futures::executor::block_on;
use iota_wallet::{
    iota_client::{stronghold::StrongholdAdapter, Client},
    secret::{stronghold::StrongholdSecretManager,},
};
use libc::c_char;
use std::ffi::{CString, CStr};


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
    let c_str_password = unsafe{

        assert!(!password_ptr.is_null());
        CStr::from_ptr(password_ptr)
    };

    let password: &str = c_str_password.to_str().unwrap();

    let secret_manager: StrongholdAdapter 
        = StrongholdSecretManager::builder()
            .password(password)
            .build("./mystronghold")
            .unwrap();

    Box::into_raw(Box::new(secret_manager))


    // let mnemonic: String = String::from("sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf");

    // block_on(secret_manager.store_mnemonic(mnemonic)).unwrap();

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

    let c_str_mnemonic = unsafe{

        assert!(!mnemonic_ptr.is_null());
        CStr::from_ptr(mnemonic_ptr)
    };

    let mnemonic: &str = c_str_mnemonic.to_str().unwrap();

    let mnemonic_string :String = String::from(mnemonic);
    
    block_on(secret_manager.store_mnemonic(mnemonic_string)).unwrap();
}