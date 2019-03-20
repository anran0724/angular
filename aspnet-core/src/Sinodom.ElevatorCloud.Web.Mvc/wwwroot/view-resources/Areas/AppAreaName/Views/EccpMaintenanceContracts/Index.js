(function () {
    $(function () {

        var _$eccpMaintenanceContractsTable = $('#EccpMaintenanceContractsTable');
        var _eccpMaintenanceContractsService = abp.services.app.eccpMaintenanceContracts;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpMaintenanceContracts.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpMaintenanceContracts.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpMaintenanceContracts.Delete'),
            'stopContract': abp.auth.hasPermission('Pages.Administration.EccpMaintenanceContracts.StopContract')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContracts/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContracts/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceContractModal'
        });

        var _stopContractModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContracts/StopContractModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceContracts/_StopContractModal.js',
            modalClass: 'StopContractEccpMaintenanceContractModal'
        });

        var _viewEccpMaintenanceContractModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceContracts/VieweccpMaintenanceContractModal',
            scriptUrl: abp.appPath + 'view-resources/Views/Shared/Views/_ViewStyle.js',
            modalClass: 'ViewStyleModal'
        });

        var dataTable = _$eccpMaintenanceContractsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceContractsService.getAll,
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
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpMaintenanceContract.id });
                                }
                            },
                            {
                                text: app.localize('StopContract'),
                                visible: function () {
                                    return _permissions.stopContract;
                                },
                                action: function (data) {
                                    _stopContractModal.open({ id: data.record.eccpMaintenanceContract.id });
                                }
                            }
                            //,
                            //{
                            //    text: app.localize('Delete'),
                            //    visible: function (data) {
                            //        if (data.record.eccpMaintenanceContract.stopContractRemarks != null &&
                            //            data.record.eccpMaintenanceContract.stopContractRemarks !== "") {
                            //            return false;
                            //        } else {
                            //            return _permissions.delete;
                            //        }
                            //    },
                            //    action: function (data) {
                            //        deleteEccpMaintenanceContract(data.record.eccpMaintenanceContract);
                            //    }
                            //}
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
                }
            ]
        });


        function getEccpMaintenanceContracts() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceContract(eccpMaintenanceContract) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceContractsService.delete({
                            id: eccpMaintenanceContract.id
                        }).done(function () {
                            getEccpMaintenanceContracts(true);
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

        $('#CreateNewEccpMaintenanceContractButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eccpMaintenanceContractsService
                .getEccpMaintenanceContractsToExcel({
                    filter: $('#EccpMaintenanceContractsTableFilter').val(),
                    minStartDateFilter: $('#MinStartDateFilterId').val(),
                    maxStartDateFilter: $('#MaxStartDateFilterId').val(),
                    minEndDateFilter: $('#MinEndDateFilterId').val(),
                    maxEndDateFilter: $('#MaxEndDateFilterId').val(),
                    eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val(),
                    eCCPBasePropertyCompanyNameFilter: $('#ECCPBasePropertyCompanyNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpMaintenanceContractModalSaved', function () {
            getEccpMaintenanceContracts();
        });

        abp.event.on('app.stopContractEccpMaintenanceContractModalSaved', function () {
            getEccpMaintenanceContracts();
        });

        $('#GetEccpMaintenanceContractsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceContracts();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceContracts();
            }
        });

    });
})();