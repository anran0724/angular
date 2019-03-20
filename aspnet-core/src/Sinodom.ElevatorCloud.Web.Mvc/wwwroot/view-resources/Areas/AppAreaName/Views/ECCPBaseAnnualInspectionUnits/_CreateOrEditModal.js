(function ($) {
    app.modals.CreateOrEditECCPBaseAnnualInspectionUnitModal = function () {
        var self = this;

        var _eCCPBaseAnnualInspectionUnitsService = abp.services.app.eCCPBaseAnnualInspectionUnits;

        var _modalManager;
        var _$eCCPBaseAnnualInspectionUnitInformationForm = null;

        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAnnualInspectionUnits/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseAnnualInspectionUnits/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseAnnualInspectionUnitInformationForm = _modalManager.getModal().find('form[name=ECCPBaseAnnualInspectionUnitInformationsForm]');
            _$eCCPBaseAnnualInspectionUnitInformationForm.validate();
        };

        $('#OpenProvinceNameLookupTableButton').click(function () {
            var eCCPBaseAnnualInspectionUnit = _$eCCPBaseAnnualInspectionUnitInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseAnnualInspectionUnit.provinceId, displayName: eCCPBaseAnnualInspectionUnit.provinceName, parentId: 0 }, function (data) {
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=provinceName]').val(data.displayName);
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=provinceId]').val(data.id);

                if (data.id !== eCCPBaseAnnualInspectionUnit.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=provinceName]').val('');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=provinceId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenCityNameLookupTableButton').click(function () {

            var eCCPBaseAnnualInspectionUnit = _$eCCPBaseAnnualInspectionUnitInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseAnnualInspectionUnit.cityId, displayName: eCCPBaseAnnualInspectionUnit.cityName, parentId: eCCPBaseAnnualInspectionUnit.provinceId  }, function (data) {
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=cityName]').val(data.displayName);
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=cityId]').val(data.id);

                if (data.id !== eCCPBaseAnnualInspectionUnit.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=cityName]').val('');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=cityId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eCCPBaseAnnualInspectionUnit = _$eCCPBaseAnnualInspectionUnitInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseAnnualInspectionUnit.districtId, displayName: eCCPBaseAnnualInspectionUnit.districtName, parentId: eCCPBaseAnnualInspectionUnit.cityId }, function (data) {
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=districtName]').val(data.displayName);
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=districtId]').val(data.id);

                if (data.id !== eCCPBaseAnnualInspectionUnit.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=districtName]').val('');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=districtId]').val('');

            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenStreetNameLookupTableButton').click(function () {

            var eCCPBaseAnnualInspectionUnit = _$eCCPBaseAnnualInspectionUnitInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseAnnualInspectionUnit.streetId, displayName: eCCPBaseAnnualInspectionUnit.streetName, parentId: eCCPBaseAnnualInspectionUnit.districtId }, function (data) {
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=streetName]').val(data.displayName);
                _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=streetId]').val(data.id);
            });
        });

        $('#ClearStreetNameButton').click(function () {
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=streetName]').val('');
            _$eCCPBaseAnnualInspectionUnitInformationForm.find('input[name=streetId]').val('');
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
            if (!_$eCCPBaseAnnualInspectionUnitInformationForm.valid()) {
                return;
            }

            var eCCPBaseAnnualInspectionUnit = _$eCCPBaseAnnualInspectionUnitInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eCCPBaseAnnualInspectionUnitsService.createOrEdit(
                eCCPBaseAnnualInspectionUnit
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditECCPBaseAnnualInspectionUnitModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };


    };
})(jQuery);