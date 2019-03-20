(function () {
    $(function () {

        var _$lanFlowStatusActionsTable = $('#LanFlowStatusActionsTable');
        var _lanFlowStatusActionsService = abp.services.app.lanFlowStatusActions;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Administration.LanFlowStatusActions.Create'),
            edit: abp.auth.hasPermission('Pages.Administration.LanFlowStatusActions.Edit'),
            'delete': abp.auth.hasPermission('Pages.Administration.LanFlowStatusActions.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/LanFlowStatusActions/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/LanFlowStatusActions/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditLanFlowStatusActionModal'
        });

		 var _viewLanFlowStatusActionModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/LanFlowStatusActions/ViewlanFlowStatusActionModal',
            modalClass: 'ViewLanFlowStatusActionModal'
        });

        var dataTable = _$lanFlowStatusActionsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _lanFlowStatusActionsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#LanFlowStatusActionsTableFilter').val(),
					statusValueFilter: $('#StatusValueFilterId').val(),
					statusNameFilter: $('#StatusNameFilterId').val(),
					actionNameFilter: $('#ActionNameFilterId').val(),
					actionCodeFilter: $('#ActionCodeFilterId').val(),
					userRoleCodeFilter: $('#UserRoleCodeFilterId').val(),
					argumentValueFilter: $('#ArgumentValueFilterId').val(),
					isStartProcessFilter: $('#IsStartProcessFilterId').val(),
					isEndProcessFilter: $('#IsEndProcessFilterId').val(),
					isAdoptFilter: $('#IsAdoptFilterId').val(),
					lanFlowSchemeSchemeNameFilter: $('#LanFlowSchemeSchemeNameFilterId').val()
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
                                    _viewLanFlowStatusActionModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.lanFlowStatusAction.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteLanFlowStatusAction(data.record.lanFlowStatusAction);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "lanFlowStatusAction.statusValue"   
					},
					{
						targets: 2,
						 data: "lanFlowStatusAction.statusName"   
					},
					{
						targets: 3,
						 data: "lanFlowStatusAction.actionName"   
					},
					{
						targets: 4,
						 data: "lanFlowStatusAction.actionDesc"   
					},
					{
						targets: 5,
						 data: "lanFlowStatusAction.actionCode"   
					},
					{
						targets: 6,
						 data: "lanFlowStatusAction.userRoleCode"   
					},
					{
						targets: 7,
						 data: "lanFlowStatusAction.argumentValue"   
					},
					{
						targets: 8,
						 data: "lanFlowStatusAction.isStartProcess"  ,
						render: function (isStartProcess) {
							if (isStartProcess) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 9,
						 data: "lanFlowStatusAction.isEndProcess"  ,
						render: function (isEndProcess) {
							if (isEndProcess) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 10,
						 data: "lanFlowStatusAction.isAdopt"  ,
						render: function (isAdopt) {
							if (isAdopt) {
								return '<div class="text-center"><i class="fa fa-check-circle m--font-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 11,
						 data: "lanFlowStatusAction.sortCode"   
					},
					{
						targets: 12,
						 data: "lanFlowSchemeSchemeName" 
					}
            ]
        });


        function getLanFlowStatusActions() {
            dataTable.ajax.reload();
        }

        function deleteLanFlowStatusAction(lanFlowStatusAction) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _lanFlowStatusActionsService.delete({
                            id: lanFlowStatusAction.id
                        }).done(function () {
                            getLanFlowStatusActions(true);
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

        $('#CreateNewLanFlowStatusActionButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditLanFlowStatusActionModalSaved', function () {
            getLanFlowStatusActions();
        });

		$('#GetLanFlowStatusActionsButton').click(function (e) {
            e.preventDefault();
            getLanFlowStatusActions();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getLanFlowStatusActions();
		  }
		});

    });
})();