(function ($) {
    app.modals.CreateOrEditEccpBaseSaicUnitModal = function () {
        var self = this;
        var _eccpBaseSaicUnitsService = abp.services.app.eccpBaseSaicUnits;

        var _modalManager;
        var _$eccpBaseSaicUnitInformationForm = null;

        var _eccpBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseSaicUnits/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseSaicUnits/_ECCPBaseAreaLookupTableModal.js',
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

            _$eccpBaseSaicUnitInformationForm = _modalManager.getModal().find('form[name=EccpBaseSaicUnitInformationsForm]');
            _$eccpBaseSaicUnitInformationForm.validate();
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

        $('#OpenECCPBaseAreaLookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eccpBaseSaicUnit = _$eccpBaseSaicUnitInformationForm.serializeFormToObject();

            _eccpBaseAreaLookupTableModal.open({ id: eccpBaseSaicUnit.provinceId, displayName: eccpBaseSaicUnit.eCCPBaseAreaName, parentId: 0 }, function (data) {
                _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName]').val(data.displayName);
                _$eccpBaseSaicUnitInformationForm.find('input[name=provinceId]').val(data.id);
                if (data.id !== eccpBaseSaicUnit.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearECCPBaseAreaNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName]').val('');
            _$eccpBaseSaicUnitInformationForm.find('input[name=provinceId]').val('');
            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenECCPBaseArea2LookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eccpBaseSaicUnit = _$eccpBaseSaicUnitInformationForm.serializeFormToObject();

            _eccpBaseAreaLookupTableModal.open({ id: eccpBaseSaicUnit.cityId, displayName: eccpBaseSaicUnit.eccpBaseAreaName2, parentId: eccpBaseSaicUnit.provinceId }, function (data) {
                _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName2]').val(data.displayName);
                _$eccpBaseSaicUnitInformationForm.find('input[name=cityId]').val(data.id);
                if (data.id !== eccpBaseSaicUnit.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearECCPBaseAreaName2Button').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName2]').val('');
            _$eccpBaseSaicUnitInformationForm.find('input[name=cityId]').val('');
            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenECCPBaseArea3LookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eccpBaseSaicUnit = _$eccpBaseSaicUnitInformationForm.serializeFormToObject();

            _eccpBaseAreaLookupTableModal.open({ id: eccpBaseSaicUnit.districtId, displayName: eccpBaseSaicUnit.eccpBaseAreaName3, parentId: eccpBaseSaicUnit.cityId }, function (data) {
                _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName3]').val(data.displayName);
                _$eccpBaseSaicUnitInformationForm.find('input[name=districtId]').val(data.id);
                if (data.id !== eccpBaseSaicUnit.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearECCPBaseAreaName3Button').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName3]').val('');
            _$eccpBaseSaicUnitInformationForm.find('input[name=districtId]').val('');
            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenECCPBaseArea4LookupTableButton').click(function () {

            var eccpBaseSaicUnit = _$eccpBaseSaicUnitInformationForm.serializeFormToObject();

            _eccpBaseAreaLookupTableModal.open({ id: eccpBaseSaicUnit.streetId, displayName: eccpBaseSaicUnit.eccpBaseAreaName4, parentId: eccpBaseSaicUnit.districtId }, function (data) {
                _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName4]').val(data.displayName);
                _$eccpBaseSaicUnitInformationForm.find('input[name=streetId]').val(data.id);
            });
        });

        $('#ClearECCPBaseAreaName4Button').click(function () {
            _$eccpBaseSaicUnitInformationForm.find('input[name=eccpBaseAreaName4]').val('');
            _$eccpBaseSaicUnitInformationForm.find('input[name=streetId]').val('');
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
            if (!_$eccpBaseSaicUnitInformationForm.valid()) {
                return;
            }

            var eccpBaseSaicUnit = _$eccpBaseSaicUnitInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eccpBaseSaicUnitsService.createOrEdit(
                eccpBaseSaicUnit
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpBaseSaicUnitModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);