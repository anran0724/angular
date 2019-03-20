(function ($) {
    app.modals.CreateOrEditEccpMaintenanceWorkModal = function () {

        var _eccpMaintenanceWorksService = abp.services.app.eccpMaintenanceWorks;

        var _modalManager;
        var _$eccpMaintenanceWorkInformationForm = null;

		        var _eccpMaintenanceWorkOrderLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorks/EccpMaintenanceWorkOrderLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorks/_EccpMaintenanceWorkOrderLookupTableModal.js',
            modalClass: 'EccpMaintenanceWorkOrderLookupTableModal'
        });        var _eccpMaintenanceTemplateNodeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorks/EccpMaintenanceTemplateNodeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorks/_EccpMaintenanceTemplateNodeLookupTableModal.js',
            modalClass: 'EccpMaintenanceTemplateNodeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceWorkInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceWorkInformationsForm]');
            _$eccpMaintenanceWorkInformationForm.validate();
        };

		          $('#OpenEccpMaintenanceWorkOrderLookupTableButton').click(function () {

            var eccpMaintenanceWork = _$eccpMaintenanceWorkInformationForm.serializeFormToObject();

            _eccpMaintenanceWorkOrderLookupTableModal.open({ id: eccpMaintenanceWork.maintenanceWorkOrderId, displayName: eccpMaintenanceWork.eccpMaintenanceWorkOrderPlanCheckDate }, function (data) {
                _$eccpMaintenanceWorkInformationForm.find('input[name=eccpMaintenanceWorkOrderPlanCheckDate]').val(data.displayName); 
                _$eccpMaintenanceWorkInformationForm.find('input[name=maintenanceWorkOrderId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceWorkOrderPlanCheckDateButton').click(function () {
                _$eccpMaintenanceWorkInformationForm.find('input[name=eccpMaintenanceWorkOrderPlanCheckDate]').val(''); 
                _$eccpMaintenanceWorkInformationForm.find('input[name=maintenanceWorkOrderId]').val(''); 
        });
		
        $('#OpenEccpMaintenanceTemplateNodeLookupTableButton').click(function () {

            var eccpMaintenanceWork = _$eccpMaintenanceWorkInformationForm.serializeFormToObject();

            _eccpMaintenanceTemplateNodeLookupTableModal.open({ id: eccpMaintenanceWork.maintenanceTemplateNodeId, displayName: eccpMaintenanceWork.eccpMaintenanceTemplateNodeNodeName }, function (data) {
                _$eccpMaintenanceWorkInformationForm.find('input[name=eccpMaintenanceTemplateNodeNodeName]').val(data.displayName); 
                _$eccpMaintenanceWorkInformationForm.find('input[name=maintenanceTemplateNodeId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceTemplateNodeNodeNameButton').click(function () {
                _$eccpMaintenanceWorkInformationForm.find('input[name=eccpMaintenanceTemplateNodeNodeName]').val(''); 
                _$eccpMaintenanceWorkInformationForm.find('input[name=maintenanceTemplateNodeId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceWorkInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceWork = _$eccpMaintenanceWorkInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceWorksService.createOrEdit(
				eccpMaintenanceWork
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceWorkModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);