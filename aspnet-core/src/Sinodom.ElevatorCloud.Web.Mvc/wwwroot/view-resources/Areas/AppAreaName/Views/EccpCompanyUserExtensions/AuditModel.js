(function ($) {
    app.modals.CreateOrEditUserModal = function () {

        var _userService = abp.services.app.eccpCompanyUserExtensionsAppServicer;

        var _modalManager;
        var _$userInformationForm = null;
        var _$userCompanyInfoForm = null;       
        var _organizationTree;       

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();             
            //_organizationTree = new OrganizationTree();
            //_organizationTree.init(_modalManager.getModal().find('.organization-tree'));     

            var userid = modal.find("input[name='Id']").val();

            $(".through-button").click(function () {
                _modalManager.setBusy(true);
                _userService.updateCompanyUserCheckState(userid,1,'审核通过').done(function () {
                    abp.notify.info(app.localize('AuditSuccessfully'));
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditUserModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
            });

            $(".noThrough-button").click(function () {
               
                var name = prompt("请输入原因", "");
                if (name) {
                    _modalManager.setBusy(true);
                    _userService.updateCompanyUserCheckState(userid, 2, name).done(function () {
                        abp.notify.info(app.localize('AuditSuccessfully'));
                        _modalManager.close();
                        abp.event.trigger('app.createOrEditUserModalSaved');
                    }).always(function () {
                        _modalManager.setBusy(false);
                    });
                }
            });

        };     
    };
})(jQuery);