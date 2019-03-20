(function ($) {
    app.modals.CreateOrEditEccpDictMaintenanceWorkFlowStatusModal = function () {

        var _eccpDictMaintenanceWorkFlowStatusesService = abp.services.app.eccpDictMaintenanceWorkFlowStatuses;

        var _modalManager;
        var _$eccpDictMaintenanceWorkFlowStatusInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictMaintenanceWorkFlowStatusInformationForm = _modalManager.getModal().find('form[name=EccpDictMaintenanceWorkFlowStatusInformationsForm]');
            _$eccpDictMaintenanceWorkFlowStatusInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictMaintenanceWorkFlowStatusInformationForm.valid()) {
                return;
            }

            var eccpDictMaintenanceWorkFlowStatus = _$eccpDictMaintenanceWorkFlowStatusInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictMaintenanceWorkFlowStatusesService.createOrEdit(
				eccpDictMaintenanceWorkFlowStatus
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictMaintenanceWorkFlowStatusModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);