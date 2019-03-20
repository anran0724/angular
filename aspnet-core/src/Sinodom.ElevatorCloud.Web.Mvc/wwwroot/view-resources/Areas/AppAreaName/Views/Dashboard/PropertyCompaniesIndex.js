(function () {
    $(function () {

        var type = 0;
        var areaType = 0;
        var _hostDashboardService = abp.services.app.tenantDashboard;
        var _$container = $("#HostDashboard");
        var _$dateRangePicker = _$container.find(".dashboard-report-range");


        var _$TempWorkOrderTable = _$container.find(".expiring-tenants-table");
        var _$MaintenancePlanTable = _$container.find(".maintenancePlan-table");
      


        var _$incomeStatisticsChartContainer = _$container.find(".income-statistics .income-statistics-chart");
        //var _$newTenantsStatusTitle = _$container.find(".new-tenants-statistics .status-title");        
        //var _$newSubscriptionAmountTitle = _$container.find(".new-subscription-statistics .status-title");

        //电梯总数
        var _$elevatorCountZ = _$container.find(".new-subscription-statistics .elevatorCountZ");
        //维保电梯数量
        var _$elevatorCount = _$container.find(".new-tenants-statistics .elevatorCount");
        //超期维保次数
        var _$exceedMaintenanceCount = _$container.find(".dashboard-statistics1 .exceedMaintenanceCount");      

        //电梯故障总数
        var _$faultElevatorCount = _$container.find(".new-subscription-statistics .faultElevatorCount");
        //临期维保电梯 总数
        var _$periodElevatorCount = _$container.find(".new-tenants-statistics .periodElevatorCount");
        //超期电梯总数
        var _$exceedElevatorCount = _$container.find(".dashboard-statistics1 .exceedElevatorCount");

        var _$refreshButton = _$container.find("button[name='RefreshButton']");

        var _tempWorkOrderList = [], _maintenancePlanList = [];

        var _selectedDateRange = {
            startDate: moment().add(-7, 'days').startOf('day'),
            endDate: moment().endOf("day")
        };

        var showSelectedDate = function () {
            if (_$dateRangePicker.attr("data-display-range") !== "0") {
                _$dateRangePicker.find(".m-subheader__daterange-date").html(_selectedDateRange.startDate.format("LL") +
                    " - " +
                    _selectedDateRange.endDate.format("LL"));
            }
        };

        var tempWorkOrderTable = function (tempWorkOrderList) {
            _tempWorkOrderList = tempWorkOrderList;
            _tempWorkOrderTable.ajax.reload();
        };
        var maintenancePlanTable = function (maintenancePlanList) {
            _maintenancePlanList = maintenancePlanList;

            _maintenancePlanTable.ajax.reload();
        };
      

        //var getCurrentDateRangeText = function () {
        //    return _selectedDateRange.startDate.format("L") + " - " + _selectedDateRange.endDate.format("L");
        //};
        //电梯总数
        var elevatorCountZ = function (count) {           
            _$elevatorCountZ.text(count);
        };
        //维保电梯数量
        var elevatorCount = function (count) {         
            _$elevatorCount.text(count);
        };
        //超期维保次数
        var exceedMaintenanceCount = function (count) {         
            _$exceedMaintenanceCount.text(count);
        };
        //电梯故障总数
        var faultElevatorCount = function (count) {            
            _$faultElevatorCount.text(count);
        };
        //临期维保电梯 总数
        var periodElevatorCount = function (count) {           
            _$periodElevatorCount.text(count);
        };
        //超期电梯总数
        var exceedElevatorCount = function (count) {            
            _$exceedElevatorCount.text(count);
        };



        //var writeNewTenantsCount = function (newTenantsCount) {
        //    _$newTenantsStatusTitle.text(getCurrentDateRangeText());
        //    _$newTenantsStatisticsCountPlaceholder.text(newTenantsCount);
        //};

        //var writeNewSubscriptionsAmount = function (newSubscriptionAmount) {
        //    _$newSubscriptionAmountTitle.text(getCurrentDateRangeText());
        //    _$newSubscriptionAmountPlaceholder.text(newSubscriptionAmount);
        //};

        ////this is a sample placeholder. You can put your own statistics here.
        //var writeDashboardPlaceholder1 = function (value) {
        //    _$dashboardStatisticsPlaceholder1.text(value);
        //};

        

        var getAllDataAndDrawCharts = function () {

            abp.ui.setBusy(_$container);
            _hostDashboardService
                .getPropertyCompaniesDashboardData()
                .done(function (result) {
                    //counts
                    //writeNewTenantsCount(result.elevatorCount);
                    //writeNewSubscriptionsAmount(result.maintenanceContractCount);
                    //writeDashboardPlaceholder1(result.userCount);
                    //writeDashboardPlaceholder2(result.afterMaintenanceWorkOrderCount, result.unfinishedMaintenanceElevatorCount);
                    //BeforeMaintenanceWorkOrderCount(result.beforeMaintenanceWorkOrderCount, result.completeMaintenanceElevatorCount);
          
            elevatorCountZ(result.propertyCompaniesMaintenanceStatistics.elevatorNum);
            elevatorCount(result.propertyCompaniesMaintenanceStatistics.maintenanceNum);
            exceedMaintenanceCount(result.propertyCompaniesMaintenanceStatistics.overdueMaintenanceNum);
            faultElevatorCount(result.propertyCompaniesMaintenanceStatistics.elevatorFaultNum);
            periodElevatorCount(result.propertyCompaniesMaintenanceStatistics.numberOfTemporaryMaintenance);
            //exceedElevatorCount(result.propertyCompaniesMaintenanceStatistics.elevatorCount);
                    //列表绑定
                    tempWorkOrderTable(result.maintenanceCompaniesList);
                    //maintenancePlanTable(result.maintenancePlanList);
                
                }).always(function () {
                    abp.ui.clearBusy(_$container);
                });
        };

        var refreshAllData = function () {
            showSelectedDate();
            getAllDataAndDrawCharts();
        };

        var refreshIncomeStatisticsData = function () {
            abp.ui.setBusy(_$incomeStatisticsChartContainer);
        };

        var initIncomeStatisticsDatePeriod = function () {
            _$container.find("input[name='IncomeStatisticsDatePeriod']").change(function () {
                if (!this.checked) {
                    return;
                }
                refreshIncomeStatisticsData();
            });
        };


        var _tempWorkOrderTable = null;
        var initTempWorkOrderTable = function () {
            _tempWorkOrderTable = _$TempWorkOrderTable.DataTable({
                paging: false,
                serverSide: false,
                processing: false,
                info: false,
                listAction: {
                    ajaxFunction: function () {
                        return $.Deferred(function ($dfd) {
                            $dfd.resolve({
                                "items": _tempWorkOrderList,
                                "totalCount": _tempWorkOrderList.length
                            });
                        });
                    }
                },
                columnDefs: [
                    {
                        targets: 0,
                        data: "name"
                    },
                    {
                        targets: 1,
                        data: "elevatorNum"
                    },
                    {
                        targets: 2,
                        data: "numberOfTemporaryMaintenance"
                    },
                    {
                        targets: 3,
                        data: "overdueNum"
                    }, {
                        targets: 4,
                        data: "numberOfMaintenanceCompleted"
                    }
                ]
            });
        };

      
        


        var _maintenancePlanTable = null;
        var initMaintenancePlanTable = function () {
            _maintenancePlanTable = _$MaintenancePlanTable.DataTable({
                paging: false,
                serverSide: false,
                processing: false,
                info: false,
                listAction: {
                    ajaxFunction: function () {
                        return $.Deferred(function ($dfd) {
                            $dfd.resolve({
                                "items": _maintenancePlanList,
                                "totalCount": _maintenancePlanList.length
                            });
                        });
                    }
                },
                columnDefs: [
                    {
                        targets: 0,
                        data: "eccpBaseElevatorNum",
                    },
                    {
                        targets: 1,
                        data: "maintenanceUserNameList",
                        render: function (maintenanceUserNameList) {
                            if (maintenanceUserNameList.length != 0) {
                                var names = "";
                                $.each(maintenanceUserNameList, function (i, _i) {
                                    names += "," + _i.maintenanceUserName;
                                })
                                names = names.substring(1, names.length);
                                if (names.length > 50) {
                                    names = names.substring(0, 50) + "...";
                                }
                                return names;
                            }
                            return "";
                        }
                    }
                ]
            });
        };


       

        

        var initialize = function () {
            initIncomeStatisticsDatePeriod();
            initTempWorkOrderTable();
            initMaintenancePlanTable();
            refreshAllData();
            mapInitialize();
        };

        _$dateRangePicker.daterangepicker(
            $.extend(true, app.createDateRangePickerOptions(), _selectedDateRange), function (start, end, label) {
                _selectedDateRange.startDate = start;
                _selectedDateRange.endDate = end;
                refreshAllData();
            });

        _$refreshButton.click(function () {
            refreshAllData();
        });

        var mapInitialize = function () {
            $.post("/AppAreaName/Dashboard/GetArea", {}, function (data) {
                var regionalCollection = [];
                if (data.result.regionalCollection) {
                    regionalCollection = data.result.regionalCollection;
                }
                if (regionalCollection.length != 0) {
                    type = data.result.type;
                    var minZoom = 0;
                    if (type == 1) {
                        minZoom = 6;
                    }
                    else if (type == 2) {
                        minZoom = 8;
                    }
                    else if (type == 3) {
                        minZoom = 12;
                    }
                    var map = new BMap.Map("allmap", { minZoom: minZoom, maxZoom: 19 });
                    var arrPoint = regionalCollection[0].point.split(",");
                    map.centerAndZoom(new BMap.Point(arrPoint[1], arrPoint[0]), (19 - minZoom));
                    map.enableScrollWheelZoom();
                    var i = 0;
                    for (; i < regionalCollection.length; i++) {
                        var arrPoint = regionalCollection[i].point.split(",");
                        var point = new BMap.Point(arrPoint[1], arrPoint[0]);
                        var title = regionalCollection[i].areaName;
                        var elevatorNumber = regionalCollection[i].elevatorNumber;
                        var content = title + "<br>" + elevatorNumber + "部";
                        //var marker = new BMap.Marker(point);

                        var circle = new BMap.Circle(point, 2000, { strokeColor: "#FF9800", strokeWeight: 2, strokeOpacity: 1, fillColor: '#FF9800' });//创建圆形覆盖

                        var opts = {
                            position: point,    // 指定文本标注所在的地理位置
                            offset: new BMap.Size(-20, -20)    //设置文本偏移量
                        }
                        var label = new BMap.Label(content, opts);  // 创建文本标注对象
                        label.setStyle({
                            color: "#FFF",
                            backgroundColor: 'transparent',//文本背景色
                            borderColor: 'transparent',//文本框边框色
                            fontSize: "12px",
                            height: "16px",
                            lineHeight: "16px",
                            fontFamily: "微软雅黑"
                        });
                        map.addOverlay(circle);
                        map.addOverlay(label);
                        circle.addEventListener("click", function (e) {
                            var p = e.target;
                            if (type == 1) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(8);
                                loadArea(swStr, neStr, 2, map);
                            }
                            else if (type == 2) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(12);
                                loadArea(swStr, neStr, 3, map);
                            }
                            else if (type == 3) {
                                //var map = new BMap.Map("allmap", { minZoom: minZoom, maxZoom: 19 });
                                //map.centerAndZoom(new BMap.Point(p.getCenter().lng, p.getCenter().lat), 12);
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(14);
                                loadArea(swStr, neStr, 4, map);
                            }
                            else if (type == 4) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(16);
                                loadElevator(swStr, neStr, map);
                            }

                        }
                        );
                    }
                    ///缩放事件
                    map.addEventListener("zoomend", function (e) {
                        i = 0;
                        map.clearOverlays();
                        var cp = map.getBounds();
                        var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                        var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                        var swStr = sw.lat + "," + sw.lng;//左下经纬度
                        var neStr = ne.lat + "," + ne.lng; //右上经纬度
                        var ZoomNum = map.getZoom();
                        //地图级别省6-7，市8-11，区12-13，园区14-15，电梯16-19  类型1，省  2，市  3，区 4，园区                          
                        console.log(ZoomNum);
                        if (ZoomNum > 5 && ZoomNum < 16) {
                            if (ZoomNum == 6 || ZoomNum == 7) {
                                areaType = 1;
                            }
                            else if (ZoomNum > 7 && ZoomNum < 12) {
                                areaType = 2;
                            }
                            else if (ZoomNum == 12 || ZoomNum == 13) {
                                areaType = 3;
                            }
                            else if (ZoomNum == 14 || ZoomNum == 15) {
                                areaType = 4;
                            }
                            loadArea(swStr, neStr, areaType, map);
                        }
                        else {
                            loadElevator(swStr, neStr, map);
                        }
                    });
                    ///拖动事件
                    //map.addEventListener("dragend", function (e) {
                    //    map.clearOverlays();
                    //    var cp = map.getBounds();
                    //    var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                    //    var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                    //    var swStr = sw.lat + "," + sw.lng;//左下经纬度
                    //    var neStr = ne.lat + "," + ne.lng; //右上经纬度
                    //    var ZoomNum = map.getZoom();
                    //    console.log(ZoomNum);
                    //    if (ZoomNum > 5 && ZoomNum < 16) {
                    //        if (ZoomNum == 6 || ZoomNum == 7) {
                    //            areaType = 1;
                    //        }
                    //        else if (ZoomNum > 7 && ZoomNum < 12) {
                    //            areaType = 2;
                    //        }
                    //        else if (ZoomNum == 12 || ZoomNum == 13) {
                    //            areaType = 3;
                    //        }
                    //        else if (ZoomNum == 14 || ZoomNum == 15) {
                    //            areaType = 4;
                    //        }
                    //        loadArea(swStr, neStr, areaType, map);
                    //    }
                    //    else {
                    //        loadElevator(swStr, neStr, map);
                    //    }
                    //});
                }
            })
        }
        loadArea = function (swStr, neStr, type, map) {
            $.post("/AppAreaName/Dashboard/GetArea", { TopLeft: swStr, BottomRight: neStr, Type: type }, function (data) {
                var regionalCollection = [];
                if (data.result.regionalCollection) {
                    regionalCollection = data.result.regionalCollection;
                }
                type = data.result.type;
                if (regionalCollection.length != 0) {
                    var i = 0;
                    for (; i < regionalCollection.length; i++) {
                        var arrPoint = regionalCollection[i].point.split(",");
                        var point = new BMap.Point(arrPoint[1], arrPoint[0]);
                        var title = regionalCollection[i].areaName;
                        var elevatorNumber = regionalCollection[i].elevatorNumber;
                        var content = title + "：" + elevatorNumber;
                        var circle = new BMap.Circle(point, 2000, { strokeColor: "#FF9800", strokeWeight: 2, strokeOpacity: 1, fillColor: '#FF9800' });//创建圆形覆盖

                        var opts = {
                            position: point,    // 指定文本标注所在的地理位置
                            offset: new BMap.Size(-20, -20)    //设置文本偏移量
                        }
                        var label = new BMap.Label(content, opts);  // 创建文本标注对象
                        label.setStyle({
                            color: "#FFF",
                            backgroundColor: 'transparent',//文本背景色
                            borderColor: 'transparent',//文本框边框色
                            fontSize: "12px",
                            height: "16px",
                            lineHeight: "16px",
                            fontFamily: "微软雅黑"
                        });
                        map.addOverlay(circle);
                        map.addOverlay(label);
                        circle.addEventListener("click", function (e) {
                            var p = e.target;
                            if (type == 1) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(8);
                                loadArea(swStr, neStr, 2, map);
                            }
                            else if (type == 2) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(12);
                                loadArea(swStr, neStr, 3, map);
                            }
                            else if (type == 3) {
                                //var map = new BMap.Map("allmap", { minZoom: minZoom, maxZoom: 19 });
                                //map.centerAndZoom(new BMap.Point(p.getCenter().lng, p.getCenter().lat), 12);
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(14);
                                loadArea(swStr, neStr, 4, map);
                            }
                            else if (type == 4) {
                                var cp = map.getBounds();
                                var sw = cp.getSouthWest();//获取西南角的经纬度(左下端点)
                                var ne = cp.getNorthEast(); //获取东北角的经纬度(右上端点)
                                var swStr = sw.lat + "," + sw.lng;//左下经纬度
                                var neStr = ne.lat + "," + ne.lng; //右上经纬度
                                map.zoomTo(16);
                                loadElevator(swStr, neStr, map);
                            }

                        }
                        );
                    }
                }
            })
        }
        loadElevator = function (swStr, neStr, map) {
            $.post("/AppAreaName/Dashboard/GetElevator", { TopLeft: swStr, BottomRight: neStr }, function (data) {
                var regionalCollection = [];
                if (data.result.regionalCollection) {
                    regionalCollection = data.result.regionalCollection;
                }
                if (elevatorCollection.length != 0) {
                    var i = 0;
                    for (; i < elevatorCollection.length; i++) {
                        //var arrPoint = elevatorCollection[i].point.split(",");
                        var myIcon;
                        if (elevatorCollection[i].maintenanceStatus == "待维保") {
                            myIcon = new BMap.Icon("/Common/Images/d.png", new BMap.Size(300, 157));
                        }
                        else if (elevatorCollection[i].maintenanceStatus == "临期维保") {
                            myIcon = new BMap.Icon("/Common/Images/l.png", new BMap.Size(300, 157));
                        }
                        else if (elevatorCollection[i].maintenanceStatus == "超期维保") {
                            myIcon = new BMap.Icon("/Common/Images/c.png", new BMap.Size(300, 157));
                        }
                        else if (elevatorCollection[i].maintenanceStatus == "已维保") {
                            myIcon = new BMap.Icon("/Common/Images/y.png", new BMap.Size(300, 157));
                        } else {
                            myIcon = new BMap.Icon("/Common/Images/d.png", new BMap.Size(300, 157));
                        }
                        var point = new BMap.Point(elevatorCollection[i].longitude, elevatorCollection[i].latitude);
                        var marker = new BMap.Marker(point, { icon: myIcon });
                        var certificateNum = elevatorCollection[i].certificateNum;
                        var maintenanceStatus = elevatorCollection[i].maintenanceStatus;
                        if (maintenanceStatus == "") {
                            maintenanceStatus = "未维保";
                        }
                        map.addOverlay(marker);
                        var content = "20位唯一注册码：" + certificateNum + "  维保状态：" + maintenanceStatus;
                        addClickHandler(content, marker, map);
                    }
                }
            })
        }
        function addClickHandler(content, marker, map) {
            marker.addEventListener("click", function (e) {
                openInfo(content, e, map);
            }
            );
        }
        var opts = {
            width: 250,     // 信息窗口宽度
            height: 80,     // 信息窗口高度
            title: "信息窗口", // 信息窗口标题
            enableMessage: true//设置允许信息窗发送短息
        };
        function openInfo(content, e, map) {
            var p = e.target;
            var point = new BMap.Point(p.getPosition().lng, p.getPosition().lat);
            var infoWindow = new BMap.InfoWindow(content, opts);  // 创建信息窗口对象 
            map.openInfoWindow(infoWindow, point); //开启信息窗口
        }

        initialize();
    });
})();
