(function ($) {
    app.modals.CreateOrEditEccpDictMaintenanceStatusModal = function () {

        var _eccpDictMaintenanceStatusesService = abp.services.app.eccpDictMaintenanceStatuses;

        var _modalManager;
        var _$eccpDictMaintenanceStatusInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictMaintenanceStatusInformationForm = _modalManager.getModal().find('form[name=EccpDictMaintenanceStatusInformationsForm]');
            _$eccpDictMaintenanceStatusInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictMaintenanceStatusInformationForm.valid()) {
                return;
            }

            var eccpDictMaintenanceStatus = _$eccpDictMaintenanceStatusInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictMaintenanceStatusesService.createOrEdit(
				eccpDictMaintenanceStatus
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictMaintenanceStatusModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);