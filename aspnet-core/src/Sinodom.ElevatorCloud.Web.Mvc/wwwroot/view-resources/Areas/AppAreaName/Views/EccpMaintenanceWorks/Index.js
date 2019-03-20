(function () {
    $(function () {

        var _$eccpMaintenanceWorksTable = $('#EccpMaintenanceWorksTable');
        var _eccpMaintenanceWorksService = abp.services.app.eccpMaintenanceWorks;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorks.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorks.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorks.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorks/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorks/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceWorkModal'
        });

		 var _viewEccpMaintenanceWorkModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorks/VieweccpMaintenanceWorkModal',
            modalClass: 'ViewEccpMaintenanceWorkModal'
        });

        var dataTable = _$eccpMaintenanceWorksTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorksService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceWorksTableFilter').val(),
					eccpMaintenanceWorkOrderPlanCheckDateFilter: $('#EccpMaintenanceWorkOrderPlanCheckDateFilterId').val(),
					eccpMaintenanceTemplateNodeNodeNameFilter: $('#EccpMaintenanceTemplateNodeNodeNameFilterId').val()
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
                                    _viewEccpMaintenanceWorkModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpMaintenanceWork.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpMaintenanceWork(data.record.eccpMaintenanceWork);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpMaintenanceWork.taskName"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceWorkOrderPlanCheckDate" 
					},
					{
						targets: 3,
						 data: "eccpMaintenanceTemplateNodeNodeName" 
					}
            ]
        });


        function getEccpMaintenanceWorks() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceWork(eccpMaintenanceWork) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceWorksService.delete({
                            id: eccpMaintenanceWork.id
                        }).done(function () {
                            getEccpMaintenanceWorks(true);
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

        $('#CreateNewEccpMaintenanceWorkButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpMaintenanceWorkModalSaved', function () {
            getEccpMaintenanceWorks();
        });

		$('#GetEccpMaintenanceWorksButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorks();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceWorks();
		  }
		});

    });
})();