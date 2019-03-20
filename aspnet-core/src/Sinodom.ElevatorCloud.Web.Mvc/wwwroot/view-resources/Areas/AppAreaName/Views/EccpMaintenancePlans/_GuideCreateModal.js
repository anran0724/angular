(function ($) {
    var _modalManager;
    var _$eccpMaintenancePlanInformationForm = null;
    var _eccpMaintenancePlansService = abp.services.app.eccpMaintenancePlans;

    var wizardDemo = function () {
        //== Base elements
        var _wizardEl, _formEl, _validator, _wizard;

        //== Private functions
        var initWizard = function () {
            //== Initialize form wizard
            _wizard = new mWizard('m_wizard', {
                startStep: 1
            });

            //== Validation before going to next page
            _wizard.on('beforeNext',
                function (wizard) {
                    $('[name=isSkip]').val(1);
                    var isCheckedItems = true;
                    var form = $('[name=EccpMaintenancePlanInformationsForm]');
                    form.find('input').parent().removeClass('has-danger');

                    if (wizard.currentStep == 1) {
                        if (form.find('input[name="elevatorName"]').val() === '') {
                            form.find('input[name="elevatorName"]').parent()
                                .addClass('has-danger');
                            isCheckedItems = false;
                        }

                        if (form.find('input[name="elevatorNames"]').val() === '') {
                            form.find('input[name="elevatorNames"]').parent()
                                .addClass('has-danger');
                            isCheckedItems = false;
                        }
                    }

                    if (wizard.currentStep == 2) {
                        if (form.find('input[name="propertyUserNames"]').val() === '') {
                            form.find('input[name="propertyUserNames"]').parent()
                                .addClass('has-danger');
                            isCheckedItems = false;
                        }

                        if (form.find('input[name="maintenanceUserNames"]').val() === '') {
                            form.find('input[name="maintenanceUserNames"]').parent()
                                .addClass('has-danger');
                            isCheckedItems = false;
                        }
                    }

                    if (wizard.currentStep == 4) {
                        if (form.find('input[name="quarterPollingPeriod"]').val() === '') {
                            form.find('input[name="quarterPollingPeriod"]').attr("required", "required");
                            isCheckedItems = false;
                        }

                        if (form.find('input[name="halfYearPollingPeriod"]').val() === '') {
                            form.find('input[name="halfYearPollingPeriod"]').attr("required", "required");
                            isCheckedItems = false;
                        }

                        if (form.find('input[name="yearPollingPeriod"]').val() === '') {
                            form.find('input[name="yearPollingPeriod"]').attr("required", "required");
                            isCheckedItems = false;
                        }
                    }

                    if (!isCheckedItems) {
                        wizard.stop();
                        //return;
                    }

                    if (_validator.form() !== true) {
                        wizard.stop(); // don't go to the next step
                    }
                });

            _wizard.on('beforeSkip', function (wizard) {
                $('[name=isSkip]').val(0);
                mUtil.scrollTop();
            });

            //== Change event
            _wizard.on('change', function (wizard) {
                if (false) {
                    if (wizard.currentStep == 4) {
                        $('[data-wizard-action="skip"]').show();
                    } else {
                        $('[data-wizard-action="skip"]').hide();
                    }
                }

                mUtil.scrollTop();
            });
        }

        var initValidation = function () {
            _validator = _formEl.validate({
                //== Validate only visible fields
                ignore: ":hidden",

                //== Validation rules
                rules: {

                },
                //== Validation messages
                messages: {

                },

                //== Display error  
                invalidHandler: function (event, validator) {
                    mUtil.scrollTop();
                    swal({
                        "title": "",
                        "text": "There are some errors in your submission. Please correct them.",
                        "type": "error",
                        "confirmButtonClass": "btn btn-secondary m-btn m-btn--wide"
                    });
                },

                //== Submit valid form
                submitHandler: function (form) {

                }
            });
        }

        var initSubmit = function () {
            var btn = _formEl.find('[data-wizard-action="submit"]');

            btn.on('click', function (e) {
                e.preventDefault();

                if (_validator.form()) {

                    var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();
                    var isCheckedItems = true;
                    eccpMaintenancePlan.MaintenanceTypes = [];
                    $.each($('[name*=maintenanceTemplateId_]'), function (i, item) {
                        if ($(this).val() != "" && $(this).val() != null && $(this).val() != "0") {
                            var objectData = {
                                TypeId: $(this).data('val'),
                                MaintenanceTemplateId: $(this).val()
                            };
                            eccpMaintenancePlan.MaintenanceTypes.push(objectData);
                        }
                    });

                    $('input').parent().removeClass('has-danger');
                    $.each($('[name*=maintenanceTemplateName_]'), function (i, item) {
                        if ($(this).val() == "") {
                            $(this).parent().addClass('has-danger');
                            isCheckedItems = false;
                        }
                    });

                    if (!isCheckedItems) {
                        mUtil.scrollTop();
                        return;
                    }

                    mApp.progress(btn);
                    _modalManager.setBusy(true);
                    _eccpMaintenancePlansService.createOrEdit(
                        eccpMaintenancePlan
                    ).done(function () {
                        abp.notify.info(app.localize("SavedSuccessfully"));
                        _modalManager.close();
                        abp.event.trigger("app.createOrEditEccpMaintenancePlanModalSaved");
                    }).always(function () {
                        _modalManager.setBusy(false);
                    });
                }
            });
        }

        return {
            // public functions
            init: function () {
                _wizardEl = $('#m_wizard');
                _formEl = $('#m_form');

                initWizard();
                initValidation();
                initSubmit();
            }
        };
    }();

    app.modals.GuideCreateEccpMaintenancePlanModal = function () {
        //$("#m_wizard").mWizard();
        var _eccpBaseElevatorLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/EccpBaseElevatorLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_EccpBaseElevatorLookupTableModal.js',
            modalClass: 'EccpBaseElevatorLookupTableModal'
        });

        var _eccpBaseElevatorsLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/EccpBaseElevatorsLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_EccpBaseElevatorsLookupTableModal.js',
            modalClass: 'EccpBaseElevatorsLookupTableModal'
        });

        var _propertyUserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/PropertyUserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_PropertyUserLookupTableModal.js',
            modalClass: 'PropertyUserLookupTableModal'
        });

        var _maintenanceUserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/MaintenanceUserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_MaintenanceUserLookupTableModal.js',
            modalClass: 'MaintenanceUserLookupTableModal'
        });

        var _maintenanceTemplatesLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenancePlans/MaintenanceTemplatesLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenancePlans/_MaintenanceTemplatesLookupTableModal.js',
            modalClass: 'MaintenanceTemplatesLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });

            _$eccpMaintenancePlanInformationForm = _modalManager.getModal().find('form[name=EccpMaintenancePlanInformationsForm]');
            _$eccpMaintenancePlanInformationForm.validate();

            wizardDemo.init();
        };

        $('#OpenEccpBaseElevatorLookupTableButton').click(function () {

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();
            _eccpBaseElevatorLookupTableModal.open({ id: eccpMaintenancePlan.elevatorId, displayName: eccpMaintenancePlan.eccpBaseElevatorName, planGroupGuid: $('[name=planGroupGuid]').val() }, function (data) {

                _$eccpMaintenancePlanInformationForm.find('input[name=elevatorName]').val(data.displayName);
                _$eccpMaintenancePlanInformationForm.find('input[name=elevatorId]').val(data.id);
                if (data.id !== eccpMaintenancePlan.elevatorId) {
                    $('#OpenEccpBaseElevatorsLookupTableButton').attr('disabled', true);
                    $('#ClearEccpBaseElevatorsNameButton').click();
                }
                $('#OpenEccpBaseElevatorsLookupTableButton').removeAttr('disabled');
            });
        });

        $('#ClearEccpBaseElevatorNameButton').click(function () {
            _$eccpMaintenancePlanInformationForm.find('input[name=elevatorName]').val('');
            _$eccpMaintenancePlanInformationForm.find('input[name=elevatorId]').val('');
            $('#OpenEccpBaseElevatorsLookupTableButton').attr('disabled', true);
            $('#ClearEccpBaseElevatorsNameButton').click();
        });

        $('#OpenEccpBaseElevatorsLookupTableButton').click(function () {

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();

            _eccpBaseElevatorsLookupTableModal.open({ id: eccpMaintenancePlan.elevatorId, displayName: eccpMaintenancePlan.eccpBaseElevatorName, planGroupGuid: $('[name=planGroupGuid]').val(), elevatorIds: $('[name=elevatorIds]').val(), elevatorNames: $('[name=elevatorNames]').val(), elevatorId: $('[name=elevatorId]').val() }, function (data) {
                _$eccpMaintenancePlanInformationForm.find('input[name=elevatorNames]').val(data.displayName);
                _$eccpMaintenancePlanInformationForm.find('input[name=elevatorIds]').val(data.id);
            });
        });

        $('#ClearEccpBaseElevatorsNameButton').click(function () {
            _$eccpMaintenancePlanInformationForm.find('input[name=elevatorNames]').val('');
            _$eccpMaintenancePlanInformationForm.find('input[name=elevatorIds]').val('');
        });

        $('#OpenPropertyUserLookupTableButton').click(function () {

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();

            _propertyUserLookupTableModal.open({ id: eccpMaintenancePlan.elevatorId, displayName: eccpMaintenancePlan.propertyUserName, propertyUserIds: $('[name=propertyUserIds]').val(), propertyUserNames: $('[name=propertyUserNames]').val() }, function (data) {
                _$eccpMaintenancePlanInformationForm.find('input[name=propertyUserNames]').val(data.displayName);
                _$eccpMaintenancePlanInformationForm.find('input[name=propertyUserIds]').val(data.id);
            });
        });

        $('#ClearPropertyUserNameButton').click(function () {
            _$eccpMaintenancePlanInformationForm.find('input[name=propertyUserNames]').val('');
            _$eccpMaintenancePlanInformationForm.find('input[name=propertyUserIds]').val('');
        });

        $('#OpenMaintenanceUserLookupTableButton').click(function () {

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();

            _maintenanceUserLookupTableModal.open({ id: eccpMaintenancePlan.elevatorId, displayName: eccpMaintenancePlan.maintenanceUserName, maintenanceUserIds: $('[name=maintenanceUserIds]').val(), maintenanceUserNames: $('[name=maintenanceUserNames]').val() }, function (data) {
                _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceUserNames]').val(data.displayName);
                _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceUserIds]').val(data.id);
            });
        });

        $('#ClearMaintenanceUserNameButton').click(function () {
            _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceUserNames]').val('');
            _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceUserIds]').val('');
        });


        $('[id*=OpenMaintenanceTemplateLookupTableButton]').click(function () {

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();
            var typeId = $(this).data('id');

            _maintenanceTemplatesLookupTableModal.open({ id: eccpMaintenancePlan.elevatorId, displayName: eccpMaintenancePlan.maintenanceUserName, maintenanceTypeId: typeId }, function (data) {
                _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateId_' + typeId + ']').data('val', typeId);
                _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateName_' + typeId + ']').val(data.displayName);
                _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateId_' + typeId + ']').val(data.id);
            });
        });

        $('[id*=ClearMaintenanceTemplateNameButton]').click(function () {
            var typeId = $(this).data('id');
            _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateId_' + typeId + ']').data('val', '');
            _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateName_' + typeId + ']').val('');
            _$eccpMaintenancePlanInformationForm.find('input[name=maintenanceTemplateId_' + typeId + ']').val('');
        });

        this.save = function () {
            if (!_$eccpMaintenancePlanInformationForm.valid()) {
                return;
            }

            var eccpMaintenancePlan = _$eccpMaintenancePlanInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eccpMaintenancePlansService.createOrEdit(
                eccpMaintenancePlan
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpMaintenancePlanModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);