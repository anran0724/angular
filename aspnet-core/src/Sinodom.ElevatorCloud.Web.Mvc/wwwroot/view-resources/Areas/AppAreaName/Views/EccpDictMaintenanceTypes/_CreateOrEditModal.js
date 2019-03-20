(function ($) {
    app.modals.CreateOrEditEccpDictMaintenanceTypeModal = function () {

        var _eccpDictMaintenanceTypesService = abp.services.app.eccpDictMaintenanceTypes;

        var _modalManager;
        var _$eccpDictMaintenanceTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictMaintenanceTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictMaintenanceTypeInformationsForm]');
            _$eccpDictMaintenanceTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictMaintenanceTypeInformationForm.valid()) {
                return;
            }

            var eccpDictMaintenanceType = _$eccpDictMaintenanceTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictMaintenanceTypesService.createOrEdit(
				eccpDictMaintenanceType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictMaintenanceTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);