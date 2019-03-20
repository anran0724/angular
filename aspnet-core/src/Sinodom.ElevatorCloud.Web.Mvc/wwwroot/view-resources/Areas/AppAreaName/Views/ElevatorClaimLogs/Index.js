(function () {
    $(function () {

        var _$elevatorClaimLogsTable = $('#ElevatorClaimLogsTable');
        var _elevatorClaimLogsService = abp.services.app.elevatorClaimLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.ElevatorClaimLogs.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.ElevatorClaimLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.ElevatorClaimLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ElevatorClaimLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/ElevatorClaimLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditElevatorClaimLogModal'
        });

		 var _viewElevatorClaimLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/ElevatorClaimLogs/ViewelevatorClaimLogModal',
            modalClass: 'ViewElevatorClaimLogModal'
        });

        var dataTable = _$elevatorClaimLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _elevatorClaimLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ElevatorClaimLogsTableFilter').val(),
					eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val()
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
                                    _viewElevatorClaimLogModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.elevatorClaimLog.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteElevatorClaimLog(data.record.elevatorClaimLog);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseElevatorName" 
					}
            ]
        });


        function getElevatorClaimLogs() {
            dataTable.ajax.reload();
        }

        function deleteElevatorClaimLog(elevatorClaimLog) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _elevatorClaimLogsService.delete({
                            id: elevatorClaimLog.id
                        }).done(function () {
                            getElevatorClaimLogs(true);
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

        $('#CreateNewElevatorClaimLogButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditElevatorClaimLogModalSaved', function () {
            getElevatorClaimLogs();
        });

		$('#GetElevatorClaimLogsButton').click(function (e) {
            e.preventDefault();
            getElevatorClaimLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getElevatorClaimLogs();
		  }
		});

    });
})();