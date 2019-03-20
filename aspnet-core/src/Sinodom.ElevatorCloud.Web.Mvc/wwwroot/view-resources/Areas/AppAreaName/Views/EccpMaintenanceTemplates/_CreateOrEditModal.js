(function ($) {
    app.modals.CreateOrEditEccpMaintenanceTemplateModal = function () {

        var _eccpMaintenanceTemplatesService = abp.services.app.eccpMaintenanceTemplates;

        var _modalManager;
        var _$eccpMaintenanceTemplateInformationForm = null;

		        var _eccpDictMaintenanceTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplates/EccpDictMaintenanceTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplates/_EccpDictMaintenanceTypeLookupTableModal.js',
            modalClass: 'EccpDictMaintenanceTypeLookupTableModal'
        });        var _eccpDictElevatorTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplates/EccpDictElevatorTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplates/_EccpDictElevatorTypeLookupTableModal.js',
            modalClass: 'EccpDictElevatorTypeLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceTemplateInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTemplateInformationsForm]');
            _$eccpMaintenanceTemplateInformationForm.validate();
        };

		          $('#OpenEccpDictMaintenanceTypeLookupTableButton').click(function () {

            var eccpMaintenanceTemplate = _$eccpMaintenanceTemplateInformationForm.serializeFormToObject();

            _eccpDictMaintenanceTypeLookupTableModal.open({ id: eccpMaintenanceTemplate.maintenanceTypeId, displayName: eccpMaintenanceTemplate.eccpDictMaintenanceTypeName }, function (data) {
                _$eccpMaintenanceTemplateInformationForm.find('input[name=eccpDictMaintenanceTypeName]').val(data.displayName); 
                _$eccpMaintenanceTemplateInformationForm.find('input[name=maintenanceTypeId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictMaintenanceTypeNameButton').click(function () {
                _$eccpMaintenanceTemplateInformationForm.find('input[name=eccpDictMaintenanceTypeName]').val(''); 
                _$eccpMaintenanceTemplateInformationForm.find('input[name=maintenanceTypeId]').val(''); 
        });
		
        $('#OpenEccpDictElevatorTypeLookupTableButton').click(function () {

            var eccpMaintenanceTemplate = _$eccpMaintenanceTemplateInformationForm.serializeFormToObject();

            _eccpDictElevatorTypeLookupTableModal.open({ id: eccpMaintenanceTemplate.elevatorTypeId, displayName: eccpMaintenanceTemplate.eccpDictElevatorTypeName }, function (data) {
                _$eccpMaintenanceTemplateInformationForm.find('input[name=eccpDictElevatorTypeName]').val(data.displayName); 
                _$eccpMaintenanceTemplateInformationForm.find('input[name=elevatorTypeId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictElevatorTypeNameButton').click(function () {
                _$eccpMaintenanceTemplateInformationForm.find('input[name=eccpDictElevatorTypeName]').val(''); 
                _$eccpMaintenanceTemplateInformationForm.find('input[name=elevatorTypeId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceTemplateInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceTemplate = _$eccpMaintenanceTemplateInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceTemplatesService.createOrEdit(
				eccpMaintenanceTemplate
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceTemplateModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);