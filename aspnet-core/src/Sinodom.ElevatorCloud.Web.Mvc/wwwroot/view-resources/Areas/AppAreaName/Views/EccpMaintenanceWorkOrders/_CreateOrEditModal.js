(function ($) {
    app.modals.CreateOrEditEccpMaintenanceWorkOrderModal = function () {

        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;

        var _modalManager;
        var _$eccpMaintenanceWorkOrderInformationForm = null;

		        var _eccpMaintenancePlanLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/EccpMaintenancePlanLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrders/_EccpMaintenancePlanLookupTableModal.js',
            modalClass: 'EccpMaintenancePlanLookupTableModal'
        });        var _eccpDictMaintenanceTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/EccpDictMaintenanceTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrders/_EccpDictMaintenanceTypeLookupTableModal.js',
            modalClass: 'EccpDictMaintenanceTypeLookupTableModal'
        });        var _eccpDictMaintenanceStatusLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrders/EccpDictMaintenanceStatusLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrders/_EccpDictMaintenanceStatusLookupTableModal.js',
            modalClass: 'EccpDictMaintenanceStatusLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });

            _$eccpMaintenanceWorkOrderInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceWorkOrderInformationsForm]');
            _$eccpMaintenanceWorkOrderInformationForm.validate();
        };

		          $('#OpenEccpMaintenancePlanLookupTableButton').click(function () {

            var eccpMaintenanceWorkOrder = _$eccpMaintenanceWorkOrderInformationForm.serializeFormToObject();

            _eccpMaintenancePlanLookupTableModal.open({ id: eccpMaintenanceWorkOrder.maintenancePlanId, displayName: eccpMaintenanceWorkOrder.eccpMaintenancePlanPollingPeriod }, function (data) {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpMaintenancePlanPollingPeriod]').val(data.displayName); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenancePlanId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenancePlanPollingPeriodButton').click(function () {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpMaintenancePlanPollingPeriod]').val(''); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenancePlanId]').val(''); 
        });
		
        $('#OpenEccpDictMaintenanceTypeLookupTableButton').click(function () {

            var eccpMaintenanceWorkOrder = _$eccpMaintenanceWorkOrderInformationForm.serializeFormToObject();

            _eccpDictMaintenanceTypeLookupTableModal.open({ id: eccpMaintenanceWorkOrder.maintenanceTypeId, displayName: eccpMaintenanceWorkOrder.eccpDictMaintenanceTypeName }, function (data) {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpDictMaintenanceTypeName]').val(data.displayName); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenanceTypeId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictMaintenanceTypeNameButton').click(function () {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpDictMaintenanceTypeName]').val(''); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenanceTypeId]').val(''); 
        });
		
        $('#OpenEccpDictMaintenanceStatusLookupTableButton').click(function () {

            var eccpMaintenanceWorkOrder = _$eccpMaintenanceWorkOrderInformationForm.serializeFormToObject();

            _eccpDictMaintenanceStatusLookupTableModal.open({ id: eccpMaintenanceWorkOrder.maintenanceStatusId, displayName: eccpMaintenanceWorkOrder.eccpDictMaintenanceStatusName }, function (data) {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpDictMaintenanceStatusName]').val(data.displayName); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenanceStatusId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictMaintenanceStatusNameButton').click(function () {
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=eccpDictMaintenanceStatusName]').val(''); 
                _$eccpMaintenanceWorkOrderInformationForm.find('input[name=maintenanceStatusId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceWorkOrderInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceWorkOrder = _$eccpMaintenanceWorkOrderInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceWorkOrdersService.createOrEdit(
				eccpMaintenanceWorkOrder
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceWorkOrderModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);