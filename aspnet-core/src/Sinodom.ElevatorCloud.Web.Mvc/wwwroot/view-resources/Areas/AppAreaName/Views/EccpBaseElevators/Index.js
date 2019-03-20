(function () {
    $(function () {

        var _$eccpBaseElevatorsTable = $('#EccpBaseElevatorsTable');
        var _eccpBaseElevatorsService = abp.services.app.eccpBaseElevators;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.Delete'),
            'viewWorkOrderEvaluations': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.ViewEvaluations'),
            'viewMaintenanceWorkOrders': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevators.ViewMaintenanceWorkOrders')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseElevatorModal'
        });

        var _viewEccpBaseElevatorModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/VieweccpBaseElevatorModal',
            modalClass: 'ViewEccpBaseElevatorModal'
        });

        //维保评价查看
        var _viewEccpMaintenanceWorkOrderEvaluations = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpMaintenanceWorkOrderEvaluationsTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpMaintenanceWorkOrderEvaluationsTableModal.js',
            modalWidth: "1200px",
            modalClass: 'ViewEccpMaintenanceWorkOrderEvaluationsTableModal'
        });

        //维保工单查看        var _viewEccpMaintenanceWorkOrders = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevators/EccpMaintenanceWorkOrdersTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevators/_EccpMaintenanceWorkOrdersTableModal.js',
            modalWidth: "1500px",
            modalClass: 'ViewEccpMaintenanceWorkOrdersTableModal'
        });


        var dataTable = _$eccpBaseElevatorsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorsService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpBaseElevatorsTableFilter').val(),
                        certificateNumFilter: $('#CertificateNumFilterId').val(),
                        machineNumFilter: $('#MachineNumFilterId').val(),
                        installationAddressFilter: $('#InstallationAddressFilterId').val(),
                        eccpDictElevatorTypeNameFilter: $('#EccpDictElevatorTypeNameFilterId').val(),
                        eCCPDictElevatorStatusNameFilter: $('#ECCPDictElevatorStatusNameFilterId').val(),
                        eCCPBaseCommunityNameFilter: $('#ECCPBaseCommunityNameFilterId').val(),
                        eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                        eccpBaseElevatorBrandNameFilter: $('#EccpBaseElevatorBrandNameFilterId').val(),
                        eccpBaseElevatorModelNameFilter: $('#EccpBaseElevatorModelNameFilterId').val(),
                        provinceNameFilter: $('#ProvinceNameFilterId').val(),
                        cityNameFilter: $('#CityNameFilterId').val(),
                        districtNameFilter: $('#DistrictNameFilterId').val(),
                        streetNameFilter: $('#StreetNameFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
                            {
                                text: app.localize('View'),
                                action: function (data) {
                                    _viewEccpBaseElevatorModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpBaseElevator.id });
                                }
                            },
                            //{
                            //    text: app.localize('Delete'),
                            //    visible: function () {
                            //        return _permissions.delete;
                            //    },
                            //    action: function (data) {
                            //        deleteEccpBaseElevator(data.record.eccpBaseElevator);
                            //    }
                            //},
                            {
                                text: app.localize('ViewWorkOrderEvaluations'),
                                visible: function (data) {
                                    return _permissions.viewWorkOrderEvaluations;
                                },
                                action: function (data) {
                                    _viewEccpMaintenanceWorkOrderEvaluations.open({ elevatorId: data.record.eccpBaseElevator.id });
                                }
                            },
                            {
                                text: app.localize('ViewMaintenanceWorkOrders'),
                                visible: function (data) {
                                    return _permissions.viewMaintenanceWorkOrders;
                                },
                                action: function (data) {
                                    _viewEccpMaintenanceWorkOrders.open({ elevatorId: data.record.eccpBaseElevator.id });
                                }
                            }
                        ]
                    }
                },
                {
                    targets: 2,
                    data: "eccpBaseElevator.name"
                },
                {
                    targets: 3,
                    data: "eccpBaseElevator.certificateNum"
                },
                {
                    targets: 4,
                    data: "eccpBaseElevator.machineNum"
                },
                {
                    targets: 5,
                    data: "eccpBaseElevator.installationAddress"
                },
                {
                    targets: 6,
                    data: "eccpBasePropertyCompanyName"
                },
                {
                    targets: 7,
                    data: "eccpBaseMaintenanceCompanyName"
                },
                {
                    targets: 8,
                    data: "provinceName"
                },
                {
                    targets: 9,
                    data: "cityName"
                },
                {
                    targets: 10,
                    data: "districtName"
                },
                {
                    targets: 11,
                    data: "streetName"
                },
                {
                    targets: 12,
                    data: "eccpBaseCommunityName"
                },
                {
                    targets: 13,
                    data: "eccpDictElevatorStatusName"
                },
                {
                    targets: 14,
                    data: "eccpDictElevatorTypeName"
                },
                {
                    targets: 15,
                    data: "eccpBaseElevatorBrandName"
                },
                {
                    targets: 16,
                    data: "eccpBaseElevatorModelName"
                },
                {
                    targets: 17,
                    data: "eccpBaseElevator.lastModificationTime",
                    render: function (lastModificationTime) {
                        if (lastModificationTime) {
                            return moment(lastModificationTime).format('L');
                        }
                        return "";
                    }
                }
            ]
        });


        function getEccpBaseElevators() {
            dataTable.ajax.reload();
        }

        function deleteEccpBaseElevator(eccpBaseElevator) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseElevatorsService.delete({
                            id: eccpBaseElevator.id
                        }).done(function () {
                            getEccpBaseElevators(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

        $('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewEccpBaseElevatorButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eccpBaseElevatorsService
                .getEccpBaseElevatorsToExcel({
                    filter: $('#EccpBaseElevatorsTableFilter').val(),
                    certificateNumFilter: $('#CertificateNumFilterId').val(),
                    machineNumFilter: $('#MachineNumFilterId').val(),
                    installationAddressFilter: $('#InstallationAddressFilterId').val(),
                    eccpDictElevatorTypeNameFilter: $('#EccpDictElevatorTypeNameFilterId').val(),
                    eCCPDictElevatorStatusNameFilter: $('#ECCPDictElevatorStatusNameFilterId').val(),
                    eCCPBaseCommunityNameFilter: $('#ECCPBaseCommunityNameFilterId').val(),
                    eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                    eccpBaseElevatorBrandNameFilter: $('#EccpBaseElevatorBrandNameFilterId').val(),
                    eccpBaseElevatorModelNameFilter: $('#EccpBaseElevatorModelNameFilterId').val(),
                    provinceNameFilter: $('#ProvinceNameFilterId').val(),
                    cityNameFilter: $('#CityNameFilterId').val(),
                    districtNameFilter: $('#DistrictNameFilterId').val(),
                    streetNameFilter: $('#StreetNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpBaseElevatorModalSaved', function () {
            getEccpBaseElevators();
        });

        $('#GetEccpBaseElevatorsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevators();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpBaseElevators();
            }
        });

    });
})();