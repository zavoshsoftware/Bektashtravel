
function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}


function AppearElement(disAppearid, apearId) {
    $(apearId).css('display', 'block');
    $(disAppearid).css('display', 'none');

}



function addToBasket(code) {

    var currentCookie = getCookie('basket');

    if (currentCookie == null ||  currentCookie === ""){
        setCookie('basket', code + '/', 1000);
    }
    else {
    
        currentCookie += code + '/';
        setCookie('basket', currentCookie, 1000);
    }

    $('#addtobasket-success').css('display', 'block');
    $("html, body").animate({ scrollTop: 0 }, "slow");
}




function addToBasketwithRedirect(code) {

    var currentCookie = getCookie('basket');

    if (currentCookie == null ||  currentCookie === ""){
        setCookie('basket', code + '/', 1000);
    }
    else {
    
        currentCookie += code + '/';
        setCookie('basket', currentCookie, 1000);
    }

    window.location = "/basket";
}



function FinalizeOrder() {
    AppearElement('#btnFinalize', '#finalizeLoading');


    var cellnumber = $('#txtMobile').val();
    var email = $('#txtEmail').val();
    var fullname = $('#txtFullname').val();


    if (fullname !== "" && cellnumber !== "") {
        $.ajax(
            {
                url: "/Basket/Finalize",
                data: {
                    cellnumber: cellnumber, email: email, fullname: fullname,
                },
                type: "GET"
            }).done(function (result) {
            
                if (result === "invalid") {
                    $('#error-box-otp').css('display', 'block');
                    $('#error-box-otp').html('کد تایید وارد شده صحیح نمی باشد');
                    AppearElement('#finalizeLoading', '#btnFinalize');
                }
                else if (result === "false") {
                    $('#error-box-otp').css('display', 'block');
                    $('#error-box-otp').html('خطایی رخ داده است. لطفا مجددا تلاش کنید');
                    AppearElement('#finalizeLoading', '#btnFinalize');


                }
                else {

                    window.location = "/payment/index?am=" + result[1] + "&code=" + result[0];



                }


            });
    }
    else {
        $('#error-box-otp').css('display', 'block');
        $('#error-box-otp').html('فیلد های ستاره دار را تکمیل نمایید.');
        AppearElement('#finalizeLoading', '#btnFinalize');

    }

}
