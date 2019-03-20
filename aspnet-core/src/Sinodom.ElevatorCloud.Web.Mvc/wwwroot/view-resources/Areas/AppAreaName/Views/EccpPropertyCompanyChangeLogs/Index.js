(function () {
    $(function () {

        var _$eccpPropertyCompanyChangeLogsTable = $('#EccpPropertyCompanyChangeLogsTable');
        var _eccpPropertyCompanyChangeLogsService = abp.services.app.eccpPropertyCompanyChangeLogs;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'YYYY-MM-DD'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.EccpPropertyCompanyChangeLogs.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.EccpPropertyCompanyChangeLogs.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.EccpPropertyCompanyChangeLogs.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpPropertyCompanyChangeLogs/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpPropertyCompanyChangeLogs/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpPropertyCompanyChangeLogModal'
        });

		 var _viewEccpPropertyCompanyChangeLogModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpPropertyCompanyChangeLogs/VieweccpPropertyCompanyChangeLogModal',
            modalClass: 'ViewEccpPropertyCompanyChangeLogModal'
        });

        var dataTable = _$eccpPropertyCompanyChangeLogsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpPropertyCompanyChangeLogsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpPropertyCompanyChangeLogsTableFilter').val(),
					fieldNameFilter: $('#FieldNameFilterId').val(),
					oldValueFilter: $('#OldValueFilterId').val(),
					newValueFilter: $('#NewValueFilterId').val(),
					eCCPBasePropertyCompanyNameFilter: $('#ECCPBasePropertyCompanyNameFilterId').val()
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
                                    _viewEccpPropertyCompanyChangeLogModal.open({ data: data.record });
                                }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpPropertyCompanyChangeLog.fieldName"   
					},
					{
						targets: 2,
						 data: "eccpPropertyCompanyChangeLog.oldValue"   
					},
					{
						targets: 3,
						 data: "eccpPropertyCompanyChangeLog.newValue"   
					},
					{
						targets: 4,
                        data: "eccpBasePropertyCompanyName" 
					}
            ]
        });


        function getEccpPropertyCompanyChangeLogs() {
            dataTable.ajax.reload();
        }

        function deleteEccpPropertyCompanyChangeLog(eccpPropertyCompanyChangeLog) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpPropertyCompanyChangeLogsService.delete({
                            id: eccpPropertyCompanyChangeLog.id
                        }).done(function () {
                            getEccpPropertyCompanyChangeLogs(true);
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

        //$('#CreateNewEccpPropertyCompanyChangeLogButton').click(function () {
        //    _createOrEditModal.open();
        //});

		

        //abp.event.on('app.createOrEditEccpPropertyCompanyChangeLogModalSaved', function () {
        //    getEccpPropertyCompanyChangeLogs();
        //});

		$('#GetEccpPropertyCompanyChangeLogsButton').click(function (e) {
            e.preventDefault();
            getEccpPropertyCompanyChangeLogs();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpPropertyCompanyChangeLogs();
		  }
		});

    });
})();