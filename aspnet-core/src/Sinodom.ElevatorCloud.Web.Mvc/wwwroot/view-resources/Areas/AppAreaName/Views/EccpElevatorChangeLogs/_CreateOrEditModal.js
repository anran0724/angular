(function ($) {
    app.modals.CreateOrEditEccpElevatorChangeLogModal = function () {

        var _eccpElevatorChangeLogsService = abp.services.app.eccpElevatorChangeLogs;

        var _modalManager;
        var _$eccpElevatorChangeLogInformationForm = null;

		        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorChangeLogs/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorChangeLogs/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpElevatorChangeLogInformationForm = _modalManager.getModal().find('form[name=EccpElevatorChangeLogInformationsForm]');
            _$eccpElevatorChangeLogInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var eccpElevatorChangeLog = _$eccpElevatorChangeLogInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpElevatorChangeLog.elevatorId, displayName: eccpElevatorChangeLog.eccpBaseElevatorName }, function (data) {
                _$eccpElevatorChangeLogInformationForm.find('input[name=eccpBaseElevatorName]').val(data.displayName); 
                _$eccpElevatorChangeLogInformationForm.find('input[name=elevatorId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorNameButton').click(function () {
                _$eccpElevatorChangeLogInformationForm.find('input[name=eccpBaseElevatorName]').val(''); 
                _$eccpElevatorChangeLogInformationForm.find('input[name=elevatorId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpElevatorChangeLogInformationForm.valid()) {
                return;
            }

            var eccpElevatorChangeLog = _$eccpElevatorChangeLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpElevatorChangeLogsService.createOrEdit(
				eccpElevatorChangeLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpElevatorChangeLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);