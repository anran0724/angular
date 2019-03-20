(function () {
    $(function () {

        var _$eccpMaintenanceWorkOrderEvaluationsTable = $('#EccpMaintenanceWorkOrderEvaluationsTable');
        var _eccpMaintenanceWorkOrderEvaluationsService = abp.services.app.eccpMaintenanceWorkOrderEvaluations;

        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrderEvaluations.Create'),
            edit: abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrderEvaluations.Edit'),
            'delete': abp.auth.hasPermission('Pages.EccpMaintenance.EccpMaintenanceWorkOrderEvaluations.Delete')
        };

        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrderEvaluations/CreateOrEditModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/AppAreaName/Views/EccpMaintenanceWorkOrderEvaluations/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditEccpMaintenanceWorkOrderEvaluationModal'
        });

		 var _viewEccpMaintenanceWorkOrderEvaluationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'AppAreaName/EccpMaintenanceWorkOrderEvaluations/VieweccpMaintenanceWorkOrderEvaluationModal',
            modalClass: 'ViewEccpMaintenanceWorkOrderEvaluationModal'
        });

        var dataTable = _$eccpMaintenanceWorkOrderEvaluationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrderEvaluationsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EccpMaintenanceWorkOrderEvaluationsTableFilter').val(),
					minRankFilter: $('#MinRankFilterId').val(),
					maxRankFilter: $('#MaxRankFilterId').val(),
					eccpMaintenanceWorkOrderRemarkFilter: $('#EccpMaintenanceWorkOrderRemarkFilterId').val()
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
                                    _viewEccpMaintenanceWorkOrderEvaluationModal.open({ data: data.record });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                                _createOrEditModal.open({ id: data.record.eccpMaintenanceWorkOrderEvaluation.id });
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteEccpMaintenanceWorkOrderEvaluation(data.record.eccpMaintenanceWorkOrderEvaluation);
                            }
                        }]
                    }
                },
					{
						targets: 1,
						 data: "eccpMaintenanceWorkOrderEvaluation.rank"   
					},
					{
						targets: 2,
						 data: "eccpMaintenanceWorkOrderEvaluation.remarks"   
					},
					{
						targets: 3,
						 data: "eccpMaintenanceWorkOrderRemark" 
					}
            ]
        });


        function getEccpMaintenanceWorkOrderEvaluations() {
            dataTable.ajax.reload();
        }

        function deleteEccpMaintenanceWorkOrderEvaluation(eccpMaintenanceWorkOrderEvaluation) {
            abp.message.confirm(
                '',
                function (isConfirmed) {
                    if (isConfirmed) {
                        _eccpMaintenanceWorkOrderEvaluationsService.delete({
                            id: eccpMaintenanceWorkOrderEvaluation.id
                        }).done(function () {
                            getEccpMaintenanceWorkOrderEvaluations(true);
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

        $('#CreateNewEccpMaintenanceWorkOrderEvaluationButton').click(function () {
            _createOrEditModal.open();
        });

		

        abp.event.on('app.createOrEditEccpMaintenanceWorkOrderEvaluationModalSaved', function () {
            getEccpMaintenanceWorkOrderEvaluations();
        });

		$('#GetEccpMaintenanceWorkOrderEvaluationsButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkOrderEvaluations();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEccpMaintenanceWorkOrderEvaluations();
		  }
		});

    });
})();