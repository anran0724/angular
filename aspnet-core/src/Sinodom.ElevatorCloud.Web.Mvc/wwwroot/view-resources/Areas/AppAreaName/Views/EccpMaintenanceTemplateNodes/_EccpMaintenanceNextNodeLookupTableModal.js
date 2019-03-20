(function ($) {
    app.modals.EccpMaintenanceNextNodeLookupTableModal = function () {

        var _modalManager;

        var _eccpMaintenanceTemplateNodesService = abp.services.app.eccpMaintenanceTemplateNodes;
        var _$eccpMaintenanceNextNodeLookupTable = $('#EccpMaintenanceNextNodeLookupTable');

        var _args;

        this.init = function (modalManager, args) {
            _modalManager = modalManager;
            _args = args;
        };


        var dataTable = _$eccpMaintenanceNextNodeLookupTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _eccpMaintenanceTemplateNodesService.getAllEccpMaintenanceNextNodeForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#EccpMaintenanceNextNodeLookupTableFilter').val(),
                        maintenanceTemplateId: _args.maintenanceTemplateId
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
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "nodeIndex"
                }
            ]
        });

        $('#EccpMaintenanceNextNodeLookupTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getEccpMaintenanceNextNode() {
            dataTable.ajax.reload();
        }

        $('#GetEccpMaintenanceNextNodeLookupTableButton').click(function (e) {
            e.preventDefault();
            getEccpMaintenanceNextNode();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getEccpMaintenanceNextNode();
            }
        });

    };
})(jQuery);

