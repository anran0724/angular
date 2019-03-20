(function ($) {
    app.modals.StopContractEccpMaintenanceContractModal = function () {

        var _eccpMaintenanceContractsService = abp.services.app.eccpMaintenanceContracts;

        var _modalManager;
        var _$eccpMaintenanceContractInformationForm = null;
        
        
        this.init = function (modalManager) {
            _modalManager = modalManager;
            _$eccpMaintenanceContractInformationForm = _modalManager.getModal().find('#EccpMaintenanceContractInformationsForm');
            _$eccpMaintenanceContractInformationForm.validate();
        };

        this.save = function () {
            if (!_$eccpMaintenanceContractInformationForm.valid()) {
                return;
            }
            
            var eccpMaintenanceContract = _$eccpMaintenanceContractInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eccpMaintenanceContractsService.stopContract(
                eccpMaintenanceContract
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.stopContractEccpMaintenanceContractModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);