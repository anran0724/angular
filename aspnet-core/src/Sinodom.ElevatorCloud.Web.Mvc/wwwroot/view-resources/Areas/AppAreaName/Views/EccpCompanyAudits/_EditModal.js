(function ($) {
    app.modals.EditEccpCompanyAuditModal = function () {
        var _eccpCompanyAuditsService = abp.services.app.eccpCompanyAudits;

        var _modalManager;
        var _$eccpCompanyAuditInformationForm = null;

        this.init = function (modalManager) {

            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpCompanyAuditInformationForm = _modalManager.getModal().find('form[name=EccpCompanyInfoInformationsForm]');
            _$eccpCompanyAuditInformationForm.validate();
        };

        this.save = function () {

            if (!_$eccpCompanyAuditInformationForm.valid()) {
                return;
            }

            var eccpCompanyAudits = _$eccpCompanyAuditInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eccpCompanyAuditsService.editCompanyAudit(
                eccpCompanyAudits
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.editEccpCompanyAuditModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);