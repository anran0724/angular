(function () {
    $(function () {

        var _$eccpMaintenanceTemplatesTable = $('#EccpMaintenanceTemplatesTable');
        var _eccpMaintenanceTemplatesService = abp.services.app.eccpMaintenanceTemplates;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplates.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplates.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplates.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplates/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplates/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceTemplateModal'
        });
        var _viewEccpMaintenanceTemplateModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplates/VieweccpMaintenanceTemplateModal',
            modalClass: 'ViewEccpMaintenanceTemplateModal'
        });


        var _addEccpMaintenanceTemplateNodes = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplates/EccpMaintenanceTemplateNodesTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplates/_EccpMaintenanceTemplateNodesTableModal.js',
            modalWidth: "1200px",
            modalClass: 'EccpMaintenanceTemplateNodesTableModal'
        });




        var dataTable = _$eccpMaintenanceTemplatesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTemplatesService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceTemplatesTableFilter').val(),
                        tempAllowFilter: $('#TempAllowFilterId').val(),
                        tempDenyFilter: $('#TempDenyFilterId').val(),
                        minTempNodeCountFilter: $('#MinTempNodeCountFilterId').val(),
                        maxTempNodeCountFilter: $('#MaxTempNodeCountFilterId').val(),
                        eccpDictMaintenanceTypeNameFilter: $('#EccpDictMaintenanceTypeNameFilterId').val(),
                        eccpDictElevatorTypeNameFilter: $('#EccpDictElevatorTypeNameFilterId').val()
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
                                    _viewEccpMaintenanceTemplateModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {
                                    _createOrEditModal.open({ id: data.record.eccpMaintenanceTemplate.id });
                                }
                            },
                            {
                                text: app.localize('AddEccpMaintenanceTemplateNodes'),
                                action: function (data) {
                                    _addEccpMaintenanceTemplateNodes.open({ id: data.record.eccpMaintenanceTemplate.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteEccpMaintenanceTemplate(data.record.eccpMaintenanceTemplate);
                                }

                            }]
                    }
                },
                {
                    targets: 1,
                    data: "eccpMaintenanceTemplate.tempName"
                },
                {
                    targets: 2,
                    data: "eccpMaintenanceTemplate.tempAllow"
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceTemplate.tempDeny"
                },
                {
                    targets: 4,
                    data: "eccpMaintenanceTemplate.tempNodeCount"
                },
                {
                    targets: 5,
                    data: "eccpDictMaintenanceTypeName"
                },
                {
                    targets: 6,
                    data: "eccpDictElevatorTypeName"
                }
            ]
        });


        function getEccpMaintenanceTemplates() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceTemplate(eccpMaintenanceTemplate) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceTemplatesService.delete({
                            id: eccpMaintenanceTemplate.id
                        }).done(function () {
                            getEccpMaintenanceTemplates(true);
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

        $('#CreateNewEccpMaintenanceTemplateButton').click(function () {
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eccpMaintenanceTemplatesService
                .getEccpMaintenanceTemplatesToExcel({
                    filter: $('#EccpMaintenanceTemplatesTableFilter').val(),
                    tempAllowFilter: $('#TempAllowFilterId').val(),
                    tempDenyFilter: $('#TempDenyFilterId').val(),
                    minTempNodeCountFilter: $('#MinTempNodeCountFilterId').val(),
                    maxTempNodeCountFilter: $('#MaxTempNodeCountFilterId').val(),
                    eccpDictMaintenanceTypeNameFilter: $('#EccpDictMaintenanceTypeNameFilterId').val(),
                    eccpDictElevatorTypeNameFilter: $('#EccpDictElevatorTypeNameFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEccpMaintenanceTemplateModalSaved', function () {
            getEccpMaintenanceTemplates();
        });
        _addEccpMaintenanceTemplateNodes.onClose(function () {            
            getEccpMaintenanceTemplates();
        });

        $('#GetEccpMaintenanceTemplatesButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTemplates();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceTemplates();
            }
        });

    });
})();