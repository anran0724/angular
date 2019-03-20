(function ($) {
    app.modals.CreateOrEditEccpDictTempWorkOrderTypeModal = function () {

        var _eccpDictTempWorkOrderTypesService = abp.services.app.eccpDictTempWorkOrderTypes;

        var _modalManager;
        var _$eccpDictTempWorkOrderTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictTempWorkOrderTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictTempWorkOrderTypeInformationsForm]');
            _$eccpDictTempWorkOrderTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictTempWorkOrderTypeInformationForm.valid()) {
                return;
            }

            var eccpDictTempWorkOrderType = _$eccpDictTempWorkOrderTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictTempWorkOrderTypesService.createOrEdit(
				eccpDictTempWorkOrderType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictTempWorkOrderTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);