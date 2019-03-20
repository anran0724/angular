(function ($) {
    app.modals.CreateOrEditEccpMaintenanceWorkOrderEvaluationModal = function () {

        var _eccpMaintenanceWorkOrderEvaluationsService = abp.services.app.eccpMaintenanceWorkOrderEvaluations;

        var _modalManager;
        var _$eccpMaintenanceWorkOrderEvaluationInformationForm = null;

		        var _eccpMaintenanceWorkOrderLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrderEvaluations/EccpMaintenanceWorkOrderLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrderEvaluations/_EccpMaintenanceWorkOrderLookupTableModal.js',
            modalClass: 'EccpMaintenanceWorkOrderLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpMaintenanceWorkOrderEvaluationInformationForm = _modalManager.getModal().find('form[name=EccpMaintenanceWorkOrderEvaluationInformationsForm]');
            _$eccpMaintenanceWorkOrderEvaluationInformationForm.validate();
        };

		          $('#OpenEccpMaintenanceWorkOrderLookupTableButton').click(function () {

            var eccpMaintenanceWorkOrderEvaluation = _$eccpMaintenanceWorkOrderEvaluationInformationForm.serializeFormToObject();

            _eccpMaintenanceWorkOrderLookupTableModal.open({ id: eccpMaintenanceWorkOrderEvaluation.workOrderId, displayName: eccpMaintenanceWorkOrderEvaluation.eccpMaintenanceWorkOrderRemark }, function (data) {
                _$eccpMaintenanceWorkOrderEvaluationInformationForm.find('input[name=eccpMaintenanceWorkOrderRemark]').val(data.displayName); 
                _$eccpMaintenanceWorkOrderEvaluationInformationForm.find('input[name=workOrderId]').val(data.id); 
            });
        });
		
		$('#ClearEccpMaintenanceWorkOrderRemarkButton').click(function () {
                _$eccpMaintenanceWorkOrderEvaluationInformationForm.find('input[name=eccpMaintenanceWorkOrderRemark]').val(''); 
                _$eccpMaintenanceWorkOrderEvaluationInformationForm.find('input[name=workOrderId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpMaintenanceWorkOrderEvaluationInformationForm.valid()) {
                return;
            }

            var eccpMaintenanceWorkOrderEvaluation = _$eccpMaintenanceWorkOrderEvaluationInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpMaintenanceWorkOrderEvaluationsService.createOrEdit(
				eccpMaintenanceWorkOrderEvaluation
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpMaintenanceWorkOrderEvaluationModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);