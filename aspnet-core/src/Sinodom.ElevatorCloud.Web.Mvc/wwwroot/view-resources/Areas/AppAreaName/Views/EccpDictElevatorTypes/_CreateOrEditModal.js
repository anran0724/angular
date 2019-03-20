(function ($) {
    app.modals.CreateOrEditEccpDictElevatorTypeModal = function () {

        var _eccpDictElevatorTypesService = abp.services.app.eccpDictElevatorTypes;

        var _modalManager;
        var _$eccpDictElevatorTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictElevatorTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictElevatorTypeInformationsForm]');
            _$eccpDictElevatorTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictElevatorTypeInformationForm.valid()) {
                return;
            }

            var eccpDictElevatorType = _$eccpDictElevatorTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictElevatorTypesService.createOrEdit(
				eccpDictElevatorType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictElevatorTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);