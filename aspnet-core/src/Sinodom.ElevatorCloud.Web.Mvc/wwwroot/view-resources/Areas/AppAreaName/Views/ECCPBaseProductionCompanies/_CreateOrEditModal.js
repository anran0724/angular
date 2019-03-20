(function ($) {
    app.modals.CreateOrEditECCPBaseProductionCompanyModal = function () {
        var self = this;
        var _eCCPBaseProductionCompaniesService = abp.services.app.eCCPBaseProductionCompanies;
        
        var _modalManager;
        var _$eCCPBaseProductionCompanyInformationForm = null;

        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseProductionCompanies/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseProductionCompanies/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseProductionCompanyInformationForm = _modalManager.getModal().find('form[name=ECCPBaseProductionCompanyInformationsForm]');
            _$eCCPBaseProductionCompanyInformationForm.validate();
        };

        $('#OpenProvinceNameLookupTableButton').click(function () {

            var eCCPBaseProductionCompany = _$eCCPBaseProductionCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseProductionCompany.provinceId, displayName: eCCPBaseProductionCompany.provinceName, ParentId: 0 }, function (data) {
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=provinceName]').val(data.displayName);
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=provinceId]').val(data.id);

                if (data.id !== eCCPBaseProductionCompany.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=provinceName]').val('');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=provinceId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearCityNameButton').click();           
        });

        $('#OpenCityNameLookupTableButton').click(function () {

            var eCCPBaseProductionCompany = _$eCCPBaseProductionCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');           
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseProductionCompany.cityId, displayName: eCCPBaseProductionCompany.cityName, ParentId: eCCPBaseProductionCompany.provinceId  }, function (data) {
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=cityName]').val(data.displayName);
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=cityId]').val(data.id);
                if (data.id !== eCCPBaseProductionCompany.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });

        });

        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=cityName]').val('');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=cityId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearDistrictNameButton').click();          
        });

        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eCCPBaseProductionCompany = _$eCCPBaseProductionCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseProductionCompany.districtId, displayName: eCCPBaseProductionCompany.districtName, ParentId: eCCPBaseProductionCompany.cityId }, function (data) {
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=districtName]').val(data.displayName);
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=districtId]').val(data.id);
                if (data.id !== eCCPBaseProductionCompany.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=districtName]').val('');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=districtId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearStreetNameButton').click();         
        });

        $('#OpenStreetNameLookupTableButton').click(function () {

            var eCCPBaseProductionCompany = _$eCCPBaseProductionCompanyInformationForm.serializeFormToObject();
           // var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseProductionCompany.streetId, displayName: eCCPBaseProductionCompany.streetName, ParentId: eCCPBaseProductionCompany.districtId }, function (data) {
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=streetName]').val(data.displayName);
                _$eCCPBaseProductionCompanyInformationForm.find('input[name=streetId]').val(data.id);
                //if (data.id !== eCCPBaseProductionCompany.cityId) {
                //    self.childSelectionHandel(childBtn, true);
                //}
                //self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearStreetNameButton').click(function () {
            //var childBtn = $(this).attr('data-child');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=streetName]').val('');
            _$eCCPBaseProductionCompanyInformationForm.find('input[name=streetId]').val('');
            //self.childSelectionHandel(childBtn, true);
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
            if (!_$eCCPBaseProductionCompanyInformationForm.valid()) {
                return;
            }

            var eCCPBaseProductionCompany = _$eCCPBaseProductionCompanyInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eCCPBaseProductionCompaniesService.createOrEdit(
                eCCPBaseProductionCompany
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditECCPBaseProductionCompanyModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);