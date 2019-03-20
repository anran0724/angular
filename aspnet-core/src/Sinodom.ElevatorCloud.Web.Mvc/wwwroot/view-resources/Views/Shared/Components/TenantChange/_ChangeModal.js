(function ($) {
    var typeaheadDemo = function () {
        var _tenantAppService = abp.services.app.tenant;

        var initTypeahead = function () {
            $('#m_typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            }, {
                    source: function (q, cb, cc) {
                        _tenantAppService.getTenantNames(q).done(function (data) {
                            cc(data.items);
                        });
                    },
                    display: 'tenancyName',
                    templates: {
                        suggestion: function (data) {
                            return '<p style="text-align: left">' + data.tenancyName + '(' + data.name + ')</p>' + "";
                        }
                    }
                });
        };

        return {
            init: function () {
                initTypeahead();
            }
        };
    }();

    app.modals.TenantChangeModal = function () {

        var _modalManager;
        var _accountService = abp.services.app.account;
        var _$form = null;

        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$form = _modalManager.getModal().find('form[name=TenantChangeForm]');

            typeaheadDemo.init();
        };


        this.save = function () {

            var tenancyName = $.trim(_$form.find('input[name=TenancyName]').val());

            if (!tenancyName) {
                abp.multiTenancy.setTenantIdCookie(null);
                _modalManager.close();
                location.reload();
                return;
            }

            _modalManager.setBusy(true);
            _accountService.isTenantAvailable({
                tenancyName: tenancyName
            }).done(function (result) {
                switch (result.state) {
                    case 1: //Available
                        abp.multiTenancy.setTenantIdCookie(result.tenantId);
                        _modalManager.close();
                        location.reload();
                        return;
                    case 2: //InActive
                        abp.message.warn(app.localize('TenantIsNotActive', tenancyName));
                        break;
                    case 3: //NotFound
                        abp.message.warn(app.localize('ThereIsNoTenantDefinedWithName{0}', tenancyName));
                        break;
                }
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);