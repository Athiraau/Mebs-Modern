function testalert() {
   alert("testing.....");
}

function ActiveLoginCheck() {
    var user = '@ViewData["EmpCode"]';
    if (user == null || user == '') {
        alert('INACTIVE SESSION');
        loadIndex();
    }

}


function encrypt(id) {
    var key = CryptoJS.enc.Utf8.parse('8080808080808080');
    var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
    var encrypteddata = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(id), key,
        {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });

    return encrypteddata;

}


