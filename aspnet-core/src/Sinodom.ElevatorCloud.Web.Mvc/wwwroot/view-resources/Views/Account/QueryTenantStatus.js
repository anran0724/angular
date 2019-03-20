var CurrentPage = function () {

    var handleQueryTenantStatus = function () {

        var $form = $('.query-tenant-form');

        $form.validate();

        $form.find('input').keypress(function (e) {
            if (e.which === 13) {
                if ($('.forget-form').valid()) {
                    $('.forget-form').submit();
                }
                return false;
            }
        });

        $form.submit(function (e) {
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
                }).done(function (resultData) {

                    if (resultData.statusCode === 1) {

                        abp.message.success('', resultData.statusName)
                            .done(function() {
                                location.href = abp.appPath + 'Account/Login';
                            });

                    } else if (resultData.statusCode === 2) {

                        abp.message.info('' ,resultData.statusName);

                    } else if (resultData.statusCode === -1 || resultData.statusCode === -2) {

                        abp.message.error('', resultData.statusName);

                    } else if (resultData.statusCode === 0) {

                        abp.message.warn(resultData.remarks, resultData.statusName)
                            .done(function () {


                                location.href = abp.appPath +
                                    'TenantRegistration/RegisterEdit' +
                                    abp.utils.buildQueryString([
                                        { name: 'tn', value: $('#tenancyName').val() }
                                    ]);
                            });

                    }
                })
            );
        });
    }

    return {
        init: function () {
            handleQueryTenantStatus();
        }
    };
}();