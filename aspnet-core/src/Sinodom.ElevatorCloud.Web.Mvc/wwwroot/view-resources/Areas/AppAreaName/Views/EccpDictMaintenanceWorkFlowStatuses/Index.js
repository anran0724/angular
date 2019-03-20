(function () {
    $(function () {

        var _$eccpDictMaintenanceWorkFlowStatusesTable = $('#EccpDictMaintenanceWorkFlowStatusesTable');
        var _eccpDictMaintenanceWorkFlowStatusesService = abp.services.app.eccpDictMaintenanceWorkFlowStatuses;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceWorkFlowStatuses.Create'),
            edit: abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceWorkFlowStatuses.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpDict.EccpDictMaintenanceWorkFlowStatuses.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceWorkFlowStatuses/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpDictMaintenanceWorkFlowStatuses/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpDictMaintenanceWorkFlowStatusModal'
        });

		 var _viewEccpDictMaintenanceWorkFlowStatusModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpDictMaintenanceWorkFlowStatuses/VieweccpDictMaintenanceWorkFlowStatusModal',
            modalClass: 'ViewEccpDictMaintenanceWorkFlowStatusModal'
        });

        var dataTable = _$eccpDictMaintenanceWorkFlowStatusesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpDictMaintenanceWorkFlowStatusesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpDictMaintenanceWorkFlowStatusesTableFilter').val()
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
                                    _viewEccpDictMaintenanceWorkFlowStatusModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpDictMaintenanceWorkFlowStatus.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpDictMaintenanceWorkFlowStatus(data.record.eccpDictMaintenanceWorkFlowStatus);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpDictMaintenanceWorkFlowStatus.name"   
					}
            ]
        });


        function getEccpDictMaintenanceWorkFlowStatuses() {
            dataTable.ajax.reload();
        }

        function deleteEccpDictMaintenanceWorkFlowStatus(eccpDictMaintenanceWorkFlowStatus) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpDictMaintenanceWorkFlowStatusesService.delete({
                            id: eccpDictMaintenanceWorkFlowStatus.id
                        }).done(function () {
                            getEccpDictMaintenanceWorkFlowStatuses(true);
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

        $('#CreateNewEccpDictMaintenanceWorkFlowStatusButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpDictMaintenanceWorkFlowStatusModalSaved', function () {
            getEccpDictMaintenanceWorkFlowStatuses();
        });

		$('#GetEccpDictMaintenanceWorkFlowStatusesButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceWorkFlowStatuses();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpDictMaintenanceWorkFlowStatuses();
		  }
		});

    });
})();