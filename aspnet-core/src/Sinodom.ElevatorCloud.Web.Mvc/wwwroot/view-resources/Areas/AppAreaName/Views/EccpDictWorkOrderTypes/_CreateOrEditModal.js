(function ($) {
    app.modals.CreateOrEditEccpDictWorkOrderTypeModal = function () {

        var _eccpDictWorkOrderTypesService = abp.services.app.eccpDictWorkOrderTypes;

        var _modalManager;
        var _$eccpDictWorkOrderTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictWorkOrderTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictWorkOrderTypeInformationsForm]');
            _$eccpDictWorkOrderTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictWorkOrderTypeInformationForm.valid()) {
                return;
            }

            var eccpDictWorkOrderType = _$eccpDictWorkOrderTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictWorkOrderTypesService.createOrEdit(
				eccpDictWorkOrderType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictWorkOrderTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);