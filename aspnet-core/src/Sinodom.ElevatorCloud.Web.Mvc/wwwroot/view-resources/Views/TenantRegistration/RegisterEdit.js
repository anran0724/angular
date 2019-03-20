var CurrentPage = function () {

    var handleRegisterEdit = function() {

        $('#TenancyName').val(mUtil.getURLParam('tn'));

        var $login = $("#m_login");
        var $form = $('.query-tenant-form');

        $form.validate();

        $form.find('input').keypress(function(e) {
            if (e.which === 13) {
                if ($('.forget-form').valid()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        $form.submit(function(e) {
            e.preventDefault();

            if (!$form.valid()) {
                return;
            }

            var formData = $form.serialize();

            abp.ui.setBusy(
                null,
                abp.ajax({
                    contentType: app.consts.contentTypes.formUrlencoded,
                    url: $form.attr('action'),
                    data: formData
                }).done(function(resultData) {

                    if (resultData === '已审核') {

                        abp.message.success('您的账号已通过审核，无需编辑注册信息', '已通过审核')
                            .done(function () {
                                location.href = abp.appPath + 'Account/Login';
                            });

                    } else if (resultData === '不存在') {

                        abp.message.error('当前公司在系统中不存在，请重新输入', '公司不存在');

                    } else if (resultData === '通过') {



                        $login.removeClass("m-login--forget-password"), $login.removeClass("m-login--signin"),
                            $login.addClass("m-login--signup"), mUtil.animateClass($login.find(".m-login__signup")[0],
                                "flipInX animated");

                    } else if (resultData === '未通过') {

                        abp.message.error('您输入的管理员密码不正确，请重新输入', '密码错误');

                    } else {
                        abp.message.error('', resultData);
                    }
                })
            );
        });
    };

    return {
        init: function () {
            handleRegisterEdit();
        }
    };
}();