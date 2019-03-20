(function ($) {
    app.modals.EccpMaintenanceWorkLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceWorkFlowsService = abp.services.app.eccpMaintenanceWorkFlows;
        var _$eccpMaintenanceWorkTable = $('#EccpMaintenanceWorkTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpMaintenanceWorkTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkFlowsService.getAllEccpMaintenanceWorkForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceWorkTableFilter').val()
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

        $('#EccpMaintenanceWorkTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenanceWork() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceWorkButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceWork();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceWork();
            }
        });

    };
})(jQuery);

