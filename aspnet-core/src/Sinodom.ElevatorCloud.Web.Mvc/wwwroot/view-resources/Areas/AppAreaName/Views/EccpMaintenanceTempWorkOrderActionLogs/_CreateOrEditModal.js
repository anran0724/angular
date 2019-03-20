(function ($) {
    app.modals.CreateOrEditEccpMaintenanceTempWorkOrderActionLogModal = function () {

        var _eccpMaintenanceTempWorkOrderActionLogsService = abp.services.app.eccpMaintenanceTempWorkOrderActionLogs;

        var _modalManager;
        var _$eccpMaintenanceTempWorkOrderActionLogInformationForm = null;

		        var _eccpMaintenanceTempWorkOrderLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrderActionLogs/EccpMaintenanceTempWorkOrderLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrderActionLogs/_EccpMaintenanceTempWorkOrderLookupTableModal.js',
            modalClass: 'EccpMaintenanceTempWorkOrderLookupTableModal'
        });        var _userLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrderActionLogs/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrderActionLogs/_UserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceTempWorkOrderActionLogInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTempWorkOrderActionLogInformationsForm]');
            _$eccpMaintenanceTempWorkOrderActionLogInformationForm.validate();
        };

		          $('#OpenEccpMaintenanceTempWorkOrderLookupTableButton').click(function () {

            var eccpMaintenanceTempWorkOrderActionLog = _$eccpMaintenanceTempWorkOrderActionLogInformationForm.serializeFormToObject();

            _eccpMaintenanceTempWorkOrderLookupTableModal.open({ id: eccpMaintenanceTempWorkOrderActionLog.tempWorkOrderId, displayName: eccpMaintenanceTempWorkOrderActionLog.eccpMaintenanceTempWorkOrderTitle }, function (data) {
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=eccpMaintenanceTempWorkOrderTitle]').val(data.displayName); 
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=tempWorkOrderId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceTempWorkOrderTitleButton').click(function () {
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=eccpMaintenanceTempWorkOrderTitle]').val(''); 
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=tempWorkOrderId]').val(''); 
        });
		
        $('#OpenUserLookupTableButton').click(function () {

            var eccpMaintenanceTempWorkOrderActionLog = _$eccpMaintenanceTempWorkOrderActionLogInformationForm.serializeFormToObject();

            _userLookupTableModal.open({ id: eccpMaintenanceTempWorkOrderActionLog.userId, displayName: eccpMaintenanceTempWorkOrderActionLog.userName }, function (data) {
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=userName]').val(data.displayName); 
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=userName]').val(''); 
                _$eccpMaintenanceTempWorkOrderActionLogInformationForm.find('input[name=userId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceTempWorkOrderActionLogInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceTempWorkOrderActionLog = _$eccpMaintenanceTempWorkOrderActionLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceTempWorkOrderActionLogsService.createOrEdit(
				eccpMaintenanceTempWorkOrderActionLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceTempWorkOrderActionLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);