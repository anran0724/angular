(function ($) {
    app.modals.CreateOrEditECCPBaseMaintenanceCompanyModal = function () {
        var self = this;
        var _eCCPBaseMaintenanceCompaniesService = abp.services.app.eCCPBaseMaintenanceCompanies;

        var _modalManager;
        var _$eCCPBaseMaintenanceCompanyInformationForm = null;
        var uploadedFileToken = null;

        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseMaintenanceCompanies/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseMaintenanceCompanies/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });
        
        var _passwordComplexityHelper = new app.PasswordComplexityHelper();

        this.init = function (modalManager) {
            handleRegister();
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseMaintenanceCompanyInformationForm = _modalManager.getModal().find('form[name=ECCPBaseMaintenanceCompanyInformationsForm]');
            _$eCCPBaseMaintenanceCompanyInformationForm.validate();

            handleUploadPicture('businessLicenseForm', 'BusinessLicenseId');
            handleUploadPicture('aptitudePhotoForm', 'AptitudePhotoId');

            if ($('[name=_BusinessLicenseId]').val() == "") {
                $('#BusinessLicenseIdResize').hide();
            }

            if ($('[name=_AptitudePhotoId]').val() == "") {
                $('#AptitudePhotoIdResize').hide();
            }

            $('[name=BusinessLicenseIdFileToken]').val(null);
            $('[name=AptitudePhotoIdFileToken]').val(null);
        };

        var handleRegister = function () {            
           
            $.validator.addMethod(
                "regex",
                function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                },
                app.localize('TenantName_Regex_Description'));

            $.validator.addMethod(
                "isMobile",
                function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                },
                app.localize('Mobile_Regex_Description'));

            $('.register-form').validate({
                errorElement: 'div',
                errorClass: 'form-control-feedback',
                focusInvalid: false, 
                ignore: ':hidden',
                rules: {
                    AdminPasswordRepeat: {
                        equalTo: "#AdminPassword"
                    },
                    TenancyName: {
                        required: true,
                        regex: '^[a-zA-Z][a-zA-Z0-9_-]{1,}$'
                    },
                    Mobile: {
                        isMobile: '^(13[0-9]{9})|(18[0-9]{9})|(14[0-9]{9})|(17[0-9]{9})|(15[0-9]{9})$'
                    }
                },
                messages: {
                    AdminPasswordRepeat: {
                        equalTo: app.localize('PasswordRepeatError')
                    }
                },
                invalidHandler: function (event, validator) {

                },
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-danger');
                },
                success: function (label) {
                    label.closest('.form-group').removeClass('has-danger');
                    label.remove();
                },
                errorPlacement: function (error, element) {
                    if (element.closest('.input-icon').length === 1) {
                        error.insertAfter(element.closest('.input-icon'));
                    } else {
                        error.insertAfter(element);
                    }
                },
                submitHandler: function (form) {
                    form.submit();
                }
            });

            var passwordComplexitySetting =
            {
                RequireDigit: false,
                RequireLowercase: false,
                RequireNonAlphanumeric: false,
                RequireUppercase: false,
                RequiredLength: 3
            };

            _passwordComplexityHelper.setPasswordComplexityRules(
                $("input[name=AdminPassword],input[name=AdminPasswordRepeat]"),
                passwordComplexitySetting);
        };

        var handleUploadPicture = function (formName, inputName) {

            $('#' + formName + ' input[name=' + inputName + ']').change(function () {
                $('#' + formName + '').submit();
            });

            $('#' + formName + '').ajaxForm({
                beforeSubmit: function (formData, jqForm, options) {
                    var $fileInput = $('#' + formName + ' input[name=' + inputName + ']');
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
                        abp.message.warn(app.localize('ContractPictureId_Warn_SizeLimit',
                            app.maxProfilPictureBytesUserFriendlyValue));
                        return false;
                    }

                    var mimeType = _.filter(formData, { name: '' + inputName + '' })[0].value.type;

                    formData.push({ name: 'FileType', value: mimeType });
                    formData.push({ name: 'FileName', value: '' + inputName + '' });
                    formData.push({ name: 'FileToken', value: app.guid() });

                    return true;
                },
                success: function (response) {
                    if (response.success) {
                        var $contractPictureIdResize = $('#' + inputName + 'Resize');

                        var profileFilePath = abp.appPath +
                            'File/DownloadTempFile?fileToken=' +
                            response.result.fileToken +
                            '&fileName=' +
                            response.result.fileName +
                            '&fileType=' +
                            response.result.fileType +
                            '&v=' +
                            new Date().valueOf();
                        uploadedFileToken = response.result.fileToken;

                        $contractPictureIdResize.show();
                        $contractPictureIdResize.attr('src', profileFilePath);
                        $contractPictureIdResize.attr('originalWidth', response.result.width);
                        $contractPictureIdResize.attr('originalHeight', response.result.height);

                        $('[name=' + inputName + 'FileToken]').val(uploadedFileToken);

                        if ($('#' + inputName + 'Resize').prev().hasClass('has-danger')) {
                            $('#' + inputName + 'Resize').prev().removeClass('has-danger').find('.form-control-feedback').remove();
                        }

                    } else {
                        abp.message.error(response.error.message);
                    }
                }
            });

        };

        

        $('#OpenProvinceNameLookupTableButton').click(function () {

            var eCCPBaseMaintenanceCompany = _$eCCPBaseMaintenanceCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseMaintenanceCompany.provinceId, displayName: eCCPBaseMaintenanceCompany.provinceName, parentId: 0 }, function (data) {
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=provinceName]').val(data.displayName);
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=provinceId]').val(data.id);

                if (data.id !== eCCPBaseMaintenanceCompany.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);

            });
        });

        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=provinceName]').val('');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=provinceId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenCityNameLookupTableButton').click(function () {

            var eCCPBaseMaintenanceCompany = _$eCCPBaseMaintenanceCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseMaintenanceCompany.cityId, displayName: eCCPBaseMaintenanceCompany.cityName, parentId: eCCPBaseMaintenanceCompany.provinceId }, function (data) {
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=cityName]').val(data.displayName);
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=cityId]').val(data.id);

                if (data.id !== eCCPBaseMaintenanceCompany.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);

            });
        });

        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=cityName]').val('');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=cityId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eCCPBaseMaintenanceCompany = _$eCCPBaseMaintenanceCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseMaintenanceCompany.districtId, displayName: eCCPBaseMaintenanceCompany.districtName, parentId: eCCPBaseMaintenanceCompany.cityId }, function (data) {
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=districtName]').val(data.displayName);
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=districtId]').val(data.id);

                if (data.id !== eCCPBaseMaintenanceCompany.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=districtName]').val('');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=districtId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenStreetNameLookupTableButton').click(function () {

            var eCCPBaseMaintenanceCompany = _$eCCPBaseMaintenanceCompanyInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseMaintenanceCompany.streetId, displayName: eCCPBaseMaintenanceCompany.streetName, parentId: eCCPBaseMaintenanceCompany.districtId }, function (data) {
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=streetName]').val(data.displayName);
                _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=streetId]').val(data.id);
            });
        });

        $('#ClearStreetNameButton').click(function () {
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=streetName]').val('');
            _$eCCPBaseMaintenanceCompanyInformationForm.find('input[name=streetId]').val('');
        });

        this.childSelectionHandel = function (childbtn, isClear) {

            if (isClear) {

                $("#" + childbtn).attr("disabled", true);

                $("#" + childbtn).parent().next().find('button').click();

            } else {

                $("#" + childbtn).removeAttr("disabled");

            }

        }

        this.save = function () {

            if (!_$eCCPBaseMaintenanceCompanyInformationForm.valid()) {
                return;
            }

            var businessLicenseId = $('[name=_BusinessLicenseId]').val();
            if (!$('[name=BusinessLicenseIdFileToken]').val()) {

                if (businessLicenseId === "") {
                    $('#BusinessLicenseIdResize').prev().addClass('has-danger');
                    return;
                }
                $("[name=BusinessLicenseIdFileToken]").val(businessLicenseId);
            }

            var aptitudePhotoId = $('[name=_AptitudePhotoId]').val();
            if (!$('[name=AptitudePhotoIdFileToken]').val()) {

                if (aptitudePhotoId === "") {
                    $('#AptitudePhotoIdResize').prev().addClass('has-danger');
                    return;
                }
                $("[name=AptitudePhotoIdFileToken]").val(aptitudePhotoId);
            }
          
            var eCCPBaseMaintenanceCompany = _$eCCPBaseMaintenanceCompanyInformationForm.serializeFormToObject();
            
            _modalManager.setBusy(true);
            _eCCPBaseMaintenanceCompaniesService.createOrEdit(
                eCCPBaseMaintenanceCompany
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditECCPBaseMaintenanceCompanyModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);