(function () {
    $(function () {

        var _$eccpMaintenanceTempWorkOrderActionLogsTable = $('#EccpMaintenanceTempWorkOrderActionLogsTable');
        var _eccpMaintenanceTempWorkOrderActionLogsService = abp.services.app.eccpMaintenanceTempWorkOrderActionLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrderActionLogs.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrderActionLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTempWorkOrderActionLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrderActionLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTempWorkOrderActionLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceTempWorkOrderActionLogModal'
        });

		 var _viewEccpMaintenanceTempWorkOrderActionLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTempWorkOrderActionLogs/VieweccpMaintenanceTempWorkOrderActionLogModal',
            modalClass: 'ViewEccpMaintenanceTempWorkOrderActionLogModal'
        });

        var dataTable = _$eccpMaintenanceTempWorkOrderActionLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTempWorkOrderActionLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceTempWorkOrderActionLogsTableFilter').val(),
					minCheckStateFilter: $('#MinCheckStateFilterId').val(),
					maxCheckStateFilter: $('#MaxCheckStateFilterId').val(),
					eccpMaintenanceTempWorkOrderTitleFilter: $('#EccpMaintenanceTempWorkOrderTitleFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val()
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
                                    _viewEccpMaintenanceTempWorkOrderActionLogModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpMaintenanceTempWorkOrderActionLog.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpMaintenanceTempWorkOrderActionLog(data.record.eccpMaintenanceTempWorkOrderActionLog);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpMaintenanceTempWorkOrderActionLog.remarks"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceTempWorkOrderActionLog.checkState"   
					},
					{
						targets: 3,
						 data: "eccpMaintenanceTempWorkOrderTitle" 
					},
					{
						targets: 4,
						 data: "userName" 
					}
            ]
        });


        function getEccpMaintenanceTempWorkOrderActionLogs() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceTempWorkOrderActionLog(eccpMaintenanceTempWorkOrderActionLog) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceTempWorkOrderActionLogsService.delete({
                            id: eccpMaintenanceTempWorkOrderActionLog.id
                        }).done(function () {
                            getEccpMaintenanceTempWorkOrderActionLogs(true);
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

        $('#CreateNewEccpMaintenanceTempWorkOrderActionLogButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpMaintenanceTempWorkOrderActionLogModalSaved', function () {
            getEccpMaintenanceTempWorkOrderActionLogs();
        });

		$('#GetEccpMaintenanceTempWorkOrderActionLogsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTempWorkOrderActionLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceTempWorkOrderActionLogs();
		  }
		});

    });
})();