
function Reservation() {
    var fullnameVal = $("#txtFullname").val();
    var emailVal = $("#txtEmail").val();
    var mobileVal = $("#txtMobile").val();
    var numberVal = $("#txtNumber").val();

    var url = window.location.pathname;
    var tourId = url.substring(url.lastIndexOf('/') + 1);
    if (fullnameVal != "", emailVal != "", mobileVal != "", numberVal != "") {
        $.ajax(
            {
                url: "/Tours/RequestPost",
                data: { fullname: fullnameVal, email: emailVal, mobile: mobileVal, number: numberVal, id: tourId },
                type: "GET"
            }).done(function (result) {
                if (result == "true") {
                    $('#errorDivQ').css('display', 'none');
                    $('#SuccessDivQ').css('display', 'block');
                }
                else if (result == "InvalidEmail") {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('ایمیل وارد شده صحیح نمی باشد.');
                }
                else {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('خطایی رخ داد! مجددا تلاش نمایید..');

                }
            });

    }
    else {
        $('#SuccessDivQ').css('display', 'none');
        $('#errorDivQ').html('لطفا تمامی موارد را تکمیل نمایید.');
        $('#errorDivQ').css('display', 'block');
    }

};
function toggle(id) {
    var current = document.getElementById(id);
    if (current.classList.contains("show")) {
        current.classList.remove("show");
        var currentLi = "#li_" + id + " p";
        $(currentLi).css('display', 'none');
    }
    else {
        current.classList.add("show");
        var currentLi = "#li_" + id + " p";
        //var inner = "#li_" + id + " p.inner";
        //$(".accordion-element .inner").css('display', 'none');
        $(currentLi).css('display', 'block');
        //$('.accordion-element p').css('display', 'block');
        //var length = current.length;
        //for (var i = 0; i < length; i++)
        //{
        //    if (current[i].classList.contains("show"))
        //    {
        //        $('.accordion-element .show p').css('display', 'block');
        //    }
        //}
        var t = 0;
    }
};
function BlogComment()
{
    var messageVal = $("#txtMessage").val();
    var mobileVal = $("#txtMobile").val();
    var fullnameVal = $("#txtFullname").val();
    var url = window.location.pathname;
    var blogId = url.substring(url.lastIndexOf('/') + 1);
    if (messageVal != "", mobileVal != "", fullnameVal != "") {
        $.ajax(
            {
                url: "/Blogs/RequestPost",
                data: { fullname: fullnameVal, message: messageVal, mobile: mobileVal, urlParam: blogId },
                type: "GET"
            }).done(function (result) {
                if (result == "true") {
                    $('#errorDivQ').css('display', 'none');
                    $('#SuccessDivQ').css('display', 'block');
                }
                else if (result == "InvalidEmail") {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('ایمیل وارد شده صحیح نمی باشد.');
                }
                else {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('خطایی رخ داد! مجددا تلاش نمایید..');

                }
            });

    }
    else {
        $('#SuccessDivQ').css('display', 'none');
        $('#errorDivQ').html('لطفا تمامی موارد را تکمیل نمایید.');
        $('#errorDivQ').css('display', 'block');
    }
};

function ContactMessage() {
    var messageVal = $("#txtMessage").val();
    var mobileVal = $("#txtMobile").val();
    var fullnameVal = $("#txtFullname").val();
   
    if (messageVal != "", mobileVal != "", fullnameVal != "") {
        $.ajax(
            {
                url: "/Home/RequestPost",
                data: { fullname: fullnameVal, message: messageVal, mobile: mobileVal },
                type: "GET"
            }).done(function (result) {
                if (result == "true") {
                    $('#errorDivQ').css('display', 'none');
                    $('#SuccessDivQ').css('display', 'block');
                }
                else if (result == "InvalidEmail") {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('ایمیل وارد شده صحیح نمی باشد.');
                }
                else {
                    $('#errorDivQ').css('display', 'block');
                    $('#SuccessDivQ').css('display', 'none');
                    $('#errorDivQ').html('خطایی رخ داد! مجددا تلاش نمایید..');

                }
            });

    }
    else {
        $('#SuccessDivQ').css('display', 'none');
        $('#errorDivQ').html('لطفا تمامی موارد را تکمیل نمایید.');
        $('#errorDivQ').css('display', 'block');
    }
};
function SubmitNewsLetter()
{
    var emailVal = $("#txtNewsEmail").val();
    if (emailVal != "") {
        $.ajax(
            {
                url: "/Home/SubmitNewLetter",
                data: { email: emailVal },
                type: "GET"
            }).done(function (result) {
                if (result == "true") {
                    $('#errorDivN').css('display', 'none');
                    $('#SuccessDivN').css('display', 'block');
                }
                else if (result == "InvalidEmail") {
                    $('#errorDivN').css('display', 'block');
                    $('#SuccessDivN').css('display', 'none');
                    $('#errorDivN').html('ایمیل وارد شده صحیح نمی باشد.');
                }
                else {
                    $('#errorDivN').css('display', 'block');
                    $('#SuccessDivN').css('display', 'none');
                    $('#errorDivN').html('خطایی رخ داد! مجددا تلاش نمایید..');

                }
            });

    }
    else {
        $('#SuccessDivN').css('display', 'none');
        $('#errorDivN').html('لطفا تمامی موارد را تکمیل نمایید.');
        $('#errorDivN').css('display', 'block');
    }
};