(function ($) {
    app.modals.CreateOrEditUserModal = function () {

        var _userService = abp.services.app.user;

        var _modalManager;
        var _$userInformationForm = null;
        var _$userCompanyInfoForm = null;
        var _passwordComplexityHelper = new app.PasswordComplexityHelper();
        var _organizationTree;

        function _findAssignedRoleNames() {
            var assignedRoleNames = [];

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .each(function () {
                    if ($(this).is(':checked')) {
                        assignedRoleNames.push($(this).attr('name'));
                    }
                });

            return assignedRoleNames;
        }

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });
            var pictureName = "";
            var pictureId = "";
            var pictureImg = "";
            $('#UserCompanyInformationsForm input[name=SignPicture]').change(function () {
                pictureName = "SignPicture";
                pictureId = "SignPictureId";
                pictureImg = "SignPictureImg";
                console.log(pictureImg);
                $('#UserCompanyInformationsForm').submit();
            });
            $('#UserCompanyInformationsForm input[name=CertificateBackPicture]').change(function () {
                pictureName = "CertificateBackPicture";
                pictureId = "CertificateBackPictureId";
                pictureImg = "CertificateBackPictureImg";
                $('#UserCompanyInformationsForm').submit();
            });
            $('#UserCompanyInformationsForm input[name=CertificateFrontPicture]').change(function () {
                pictureName = "CertificateFrontPicture";
                pictureId = "CertificateFrontPictureId";
                pictureImg = "CertificateFrontPictureImg";
                $('#UserCompanyInformationsForm').submit();
            });

            $('#UserCompanyInformationsForm').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {
                    var $fileInput = $('#UserCompanyInformationsForm input[name=' + pictureName + ']');
                    var files = $fileInput.get()[0].files;

                    if (!files.length) {
                        return false;
                    }
                    console.log(files.length);
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

                    var mimeType = _.filter(formData, { name: pictureName })[0].value.type;

                    formData.push({ name: 'FileType', value: mimeType });
                    formData.push({ name: 'FileName', value: pictureName });
                    formData.push({ name: 'FileToken', value: app.guid() });

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        var $contractPictureIdResize = $('#' + pictureImg + '');

                        var profileFilePath = abp.appPath + 'File/DownloadTempFile?fileToken=' + response.result.fileToken + '&fileName=' + response.result.fileName + '&fileType=' + response.result.fileType + '&v=' + new Date().valueOf();
                        $("input[name='" + pictureId + "']").val(response.result.fileToken);

                        $contractPictureIdResize.show();
                        $contractPictureIdResize.attr('src', profileFilePath);

                        $contractPictureIdResize.attr('originalWidth', response.result.width);
                        $contractPictureIdResize.attr('originalHeight', response.result.height);

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });


            _organizationTree = new OrganizationTree();
            _organizationTree.init(_modalManager.getModal().find('.organization-tree'));

            _$userInformationForm = _modalManager.getModal().find('form[name=UserInformationsForm]');
            _$userInformationForm.validate();

            _$userCompanyInfoForm = _modalManager.getModal().find('form[name=UserCompanyInformationsForm]');
           // _$userCompanyInfoForm.validate();

            var passwordInputs = _modalManager.getModal().find('input[name=Password],input[name=PasswordRepeat]');
            var passwordInputGroups = passwordInputs.closest('.form-group');

            _passwordComplexityHelper.setPasswordComplexityRules(passwordInputs, window.passwordComplexitySetting);

            $('#EditUser_SetRandomPassword').change(function () {
                if ($(this).is(':checked')) {
                    passwordInputGroups.slideUp('fast');
                    if (!_modalManager.getArgs().id) {
                        passwordInputs.removeAttr('required');
                    }
                } else {
                    passwordInputGroups.slideDown('fast');
                    if (!_modalManager.getArgs().id) {
                        passwordInputs.attr('required', 'required');
                    }
                }
            });       

            _modalManager.getModal()
                .find('.user-role-checkbox-list input[type=checkbox]')
                .change(function () {
                    $('#assigned-role-count').text(_findAssignedRoleNames().length);
                });

            _modalManager.getModal().find('[data-toggle=tooltip]').tooltip();           
           

        };

        this.save = function () {
            if (!_$userInformationForm.valid()) {
                return;
            }
            var assignedRoleNames = _findAssignedRoleNames();
            var user = _$userInformationForm.serializeFormToObject();
            var companyUser = _$userCompanyInfoForm.serializeFormToObject();

            if (user.SetRandomPassword) {
                user.Password = null;
            }

            _modalManager.setBusy(true);
            _userService.createOrUpdateUser({
                user: user,
                companyUser: companyUser,
                assignedRoleNames: assignedRoleNames,
                sendActivationEmail: user.SendActivationEmail,
                SetRandomPassword: user.SetRandomPassword,
                SignPictureToken: companyUser.SignPictureId,
                CertificateBackPictureToken: companyUser.CertificateBackPictureId,
                CertificateFrontPictureToken: companyUser.CertificateFrontPictureId,
                organizationUnits: _organizationTree.getSelectedOrganizations()
            }).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditUserModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);