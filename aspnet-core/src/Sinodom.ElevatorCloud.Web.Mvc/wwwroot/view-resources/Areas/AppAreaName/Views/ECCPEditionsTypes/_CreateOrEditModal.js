(function ($) {
    app.modals.CreateOrEditECCPEditionsTypeModal = function () {

        var _eCCPEditionsTypesService = abp.services.app.eCCPEditionsTypes;

        var _modalManager;
        var _$eCCPEditionsTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPEditionsTypeInformationForm = _modalManager.getModal().find('form[name=ECCPEditionsTypeInformationsForm]');
            _$eCCPEditionsTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eCCPEditionsTypeInformationForm.valid()) {
                return;
            }

            var eCCPEditionsType = _$eCCPEditionsTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eCCPEditionsTypesService.createOrEdit(
				eCCPEditionsType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditECCPEditionsTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);