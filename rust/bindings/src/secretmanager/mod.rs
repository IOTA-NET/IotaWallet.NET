use iota_wallet::{
    iota_client::{stronghold::StrongholdAdapter},
    secret::{stronghold::StrongholdSecretManager},
};


pub async extern "C" fn create_stronghold_secret_manager()
{
    let mut secret_manager: StrongholdAdapter 
        = StrongholdSecretManager::builder()
            .password("password")
            .build("./mystronghold")
            .unwrap();

    let mnemonic: String = String::from("sail symbol venture people general equal sight pencil slight muscle sausage faculty retreat decorate library all humor metal place mandate cake door disease dwarf");

    secret_manager.store_mnemonic(mnemonic)
                    .await
                    .unwrap();

}