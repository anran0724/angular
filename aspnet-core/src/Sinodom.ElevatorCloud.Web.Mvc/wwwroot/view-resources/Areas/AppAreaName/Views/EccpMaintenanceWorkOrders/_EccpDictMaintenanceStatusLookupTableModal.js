(function ($) {
    app.modals.EccpDictMaintenanceStatusLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceWorkOrdersService = abp.services.app.eccpMaintenanceWorkOrders;
        var _$eccpDictMaintenanceStatusTable = $('#EccpDictMaintenanceStatusTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictMaintenanceStatusTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkOrdersService.getAllEccpDictMaintenanceStatusForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictMaintenanceStatusTableFilter').val()
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

        $('#EccpDictMaintenanceStatusTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictMaintenanceStatus() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictMaintenanceStatusButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceStatus();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictMaintenanceStatus();
            }
        });

    };
})(jQuery);

