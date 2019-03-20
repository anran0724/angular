(function () {
    $(function () {

        var _$eccpMaintenanceTemplateNodesTable = $('#EccpMaintenanceTemplateNodesTable');
        var _eccpMaintenanceTemplateNodesService = abp.services.app.eccpMaintenanceTemplateNodes;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplateNodes.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplateNodes.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceTemplateNodes.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplateNodes/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceTemplateNodes/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceTemplateNodeModal'
        });

		 var _viewEccpMaintenanceTemplateNodeModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceTemplateNodes/VieweccpMaintenanceTemplateNodeModal',
            modalClass: 'ViewEccpMaintenanceTemplateNodeModal'
        });

        var dataTable = _$eccpMaintenanceTemplateNodesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTemplateNodesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceTemplateNodesTableFilter').val(),
					nodeNameFilter: $('#NodeNameFilterId').val(),
					minNodeIndexFilter: $('#MinNodeIndexFilterId').val(),
					maxNodeIndexFilter: $('#MaxNodeIndexFilterId').val(),
					actionCodeFilter: $('#ActionCodeFilterId').val(),
					eccpMaintenanceTemplateTempNameFilter: $('#EccpMaintenanceTemplateTempNameFilterId').val(),
					eccpDictNodeTypeNameFilter: $('#EccpDictNodeTypeNameFilterId').val()
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
                                    _viewEccpMaintenanceTemplateNodeModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpMaintenanceTemplateNode.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpMaintenanceTemplateNode(data.record.eccpMaintenanceTemplateNode);
                            }
                        }]
                    }
                },
					{
						targets: 1,
                        data: "eccpMaintenanceTemplateNode.templateNodeName"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceTemplateNode.nodeIndex"   
					},
					{
						targets: 3,
						 data: "eccpMaintenanceTemplateNode.actionCode"   
					},
					{
						targets: 4,
                        data: "eccpMaintenanceNextNodeName" 
					},
					{
						targets: 5,
						 data: "eccpDictNodeTypeName" 
					}
            ]
        });


        function getEccpMaintenanceTemplateNodes() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceTemplateNode(eccpMaintenanceTemplateNode) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceTemplateNodesService.delete({
                            id: eccpMaintenanceTemplateNode.id
                        }).done(function () {
                            getEccpMaintenanceTemplateNodes(true);
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

        $('#CreateNewEccpMaintenanceTemplateNodeButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpMaintenanceTemplateNodeModalSaved', function () {
            getEccpMaintenanceTemplateNodes();
        });

		$('#GetEccpMaintenanceTemplateNodesButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTemplateNodes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceTemplateNodes();
		  }
		});

    });
})();