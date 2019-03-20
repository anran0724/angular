(function ($) {
    app.modals.CreateOrEditECCPDictElevatorStatusModal = function () {

        var _eCCPDictElevatorStatusesService = abp.services.app.eCCPDictElevatorStatuses;

        var _modalManager;
        var _$eCCPDictElevatorStatusInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPDictElevatorStatusInformationForm = _modalManager.getModal().find('form[name=ECCPDictElevatorStatusInformationsForm]');
            _$eCCPDictElevatorStatusInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eCCPDictElevatorStatusInformationForm.valid()) {
                return;
            }

            var eCCPDictElevatorStatus = _$eCCPDictElevatorStatusInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eCCPDictElevatorStatusesService.createOrEdit(
				eCCPDictElevatorStatus
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditECCPDictElevatorStatusModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);