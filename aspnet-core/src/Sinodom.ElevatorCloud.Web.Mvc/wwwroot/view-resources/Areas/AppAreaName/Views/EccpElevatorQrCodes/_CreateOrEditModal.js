(function ($) {
    app.modals.CreateOrEditEccpElevatorQrCodeModal = function () {
    
        var _eccpElevatorQrCodesService = abp.services.app.eccpElevatorQrCodes;

        var _modalManager;
        var _$eccpElevatorQrCodeInformationForm = null;

		        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorQrCodes/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorQrCodes/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpElevatorQrCodeInformationForm = _modalManager.getModal().find('form[name=EccpElevatorQrCodeInformationsForm]');
            _$eccpElevatorQrCodeInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var eccpElevatorQrCode = _$eccpElevatorQrCodeInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpElevatorQrCode.elevatorId, displayName: eccpElevatorQrCode.eccpBaseElevatorName }, function (data) {
                _$eccpElevatorQrCodeInformationForm.find('input[name=eccpBaseElevatorName]').val(data.displayName); 
                _$eccpElevatorQrCodeInformationForm.find('input[name=elevatorId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorNameButton').click(function () {
                _$eccpElevatorQrCodeInformationForm.find('input[name=eccpBaseElevatorName]').val(''); 
                _$eccpElevatorQrCodeInformationForm.find('input[name=elevatorId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpElevatorQrCodeInformationForm.valid()) {
                return;
            }


            var eccpElevatorQrCode = _$eccpElevatorQrCodeInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpElevatorQrCodesService.createOrEdit(
				eccpElevatorQrCode
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpElevatorQrCodeModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);