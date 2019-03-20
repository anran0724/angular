(function () {
    $(function () {

        var _$eccpElevatorChangeLogsTable = $('#EccpElevatorChangeLogsTable');
        var _eccpElevatorChangeLogsService = abp.services.app.eccpElevatorChangeLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorChangeLogs.Create'),
            edit: abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorChangeLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpElevator.EccpElevatorChangeLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorChangeLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpElevatorChangeLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpElevatorChangeLogModal'
        });

		 var _viewEccpElevatorChangeLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpElevatorChangeLogs/VieweccpElevatorChangeLogModal',
            modalClass: 'ViewEccpElevatorChangeLogModal'
        });

        var dataTable = _$eccpElevatorChangeLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpElevatorChangeLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpElevatorChangeLogsTableFilter').val(),
					fieldNameFilter: $('#FieldNameFilterId').val(),
					oldValueFilter: $('#OldValueFilterId').val(),
					newValueFilter: $('#NewValueFilterId').val(),
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
                                    _viewEccpElevatorChangeLogModal.open({ data: data.record });
                                }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpElevatorChangeLog.fieldName"   
					},
					{
						targets: 2,
						 data: "eccpElevatorChangeLog.oldValue"   
					},
					{
						targets: 3,
						 data: "eccpElevatorChangeLog.newValue"   
					},
					{
						targets: 4,
						 data: "eccpBaseElevatorName" 
					}
            ]
        });


        function getEccpElevatorChangeLogs() {
            dataTable.ajax.reload();
        }

        //function deleteEccpElevatorChangeLog(eccpElevatorChangeLog) {
        //    abp.message.confirm(
        //        '',
        //        function (isConfirmed) {
        //            if (isConfirmed) {
        //                _eccpElevatorChangeLogsService.delete({
        //                    id: eccpElevatorChangeLog.id
        //                }).done(function () {
        //                    getEccpElevatorChangeLogs(true);
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

        //$('#CreateNewEccpElevatorChangeLogButton').click(function () {
        //    _createOrEditModal.open();
        //});

		

        //abp.event.on('app.createOrEditEccpElevatorChangeLogModalSaved', function () {
        //    getEccpElevatorChangeLogs();
        //});

		$('#GetEccpElevatorChangeLogsButton').click(function (e) {
            e.preventDefault();
            getEccpElevatorChangeLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpElevatorChangeLogs();
		  }
		});

    });
})();