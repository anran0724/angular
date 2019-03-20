(function () {
    $(function () {

        var _$eccpCompanyUserAuditLogsTable = $('#EccpCompanyUserAuditLogsTable');
        var _eccpCompanyUserAuditLogsService = abp.services.app.eccpCompanyUserAuditLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpCompanyUserAuditLogs.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpCompanyUserAuditLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpCompanyUserAuditLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyUserAuditLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpCompanyUserAuditLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpCompanyUserAuditLogModal'
        });

		 var _viewEccpCompanyUserAuditLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpCompanyUserAuditLogs/VieweccpCompanyUserAuditLogModal',
            modalClass: 'ViewEccpCompanyUserAuditLogModal'
        });

        var dataTable = _$eccpCompanyUserAuditLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpCompanyUserAuditLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpCompanyUserAuditLogsTableFilter').val(),
					checkStateFilter: $('#CheckStateFilterId').val(),
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
                                    _viewEccpCompanyUserAuditLogModal.open({ data: data.record });
                                }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpCompanyUserAuditLog.checkState"  ,
						render: function (checkState) {
							if (checkState) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 2,
						 data: "eccpCompanyUserAuditLog.remarks"   
					},
					{
						targets: 3,
						 data: "userName" 
					}
            ]
        });


        function getEccpCompanyUserAuditLogs() {
            dataTable.ajax.reload();
        }

        function deleteEccpCompanyUserAuditLog(eccpCompanyUserAuditLog) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpCompanyUserAuditLogsService.delete({
                            id: eccpCompanyUserAuditLog.id
                        }).done(function () {
                            getEccpCompanyUserAuditLogs(true);
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

        $('#CreateNewEccpCompanyUserAuditLogButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpCompanyUserAuditLogModalSaved', function () {
            getEccpCompanyUserAuditLogs();
        });

		$('#GetEccpCompanyUserAuditLogsButton').click(function (e) {
            e.preventDefault();
            getEccpCompanyUserAuditLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpCompanyUserAuditLogs();
		  }
		});

    });
})();