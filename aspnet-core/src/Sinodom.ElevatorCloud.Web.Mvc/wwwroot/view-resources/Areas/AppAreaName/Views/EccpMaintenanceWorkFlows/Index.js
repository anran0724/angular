(function () {
    $(function () {

        var _$eccpMaintenanceWorkFlowsTable = $('#EccpMaintenanceWorkFlowsTable');
        var _eccpMaintenanceWorkFlowsService = abp.services.app.eccpMaintenanceWorkFlows;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkFlows.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkFlows.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkFlows.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkFlows/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkFlows/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceWorkFlowModal'
        });

		 var _viewEccpMaintenanceWorkFlowModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkFlows/VieweccpMaintenanceWorkFlowModal',
            modalClass: 'ViewEccpMaintenanceWorkFlowModal'
        });

        var dataTable = _$eccpMaintenanceWorkFlowsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkFlowsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceWorkFlowsTableFilter').val(),
					actionCodeValueFilter: $('#ActionCodeValueFilterId').val(),
					eccpMaintenanceTemplateNodeNodeNameFilter: $('#EccpMaintenanceTemplateNodeNodeNameFilterId').val(),
					eccpMaintenanceWorkTaskNameFilter: $('#EccpMaintenanceWorkTaskNameFilterId').val(),
					eccpDictMaintenanceWorkFlowStatusNameFilter: $('#EccpDictMaintenanceWorkFlowStatusNameFilterId').val()
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
                                    _viewEccpMaintenanceWorkFlowModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpMaintenanceWorkFlow.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpMaintenanceWorkFlow(data.record.eccpMaintenanceWorkFlow);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpMaintenanceWorkFlow.actionCodeValue"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceTemplateNodeNodeName" 
					},
					{
						targets: 3,
						 data: "eccpMaintenanceWorkTaskName" 
					},
					{
						targets: 4,
						 data: "eccpDictMaintenanceWorkFlowStatusName" 
					}
            ]
        });


        function getEccpMaintenanceWorkFlows() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceWorkFlow(eccpMaintenanceWorkFlow) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceWorkFlowsService.delete({
                            id: eccpMaintenanceWorkFlow.id
                        }).done(function () {
                            getEccpMaintenanceWorkFlows(true);
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

        $('#CreateNewEccpMaintenanceWorkFlowButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpMaintenanceWorkFlowModalSaved', function () {
            getEccpMaintenanceWorkFlows();
        });

		$('#GetEccpMaintenanceWorkFlowsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkFlows();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceWorkFlows();
		  }
		});

    });
})();