(function ($) {
    app.modals.CreateOrEditEccpBaseElevatorLabelBindLogModal = function () {

        var _eccpBaseElevatorLabelBindLogsService = abp.services.app.eccpBaseElevatorLabelBindLogs;

        var _modalManager;
        var _$eccpBaseElevatorLabelBindLogInformationForm = null;

		        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabelBindLogs/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabelBindLogs/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });        var _eccpDictLabelStatusLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabelBindLogs/EccpDictLabelStatusLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabelBindLogs/_EccpDictLabelStatusLookupTableModal.js',
            modalClass: 'EccpDictLabelStatusLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpBaseElevatorLabelBindLogInformationForm = _modalManager.getModal().find('form[name=EccpBaseElevatorLabelBindLogInformationsForm]');
            _$eccpBaseElevatorLabelBindLogInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var eccpBaseElevatorLabelBindLog = _$eccpBaseElevatorLabelBindLogInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpBaseElevatorLabelBindLog.elevatorId, displayName: eccpBaseElevatorLabelBindLog.eccpBaseElevatorName }, function (data) {
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=eccpBaseElevatorName]').val(data.displayName); 
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=elevatorId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorNameButton').click(function () {
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=eccpBaseElevatorName]').val(''); 
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=elevatorId]').val(''); 
        });
		
        $('#OpenEccpDictLabelStatusLookupTableButton').click(function () {

            var eccpBaseElevatorLabelBindLog = _$eccpBaseElevatorLabelBindLogInformationForm.serializeFormToObject();

            _eccpDictLabelStatusLookupTableModal.open({ id: eccpBaseElevatorLabelBindLog.labelStatusId, displayName: eccpBaseElevatorLabelBindLog.eccpDictLabelStatusName }, function (data) {
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=eccpDictLabelStatusName]').val(data.displayName); 
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=labelStatusId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictLabelStatusNameButton').click(function () {
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=eccpDictLabelStatusName]').val(''); 
                _$eccpBaseElevatorLabelBindLogInformationForm.find('input[name=labelStatusId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpBaseElevatorLabelBindLogInformationForm.valid()) {
                return;
            }

            var eccpBaseElevatorLabelBindLog = _$eccpBaseElevatorLabelBindLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpBaseElevatorLabelBindLogsService.createOrEdit(
				eccpBaseElevatorLabelBindLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpBaseElevatorLabelBindLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);