
use std::str::FromStr;

use iota_wallet::{message_interface::ManagerOptions, ClientOptions, account_manager::AccountManager, secret::SecretManager};
use serde::{Serialize, Deserialize};
use serde_json::Value;


#[derive(Serialize, Deserialize)]
struct Hello
{
    value: u32,
    name: String,
}

fn main()
{
    match SecretManager::from_str(r#"{"Stronghold":{"Password":"password","snapshotPath":"./jsonstronghold"}}"#)
    {
        Ok(v) => println!("good"),
        Err(e) => println!("g {e:?}"),
    };

    // AccountManager::builder().with_secret_manager(secret_manager)
    ClientOptions::new().from_json(r#" 
    {
        "nodes":[],
        "localPow":true,
        "fallbackToLocalPow": true,
        "offline": true
    } "#);


    // match serde_json::from_str(r#" {
    //     "StoragePath": "./walletdb",
    //     "CoinType": 4219,
    //     "secretManager": "{"Stronghold":{"Password":"password","SnapshotPath":"./jsonstronghold"}}",
    //     "clientOptions": "{"PrimaryNode":{"Url":"https://api.testnet.shimmer.network"},"LocalPow":false}"
    //   } "#) 
    // {
    //     Ok(v) => v,
    //     Err(e) => println!("g {e:?}"),
    // };

    // match serde_json::from_str(json.as_str()) {
    //     Ok(v) => println!("{}", v),
    //     Err(e) => println!("{e:?}")
    // }
    //  let x:ManagerOptions = serde_json::from_str(json.as_str()).unwrap();

    // let x = serde_json::ser::to_string(&x).unwrap();
    // println!("{}", x);
}