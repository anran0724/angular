(function ($) {
    app.modals.CreateOrEditEccpAppVersionModal = function () {

        var _eccpAppVersionsService = abp.services.app.eccpAppVersions;

        var _modalManager;
        var _$eccpAppVersionInformationForm = null;
        var uploadedFileToken = null;
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            $('#EccpAppVersionInformationsForm input[name=DownloadUrl]').change(function () {
                $('#EccpAppVersionInformationsForm').submit();
            });

            $('#EccpAppVersionInformationsForm').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {
                    var $fileInput = $('#EccpAppVersionInformationsForm input[name=DownloadUrl]');
                    var files = $fileInput.get()[0].files;

                    if (!files.length) {
                        return false;
                    }

                    var file = files[0];
                    //File type check
                    var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                    if ('|vnd.android.package-archive|'.indexOf(type) === -1) {
                        abp.message.warn(app.localize('ContractPictureId_Warn_FileType'));
                        return false;
                    }
                    

                    var mimeType = _.filter(formData, { name: 'DownloadUrl' })[0].value.type;

                    formData.push({ name: 'FileType', value: mimeType });
                    formData.push({ name: 'FileName', value: 'DownloadUrl' });
                    formData.push({ name: 'FileToken', value: app.guid() });

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        var $showResult = $('#showResult');
                        uploadedFileToken = response.result.fileToken;
                        $showResult.show();
                        $showResult.text(app.localize('UploadSuccess'));

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });

            if ($('[name=_downloadUrl]').val() == "") {
                $('#showResult').hide();
            }

            _$eccpAppVersionInformationForm = _modalManager.getModal().find('#EccpAppVersionInformationsForm');
            _$eccpAppVersionInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$eccpAppVersionInformationForm.valid()) {
                return;
            }
            
            var downloadUrl = $('[name=_downloadUrl]').val();
            if (uploadedFileToken) {

                $("[name=fileToken]").val(uploadedFileToken);
            } else {
                if (downloadUrl === "") {
                    return;
                }
                $("[name=fileToken]").val(downloadUrl);
            }

            var eccpAppVersion = _$eccpAppVersionInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eccpAppVersionsService.createOrEdit(
				eccpAppVersion
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEccpAppVersionModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);