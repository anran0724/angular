(function ($) {
    app.modals.CreateOrEditEccpMaintenanceCompanyChangeLogModal = function () {

        var _eccpMaintenanceCompanyChangeLogsService = abp.services.app.eccpMaintenanceCompanyChangeLogs;

        var _modalManager;
        var _$eccpMaintenanceCompanyChangeLogInformationForm = null;

		        var _eCCPBaseMaintenanceCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceCompanyChangeLogs/ECCPBaseMaintenanceCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceCompanyChangeLogs/_ECCPBaseMaintenanceCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseMaintenanceCompanyLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceCompanyChangeLogInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceCompanyChangeLogInformationsForm]');
            _$eccpMaintenanceCompanyChangeLogInformationForm.validate();
        };

		          $('#OpenECCPBaseMaintenanceCompanyLookupTableButton').click(function () {

            var eccpMaintenanceCompanyChangeLog = _$eccpMaintenanceCompanyChangeLogInformationForm.serializeFormToObject();

            _eCCPBaseMaintenanceCompanyLookupTableModal.open({ id: eccpMaintenanceCompanyChangeLog.maintenanceCompanyId, displayName: eccpMaintenanceCompanyChangeLog.eCCPBaseMaintenanceCompanyName }, function (data) {
                _$eccpMaintenanceCompanyChangeLogInformationForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val(data.displayName); 
                _$eccpMaintenanceCompanyChangeLogInformationForm.find('input[name=maintenanceCompanyId]').val(data.id); 
            });
        });
		
		$('#ClearECCPBaseMaintenanceCompanyNameButton').click(function () {
                _$eccpMaintenanceCompanyChangeLogInformationForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val(''); 
                _$eccpMaintenanceCompanyChangeLogInformationForm.find('input[name=maintenanceCompanyId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceCompanyChangeLogInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceCompanyChangeLog = _$eccpMaintenanceCompanyChangeLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceCompanyChangeLogsService.createOrEdit(
				eccpMaintenanceCompanyChangeLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceCompanyChangeLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);