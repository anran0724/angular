(function ($) {
    app.modals.CreateOrEditEccpBaseElevatorLabelModal = function () {

        var _eccpBaseElevatorLabelsService = abp.services.app.eccpBaseElevatorLabels;

        var _modalManager;
        var _$eccpBaseElevatorLabelInformationForm = null;

		        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabels/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabels/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });        var _eccpDictLabelStatusLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabels/EccpDictLabelStatusLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabels/_EccpDictLabelStatusLookupTableModal.js',
            modalClass: 'EccpDictLabelStatusLookupTableModal'
        });        var _userLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabels/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabels/_UserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eccpBaseElevatorLabelInformationForm = _modalManager.getModal().find('form[name=EccpBaseElevatorLabelInformationsForm]');
            _$eccpBaseElevatorLabelInformationForm.validate();
        };

		          $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var eccpBaseElevatorLabel = _$eccpBaseElevatorLabelInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpBaseElevatorLabel.elevatorId, displayName: eccpBaseElevatorLabel.eccpBaseElevatorName }, function (data) {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=eccpBaseElevatorName]').val(data.displayName); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=elevatorId]').val(data.id); 
            });
        });
		
		$('#ClearEccpBaseElevatorNameButton').click(function () {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=eccpBaseElevatorName]').val(''); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=elevatorId]').val(''); 
        });
		
        $('#OpenEccpDictLabelStatusLookupTableButton').click(function () {

            var eccpBaseElevatorLabel = _$eccpBaseElevatorLabelInformationForm.serializeFormToObject();

            _eccpDictLabelStatusLookupTableModal.open({ id: eccpBaseElevatorLabel.labelStatusId, displayName: eccpBaseElevatorLabel.eccpDictLabelStatusName }, function (data) {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=eccpDictLabelStatusName]').val(data.displayName); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=labelStatusId]').val(data.id); 
            });
        });
		
		$('#ClearEccpDictLabelStatusNameButton').click(function () {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=eccpDictLabelStatusName]').val(''); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=labelStatusId]').val(''); 
        });
		
        $('#OpenUserLookupTableButton').click(function () {

            var eccpBaseElevatorLabel = _$eccpBaseElevatorLabelInformationForm.serializeFormToObject();

            _userLookupTableModal.open({ id: eccpBaseElevatorLabel.userId, displayName: eccpBaseElevatorLabel.userName }, function (data) {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=userName]').val(data.displayName); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$eccpBaseElevatorLabelInformationForm.find('input[name=userName]').val(''); 
                _$eccpBaseElevatorLabelInformationForm.find('input[name=userId]').val(''); 
        });
		


        this.save = function () {
            if (!_$eccpBaseElevatorLabelInformationForm.valid()) {
                return;
            }

            var eccpBaseElevatorLabel = _$eccpBaseElevatorLabelInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpBaseElevatorLabelsService.createOrEdit(
				eccpBaseElevatorLabel
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpBaseElevatorLabelModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);