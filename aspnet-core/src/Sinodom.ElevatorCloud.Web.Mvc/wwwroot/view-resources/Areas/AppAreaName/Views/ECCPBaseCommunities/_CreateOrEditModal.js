(function ($) {
    app.modals.CreateOrEditECCPBaseCommunityModal = function () {
        var self = this;
        var _eCCPBaseCommunitiesService = abp.services.app.eCCPBaseCommunities;
       
        var _modalManager;
        var _$eCCPBaseCommunityInformationForm = null;

        var _eCCPBaseAreaLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseCommunities/ECCPBaseAreaLookupTableModal',

            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseCommunities/_ECCPBaseAreaLookupTableModal.js',

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
            var point = new BMap.Point(116.332117, 39.945034);//定义一个中心点坐标              
            map.centerAndZoom(point, 11);//设定地图的中心点和坐标并将地图显示在地图容器中
            window.map = map;//将map变量存储在全局   
            map.enableScrollWheelZoom();
            map.addEventListener("click", showInfo);
        }
        function showInfo(e) {
            var geoc = new BMap.Geocoder();
            var pt = e.point;
            geoc.getLocation(pt, function (rs) {
                //addressComponents对象可以获取到详细的地址信息
                var addComp = rs.addressComponents;
                var site = addComp.province + ", " + addComp.city + ", " + addComp.district + ", " + addComp.street + ", " + addComp.streetNumber;
                //将对应的HTML元素设置值
                $("#ECCPBaseCommunity_Address").val(site);
            });
            setMapValue(e.point);
        }

        function setMapValue(point) {
            map.centerAndZoom(point, map.getZoom());
            map.clearOverlays();
            var marker = new BMap.Marker(point, { icon: myIcon });
            map.addOverlay(marker);
            $("#ECCPBaseCommunity_Longitude").val(point.lng);
            $("#ECCPBaseCommunity_Latitude").val(point.lat);
        }

        this.init = function (modalManager) {
            _modalManager = modalManager;

            var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$eCCPBaseCommunityInformationForm = _modalManager.getModal().find('form[name=ECCPBaseCommunityInformationsForm]');
            _$eCCPBaseCommunityInformationForm.validate();           
            LoadMap();
            console.log("ECCPBaseCommunity_Longitude");
            map.addEventListener("tilesloaded", function () {
                var longitude = $("#ECCPBaseCommunity_Longitude").val();
                var latitude = $("#ECCPBaseCommunity_Latitude").val();
                if (longitude != "" && latitude != "") {
                    var new_point = new BMap.Point(longitude, latitude);
                    var marker = new BMap.Marker(new_point);
                    map.addOverlay(marker);
                    map.panTo(new_point);
                };
            });         
           
        };



        $('#OpenProvinceNameLookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eCCPBaseCommunity = _$eCCPBaseCommunityInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseCommunity.provinceId, displayName: eCCPBaseCommunity.provinceName, parentId: 0 },
                function (data) {
                    _$eCCPBaseCommunityInformationForm.find('input[name=provinceName]').val(data.displayName);
                    _$eCCPBaseCommunityInformationForm.find('input[name=provinceId]').val(data.id);
                    if (data.id !== eCCPBaseCommunity.provinceId) {
                        self.childSelectionHandel(childBtn, true);
                    }
                    self.childSelectionHandel(childBtn, false);
                });
        });

        $('#ClearProvinceNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseCommunityInformationForm.find('input[name=provinceName]').val('');
            _$eCCPBaseCommunityInformationForm.find('input[name=provinceId]').val('');
            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenCityNameLookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eCCPBaseCommunity = _$eCCPBaseCommunityInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseCommunity.cityId, displayName: eCCPBaseCommunity.cityName, parentId: eCCPBaseCommunity.provinceId },
                function (data) {
                    _$eCCPBaseCommunityInformationForm.find('input[name=cityName]').val(data.displayName);
                    _$eCCPBaseCommunityInformationForm.find('input[name=cityId]').val(data.id);
                    if (data.id !== eCCPBaseCommunity.cityId) {
                        self.childSelectionHandel(childBtn, true);
                    }
                    self.childSelectionHandel(childBtn, false);
                    map.centerAndZoom(data.displayName, 11); 
                });
        });

        $('#ClearCityNameButton').click(function () {
            var childBtn = $(this).attr('data-child');
            _$eCCPBaseCommunityInformationForm.find('input[name=cityName]').val('');
            _$eCCPBaseCommunityInformationForm.find('input[name=cityId]').val('');
            self.childSelectionHandel(childBtn, true);
        });

        $('#OpenDistrictNameLookupTableButton').click(function () {
            var childBtn = $(this).attr('data-child');
            var eCCPBaseCommunity = _$eCCPBaseCommunityInformationForm.serializeFormToObject();

            _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseCommunity.districtId, displayName: eCCPBaseCommunity.districtName, parentId: eCCPBaseCommunity.cityId },
                function (data) {
                    _$eCCPBaseCommunityInformationForm.find('input[name=districtName]').val(data.displayName);
                    _$eCCPBaseCommunityInformationForm.find('input[name=districtId]').val(data.id);
                    if (data.id !== eCCPBaseCommunity.districtId) {
                        self.childSelectionHandel(childBtn, true);
                    }
                    self.childSelectionHandel(childBtn, false);
                });
        });

        $('#ClearDistrictNameButton').click(function () {
            //var childBtn = $(this).attr('data-child');
            _$eCCPBaseCommunityInformationForm.find('input[name=districtName]').val('');
            _$eCCPBaseCommunityInformationForm.find('input[name=districtId]').val('');
            //self.childSelectionHandel(childBtn, true);
        });

        //      $('#OpenStreetNameLookupTableButton').click(function () {
        //          var eCCPBaseCommunity = _$eCCPBaseCommunityInformationForm.serializeFormToObject();

        //          _eCCPBaseAreaLookupTableModal.open({ id: eCCPBaseCommunity.streetId, displayName: eCCPBaseCommunity.streetName, parentId: eCCPBaseCommunity.districtId},
        //          function (data) {
        //              _$eCCPBaseCommunityInformationForm.find('input[name=streetName]').val(data.displayName); 
        //              _$eCCPBaseCommunityInformationForm.find('input[name=streetId]').val(data.id); 
        //          });
        //      });

        //$('#ClearStreetNameButton').click(function () {
        //              _$eCCPBaseCommunityInformationForm.find('input[name=streetName]').val(''); 
        //              _$eCCPBaseCommunityInformationForm.find('input[name=streetId]').val(''); 
        //      });

        this.childSelectionHandel = function (childbtn, isClear) {
            if (isClear) {
                $("#" + childbtn).attr("disabled", true);
                $("#" + childbtn).parent().next().find('button').click();
            } else {
                $("#" + childbtn).removeAttr("disabled");
            }
        }  

        $("#ECCPBaseCommunity_Address").keyup(function () {
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


        this.save = function () {
            if (!_$eCCPBaseCommunityInformationForm.valid()) {
                return;
            }

            var eCCPBaseCommunity = _$eCCPBaseCommunityInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _eCCPBaseCommunitiesService.createOrEdit(
                eCCPBaseCommunity
            ).done(function () {
                abp.notify.info(app.localize('SavedSuccessfully'));
                _modalManager.close();
                abp.event.trigger('app.createOrEditECCPBaseCommunityModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);