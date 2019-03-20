(function ($) {
    app.modals.EccpMaintenanceWorkOrderLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTroubledWorkOrdersService = abp.services.app.eccpMaintenanceTroubledWorkOrders;
        var _$eccpMaintenanceWorkOrderTable = $('#EccpMaintenanceWorkOrderTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpMaintenanceWorkOrderTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTroubledWorkOrdersService.getAllEccpMaintenanceWorkOrderForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceWorkOrderTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#EccpMaintenanceWorkOrderTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenanceWorkOrder() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceWorkOrderButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWorkOrder();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceWorkOrder();
            }
        });

    };
})(jQuery);

