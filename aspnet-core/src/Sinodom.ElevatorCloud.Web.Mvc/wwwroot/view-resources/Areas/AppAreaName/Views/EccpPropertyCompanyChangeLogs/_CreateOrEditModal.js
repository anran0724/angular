(function ($) {
    app.modals.CreateOrEditEccpPropertyCompanyChangeLogModal = function () {

        var _eccpPropertyCompanyChangeLogsService = abp.services.app.eccpPropertyCompanyChangeLogs;

        var _modalManager;
        var _$eccpPropertyCompanyChangeLogInformationForm = null;

		        var _eCCPBasePropertyCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpPropertyCompanyChangeLogs/ECCPBasePropertyCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpPropertyCompanyChangeLogs/_ECCPBasePropertyCompanyLookupTableModal.js',
            modalClass: 'ECCPBasePropertyCompanyLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpPropertyCompanyChangeLogInformationForm = _modalManager.getModal().find('form[name=EccpPropertyCompanyChangeLogInformationsForm]');
            _$eccpPropertyCompanyChangeLogInformationForm.validate();
        };

		          $('#OpenECCPBasePropertyCompanyLookupTableButton').click(function () {

            var eccpPropertyCompanyChangeLog = _$eccpPropertyCompanyChangeLogInformationForm.serializeFormToObject();

            _eCCPBasePropertyCompanyLookupTableModal.open({ id: eccpPropertyCompanyChangeLog.propertyCompanyId, displayName: eccpPropertyCompanyChangeLog.eCCPBasePropertyCompanyName }, function (data) {
                _$eccpPropertyCompanyChangeLogInformationForm.find('input[name=eCCPBasePropertyCompanyName]').val(data.displayName); 
                _$eccpPropertyCompanyChangeLogInformationForm.find('input[name=propertyCompanyId]').val(data.id); 
            });
        });
		
		$('#ClearECCPBasePropertyCompanyNameButton').click(function () {
                _$eccpPropertyCompanyChangeLogInformationForm.find('input[name=eCCPBasePropertyCompanyName]').val(''); 
                _$eccpPropertyCompanyChangeLogInformationForm.find('input[name=propertyCompanyId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpPropertyCompanyChangeLogInformationForm.valid()) {
                return;
            }

            var eccpPropertyCompanyChangeLog = _$eccpPropertyCompanyChangeLogInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpPropertyCompanyChangeLogsService.createOrEdit(
				eccpPropertyCompanyChangeLog
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpPropertyCompanyChangeLogModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);