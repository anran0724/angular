(function () {
    $(function () {

        var _$eccpBaseElevatorLabelBindLogsTable = $('#EccpBaseElevatorLabelBindLogsTable');
        var _eccpBaseElevatorLabelBindLogsService = abp.services.app.eccpBaseElevatorLabelBindLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabelBindLogs.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabelBindLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpBaseElevatorLabelBindLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabelBindLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpBaseElevatorLabelBindLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpBaseElevatorLabelBindLogModal'
        });

		 var _viewEccpBaseElevatorLabelBindLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpBaseElevatorLabelBindLogs/VieweccpBaseElevatorLabelBindLogModal',
            modalClass: 'ViewEccpBaseElevatorLabelBindLogModal'
        });

        var dataTable = _$eccpBaseElevatorLabelBindLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpBaseElevatorLabelBindLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpBaseElevatorLabelBindLogsTableFilter').val(),
					minBindingTimeFilter: $('#MinBindingTimeFilterId').val(),
					maxBindingTimeFilter: $('#MaxBindingTimeFilterId').val(),
					eccpBaseElevatorNameFilter: $('#EccpBaseElevatorNameFilterId').val(),
					eccpDictLabelStatusNameFilter: $('#EccpDictLabelStatusNameFilterId').val()
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
                                    _viewEccpBaseElevatorLabelBindLogModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpBaseElevatorLabelBindLog.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpBaseElevatorLabelBindLog(data.record.eccpBaseElevatorLabelBindLog);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpBaseElevatorLabelBindLog.labelName"   
					},
					{
						targets: 2,
						 data: "eccpBaseElevatorLabelBindLog.localInformation"   
					},
					{
						targets: 3,
						 data: "eccpBaseElevatorLabelBindLog.bindingTime" ,
					render: function (bindingTime) {
						if (bindingTime) {
							return moment(bindingTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 4,
						 data: "eccpBaseElevatorLabelBindLog.binaryObjectsId"   
					},
					{
						targets: 5,
						 data: "eccpBaseElevatorLabelBindLog.elevatorLabelId"   
					},
					{
						targets: 6,
						 data: "eccpBaseElevatorName" 
					},
					{
						targets: 7,
						 data: "eccpDictLabelStatusName" 
					}
            ]
        });


        function getEccpBaseElevatorLabelBindLogs() {
            dataTable.ajax.reload();
        }

        function deleteEccpBaseElevatorLabelBindLog(eccpBaseElevatorLabelBindLog) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpBaseElevatorLabelBindLogsService.delete({
                            id: eccpBaseElevatorLabelBindLog.id
                        }).done(function () {
                            getEccpBaseElevatorLabelBindLogs(true);
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

        $('#CreateNewEccpBaseElevatorLabelBindLogButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpBaseElevatorLabelBindLogModalSaved', function () {
            getEccpBaseElevatorLabelBindLogs();
        });

		$('#GetEccpBaseElevatorLabelBindLogsButton').click(function (e) {
            e.preventDefault();
            getEccpBaseElevatorLabelBindLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpBaseElevatorLabelBindLogs();
		  }
		});

    });
})();