(function ($) {
    app.modals.CreateOrEditEccpDictPlaceTypeModal = function () {

        var _eccpDictPlaceTypesService = abp.services.app.eccpDictPlaceTypes;

        var _modalManager;
        var _$eccpDictPlaceTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictPlaceTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictPlaceTypeInformationsForm]');
            _$eccpDictPlaceTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictPlaceTypeInformationForm.valid()) {
                return;
            }

            var eccpDictPlaceType = _$eccpDictPlaceTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictPlaceTypesService.createOrEdit(
				eccpDictPlaceType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictPlaceTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);