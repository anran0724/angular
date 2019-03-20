(function ($) {
    app.modals.RecoveryContractEccpMaintenanceContractModal = function () {

        var _eccpMaintenanceContractsStopService = abp.services.app.eccpMaintenanceContractsStop;

        var _modalManager;
        var _$eccpMaintenanceContractInformationForm = null;

        var uploadedFileToken = null;

        var _eCCPBaseMaintenanceCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContractsStop/ECCPBaseMaintenanceCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContractsStop/_ECCPBaseMaintenanceCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseMaintenanceCompanyLookupTableModal'
        });
        var _eCCPBasePropertyCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContractsStop/ECCPBasePropertyCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContractsStop/_ECCPBasePropertyCompanyLookupTableModal.js',
            modalClass: 'ECCPBasePropertyCompanyLookupTableModal'
        });
        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContractsStop/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContractsStop/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });



            $('#EccpMaintenanceContractInformationsForm input[name=ContractPictureId]').change(function () {
                $('#EccpMaintenanceContractInformationsForm').submit();
            });

            $('#EccpMaintenanceContractInformationsForm').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {
                    var $fileInput = $('#EccpMaintenanceContractInformationsForm input[name=ContractPictureId]');
                    var files = $fileInput.get()[0].files;

                    if (!files.length) {
                        return false;
                    }

                    var file = files[0];

                    //File type check
                    var type = '|' + file.type.slice(file.type.lastIndexOf('/') + 1) + '|';
                    if ('|jpg|jpeg|png|gif|'.indexOf(type) === -1) {
                        abp.message.warn(app.localize('ContractPictureId_Warn_FileType'));
                        return false;
                    }

                    //File size check
                    if (file.size > 5242880) //5MB
                    {
                        abp.message.warn(app.localize('ContractPictureId_Warn_SizeLimit', app.maxProfilPictureBytesUserFriendlyValue));
                        return false;
                    }

                    var mimeType = _.filter(formData, { name: 'ContractPictureId' })[0].value.type;

                    formData.push({ name: 'FileType', value: mimeType });
                    formData.push({ name: 'FileName', value: 'ContractPictureId' });
                    formData.push({ name: 'FileToken', value: app.guid() });

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        var $contractPictureIdResize = $('#ContractPictureIdResize');

                        var profileFilePath = abp.appPath + 'File/DownloadTempFile?fileToken=' + response.result.fileToken + '&fileName=' + response.result.fileName + '&fileType=' + response.result.fileType + '&v=' + new Date().valueOf();
                        uploadedFileToken = response.result.fileToken;

                        $contractPictureIdResize.show();
                        $contractPictureIdResize.attr('src', profileFilePath);
                        $contractPictureIdResize.attr('originalWidth', response.result.width);
                        $contractPictureIdResize.attr('originalHeight', response.result.height);

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });

            if ($('[name=_contractPictureId]').val() == "") {
                $('#ContractPictureIdResize').hide();
            }

            _$eccpMaintenanceContractInformationForm = _modalManager.getModal().find('#EccpMaintenanceContractInformationsForm');
            _$eccpMaintenanceContractInformationForm.validate();
        };

        $('#OpenECCPBaseMaintenanceCompanyLookupTableButton').click(function () {

            var eccpMaintenanceContract = _$eccpMaintenanceContractInformationForm.serializeFormToObject();

            _eCCPBaseMaintenanceCompanyLookupTableModal.open({ id: eccpMaintenanceContract.maintenanceCompanyId, displayName: eccpMaintenanceContract.eCCPBaseMaintenanceCompanyName }, function (data) {
                _$eccpMaintenanceContractInformationForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val(data.displayName);
                _$eccpMaintenanceContractInformationForm.find('input[name=maintenanceCompanyId]').val(data.id);
            });
        });

        $('#ClearECCPBaseMaintenanceCompanyNameButton').click(function () {
            _$eccpMaintenanceContractInformationForm.find('input[name=eCCPBaseMaintenanceCompanyName]').val('');
            _$eccpMaintenanceContractInformationForm.find('input[name=maintenanceCompanyId]').val('');
        });

        $('#OpenECCPBasePropertyCompanyLookupTableButton').click(function () {

            var eccpMaintenanceContract = _$eccpMaintenanceContractInformationForm.serializeFormToObject();

            _eCCPBasePropertyCompanyLookupTableModal.open({ id: eccpMaintenanceContract.propertyCompanyId, displayName: eccpMaintenanceContract.eCCPBasePropertyCompanyName }, function (data) {
                _$eccpMaintenanceContractInformationForm.find('input[name=eCCPBasePropertyCompanyName]').val(data.displayName);
                _$eccpMaintenanceContractInformationForm.find('input[name=propertyCompanyId]').val(data.id);
            });
        });

        $('#ClearECCPBasePropertyCompanyNameButton').click(function () {
            _$eccpMaintenanceContractInformationForm.find('input[name=eCCPBasePropertyCompanyName]').val('');
            _$eccpMaintenanceContractInformationForm.find('input[name=propertyCompanyId]').val('');
        });


        $('#OpenEccpBaseElevatorsLookupTableButton').click(function () {
            var eccpMaintenanceContract = _$eccpMaintenanceContractInformationForm.serializeFormToObject();

            _eccpBaseElevatorLookupTableModal.open({ id: eccpMaintenanceContract.eccpBaseElevatorsIds, displayName: eccpMaintenanceContract.eccpBaseElevatorsNames, eccpBaseElevatorsIds: $('[name=eccpBaseElevatorsIds]').val(), eccpBaseElevatorsNames: $('[name=eccpBaseElevatorsNames]').val(), maintenanceContractId: $('[name=id]').val() }, function (data) {
                _$eccpMaintenanceContractInformationForm.find('input[name=eccpBaseElevatorsNames]').val(data.displayName);
                _$eccpMaintenanceContractInformationForm.find('input[name=eccpBaseElevatorsIds]').val(data.id);
            });
        });

        $('#ClearEccpBaseElevatorsNameButton').click(function () {
            _$eccpMaintenanceContractInformationForm.find('input[name=eccpBaseElevatorsNames]').val('');
            _$eccpMaintenanceContractInformationForm.find('input[name=eccpBaseElevatorsIds]').val('');
        });


        this.save = function () {
            var form = $('#EccpMaintenanceContractInformationsForm');
            form.find('input').parent().removeClass('has-danger');

            if (!_$eccpMaintenanceContractInformationForm.valid()) {
                return;
            }
            if (form.find('input[name=eccpBaseElevatorsNames]').val() === '') {
                form.find('input[name=eccpBaseElevatorsNames]').parent().addClass('has-danger');
                return;
            }
            if (form.find('input[name=eCCPBaseMaintenanceCompanyName]').val() === '') {
                form.find('input[name=eCCPBaseMaintenanceCompanyName]').parent().addClass('has-danger');
                return;
            }
            if (form.find('input[name=eCCPBasePropertyCompanyName]').val() === '') {
                form.find('input[name=eCCPBasePropertyCompanyName]').parent().addClass('has-danger');
                return;
            }
            var contractPictureId = $('[name=_contractPictureId]').val();
            if (uploadedFileToken) {

                $("[name=fileToken]").val(uploadedFileToken);
            } else {
                if (contractPictureId === "") {
                    form.find('input[name=ContractPictureId]').parent().addClass('has-danger');
                    return;
                }
                $("[name=fileToken]").val(contractPictureId);
            }

            var eccpMaintenanceContract = _$eccpMaintenanceContractInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eccpMaintenanceContractsStopService.recoveryContract(
                eccpMaintenanceContract
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.recoveryContractEccpMaintenanceContractModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);