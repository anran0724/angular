(function () {
    $(function () {

        var _$eccpDictMaintenanceStatusesTable = $('#EccpDictMaintenanceStatusesTable');
        var _eccpDictMaintenanceStatusesService = abp.services.app.eccpDictMaintenanceStatuses;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceStatuses.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceStatuses.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceStatuses.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceStatuses/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictMaintenanceStatuses/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictMaintenanceStatusModal'
        });

		 var _viewEccpDictMaintenanceStatusModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceStatuses/VieweccpDictMaintenanceStatusModal',
            modalClass: 'ViewEccpDictMaintenanceStatusModal'
        });

        var dataTable = _$eccpDictMaintenanceStatusesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictMaintenanceStatusesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictMaintenanceStatusesTableFilter').val()
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
                                    _viewEccpDictMaintenanceStatusModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictMaintenanceStatus.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictMaintenanceStatus(data.record.eccpDictMaintenanceStatus);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictMaintenanceStatus.name"   
					}
            ]
        });


        function getEccpDictMaintenanceStatuses() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictMaintenanceStatus(eccpDictMaintenanceStatus) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictMaintenanceStatusesService.delete({
                            id: eccpDictMaintenanceStatus.id
                        }).done(function () {
                            getEccpDictMaintenanceStatuses(true);
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

        $('#CreateNewEccpDictMaintenanceStatusButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictMaintenanceStatusModalSaved', function () {
            getEccpDictMaintenanceStatuses();
        });

		$('#GetEccpDictMaintenanceStatusesButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceStatuses();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictMaintenanceStatuses();
		  }
		});

    });
})();