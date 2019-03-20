(function ($) {
    app.modals.AuditEccpMaintenanceTroubledWorkOrderModal = function () {

        var _eccpMaintenanceTroubledWorkOrdersService = abp.services.app.eccpMaintenanceTroubledWorkOrders;

        var _modalManager;
        var _$eccpMaintenanceTroubledWorkOrderInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceTroubledWorkOrderInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTroubledWorkOrderInformationsForm]');
            _$eccpMaintenanceTroubledWorkOrderInformationForm.validate();
        };

        $('#IsAuditFilterId').change(function () {
            var opt = $("#IsAuditFilterId").val();
            if (opt == 2) {
                $('#remark').show();
            } else {
                $('#remark').hide();
            }
        });

        this.save = function () {
            if (!_$eccpMaintenanceTroubledWorkOrderInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceTroubledWorkOrder = _$eccpMaintenanceTroubledWorkOrderInformationForm.serializeFormToObject();
            eccpMaintenanceTroubledWorkOrder.IsAudit = $("#IsAuditFilterId").val();
            if (eccpMaintenanceTroubledWorkOrder.IsAudit == 2) {
                if ($("#EccpMaintenanceTroubledWorkOrder_Remarks").val() == "") {
                    return;
                }
            }
            _modalManager.setBusy(true);
            _eccpMaintenanceTroubledWorkOrdersService.audit(
                eccpMaintenanceTroubledWorkOrder
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpMaintenanceTroubledWorkOrderModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);