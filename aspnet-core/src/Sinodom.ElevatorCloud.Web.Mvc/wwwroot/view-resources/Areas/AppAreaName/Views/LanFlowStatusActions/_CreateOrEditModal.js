(function ($) {
    app.modals.CreateOrEditLanFlowStatusActionModal = function () {

        var _lanFlowStatusActionsService = abp.services.app.lanFlowStatusActions;

        var _modalManager;
        var _$lanFlowStatusActionInformationForm = null;

		        var _lanFlowSchemeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/LanFlowStatusActions/LanFlowSchemeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/LanFlowStatusActions/_LanFlowSchemeLookupTableModal.js',
            modalClass: 'LanFlowSchemeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$lanFlowStatusActionInformationForm = _modalManager.getModal().find('form[name=LanFlowStatusActionInformationsForm]');
            _$lanFlowStatusActionInformationForm.validate();
        };

		          $('#OpenLanFlowSchemeLookupTableButton').click(function () {

            var lanFlowStatusAction = _$lanFlowStatusActionInformationForm.serializeFormToObject();

            _lanFlowSchemeLookupTableModal.open({ id: lanFlowStatusAction.schemeId, displayName: lanFlowStatusAction.lanFlowSchemeSchemeName }, function (data) {
                _$lanFlowStatusActionInformationForm.find('input[name=lanFlowSchemeSchemeName]').val(data.displayName); 
                _$lanFlowStatusActionInformationForm.find('input[name=schemeId]').val(data.id); 
            });
        });
		
		$('#ClearLanFlowSchemeSchemeNameButton').click(function () {
                _$lanFlowStatusActionInformationForm.find('input[name=lanFlowSchemeSchemeName]').val(''); 
                _$lanFlowStatusActionInformationForm.find('input[name=schemeId]').val(''); 
        });
		


        this.save = function () {
            if (!_$lanFlowStatusActionInformationForm.valid()) {
                return;
            }

            var lanFlowStatusAction = _$lanFlowStatusActionInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _lanFlowStatusActionsService.createOrEdit(
				lanFlowStatusAction
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditLanFlowStatusActionModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);