(function ($) {
    app.modals.CreateOrEditECCPBaseAreaModal = function () {
        var self = this;
        var _eCCPBaseAreasService = abp.services.app.eCCPBaseAreas;

        var _modalManager;
        var _$eCCPBaseAreaInformationForm = null;

        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAreas/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseAreas/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });

        this.init = function (modalManager) {
          
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseAreaInformationForm = _modalManager.getModal().find('form[name=ECCPBaseAreaInformationsForm]');
            _$eCCPBaseAreaInformationForm.validate();
            
        };


        $('#OpenProvinceNameLookupTableButton').click(function () {
            var eCCPBaseArea = _$eCCPBaseAreaInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseArea.provinceId, displayName: eCCPBaseArea.provinceName, parentId: 0 }, function (data) {
                _$eCCPBaseAreaInformationForm.find('input[name=provinceName]').val(data.displayName);
                _$eCCPBaseAreaInformationForm.find('input[name=provinceId]').val(data.id);

                $("#ECCPBaseArea_ParentId").val(data.id);
                if (data.id !== eCCPBaseArea.provinceId) {
                    $("#ECCPBaseArea_Level").val(1);
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAreaInformationForm.find('input[name=provinceName]').val('');
            _$eCCPBaseAreaInformationForm.find('input[name=provinceId]').val('');

            self.childSelectionHandel(childBtn, true);
            $("#ECCPBaseArea_Level").val(0);
            $("#ECCPBaseArea_ParentId").val(0);
        });

        $('#OpenCityNameLookupTableButton').click(function () {

            var eCCPBaseArea = _$eCCPBaseAreaInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseArea.cityId, displayName: eCCPBaseArea.cityName, parentId: eCCPBaseArea.provinceId }, function (data) {
                _$eCCPBaseAreaInformationForm.find('input[name=cityName]').val(data.displayName);
                _$eCCPBaseAreaInformationForm.find('input[name=cityId]').val(data.id);

                $("#ECCPBaseArea_ParentId").val(data.id);
                if (data.id !== eCCPBaseArea.cityId) {
                    $("#ECCPBaseArea_Level").val(2);
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearCityNameButton').click(function () {

            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAreaInformationForm.find('input[name=cityName]').val('');
            _$eCCPBaseAreaInformationForm.find('input[name=cityId]').val('');

            self.childSelectionHandel(childBtn, true);
            $("#ECCPBaseArea_Level").val(1);


            if ($("#provinceId").val() != "") {
                $("#ECCPBaseArea_ParentId").val($("#provinceId").val());
            } else {
                $("#ECCPBaseArea_ParentId").val(0);
            }
        });


        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eCCPBaseArea = _$eCCPBaseAreaInformationForm.serializeFormToObject();
            var childBtn = $(this).attr('data-child');

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseArea.districtId, displayName: eCCPBaseArea.districtName, parentId: eCCPBaseArea.cityId }, function (data) {
                _$eCCPBaseAreaInformationForm.find('input[name=districtName]').val(data.displayName);
                _$eCCPBaseAreaInformationForm.find('input[name=districtId]').val(data.id);

                $("#ECCPBaseArea_ParentId").val(data.id);
                if (data.id !== eCCPBaseArea.districtId) {
                    $("#ECCPBaseArea_Level").val(3);
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseAreaInformationForm.find('input[name=districtName]').val('');
            _$eCCPBaseAreaInformationForm.find('input[name=districtId]').val('');

            self.childSelectionHandel(childBtn, true);
            $("#ECCPBaseArea_Level").val(2);

            if ($("#cityId").val() != "") {
                $("#ECCPBaseArea_ParentId").val($("#cityId").val());
            } else if ($("#provinceId").val() != "") {
                $("#ECCPBaseArea_ParentId").val($("#provinceId").val());
            } else {
                $("#ECCPBaseArea_ParentId").val(0);
            }

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
            if (!_$eCCPBaseAreaInformationForm.valid()) {
                return;
            }

            var eCCPBaseArea = _$eCCPBaseAreaInformationForm.serializeFormToObject();           
            _modalManager.setBusy(true);
            _eCCPBaseAreasService.createOrEdit(
                eCCPBaseArea
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditECCPBaseAreaModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);