(function ($) {
    app.modals.EccpDictMaintenanceWorkFlowStatusLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceWorkFlowsService = abp.services.app.eccpMaintenanceWorkFlows;
        var _$eccpDictMaintenanceWorkFlowStatusTable = $('#EccpDictMaintenanceWorkFlowStatusTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$eccpDictMaintenanceWorkFlowStatusTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceWorkFlowsService.getAllEccpDictMaintenanceWorkFlowStatusForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpDictMaintenanceWorkFlowStatusTableFilter').val()
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

        $('#EccpDictMaintenanceWorkFlowStatusTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpDictMaintenanceWorkFlowStatus() {
            dataTable.ajax.reload();
        }

        $('#GetEccpDictMaintenanceWorkFlowStatusButton').click(function (e) {
            e.preventDefault();
            getEccpDictMaintenanceWorkFlowStatus();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpDictMaintenanceWorkFlowStatus();
            }
        });

    };
})(jQuery);

