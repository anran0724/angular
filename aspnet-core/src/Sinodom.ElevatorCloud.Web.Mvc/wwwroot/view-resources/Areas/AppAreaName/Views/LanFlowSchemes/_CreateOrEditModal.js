(function ($) {
    app.modals.CreateOrEditLanFlowSchemeModal = function () {

        var _lanFlowSchemesService = abp.services.app.lanFlowSchemes;

        var _modalManager;
        var _$lanFlowSchemeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$lanFlowSchemeInformationForm = _modalManager.getModal().find('form[name=LanFlowSchemeInformationsForm]');
            _$lanFlowSchemeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$lanFlowSchemeInformationForm.valid()) {
                return;
            }

            var lanFlowScheme = _$lanFlowSchemeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _lanFlowSchemesService.createOrEdit(
				lanFlowScheme
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditLanFlowSchemeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);