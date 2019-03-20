(function () {
    $(function () {

        var _$eccpMaintenanceContractsStopTable = $('#EccpMaintenanceContractsStopTable');
        var _eccpMaintenanceContractsStopService = abp.services.app.eccpMaintenanceContractsStop;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            'recoveryContract': abp.auth.hasPermission('Pages.Administration.EccpMaintenanceContractsStop.RecoveryContract')
        };

        var _recoveryContractModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContractsStop/RecoveryContractModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContractsStop/_RecoveryContractModal.js',
            modalClass: 'RecoveryContractEccpMaintenanceContractModal'
        });

        var _viewEccpMaintenanceContractModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContractsStop/VieweccpMaintenanceContractModal',
            scriptUrl: abp.appPath + 'view-resources/Views/Shared/Views/_ViewStyle.js',
            modalClass: 'ViewStyleModal'
        });

        var dataTable = _$eccpMaintenanceContractsStopTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceContractsStopService.getAllStopMaintenanceContract,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceContractsTableFilter').val(),
                        minStartDateFilter: $('#MinStartDateFilterId').val(),
                        maxStartDateFilter: $('#MaxStartDateFilterId').val(),
                        minEndDateFilter: $('#MinEndDateFilterId').val(),
                        maxEndDateFilter: $('#MaxEndDateFilterId').val(),
                        eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                        eCCPBasePropertyCompanyNameFilter: $('#ECCPBasePropertyCompanyNameFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    width: 120,
                    targets: 0,
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
                                    _viewEccpMaintenanceContractModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('RecoveryContract'),
                                visible: function () {
                                    return _permissions.recoveryContract;
                                },
                                action: function (data) {
                                    recoveryContract(data.record.eccpMaintenanceContract.id);
                                }
                            }
                        ]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceContract.contractPictureDesc"

                },
                {
                    targets: 2,
                    data: "eccpMaintenanceContract.startDate",
                    render: function (endDate) {
                        if (endDate) {
                            return moment(endDate).format('L');
                        }
                        return "";
                    }

                },
                {
                    targets: 3,
                    data: "eccpMaintenanceContract.endDate",
                    render: function (startDate) {
                        if (startDate) {
                            return moment(startDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 4,
                    data: "eccpBaseMaintenanceCompanyName"
                },
                {
                    targets: 5,
                    data: "eccpBasePropertyCompanyName"
                },
                {
                    targets: 6,
                    data: "eccpMaintenanceContract.stopDate",
                    render: function (stopDate) {
                        if (stopDate) {
                            return moment(stopDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 7,
                    data: "eccpMaintenanceContract.stopContractRemarks"
                }
            ]
        });

        function recoveryContract(id) {
            _eccpMaintenanceContractsStopService.getAllECCPBaseElevator({
                maintenanceContractId: id
            }).done(function (data) {
                if (data.eccpBaseElevatorLookupTableDtoList.length > 0) {
                    if (data.problemElevatorNames !== "" && data.problemElevatorNames != null) {
                        abp.message.confirm(
                            app.localize('PartRecoveryContract') +"\r\n"+ data.problemElevatorNames,
                            function (isConfirmed) {
                                if (isConfirmed) {
                                    _recoveryContractModal.open({ id: id, elevators: data.eccpBaseElevatorLookupTableDtoList });
                                }
                            }
                        );
                    } else {
                        _recoveryContractModal.open({ id: id, elevators: data.eccpBaseElevatorLookupTableDtoList });
                    }
                } else {
                    abp.notify.error(app.localize('WholeRecoveryContract'));
                }
            });
        }

        function getEccpMaintenanceContractsStop() {
            dataTable.ajax.reload();
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

        abp.event.on('app.recoveryContractEccpMaintenanceContractModalSaved', function () {
            getEccpMaintenanceContractsStop();
        });

        $('#GetEccpMaintenanceContractsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceContractsStop();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceContractsStop();
            }
        });

    });
})();