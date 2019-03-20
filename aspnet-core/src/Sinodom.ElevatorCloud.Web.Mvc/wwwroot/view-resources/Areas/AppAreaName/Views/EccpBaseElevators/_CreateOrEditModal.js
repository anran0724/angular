(function ($) {
    app.modals.CreateOrEditEccpBaseElevatorModal = function () {

        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;
        var self = this;
        var _modalManager;
        var _$eccpBaseElevatorInformationForm = null;
        var _$ElevatorSubsidiaryInfoForm = null;

        var _eccpDictPlaceTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpDictPlaceTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpDictPlaceTypeLookupTableModal.js',
            modalClass: 'EccpDictPlaceTypeLookupTableModal'
        });
        var _eccpDictElevatorTypeLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpDictElevatorTypeLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpDictElevatorTypeLookupTableModal.js',
            modalClass: 'EccpDictElevatorTypeLookupTableModal'
        });
        var _eCCPDictElevatorStatusLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPDictElevatorStatusLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPDictElevatorStatusLookupTableModal.js',
            modalClass: 'ECCPDictElevatorStatusLookupTableModal'
        });
        //园区映射方法配置
        var _eCCPBaseCommunityLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseCommunityLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseCommunityLookupTableModal.js',
            modalClass: 'ECCPBaseCommunityLookupTableModal'
        });
        var _eCCPBasePropertyCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBasePropertyCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBasePropertyCompanyLookupTableModal.js',
            modalClass: 'ECCPBasePropertyCompanyLookupTableModal'
        });

        var _eCCPBaseMaintenanceCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseMaintenanceCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseMaintenanceCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseMaintenanceCompanyLookupTableModal'
        });
        var _eCCPBaseAnnualInspectionUnitLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseAnnualInspectionUnitLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseAnnualInspectionUnitLookupTableModal.js',
            modalClass: 'ECCPBaseAnnualInspectionUnitLookupTableModal'
        });
        var _eCCPBaseRegisterCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseRegisterCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseRegisterCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseRegisterCompanyLookupTableModal'
        });
        var _eCCPBaseProductionCompanyLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseProductionCompanyLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseProductionCompanyLookupTableModal.js',
            modalClass: 'ECCPBaseProductionCompanyLookupTableModal'
        });
        var _eccpBaseElevatorBrandLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpBaseElevatorBrandLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpBaseElevatorBrandLookupTableModal.js',
            modalClass: 'EccpBaseElevatorBrandLookupTableModal'
        });
        var _eccpBaseElevatorModelLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpBaseElevatorModelLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpBaseElevatorModelLookupTableModal.js',
            modalClass: 'EccpBaseElevatorModelLookupTableModal'
        });
        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/ECCPBaseAreaLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_ECCPBaseAreaLookupTableModal.js',
            modalClass: 'ECCPBaseAreaLookupTableModal'
        });

        var myIcon = new BMap.Icon("https://api.map.baidu.com/img/markers.png", new BMap.Size(43, 25), {
            offset: new BMap.Size(10, 25),
            imageOffset: new BMap.Size(20, -11 * 25),
            anchor: new BMap.Size(30, 25)
        });


       


        function LoadMap() {
            map = new BMap.Map("baiduMap");
            var opts = { type: BMAP_NAVIGATION_CONTROL_SMALL, anchor: BMAP_ANCHOR_TOP_RIGHT, offset: new BMap.Size(5, 5) }
            map.addControl(new BMap.NavigationControl(opts));
            initMapData();
            map.enableScrollWheelZoom();
            map.addEventListener("click", showInfo);
        }

        function initMapData() {
            var point = new BMap.Point(116.332117, 39.945034);//定义一个中心点坐标              
            map.centerAndZoom(point, 12);//设定地图的中心点和坐标并将地图显示在地图容器中
            window.map = map;//将map变量存储在全局                        
        }

        function showInfo(e) {
            var geoc = new BMap.Geocoder();
            var pt = e.point;
            geoc.getLocation(pt, function (rs) {
                //addressComponents对象可以获取到详细的地址信息
                var addComp = rs.addressComponents;
                var site = addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
                //将对应的HTML元素设置值
                $("#EccpBaseElevator_InstallationAddress").val(site);
            });
            setMapValue(e.point);
        }

        function setMapValue(point) {
            map.centerAndZoom(point, map.getZoom());
            map.clearOverlays();
            var marker = new BMap.Marker(point, { icon: myIcon });
            map.addOverlay(marker);
            $("#EccpBaseElevator_Longitude").val(point.lng);
            $("#EccpBaseElevator_Latitude").val(point.lat);
        }
        
        this.init = function (modalManager) {            
            _modalManager = modalManager;
            console.log("add");
            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'YYYY-MM-DD'
            });

            _$elevatorInformationsForm1 = _modalManager.getModal().find('form[name=ElevatorInformationsForm1]');
            _$elevatorInformationsForm1.validate();

            _$elevatorInformationsForm2 = modalManager.getModal().find('form[name=ElevatorInformationsForm2]');
            _$elevatorInformationsForm2.validate();

            _$elevatorInformationsForm3 = _modalManager.getModal().find('form[name=ElevatorInformationsForm3]');
            _$elevatorInformationsForm3.validate();

            _$elevatorInformationsForm4 = modalManager.getModal().find('form[name=ElevatorInformationsForm4]');
            _$elevatorInformationsForm4.validate();

            LoadMap();
            map.addEventListener("tilesloaded", function () {
                var longitude = $("#EccpBaseElevator_Longitude").val();
                var latitude = $("#EccpBaseElevator_Latitude").val();
                if (longitude != "" && latitude != "") {
                    var new_point = new BMap.Point(longitude, latitude);
                    var marker = new BMap.Marker(new_point);
                    map.addOverlay(marker);
                    map.panTo(new_point);
                };
            });         
        };
        
        $("#EccpBaseElevator_InstallationAddress").keyup(function () {
            var city = $("#input[name='cityName']").val();
            map.clearOverlays();
            var myGeo = new BMap.Geocoder();
            if ($(this).val() != "") {
                myGeo.getPoint($(this).val(), function (point) {
                    if (point) {

                        map.centerAndZoom(point, 16);
                        map.addOverlay(new BMap.Marker(point));
                    }
                }, city);
            }
        });

        $('#OpenEccpDictPlaceTypeLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();

            _eccpDictPlaceTypeLookupTableModal.open({ id: eccpBaseElevator.eccpDictPlaceTypeId, displayName: eccpBaseElevator.eccpDictPlaceTypeName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eccpDictPlaceTypeName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eccpDictPlaceTypeId]').val(data.id);
            });
        });

        $('#ClearEccpDictPlaceTypeNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eccpDictPlaceTypeName]').val('');
            _$elevatorInformationsForm3.find('input[name=eccpDictPlaceTypeId]').val('');
        });

        $('#OpenEccpDictElevatorTypeLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();

            _eccpDictElevatorTypeLookupTableModal.open({ id: eccpBaseElevator.eccpDictElevatorTypeId, displayName: eccpBaseElevator.eccpDictElevatorTypeName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eccpDictElevatorTypeName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eccpDictElevatorTypeId]').val(data.id);
            });
        });

        $('#ClearEccpDictElevatorTypeNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eccpDictElevatorTypeName]').val('');
            _$elevatorInformationsForm3.find('input[name=eccpDictElevatorTypeId]').val('');
        });

        $('#OpenECCPDictElevatorStatusLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();

            _eCCPDictElevatorStatusLookupTableModal.open({ id: eccpBaseElevator.eCCPDictElevatorStatusId, displayName: eccpBaseElevator.eCCPDictElevatorStatusName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eCCPDictElevatorStatusName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eCCPDictElevatorStatusId]').val(data.id);
            });
        });

        $('#ClearECCPDictElevatorStatusNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eCCPDictElevatorStatusName]').val('');
            _$elevatorInformationsForm3.find('input[name=eCCPDictElevatorStatusId]').val('');
        });


        $('#OpenECCPBaseMaintenanceCompanyLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();

            _eCCPBaseMaintenanceCompanyLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseMaintenanceCompanyId, displayName: eccpBaseElevator.eCCPBaseMaintenanceCompanyName }, function (data) {
                _$elevatorInformationsForm1.find('input[name=eCCPBaseMaintenanceCompanyName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=eCCPBaseMaintenanceCompanyId]').val(data.id);
            });
        });

        $('#ClearECCPBaseMaintenanceCompanyNameButton').click(function () {
            _$elevatorInformationsForm1.find('input[name=eCCPBaseMaintenanceCompanyName]').val('');
            _$elevatorInformationsForm1.find('input[name=eCCPBaseMaintenanceCompanyId]').val('');
        });

        $('#OpenECCPBasePropertyCompanyLookupTableButton').click(function () {

            var eccpMaintenanceContract = _$elevatorInformationsForm1.serializeFormToObject();

            _eCCPBasePropertyCompanyLookupTableModal.open({ id: eccpMaintenanceContract.eCCPBasePropertyCompanyId, displayName: eccpMaintenanceContract.eCCPBasePropertyCompanyName }, function (data) {
                _$elevatorInformationsForm1.find('input[name=eCCPBasePropertyCompanyName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=eCCPBasePropertyCompanyId]').val(data.id);
            });
        });
        $('#ClearECCPBasePropertyCompanyNameButton').click(function () {
            _$elevatorInformationsForm1.find('input[name=eCCPBasePropertyCompanyName]').val('');
            _$elevatorInformationsForm1.find('input[name=eCCPBasePropertyCompanyId]').val('');
        });

        $('#OpenECCPBaseAnnualInspectionUnitLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();

            _eCCPBaseAnnualInspectionUnitLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseAnnualInspectionUnitId, displayName: eccpBaseElevator.eCCPBaseAnnualInspectionUnitName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eCCPBaseAnnualInspectionUnitName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eCCPBaseAnnualInspectionUnitId]').val(data.id);
            });
        });

        $('#ClearECCPBaseAnnualInspectionUnitNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eCCPBaseAnnualInspectionUnitName]').val('');
            _$elevatorInformationsForm3.find('input[name=eCCPBaseAnnualInspectionUnitId]').val('');
        });

        $('#OpenECCPBaseRegisterCompanyLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();

            _eCCPBaseRegisterCompanyLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseRegisterCompanyId, displayName: eccpBaseElevator.eCCPBaseRegisterCompanyName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eCCPBaseRegisterCompanyName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eCCPBaseRegisterCompanyId]').val(data.id);
            });
        });

        $('#ClearECCPBaseRegisterCompanyNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eCCPBaseRegisterCompanyName]').val('');
            _$elevatorInformationsForm3.find('input[name=eCCPBaseRegisterCompanyId]').val('');
        });

        //生产企业
        $('#OpenECCPBaseProductionCompanyLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseProductionCompanyLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseProductionCompanyId, displayName: eccpBaseElevator.eCCPBaseProductionCompanyName }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eCCPBaseProductionCompanyName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eCCPBaseProductionCompanyId]').val(data.id);
                if (data.id !== eccpBaseElevator.eCCPBaseProductionCompanyId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearECCPBaseProductionCompanyNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$elevatorInformationsForm3.find('input[name=eCCPBaseProductionCompanyName]').val('');
            _$elevatorInformationsForm3.find('input[name=eCCPBaseProductionCompanyId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearEccpBaseElevatorBrandNameButton').click();
        });

        //电梯品牌点击按钮
        $('#OpenEccpBaseElevatorBrandLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eccpBaseElevatorBrandLookupTableModal.open({ id: eccpBaseElevator.eccpBaseElevatorBrandId, displayName: eccpBaseElevator.eccpBaseElevatorBrandName, ParentId: eccpBaseElevator.eCCPBaseProductionCompanyId }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorBrandName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorBrandId]').val(data.id);
                if (data.id !== eccpBaseElevator.eccpBaseElevatorBrandId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });

        $('#ClearEccpBaseElevatorBrandNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorBrandName]').val('');
            _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorBrandId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearEccpBaseElevatorModelNameButton').click();
        });

        //电梯型号点击按钮
        $('#OpenEccpBaseElevatorModelLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm3.serializeFormToObject();
            _eccpBaseElevatorModelLookupTableModal.open({ id: eccpBaseElevator.eccpBaseElevatorModelId, displayName: eccpBaseElevator.eccpBaseElevatorModelName, ParentId: eccpBaseElevator.eccpBaseElevatorBrandId }, function (data) {
                _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorModelName]').val(data.displayName);
                _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorModelId]').val(data.id);
            });
        });

        $('#ClearEccpBaseElevatorModelNameButton').click(function () {
            _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorModelName]').val('');
            _$elevatorInformationsForm3.find('input[name=eccpBaseElevatorModelId]').val('');
        });

        //省点击按钮
        $('#OpenProvinceNameLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eccpBaseElevator.provinceId, displayName: eccpBaseElevator.provinceName, ParentId: 0 }, function (data) {
                _$elevatorInformationsForm1.find('input[name=provinceName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=provinceId]').val(data.id);
                if (data.id !== eccpBaseElevator.provinceId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });
        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$elevatorInformationsForm1.find('input[name=provinceName]').val('');
            _$elevatorInformationsForm1.find('input[name=provinceId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearCityNameButton').click();
        });

        //市点击按钮
        $('#OpenCityNameLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eccpBaseElevator.cityId, displayName: eccpBaseElevator.cityName, ParentId: eccpBaseElevator.provinceId }, function (data) {
                _$elevatorInformationsForm1.find('input[name=cityName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=cityId]').val(data.id);
                if (data.id !== eccpBaseElevator.cityId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
            });
        });
        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$elevatorInformationsForm1.find('input[name=cityName]').val('');
            _$elevatorInformationsForm1.find('input[name=cityId]').val('');
            self.childSelectionHandel(childBtn, true);
            $('#ClearDistrictNameButton').click();
        });

        //区点击按钮
        $('#OpenDistrictNameLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();
            var childBtn = $(this).attr('data-child');
            _eCCPBaseAreaLookupTableModal.open({ id: eccpBaseElevator.districtId, displayName: eccpBaseElevator.districtName, ParentId: eccpBaseElevator.cityId }, function (data) {
                _$elevatorInformationsForm1.find('input[name=districtName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=districtId]').val(data.id);
                if (data.id !== eccpBaseElevator.districtId) {
                    self.childSelectionHandel(childBtn, true);
                }
                self.childSelectionHandel(childBtn, false);
                //if (data.id !== eccpBaseElevator.districtId) {
                //    self.childSelectionHandel(childBtn, true);
                //}
                //self.childSelectionHandel(childBtn, false);
            });
        });
        $('#ClearDistrictNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$elevatorInformationsForm1.find('input[name=districtName]').val('');
            _$elevatorInformationsForm1.find('input[name=districtId]').val('');
            self.childSelectionHandel(childBtn, true);
            //$('#ClearStreetNameButton').click();
            $('#ClearECCPBaseCommunityNameButton').click();
        });

        //街道点击按钮
        //$('#OpenStreetNameLookupTableButton').click(function () {
        //    var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();
        //    var childBtn = $(this).attr('data-child');
        //    _eCCPBaseAreaLookupTableModal.open({ id: eccpBaseElevator.streetId, displayName: eccpBaseElevator.streetName, ParentId: eccpBaseElevator.districtId }, function (data) {
        //        _$elevatorInformationsForm1.find('input[name=streetName]').val(data.displayName);
        //        _$elevatorInformationsForm1.find('input[name=streetId]').val(data.id);
        //        if (data.id !== eccpBaseElevator.districtId) {
        //            self.childSelectionHandel(childBtn, true);
        //        }
        //        self.childSelectionHandel(childBtn, false);
        //    });
        //});
        //$('#ClearStreetNameButton').click(function () {
        //    var childBtn = $(this).attr('data-child');
        //    _$elevatorInformationsForm1.find('input[name=streetName]').val('');
        //    _$elevatorInformationsForm1.find('input[name=streetId]').val('');
        //    self.childSelectionHandel(childBtn, true);
        //    $('#ClearECCPBaseCommunityNameButton').click();
        //});

        //园区点击按钮
        $('#OpenECCPBaseCommunityLookupTableButton').click(function () {

            var eccpBaseElevator = _$elevatorInformationsForm1.serializeFormToObject();

            _eCCPBaseCommunityLookupTableModal.open({ id: eccpBaseElevator.eCCPBaseCommunityId, displayName: eccpBaseElevator.eCCPBaseCommunityName, ParentId: eccpBaseElevator.districtId }, function (data) {
                _$elevatorInformationsForm1.find('input[name=eCCPBaseCommunityName]').val(data.displayName);
                _$elevatorInformationsForm1.find('input[name=eCCPBaseCommunityId]').val(data.id);
            });
        });
        $('#ClearECCPBaseCommunityNameButton').click(function () {
            _$elevatorInformationsForm1.find('input[name=eCCPBaseCommunityName]').val('');
            _$elevatorInformationsForm1.find('input[name=eCCPBaseCommunityId]').val('');
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

            if (!_$elevatorInformationsForm1.valid()) {
                return;
            }
            if (!_$elevatorInformationsForm2.valid()) {
                return;
            }
            if (!_$elevatorInformationsForm3.valid()) {
                return;
            }
            if (!_$elevatorInformationsForm4.valid()) {
                return;
            }

            var elevatorForm1 = _$elevatorInformationsForm1.serializeFormToObject();
            var elevatorForm2 = _$elevatorInformationsForm2.serializeFormToObject();
            var elevatorForm3 = _$elevatorInformationsForm3.serializeFormToObject();
            var elevatorForm4 = _$elevatorInformationsForm4.serializeFormToObject();

            var eccpBaseElevator = {};
            var createOrEditEccpBaseElevatorSubsidiaryInfoDto = {};

            eccpBaseElevator.id = elevatorForm1.id;
            eccpBaseElevator.certificateNum = elevatorForm1.certificateNum;
            eccpBaseElevator.cityId = elevatorForm1.cityId;
            eccpBaseElevator.districtId = elevatorForm1.districtId;
            eccpBaseElevator.eCCPBaseCommunityId = elevatorForm1.eCCPBaseCommunityId;
            eccpBaseElevator.eCCPBaseMaintenanceCompanyId = elevatorForm1.eCCPBaseMaintenanceCompanyId;
            eccpBaseElevator.eCCPBasePropertyCompanyId = elevatorForm1.eCCPBasePropertyCompanyId;
            eccpBaseElevator.provinceId = elevatorForm1.provinceId;
            eccpBaseElevator.streetId = elevatorForm1.streetId;

            eccpBaseElevator.installationAddress = elevatorForm2.installationAddress;
            eccpBaseElevator.latitude = elevatorForm2.latitude;
            eccpBaseElevator.longitude = elevatorForm2.longitude;

            eccpBaseElevator.eCCPBaseAnnualInspectionUnitId = elevatorForm3.eCCPBaseAnnualInspectionUnitId;
            eccpBaseElevator.eCCPBaseProductionCompanyId = elevatorForm3.eCCPBaseProductionCompanyId;
            eccpBaseElevator.eCCPBaseRegisterCompanyId = elevatorForm3.eCCPBaseRegisterCompanyId;
            eccpBaseElevator.eCCPDictElevatorStatusId = elevatorForm3.eCCPDictElevatorStatusId;
            eccpBaseElevator.eccpBaseElevatorBrandId = elevatorForm3.eccpBaseElevatorBrandId;
            eccpBaseElevator.eccpBaseElevatorModelId = elevatorForm3.eccpBaseElevatorModelId;
            eccpBaseElevator.eccpDictElevatorTypeId = elevatorForm3.eccpDictElevatorTypeId;
            eccpBaseElevator.eccpDictPlaceTypeId = elevatorForm3.eccpDictPlaceTypeId;

            eccpBaseElevator.name = elevatorForm4.name;
            eccpBaseElevator.installationDatetime = elevatorForm4.installationDatetime;
            eccpBaseElevator.machineNum = elevatorForm4.machineNum;


            createOrEditEccpBaseElevatorSubsidiaryInfoDto.CustomNum = elevatorForm4.CustomNum;
            createOrEditEccpBaseElevatorSubsidiaryInfoDto.Deadweight = elevatorForm4.Deadweight;
            createOrEditEccpBaseElevatorSubsidiaryInfoDto.FloorNumber = elevatorForm4.FloorNumber;
            createOrEditEccpBaseElevatorSubsidiaryInfoDto.GateNumber = elevatorForm4.GateNumber;
            createOrEditEccpBaseElevatorSubsidiaryInfoDto.ManufacturingLicenseNumber = elevatorForm4.ManufacturingLicenseNumber;
            createOrEditEccpBaseElevatorSubsidiaryInfoDto.RatedSpeed = elevatorForm4.RatedSpeed;

            eccpBaseElevator.createOrEditEccpBaseElevatorSubsidiaryInfoDto = createOrEditEccpBaseElevatorSubsidiaryInfoDto;

            _modalManager.setBusy(true);
            _eccpBaseElevatorsService.createOrEdit(
                eccpBaseElevator
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditEccpBaseElevatorModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);