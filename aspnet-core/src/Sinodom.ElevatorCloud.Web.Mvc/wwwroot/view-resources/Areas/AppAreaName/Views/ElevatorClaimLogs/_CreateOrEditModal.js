(function ($) {
    app.modals.CreateOrEditElevatorClaimLogModal = function () {

        var _elevatorClaimLogsService = abp.services.app.elevatorClaimLogs;

        var _modalManager;
        var _$elevatorClaimLogInformationForm = null;

		        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ElevatorClaimLogs/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ElevatorClaimLogs/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$elevatorClaimLogInformationForm = _modalManager.getModal().find('form[name=ElevatorClaimLogInformationsForm]');
            _$elevatorClaimLogInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var elevatorClaimLog = _$elevatorClaimLogInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: elevatorClaimLog.elevatorId, displayName: elevatorClaimLog.eccpBaseElevatorName }, function (data) {
                _$elevatorClaimLogInformationForm.find('input[name=eccpBaseElevatorName]').val(data.displayName); 
                _$elevatorClaimLogInformationForm.find('input[name=elevatorId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorNameButton').click(function () {
                _$elevatorClaimLogInformationForm.find('input[name=eccpBaseElevatorName]').val(''); 
                _$elevatorClaimLogInformationForm.find('input[name=elevatorId]').val(''); 
        });
		


        this.save = function () {
            if (!_$elevatorClaimLogInformationForm.valid()) {
                return;
            }

            var elevatorClaimLog = _$elevatorClaimLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _elevatorClaimLogsService.createOrEdit(
				elevatorClaimLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditElevatorClaimLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);