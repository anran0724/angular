(function ($) {
    app.modals.CreateOrEditEccpMaintenanceWorkLogModal = function () {

        var _eccpMaintenanceWorkLogsService = abp.services.app.eccpMaintenanceWorkLogs;

        var _modalManager;
        var _$eccpMaintenanceWorkLogInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceWorkLogInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceWorkLogInformationsForm]');
            _$eccpMaintenanceWorkLogInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpMaintenanceWorkLogInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceWorkLog = _$eccpMaintenanceWorkLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceWorkLogsService.createOrEdit(
				eccpMaintenanceWorkLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceWorkLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);