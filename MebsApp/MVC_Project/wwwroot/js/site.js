// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ActiveLoginCheck() {
    var user = '@ViewData["EmpCode"]';
    if (user == null || user == '') {
        alert('INACTIVE SESSION');
        loadIndex();
    }

}



function MenuPageLoader(pageID) {

    var id = '@ViewData["empCode"]';
    //   alert(pageID);
    var code = encrypt(id);


    window.location.href = "../" + pageID + "/Index?datas=" + pageID;

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

