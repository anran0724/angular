(function () {
    $(function () {

        var _$eCCPBaseAreasTable = $('#ECCPBaseAreasTable');
        var _eCCPBaseAreasService = abp.services.app.eCCPBaseAreas;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpBase.EccpBaseAreas.Create'),
            edit: abp.auth.hasPermission('Pages.EccpBase.EccpBaseAreas.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpBase.EccpBaseAreas.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAreas/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ECCPBaseAreas/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditECCPBaseAreaModal'
        });

        var _viewECCPBaseAreaModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ECCPBaseAreas/VieweCCPBaseAreaModal',
            modalClass: 'ViewECCPBaseAreaModal'
        });

        var dataTable = _$eCCPBaseAreasTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eCCPBaseAreasService.getAll,
                inputFilter: function () {
                    return {
                        filter: $('#ECCPBaseAreasTableFilter').val(),
                        minParentIdFilter: $('#MinParentIdFilterId').val(),
                        maxParentIdFilter: $('#MaxParentIdFilterId').val(),
                        codeFilter: $('#CodeFilterId').val(),
                        nameFilter: $('#NameFilterId').val(),
                        minLevelFilter: $('#MinLevelFilterId').val(),
                        maxLevelFilter: $('#MaxLevelFilterId').val()
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
                                    _viewECCPBaseAreaModal.open({ data: data.record });
                                }
                            },
                            {
                                text: app.localize('Edit'),
                                visible: function () {
                                    return _permissions.edit;
                                },
                                action: function (data) {                                  
                                    _createOrEditModal.open({ id: data.record.eccpBaseArea.id });
                                }
                            },
                            {
                                text: app.localize('Delete'),
                                visible: function () {
                                    return _permissions.delete;
                                },
                                action: function (data) {
                                    deleteECCPBaseArea(data.record.eccpBaseArea);
                                }
                            }]
                    }
                },
					{
						targets: 1,
                        data: "eccpBaseArea.parentId"   
					},
					{
						targets: 2,
                        data: "eccpBaseArea.code"   
					},
					{
						targets: 3,
                        data: "eccpBaseArea.name"   
					},
					{
						targets: 4,
                        data: "eccpBaseArea.level"   
					}
            ]
        });


        function getECCPBaseAreas() {
            dataTable.ajax.reload();
        }

        function deleteECCPBaseArea(eccpBaseArea) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eCCPBaseAreasService.delete({
                            id: eccpBaseArea.id
                        }).done(function () {
                            getECCPBaseAreas(true);
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

        $('#CreateNewECCPBaseAreaButton').click(function () {           
            _createOrEditModal.open();
        });

        $('#ExportToExcelButton').click(function () {
            _eCCPBaseAreasService
                .getECCPBaseAreasToExcel({
                    filter: $('#ECCPBaseAreasTableFilter').val(),
                    minParentIdFilter: $('#MinParentIdFilterId').val(),
                    maxParentIdFilter: $('#MaxParentIdFilterId').val(),
                    codeFilter: $('#CodeFilterId').val(),
                    nameFilter: $('#NameFilterId').val(),
                    minLevelFilter: $('#MinLevelFilterId').val(),
                    maxLevelFilter: $('#MaxLevelFilterId').val()
                })
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditECCPBaseAreaModalSaved', function () {
            getECCPBaseAreas();
        });

        $('#GetECCPBaseAreasButton').click(function (e) {
            e.preventDefault();
            getECCPBaseAreas();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getECCPBaseAreas();
            }
        });

    });
})();