(function ($) {
    app.modals.CreateOrEditEccpDictMaintenanceItemModal = function () {

        var _eccpDictMaintenanceItemsService = abp.services.app.eccpDictMaintenanceItems;

        var _modalManager;
        var _$eccpDictMaintenanceItemInformationForm = null;

		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpDictMaintenanceItemInformationForm = _modalManager.getModal().find('form[name=EccpDictMaintenanceItemInformationsForm]');
            _$eccpDictMaintenanceItemInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpDictMaintenanceItemInformationForm.valid()) {
                return;
            }

            var eccpDictMaintenanceItem = _$eccpDictMaintenanceItemInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpDictMaintenanceItemsService.createOrEdit(
				eccpDictMaintenanceItem
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpDictMaintenanceItemModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);