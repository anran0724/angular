(function ($) {
    app.modals.CreateOrEditEccpCompanyUserAuditLogModal = function () {

        var _eccpCompanyUserAuditLogsService = abp.services.app.eccpCompanyUserAuditLogs;

        var _modalManager;
        var _$eccpCompanyUserAuditLogInformationForm = null;

		        var _userLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyUserAuditLogs/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpCompanyUserAuditLogs/_UserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpCompanyUserAuditLogInformationForm = _modalManager.getModal().find('form[name=EccpCompanyUserAuditLogInformationsForm]');
            _$eccpCompanyUserAuditLogInformationForm.validate();
        };

		          $('#OpenUserLookupTableButton').click(function () {

            var eccpCompanyUserAuditLog = _$eccpCompanyUserAuditLogInformationForm.serializeFormToObject();

            _userLookupTableModal.open({ id: eccpCompanyUserAuditLog.userId, displayName: eccpCompanyUserAuditLog.userName }, function (data) {
                _$eccpCompanyUserAuditLogInformationForm.find('input[name=userName]').val(data.displayName); 
                _$eccpCompanyUserAuditLogInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$eccpCompanyUserAuditLogInformationForm.find('input[name=userName]').val(''); 
                _$eccpCompanyUserAuditLogInformationForm.find('input[name=userId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpCompanyUserAuditLogInformationForm.valid()) {
                return;
            }

            var eccpCompanyUserAuditLog = _$eccpCompanyUserAuditLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpCompanyUserAuditLogsService.createOrEdit(
				eccpCompanyUserAuditLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpCompanyUserAuditLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);