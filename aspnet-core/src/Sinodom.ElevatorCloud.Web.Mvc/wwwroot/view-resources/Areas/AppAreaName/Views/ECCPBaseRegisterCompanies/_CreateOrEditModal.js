(function ($) {
    app.modals.CreateOrEditECCPBaseRegisterCompanyModal = function () {
        var self = this;
        var _eCCPBaseRegisterCompaniesService = abp.services.app.eCCPBaseRegisterCompanies;

        var _modalManager;
        var _$eCCPBaseRegisterCompanyInformationForm = null;

		        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseRegisterCompanies/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseRegisterCompanies/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseRegisterCompanyInformationForm = _modalManager.getModal().find('form[name=ECCPBaseRegisterCompanyInformationsForm]');
            _$eCCPBaseRegisterCompanyInformationForm.validate();
        };

		          $('#OpenProvinceNameLookupTableButton').click(function () {

            var eCCPBaseRegisterCompany = _$eCCPBaseRegisterCompanyInformationForm.serializeFormToObject();
                      var childBtn = $(this).attr('data-child');
                      _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseRegisterCompany.provinceId, displayName: eCCPBaseRegisterCompany.provinceName ,parentId: 0}, function (data) {
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=provinceName]').val(data.displayName); 
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=provinceId]').val(data.id); 

              
                if (data.id !== eCCPBaseRegisterCompany.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }

                self.childSelectionHandel(childBtn, false);
            });
        });
		
        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=provinceName]').val(''); 
            _$eCCPBaseRegisterCompanyInformationForm.find('input[name=provinceId]').val(''); 
             self.childSelectionHandel(childBtn, true);
        });
		
        $('#OpenCityNameLookupTableButton').click(function () {

            var eCCPBaseRegisterCompany = _$eCCPBaseRegisterCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseRegisterCompany.cityId, displayName: eCCPBaseRegisterCompany.cityName, parentId: eCCPBaseRegisterCompany.provinceId}, function (data) {
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=cityName]').val(data.displayName); 
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=cityId]').val(data.id); 
                if (data.id !== eCCPBaseRegisterCompany.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });
		
        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=cityName]').val(''); 
            _$eCCPBaseRegisterCompanyInformationForm.find('input[name=cityId]').val(''); 
            self.childSelectionHandel(childBtn, true);
        });
		
        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eCCPBaseRegisterCompany = _$eCCPBaseRegisterCompanyInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseRegisterCompany.districtId, displayName: eCCPBaseRegisterCompany.districtName, parentId: eCCPBaseRegisterCompany.cityId}, function (data) {
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=districtName]').val(data.displayName); 
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=districtId]').val(data.id); 
                if (data.id !== eCCPBaseRegisterCompany.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });
		
        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=districtName]').val(''); 
            _$eCCPBaseRegisterCompanyInformationForm.find('input[name=districtId]').val(''); 
            self.childSelectionHandel(childBtn, true);
        });
		
        $('#OpenStreetNameLookupTableButton').click(function () {

            var eCCPBaseRegisterCompany = _$eCCPBaseRegisterCompanyInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseRegisterCompany.streetId, displayName: eCCPBaseRegisterCompany.streetName, parentId: eCCPBaseRegisterCompany.districtId}, function (data) {
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=streetName]').val(data.displayName); 
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=streetId]').val(data.id); 
            });
        });
		
		$('#ClearStreetNameButton').click(function () {
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=streetName]').val(''); 
                _$eCCPBaseRegisterCompanyInformationForm.find('input[name=streetId]').val(''); 
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
            if (!_$eCCPBaseRegisterCompanyInformationForm.valid()) {
                return;
            }

            var eCCPBaseRegisterCompany = _$eCCPBaseRegisterCompanyInformationForm.serializeFormToObject();
			
			 _modalManager.setBusy(true);
			 _eCCPBaseRegisterCompaniesService.createOrEdit(
				eCCPBaseRegisterCompany
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditECCPBaseRegisterCompanyModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
    };
})(jQuery);