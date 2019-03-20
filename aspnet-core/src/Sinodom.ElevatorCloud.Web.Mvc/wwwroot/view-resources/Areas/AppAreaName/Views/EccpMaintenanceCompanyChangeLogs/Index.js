(function () {
    $(function () {

        var _$eccpMaintenanceCompanyChangeLogsTable = $('#EccpMaintenanceCompanyChangeLogsTable');
        var _eccpMaintenanceCompanyChangeLogsService = abp.services.app.eccpMaintenanceCompanyChangeLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpMaintenanceCompanyChangeLogs.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpMaintenanceCompanyChangeLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpMaintenanceCompanyChangeLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceCompanyChangeLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceCompanyChangeLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceCompanyChangeLogModal'
        });

		 var _viewEccpMaintenanceCompanyChangeLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceCompanyChangeLogs/VieweccpMaintenanceCompanyChangeLogModal',
            modalClass: 'ViewEccpMaintenanceCompanyChangeLogModal'
        });

        var dataTable = _$eccpMaintenanceCompanyChangeLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceCompanyChangeLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceCompanyChangeLogsTableFilter').val(),
					fieldNameFilter: $('#FieldNameFilterId').val(),
					oldValueFilter: $('#OldValueFilterId').val(),
					newValueFilter: $('#NewValueFilterId').val(),
					eCCPBaseMaintenanceCompanyNameFilter: $('#ECCPBaseMaintenanceCompanyNameFilterId').val()
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
                                    _viewEccpMaintenanceCompanyChangeLogModal.open({ data: data.record });
                                }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpMaintenanceCompanyChangeLog.fieldName"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceCompanyChangeLog.oldValue"   
					},
					{
						targets: 3,
						 data: "eccpMaintenanceCompanyChangeLog.newValue"   
					},
					{
						targets: 4,
						 data: "eccpBaseMaintenanceCompanyName" 
					}
            ]
        });


        function getEccpMaintenanceCompanyChangeLogs() {
            dataTable.ajax.reload();
        }

        //function deleteEccpMaintenanceCompanyChangeLog(eccpMaintenanceCompanyChangeLog) {
        //    abp.message.confirm(
        //        '',
        //        function (isConfirmed) {
        //            if (isConfirmed) {
        //                _eccpMaintenanceCompanyChangeLogsService.delete({
        //                    id: eccpMaintenanceCompanyChangeLog.id
        //                }).done(function () {
        //                    getEccpMaintenanceCompanyChangeLogs(true);
        //                    abp.notify.success(app.localize('SuccessfullyDeleted'));
        //                });
        //            }
        //        }
        //    );
        //}

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

        //$('#CreateNewEccpMaintenanceCompanyChangeLogButton').click(function () {
        //    _createOrEditModal.open();
        //});

		

        //abp.event.on('app.createOrEditEccpMaintenanceCompanyChangeLogModalSaved', function () {
        //    getEccpMaintenanceCompanyChangeLogs();
        //});

		$('#GetEccpMaintenanceCompanyChangeLogsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceCompanyChangeLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceCompanyChangeLogs();
		  }
		});

    });
})();