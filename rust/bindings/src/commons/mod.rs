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