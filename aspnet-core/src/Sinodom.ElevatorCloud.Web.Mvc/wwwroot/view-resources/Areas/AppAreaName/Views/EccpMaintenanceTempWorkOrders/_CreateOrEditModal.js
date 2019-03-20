(function ($) {
    app.modals.CreateOrEditEccpMaintenanceTempWorkOrderModal = function () {

        var _eccpMaintenanceTempWorkOrdersService = abp.services.app.eccpMaintenanceTempWorkOrders;

        var _modalManager;
        var _$eccpMaintenanceTempWorkOrderInformationForm = null;

        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrders/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrders/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });
		     
        var _userLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrders/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrders/_UserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });

        var _eccpDictTempWorkOrderTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrders/ECCPDictTempWorkOrderTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrders/_ECCPDictTempWorkOrderTypeLookupTableModel.js',
            modalClass: 'ECCPDictTempWorkOrderTypeLookupTableModal'
        });


        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceTempWorkOrderInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTempWorkOrderInformationsForm]');
            _$eccpMaintenanceTempWorkOrderInformationForm.validate();
        };

        $('#OpenEccpDictTempWorkOrderTypLookupTableButton').click(function () {
            var eccpMaintenanceTempWorkOrder = _$eccpMaintenanceTempWorkOrderInformationForm.serializeFormToObject();
            _eccpDictTempWorkOrderTypeLookupTableModal.open({ id: eccpMaintenanceTempWorkOrder.TempWorkOrderTypeId, displayName: eccpMaintenanceTempWorkOrder.EccpDictTempWorkOrderTypeName }, function (data) {
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=EccpDictTempWorkOrderTypeName]').val(data.displayName);
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=TempWorkOrderTypeId]').val(data.id);
            });
        });

        $('#ClearEccpDictTempWorkOrderTypNameButton').click(function () {
            _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=EccpDictTempWorkOrderTypeName]').val(''); 
            _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=TempWorkOrderTypeId]').val(''); 
        });
		
        $('#OpenUserLookupTableButton').click(function () {

            var eccpMaintenanceTempWorkOrder = _$eccpMaintenanceTempWorkOrderInformationForm.serializeFormToObject();

            _userLookupTableModal.open({ id: eccpMaintenanceTempWorkOrder.userId, displayName: eccpMaintenanceTempWorkOrder.userName }, function (data) {
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=userName]').val(data.displayName); 
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=userName]').val(''); 
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=userId]').val(''); 
        });
		
        $('#OpenEccpBaseElevatorLookupTableButton').click(function () {
            var eccpMaintenanceTempWorkOrder = _$eccpMaintenanceTempWorkOrderInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpMaintenanceTempWorkOrder.elevatorId, displayName: eccpMaintenanceTempWorkOrder.elevatorName}, function (data) {
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=elevatorName]').val(data.displayName);
                _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=elevatorId]').val(data.id);
            });
        });

        $('#ClearEccpBaseElevatorNameButton').click(function () {
            _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=elevatorName]').val('');
            _$eccpMaintenanceTempWorkOrderInformationForm.find('input[name=elevatorId]').val('');
        });

        this.save = function () {
            if (!_$eccpMaintenanceTempWorkOrderInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceTempWorkOrder = _$eccpMaintenanceTempWorkOrderInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceTempWorkOrdersService.createOrEdit(
				eccpMaintenanceTempWorkOrder
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceTempWorkOrderModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);