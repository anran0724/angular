(function ($) {
    app.modals.CreateOrEditEccpMaintenanceTroubledWorkOrderModal = function () {

        var _eccpMaintenanceTroubledWorkOrdersService = abp.services.app.eccpMaintenanceTroubledWorkOrders;

        var _modalManager;
        var _$eccpMaintenanceTroubledWorkOrderInformationForm = null;

		        var _eccpMaintenanceWorkOrderLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTroubledWorkOrders/EccpMaintenanceWorkOrderLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTroubledWorkOrders/_EccpMaintenanceWorkOrderLookupTableModal.js',
            modalClass: 'EccpMaintenanceWorkOrderLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceTroubledWorkOrderInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceTroubledWorkOrderInformationsForm]');
            _$eccpMaintenanceTroubledWorkOrderInformationForm.validate();
        };

		          $('#OpenEccpMaintenanceWorkOrderLookupTableButton').click(function () {

            var eccpMaintenanceTroubledWorkOrder = _$eccpMaintenanceTroubledWorkOrderInformationForm.serializeFormToObject();

            _eccpMaintenanceWorkOrderLookupTableModal.open({ id: eccpMaintenanceTroubledWorkOrder.maintenanceWorkOrderId, displayName: eccpMaintenanceTroubledWorkOrder.eccpMaintenanceWorkOrderRemark }, function (data) {
                _$eccpMaintenanceTroubledWorkOrderInformationForm.find('input[name=eccpMaintenanceWorkOrderRemark]').val(data.displayName); 
                _$eccpMaintenanceTroubledWorkOrderInformationForm.find('input[name=maintenanceWorkOrderId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceWorkOrderRemarkButton').click(function () {
                _$eccpMaintenanceTroubledWorkOrderInformationForm.find('input[name=eccpMaintenanceWorkOrderRemark]').val(''); 
                _$eccpMaintenanceTroubledWorkOrderInformationForm.find('input[name=maintenanceWorkOrderId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceTroubledWorkOrderInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceTroubledWorkOrder = _$eccpMaintenanceTroubledWorkOrderInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceTroubledWorkOrdersService.createOrEdit(
				eccpMaintenanceTroubledWorkOrder
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceTroubledWorkOrderModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);