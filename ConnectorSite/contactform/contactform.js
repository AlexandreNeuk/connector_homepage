jQuery(document).ready(function ($) {
    "use strict";

    $('form.newsLetterForm').submit(function () {
        $('.validationnews').html('');
        var email = $('#emailnews').val();

        if (email) {
            var action = $(this).attr('action');
            if (!action) {
                action = 'home/newsletter?email=' + email;
            }
            $.ajax({
                type: "POST",
                url: action,
                success: function (msg) {
                    //
                    if (msg.data == 'ok') {
                        $("#sendmessagenews").addClass("show");
                        $("#errormessagenews").removeClass("show");
                        $('#emailnews').val('');
                    } else {
                        $("#sendmessagenews").removeClass("show");
                        $("#errormessagenews").addClass("show");
                    }
                }
            });
        }
        else {
            $('.validationnews').html('Por favor informe o email').show('blind');
        }
        return false;
    })

    $('form.contactForm').submit(function () {
        var f = $(this).find('.form-group'),
            ferror = false,
            emailExp = /^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$/i;

        f.children('input').each(function () {
            var i = $(this);
            var rule = i.attr('data-rule');
            if (rule !== undefined) {
                var ierror = false;
                var pos = rule.indexOf(':', 0);
                if (pos >= 0) {
                    var exp = rule.substr(pos + 1, rule.length);
                    rule = rule.substr(0, pos);
                } else {
                    rule = rule.substr(pos + 1, rule.length);
                }
                switch (rule) {
                    case 'required':
                        if (i.val() === '') {
                            ferror = ierror = true;
                        }
                        break;
                    case 'minlen':
                        if (i.val().length < parseInt(exp)) {
                            ferror = ierror = true;
                        }
                        break;
                    case 'email':
                        if (!emailExp.test(i.val())) {
                            ferror = ierror = true;
                        }
                        break;
                    case 'checked':
                        if (!i.is(':checked')) {
                            ferror = ierror = true;
                        }
                        break;
                    case 'regexp':
                        exp = new RegExp(exp);
                        if (!exp.test(i.val())) {
                            ferror = ierror = true;
                        }
                        break;
                }
                i.next('.validation').html((ierror ? (i.attr('data-msg') !== undefined ? i.attr('data-msg') : 'wrong Input') : '')).show('blind');
            }
        });

        f.children('textarea').each(function () {
            var i = $(this);
            var rule = i.attr('data-rule');

            if (rule !== undefined) {
                var ierror = false;
                var pos = rule.indexOf(':', 0);
                if (pos >= 0) {
                    var exp = rule.substr(pos + 1, rule.length);
                    rule = rule.substr(0, pos);
                } else {
                    rule = rule.substr(pos + 1, rule.length);
                }
                switch (rule) {
                    case 'required':
                        if (i.val() === '') {
                            ferror = ierror = true;
                        }
                        break;

                    case 'minlen':
                        if (i.val().length < parseInt(exp)) {
                            ferror = ierror = true;
                        }
                        break;
                }
                i.next('.validation').html((ierror ? (i.attr('data-msg') != undefined ? i.attr('data-msg') : 'wrong Input') : '')).show('blind');
            }
        });

        if (ferror) return false;
        var action = $(this).attr('action');
        if (!action) {
            action = 'home/Contact?nome=' +
                document.getElementById('name').value + '&email=' +
                document.getElementById('email').value + '&assunto=' +
                document.getElementsByName('subject')[0].value + '&mensagem=' +
                document.getElementsByName('Mensagem')[0].value;
        }
        $.ajax({
            type: "POST",
            url: action,
            success: function (msg) {
                //
                if (msg.data == 'ok') {
                    $("#sendmessage").addClass('show');
                    $("#errormessage").removeClass('show');
                    $('.contactForm').find("input, textarea").val('');
                } else {
                    $("#sendmessage").removeClass('show');
                    $("#errormessage").addClass("show");
                }
            }
        });
        return false;
    });
});