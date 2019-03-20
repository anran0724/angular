(function ($) {
    app.modals.CreateOrEditEccpDictLabelStatusModal = function () {

        var _eccpDictLabelStatusesService = abp.services.app.eccpDictLabelStatuses;

        var _modalManager;
        var _$eccpDictLabelStatusInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictLabelStatusInformationForm = _modalManager.getModal().find('form[name=EccpDictLabelStatusInformationsForm]');
            _$eccpDictLabelStatusInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictLabelStatusInformationForm.valid()) {
                return;
            }

            var eccpDictLabelStatus = _$eccpDictLabelStatusInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictLabelStatusesService.createOrEdit(
				eccpDictLabelStatus
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictLabelStatusModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);