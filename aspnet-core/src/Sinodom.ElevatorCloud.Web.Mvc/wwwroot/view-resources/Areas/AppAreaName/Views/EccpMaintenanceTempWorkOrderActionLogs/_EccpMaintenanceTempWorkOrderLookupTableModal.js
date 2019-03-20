(function ($) {
    app.modals.EccpMaintenanceTempWorkOrderLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTempWorkOrderActionLogsService = abp.services.app.eccpMaintenanceTempWorkOrderActionLogs;
        var _$eccpMaintenanceTempWorkOrderTable = $('#EccpMaintenanceTempWorkOrderTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpMaintenanceTempWorkOrderTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTempWorkOrderActionLogsService.getAllEccpMaintenanceTempWorkOrderForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceTempWorkOrderTableFilter').val()
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

        $('#EccpMaintenanceTempWorkOrderTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenanceTempWorkOrder() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceTempWorkOrderButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceTempWorkOrder();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceTempWorkOrder();
            }
        });

    };
})(jQuery);

