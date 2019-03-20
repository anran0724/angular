(function ($) {
    app.modals.CreateOrEditEccpBaseElevatorModelModal = function () {

        var _eccpBaseElevatorModelsService = abp.services.app.eccpBaseElevatorModels;

        var _modalManager;
        var _$eccpBaseElevatorModelInformationForm = null;

		        var _eccpBaseElevatorBrandLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorModels/EccpBaseElevatorBrandLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorModels/_EccpBaseElevatorBrandLookupTableModal.js',
            modalClass: 'EccpBaseElevatorBrandLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpBaseElevatorModelInformationForm = _modalManager.getModal().find('form[name=EccpBaseElevatorModelInformationsForm]');
            _$eccpBaseElevatorModelInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorBrandLookupTableButton').click(function () {

            var eccpBaseElevatorModel = _$eccpBaseElevatorModelInformationForm.serializeFormToObject();

            _eccpBaseElevatorBrandLookupTableModal.open({ id: eccpBaseElevatorModel.elevatorBrandId, displayName: eccpBaseElevatorModel.eccpBaseElevatorBrandName }, function (data) {
                _$eccpBaseElevatorModelInformationForm.find('input[name=eccpBaseElevatorBrandName]').val(data.displayName); 
                _$eccpBaseElevatorModelInformationForm.find('input[name=elevatorBrandId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorBrandNameButton').click(function () {
                _$eccpBaseElevatorModelInformationForm.find('input[name=eccpBaseElevatorBrandName]').val(''); 
                _$eccpBaseElevatorModelInformationForm.find('input[name=elevatorBrandId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpBaseElevatorModelInformationForm.valid()) {
                return;
            }

            var eccpBaseElevatorModel = _$eccpBaseElevatorModelInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpBaseElevatorModelsService.createOrEdit(
				eccpBaseElevatorModel
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpBaseElevatorModelModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);