﻿(function () {

    app.modals.ViewEccpMaintenanceWorkOrderEvaluationsTableModal = function () {
        var _args;
        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;
        var _$eccpMaintenanceWorkOrderEvaluationsTable = $('#EccpMaintenanceWorkOrderEvaluationsTable');

        this.init = function (modalManager, args) {
            _args = args;
        };

        var dataTable = _$eccpMaintenanceWorkOrderEvaluationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getAllWorkOrderEvaluationByWorkOrderId,
                inputFilter: function () {
                    return {
                        maintenanceWorkOrderIdFilter: _args.workOrderId
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
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>'
                    }
                },
                {
                    targets: 1,
                    data: "eccpDictMaintenanceTypeName"
                },
                {
                    targets: 2,
                    data: "eccpMaintenanceWorkOrderComplateDate",
                    render: function (eccpMaintenanceWorkOrderComplateDate) {
                        if (eccpMaintenanceWorkOrderComplateDate) {
                            return moment(eccpMaintenanceWorkOrderComplateDate).format('L');
                        }
                        return "";
                    }
                },
                {
                    targets: 3,
                    data: "eccpMaintenanceWorkOrderEvaluation.rank"
                },
                {
                    targets: 4,
                    data: "eccpMaintenanceWorkOrderEvaluation.remarks"
                }
            ]
        });
    }

})();