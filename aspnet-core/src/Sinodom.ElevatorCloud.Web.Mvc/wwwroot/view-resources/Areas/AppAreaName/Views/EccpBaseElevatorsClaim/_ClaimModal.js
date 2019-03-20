(function ($) {
    app.modals.ClaimModal = function () {
        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;

        var _modalManager;
        var _$eccpBaseElevatorsClaimForm = null;

        var _eCCPBaseMaintenanceCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseMaintenanceCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseMaintenanceCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseMaintenanceCompanyLookupTableModal'
        });
        var _eCCPBasePropertyCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContracts/ECCPBasePropertyCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContracts/_ECCPBasePropertyCompanyLookupTableModal.js',
            modalClass: 'ECCPBasePropertyCompanyLookupTableModal'
        });

        var _args;

        this.init = function (modalManager, args) {           
            _modalManager = modalManager;
            _args = args;
            _$eccpBaseElevatorsClaimForm = _modalManager.getModal().find('form[name=EccpBaseElevatorsClaimForm]');
        };
        $('#OpenECCPBaseMaintenanceCompanyLookupTableButton').click(function () {

            var eccpBaseElevator = _$eccpBaseElevatorsClaimForm.serializeFormToObject();
            _eCCPBaseMaintenanceCompanyLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseMaintenanceCompanyId, displayName: eccpBaseElevator.eCCPBaseMaintenanceCompanyName }, function (data) {
                _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val(data.displayName);
                _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBaseMaintenanceCompanyId]').val(data.id);
            });
        });
        $('#ClearECCPBaseMaintenanceCompanyNameButton').click(function () {
            _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val('');
            _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBaseMaintenanceCompanyId]').val('');
        });

        $('#OpenECCPBasePropertyCompanyLookupTableButton').click(function () {

            var eccpMaintenanceContract = _$eccpBaseElevatorsClaimForm.serializeFormToObject();

            _eCCPBasePropertyCompanyLookupTableModal.open({ id: eccpMaintenanceContract.propertyCompanyId, displayName: eccpMaintenanceContract.eCCPBasePropertyCompanyName }, function (data) {
                _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBasePropertyCompanyName]').val(data.displayName);
                _$eccpBaseElevatorsClaimForm.find('input[name=propertyCompanyId]').val(data.id);
            });
        });
        $('#ClearECCPBasePropertyCompanyNameButton').click(function () {
            _$eccpBaseElevatorsClaimForm.find('input[name=eCCPBasePropertyCompanyName]').val('');
            _$eccpBaseElevatorsClaimForm.find('input[name=propertyCompanyId]').val('');
        });
        this.save = function () {


            var eccpBaseElevator = _$eccpBaseElevatorsClaimForm.serializeFormToObject();
            //if (eccpBaseElevator.eCCPBaseMaintenanceCompanyName == "") {
            //    abp.message.error("请选择维保单位");
            //    return;
            //}
            //if (eccpBaseElevator.eCCPBasePropertyCompanyName == "") {
            //    abp.message.error("请选择物业单位");
            //    return;
            //}
            _modalManager.setBusy(true);
            //var idsArray = [];
            //idsArray.push();           

            _eccpBaseElevatorsService.claimElevators(_args.id, eccpBaseElevator.eCCPBaseMaintenanceCompanyId, eccpBaseElevator.propertyCompanyId).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.setResult(false);
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpBaseElevatorModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);