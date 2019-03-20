(function ($) {
    app.modals.CreateOrEditEccpBaseElevatorBrandModal = function () {

        var _eccpBaseElevatorBrandsService = abp.services.app.eccpBaseElevatorBrands;

        var _modalManager;
        var _$eccpBaseElevatorBrandInformationForm = null;

		        var _eCCPBaseProductionCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorBrands/ECCPBaseProductionCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorBrands/_ECCPBaseProductionCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseProductionCompanyLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpBaseElevatorBrandInformationForm = _modalManager.getModal().find('form[name=EccpBaseElevatorBrandInformationsForm]');
            _$eccpBaseElevatorBrandInformationForm.validate();
        };

		          $('#OpenECCPBaseProductionCompanyLookupTableButton').click(function () {

            var eccpBaseElevatorBrand = _$eccpBaseElevatorBrandInformationForm.serializeFormToObject();

            _eCCPBaseProductionCompanyLookupTableModal.open({ id: eccpBaseElevatorBrand.productionCompanyId, displayName: eccpBaseElevatorBrand.eCCPBaseProductionCompanyName }, function (data) {
                _$eccpBaseElevatorBrandInformationForm.find('input[name=eCCPBaseProductionCompanyName]').val(data.displayName); 
                _$eccpBaseElevatorBrandInformationForm.find('input[name=productionCompanyId]').val(data.id); 
            });
        });
		
		$('#ClearECCPBaseProductionCompanyNameButton').click(function () {
                _$eccpBaseElevatorBrandInformationForm.find('input[name=eCCPBaseProductionCompanyName]').val(''); 
                _$eccpBaseElevatorBrandInformationForm.find('input[name=productionCompanyId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpBaseElevatorBrandInformationForm.valid()) {
                return;
            }

            var eccpBaseElevatorBrand = _$eccpBaseElevatorBrandInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpBaseElevatorBrandsService.createOrEdit(
				eccpBaseElevatorBrand
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpBaseElevatorBrandModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);