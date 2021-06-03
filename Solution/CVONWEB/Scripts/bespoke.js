$(document).ready(function (e) {
    ModalHandlers();
    Forms();
    EventHandlers();
});
function ModalHandlers() {
    $('a.mainlogin').on('click', function (e) { e.preventDefault(); $('div.desktop-form').toggleClass('show-form'); });
    $('a.btn-register').on('click', function (e) { e.preventDefault(); $('div#register-block').toggleClass('show-reg-form'); });
    $('div#register-block span.close-me').on('click', function (e) { e.preventDefault(); $('div#register-block').toggleClass('show-reg-form'); });
}
function Forms() {
    $('form#userregistrationform').on('submit', function (e) {
        e.preventDefault();
        if ($(this).valid()) {
            var model = {
                RegisterUserFullName: $('#RegisterUserFullName').val(), RegisterUserEmailAddress: $('#RegisterUserEmailAddress').val(), RegisterUserPassword: $('#RegisterUserPassword').val()
            };
            var url = '/umbraco/api/UserManagementApi/RegisterUser';

            $.post(url, model, function (res) {
                if (res.ResponseCode === 'success') {
                    location = res.ResponseData;
                } else {
                    $('div#register-block div.response-data').html(res.ResponseMessage);
                }
            });
        }

        //$.ajax({
        //    type: "POST",
        //    url: '/umbraco/api/UserManagement/RegisterUser',
        //    data: sform,
        //    contentType: "application/json; charset=utf-8",
        //    cache: false,
        //    success: function (res) {
        //        if (res.ResponseCode === 'success') {
        //            location = res.ResponseData;
        //        } else {
        //            $('div#register-block div.response-data').html('res.ResponseData');
        //        }
        //    }
        //});
    });
}
function EventHandlers() {
    $('div.title').on('click', function (e) {
        $(this).toggleClass('active');
        $(this).next().toggleClass('active');
    });
}
function ShowNotification(type, msg) { var ele = $('div.notifybar'); if (type === 'success') { $(ele).toggleClass('success'); $(ele).html(msg); } else { $(ele).addClass('error'); $(ele).html(msg); } setTimeout(function () { if (type === 'success') { $(ele).toggleClass('success'); $(ele).html(''); } else { $(ele).addClass('error'); $(ele).html(''); } }, 3000); }