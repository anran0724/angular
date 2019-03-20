(function ($) {
    app.modals.CreateOrEditEccpDictNodeTypeModal = function () {

        var _eccpDictNodeTypesService = abp.services.app.eccpDictNodeTypes;

        var _modalManager;
        var _$eccpDictNodeTypeInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictNodeTypeInformationForm = _modalManager.getModal().find('form[name=EccpDictNodeTypeInformationsForm]');
            _$eccpDictNodeTypeInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictNodeTypeInformationForm.valid()) {
                return;
            }

            var eccpDictNodeType = _$eccpDictNodeTypeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictNodeTypesService.createOrEdit(
				eccpDictNodeType
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictNodeTypeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);