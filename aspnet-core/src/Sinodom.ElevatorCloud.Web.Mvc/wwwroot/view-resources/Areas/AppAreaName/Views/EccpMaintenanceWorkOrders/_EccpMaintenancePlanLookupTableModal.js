(function ($) {
    app.modals.EccpMaintenancePlanLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;
        var _$eccpMaintenancePlanTable = $('#EccpMaintenancePlanTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpMaintenancePlanTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getAllEccpMaintenancePlanForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenancePlanTableFilter').val()
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

        $('#EccpMaintenancePlanTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenancePlan() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenancePlanButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenancePlan();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenancePlan();
            }
        });

    };
})(jQuery);

