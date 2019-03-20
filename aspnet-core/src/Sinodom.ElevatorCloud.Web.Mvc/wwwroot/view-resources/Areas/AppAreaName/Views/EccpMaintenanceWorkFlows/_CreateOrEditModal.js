(function ($) {
    app.modals.CreateOrEditEccpMaintenanceWorkFlowModal = function () {

        var _eccpMaintenanceWorkFlowsService = abp.services.app.eccpMaintenanceWorkFlows;

        var _modalManager;
        var _$eccpMaintenanceWorkFlowInformationForm = null;

		        var _eccpMaintenanceTemplateNodeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkFlows/EccpMaintenanceTemplateNodeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkFlows/_EccpMaintenanceTemplateNodeLookupTableModal.js',
            modalClass: 'EccpMaintenanceTemplateNodeLookupTableModal'
        });        var _eccpMaintenanceWorkLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkFlows/EccpMaintenanceWorkLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkFlows/_EccpMaintenanceWorkLookupTableModal.js',
            modalClass: 'EccpMaintenanceWorkLookupTableModal'
        });        var _eccpDictMaintenanceWorkFlowStatusLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkFlows/EccpDictMaintenanceWorkFlowStatusLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkFlows/_EccpDictMaintenanceWorkFlowStatusLookupTableModal.js',
            modalClass: 'EccpDictMaintenanceWorkFlowStatusLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceWorkFlowInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceWorkFlowInformationsForm]');
            _$eccpMaintenanceWorkFlowInformationForm.validate();
        };

		          $('#OpenEccpMaintenanceTemplateNodeLookupTableButton').click(function () {

            var eccpMaintenanceWorkFlow = _$eccpMaintenanceWorkFlowInformationForm.serializeFormToObject();

            _eccpMaintenanceTemplateNodeLookupTableModal.open({ id: eccpMaintenanceWorkFlow.maintenanceTemplateNodeId, displayName: eccpMaintenanceWorkFlow.eccpMaintenanceTemplateNodeNodeName }, function (data) {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpMaintenanceTemplateNodeNodeName]').val(data.displayName); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=maintenanceTemplateNodeId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceTemplateNodeNodeNameButton').click(function () {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpMaintenanceTemplateNodeNodeName]').val(''); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=maintenanceTemplateNodeId]').val(''); 
        });
		
        $('#OpenEccpMaintenanceWorkLookupTableButton').click(function () {

            var eccpMaintenanceWorkFlow = _$eccpMaintenanceWorkFlowInformationForm.serializeFormToObject();

            _eccpMaintenanceWorkLookupTableModal.open({ id: eccpMaintenanceWorkFlow.maintenanceWorkId, displayName: eccpMaintenanceWorkFlow.eccpMaintenanceWorkTaskName }, function (data) {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpMaintenanceWorkTaskName]').val(data.displayName); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=maintenanceWorkId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceWorkTaskNameButton').click(function () {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpMaintenanceWorkTaskName]').val(''); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=maintenanceWorkId]').val(''); 
        });
		
        $('#OpenEccpDictMaintenanceWorkFlowStatusLookupTableButton').click(function () {

            var eccpMaintenanceWorkFlow = _$eccpMaintenanceWorkFlowInformationForm.serializeFormToObject();

            _eccpDictMaintenanceWorkFlowStatusLookupTableModal.open({ id: eccpMaintenanceWorkFlow.dictMaintenanceWorkFlowStatusId, displayName: eccpMaintenanceWorkFlow.eccpDictMaintenanceWorkFlowStatusName }, function (data) {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpDictMaintenanceWorkFlowStatusName]').val(data.displayName); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=dictMaintenanceWorkFlowStatusId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictMaintenanceWorkFlowStatusNameButton').click(function () {
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=eccpDictMaintenanceWorkFlowStatusName]').val(''); 
                _$eccpMaintenanceWorkFlowInformationForm.find('input[name=dictMaintenanceWorkFlowStatusId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceWorkFlowInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceWorkFlow = _$eccpMaintenanceWorkFlowInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceWorkFlowsService.createOrEdit(
				eccpMaintenanceWorkFlow
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceWorkFlowModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);