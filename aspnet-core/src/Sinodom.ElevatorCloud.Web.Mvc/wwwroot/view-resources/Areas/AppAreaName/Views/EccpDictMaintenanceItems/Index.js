(function () {
    $(function () {

        var _$eccpDictMaintenanceItemsTable = $('#EccpDictMaintenanceItemsTable');
        var _eccpDictMaintenanceItemsService = abp.services.app.eccpDictMaintenanceItems;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceItems.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceItems.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceItems.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceItems/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictMaintenanceItems/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictMaintenanceItemModal'
        });

		 var _viewEccpDictMaintenanceItemModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceItems/VieweccpDictMaintenanceItemModal',
            modalClass: 'ViewEccpDictMaintenanceItemModal'
        });

        var dataTable = _$eccpDictMaintenanceItemsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictMaintenanceItemsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictMaintenanceItemsTableFilter').val(),
					termCodeFilter: $('#TermCodeFilterId').val()
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
                                    _viewEccpDictMaintenanceItemModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictMaintenanceItem.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictMaintenanceItem(data.record.eccpDictMaintenanceItem);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictMaintenanceItem.name"   
					},
					{
						targets: 2,
						 data: "eccpDictMaintenanceItem.termCode"   
					},
					{
						targets: 3,
						 data: "eccpDictMaintenanceItem.disOrder"   
					},
					{
						targets: 4,
						 data: "eccpDictMaintenanceItem.termDesc"   
					}
            ]
        });


        function getEccpDictMaintenanceItems() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictMaintenanceItem(eccpDictMaintenanceItem) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictMaintenanceItemsService.delete({
                            id: eccpDictMaintenanceItem.id
                        }).done(function () {
                            getEccpDictMaintenanceItems(true);
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

        $('#CreateNewEccpDictMaintenanceItemButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictMaintenanceItemModalSaved', function () {
            getEccpDictMaintenanceItems();
        });

		$('#GetEccpDictMaintenanceItemsButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceItems();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictMaintenanceItems();
		  }
		});

    });
})();